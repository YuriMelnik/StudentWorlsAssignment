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
            textBoxAssignmentDescription = new TextBox();
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
            buttonLosdArchive.Location = new Point(773, 15);
            buttonLosdArchive.Margin = new Padding(2, 1, 2, 1);
            buttonLosdArchive.Name = "buttonLosdArchive";
            buttonLosdArchive.RightToLeft = RightToLeft.Yes;
            buttonLosdArchive.Size = new Size(101, 56);
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
            panelPreview.Location = new Point(7, 15);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(491, 457);
            panelPreview.TabIndex = 0;
            // 
            // dataGridViewStudentvsMark
            // 
            dataGridViewStudentvsMark.AllowUserToOrderColumns = true;
            dataGridViewStudentvsMark.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dataGridViewStudentvsMark.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewStudentvsMark.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudentvsMark.Columns.AddRange(new DataGridViewColumn[] { Student, Mark });
            dataGridViewStudentvsMark.Location = new Point(506, 100);
            dataGridViewStudentvsMark.MultiSelect = false;
            dataGridViewStudentvsMark.Name = "dataGridViewStudentvsMark";
            dataGridViewStudentvsMark.RowHeadersWidth = 40;
            dataGridViewStudentvsMark.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewStudentvsMark.Size = new Size(368, 244);
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
            // textBoxAssignmentDescription
            // 
            textBoxAssignmentDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBoxAssignmentDescription.BackColor = SystemColors.GradientInactiveCaption;
            textBoxAssignmentDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxAssignmentDescription.ForeColor = SystemColors.ActiveCaption;
            textBoxAssignmentDescription.Location = new Point(504, 350);
            textBoxAssignmentDescription.MinimumSize = new Size(0, 80);
            textBoxAssignmentDescription.Multiline = true;
            textBoxAssignmentDescription.Name = "textBoxAssignmentDescription";
            textBoxAssignmentDescription.ScrollBars = ScrollBars.Vertical;
            textBoxAssignmentDescription.Size = new Size(370, 172);
            textBoxAssignmentDescription.TabIndex = 4;
            textBoxAssignmentDescription.Text = "Условие задачи\r\nЗадание для студентов";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonShadow;
            label1.Location = new Point(743, -1);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 7;
            label1.Text = "(С) Мельник Ю.В. 2025";
            // 
            // labelArchivFileName
            // 
            labelArchivFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelArchivFileName.AutoSize = true;
            labelArchivFileName.ForeColor = SystemColors.ControlDarkDark;
            labelArchivFileName.Location = new Point(503, 15);
            labelArchivFileName.MaximumSize = new Size(260, 60);
            labelArchivFileName.Name = "labelArchivFileName";
            labelArchivFileName.Size = new Size(260, 60);
            labelArchivFileName.TabIndex = 9;
            labelArchivFileName.Text = "имя архива - загрузите архив с ответами!\r\nВ СДО ИДО на странице задания выберите \"Сохранить все ответы\". Загрузите этот архив .\r\n";
            // 
            // buttonAiReview
            // 
            buttonAiReview.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAiReview.BackColor = SystemColors.GradientInactiveCaption;
            buttonAiReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonAiReview.ForeColor = SystemColors.HotTrack;
            buttonAiReview.Location = new Point(773, 528);
            buttonAiReview.Name = "buttonAiReview";
            buttonAiReview.Size = new Size(101, 26);
            buttonAiReview.TabIndex = 10;
            buttonAiReview.Text = "AI-проверка";
            buttonAiReview.UseVisualStyleBackColor = false;
            buttonAiReview.Click += buttonAiReview_Click;
            // 
            // buttonProperties
            // 
            buttonProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonProperties.BackColor = SystemColors.ButtonFace;
            buttonProperties.BackgroundImage = Properties.Resources.settings100;
            buttonProperties.BackgroundImageLayout = ImageLayout.Stretch;
            buttonProperties.Image = Properties.Resources.settings;
            buttonProperties.Location = new Point(740, 528);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(27, 26);
            buttonProperties.TabIndex = 11;
            buttonProperties.UseVisualStyleBackColor = false;
            // 
            // rbNoHighlight
            // 
            rbNoHighlight.AutoSize = true;
            rbNoHighlight.Location = new Point(147, -3);
            rbNoHighlight.Name = "rbNoHighlight";
            rbNoHighlight.Size = new Size(109, 19);
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
            groupBoxHighLighting.Location = new Point(504, 75);
            groupBoxHighLighting.Name = "groupBoxHighLighting";
            groupBoxHighLighting.Size = new Size(370, 19);
            groupBoxHighLighting.TabIndex = 13;
            groupBoxHighLighting.TabStop = false;
            groupBoxHighLighting.Text = "Подсветка синтаксиса";
            // 
            // rbPython
            // 
            rbPython.AutoSize = true;
            rbPython.Location = new Point(298, -3);
            rbPython.Name = "rbPython";
            rbPython.Size = new Size(64, 19);
            rbPython.TabIndex = 14;
            rbPython.Text = "Python";
            rbPython.UseVisualStyleBackColor = true;
            rbPython.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // rbCSharp
            // 
            rbCSharp.AutoSize = true;
            rbCSharp.Checked = true;
            rbCSharp.Location = new Point(258, -3);
            rbCSharp.Name = "rbCSharp";
            rbCSharp.Size = new Size(39, 19);
            rbCSharp.TabIndex = 13;
            rbCSharp.TabStop = true;
            rbCSharp.Text = "C#";
            rbCSharp.UseVisualStyleBackColor = true;
            rbCSharp.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // checkedListBoxFiles
            // 
            checkedListBoxFiles.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            checkedListBoxFiles.FormattingEnabled = true;
            checkedListBoxFiles.HorizontalScrollbar = true;
            checkedListBoxFiles.Location = new Point(7, 478);
            checkedListBoxFiles.Name = "checkedListBoxFiles";
            checkedListBoxFiles.Size = new Size(491, 76);
            checkedListBoxFiles.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(checkedListBoxFiles);
            Controls.Add(groupBoxHighLighting);
            Controls.Add(buttonProperties);
            Controls.Add(buttonAiReview);
            Controls.Add(labelArchivFileName);
            Controls.Add(label1);
            Controls.Add(textBoxAssignmentDescription);
            Controls.Add(dataGridViewStudentvsMark);
            Controls.Add(buttonLosdArchive);
            Controls.Add(panelPreview);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(900, 600);
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
        private TextBox textBoxAssignmentDescription;
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
