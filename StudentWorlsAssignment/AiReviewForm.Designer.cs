namespace StudentWorksAssignment
{
    partial class AiReviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiReviewForm));
            richTextBoxAiReview = new RichTextBox();
            labelStudent = new Label();
            listBoxAiFiles = new ListBox();
            buttonGenerateReview = new Button();
            progressBarAi = new ProgressBar();
            labelStatus = new Label();
            labelStudrentGrade = new Label();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            comboBox1 = new ComboBox();
            buttonProperties = new Button();
            buttonCopyPrompt = new Button();
            buttonCopyText = new Button();
            buttonCopyAiReviewText = new Button();
            SuspendLayout();
            // 
            // richTextBoxAiReview
            // 
            richTextBoxAiReview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxAiReview.Location = new Point(388, 155);
            richTextBoxAiReview.MinimumSize = new Size(100, 0);
            richTextBoxAiReview.Name = "richTextBoxAiReview";
            richTextBoxAiReview.ReadOnly = true;
            richTextBoxAiReview.Size = new Size(388, 403);
            richTextBoxAiReview.TabIndex = 0;
            richTextBoxAiReview.Text = "richTextBoxAiReview";
            // 
            // labelStudent
            // 
            labelStudent.AutoSize = true;
            labelStudent.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelStudent.Location = new Point(3, 10);
            labelStudent.Name = "labelStudent";
            labelStudent.Size = new Size(66, 45);
            labelStudent.TabIndex = 1;
            labelStudent.Text = "Ф\r\nИ\r\nО студента";
            // 
            // listBoxAiFiles
            // 
            listBoxAiFiles.FormattingEnabled = true;
            listBoxAiFiles.HorizontalScrollbar = true;
            listBoxAiFiles.Location = new Point(6, 80);
            listBoxAiFiles.MinimumSize = new Size(100, 0);
            listBoxAiFiles.Name = "listBoxAiFiles";
            listBoxAiFiles.Size = new Size(210, 49);
            listBoxAiFiles.TabIndex = 2;
            // 
            // buttonGenerateReview
            // 
            buttonGenerateReview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonGenerateReview.BackColor = SystemColors.GradientInactiveCaption;
            buttonGenerateReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonGenerateReview.ForeColor = SystemColors.HotTrack;
            buttonGenerateReview.Location = new Point(692, 77);
            buttonGenerateReview.Name = "buttonGenerateReview";
            buttonGenerateReview.Size = new Size(87, 52);
            buttonGenerateReview.TabIndex = 3;
            buttonGenerateReview.Text = "Generate \r\n Review";
            buttonGenerateReview.UseVisualStyleBackColor = false;
            buttonGenerateReview.Click += buttonGenerateReview_Click;
            // 
            // progressBarAi
            // 
            progressBarAi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarAi.Location = new Point(3, 139);
            progressBarAi.Name = "progressBarAi";
            progressBarAi.Size = new Size(777, 10);
            progressBarAi.TabIndex = 4;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.BackColor = Color.Transparent;
            labelStatus.Location = new Point(116, 136);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(38, 15);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "label1";
            // 
            // labelStudrentGrade
            // 
            labelStudrentGrade.AutoSize = true;
            labelStudrentGrade.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelStudrentGrade.Location = new Point(3, 57);
            labelStudrentGrade.Name = "labelStudrentGrade";
            labelStudrentGrade.Size = new Size(133, 20);
            labelStudrentGrade.TabIndex = 6;
            labelStudrentGrade.Text = "labelStudentGrade";
            labelStudrentGrade.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.Window;
            textBox1.Location = new Point(222, 10);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(468, 119);
            textBox1.TabIndex = 7;
            textBox1.Text = "prompt";
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBox1.Location = new Point(3, 155);
            richTextBox1.MinimumSize = new Size(100, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(383, 403);
            richTextBox1.TabIndex = 8;
            richTextBox1.Text = "richTextBoxFilePerview";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(692, 10);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(87, 23);
            comboBox1.TabIndex = 17;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // buttonProperties
            // 
            buttonProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonProperties.BackColor = SystemColors.ButtonFace;
            buttonProperties.BackgroundImage = Properties.Resources.settings100;
            buttonProperties.BackgroundImageLayout = ImageLayout.Stretch;
            buttonProperties.Image = Properties.Resources.settings;
            buttonProperties.Location = new Point(752, 39);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(27, 26);
            buttonProperties.TabIndex = 18;
            buttonProperties.UseVisualStyleBackColor = false;
            // 
            // buttonCopyPrompt
            // 
            buttonCopyPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCopyPrompt.AutoEllipsis = true;
            buttonCopyPrompt.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonCopyPrompt.BackColor = Color.Transparent;
            buttonCopyPrompt.BackgroundImage = Properties.Resources.copy;
            buttonCopyPrompt.BackgroundImageLayout = ImageLayout.Stretch;
            buttonCopyPrompt.ForeColor = SystemColors.ActiveCaption;
            buttonCopyPrompt.ImageKey = "(нет)";
            buttonCopyPrompt.Location = new Point(664, 10);
            buttonCopyPrompt.Name = "buttonCopyPrompt";
            buttonCopyPrompt.Size = new Size(22, 23);
            buttonCopyPrompt.TabIndex = 19;
            buttonCopyPrompt.UseVisualStyleBackColor = false;
            // 
            // buttonCopyText
            // 
            buttonCopyText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCopyText.AutoEllipsis = true;
            buttonCopyText.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonCopyText.BackColor = Color.Transparent;
            buttonCopyText.BackgroundImage = Properties.Resources.copy;
            buttonCopyText.BackgroundImageLayout = ImageLayout.Stretch;
            buttonCopyText.ForeColor = SystemColors.ActiveCaption;
            buttonCopyText.ImageKey = "(нет)";
            buttonCopyText.Location = new Point(361, 157);
            buttonCopyText.Name = "buttonCopyText";
            buttonCopyText.Size = new Size(22, 23);
            buttonCopyText.TabIndex = 20;
            buttonCopyText.UseVisualStyleBackColor = false;
            // 
            // buttonCopyAiReviewText
            // 
            buttonCopyAiReviewText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCopyAiReviewText.AutoEllipsis = true;
            buttonCopyAiReviewText.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonCopyAiReviewText.BackColor = Color.Transparent;
            buttonCopyAiReviewText.BackgroundImage = Properties.Resources.copy;
            buttonCopyAiReviewText.BackgroundImageLayout = ImageLayout.Stretch;
            buttonCopyAiReviewText.ForeColor = SystemColors.ActiveCaption;
            buttonCopyAiReviewText.ImageKey = "(нет)";
            buttonCopyAiReviewText.Location = new Point(753, 157);
            buttonCopyAiReviewText.Name = "buttonCopyAiReviewText";
            buttonCopyAiReviewText.Size = new Size(22, 23);
            buttonCopyAiReviewText.TabIndex = 21;
            buttonCopyAiReviewText.UseVisualStyleBackColor = false;
            // 
            // AiReviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(784, 561);
            Controls.Add(buttonCopyAiReviewText);
            Controls.Add(buttonCopyText);
            Controls.Add(buttonCopyPrompt);
            Controls.Add(buttonProperties);
            Controls.Add(comboBox1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(labelStudrentGrade);
            Controls.Add(labelStatus);
            Controls.Add(progressBarAi);
            Controls.Add(buttonGenerateReview);
            Controls.Add(listBoxAiFiles);
            Controls.Add(labelStudent);
            Controls.Add(richTextBoxAiReview);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 600);
            Name = "AiReviewForm";
            Text = "AiReviewForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxAiReview;
        private Label labelStudent;
        private ListBox listBoxAiFiles;
        private Button buttonGenerateReview;
        private ProgressBar progressBarAi;
        private Label labelStatus;
        private Label labelStudrentGrade;
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private ComboBox comboBox1;
        private Button buttonProperties;
        private Button buttonCopyPrompt;
        private Button buttonCopyText;
        private Button buttonCopyAiReviewText;
    }
}