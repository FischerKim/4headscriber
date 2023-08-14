namespace FourHeadScriber
{
    partial class frmMarkDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarkDetail));
            this.LblTitle = new Jhjo.Component.CLabel();
            this.PnlTitle = new Jhjo.Component.CPanel();
            this.PnlSetup = new Jhjo.Component.CPanel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.NudHalfPixel = new System.Windows.Forms.NumericUpDown();
            this.NudThreshold = new System.Windows.Forms.NumericUpDown();
            this.LblTitleSetup = new Jhjo.Component.CLabel();
            this.LblTitleThreshold = new Jhjo.Component.CLabel();
            this.BtnApply = new System.Windows.Forms.Button();
            this.LblTitleHalfPixel = new Jhjo.Component.CLabel();
            this.BtnInspect = new System.Windows.Forms.Button();
            this.NudEdgeLength = new System.Windows.Forms.NumericUpDown();
            this.CmbPolarity1 = new System.Windows.Forms.ComboBox();
            this.CmbPolarity0 = new System.Windows.Forms.ComboBox();
            this.CmbDirection = new System.Windows.Forms.ComboBox();
            this.LblTitlePolarity1 = new Jhjo.Component.CLabel();
            this.LblTitlePolarity0 = new Jhjo.Component.CLabel();
            this.LblTitleEdgeLength = new Jhjo.Component.CLabel();
            this.LblTitleDirection = new Jhjo.Component.CLabel();
            this.PnlResultImage = new Jhjo.Component.CPanel();
            this.CdpResult = new Cognex.VisionPro.Display.CogDisplay();
            this.LblTitleResult = new Jhjo.Component.CLabel();
            this.PnlFindImage = new Jhjo.Component.CPanel();
            this.CdpFind = new Cognex.VisionPro.Display.CogDisplay();
            this.LblTitleFind = new Jhjo.Component.CLabel();
            this.PnlImage = new Jhjo.Component.CPanel();
            this.PnlTitle.SuspendLayout();
            this.PnlSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudHalfPixel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudEdgeLength)).BeginInit();
            this.PnlResultImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CdpResult)).BeginInit();
            this.PnlFindImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CdpFind)).BeginInit();
            this.PnlImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.DarkSlateGray;
            this.LblTitle.BDrawBorderBottom = true;
            this.LblTitle.BDrawBorderLeft = false;
            this.LblTitle.BDrawBorderRight = true;
            this.LblTitle.BDrawBorderTop = true;
            this.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.OColor = System.Drawing.Color.Black;
            this.LblTitle.Size = new System.Drawing.Size(1079, 30);
            this.LblTitle.TabIndex = 2;
            this.LblTitle.Text = "Mark Definition";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlTitle
            // 
            this.PnlTitle.BDrawBorderBottom = false;
            this.PnlTitle.BDrawBorderLeft = false;
            this.PnlTitle.BDrawBorderRight = false;
            this.PnlTitle.BDrawBorderTop = false;
            this.PnlTitle.Controls.Add(this.LblTitle);
            this.PnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTitle.Location = new System.Drawing.Point(0, 0);
            this.PnlTitle.Name = "PnlTitle";
            this.PnlTitle.Size = new System.Drawing.Size(1079, 30);
            this.PnlTitle.TabIndex = 66;
            // 
            // PnlSetup
            // 
            this.PnlSetup.BDrawBorderBottom = false;
            this.PnlSetup.BDrawBorderLeft = true;
            this.PnlSetup.BDrawBorderRight = true;
            this.PnlSetup.BDrawBorderTop = false;
            this.PnlSetup.Controls.Add(this.BtnCancel);
            this.PnlSetup.Controls.Add(this.BtnOK);
            this.PnlSetup.Controls.Add(this.NudHalfPixel);
            this.PnlSetup.Controls.Add(this.NudThreshold);
            this.PnlSetup.Controls.Add(this.LblTitleSetup);
            this.PnlSetup.Controls.Add(this.LblTitleThreshold);
            this.PnlSetup.Controls.Add(this.BtnApply);
            this.PnlSetup.Controls.Add(this.LblTitleHalfPixel);
            this.PnlSetup.Controls.Add(this.BtnInspect);
            this.PnlSetup.Controls.Add(this.NudEdgeLength);
            this.PnlSetup.Controls.Add(this.CmbPolarity1);
            this.PnlSetup.Controls.Add(this.CmbPolarity0);
            this.PnlSetup.Controls.Add(this.CmbDirection);
            this.PnlSetup.Controls.Add(this.LblTitlePolarity1);
            this.PnlSetup.Controls.Add(this.LblTitlePolarity0);
            this.PnlSetup.Controls.Add(this.LblTitleEdgeLength);
            this.PnlSetup.Controls.Add(this.LblTitleDirection);
            this.PnlSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlSetup.Location = new System.Drawing.Point(800, 30);
            this.PnlSetup.Name = "PnlSetup";
            this.PnlSetup.Size = new System.Drawing.Size(279, 330);
            this.PnlSetup.TabIndex = 69;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(154, 249);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(120, 35);
            this.BtnCancel.TabIndex = 66;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnOK.ForeColor = System.Drawing.Color.White;
            this.BtnOK.Location = new System.Drawing.Point(32, 249);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(120, 35);
            this.BtnOK.TabIndex = 66;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // NudHalfPixel
            // 
            this.NudHalfPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.NudHalfPixel.Location = new System.Drawing.Point(124, 183);
            this.NudHalfPixel.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NudHalfPixel.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NudHalfPixel.Name = "NudHalfPixel";
            this.NudHalfPixel.Size = new System.Drawing.Size(150, 24);
            this.NudHalfPixel.TabIndex = 65;
            this.NudHalfPixel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudHalfPixel.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // NudThreshold
            // 
            this.NudThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.NudThreshold.Location = new System.Drawing.Point(124, 153);
            this.NudThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NudThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudThreshold.Name = "NudThreshold";
            this.NudThreshold.Size = new System.Drawing.Size(150, 24);
            this.NudThreshold.TabIndex = 64;
            this.NudThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // LblTitleSetup
            // 
            this.LblTitleSetup.BackColor = System.Drawing.Color.DimGray;
            this.LblTitleSetup.BDrawBorderBottom = true;
            this.LblTitleSetup.BDrawBorderLeft = true;
            this.LblTitleSetup.BDrawBorderRight = true;
            this.LblTitleSetup.BDrawBorderTop = false;
            this.LblTitleSetup.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitleSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleSetup.ForeColor = System.Drawing.Color.White;
            this.LblTitleSetup.Location = new System.Drawing.Point(0, 0);
            this.LblTitleSetup.Name = "LblTitleSetup";
            this.LblTitleSetup.OColor = System.Drawing.Color.Black;
            this.LblTitleSetup.Size = new System.Drawing.Size(279, 30);
            this.LblTitleSetup.TabIndex = 67;
            this.LblTitleSetup.Text = "Setup";
            this.LblTitleSetup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleThreshold
            // 
            this.LblTitleThreshold.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleThreshold.BDrawBorderBottom = true;
            this.LblTitleThreshold.BDrawBorderLeft = true;
            this.LblTitleThreshold.BDrawBorderRight = true;
            this.LblTitleThreshold.BDrawBorderTop = false;
            this.LblTitleThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleThreshold.ForeColor = System.Drawing.Color.White;
            this.LblTitleThreshold.Location = new System.Drawing.Point(0, 150);
            this.LblTitleThreshold.Name = "LblTitleThreshold";
            this.LblTitleThreshold.OColor = System.Drawing.Color.Black;
            this.LblTitleThreshold.Size = new System.Drawing.Size(120, 30);
            this.LblTitleThreshold.TabIndex = 62;
            this.LblTitleThreshold.Text = "Threshold";
            this.LblTitleThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnApply
            // 
            this.BtnApply.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnApply.ForeColor = System.Drawing.Color.White;
            this.BtnApply.Location = new System.Drawing.Point(154, 212);
            this.BtnApply.Name = "BtnApply";
            this.BtnApply.Size = new System.Drawing.Size(120, 35);
            this.BtnApply.TabIndex = 66;
            this.BtnApply.Text = "Apply";
            this.BtnApply.UseVisualStyleBackColor = false;
            this.BtnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // LblTitleHalfPixel
            // 
            this.LblTitleHalfPixel.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleHalfPixel.BDrawBorderBottom = true;
            this.LblTitleHalfPixel.BDrawBorderLeft = true;
            this.LblTitleHalfPixel.BDrawBorderRight = true;
            this.LblTitleHalfPixel.BDrawBorderTop = false;
            this.LblTitleHalfPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleHalfPixel.ForeColor = System.Drawing.Color.White;
            this.LblTitleHalfPixel.Location = new System.Drawing.Point(0, 180);
            this.LblTitleHalfPixel.Name = "LblTitleHalfPixel";
            this.LblTitleHalfPixel.OColor = System.Drawing.Color.Black;
            this.LblTitleHalfPixel.Size = new System.Drawing.Size(120, 30);
            this.LblTitleHalfPixel.TabIndex = 62;
            this.LblTitleHalfPixel.Text = "Half Pixel";
            this.LblTitleHalfPixel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnInspect
            // 
            this.BtnInspect.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnInspect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnInspect.ForeColor = System.Drawing.Color.White;
            this.BtnInspect.Location = new System.Drawing.Point(32, 212);
            this.BtnInspect.Name = "BtnInspect";
            this.BtnInspect.Size = new System.Drawing.Size(120, 35);
            this.BtnInspect.TabIndex = 66;
            this.BtnInspect.Text = "Inspect";
            this.BtnInspect.UseVisualStyleBackColor = false;
            this.BtnInspect.Click += new System.EventHandler(this.BtnInspect_Click);
            // 
            // NudEdgeLength
            // 
            this.NudEdgeLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.NudEdgeLength.Location = new System.Drawing.Point(124, 123);
            this.NudEdgeLength.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NudEdgeLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NudEdgeLength.Name = "NudEdgeLength";
            this.NudEdgeLength.Size = new System.Drawing.Size(150, 24);
            this.NudEdgeLength.TabIndex = 65;
            this.NudEdgeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudEdgeLength.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // CmbPolarity1
            // 
            this.CmbPolarity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPolarity1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.CmbPolarity1.FormattingEnabled = true;
            this.CmbPolarity1.Items.AddRange(new object[] {
            "Dark To Light",
            "Light To Dark",
            "Don\'t Care"});
            this.CmbPolarity1.Location = new System.Drawing.Point(124, 92);
            this.CmbPolarity1.Name = "CmbPolarity1";
            this.CmbPolarity1.Size = new System.Drawing.Size(150, 26);
            this.CmbPolarity1.TabIndex = 63;
            // 
            // CmbPolarity0
            // 
            this.CmbPolarity0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPolarity0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.CmbPolarity0.FormattingEnabled = true;
            this.CmbPolarity0.Items.AddRange(new object[] {
            "Dark To Light",
            "Light To Dark",
            "Don\'t Care"});
            this.CmbPolarity0.Location = new System.Drawing.Point(124, 62);
            this.CmbPolarity0.Name = "CmbPolarity0";
            this.CmbPolarity0.Size = new System.Drawing.Size(150, 26);
            this.CmbPolarity0.TabIndex = 63;
            // 
            // CmbDirection
            // 
            this.CmbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.CmbDirection.FormattingEnabled = true;
            this.CmbDirection.Items.AddRange(new object[] {
            "HORIZONTAL",
            "VERTICAL"});
            this.CmbDirection.Location = new System.Drawing.Point(124, 32);
            this.CmbDirection.Name = "CmbDirection";
            this.CmbDirection.Size = new System.Drawing.Size(150, 26);
            this.CmbDirection.TabIndex = 63;
            this.CmbDirection.SelectedIndexChanged += new System.EventHandler(this.CmbDirection_SelectedIndexChanged);
            // 
            // LblTitlePolarity1
            // 
            this.LblTitlePolarity1.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitlePolarity1.BDrawBorderBottom = true;
            this.LblTitlePolarity1.BDrawBorderLeft = true;
            this.LblTitlePolarity1.BDrawBorderRight = true;
            this.LblTitlePolarity1.BDrawBorderTop = false;
            this.LblTitlePolarity1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitlePolarity1.ForeColor = System.Drawing.Color.White;
            this.LblTitlePolarity1.Location = new System.Drawing.Point(0, 90);
            this.LblTitlePolarity1.Name = "LblTitlePolarity1";
            this.LblTitlePolarity1.OColor = System.Drawing.Color.Black;
            this.LblTitlePolarity1.Size = new System.Drawing.Size(120, 30);
            this.LblTitlePolarity1.TabIndex = 62;
            this.LblTitlePolarity1.Text = "Polarity 1";
            this.LblTitlePolarity1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitlePolarity0
            // 
            this.LblTitlePolarity0.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitlePolarity0.BDrawBorderBottom = true;
            this.LblTitlePolarity0.BDrawBorderLeft = true;
            this.LblTitlePolarity0.BDrawBorderRight = true;
            this.LblTitlePolarity0.BDrawBorderTop = false;
            this.LblTitlePolarity0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitlePolarity0.ForeColor = System.Drawing.Color.White;
            this.LblTitlePolarity0.Location = new System.Drawing.Point(0, 60);
            this.LblTitlePolarity0.Name = "LblTitlePolarity0";
            this.LblTitlePolarity0.OColor = System.Drawing.Color.Black;
            this.LblTitlePolarity0.Size = new System.Drawing.Size(120, 30);
            this.LblTitlePolarity0.TabIndex = 62;
            this.LblTitlePolarity0.Text = "Polarity 0";
            this.LblTitlePolarity0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleEdgeLength
            // 
            this.LblTitleEdgeLength.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleEdgeLength.BDrawBorderBottom = true;
            this.LblTitleEdgeLength.BDrawBorderLeft = true;
            this.LblTitleEdgeLength.BDrawBorderRight = true;
            this.LblTitleEdgeLength.BDrawBorderTop = false;
            this.LblTitleEdgeLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleEdgeLength.ForeColor = System.Drawing.Color.White;
            this.LblTitleEdgeLength.Location = new System.Drawing.Point(0, 120);
            this.LblTitleEdgeLength.Name = "LblTitleEdgeLength";
            this.LblTitleEdgeLength.OColor = System.Drawing.Color.Black;
            this.LblTitleEdgeLength.Size = new System.Drawing.Size(120, 30);
            this.LblTitleEdgeLength.TabIndex = 62;
            this.LblTitleEdgeLength.Text = "Edge Len.";
            this.LblTitleEdgeLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleDirection
            // 
            this.LblTitleDirection.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleDirection.BDrawBorderBottom = true;
            this.LblTitleDirection.BDrawBorderLeft = true;
            this.LblTitleDirection.BDrawBorderRight = true;
            this.LblTitleDirection.BDrawBorderTop = false;
            this.LblTitleDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleDirection.ForeColor = System.Drawing.Color.White;
            this.LblTitleDirection.Location = new System.Drawing.Point(0, 30);
            this.LblTitleDirection.Name = "LblTitleDirection";
            this.LblTitleDirection.OColor = System.Drawing.Color.Black;
            this.LblTitleDirection.Size = new System.Drawing.Size(120, 30);
            this.LblTitleDirection.TabIndex = 62;
            this.LblTitleDirection.Text = "Direction";
            this.LblTitleDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlResultImage
            // 
            this.PnlResultImage.BDrawBorderBottom = false;
            this.PnlResultImage.BDrawBorderLeft = true;
            this.PnlResultImage.BDrawBorderRight = false;
            this.PnlResultImage.BDrawBorderTop = false;
            this.PnlResultImage.Controls.Add(this.CdpResult);
            this.PnlResultImage.Controls.Add(this.LblTitleResult);
            this.PnlResultImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlResultImage.Location = new System.Drawing.Point(400, 0);
            this.PnlResultImage.Name = "PnlResultImage";
            this.PnlResultImage.Size = new System.Drawing.Size(400, 330);
            this.PnlResultImage.TabIndex = 69;
            // 
            // CdpResult
            // 
            this.CdpResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CdpResult.Location = new System.Drawing.Point(0, 30);
            this.CdpResult.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CdpResult.MouseWheelSensitivity = 1D;
            this.CdpResult.Name = "CdpResult";
            this.CdpResult.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CdpResult.OcxState")));
            this.CdpResult.Size = new System.Drawing.Size(400, 300);
            this.CdpResult.TabIndex = 71;
            // 
            // LblTitleResult
            // 
            this.LblTitleResult.BackColor = System.Drawing.Color.DimGray;
            this.LblTitleResult.BDrawBorderBottom = true;
            this.LblTitleResult.BDrawBorderLeft = true;
            this.LblTitleResult.BDrawBorderRight = false;
            this.LblTitleResult.BDrawBorderTop = false;
            this.LblTitleResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitleResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleResult.ForeColor = System.Drawing.Color.White;
            this.LblTitleResult.Location = new System.Drawing.Point(0, 0);
            this.LblTitleResult.Name = "LblTitleResult";
            this.LblTitleResult.OColor = System.Drawing.Color.Black;
            this.LblTitleResult.Size = new System.Drawing.Size(400, 30);
            this.LblTitleResult.TabIndex = 62;
            this.LblTitleResult.Text = "Result";
            this.LblTitleResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlFindImage
            // 
            this.PnlFindImage.BDrawBorderBottom = false;
            this.PnlFindImage.BDrawBorderLeft = false;
            this.PnlFindImage.BDrawBorderRight = false;
            this.PnlFindImage.BDrawBorderTop = false;
            this.PnlFindImage.Controls.Add(this.CdpFind);
            this.PnlFindImage.Controls.Add(this.LblTitleFind);
            this.PnlFindImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlFindImage.Location = new System.Drawing.Point(0, 0);
            this.PnlFindImage.Name = "PnlFindImage";
            this.PnlFindImage.Size = new System.Drawing.Size(400, 330);
            this.PnlFindImage.TabIndex = 70;
            // 
            // CdpFind
            // 
            this.CdpFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CdpFind.Location = new System.Drawing.Point(0, 30);
            this.CdpFind.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CdpFind.MouseWheelSensitivity = 1D;
            this.CdpFind.Name = "CdpFind";
            this.CdpFind.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CdpFind.OcxState")));
            this.CdpFind.Size = new System.Drawing.Size(400, 300);
            this.CdpFind.TabIndex = 71;
            // 
            // LblTitleFind
            // 
            this.LblTitleFind.BackColor = System.Drawing.Color.DimGray;
            this.LblTitleFind.BDrawBorderBottom = true;
            this.LblTitleFind.BDrawBorderLeft = true;
            this.LblTitleFind.BDrawBorderRight = false;
            this.LblTitleFind.BDrawBorderTop = false;
            this.LblTitleFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitleFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleFind.ForeColor = System.Drawing.Color.White;
            this.LblTitleFind.Location = new System.Drawing.Point(0, 0);
            this.LblTitleFind.Name = "LblTitleFind";
            this.LblTitleFind.OColor = System.Drawing.Color.Black;
            this.LblTitleFind.Size = new System.Drawing.Size(400, 30);
            this.LblTitleFind.TabIndex = 62;
            this.LblTitleFind.Text = "Find";
            this.LblTitleFind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlImage
            // 
            this.PnlImage.BDrawBorderBottom = false;
            this.PnlImage.BDrawBorderLeft = false;
            this.PnlImage.BDrawBorderRight = false;
            this.PnlImage.BDrawBorderTop = false;
            this.PnlImage.Controls.Add(this.PnlResultImage);
            this.PnlImage.Controls.Add(this.PnlFindImage);
            this.PnlImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlImage.Location = new System.Drawing.Point(0, 30);
            this.PnlImage.Name = "PnlImage";
            this.PnlImage.Size = new System.Drawing.Size(800, 330);
            this.PnlImage.TabIndex = 71;
            // 
            // frmMarkDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 360);
            this.ControlBox = false;
            this.Controls.Add(this.PnlSetup);
            this.Controls.Add(this.PnlImage);
            this.Controls.Add(this.PnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMarkDetail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMarkDetail";
            this.Load += new System.EventHandler(this.frmMarkDetail_Load);
            this.PnlTitle.ResumeLayout(false);
            this.PnlSetup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NudHalfPixel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudEdgeLength)).EndInit();
            this.PnlResultImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CdpResult)).EndInit();
            this.PnlFindImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CdpFind)).EndInit();
            this.PnlImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Jhjo.Component.CLabel LblTitle;
        private Jhjo.Component.CPanel PnlTitle;
        private Jhjo.Component.CPanel PnlSetup;
        private Jhjo.Component.CLabel LblTitleDirection;
        private Jhjo.Component.CLabel LblTitlePolarity0;
        private Jhjo.Component.CLabel LblTitlePolarity1;
        private System.Windows.Forms.ComboBox CmbDirection;
        private Jhjo.Component.CLabel LblTitleHalfPixel;
        private Jhjo.Component.CLabel LblTitleThreshold;
        private System.Windows.Forms.ComboBox CmbPolarity1;
        private System.Windows.Forms.ComboBox CmbPolarity0;
        private System.Windows.Forms.NumericUpDown NudHalfPixel;
        private System.Windows.Forms.NumericUpDown NudThreshold;
        private System.Windows.Forms.Button BtnInspect;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private Jhjo.Component.CPanel PnlResultImage;
        private Cognex.VisionPro.Display.CogDisplay CdpResult;
        private Jhjo.Component.CLabel LblTitleResult;
        private Jhjo.Component.CPanel PnlFindImage;
        private Cognex.VisionPro.Display.CogDisplay CdpFind;
        private Jhjo.Component.CLabel LblTitleFind;
        private Jhjo.Component.CPanel PnlImage;
        private Jhjo.Component.CLabel LblTitleSetup;
        private System.Windows.Forms.NumericUpDown NudEdgeLength;
        private Jhjo.Component.CLabel LblTitleEdgeLength;
    }
}