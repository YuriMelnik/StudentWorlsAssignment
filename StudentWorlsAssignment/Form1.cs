using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.Diagnostics;
using System.IO.Compression;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
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


        private string _baseOutputDir;

        public Form1()
        {
            InitializeComponent();

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
                // первый студент — мигнём
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
            // 1) Если есть ещё файл в списке — просто выбрать его
            if (listBoxFiles.Items.Count > 0 &&
            listBoxFiles.SelectedIndex >= 0 &&
            listBoxFiles.SelectedIndex < listBoxFiles.Items.Count - 1)
            {
                listBoxFiles.SelectedIndex += 1;
                return;
            }

            // 2) Файлы закончились — переходим к следующему студенту
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

            // загрузить файлы для следующего студента и выбрать первый
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
                MessageBox.Show("Файл не найден: " + path);
                return;
            }

            try
            {
                // Открыть файлы в ассоциированном приложении (Word, VS, просмотрщик PDF и т.п.).[web:160][web:171]
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

        private void buttonLoadZip_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "ZIP архивы (*.zip)|*.zip",
                Title = "Выберите архив с работами учеников"
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
                Path.GetDirectoryName(archivePath),
                Path.GetFileNameWithoutExtension(archivePath)); // папка для всех работ[web:6]

                Directory.CreateDirectory(_baseOutputDir); // если есть — не упадёт[web:12]

                ExtractArchivePerStudent(archivePath, _baseOutputDir);
            }
        }

        private void ExtractArchivePerStudent(string archivePath, string baseOutputDir)
        {
            string ext = Path.GetExtension(archivePath).ToLowerInvariant();

            if (ext == ".zip")
            {
                // Открываем общий ZIP-архив с работами
                using (ZipArchive archive = ZipFile.OpenRead(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        // Пропускаем "папки" внутри архива
                        if (string.IsNullOrEmpty(entry.Name))
                            continue;

                        // Имя файла внутри архива, например "ivanov.zip" или "petrov.rar"
                        string innerFileName = entry.Name;
                        string studentName = Path.GetFileNameWithoutExtension(innerFileName); // "ivanov"[web:29]
                        studentName = studentName.Split(['-', '_'])[0];

                        // добавляю студента в список, если его ещё нет
                        bool exists = dataGridViewStudentvsMark.Rows
                        .Cast<DataGridViewRow>()
                        .Any(r => (r.Cells[0].Value?.ToString() ?? "") == studentName);

                        if (!exists)
                        {
                            // добавляю студента в список
                            int rowIndex = dataGridViewStudentvsMark.Rows.Add();
                            dataGridViewStudentvsMark.Rows[rowIndex].Cells[0].Value = studentName;
                        }
                        // может и не надо папку. в проектах и так есть папка, а простые задания просто файлы с именем студента
                        // -- надо, в ответе может быть несколько файлов
                        // Папка для конкретного студента
                        string studentFolder = Path.Combine(baseOutputDir, studentName);
                        Directory.CreateDirectory(studentFolder);

                        // Временный путь для вложенного архива/файла
                        string tempPath = Path.Combine(baseOutputDir, innerFileName);

                        // Сохраняем вложенный файл из общего архива
                        entry.ExtractToFile(tempPath, overwrite: true);

                        // Определяем, что за файл внутри: zip, rar или просто файл
                        string innerExt = Path.GetExtension(tempPath).ToLowerInvariant();

                        if (innerExt == ".zip")
                        {
                            // Внутри ещё один zip — распаковываем его в папку студента
                            ZipFile.ExtractToDirectory(tempPath, studentFolder, overwriteFiles: true);
                            File.Delete(tempPath);
                        }
                        else if (innerExt == ".rar")
                        {
                            // Внутри rar — распаковываем через SharpCompress в папку студента
                            using (var rar = RarArchive.Open(tempPath))
                            {
                                foreach (var rarEntry in rar.Entries)
                                {
                                    // SharpCompress сам создаст подпапки под studentFolder.[web:243][web:246]
                                    rarEntry.WriteToDirectory(studentFolder, new ExtractionOptions
                                    {
                                        ExtractFullPath = true,
                                        Overwrite = true
                                    });
                                }
                            }
                            File.Delete(tempPath);
                        }
                        else
                        {
                            // Это просто файл (например, один .csproj, .docx и т.п.)
                            string destPath = Path.Combine(studentFolder, innerFileName);
                            if (File.Exists(destPath))
                                File.Delete(destPath);

                            File.Move(tempPath, destPath);
                        }
                    }
                }
            }
            else if (ext == ".rar")
            {
                // Если верхний архив уже RAR (общий архив со всеми работами)
                using (var archive = RarArchive.Open(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.IsDirectory)
                            continue;

                        string innerFileName = Path.GetFileName(entry.Key);
                        string studentName = Path.GetFileNameWithoutExtension(innerFileName);

                        string studentFolder = Path.Combine(baseOutputDir, studentName);
                        Directory.CreateDirectory(studentFolder);

                        string destPath = Path.Combine(studentFolder, innerFileName);
                        string destDir = Path.GetDirectoryName(destPath);
                        if (!string.IsNullOrEmpty(destDir))
                            Directory.CreateDirectory(destDir);

                        entry.WriteToFile(destPath, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show("Поддерживаются только ZIP и RAR архивы.");
            }
            if (dataGridViewStudentvsMark.Rows.Count > 0)
            {
                dataGridViewStudentvsMark.ClearSelection();
                dataGridViewStudentvsMark.CurrentCell =
                dataGridViewStudentvsMark.Rows[0].Cells[0];
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
            if (string.IsNullOrEmpty(_baseOutputDir))
                return;

            string studentFolder = Path.Combine(_baseOutputDir, studentName);


            // если папка отсутствует — просто очищаем всё и выходим
            if (!Directory.Exists(studentFolder)) // проверка существования каталога.[web:303]
            {
                listBoxFiles.Items.Clear();
                panelPreview.Controls.Clear();
                TextBox textBox = new TextBox();
                textBox.Width = panelPreview.Width;
                textBox.Height = panelPreview.Height;
                textBox.Font = new WinFont(textBox.Font.FontFamily, 18);
                textBox.Text = "Для этого студента нет папки с ответом";
                panelPreview.Controls.Add(textBox);
                return;
            }

            var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif",
".pdf", ".doc", ".docx", ".txt", ".rtf",
".cs", ".xcf", ".py" };

            var files = Directory.GetFiles(studentFolder, "*.*", SearchOption.AllDirectories)
            .Where(f => allowedExt.Contains(Path.GetExtension(f).ToLowerInvariant()))
            .ToList();

            foreach (var f in files)
                listBoxFiles.Items.Add(new FileItem(f));

            if (listBoxFiles.Items.Count > 0)
            {
                listBoxFiles.SelectedIndex = 0;
            }
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
                panelPreview.Controls.Add(pb); // вывод изображения в панели.[web:1][web:2]
            }
            //else if (new[] { ".pdf", ".doc", ".docx" }.Contains(ext))
            //{
            // var wb = new WebBrowser
            // {
            // Dock = DockStyle.Fill
            // };
            // wb.Navigate(filePath); // Office/PDF в WebBrowser, если установлен соответствующий просмотрщик.[web:6][web:9]
            // panelPreview.Controls.Add(wb);
            //}
            else if (ext == ".docx") // || ext == )
            {
                var rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Font = new WinFont("Consolas", 10),
                    WordWrap = false
                };

                string plainText = ExtractDocxText(filePath); // свой метод извлечения текста
                rtb.Text = plainText;
                if (checkBoxSyntax.Checked)
                {
                    // подсвечиваем как C#
                    HighlightCSharp(rtb); // тот же метод, что и для .cs
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
                            HighlightPython(rtb); // подсветка синтаксиса для Python.[web:425][web:430]
                        }
                        else if (ext == ".cs")
                        {
                            HighlightCSharp(rtb); // новая подсветка для C#.[web:423]
                        }
                    }
                }
                panelPreview.Controls.Add(rtb);
            }
        }

        private void HighlightPython(RichTextBox rtb)
        {
            // отключаем перерисовку для уменьшения мерцания
            rtb.SuspendLayout();

            // сброс цвета
            rtb.Select(0, rtb.TextLength);
            rtb.SelectionColor = System.Drawing.Color.Black;

            string text = rtb.Text;

            // комментарии: с # до конца строки
            var commentRegex = new Regex(@"#.*$", RegexOptions.Multiline);
            foreach (Match m in commentRegex.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = System.Drawing.Color.Green;
            }

            // строковые литералы: "..." или '...'
            var stringRegex = new Regex("\"[^\"]*\"|'[^']*'");
            foreach (Match m in stringRegex.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = System.Drawing.Color.Brown;
            }

            // ключевые слова (грубовато, но рабоче)
            foreach (string kw in PythonKeywords)
            {
                var kwRegex = new Regex(@"\b" + Regex.Escape(kw) + @"\b");
                foreach (Match m in kwRegex.Matches(text))
                {
                    rtb.Select(m.Index, m.Length);
                    rtb.SelectionColor = System.Drawing.Color.OrangeRed;
                }
            }

            // возвращаем курсор в начало (опционально)
            rtb.Select(0, 0);
            rtb.SelectionColor = System.Drawing.Color.Black;

            rtb.ResumeLayout();
        }

        private void HighlightCSharp(RichTextBox rtb)
        {
            rtb.SuspendLayout();

            rtb.Select(0, rtb.TextLength);
            rtb.SelectionColor = System.Drawing.Color.Black;

            string text = rtb.Text;

            // комментарии //...
            var lineCommentRegex = new Regex(@"//.*$", RegexOptions.Multiline);
            foreach (Match m in lineCommentRegex.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = System.Drawing.Color.Green;
            }

            // многострочные комментарии /* ... */
            var blockCommentRegex = new Regex(@"/\*.*?\*/", RegexOptions.Singleline);
            foreach (Match m in blockCommentRegex.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = System.Drawing.Color.Green;
            }

            // строковые литералы "..."
            var stringRegex = new Regex("\"[^\"]*\"");
            foreach (Match m in stringRegex.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = System.Drawing.Color.Brown;
            }

            // ключевые слова
            foreach (string kw in CSharpKeywords)
            {
                var kwRegex = new Regex(@"\b" + Regex.Escape(kw) + @"\b");
                foreach (Match m in kwRegex.Matches(text))
                {
                    rtb.Select(m.Index, m.Length);
                    rtb.SelectionColor = System.Drawing.Color.Blue;
                }
            }

            rtb.Select(0, 0);
            rtb.SelectionColor = System.Drawing.Color.Black;

            rtb.ResumeLayout();
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

        private void ListBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem is FileItem item && File.Exists(item.FullPath))
            {
                ShowFileInPanel(item.FullPath);
            }
        }

        private string ExtractDocxText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return string.Empty;

            var sb = new StringBuilder();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                WBody body = wordDoc.MainDocumentPart.Document.Body;
                if (body == null)
                    return string.Empty;

                foreach (Paragraph p in body.Descendants<Paragraph>()) // проход по абзацам.[web:94][web:117]
                {
                    string text = p.InnerText;
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        sb.AppendLine(text);
                    }
                }
            }

            return sb.ToString();
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
    public class FileItem
    {
        public string FullPath { get; }

        public FileItem(string fullPath)
        {
            FullPath = fullPath;
        }

        public override string ToString()
        {
            return Path.GetFileName(FullPath); // в ListBox будет только имя файла.[web:181]
        }
    }

}




