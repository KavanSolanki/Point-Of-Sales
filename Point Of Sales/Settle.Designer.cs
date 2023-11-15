namespace Point_Of_Sales
{
    partial class Settle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settle));
            this.panel1 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSale = new System.Windows.Forms.TextBox();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNine = new System.Windows.Forms.Button();
            this.btnEight = new System.Windows.Forms.Button();
            this.btnSeven = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnSix = new System.Windows.Forms.Button();
            this.btnFive = new System.Windows.Forms.Button();
            this.btnFour = new System.Windows.Forms.Button();
            this.btnDZero = new System.Windows.Forms.Button();
            this.btnThree = new System.Windows.Forms.Button();
            this.btnTwo = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnOne = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.picClose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 39);
            this.panel1.TabIndex = 10;
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(217, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(30, 32);
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settle Payment";
            // 
            // txtSale
            // 
            this.txtSale.Enabled = false;
            this.txtSale.Location = new System.Drawing.Point(13, 50);
            this.txtSale.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtSale.Multiline = true;
            this.txtSale.Name = "txtSale";
            this.txtSale.Size = new System.Drawing.Size(217, 28);
            this.txtSale.TabIndex = 11;
            // 
            // txtChange
            // 
            this.txtChange.Enabled = false;
            this.txtChange.Location = new System.Drawing.Point(15, 130);
            this.txtChange.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtChange.Multiline = true;
            this.txtChange.Name = "txtChange";
            this.txtChange.Size = new System.Drawing.Size(217, 28);
            this.txtChange.TabIndex = 12;
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(13, 90);
            this.txtCash.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtCash.Multiline = true;
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(217, 28);
            this.txtCash.TabIndex = 13;
            this.txtCash.TextChanged += new System.EventHandler(this.txtCash_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(182, 167);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 50);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNine
            // 
            this.btnNine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnNine.ForeColor = System.Drawing.Color.White;
            this.btnNine.Location = new System.Drawing.Point(126, 167);
            this.btnNine.Name = "btnNine";
            this.btnNine.Size = new System.Drawing.Size(50, 50);
            this.btnNine.TabIndex = 15;
            this.btnNine.Text = "9";
            this.btnNine.UseVisualStyleBackColor = false;
            this.btnNine.Click += new System.EventHandler(this.btnNine_Click);
            // 
            // btnEight
            // 
            this.btnEight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnEight.ForeColor = System.Drawing.Color.White;
            this.btnEight.Location = new System.Drawing.Point(70, 167);
            this.btnEight.Name = "btnEight";
            this.btnEight.Size = new System.Drawing.Size(50, 50);
            this.btnEight.TabIndex = 16;
            this.btnEight.Text = "8";
            this.btnEight.UseVisualStyleBackColor = false;
            this.btnEight.Click += new System.EventHandler(this.btnEight_Click);
            // 
            // btnSeven
            // 
            this.btnSeven.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnSeven.ForeColor = System.Drawing.Color.White;
            this.btnSeven.Location = new System.Drawing.Point(14, 167);
            this.btnSeven.Name = "btnSeven";
            this.btnSeven.Size = new System.Drawing.Size(50, 50);
            this.btnSeven.TabIndex = 17;
            this.btnSeven.Text = "7";
            this.btnSeven.UseVisualStyleBackColor = false;
            this.btnSeven.Click += new System.EventHandler(this.btnSeven_Click);
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnZero.ForeColor = System.Drawing.Color.White;
            this.btnZero.Location = new System.Drawing.Point(182, 223);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(50, 50);
            this.btnZero.TabIndex = 18;
            this.btnZero.Text = "0";
            this.btnZero.UseVisualStyleBackColor = false;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnSix
            // 
            this.btnSix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnSix.ForeColor = System.Drawing.Color.White;
            this.btnSix.Location = new System.Drawing.Point(126, 223);
            this.btnSix.Name = "btnSix";
            this.btnSix.Size = new System.Drawing.Size(50, 50);
            this.btnSix.TabIndex = 19;
            this.btnSix.Text = "6";
            this.btnSix.UseVisualStyleBackColor = false;
            this.btnSix.Click += new System.EventHandler(this.btnSix_Click);
            // 
            // btnFive
            // 
            this.btnFive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnFive.ForeColor = System.Drawing.Color.White;
            this.btnFive.Location = new System.Drawing.Point(70, 223);
            this.btnFive.Name = "btnFive";
            this.btnFive.Size = new System.Drawing.Size(50, 50);
            this.btnFive.TabIndex = 20;
            this.btnFive.Text = "5";
            this.btnFive.UseVisualStyleBackColor = false;
            this.btnFive.Click += new System.EventHandler(this.btnFive_Click);
            // 
            // btnFour
            // 
            this.btnFour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnFour.ForeColor = System.Drawing.Color.White;
            this.btnFour.Location = new System.Drawing.Point(14, 223);
            this.btnFour.Name = "btnFour";
            this.btnFour.Size = new System.Drawing.Size(50, 50);
            this.btnFour.TabIndex = 21;
            this.btnFour.Text = "4";
            this.btnFour.UseVisualStyleBackColor = false;
            this.btnFour.Click += new System.EventHandler(this.btnFour_Click);
            // 
            // btnDZero
            // 
            this.btnDZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnDZero.ForeColor = System.Drawing.Color.White;
            this.btnDZero.Location = new System.Drawing.Point(182, 279);
            this.btnDZero.Name = "btnDZero";
            this.btnDZero.Size = new System.Drawing.Size(50, 50);
            this.btnDZero.TabIndex = 22;
            this.btnDZero.Text = "00";
            this.btnDZero.UseVisualStyleBackColor = false;
            this.btnDZero.Click += new System.EventHandler(this.btnDZero_Click);
            // 
            // btnThree
            // 
            this.btnThree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnThree.ForeColor = System.Drawing.Color.White;
            this.btnThree.Location = new System.Drawing.Point(126, 279);
            this.btnThree.Name = "btnThree";
            this.btnThree.Size = new System.Drawing.Size(50, 50);
            this.btnThree.TabIndex = 23;
            this.btnThree.Text = "3";
            this.btnThree.UseVisualStyleBackColor = false;
            this.btnThree.Click += new System.EventHandler(this.btnThree_Click);
            // 
            // btnTwo
            // 
            this.btnTwo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnTwo.ForeColor = System.Drawing.Color.White;
            this.btnTwo.Location = new System.Drawing.Point(70, 279);
            this.btnTwo.Name = "btnTwo";
            this.btnTwo.Size = new System.Drawing.Size(50, 50);
            this.btnTwo.TabIndex = 24;
            this.btnTwo.Text = "2";
            this.btnTwo.UseVisualStyleBackColor = false;
            this.btnTwo.Click += new System.EventHandler(this.btnTwo_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnEnter.ForeColor = System.Drawing.Color.White;
            this.btnEnter.Location = new System.Drawing.Point(13, 335);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(218, 50);
            this.btnEnter.TabIndex = 25;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnOne
            // 
            this.btnOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnOne.ForeColor = System.Drawing.Color.White;
            this.btnOne.Location = new System.Drawing.Point(14, 279);
            this.btnOne.Name = "btnOne";
            this.btnOne.Size = new System.Drawing.Size(50, 50);
            this.btnOne.TabIndex = 26;
            this.btnOne.Text = "1";
            this.btnOne.UseVisualStyleBackColor = false;
            this.btnOne.Click += new System.EventHandler(this.btnOne_Click);
            // 
            // Settle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(247, 403);
            this.Controls.Add(this.btnDZero);
            this.Controls.Add(this.btnThree);
            this.Controls.Add(this.btnTwo);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnOne);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.btnSix);
            this.Controls.Add(this.btnFive);
            this.Controls.Add(this.btnFour);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnNine);
            this.Controls.Add(this.btnEight);
            this.Controls.Add(this.btnSeven);
            this.Controls.Add(this.txtChange);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.txtSale);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Settle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settle";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settle_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtSale;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnNine;
        private System.Windows.Forms.Button btnEight;
        private System.Windows.Forms.Button btnSeven;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnSix;
        private System.Windows.Forms.Button btnFive;
        private System.Windows.Forms.Button btnFour;
        private System.Windows.Forms.Button btnDZero;
        private System.Windows.Forms.Button btnThree;
        private System.Windows.Forms.Button btnTwo;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnOne;
    }
}