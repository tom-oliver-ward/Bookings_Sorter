namespace BookingsSorter
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
            this.SpreadSheets2Sort = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Period = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOutputLoc = new System.Windows.Forms.Button();
            this.textBoxOutputLoc = new System.Windows.Forms.TextBox();
            this.buttonSort = new System.Windows.Forms.Button();
            this.textBoxExcelLine = new System.Windows.Forms.TextBox();
            this.FilenumTB = new System.Windows.Forms.TextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SpreadSheets2Sort
            // 
            this.SpreadSheets2Sort.AllowDrop = true;
            this.SpreadSheets2Sort.FormattingEnabled = true;
            this.SpreadSheets2Sort.Location = new System.Drawing.Point(12, 38);
            this.SpreadSheets2Sort.Name = "SpreadSheets2Sort";
            this.SpreadSheets2Sort.Size = new System.Drawing.Size(700, 95);
            this.SpreadSheets2Sort.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Drag spreadsheets to sort here";
            // 
            // textBox_Period
            // 
            this.textBox_Period.Location = new System.Drawing.Point(280, 137);
            this.textBox_Period.Name = "textBox_Period";
            this.textBox_Period.Size = new System.Drawing.Size(100, 20);
            this.textBox_Period.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Enter period to right, e.g. 11/2017 or Q1 2017 or 2017";
            // 
            // buttonOutputLoc
            // 
            this.buttonOutputLoc.Location = new System.Drawing.Point(15, 165);
            this.buttonOutputLoc.Name = "buttonOutputLoc";
            this.buttonOutputLoc.Size = new System.Drawing.Size(140, 23);
            this.buttonOutputLoc.TabIndex = 14;
            this.buttonOutputLoc.Text = "Select Location of Output";
            this.buttonOutputLoc.UseVisualStyleBackColor = true;
            this.buttonOutputLoc.Click += new System.EventHandler(this.buttonOutputLoc_Click);
            // 
            // textBoxOutputLoc
            // 
            this.textBoxOutputLoc.Location = new System.Drawing.Point(165, 168);
            this.textBoxOutputLoc.Name = "textBoxOutputLoc";
            this.textBoxOutputLoc.Size = new System.Drawing.Size(547, 20);
            this.textBoxOutputLoc.TabIndex = 15;
            this.textBoxOutputLoc.TextChanged += new System.EventHandler(this.textBoxOutputLoc_TextChanged);
            // 
            // buttonSort
            // 
            this.buttonSort.Location = new System.Drawing.Point(280, 194);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(140, 23);
            this.buttonSort.TabIndex = 16;
            this.buttonSort.Text = "Sort";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // textBoxExcelLine
            // 
            this.textBoxExcelLine.Location = new System.Drawing.Point(280, 223);
            this.textBoxExcelLine.Name = "textBoxExcelLine";
            this.textBoxExcelLine.Size = new System.Drawing.Size(140, 20);
            this.textBoxExcelLine.TabIndex = 18;
            // 
            // FilenumTB
            // 
            this.FilenumTB.Location = new System.Drawing.Point(280, 251);
            this.FilenumTB.Name = "FilenumTB";
            this.FilenumTB.Size = new System.Drawing.Size(140, 20);
            this.FilenumTB.TabIndex = 17;
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(280, 277);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(140, 23);
            this.button_Clear.TabIndex = 19;
            this.button_Clear.Text = "Clear All";
            this.button_Clear.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 314);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBoxExcelLine);
            this.Controls.Add(this.FilenumTB);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.textBoxOutputLoc);
            this.Controls.Add(this.buttonOutputLoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Period);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpreadSheets2Sort);
            this.Name = "Form1";
            this.Text = "Bookings Sorter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox SpreadSheets2Sort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Period;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOutputLoc;
        public System.Windows.Forms.TextBox textBoxOutputLoc;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.TextBox textBoxExcelLine;
        public System.Windows.Forms.TextBox FilenumTB;
        private System.Windows.Forms.Button button_Clear;
    }
}

