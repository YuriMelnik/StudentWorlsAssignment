using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using StudentWorlsAssignment.Models;
using StudentWorlsAssignment.Services;
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

        private static readonly string[] CSharpKeywords =
        {
"abstract","as","base","bool","break","byte","case","catch","char","checked",
"class","const","continue","decimal","default","delegate","do","double","else",
"enum","event","explicit","extern","false","finally","fixed","float","for",
"foreach","goto","if","implicit","in","int","interface","internal","is","lock",
"long","namespace","new","null","object","operator","out","override","params",
"private","protected","public","readonly","ref","return","sbyte","sealed",
"short","sizeof","stackalloc","static","string","struct","switch","this",
"throw","true","try","typeof","uint","ulong","unchecked","unsafe","ushort",
"using","virtual","void","volatile","while","var"
};

        private static readonly string[] PythonKeywords =
        {
"False", "None", "True", "and", "as", "assert", "async", "await",
"break", "class", "continue", "def", "del", "elif", "else", "except",
"finally", "for", "from", "global", "if", "import", "in", "is",
"lambda", "nonlocal", "not", "or", "pass", "raise", "return",
"try", "while", "with", "yield"
};

        private static readonly string[] АllowedExtentions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif",
".pdf", ".doc", ".docx", ".txt", ".rtf",
".cs", ".xcf", ".py" };

        private readonly ArchiveExtractor _archiveExtractor = new();
        private readonly SyntaxHighlighter _syntaxHighlighter;
        private readonly DocxTextExtractor _docxTextExtractor = new();
        private readonly AiCodeReviewService _aiCodeReviewService;

        private string _baseOutputDir;
        private StudentFileService? _studentFileService;

        public Form1()
        {
            InitializeComponent();

            var httpClient = new HttpClient();
            _aiCodeReviewService = new AiCodeReviewService(httpClient);

            _syntaxHighlighter = new SyntaxHighlighter(CSharpKeywords, PythonKeywords);
            this.KeyPreview = true; // чтобы форма получала клавиши первой.[web:271]

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            checkBoxSyntax.CheckedChanged += CheckBoxSyntax_CheckedChanged;
            dataGridViewStudentvsMark.CellContentClick += DataGridViewStudentvsMark_CellContentClick;
            listBoxFiles.SelectedIndexChanged += ListBoxFiles_SelectedIndexChanged;
            listBoxFiles.DoubleClick += ListBoxFiles_DoubleClick;
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
                            dataGridViewStudentvsMark.CurrentRow.Cells[0]; // имя студента
                    }
                    return true;

                case Keys.Right:
                    // сначала оценка
                    if (dataGridViewStudentvsMark.Focused &&
                        dataGridViewStudentvsMark.CurrentRow != null)
                    {
                        dataGridViewStudentvsMark.CurrentCell =
                            dataGridViewStudentvsMark.CurrentRow.Cells[1]; // оценка
                    }
                    else
                    {
                        // потом список файлов
                        if (listBoxFiles.Items.Count > 0)
                        {
                            listBoxFiles.Focus();
                            if (listBoxFiles.SelectedIndex < 0)
                                listBoxFiles.SelectedIndex = 0;
                        }
                    }
                    return true;
                case Keys.Enter:
                    // если фокус на списке файлов – открыть как по двойному клику
                    if (//listBoxFiles.Focused &&
                        listBoxFiles.SelectedItem is FileItem item &&
                        File.Exists(item.FullPath))
                    {
                        OpenFileInAssociatedApp(item.FullPath);
                        return true;
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
            listBoxFiles.Items.Clear();
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
            _studentFileService = new StudentFileService(_baseOutputDir, АllowedExtentions);

            // здесь — уже чисто UI: заполняем таблицу студентов по папкам
            FillStudentsFromFolders();
        }
        private void CheckBoxSyntax_CheckedChanged(object sender, EventArgs e)
        {
            // перерисовать текущий выбранный файл, если есть
            if (listBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }
        private void DataGridViewStudentvsMark_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_baseOutputDir == null)
            {
                MessageBox.Show("Сначала загрузите архив.");
                return;
            }

            LoadFilesForCurrentStudent();
        }
        private void ListBoxFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem is not FileItem item)
                return;

            OpenFileInAssociatedApp(item.FullPath);

        }
        private void ListBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }
        private async void buttonAiReview_Click(object sender, EventArgs e)
        {
            if (panelPreview.Controls.OfType<RichTextBox>().FirstOrDefault() is not RichTextBox rtb)
            {
                MessageBox.Show("Сначала выберите текстовый файл или DOCX с кодом.");
                return;
            }

            string code = rtb.Text;
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Текст для анализа пуст.");
                return;
            }

            buttonAiReview.Enabled = false;

            try
            {
                string feedback = await _aiCodeReviewService.ReviewAsync(code);
                ShowAiFeedback(feedback);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запросе к AI-помощнику:\n" + ex.Message);
            }
            finally
            {
                buttonAiReview.Enabled = true;
            }
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
                dataGridViewStudentvsMark.Rows[rowIndex].Cells[0].Value = studentName;
            }
            if (dataGridViewStudentvsMark.Rows.Count > 0)
            {
                // выбираем первого студента
                dataGridViewStudentvsMark.ClearSelection();
                var firstRow = dataGridViewStudentvsMark.Rows[0];

                dataGridViewStudentvsMark.CurrentCell = firstRow.Cells[0];
                firstRow.Cells[0].Selected = true;

                // ставим фокус в таблицу
                dataGridViewStudentvsMark.Focus();

                // загружаем файлы и показываем первый
                LoadFilesForCurrentStudent();

                if (listBoxFiles.Items.Count > 0 &&
                    listBoxFiles.SelectedIndex >= 0 &&
                    listBoxFiles.SelectedItem is FileItem item &&
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

            var cellValue = dataGridViewStudentvsMark.CurrentRow.Cells[0].Value;
            if (cellValue == null)
                return;

            string studentName = cellValue.ToString();
            if (string.IsNullOrWhiteSpace(studentName))
                return;

            LoadStudentFilesToListBox(studentName);
        }
        private void LoadStudentFilesToListBox(string studentName)
        {
            listBoxFiles.Items.Clear();
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
                listBoxFiles.Items.Add(file);
            }

            if (listBoxFiles.Items.Count > 0)
                listBoxFiles.SelectedIndex = 0;

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
            // часть с listBoxFiles оставляем как есть
            if (listBoxFiles.Items.Count > 0 &&
                listBoxFiles.SelectedIndex > 0)
            {
                listBoxFiles.SelectedIndex -= 1;
                return;
            }

            if (dataGridViewStudentvsMark.CurrentRow == null)
            {
                await BlinkControlAsync(listBoxFiles);
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

            if (listBoxFiles.Items.Count > 0)
                listBoxFiles.SelectedIndex = listBoxFiles.Items.Count - 1;
        }
        private async void MoveToNextFileOrStudent()
        {
            // 1) сначала листаем файлы
            if (listBoxFiles.Items.Count > 0 &&
                listBoxFiles.SelectedIndex >= 0 &&
                listBoxFiles.SelectedIndex < listBoxFiles.Items.Count - 1)
            {
                listBoxFiles.SelectedIndex += 1;
                return;
            }

            if (dataGridViewStudentvsMark.CurrentRow == null)
            {
                await BlinkControlAsync(listBoxFiles);
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
            if (listBoxFiles.Items.Count > 0)
                listBoxFiles.SelectedIndex = 0;
        }

        // Предпросмотр файлов
        private void ShowFileInPanel(string filePath)
        {
            ClearPreview();

            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            if (new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" }.Contains(ext))
            {
                var pb = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackgroundImage = Properties.Resources.chess800,
                    Image = Image.FromFile(filePath)
                };
                panelPreview.Controls.Add(pb); // вывод изображения в панели.[web:1][web:2]
            }

            else if (ext == ".docx") // || ext == )
            {
                var rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Font = new WinFont("Consolas", 10),
                    WordWrap = false
                };

                string plainText = _docxTextExtractor.ExtractPlainTextFromDocx(filePath); // свой метод извлечения текста
                rtb.Text = plainText;
                if (checkBoxSyntax.Checked)
                {
                    // подсвечиваем как C#
                    _syntaxHighlighter.HighlightCSharp(rtb); // тот же метод, что и для .cs
                }
                panelPreview.Controls.Add(rtb);
            }

            else if (new[] { ".txt", ".rtf", ".doc", ".pdf", ".cs", ".py" }.Contains(ext))
            {
                var rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Font = new WinFont("Consolas", 10),
                    WordWrap = false
                };

                if (ext == ".rtf")
                    rtb.LoadFile(filePath, RichTextBoxStreamType.RichText);
                else if (ext == ".doc" || ext == ".pdf")
                    rtb.Text = "\t\tПросмотр невозможен. \nОткройте двойным кликом этот файл во нешнем приложении";
                else
                {
                    rtb.Text = File.ReadAllText(filePath); // показ содержимого текстового/CS файла.[web:16]
                    if (checkBoxSyntax.Checked)
                    {
                        if (ext == ".py")
                        {
                            _syntaxHighlighter.HighlightPython(rtb); // подсветка синтаксиса для Python.[web:425][web:430]
                        }
                        else if (ext == ".cs")
                        {
                            _syntaxHighlighter.HighlightCSharp(rtb); // новая подсветка для C#.[web:423]
                        }
                    }
                }
                panelPreview.Controls.Add(rtb);
            }
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

            panelPreview.Controls.Clear();
        }

        // Прочие вспомогательные методы
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


    }

}