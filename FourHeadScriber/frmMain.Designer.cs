namespace FourHeadScriber
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PnlHeader = new Jhjo.Component.CPanel();
            this.PnlHeader2 = new Jhjo.Component.CPanel();
            this.BtnMinimize2 = new System.Windows.Forms.Button();
            this.LblTitle2 = new Jhjo.Component.CLabel();
            this.LblTime2 = new Jhjo.Component.CLabel();
            this.LblRecipe2 = new Jhjo.Component.CLabel();
            this.PnlHeader1 = new Jhjo.Component.CPanel();
            this.BtnMinimize1 = new System.Windows.Forms.Button();
            this.LblTime1 = new Jhjo.Component.CLabel();
            this.LblRecipe1 = new Jhjo.Component.CLabel();
            this.LblTitle1 = new Jhjo.Component.CLabel();
            this.PnlFooter = new Jhjo.Component.CPanel();
            this.PnlFooter2 = new Jhjo.Component.CPanel();
            this.BtnExit2 = new System.Windows.Forms.Button();
            this.BtnRecipe2 = new System.Windows.Forms.Button();
            this.BtnHome2 = new System.Windows.Forms.Button();
            this.BtnSetup2 = new System.Windows.Forms.Button();
            this.BtnReport2 = new System.Windows.Forms.Button();
            this.PnlFooter1 = new Jhjo.Component.CPanel();
            this.BtnExit1 = new System.Windows.Forms.Button();
            this.BtnHome1 = new System.Windows.Forms.Button();
            this.BtnReport1 = new System.Windows.Forms.Button();
            this.BtnSetup1 = new System.Windows.Forms.Button();
            this.BtnRecipe1 = new System.Windows.Forms.Button();
            this.PnlBody = new Jhjo.Component.CPanel();
            this.Timer1000 = new System.Windows.Forms.Timer(this.components);
            this.PnlHeader.SuspendLayout();
            this.PnlHeader2.SuspendLayout();
            this.PnlHeader1.SuspendLayout();
            this.PnlFooter.SuspendLayout();
            this.PnlFooter2.SuspendLayout();
            this.PnlFooter1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlHeader
            // 
            this.PnlHeader.BDrawBorderBottom = false;
            this.PnlHeader.BDrawBorderLeft = false;
            this.PnlHeader.BDrawBorderRight = false;
            this.PnlHeader.BDrawBorderTop = false;
            this.PnlHeader.Controls.Add(this.PnlHeader2);
            this.PnlHeader.Controls.Add(this.PnlHeader1);
            this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(1932, 36);
            this.PnlHeader.TabIndex = 0;
            // 
            // PnlHeader2
            // 
            this.PnlHeader2.BDrawBorderBottom = false;
            this.PnlHeader2.BDrawBorderLeft = false;
            this.PnlHeader2.BDrawBorderRight = false;
            this.PnlHeader2.BDrawBorderTop = false;
            this.PnlHeader2.Controls.Add(this.BtnMinimize2);
            this.PnlHeader2.Controls.Add(this.LblTitle2);
            this.PnlHeader2.Controls.Add(this.LblTime2);
            this.PnlHeader2.Controls.Add(this.LblRecipe2);
            this.PnlHeader2.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlHeader2.Location = new System.Drawing.Point(1280, 0);
            this.PnlHeader2.Name = "PnlHeader2";
            this.PnlHeader2.Size = new System.Drawing.Size(1280, 36);
            this.PnlHeader2.TabIndex = 10;
            // 
            // BtnMinimize2
            // 
            this.BtnMinimize2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimize2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnMinimize2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.BtnMinimize2.ForeColor = System.Drawing.Color.White;
            this.BtnMinimize2.Location = new System.Drawing.Point(1238, 2);
            this.BtnMinimize2.Name = "BtnMinimize2";
            this.BtnMinimize2.Size = new System.Drawing.Size(39, 33);
            this.BtnMinimize2.TabIndex = 8;
            this.BtnMinimize2.Text = "_";
            this.BtnMinimize2.UseVisualStyleBackColor = false;
            this.BtnMinimize2.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // LblTitle2
            // 
            this.LblTitle2.BackColor = System.Drawing.Color.RoyalBlue;
            this.LblTitle2.BDrawBorderBottom = false;
            this.LblTitle2.BDrawBorderLeft = true;
            this.LblTitle2.BDrawBorderRight = true;
            this.LblTitle2.BDrawBorderTop = false;
            this.LblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblTitle2.Location = new System.Drawing.Point(0, 0);
            this.LblTitle2.Name = "LblTitle2";
            this.LblTitle2.OColor = System.Drawing.Color.Black;
            this.LblTitle2.Size = new System.Drawing.Size(200, 36);
            this.LblTitle2.TabIndex = 4;
            this.LblTitle2.Text = "4Head Scriber";
            this.LblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTime2
            // 
            this.LblTime2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTime2.BackColor = System.Drawing.Color.Coral;
            this.LblTime2.BDrawBorderBottom = false;
            this.LblTime2.BDrawBorderLeft = false;
            this.LblTime2.BDrawBorderRight = true;
            this.LblTime2.BDrawBorderTop = false;
            this.LblTime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblTime2.ForeColor = System.Drawing.Color.White;
            this.LblTime2.Location = new System.Drawing.Point(1035, 0);
            this.LblTime2.Name = "LblTime2";
            this.LblTime2.OColor = System.Drawing.Color.Black;
            this.LblTime2.Size = new System.Drawing.Size(200, 36);
            this.LblTime2.TabIndex = 7;
            this.LblTime2.Text = "2012-03-02 44:33:22";
            this.LblTime2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRecipe2
            // 
            this.LblRecipe2.BackColor = System.Drawing.Color.DimGray;
            this.LblRecipe2.BDrawBorderBottom = false;
            this.LblRecipe2.BDrawBorderLeft = false;
            this.LblRecipe2.BDrawBorderRight = true;
            this.LblRecipe2.BDrawBorderTop = false;
            this.LblRecipe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblRecipe2.ForeColor = System.Drawing.Color.White;
            this.LblRecipe2.Location = new System.Drawing.Point(200, 0);
            this.LblRecipe2.Name = "LblRecipe2";
            this.LblRecipe2.OColor = System.Drawing.Color.Black;
            this.LblRecipe2.Size = new System.Drawing.Size(835, 36);
            this.LblRecipe2.TabIndex = 5;
            this.LblRecipe2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlHeader1
            // 
            this.PnlHeader1.BDrawBorderBottom = false;
            this.PnlHeader1.BDrawBorderLeft = false;
            this.PnlHeader1.BDrawBorderRight = false;
            this.PnlHeader1.BDrawBorderTop = false;
            this.PnlHeader1.Controls.Add(this.BtnMinimize1);
            this.PnlHeader1.Controls.Add(this.LblTime1);
            this.PnlHeader1.Controls.Add(this.LblRecipe1);
            this.PnlHeader1.Controls.Add(this.LblTitle1);
            this.PnlHeader1.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlHeader1.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader1.Name = "PnlHeader1";
            this.PnlHeader1.Size = new System.Drawing.Size(1280, 36);
            this.PnlHeader1.TabIndex = 9;
            // 
            // BtnMinimize1
            // 
            this.BtnMinimize1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimize1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnMinimize1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.BtnMinimize1.ForeColor = System.Drawing.Color.White;
            this.BtnMinimize1.Location = new System.Drawing.Point(1238, 0);
            this.BtnMinimize1.Name = "BtnMinimize1";
            this.BtnMinimize1.Size = new System.Drawing.Size(39, 35);
            this.BtnMinimize1.TabIndex = 8;
            this.BtnMinimize1.Text = "_";
            this.BtnMinimize1.UseVisualStyleBackColor = false;
            this.BtnMinimize1.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // LblTime1
            // 
            this.LblTime1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTime1.BackColor = System.Drawing.Color.Coral;
            this.LblTime1.BDrawBorderBottom = false;
            this.LblTime1.BDrawBorderLeft = false;
            this.LblTime1.BDrawBorderRight = true;
            this.LblTime1.BDrawBorderTop = false;
            this.LblTime1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblTime1.ForeColor = System.Drawing.Color.White;
            this.LblTime1.Location = new System.Drawing.Point(1035, 0);
            this.LblTime1.Name = "LblTime1";
            this.LblTime1.OColor = System.Drawing.Color.Black;
            this.LblTime1.Size = new System.Drawing.Size(200, 36);
            this.LblTime1.TabIndex = 7;
            this.LblTime1.Text = "2012-03-02 44:33:22";
            this.LblTime1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRecipe1
            // 
            this.LblRecipe1.BackColor = System.Drawing.Color.DimGray;
            this.LblRecipe1.BDrawBorderBottom = false;
            this.LblRecipe1.BDrawBorderLeft = false;
            this.LblRecipe1.BDrawBorderRight = true;
            this.LblRecipe1.BDrawBorderTop = false;
            this.LblRecipe1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblRecipe1.ForeColor = System.Drawing.Color.White;
            this.LblRecipe1.Location = new System.Drawing.Point(200, 0);
            this.LblRecipe1.Name = "LblRecipe1";
            this.LblRecipe1.OColor = System.Drawing.Color.Black;
            this.LblRecipe1.Size = new System.Drawing.Size(835, 36);
            this.LblRecipe1.TabIndex = 5;
            this.LblRecipe1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitle1
            // 
            this.LblTitle1.BackColor = System.Drawing.Color.RoyalBlue;
            this.LblTitle1.BDrawBorderBottom = false;
            this.LblTitle1.BDrawBorderLeft = false;
            this.LblTitle1.BDrawBorderRight = true;
            this.LblTitle1.BDrawBorderTop = false;
            this.LblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.LblTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblTitle1.Location = new System.Drawing.Point(0, 0);
            this.LblTitle1.Name = "LblTitle1";
            this.LblTitle1.OColor = System.Drawing.Color.Black;
            this.LblTitle1.Size = new System.Drawing.Size(200, 36);
            this.LblTitle1.TabIndex = 4;
            this.LblTitle1.Text = "4Head Scriber";
            this.LblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlFooter
            // 
            this.PnlFooter.BDrawBorderBottom = false;
            this.PnlFooter.BDrawBorderLeft = false;
            this.PnlFooter.BDrawBorderRight = false;
            this.PnlFooter.BDrawBorderTop = true;
            this.PnlFooter.Controls.Add(this.PnlFooter2);
            this.PnlFooter.Controls.Add(this.PnlFooter1);
            this.PnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFooter.Location = new System.Drawing.Point(0, 984);
            this.PnlFooter.Name = "PnlFooter";
            this.PnlFooter.Size = new System.Drawing.Size(1932, 40);
            this.PnlFooter.TabIndex = 1;
            // 
            // PnlFooter2
            // 
            this.PnlFooter2.BDrawBorderBottom = false;
            this.PnlFooter2.BDrawBorderLeft = true;
            this.PnlFooter2.BDrawBorderRight = false;
            this.PnlFooter2.BDrawBorderTop = true;
            this.PnlFooter2.Controls.Add(this.BtnExit2);
            this.PnlFooter2.Controls.Add(this.BtnRecipe2);
            this.PnlFooter2.Controls.Add(this.BtnHome2);
            this.PnlFooter2.Controls.Add(this.BtnSetup2);
            this.PnlFooter2.Controls.Add(this.BtnReport2);
            this.PnlFooter2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFooter2.Location = new System.Drawing.Point(1280, 0);
            this.PnlFooter2.Name = "PnlFooter2";
            this.PnlFooter2.Size = new System.Drawing.Size(652, 40);
            this.PnlFooter2.TabIndex = 6;
            // 
            // BtnExit2
            // 
            this.BtnExit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnExit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnExit2.ForeColor = System.Drawing.Color.White;
            this.BtnExit2.Location = new System.Drawing.Point(530, 2);
            this.BtnExit2.Name = "BtnExit2";
            this.BtnExit2.Size = new System.Drawing.Size(120, 35);
            this.BtnExit2.TabIndex = 5;
            this.BtnExit2.Tag = "";
            this.BtnExit2.Text = "Exit";
            this.BtnExit2.UseVisualStyleBackColor = false;
            this.BtnExit2.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRecipe2
            // 
            this.BtnRecipe2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnRecipe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRecipe2.ForeColor = System.Drawing.Color.White;
            this.BtnRecipe2.Location = new System.Drawing.Point(247, 3);
            this.BtnRecipe2.Name = "BtnRecipe2";
            this.BtnRecipe2.Size = new System.Drawing.Size(120, 35);
            this.BtnRecipe2.TabIndex = 5;
            this.BtnRecipe2.Tag = "RECIPE";
            this.BtnRecipe2.Text = "Recipe";
            this.BtnRecipe2.UseVisualStyleBackColor = false;
            this.BtnRecipe2.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnHome2
            // 
            this.BtnHome2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnHome2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnHome2.ForeColor = System.Drawing.Color.White;
            this.BtnHome2.Location = new System.Drawing.Point(3, 3);
            this.BtnHome2.Name = "BtnHome2";
            this.BtnHome2.Size = new System.Drawing.Size(120, 35);
            this.BtnHome2.TabIndex = 5;
            this.BtnHome2.Tag = "HOME";
            this.BtnHome2.Text = "Home";
            this.BtnHome2.UseVisualStyleBackColor = false;
            this.BtnHome2.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnSetup2
            // 
            this.BtnSetup2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSetup2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSetup2.ForeColor = System.Drawing.Color.White;
            this.BtnSetup2.Location = new System.Drawing.Point(125, 3);
            this.BtnSetup2.Name = "BtnSetup2";
            this.BtnSetup2.Size = new System.Drawing.Size(120, 35);
            this.BtnSetup2.TabIndex = 5;
            this.BtnSetup2.Tag = "SETUP";
            this.BtnSetup2.Text = "Setup";
            this.BtnSetup2.UseVisualStyleBackColor = false;
            this.BtnSetup2.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnReport2
            // 
            this.BtnReport2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnReport2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnReport2.ForeColor = System.Drawing.Color.White;
            this.BtnReport2.Location = new System.Drawing.Point(369, 3);
            this.BtnReport2.Name = "BtnReport2";
            this.BtnReport2.Size = new System.Drawing.Size(120, 35);
            this.BtnReport2.TabIndex = 5;
            this.BtnReport2.Tag = "REPORT";
            this.BtnReport2.Text = "Report";
            this.BtnReport2.UseVisualStyleBackColor = false;
            this.BtnReport2.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // PnlFooter1
            // 
            this.PnlFooter1.BDrawBorderBottom = false;
            this.PnlFooter1.BDrawBorderLeft = false;
            this.PnlFooter1.BDrawBorderRight = false;
            this.PnlFooter1.BDrawBorderTop = true;
            this.PnlFooter1.Controls.Add(this.BtnExit1);
            this.PnlFooter1.Controls.Add(this.BtnHome1);
            this.PnlFooter1.Controls.Add(this.BtnReport1);
            this.PnlFooter1.Controls.Add(this.BtnSetup1);
            this.PnlFooter1.Controls.Add(this.BtnRecipe1);
            this.PnlFooter1.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlFooter1.Location = new System.Drawing.Point(0, 0);
            this.PnlFooter1.Name = "PnlFooter1";
            this.PnlFooter1.Size = new System.Drawing.Size(1280, 40);
            this.PnlFooter1.TabIndex = 6;
            // 
            // BtnExit1
            // 
            this.BtnExit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnExit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnExit1.ForeColor = System.Drawing.Color.White;
            this.BtnExit1.Location = new System.Drawing.Point(1158, 2);
            this.BtnExit1.Name = "BtnExit1";
            this.BtnExit1.Size = new System.Drawing.Size(120, 35);
            this.BtnExit1.TabIndex = 5;
            this.BtnExit1.Tag = "";
            this.BtnExit1.Text = "Exit";
            this.BtnExit1.UseVisualStyleBackColor = false;
            this.BtnExit1.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnHome1
            // 
            this.BtnHome1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnHome1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnHome1.ForeColor = System.Drawing.Color.White;
            this.BtnHome1.Location = new System.Drawing.Point(3, 3);
            this.BtnHome1.Name = "BtnHome1";
            this.BtnHome1.Size = new System.Drawing.Size(120, 35);
            this.BtnHome1.TabIndex = 5;
            this.BtnHome1.Tag = "HOME";
            this.BtnHome1.Text = "Home";
            this.BtnHome1.UseVisualStyleBackColor = false;
            this.BtnHome1.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnReport1
            // 
            this.BtnReport1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnReport1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnReport1.ForeColor = System.Drawing.Color.White;
            this.BtnReport1.Location = new System.Drawing.Point(369, 3);
            this.BtnReport1.Name = "BtnReport1";
            this.BtnReport1.Size = new System.Drawing.Size(120, 35);
            this.BtnReport1.TabIndex = 5;
            this.BtnReport1.Tag = "REPORT";
            this.BtnReport1.Text = "Report";
            this.BtnReport1.UseVisualStyleBackColor = false;
            this.BtnReport1.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnSetup1
            // 
            this.BtnSetup1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSetup1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSetup1.ForeColor = System.Drawing.Color.White;
            this.BtnSetup1.Location = new System.Drawing.Point(125, 3);
            this.BtnSetup1.Name = "BtnSetup1";
            this.BtnSetup1.Size = new System.Drawing.Size(120, 35);
            this.BtnSetup1.TabIndex = 5;
            this.BtnSetup1.Tag = "SETUP";
            this.BtnSetup1.Text = "Setup";
            this.BtnSetup1.UseVisualStyleBackColor = false;
            this.BtnSetup1.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnRecipe1
            // 
            this.BtnRecipe1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnRecipe1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRecipe1.ForeColor = System.Drawing.Color.White;
            this.BtnRecipe1.Location = new System.Drawing.Point(247, 3);
            this.BtnRecipe1.Name = "BtnRecipe1";
            this.BtnRecipe1.Size = new System.Drawing.Size(120, 35);
            this.BtnRecipe1.TabIndex = 5;
            this.BtnRecipe1.Tag = "RECIPE";
            this.BtnRecipe1.Text = "Recipe";
            this.BtnRecipe1.UseVisualStyleBackColor = false;
            this.BtnRecipe1.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // PnlBody
            // 
            this.PnlBody.BDrawBorderBottom = false;
            this.PnlBody.BDrawBorderLeft = false;
            this.PnlBody.BDrawBorderRight = false;
            this.PnlBody.BDrawBorderTop = false;
            this.PnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlBody.Location = new System.Drawing.Point(0, 36);
            this.PnlBody.Name = "PnlBody";
            this.PnlBody.Size = new System.Drawing.Size(1932, 948);
            this.PnlBody.TabIndex = 2;
            // 
            // Timer1000
            // 
            this.Timer1000.Enabled = true;
            this.Timer1000.Interval = 1000;
            this.Timer1000.Tick += new System.EventHandler(this.Timer1000_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1932, 1024);
            this.ControlBox = false;
            this.Controls.Add(this.PnlBody);
            this.Controls.Add(this.PnlFooter);
            this.Controls.Add(this.PnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader2.ResumeLayout(false);
            this.PnlHeader1.ResumeLayout(false);
            this.PnlFooter.ResumeLayout(false);
            this.PnlFooter2.ResumeLayout(false);
            this.PnlFooter1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Jhjo.Component.CPanel PnlHeader;
        private Jhjo.Component.CLabel LblTitle1;
        private Jhjo.Component.CLabel LblTime1;
        private Jhjo.Component.CLabel LblRecipe1;
        private System.Windows.Forms.Button BtnMinimize1;
        private Jhjo.Component.CPanel PnlFooter;
        private System.Windows.Forms.Button BtnHome1;
        private System.Windows.Forms.Button BtnSetup1;
        private System.Windows.Forms.Button BtnRecipe1;
        private System.Windows.Forms.Button BtnReport1;
        private System.Windows.Forms.Button BtnExit1;
        private Jhjo.Component.CPanel PnlBody;
        private System.Windows.Forms.Timer Timer1000;
        private Jhjo.Component.CPanel PnlFooter2;
        private Jhjo.Component.CPanel PnlFooter1;
        private System.Windows.Forms.Button BtnExit2;
        private System.Windows.Forms.Button BtnRecipe2;
        private System.Windows.Forms.Button BtnHome2;
        private System.Windows.Forms.Button BtnSetup2;
        private System.Windows.Forms.Button BtnReport2;
        private Jhjo.Component.CPanel PnlHeader1;
        private Jhjo.Component.CPanel PnlHeader2;
        private System.Windows.Forms.Button BtnMinimize2;
        private Jhjo.Component.CLabel LblTitle2;
        private Jhjo.Component.CLabel LblTime2;
        private Jhjo.Component.CLabel LblRecipe2;
    }
}

