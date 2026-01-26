namespace StudentWorksAssignment
{
    partial class Form2
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
            panelRight = new Panel();
            panelLeft = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panelRight.SuspendLayout();
            panelLeft.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panelRight
            // 
            panelRight.BackColor = SystemColors.ControlLight;
            panelRight.Controls.Add(panel1);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(0, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(607, 415);
            panelRight.TabIndex = 0;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = SystemColors.ButtonShadow;
            panelLeft.Controls.Add(panel2);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(299, 415);
            panelLeft.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Location = new Point(56, 33);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 100);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(10, 16);
            panel2.Name = "panel2";
            panel2.Size = new Size(266, 323);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Location = new Point(14, 18);
            panel3.Name = "panel3";
            panel3.Size = new Size(183, 76);
            panel3.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(24, 121);
            panel4.Name = "panel4";
            panel4.Size = new Size(211, 185);
            panel4.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Location = new Point(84, 17);
            panel5.Name = "panel5";
            panel5.Size = new Size(95, 144);
            panel5.TabIndex = 0;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 415);
            Controls.Add(panelLeft);
            Controls.Add(panelRight);
            Name = "Form2";
            Text = "Form2";
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRight;
        private Panel panel1;
        private Panel panelLeft;
        private Panel panel2;
        private Panel panel4;
        private Panel panel5;
        private Panel panel3;
    }
}