namespace EHR_Monitor_Test
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.ourText = new System.Windows.Forms.RichTextBox();
            this.testNumber = new System.Windows.Forms.Label();
            this.theirText = new System.Windows.Forms.RichTextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.successButton = new System.Windows.Forms.Button();
            this.failedButton = new System.Windows.Forms.Button();
            this.testDescription = new System.Windows.Forms.TextBox();
            this.testCountdown = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ourText
            // 
            this.ourText.Location = new System.Drawing.Point(17, 198);
            this.ourText.Name = "ourText";
            this.ourText.Size = new System.Drawing.Size(486, 500);
            this.ourText.TabIndex = 0;
            this.ourText.Text = "";
            // 
            // testNumber
            // 
            this.testNumber.AutoSize = true;
            this.testNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testNumber.Location = new System.Drawing.Point(12, 9);
            this.testNumber.Name = "testNumber";
            this.testNumber.Size = new System.Drawing.Size(193, 29);
            this.testNumber.TabIndex = 1;
            this.testNumber.Text = "Test Number: 0";
            // 
            // theirText
            // 
            this.theirText.Location = new System.Drawing.Point(510, 198);
            this.theirText.Name = "theirText";
            this.theirText.Size = new System.Drawing.Size(486, 500);
            this.theirText.TabIndex = 3;
            this.theirText.Text = "";
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.Location = new System.Drawing.Point(466, 704);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(84, 30);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run Test";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // successButton
            // 
            this.successButton.Enabled = false;
            this.successButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successButton.Location = new System.Drawing.Point(423, 704);
            this.successButton.Name = "successButton";
            this.successButton.Size = new System.Drawing.Size(80, 30);
            this.successButton.TabIndex = 5;
            this.successButton.Text = "Success";
            this.successButton.UseVisualStyleBackColor = true;
            this.successButton.Visible = false;
            this.successButton.Click += new System.EventHandler(this.successButton_Click);
            // 
            // failedButton
            // 
            this.failedButton.Enabled = false;
            this.failedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedButton.Location = new System.Drawing.Point(510, 704);
            this.failedButton.Name = "failedButton";
            this.failedButton.Size = new System.Drawing.Size(80, 30);
            this.failedButton.TabIndex = 6;
            this.failedButton.Text = "Failed";
            this.failedButton.UseVisualStyleBackColor = true;
            this.failedButton.Visible = false;
            this.failedButton.Click += new System.EventHandler(this.failedButton_Click);
            // 
            // testDescription
            // 
            this.testDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.testDescription.Enabled = false;
            this.testDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testDescription.Location = new System.Drawing.Point(17, 41);
            this.testDescription.Multiline = true;
            this.testDescription.Name = "testDescription";
            this.testDescription.ReadOnly = true;
            this.testDescription.Size = new System.Drawing.Size(979, 128);
            this.testDescription.TabIndex = 7;
            this.testDescription.Text = "Detailed Test description goes here.";
            // 
            // testCountdown
            // 
            this.testCountdown.AutoSize = true;
            this.testCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testCountdown.Location = new System.Drawing.Point(357, 172);
            this.testCountdown.Name = "testCountdown";
            this.testCountdown.Size = new System.Drawing.Size(233, 20);
            this.testCountdown.TabIndex = 8;
            this.testCountdown.Text = "Test will begin in: 5 seconds";
            this.testCountdown.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 740);
            this.Controls.Add(this.testCountdown);
            this.Controls.Add(this.testDescription);
            this.Controls.Add(this.failedButton);
            this.Controls.Add(this.successButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.theirText);
            this.Controls.Add(this.testNumber);
            this.Controls.Add(this.ourText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "EHR Monitor Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ourText;
        private System.Windows.Forms.Label testNumber;
        private System.Windows.Forms.RichTextBox theirText;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button successButton;
        private System.Windows.Forms.Button failedButton;
        private System.Windows.Forms.TextBox testDescription;
        private System.Windows.Forms.Label testCountdown;
        private System.Windows.Forms.Timer timer1;
    }
}

