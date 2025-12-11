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

        private static readonly string[] allowedExtentions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif",
".pdf", ".doc", ".docx", ".txt", ".rtf",
".cs", ".xcf", ".py" };

        private string _baseOutputDir;

        private StudentFileService _studentFileService;
        private readonly ArchiveExtractor _archiveExtractor = new ArchiveExtractor();
        private readonly SyntaxHighlighter _syntaxHighlighter;
        private readonly DocxTextExtractor _docxTextExtractor = new DocxTextExtractor();

        public Form1()
        {
            InitializeComponent();

            _syntaxHighlighter = new SyntaxHighlighter(CSharpKeywords, PythonKeywords);
            // TODO: ѕќ —“–≈Ћ ≈ ¬Ћ≈¬ќ ѕ≈–≈ходить на клетку с оценкой
            this.KeyPreview = true; // чтобы форма получала клавиши первой.[web:271]
            checkBoxSyntax.CheckedChanged += CheckBoxSyntax_CheckedChanged;
            dataGridViewStudentvsMark.CellContentClick += DataGridViewStudentvsMark_CellContentClick;
            listBoxFiles.SelectedIndexChanged += ListBoxFiles_SelectedIndexChanged;
            listBoxFiles.DoubleClick += ListBoxFiles_DoubleClick;
        }
        private void CheckBoxSyntax_CheckedChanged(object sender, EventArgs e)
        {
            // перерисовать текущий выбранный файл, если есть
            if (listBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }

        private async void MoveToPrevFileOrStudent()
        {
            // есть предыдущий файл
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

            int rowIndex = dataGridViewStudentvsMark.CurrentRow.Index;
            if (rowIndex <= 0)
            {
                // первый студент Ч мигнЄм
                await BlinkControlAsync(dataGridViewStudentvsMark);
                return;
            }

            int prevRowIndex = rowIndex - 1;

            dataGridViewStudentvsMark.ClearSelection();
            dataGridViewStudentvsMark.CurrentCell =
            dataGridViewStudentvsMark.Rows[prevRowIndex].Cells[0];
            dataGridViewStudentvsMark.Rows[prevRowIndex].Selected = true;

            LoadFilesForCurrentStudent();

            if (listBoxFiles.Items.Count > 0)
                listBoxFiles.SelectedIndex = listBoxFiles.Items.Count - 1;
        }

        private async void MoveToNextFileOrStudent()
        {
            // 1) ≈сли есть ещЄ файл в списке Ч просто выбрать его
            if (listBoxFiles.Items.Count > 0 &&
            listBoxFiles.SelectedIndex >= 0 &&
            listBoxFiles.SelectedIndex < listBoxFiles.Items.Count - 1)
            {
                listBoxFiles.SelectedIndex += 1;
                return;
            }

            // 2) ‘айлы закончились Ч переходим к следующему студенту
            if (dataGridViewStudentvsMark.CurrentRow == null)
            {
                await BlinkControlAsync(listBoxFiles); // совсем некуда
                return;

            }

            int rowIndex = dataGridViewStudentvsMark.CurrentRow.Index;
            if (rowIndex < 0 || rowIndex >= dataGridViewStudentvsMark.Rows.Count - 2)
            {
                await BlinkControlAsync(dataGridViewStudentvsMark);
                return; // текущий студент последний, дальше некуда
            }

            int nextRowIndex = rowIndex + 1;

            dataGridViewStudentvsMark.ClearSelection();
            dataGridViewStudentvsMark.CurrentCell =
            dataGridViewStudentvsMark.Rows[nextRowIndex].Cells[0];
            dataGridViewStudentvsMark.Rows[nextRowIndex].Selected = true;

            // загрузить файлы дл€ следующего студента и выбрать первый
            LoadFilesForCurrentStudent();
            if (listBoxFiles.Items.Count > 0)
                listBoxFiles.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                MoveToNextFileOrStudent();
                return true;
            }
            if (keyData == Keys.Up)
            {
                MoveToPrevFileOrStudent();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void ListBoxFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem is not FileItem item)
                return;

            string path = item.FullPath;

            if (!File.Exists(path))
            {
                MessageBox.Show("‘айл не найден: " + path);
                return;
            }

            try
            {
                // ќткрыть файлы в ассоциированном приложении (Word, VS, просмотрщик PDF и т.п.).[web:160][web:171]
                var psi = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ќе удалось открыть файл:\n" + ex.Message);
            }
        }

        private void buttonLoadZip_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "ZIP архивы (*.zip)|*.zip",
                Title = "¬ыберите архив с работами учеников"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // очищаем список студентов и файлов
                dataGridViewStudentvsMark.Rows.Clear(); // убрать старые строки.[web:195]
                listBoxFiles.Items.Clear();
                panelPreview.Controls.Clear();

                string archivePath = openFileDialog.FileName;

                labelArchivFileName.Text = archivePath;

                _baseOutputDir = Path.Combine(
                    Path.GetDirectoryName(archivePath)!,
                    Path.GetFileNameWithoutExtension(archivePath)); // папка дл€ всех работ[web:6]

                Directory.CreateDirectory(_baseOutputDir); // если есть Ч не упадЄт[web:12]

                try
                {
                    _archiveExtractor.ExtractPerStudent(archivePath, _baseOutputDir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ќшибка при распаковке архива: " + ex.Message);
                    return;
                }

                // здесь Ч уже чисто UI: заполн€ем таблицу студентов по папкам
                FillStudentsFromFolders();
                // создаем сервис
                _studentFileService = new StudentFileService(_baseOutputDir, allowedExtentions);
            }
        }

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
                dataGridViewStudentvsMark.ClearSelection();
                dataGridViewStudentvsMark.CurrentCell = dataGridViewStudentvsMark.Rows[0].Cells[0];
                dataGridViewStudentvsMark.Rows[0].Selected = true;
                LoadFilesForCurrentStudent();
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
            panelPreview.Controls.Clear();

            if (_studentFileService == null)
                return;

            if (!_studentFileService.TryGetStudentfiles(studentName, out var files))
            {
                // нет папки или нет файлов Ч аккуратно показали сообщение
                var textBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ReadOnly = true,
                    Font = new WinFont("Segoe UI", 12),
                    Text = "ƒл€ этого студента нет найденных файлов ответа."
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


            //            string studentFolder = Path.Combine(_baseOutputDir, studentName);


            //            // если папка отсутствует Ч просто очищаем всЄ и выходим
            //            if (!Directory.Exists(studentFolder)) // проверка существовани€ каталога.[web:303]
            //            {
            //                listBoxFiles.Items.Clear();
            //                panelPreview.Controls.Clear();
            //                TextBox textBox = new TextBox();
            //                textBox.Width = panelPreview.Width;
            //                textBox.Height = panelPreview.Height;
            //                textBox.Font = new WinFont(textBox.Font.FontFamily, 18);
            //                textBox.Text = "ƒл€ этого студента нет папки с ответом";
            //                panelPreview.Controls.Add(textBox);
            //                return;
            //            }

            //            var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif",
            //".pdf", ".doc", ".docx", ".txt", ".rtf",
            //".cs", ".xcf", ".py" };

            //            var files = Directory.GetFiles(studentFolder, "*.*", SearchOption.AllDirectories)
            //            .Where(f => allowedExt.Contains(Path.GetExtension(f).ToLowerInvariant()))
            //            .ToList();

            //            foreach (var f in files)
            //                listBoxFiles.Items.Add(new FileItem(f));

            //            if (listBoxFiles.Items.Count > 0)
            //            {
            //                listBoxFiles.SelectedIndex = 0;
            //            }
        }

        private void ShowFileInPanel(string filePath)
        {
            panelPreview.Controls.Clear();

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
                panelPreview.Controls.Add(pb); // вывод изображени€ в панели.[web:1][web:2]
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

                string plainText = _docxTextExtractor.ExtractPlainTextFromDocx(filePath); // свой метод извлечени€ текста
                rtb.Text = plainText;
                if (checkBoxSyntax.Checked)
                {
                    // подсвечиваем как C#
                    _syntaxHighlighter.HighlightCSharp(rtb); // тот же метод, что и дл€ .cs
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
                    rtb.Text = "\t\tѕросмотр невозможен. \nќткройте двойным кликом этот файл во нешнем приложении";
                else
                {
                    rtb.Text = File.ReadAllText(filePath); // показ содержимого текстового/CS файла.[web:16]
                    if (checkBoxSyntax.Checked)
                    {
                        if (ext == ".py")
                        {
                            _syntaxHighlighter.HighlightPython(rtb); // подсветка синтаксиса дл€ Python.[web:425][web:430]
                        }
                        else if (ext == ".cs")
                        {
                            _syntaxHighlighter.HighlightCSharp(rtb); // нова€ подсветка дл€ C#.[web:423]
                        }
                    }
                }
                panelPreview.Controls.Add(rtb);
            }
        }

        private void DataGridViewStudentvsMark_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_baseOutputDir == null)
            {
                MessageBox.Show("—начала загрузите архив.");
                return;
            }

            LoadFilesForCurrentStudent();
        }

        private void ListBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
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




