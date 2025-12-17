namespace StudentWorlsAssignment
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button buttonLosdArchive;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelPreview = new Panel();
            dataGridViewStudentvsMark = new DataGridView();
            Student = new DataGridViewTextBoxColumn();
            Mark = new DataGridViewTextBoxColumn();
            textBox_NeuroTask = new TextBox();
            label1 = new Label();
            labelArchivFileName = new Label();
            buttonAiReview = new Button();
            buttonProperties = new Button();
            rbNoHighlight = new RadioButton();
            groupBoxHighLighting = new GroupBox();
            rbPython = new RadioButton();
            rbCSharp = new RadioButton();
            checkedListBoxFiles = new CheckedListBox();
            buttonLosdArchive = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentvsMark).BeginInit();
            groupBoxHighLighting.SuspendLayout();
            SuspendLayout();
            // 
            // buttonLosdArchive
            // 
            buttonLosdArchive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLosdArchive.BackColor = SystemColors.ButtonHighlight;
            buttonLosdArchive.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonLosdArchive.Location = new Point(1306, 250);
            buttonLosdArchive.Margin = new Padding(6);
            buttonLosdArchive.Name = "buttonLosdArchive";
            buttonLosdArchive.RightToLeft = RightToLeft.Yes;
            buttonLosdArchive.Size = new Size(188, 119);
            buttonLosdArchive.TabIndex = 1;
            buttonLosdArchive.Text = "Загрузить архив \r\n    с работами";
            buttonLosdArchive.UseVisualStyleBackColor = false;
            buttonLosdArchive.Click += buttonLoadZip_Click;
            // 
            // panelPreview
            // 
            panelPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPreview.BackColor = SystemColors.ControlLightLight;
            panelPreview.BackgroundImageLayout = ImageLayout.Stretch;
            panelPreview.Location = new Point(22, 26);
            panelPreview.Margin = new Padding(6);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(776, 796);
            panelPreview.TabIndex = 0;
            // 
            // dataGridViewStudentvsMark
            // 
            dataGridViewStudentvsMark.AllowUserToOrderColumns = true;
            dataGridViewStudentvsMark.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridViewStudentvsMark.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewStudentvsMark.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudentvsMark.Columns.AddRange(new DataGridViewColumn[] { Student, Mark });
            dataGridViewStudentvsMark.Location = new Point(810, 429);
            dataGridViewStudentvsMark.Margin = new Padding(6);
            dataGridViewStudentvsMark.MultiSelect = false;
            dataGridViewStudentvsMark.Name = "dataGridViewStudentvsMark";
            dataGridViewStudentvsMark.RowHeadersWidth = 40;
            dataGridViewStudentvsMark.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewStudentvsMark.Size = new Size(683, 606);
            dataGridViewStudentvsMark.TabIndex = 2;
            dataGridViewStudentvsMark.CellClick += DataGridViewStudentvsMark_CellContentClick;
            // 
            // Student
            // 
            Student.Frozen = true;
            Student.HeaderText = "Ученик";
            Student.MinimumWidth = 150;
            Student.Name = "Student";
            Student.Width = 250;
            // 
            // Mark
            // 
            Mark.HeaderText = "Оценка";
            Mark.MinimumWidth = 10;
            Mark.Name = "Mark";
            Mark.Width = 200;
            // 
            // textBox_NeuroTask
            // 
            textBox_NeuroTask.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox_NeuroTask.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_NeuroTask.ForeColor = SystemColors.ActiveCaption;
            textBox_NeuroTask.Location = new Point(810, 26);
            textBox_NeuroTask.Margin = new Padding(6);
            textBox_NeuroTask.Name = "textBox_NeuroTask";
            textBox_NeuroTask.Size = new Size(634, 50);
            textBox_NeuroTask.TabIndex = 4;
            textBox_NeuroTask.Text = "Задание для нейропомощника";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonShadow;
            label1.Location = new Point(1254, 0);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(264, 32);
            label1.TabIndex = 7;
            label1.Text = "(С) Мельник Ю.В. 2025";
            // 
            // labelArchivFileName
            // 
            labelArchivFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelArchivFileName.AutoSize = true;
            labelArchivFileName.ForeColor = SystemColors.ControlDarkDark;
            labelArchivFileName.Location = new Point(810, 250);
            labelArchivFileName.Margin = new Padding(6, 0, 6, 0);
            labelArchivFileName.MaximumSize = new Size(483, 128);
            labelArchivFileName.Name = "labelArchivFileName";
            labelArchivFileName.Size = new Size(476, 128);
            labelArchivFileName.TabIndex = 9;
            labelArchivFileName.Text = "имя архива - загрузите архив с ответами!\r\nВ СДО ИДО на странице задания выберите \"Сохранить все ответы\". Загрузите этот архив .\r\n";
            // 
            // buttonAiReview
            // 
            buttonAiReview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAiReview.BackColor = SystemColors.GradientInactiveCaption;
            buttonAiReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonAiReview.ForeColor = SystemColors.HotTrack;
            buttonAiReview.Location = new Point(1306, 162);
            buttonAiReview.Margin = new Padding(6);
            buttonAiReview.Name = "buttonAiReview";
            buttonAiReview.Size = new Size(184, 75);
            buttonAiReview.TabIndex = 10;
            buttonAiReview.Text = "AI-проверка";
            buttonAiReview.UseVisualStyleBackColor = false;
            buttonAiReview.Click += buttonAiReview_Click;
            // 
            // buttonProperties
            // 
            buttonProperties.BackColor = SystemColors.ButtonFace;
            buttonProperties.BackgroundImage = Properties.Resources.settings100;
            buttonProperties.BackgroundImageLayout = ImageLayout.Stretch;
            buttonProperties.Image = Properties.Resources.settings;
            buttonProperties.Location = new Point(1692, 32);
            buttonProperties.Margin = new Padding(6);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(46, 47);
            buttonProperties.TabIndex = 11;
            buttonProperties.UseVisualStyleBackColor = false;
            // 
            // rbNoHighlight
            // 
            rbNoHighlight.AutoSize = true;
            rbNoHighlight.Location = new Point(273, -4);
            rbNoHighlight.Margin = new Padding(6);
            rbNoHighlight.Name = "rbNoHighlight";
            rbNoHighlight.Size = new Size(215, 36);
            rbNoHighlight.TabIndex = 12;
            rbNoHighlight.Text = "без подсветки";
            rbNoHighlight.UseVisualStyleBackColor = true;
            rbNoHighlight.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // groupBoxHighLighting
            // 
            groupBoxHighLighting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxHighLighting.BackColor = SystemColors.ActiveCaption;
            groupBoxHighLighting.Controls.Add(rbPython);
            groupBoxHighLighting.Controls.Add(rbCSharp);
            groupBoxHighLighting.Controls.Add(rbNoHighlight);
            groupBoxHighLighting.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxHighLighting.ForeColor = SystemColors.ControlLightLight;
            groupBoxHighLighting.Location = new Point(1051, 375);
            groupBoxHighLighting.Margin = new Padding(6);
            groupBoxHighLighting.Name = "groupBoxHighLighting";
            groupBoxHighLighting.Padding = new Padding(6);
            groupBoxHighLighting.Size = new Size(687, 41);
            groupBoxHighLighting.TabIndex = 13;
            groupBoxHighLighting.TabStop = false;
            groupBoxHighLighting.Text = "Подсветка синтаксиса";
            // 
            // rbPython
            // 
            rbPython.AutoSize = true;
            rbPython.Location = new Point(553, -4);
            rbPython.Margin = new Padding(6);
            rbPython.Name = "rbPython";
            rbPython.Size = new Size(126, 36);
            rbPython.TabIndex = 14;
            rbPython.Text = "Python";
            rbPython.UseVisualStyleBackColor = true;
            rbPython.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // rbCSharp
            // 
            rbCSharp.AutoSize = true;
            rbCSharp.Checked = true;
            rbCSharp.Location = new Point(479, -4);
            rbCSharp.Margin = new Padding(6);
            rbCSharp.Name = "rbCSharp";
            rbCSharp.Size = new Size(74, 36);
            rbCSharp.TabIndex = 13;
            rbCSharp.TabStop = true;
            rbCSharp.Text = "C#";
            rbCSharp.UseVisualStyleBackColor = true;
            rbCSharp.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // checkedListBoxFiles
            // 
            checkedListBoxFiles.FormattingEnabled = true;
            checkedListBoxFiles.Location = new Point(22, 834);
            checkedListBoxFiles.Margin = new Padding(6);
            checkedListBoxFiles.Name = "checkedListBoxFiles";
            checkedListBoxFiles.Size = new Size(773, 184);
            checkedListBoxFiles.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1515, 1060);
            Controls.Add(checkedListBoxFiles);
            Controls.Add(groupBoxHighLighting);
            Controls.Add(buttonProperties);
            Controls.Add(buttonAiReview);
            Controls.Add(labelArchivFileName);
            Controls.Add(label1);
            Controls.Add(textBox_NeuroTask);
            Controls.Add(dataGridViewStudentvsMark);
            Controls.Add(buttonLosdArchive);
            Controls.Add(panelPreview);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            Name = "Form1";
            Text = "Помощник  проверки заданий";
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentvsMark).EndInit();
            groupBoxHighLighting.ResumeLayout(false);
            groupBoxHighLighting.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelPreview;
        private Button buttonLosdArchive;
        private DataGridView dataGridViewStudentvsMark;
        private TextBox textBox_NeuroTask;
        private DataGridViewTextBoxColumn Student;
        private DataGridViewTextBoxColumn Mark;
        private Label label1;
        private Label labelArchivFileName;
        private Button buttonAiReview;
        private Button buttonProperties;
        private RadioButton rbNoHighlight;
        private GroupBox groupBoxHighLighting;
        private RadioButton rbPython;
        private RadioButton rbCSharp;
        private CheckedListBox checkedListBoxFiles;
    }
}
