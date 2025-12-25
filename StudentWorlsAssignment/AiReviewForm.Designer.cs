namespace StudentWorlsAssignment
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
            SuspendLayout();
            // 
            // richTextBoxAiReview
            // 
            richTextBoxAiReview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxAiReview.Location = new Point(3, 98);
            richTextBoxAiReview.MinimumSize = new Size(100, 0);
            richTextBoxAiReview.Name = "richTextBoxAiReview";
            richTextBoxAiReview.ReadOnly = true;
            richTextBoxAiReview.Size = new Size(384, 368);
            richTextBoxAiReview.TabIndex = 0;
            richTextBoxAiReview.Text = "richTextBoxAiReview";
            // 
            // labelStudent
            // 
            labelStudent.AutoSize = true;
            labelStudent.Location = new Point(12, 9);
            labelStudent.Name = "labelStudent";
            labelStudent.Size = new Size(84, 15);
            labelStudent.TabIndex = 1;
            labelStudent.Text = "ФИО студента";
            // 
            // listBoxAiFiles
            // 
            listBoxAiFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxAiFiles.FormattingEnabled = true;
            listBoxAiFiles.HorizontalScrollbar = true;
            listBoxAiFiles.Location = new Point(3, 27);
            listBoxAiFiles.MinimumSize = new Size(100, 0);
            listBoxAiFiles.Name = "listBoxAiFiles";
            listBoxAiFiles.Size = new Size(291, 49);
            listBoxAiFiles.TabIndex = 2;
            // 
            // buttonGenerateReview
            // 
            buttonGenerateReview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonGenerateReview.BackColor = SystemColors.ActiveCaption;
            buttonGenerateReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonGenerateReview.ForeColor = SystemColors.HotTrack;
            buttonGenerateReview.Location = new Point(300, 27);
            buttonGenerateReview.Name = "buttonGenerateReview";
            buttonGenerateReview.Size = new Size(87, 49);
            buttonGenerateReview.TabIndex = 3;
            buttonGenerateReview.Text = "Generate \r\n Review";
            buttonGenerateReview.UseVisualStyleBackColor = false;
            buttonGenerateReview.Click += buttonGenerateReview_Click;
            // 
            // progressBarAi
            // 
            progressBarAi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarAi.Location = new Point(3, 82);
            progressBarAi.Name = "progressBarAi";
            progressBarAi.Size = new Size(384, 10);
            progressBarAi.TabIndex = 4;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(116, 79);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(38, 15);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "label1";
            // 
            // labelStudrentGrade
            // 
            labelStudrentGrade.AutoSize = true;
            labelStudrentGrade.Location = new Point(283, 9);
            labelStudrentGrade.Name = "labelStudrentGrade";
            labelStudrentGrade.Size = new Size(104, 15);
            labelStudrentGrade.TabIndex = 6;
            labelStudrentGrade.Text = "labelStudentGrade";
            labelStudrentGrade.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AiReviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 469);
            Controls.Add(labelStudrentGrade);
            Controls.Add(labelStatus);
            Controls.Add(progressBarAi);
            Controls.Add(buttonGenerateReview);
            Controls.Add(listBoxAiFiles);
            Controls.Add(labelStudent);
            Controls.Add(richTextBoxAiReview);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
    }
}