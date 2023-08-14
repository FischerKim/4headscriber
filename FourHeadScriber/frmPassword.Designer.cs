namespace FourHeadScriber
{
    partial class frmPassword
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
            this.PnlButton = new Jhjo.Component.CPanel();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PnlMiddle = new Jhjo.Component.CPanel();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LblTitlePassword = new Jhjo.Component.CLabel();
            this.PnlButton.SuspendLayout();
            this.PnlMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlButton
            // 
            this.PnlButton.BDrawBorderBottom = false;
            this.PnlButton.BDrawBorderLeft = false;
            this.PnlButton.BDrawBorderRight = false;
            this.PnlButton.BDrawBorderTop = true;
            this.PnlButton.Controls.Add(this.BtnOK);
            this.PnlButton.Controls.Add(this.BtnCancel);
            this.PnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlButton.Location = new System.Drawing.Point(0, 30);
            this.PnlButton.Name = "PnlButton";
            this.PnlButton.Size = new System.Drawing.Size(250, 40);
            this.PnlButton.TabIndex = 10;
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnOK.ForeColor = System.Drawing.Color.White;
            this.BtnOK.Location = new System.Drawing.Point(5, 3);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(120, 35);
            this.BtnOK.TabIndex = 4;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(127, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(120, 35);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PnlMiddle
            // 
            this.PnlMiddle.BDrawBorderBottom = false;
            this.PnlMiddle.BDrawBorderLeft = false;
            this.PnlMiddle.BDrawBorderRight = false;
            this.PnlMiddle.BDrawBorderTop = false;
            this.PnlMiddle.Controls.Add(this.TxtPassword);
            this.PnlMiddle.Controls.Add(this.LblTitlePassword);
            this.PnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMiddle.Location = new System.Drawing.Point(0, 0);
            this.PnlMiddle.Name = "PnlMiddle";
            this.PnlMiddle.Size = new System.Drawing.Size(250, 30);
            this.PnlMiddle.TabIndex = 11;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.TxtPassword.Location = new System.Drawing.Point(104, 3);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(143, 24);
            this.TxtPassword.TabIndex = 12;
            this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblTitlePassword
            // 
            this.LblTitlePassword.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitlePassword.BDrawBorderBottom = false;
            this.LblTitlePassword.BDrawBorderLeft = false;
            this.LblTitlePassword.BDrawBorderRight = true;
            this.LblTitlePassword.BDrawBorderTop = false;
            this.LblTitlePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitlePassword.ForeColor = System.Drawing.Color.White;
            this.LblTitlePassword.Location = new System.Drawing.Point(0, 0);
            this.LblTitlePassword.Name = "LblTitlePassword";
            this.LblTitlePassword.OColor = System.Drawing.Color.Black;
            this.LblTitlePassword.Size = new System.Drawing.Size(100, 30);
            this.LblTitlePassword.TabIndex = 6;
            this.LblTitlePassword.Text = "Password";
            this.LblTitlePassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 70);
            this.Controls.Add(this.PnlMiddle);
            this.Controls.Add(this.PnlButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passwords";
            this.PnlButton.ResumeLayout(false);
            this.PnlMiddle.ResumeLayout(false);
            this.PnlMiddle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Jhjo.Component.CPanel PnlButton;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private Jhjo.Component.CPanel PnlMiddle;
        private Jhjo.Component.CLabel LblTitlePassword;
        private System.Windows.Forms.TextBox TxtPassword;
    }
}