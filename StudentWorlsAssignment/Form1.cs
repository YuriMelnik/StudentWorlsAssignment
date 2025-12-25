using Microsoft.Extensions.Configuration;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using StudentWorlsAssignment.Models;
using StudentWorlsAssignment.Services;
using StudentWorlsAssignment.Ai;
using StudentWorlsAssignment.Services.TextRendering;
using System.Diagnostics;
using System.Media;
using System.Text;
using WBody = DocumentFormat.OpenXml.Wordprocessing.Body;
using WControl = System.Windows.Forms.Control;
using WinColor = System.Drawing.Color;
using WinFont = System.Drawing.Font;



namespace StudentWorlsAssignment
{
    public partial class Form1 : Form
    {
        private const int StudentNameColumnIndex = 0;
        private const int MarkColumnIndex = 1;

        private static readonly string[] CSharpKeywords =
        [
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "var"
        ];

        private static readonly string[] PythonKeywords =
        [
            "False", "None", "True", "and", "as", "assert", "async", "await", "break", "class", "continue", "def", "del",
            "elif", "else", "except", "finally", "for", "from", "global", "if", "import", "in", "is", "lambda",
            "nonlocal", "not", "or", "pass", "raise", "return", "try", "while", "with", "yield"
        ];

        private static readonly string[] AllowedExtentions =
        [
            ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".pdf", ".doc", ".docx", ".txt", ".rtf", ".cs", ".sln", ".xcf",
            ".py"
        ];

        private static readonly string[] ImageExtensions = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];

        private readonly ArchiveExtractor _archiveExtractor = new();
        private readonly SyntaxHighlighter _syntaxHighlighter =
            new SyntaxHighlighter(CSharpKeywords, PythonKeywords);
        private readonly DocxTextExtractor _docxTextExtractor = new();
        private readonly AiCodeReviewService _aiCodeReviewService;

        private string _baseOutputDir = string.Empty;
        private StudentFileService? _studentFileService;
        private RichTextBox? _previewRtb;
        private string _lastPreviewExt;
        private string _currentStudentName;

        public Form1()
        {
            InitializeComponent();

            // --- НАЧАЛО: НАСТРОЙКА AI СЕРВИСА ---
            // 1. Создаем билдер конфигурации
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ищем файл в папке с .exe
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // 2. Собираем конфигурацию в объект
            IConfigurationRoot configuration = configBuilder.Build();

            // 3. Получаем нужную нам секцию "AiSettings"
            var aiSettings = configuration.GetSection("AiSettings");

            // ... ваш код для чтения из appsettings.json ...
            var apiKey = aiSettings["ApiKey"];
            var endpoint = aiSettings["Endpoint"];
            var model = aiSettings["Model"]; // Убедитесь, что эта строка есть!

            // 5. Проверяем, что все настройки на месте
            if (string.IsNullOrEmpty(apiKey) 
                || string.IsNullOrEmpty(endpoint) 
                || string.IsNullOrEmpty(model)) 
            {
                MessageBox.Show(
                    "AI-сервис не настроен. Проверьте файл appsettings.json",
                    "Ошибка конфигурации", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                buttonAiReview.Enabled = false;  // Отключаем кнопку, если настройки неверны
            }

            // 6. Создаем HttpClient и наш сервис
            var httpClient = new HttpClient();
            _aiCodeReviewService = new AiCodeReviewService(httpClient, apiKey, endpoint, model);
            // --- КОНЕЦ: НАСТРОЙКА AI СЕРВИСА ---

            this.KeyPreview = true; // чтобы форма получала клавиши первой.[web:271]
            dataGridViewStudentvsMark.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridViewStudentvsMark.Columns[StudentNameColumnIndex].ReadOnly = true; // колонка с именем студента

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            //checkBoxSyntax.CheckedChanged += CheckBoxSyntax_CheckedChanged;
            dataGridViewStudentvsMark.CellContentClick += DataGridViewStudentvsMark_CellContentClick;
            checkedListBoxFiles.SelectedIndexChanged += checkedListBoxFiles_SelectedIndexChanged;
            checkedListBoxFiles.DoubleClick += checkedListBoxFiles_DoubleClick;
        }

        // Публичные/protected переопределения
        protected override bool ShowFocusCues => true;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    MoveToNextFileOrStudent();
                    return true;
                case Keys.Up:
                    MoveToPrevFileOrStudent();
                    return true;
                case Keys.Left:
                    if (dataGridViewStudentvsMark.CurrentRow != null)
                    {
                        dataGridViewStudentvsMark.Focus();
                        dataGridViewStudentvsMark.CurrentCell =
                            dataGridViewStudentvsMark.CurrentRow.Cells[StudentNameColumnIndex]; // имя студента
                    }
                    return true;

                case Keys.Right:
                    // сначала оценка
                    if (dataGridViewStudentvsMark.Focused &&
                        dataGridViewStudentvsMark.CurrentRow != null)
                    {
                        dataGridViewStudentvsMark.CurrentCell =
                            dataGridViewStudentvsMark.CurrentRow.Cells[MarkColumnIndex]; // оценка
                    }
                    else
                    {
                        // потом список файлов
                        if (checkedListBoxFiles.Items.Count > 0)
                        {
                            checkedListBoxFiles.Focus();
                            if (checkedListBoxFiles.SelectedIndex < 0)
                                checkedListBoxFiles.SelectedIndex = 0;
                        }
                    }
                    return true;
                case Keys.Enter:
                    // если фокус на списке файлов – открыть как по двойному клику
                    if (//checkedListBoxFiles.Focused &&
                        checkedListBoxFiles.SelectedItem is FileItem item &&
                        File.Exists(item.FullPath))
                    {
                        OpenFileInAssociatedApp(item.FullPath);
                        return true;
                    }
                    break;
                case Keys.Space:
                    // здесь ставим/снимаем чек у файла, а в грид событие не пускаем
                    ToggleCurrentFileCheck(); // твоя логика чекбокса/флага
                    return true;              // важное: блокируем стандартную обработку
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToggleCurrentFileCheck()
        {
            int index = checkedListBoxFiles.SelectedIndex;
            if (index < 0 || index >= checkedListBoxFiles.Items.Count)
                return;
            bool isChecked = checkedListBoxFiles.GetItemChecked(index);
            checkedListBoxFiles.SetItemChecked(index, !isChecked);
        }

        // Обработчики событий UI
        private void buttonLoadZip_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "ZIP архивы (*.zip)|*.zip",
                Title = "Выберите архив с работами учеников"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            // очищаем список студентов и файлов
            dataGridViewStudentvsMark.Rows.Clear(); // убрать старые строки.[web:195]
            checkedListBoxFiles.Items.Clear();
            ClearPreview();

            string archivePath = openFileDialog.FileName;

            labelArchivFileName.Text = archivePath;

            _baseOutputDir = Path.Combine(
                Path.GetDirectoryName(archivePath)!,
                Path.GetFileNameWithoutExtension(archivePath)); // папка для всех работ[web:6]

            Directory.CreateDirectory(_baseOutputDir); // если есть — не упадёт[web:12]

            try
            {
                _archiveExtractor.ExtractPerStudent(archivePath, _baseOutputDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при распаковке архива: " + ex.Message);
                return;
            }

            // создаем сервис
            _studentFileService = new StudentFileService(_baseOutputDir, AllowedExtentions);

            // здесь — уже чисто UI: заполняем таблицу студентов по папкам
            FillStudentsFromFolders();
        }
        private void CheckBoxSyntax_CheckedChanged(object? sender, EventArgs e)
        {
            // перерисовать текущий выбранный файл, если есть
            if (checkedListBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }
        private void DataGridViewStudentvsMark_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_baseOutputDir == null)
            {
                MessageBox.Show("Сначала загрузите архив.");
                return;
            }

            LoadFilesForCurrentStudent();
        }
        private void checkedListBoxFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (checkedListBoxFiles.SelectedItem is not FileItem item)
                return;

            OpenFileInAssociatedApp(item.FullPath);

        }
        private void checkedListBoxFiles_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (checkedListBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }
        private async void buttonAiReview_Click(object sender, EventArgs e)
        {
            var filesToReview = checkedListBoxFiles.CheckedItems
                .Cast<FileItem>()
                .ToList();

            if (filesToReview.Count == 0)
            {
                MessageBox.Show("Отметьте хотя бы один файл для AI‑проверки.");
                return;
            }

            // ВАЖНО: Вам нужно откуда-то взять текст задания.
            // Например, из TextBox на форме.
            string assignmentDescription = textBoxAssignmentDescription.Text;
            if (string.IsNullOrWhiteSpace(assignmentDescription))
            {
                if (MessageBox.Show(
                    "Введите условие задачи для оценки.", 
                    "Будем вводить условие?", 
                    MessageBoxButtons.YesNo) 
                    == DialogResult.Yes)
                return;
            }

            string studentName = _currentStudentName;

            // Создаем запрос, включая описание задания
            var request = new AiReviewRequest(studentName, filesToReview, assignmentDescription);

            // Создаем и показываем форму ревью
            using var dlg = new AiReviewForm(request, _aiCodeReviewService, _docxTextExtractor);
            dlg.ShowDialog(this); // ShowDialog делает форму модальной
        }


        // Загрузка данных
        private void FillStudentsFromFolders()
        {
            dataGridViewStudentvsMark.Rows.Clear();

            if (string.IsNullOrEmpty(_baseOutputDir))
                return;

            var studentFolders = Directory.GetDirectories(_baseOutputDir);

            foreach (var folder in studentFolders)
            {
                string studentName = Path.GetFileName(folder);
                int rowIndex = dataGridViewStudentvsMark.Rows.Add();
                dataGridViewStudentvsMark.Rows[rowIndex].Cells[StudentNameColumnIndex].Value = studentName;
            }
            if (dataGridViewStudentvsMark.Rows.Count > 0)
            {
                // выбираем первого студента
                dataGridViewStudentvsMark.ClearSelection();
                var firstRow = dataGridViewStudentvsMark.Rows[0];

                dataGridViewStudentvsMark.CurrentCell = firstRow.Cells[StudentNameColumnIndex];
                firstRow.Cells[StudentNameColumnIndex].Selected = true;

                // ставим фокус в таблицу
                dataGridViewStudentvsMark.Focus();

                // загружаем файлы и показываем первый
                LoadFilesForCurrentStudent();

                if (checkedListBoxFiles.Items.Count > 0 &&
                    checkedListBoxFiles.SelectedIndex >= 0 &&
                    checkedListBoxFiles.SelectedItem is FileItem item &&
                    File.Exists(item.FullPath))
                {
                    ShowFileInPanel(item.FullPath);
                }
            }
        }
        private void LoadFilesForCurrentStudent()
        {
            if (dataGridViewStudentvsMark.CurrentRow == null)
                return;

            var cellValue = dataGridViewStudentvsMark.CurrentRow.Cells[StudentNameColumnIndex].Value;
            if (cellValue == null)
                return;

            string studentName = cellValue?.ToString() ?? string.Empty;
            _currentStudentName = studentName;

            if (string.IsNullOrWhiteSpace(studentName))
                return;

            LoadStudentFilesToListBox(studentName);
        }
        private void LoadStudentFilesToListBox(string studentName)
        {
            checkedListBoxFiles.Items.Clear();
            ClearPreview();

            if (_studentFileService == null)
                return;

            if (!_studentFileService.TryGetStudentfiles(studentName, out var files))
            {
                // нет папки или нет файлов — аккуратно показали сообщение
                var textBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ReadOnly = true,
                    Font = new WinFont("Segoe UI", 12),
                    Text = "Для этого студента нет найденных файлов ответа."
                };
                panelPreview.Controls.Add(textBox);
                return;
            }
            foreach (var file in files)
            {
                checkedListBoxFiles.Items.Add(file);
            }

            if (checkedListBoxFiles.Items.Count > 0)
                checkedListBoxFiles.SelectedIndex = 0;

        }

        private void ShowAiFeedback(string feedback)
        {
            var form = new Form
            {
                Text = "AI-отзыв по коду",
                StartPosition = FormStartPosition.CenterParent,
                Width = 800,
                Height = 600
            };

            var rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new WinFont("Consolas", 10),
                WordWrap = true,
                Text = feedback
            };

            form.Controls.Add(rtb);
            form.ShowDialog(this);
        }

        // Навигация по студентам/файлам
        private async void MoveToPrevFileOrStudent()
        {
            // часть с checkedListBoxFiles оставляем как есть
            if (checkedListBoxFiles.Items.Count > 0 &&
                checkedListBoxFiles.SelectedIndex > 0)
            {
                checkedListBoxFiles.SelectedIndex -= 1;
                return;
            }

            if (dataGridViewStudentvsMark.CurrentRow == null)
            {
                await BlinkControlAsync(checkedListBoxFiles);
                return;
            }

            int currentCol = dataGridViewStudentvsMark.CurrentCell?.ColumnIndex ?? 0;
            int rowIndex = dataGridViewStudentvsMark.CurrentRow.Index;

            if (rowIndex <= 0)
            {
                await BlinkControlAsync(dataGridViewStudentvsMark);
                return;
            }

            int prevRowIndex = rowIndex - 1;

            dataGridViewStudentvsMark.ClearSelection();
            dataGridViewStudentvsMark.CurrentCell =
                dataGridViewStudentvsMark.Rows[prevRowIndex].Cells[currentCol];
            dataGridViewStudentvsMark.Rows[prevRowIndex].Cells[currentCol].Selected = true;

            LoadFilesForCurrentStudent();

            if (checkedListBoxFiles.Items.Count > 0)
                checkedListBoxFiles.SelectedIndex = checkedListBoxFiles.Items.Count - 1;
        }
        private async void MoveToNextFileOrStudent()
        {
            // 1) сначала листаем файлы
            if (checkedListBoxFiles.Items.Count > 0 &&
                checkedListBoxFiles.SelectedIndex >= 0 &&
                checkedListBoxFiles.SelectedIndex < checkedListBoxFiles.Items.Count - 1)
            {
                checkedListBoxFiles.SelectedIndex += 1;
                return;
            }

            if (dataGridViewStudentvsMark.CurrentRow == null)
            {
                await BlinkControlAsync(checkedListBoxFiles);
                return;
            }

            int currentCol = dataGridViewStudentvsMark.CurrentCell?.ColumnIndex ?? 0;
            int rowIndex = dataGridViewStudentvsMark.CurrentRow.Index;

            if (rowIndex < 0 || rowIndex >= dataGridViewStudentvsMark.Rows.Count - 2)
            {
                await BlinkControlAsync(dataGridViewStudentvsMark);
                return;
            }

            int nextRowIndex = rowIndex + 1;

            dataGridViewStudentvsMark.ClearSelection();
            dataGridViewStudentvsMark.CurrentCell =
                dataGridViewStudentvsMark.Rows[nextRowIndex].Cells[currentCol];
            dataGridViewStudentvsMark.Rows[nextRowIndex].Cells[currentCol].Selected = true;

            LoadFilesForCurrentStudent();
            if (checkedListBoxFiles.Items.Count > 0)
                checkedListBoxFiles.SelectedIndex = 0;
        }

        // Предпросмотр файлов
        private void ShowFileInPanel(string filePath)
        {
            ClearPreview();

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            _lastPreviewExt = ext;

            if (IsImageExt(ext))
            {
                ShowImagePreview(filePath);
            }

            else ShowDocumentPreview(filePath, ext);
        }

        private static bool IsImageExt(string ext) => ImageExtensions.Contains(ext);

        private void ShowImagePreview(string filePath)
        {
            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackgroundImage = Properties.Resources.chess800,
                Image = Image.FromFile(filePath)
            };
            panelPreview.Controls.Add(pb); // вывод изображения в панели.[web:1][web:2]
            _previewRtb = null; // сейчас нет текстового предпросмотра
        }

        private void ShowDocumentPreview(string filePath, string ext)
        {
            var rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new WinFont("Consolas", 10),
                WordWrap = false
            };
            rtb.ScrollBars = RichTextBoxScrollBars.Both;

            if (ext == ".docx")
            {
                string plainText = _docxTextExtractor.ExtractPlainTextFromDocx(filePath);
                rtb.Text = plainText;
            }
            else if (ext == ".rtf")
            {
                rtb.LoadFile(filePath, RichTextBoxStreamType.RichText);
            }
            else if (ext == ".doc" || ext == ".pdf")
            {
                rtb.Text = "\t\tПросмотр невозможен. \nОткройте двойным кликом этот файл во нешнем приложении";
            }
            else
            {
                rtb.Text = ReadTextWithUTF8orANSI(filePath); // показ содержимого текстового/CS файла.[web:16]
            }
            panelPreview.Controls.Add(rtb);
            _previewRtb = rtb;

            ApplyHighlightingForCurrentFile(ext);
        }


        private void ApplyHighlightingForCurrentFile(string ext)
        {
            // Нечего подсвечивать
            if (_previewRtb is null)
                return;

            // Режим "без подсветки"
            if (rbNoHighlight.Checked)
                return;

            // Сбросить предыдущее форматирование
            _previewRtb.SuspendLayout();
            _previewRtb.Select(0, _previewRtb.TextLength);
            _previewRtb.SelectionColor = WinColor.Black;
            _previewRtb.Select(0, 0);
            _previewRtb.ResumeLayout();

            // Выбор подсветки по радиокнопкам + расширению файла
            if (rbCSharp.Checked)
            {
                _syntaxHighlighter.HighlightCSharp(_previewRtb);
            }
            else if (rbPython.Checked)
            {
                _syntaxHighlighter.HighlightPython(_previewRtb);
            }
            // для остальных комбинаций (например, .txt) подсветка не применяется
        }

        private void ClearPreview()
        {
            foreach (System.Windows.Forms.Control ctrl in panelPreview.Controls)
            {
                if (ctrl is PictureBox pb && pb.Image != null)
                {
                    pb.Image.Dispose();
                    pb.Image = null;
                }
                ctrl.Dispose();
            }
            // очищаем панель предпросмотра
            panelPreview.Controls.Clear();
            _previewRtb = null;
        }

        // Прочие вспомогательные методы
        private static string ReadTextWithUTF8orANSI(string filePath)
        {
            // пытаемся прочитать в UTF-8
            byte[] bytes = File.ReadAllBytes(filePath);

            var utf8Strict = new UTF8Encoding(false, true); // без BOM, с ошибкой при невалидных байтах

            try
            {
                return utf8Strict.GetString(bytes); // сработает и для UTF‑8 с BOM, и без BOM
            }
            catch (DecoderFallbackException)
            {
                return Encoding.GetEncoding(1251).GetString(bytes); // ANSI (Windows‑1251)
            }
        }

        private void OpenFileInAssociatedApp(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Файл не найден: " + path);
                return;
            }

            try
            {
                var psi = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть файл:\n" + ex.Message);
            }
        }

        private async Task BlinkControlAsync(WControl ctrl)
        {
            var oldColor = ctrl.BackColor;
            ctrl.BackColor = WinColor.LightPink;
            SystemSounds.Beep.Play(); // короткий системный звук.[web:283][web:289]
            await Task.Delay(150);
            ctrl.BackColor = oldColor;
        }

        private void rbHighlightMode_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not RadioButton rb || !rb.Checked || _previewRtb is null)
                return;  // реагируем только когда кнопка стала выбранной

            ApplyHighlightingForCurrentFile(_lastPreviewExt);
        }
    }

}