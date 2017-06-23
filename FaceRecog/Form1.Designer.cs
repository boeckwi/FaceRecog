namespace FaceRecog
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CBmin = new System.Windows.Forms.TextBox();
            this.CBmax = new System.Windows.Forms.TextBox();
            this.CRmin = new System.Windows.Forms.TextBox();
            this.CRmax = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // CBmin
            // 
            this.CBmin.Location = new System.Drawing.Point(167, 14);
            this.CBmin.Name = "CBmin";
            this.CBmin.Size = new System.Drawing.Size(48, 20);
            this.CBmin.TabIndex = 2;
            this.CBmin.Text = "103";
            // 
            // CBmax
            // 
            this.CBmax.Location = new System.Drawing.Point(221, 14);
            this.CBmax.Name = "CBmax";
            this.CBmax.Size = new System.Drawing.Size(48, 20);
            this.CBmax.TabIndex = 3;
            this.CBmax.Text = "123";
            // 
            // CRmin
            // 
            this.CRmin.Location = new System.Drawing.Point(49, 15);
            this.CRmin.Name = "CRmin";
            this.CRmin.Size = new System.Drawing.Size(48, 20);
            this.CRmin.TabIndex = 4;
            this.CRmin.Text = "142";
            // 
            // CRmax
            // 
            this.CRmax.Location = new System.Drawing.Point(103, 15);
            this.CRmax.Name = "CRmax";
            this.CRmax.Size = new System.Drawing.Size(48, 20);
            this.CRmax.TabIndex = 5;
            this.CRmax.Text = "170";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 425);
            this.Controls.Add(this.CRmax);
            this.Controls.Add(this.CRmin);
            this.Controls.Add(this.CBmax);
            this.Controls.Add(this.CBmin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox CBmin;
        private System.Windows.Forms.TextBox CBmax;
        private System.Windows.Forms.TextBox CRmin;
        private System.Windows.Forms.TextBox CRmax;
    }
}

