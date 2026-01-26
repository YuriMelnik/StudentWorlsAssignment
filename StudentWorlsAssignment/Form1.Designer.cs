namespace StudentWorksAssignment
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelLeft = new Panel();
            panelPreview = new Panel();
            checkedListBoxFiles = new CheckedListBox();
            panelRight = new Panel();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelTopRight = new Panel();
            labelCopyRight = new Label();
            buttonLoadArchive = new Button();
            labelArchiveFileName = new Label();
            tableLayoutPanelDescription = new TableLayoutPanel();
            textBoxAssignmentDescription = new TextBox();
            panelControls = new Panel();
            panelHighLightingSyntax = new Panel();
            labelHighLightText = new Label();
            rbPython = new RadioButton();
            rbCSharp = new RadioButton();
            rbNoHighlight = new RadioButton();
            buttonProperties = new Button();
            buttonAiReview = new Button();
            dataGridViewStudentVsMark = new DataGridView();
            Student = new DataGridViewTextBoxColumn();
            Mark = new DataGridViewTextBoxColumn();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelTopRight.SuspendLayout();
            tableLayoutPanelDescription.SuspendLayout();
            panelControls.SuspendLayout();
            panelHighLightingSyntax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentVsMark).BeginInit();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(panelPreview);
            panelLeft.Controls.Add(checkedListBoxFiles);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(5);
            panelLeft.Size = new Size(684, 361);
            panelLeft.TabIndex = 0;
            // 
            // panelPreview
            // 
            panelPreview.BackColor = SystemColors.ControlLightLight;
            panelPreview.BackgroundImageLayout = ImageLayout.Stretch;
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(5, 5);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(674, 275);
            panelPreview.TabIndex = 0;
            // 
            // checkedListBoxFiles
            // 
            checkedListBoxFiles.Dock = DockStyle.Bottom;
            checkedListBoxFiles.FormattingEnabled = true;
            checkedListBoxFiles.HorizontalScrollbar = true;
            checkedListBoxFiles.Location = new Point(5, 280);
            checkedListBoxFiles.Name = "checkedListBoxFiles";
            checkedListBoxFiles.Size = new Size(674, 76);
            checkedListBoxFiles.TabIndex = 1;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(tableLayoutPanelMain);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(276, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(5);
            panelRight.Size = new Size(408, 361);
            panelRight.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(panelTopRight, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelDescription, 0, 1);
            tableLayoutPanelMain.Controls.Add(dataGridViewStudentVsMark, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(5, 5);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(398, 351);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelTopRight
            // 
            panelTopRight.BackColor = SystemColors.Control;
            panelTopRight.Controls.Add(labelCopyRight);
            panelTopRight.Controls.Add(buttonLoadArchive);
            panelTopRight.Controls.Add(labelArchiveFileName);
            panelTopRight.Dock = DockStyle.Fill;
            panelTopRight.Location = new Point(3, 3);
            panelTopRight.Name = "panelTopRight";
            panelTopRight.Size = new Size(392, 65);
            panelTopRight.TabIndex = 0;
            // 
            // labelCopyRight
            // 
            labelCopyRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelCopyRight.AutoSize = true;
            labelCopyRight.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCopyRight.ForeColor = SystemColors.ButtonShadow;
            labelCopyRight.Location = new Point(267, -3);
            labelCopyRight.Name = "labelCopyRight";
            labelCopyRight.Size = new Size(125, 13);
            labelCopyRight.TabIndex = 2;
            labelCopyRight.Text = "(С) Мельник Ю.В. 2026";
            // 
            // buttonLoadArchive
            // 
            buttonLoadArchive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadArchive.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonLoadArchive.Location = new Point(283, 10);
            buttonLoadArchive.Name = "buttonLoadArchive";
            buttonLoadArchive.Size = new Size(106, 55);
            buttonLoadArchive.TabIndex = 1;
            buttonLoadArchive.Text = "Загрузить архив \r\n    с работами";
            buttonLoadArchive.UseVisualStyleBackColor = true;
            buttonLoadArchive.Click += buttonLoadZip_Click;
            // 
            // labelArchiveFileName
            // 
            labelArchiveFileName.Dock = DockStyle.Fill;
            labelArchiveFileName.ForeColor = SystemColors.ControlDarkDark;
            labelArchiveFileName.Location = new Point(0, 0);
            labelArchiveFileName.Name = "labelArchiveFileName";
            labelArchiveFileName.Padding = new Padding(5, 10, 120, 0);
            labelArchiveFileName.Size = new Size(392, 65);
            labelArchiveFileName.TabIndex = 0;
            labelArchiveFileName.Text = "имя архива - загрузите архив с ответами!\r\nВ СДО ИДО на странице задания выберите \"Сохранить все ответы\". Загрузите этот архив .";
            // 
            // tableLayoutPanelDescription
            // 
            tableLayoutPanelDescription.ColumnCount = 2;
            tableLayoutPanelDescription.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelDescription.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            tableLayoutPanelDescription.Controls.Add(textBoxAssignmentDescription, 0, 0);
            tableLayoutPanelDescription.Controls.Add(panelControls, 1, 0);
            tableLayoutPanelDescription.Dock = DockStyle.Fill;
            tableLayoutPanelDescription.Location = new Point(3, 74);
            tableLayoutPanelDescription.Name = "tableLayoutPanelDescription";
            tableLayoutPanelDescription.RowCount = 1;
            tableLayoutPanelDescription.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelDescription.Size = new Size(392, 174);
            tableLayoutPanelDescription.TabIndex = 1;
            // 
            // textBoxAssignmentDescription
            // 
            textBoxAssignmentDescription.Dock = DockStyle.Fill;
            textBoxAssignmentDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxAssignmentDescription.ForeColor = SystemColors.GrayText;
            textBoxAssignmentDescription.Location = new Point(3, 3);
            textBoxAssignmentDescription.Multiline = true;
            textBoxAssignmentDescription.Name = "textBoxAssignmentDescription";
            textBoxAssignmentDescription.ScrollBars = ScrollBars.Vertical;
            textBoxAssignmentDescription.Size = new Size(274, 168);
            textBoxAssignmentDescription.TabIndex = 4;
            textBoxAssignmentDescription.Text = "Условие задачи\r\nЗадание для студентов";
            // 
            // panelControls
            // 
            panelControls.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelControls.Controls.Add(panelHighLightingSyntax);
            panelControls.Controls.Add(buttonProperties);
            panelControls.Controls.Add(buttonAiReview);
            panelControls.Enabled = false;
            panelControls.Location = new Point(283, 3);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(106, 168);
            panelControls.TabIndex = 0;
            // 
            // panelHighLightingSyntax
            // 
            panelHighLightingSyntax.Controls.Add(labelHighLightText);
            panelHighLightingSyntax.Controls.Add(rbPython);
            panelHighLightingSyntax.Controls.Add(rbCSharp);
            panelHighLightingSyntax.Controls.Add(rbNoHighlight);
            panelHighLightingSyntax.Dock = DockStyle.Top;
            panelHighLightingSyntax.Location = new Point(0, 0);
            panelHighLightingSyntax.Name = "panelHighLightingSyntax";
            panelHighLightingSyntax.Size = new Size(106, 103);
            panelHighLightingSyntax.TabIndex = 15;
            // 
            // labelHighLightText
            // 
            labelHighLightText.AutoSize = true;
            labelHighLightText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelHighLightText.Location = new Point(2, 5);
            labelHighLightText.Name = "labelHighLightText";
            labelHighLightText.Size = new Size(72, 30);
            labelHighLightText.TabIndex = 15;
            labelHighLightText.Text = "Подсветка \r\nсинтаксиса";
            // 
            // rbPython
            // 
            rbPython.AutoSize = true;
            rbPython.Location = new Point(5, 80);
            rbPython.Name = "rbPython";
            rbPython.Size = new Size(63, 19);
            rbPython.TabIndex = 14;
            rbPython.Text = "Python";
            rbPython.UseVisualStyleBackColor = true;
            rbPython.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // rbCSharp
            // 
            rbCSharp.AutoSize = true;
            rbCSharp.Checked = true;
            rbCSharp.Location = new Point(5, 59);
            rbCSharp.Name = "rbCSharp";
            rbCSharp.Size = new Size(40, 19);
            rbCSharp.TabIndex = 13;
            rbCSharp.TabStop = true;
            rbCSharp.Text = "C#";
            rbCSharp.UseVisualStyleBackColor = true;
            rbCSharp.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // rbNoHighlight
            // 
            rbNoHighlight.AutoSize = true;
            rbNoHighlight.Location = new Point(5, 38);
            rbNoHighlight.Name = "rbNoHighlight";
            rbNoHighlight.Size = new Size(102, 19);
            rbNoHighlight.TabIndex = 12;
            rbNoHighlight.Text = "без подсветки";
            rbNoHighlight.UseVisualStyleBackColor = true;
            rbNoHighlight.CheckedChanged += rbHighlightMode_CheckedChanged;
            // 
            // buttonProperties
            // 
            buttonProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonProperties.BackgroundImage = Properties.Resources.settings100;
            buttonProperties.BackgroundImageLayout = ImageLayout.Stretch;
            buttonProperties.ImageAlign = ContentAlignment.TopLeft;
            buttonProperties.Location = new Point(81, 105);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(24, 24);
            buttonProperties.TabIndex = 11;
            buttonProperties.UseVisualStyleBackColor = true;
            // 
            // buttonAiReview
            // 
            buttonAiReview.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonAiReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonAiReview.ForeColor = Color.DarkBlue;
            buttonAiReview.Location = new Point(0, 129);
            buttonAiReview.Name = "buttonAiReview";
            buttonAiReview.Size = new Size(106, 36);
            buttonAiReview.TabIndex = 10;
            buttonAiReview.Text = "AI-проверка";
            buttonAiReview.UseVisualStyleBackColor = true;
            buttonAiReview.Click += buttonAiReview_Click;
            // 
            // dataGridViewStudentVsMark
            // 
            dataGridViewStudentVsMark.AllowUserToOrderColumns = true;
            dataGridViewStudentVsMark.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewStudentVsMark.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudentVsMark.Columns.AddRange(new DataGridViewColumn[] { Student, Mark });
            dataGridViewStudentVsMark.Dock = DockStyle.Fill;
            dataGridViewStudentVsMark.Location = new Point(3, 254);
            dataGridViewStudentVsMark.MultiSelect = false;
            dataGridViewStudentVsMark.Name = "dataGridViewStudentVsMark";
            dataGridViewStudentVsMark.RowHeadersWidth = 40;
            dataGridViewStudentVsMark.Size = new Size(392, 94);
            dataGridViewStudentVsMark.TabIndex = 2;
            dataGridViewStudentVsMark.CellClick += DataGridViewStudentvsMark_CellContentClick;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 361);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(700, 400);
            Name = "Form1";
            Text = "Помощник проверки заданий";
            panelLeft.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            panelTopRight.ResumeLayout(false);
            panelTopRight.PerformLayout();
            tableLayoutPanelDescription.ResumeLayout(false);
            tableLayoutPanelDescription.PerformLayout();
            panelControls.ResumeLayout(false);
            panelHighLightingSyntax.ResumeLayout(false);
            panelHighLightingSyntax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudentVsMark).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Button buttonLoadArchive;
        private System.Windows.Forms.DataGridView dataGridViewStudentVsMark;
        private System.Windows.Forms.TextBox textBoxAssignmentDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Student;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mark;
        private System.Windows.Forms.Label labelCopyRight;
        private System.Windows.Forms.Label labelArchiveFileName;
        private System.Windows.Forms.Button buttonAiReview;
        private System.Windows.Forms.Button buttonProperties;
        private System.Windows.Forms.RadioButton rbNoHighlight;
        private System.Windows.Forms.Panel panelHighLightingSyntax;
        private System.Windows.Forms.RadioButton rbPython;
        private System.Windows.Forms.RadioButton rbCSharp;
        private System.Windows.Forms.CheckedListBox checkedListBoxFiles;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label labelHighLightText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDescription;
        private System.Windows.Forms.Panel panelControls;
    }
}