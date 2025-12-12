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
            Button button1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelPreview = new Panel();
            dataGridViewStudentvsMark = new DataGridView();
            Student = new DataGridViewTextBoxColumn();
            Mark = new DataGridViewTextBoxColumn();
            textBox2 = new TextBox();
            listBoxFiles = new ListBox();
            label1 = new Label();
            label2 = new Label();
            checkBoxSyntax = new CheckBox();
            labelArchivFileName = new Label();
            buttonAiReview = new Button();
            buttonProperties = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentvsMark).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.Location = new Point(835, 139);
            button1.Name = "button1";
            button1.RightToLeft = RightToLeft.Yes;
            button1.Size = new Size(101, 56);
            button1.TabIndex = 1;
            button1.Text = "Загрузить архив \r\n    с работами";
            button1.UseVisualStyleBackColor = false;
            button1.Click += buttonLoadZip_Click;
            // 
            // panelPreview
            // 
            panelPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPreview.BackgroundImageLayout = ImageLayout.Stretch;
            panelPreview.Location = new Point(12, 12);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(529, 447);
            panelPreview.TabIndex = 0;
            // 
            // dataGridViewStudentvsMark
            // 
            dataGridViewStudentvsMark.AllowUserToOrderColumns = true;
            dataGridViewStudentvsMark.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridViewStudentvsMark.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewStudentvsMark.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudentvsMark.Columns.AddRange(new DataGridViewColumn[] { Student, Mark });
            dataGridViewStudentvsMark.Location = new Point(547, 201);
            dataGridViewStudentvsMark.MultiSelect = false;
            dataGridViewStudentvsMark.Name = "dataGridViewStudentvsMark";
            dataGridViewStudentvsMark.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewStudentvsMark.Size = new Size(387, 325);
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
            Mark.Name = "Mark";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox2.ForeColor = SystemColors.WindowFrame;
            textBox2.Location = new Point(547, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(362, 29);
            textBox2.TabIndex = 4;
            textBox2.Text = "Задание для нейропомощника";
            // 
            // listBoxFiles
            // 
            listBoxFiles.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxFiles.FormattingEnabled = true;
            listBoxFiles.ItemHeight = 15;
            listBoxFiles.Location = new Point(12, 465);
            listBoxFiles.Name = "listBoxFiles";
            listBoxFiles.Size = new Size(526, 64);
            listBoxFiles.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonShadow;
            label1.Location = new Point(805, 0);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 7;
            label1.Text = "(С) Мельник Ю.В. 2025";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(544, 139);
            label2.Name = "label2";
            label2.Size = new Size(263, 35);
            label2.TabIndex = 8;
            label2.Text = "В СДО ИДО на странице задания выберите \"Сохранить все ответы\". Загрузите этот архив .";
            // 
            // checkBoxSyntax
            // 
            checkBoxSyntax.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxSyntax.AutoSize = true;
            checkBoxSyntax.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkBoxSyntax.Location = new Point(547, 177);
            checkBoxSyntax.Name = "checkBoxSyntax";
            checkBoxSyntax.Size = new Size(200, 19);
            checkBoxSyntax.TabIndex = 0;
            checkBoxSyntax.Text = "подсветить синтаксис *.cs *.py\r\n";
            checkBoxSyntax.UseVisualStyleBackColor = true;
            // 
            // labelArchivFileName
            // 
            labelArchivFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelArchivFileName.AutoSize = true;
            labelArchivFileName.ForeColor = SystemColors.HotTrack;
            labelArchivFileName.Location = new Point(544, 104);
            labelArchivFileName.MaximumSize = new Size(387, 0);
            labelArchivFileName.Name = "labelArchivFileName";
            labelArchivFileName.Size = new Size(276, 15);
            labelArchivFileName.TabIndex = 9;
            labelArchivFileName.Text = "полное имя архива - загрузите архив с ответами!";
            // 
            // buttonAiReview
            // 
            buttonAiReview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAiReview.BackColor = SystemColors.GradientInactiveCaption;
            buttonAiReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonAiReview.ForeColor = SystemColors.HotTrack;
            buttonAiReview.Location = new Point(835, 98);
            buttonAiReview.Name = "buttonAiReview";
            buttonAiReview.Size = new Size(99, 35);
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
            buttonProperties.Image = Properties.Resources.settings100;
            buttonProperties.Location = new Point(913, 16);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(22, 22);
            buttonProperties.TabIndex = 11;
            buttonProperties.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 538);
            Controls.Add(buttonProperties);
            Controls.Add(buttonAiReview);
            Controls.Add(labelArchivFileName);
            Controls.Add(checkBoxSyntax);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxFiles);
            Controls.Add(textBox2);
            Controls.Add(dataGridViewStudentvsMark);
            Controls.Add(button1);
            Controls.Add(panelPreview);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Помощник  проверки заданий";
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentvsMark).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelPreview;
        private Button button1;
        private DataGridView dataGridViewStudentvsMark;
        private TextBox textBox2;
        private DataGridViewTextBoxColumn Student;
        private DataGridViewTextBoxColumn Mark;
        private ListBox listBoxFiles;
        private Label label1;
        private Label label2;
        private CheckBox checkBoxSyntax;
        private Label labelArchivFileName;
        private Button buttonAiReview;
        private Button buttonProperties;
    }
}
