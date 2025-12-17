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
            rtbAiReview = new RichTextBox();
            labelStudent = new Label();
            listBoxFiles = new ListBox();
            SuspendLayout();
            // 
            // rtbAiReview
            // 
            rtbAiReview.Location = new Point(0, 129);
            rtbAiReview.Name = "rtbAiReview";
            rtbAiReview.Size = new Size(279, 298);
            rtbAiReview.TabIndex = 0;
            rtbAiReview.Text = "";
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
            // listBoxFiles
            // 
            listBoxFiles.FormattingEnabled = true;
            listBoxFiles.Location = new Point(0, 29);
            listBoxFiles.Name = "listBoxFiles";
            listBoxFiles.Size = new Size(279, 49);
            listBoxFiles.TabIndex = 2;
            // 
            // AiReviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 450);
            Controls.Add(listBoxFiles);
            Controls.Add(labelStudent);
            Controls.Add(rtbAiReview);
            Name = "AiReviewForm";
            Text = "AiReviewForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbAiReview;
        private Label labelStudent;
        private ListBox listBoxFiles;
    }
}