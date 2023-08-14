namespace FourHeadScriber
{
    partial class frmCameraSelector
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PnlTop = new Jhjo.Component.CPanel();
            this.LblTitleCamSelector = new Jhjo.Component.CLabel();
            this.PnlButton = new Jhjo.Component.CPanel();
            this.BtnApply = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.PnlMiddleTop = new Jhjo.Component.CPanel();
            this.LblHead4 = new Jhjo.Component.CLabel();
            this.LblHead3 = new Jhjo.Component.CLabel();
            this.LblHead2 = new Jhjo.Component.CLabel();
            this.LblHead1 = new Jhjo.Component.CLabel();
            this.LblTitleHead4 = new Jhjo.Component.CLabel();
            this.LblTitleHead3 = new Jhjo.Component.CLabel();
            this.LblTitleHead2 = new Jhjo.Component.CLabel();
            this.LblTitleHead1 = new Jhjo.Component.CLabel();
            this.BtnSelectHead4 = new System.Windows.Forms.Button();
            this.BtnSelectHead3 = new System.Windows.Forms.Button();
            this.BtnSelectHead2 = new System.Windows.Forms.Button();
            this.BtnSelectHead1 = new System.Windows.Forms.Button();
            this.LblTitleSelect = new Jhjo.Component.CLabel();
            this.PnlMiddleBottom = new Jhjo.Component.CPanel();
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.ColCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblTitleList = new Jhjo.Component.CLabel();
            this.PnlTop.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.PnlMiddleTop.SuspendLayout();
            this.PnlMiddleBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlTop
            // 
            this.PnlTop.BDrawBorderBottom = false;
            this.PnlTop.BDrawBorderLeft = false;
            this.PnlTop.BDrawBorderRight = false;
            this.PnlTop.BDrawBorderTop = false;
            this.PnlTop.Controls.Add(this.LblTitleCamSelector);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(375, 30);
            this.PnlTop.TabIndex = 4;
            // 
            // LblTitleCamSelector
            // 
            this.LblTitleCamSelector.BackColor = System.Drawing.Color.DarkSlateGray;
            this.LblTitleCamSelector.BDrawBorderBottom = true;
            this.LblTitleCamSelector.BDrawBorderLeft = false;
            this.LblTitleCamSelector.BDrawBorderRight = false;
            this.LblTitleCamSelector.BDrawBorderTop = false;
            this.LblTitleCamSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitleCamSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleCamSelector.ForeColor = System.Drawing.Color.White;
            this.LblTitleCamSelector.Location = new System.Drawing.Point(0, 0);
            this.LblTitleCamSelector.Name = "LblTitleCamSelector";
            this.LblTitleCamSelector.OColor = System.Drawing.Color.Black;
            this.LblTitleCamSelector.Size = new System.Drawing.Size(375, 30);
            this.LblTitleCamSelector.TabIndex = 0;
            this.LblTitleCamSelector.Text = "CAMERA SELECTOR";
            this.LblTitleCamSelector.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlButton
            // 
            this.PnlButton.BDrawBorderBottom = false;
            this.PnlButton.BDrawBorderLeft = false;
            this.PnlButton.BDrawBorderRight = false;
            this.PnlButton.BDrawBorderTop = true;
            this.PnlButton.Controls.Add(this.BtnApply);
            this.PnlButton.Controls.Add(this.BtnClose);
            this.PnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlButton.Location = new System.Drawing.Point(0, 399);
            this.PnlButton.Name = "PnlButton";
            this.PnlButton.Size = new System.Drawing.Size(375, 40);
            this.PnlButton.TabIndex = 5;
            // 
            // BtnApply
            // 
            this.BtnApply.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnApply.ForeColor = System.Drawing.Color.White;
            this.BtnApply.Location = new System.Drawing.Point(131, 3);
            this.BtnApply.Name = "BtnApply";
            this.BtnApply.Size = new System.Drawing.Size(120, 35);
            this.BtnApply.TabIndex = 4;
            this.BtnApply.Text = "Apply";
            this.BtnApply.UseVisualStyleBackColor = false;
            this.BtnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(253, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(120, 35);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PnlMiddleTop
            // 
            this.PnlMiddleTop.BDrawBorderBottom = false;
            this.PnlMiddleTop.BDrawBorderLeft = true;
            this.PnlMiddleTop.BDrawBorderRight = false;
            this.PnlMiddleTop.BDrawBorderTop = false;
            this.PnlMiddleTop.Controls.Add(this.LblHead4);
            this.PnlMiddleTop.Controls.Add(this.LblHead3);
            this.PnlMiddleTop.Controls.Add(this.LblHead2);
            this.PnlMiddleTop.Controls.Add(this.LblHead1);
            this.PnlMiddleTop.Controls.Add(this.LblTitleHead4);
            this.PnlMiddleTop.Controls.Add(this.LblTitleHead3);
            this.PnlMiddleTop.Controls.Add(this.LblTitleHead2);
            this.PnlMiddleTop.Controls.Add(this.LblTitleHead1);
            this.PnlMiddleTop.Controls.Add(this.BtnSelectHead4);
            this.PnlMiddleTop.Controls.Add(this.BtnSelectHead3);
            this.PnlMiddleTop.Controls.Add(this.BtnSelectHead2);
            this.PnlMiddleTop.Controls.Add(this.BtnSelectHead1);
            this.PnlMiddleTop.Controls.Add(this.LblTitleSelect);
            this.PnlMiddleTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlMiddleTop.Location = new System.Drawing.Point(0, 30);
            this.PnlMiddleTop.Name = "PnlMiddleTop";
            this.PnlMiddleTop.Size = new System.Drawing.Size(375, 174);
            this.PnlMiddleTop.TabIndex = 7;
            // 
            // LblHead4
            // 
            this.LblHead4.BackColor = System.Drawing.Color.White;
            this.LblHead4.BDrawBorderBottom = false;
            this.LblHead4.BDrawBorderLeft = false;
            this.LblHead4.BDrawBorderRight = true;
            this.LblHead4.BDrawBorderTop = false;
            this.LblHead4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblHead4.ForeColor = System.Drawing.Color.Black;
            this.LblHead4.Location = new System.Drawing.Point(100, 138);
            this.LblHead4.Name = "LblHead4";
            this.LblHead4.OColor = System.Drawing.Color.Black;
            this.LblHead4.Size = new System.Drawing.Size(150, 36);
            this.LblHead4.TabIndex = 7;
            this.LblHead4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblHead3
            // 
            this.LblHead3.BackColor = System.Drawing.Color.White;
            this.LblHead3.BDrawBorderBottom = true;
            this.LblHead3.BDrawBorderLeft = false;
            this.LblHead3.BDrawBorderRight = true;
            this.LblHead3.BDrawBorderTop = false;
            this.LblHead3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblHead3.ForeColor = System.Drawing.Color.Black;
            this.LblHead3.Location = new System.Drawing.Point(100, 102);
            this.LblHead3.Name = "LblHead3";
            this.LblHead3.OColor = System.Drawing.Color.Black;
            this.LblHead3.Size = new System.Drawing.Size(150, 36);
            this.LblHead3.TabIndex = 7;
            this.LblHead3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblHead2
            // 
            this.LblHead2.BackColor = System.Drawing.Color.White;
            this.LblHead2.BDrawBorderBottom = true;
            this.LblHead2.BDrawBorderLeft = false;
            this.LblHead2.BDrawBorderRight = true;
            this.LblHead2.BDrawBorderTop = false;
            this.LblHead2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblHead2.ForeColor = System.Drawing.Color.Black;
            this.LblHead2.Location = new System.Drawing.Point(100, 66);
            this.LblHead2.Name = "LblHead2";
            this.LblHead2.OColor = System.Drawing.Color.Black;
            this.LblHead2.Size = new System.Drawing.Size(150, 36);
            this.LblHead2.TabIndex = 7;
            this.LblHead2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblHead1
            // 
            this.LblHead1.BackColor = System.Drawing.Color.White;
            this.LblHead1.BDrawBorderBottom = true;
            this.LblHead1.BDrawBorderLeft = false;
            this.LblHead1.BDrawBorderRight = true;
            this.LblHead1.BDrawBorderTop = false;
            this.LblHead1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblHead1.ForeColor = System.Drawing.Color.Black;
            this.LblHead1.Location = new System.Drawing.Point(100, 30);
            this.LblHead1.Name = "LblHead1";
            this.LblHead1.OColor = System.Drawing.Color.Black;
            this.LblHead1.Size = new System.Drawing.Size(150, 36);
            this.LblHead1.TabIndex = 7;
            this.LblHead1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTitleHead4
            // 
            this.LblTitleHead4.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleHead4.BDrawBorderBottom = false;
            this.LblTitleHead4.BDrawBorderLeft = false;
            this.LblTitleHead4.BDrawBorderRight = true;
            this.LblTitleHead4.BDrawBorderTop = false;
            this.LblTitleHead4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleHead4.ForeColor = System.Drawing.Color.White;
            this.LblTitleHead4.Location = new System.Drawing.Point(0, 138);
            this.LblTitleHead4.Name = "LblTitleHead4";
            this.LblTitleHead4.OColor = System.Drawing.Color.Black;
            this.LblTitleHead4.Size = new System.Drawing.Size(100, 36);
            this.LblTitleHead4.TabIndex = 6;
            this.LblTitleHead4.Text = "Head #4";
            this.LblTitleHead4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleHead3
            // 
            this.LblTitleHead3.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleHead3.BDrawBorderBottom = true;
            this.LblTitleHead3.BDrawBorderLeft = false;
            this.LblTitleHead3.BDrawBorderRight = true;
            this.LblTitleHead3.BDrawBorderTop = false;
            this.LblTitleHead3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleHead3.ForeColor = System.Drawing.Color.White;
            this.LblTitleHead3.Location = new System.Drawing.Point(0, 102);
            this.LblTitleHead3.Name = "LblTitleHead3";
            this.LblTitleHead3.OColor = System.Drawing.Color.Black;
            this.LblTitleHead3.Size = new System.Drawing.Size(100, 36);
            this.LblTitleHead3.TabIndex = 6;
            this.LblTitleHead3.Text = "Head #3";
            this.LblTitleHead3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleHead2
            // 
            this.LblTitleHead2.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleHead2.BDrawBorderBottom = true;
            this.LblTitleHead2.BDrawBorderLeft = false;
            this.LblTitleHead2.BDrawBorderRight = true;
            this.LblTitleHead2.BDrawBorderTop = false;
            this.LblTitleHead2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleHead2.ForeColor = System.Drawing.Color.White;
            this.LblTitleHead2.Location = new System.Drawing.Point(0, 66);
            this.LblTitleHead2.Name = "LblTitleHead2";
            this.LblTitleHead2.OColor = System.Drawing.Color.Black;
            this.LblTitleHead2.Size = new System.Drawing.Size(100, 36);
            this.LblTitleHead2.TabIndex = 6;
            this.LblTitleHead2.Text = "Head #2";
            this.LblTitleHead2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitleHead1
            // 
            this.LblTitleHead1.BackColor = System.Drawing.Color.CadetBlue;
            this.LblTitleHead1.BDrawBorderBottom = true;
            this.LblTitleHead1.BDrawBorderLeft = false;
            this.LblTitleHead1.BDrawBorderRight = true;
            this.LblTitleHead1.BDrawBorderTop = false;
            this.LblTitleHead1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleHead1.ForeColor = System.Drawing.Color.White;
            this.LblTitleHead1.Location = new System.Drawing.Point(0, 30);
            this.LblTitleHead1.Name = "LblTitleHead1";
            this.LblTitleHead1.OColor = System.Drawing.Color.Black;
            this.LblTitleHead1.Size = new System.Drawing.Size(100, 36);
            this.LblTitleHead1.TabIndex = 6;
            this.LblTitleHead1.Text = "Head #1";
            this.LblTitleHead1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnSelectHead4
            // 
            this.BtnSelectHead4.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSelectHead4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSelectHead4.ForeColor = System.Drawing.Color.White;
            this.BtnSelectHead4.Location = new System.Drawing.Point(253, 139);
            this.BtnSelectHead4.Name = "BtnSelectHead4";
            this.BtnSelectHead4.Size = new System.Drawing.Size(120, 35);
            this.BtnSelectHead4.TabIndex = 5;
            this.BtnSelectHead4.Tag = "HEAD4";
            this.BtnSelectHead4.Text = "Select";
            this.BtnSelectHead4.UseVisualStyleBackColor = false;
            this.BtnSelectHead4.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnSelectHead3
            // 
            this.BtnSelectHead3.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSelectHead3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSelectHead3.ForeColor = System.Drawing.Color.White;
            this.BtnSelectHead3.Location = new System.Drawing.Point(253, 103);
            this.BtnSelectHead3.Name = "BtnSelectHead3";
            this.BtnSelectHead3.Size = new System.Drawing.Size(120, 35);
            this.BtnSelectHead3.TabIndex = 5;
            this.BtnSelectHead3.Tag = "HEAD3";
            this.BtnSelectHead3.Text = "Select";
            this.BtnSelectHead3.UseVisualStyleBackColor = false;
            this.BtnSelectHead3.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnSelectHead2
            // 
            this.BtnSelectHead2.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSelectHead2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSelectHead2.ForeColor = System.Drawing.Color.White;
            this.BtnSelectHead2.Location = new System.Drawing.Point(253, 67);
            this.BtnSelectHead2.Name = "BtnSelectHead2";
            this.BtnSelectHead2.Size = new System.Drawing.Size(120, 35);
            this.BtnSelectHead2.TabIndex = 5;
            this.BtnSelectHead2.Tag = "HEAD2";
            this.BtnSelectHead2.Text = "Select";
            this.BtnSelectHead2.UseVisualStyleBackColor = false;
            this.BtnSelectHead2.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnSelectHead1
            // 
            this.BtnSelectHead1.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnSelectHead1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSelectHead1.ForeColor = System.Drawing.Color.White;
            this.BtnSelectHead1.Location = new System.Drawing.Point(253, 31);
            this.BtnSelectHead1.Name = "BtnSelectHead1";
            this.BtnSelectHead1.Size = new System.Drawing.Size(120, 35);
            this.BtnSelectHead1.TabIndex = 5;
            this.BtnSelectHead1.Tag = "HEAD1";
            this.BtnSelectHead1.Text = "Select";
            this.BtnSelectHead1.UseVisualStyleBackColor = false;
            this.BtnSelectHead1.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // LblTitleSelect
            // 
            this.LblTitleSelect.BackColor = System.Drawing.Color.DimGray;
            this.LblTitleSelect.BDrawBorderBottom = true;
            this.LblTitleSelect.BDrawBorderLeft = false;
            this.LblTitleSelect.BDrawBorderRight = false;
            this.LblTitleSelect.BDrawBorderTop = false;
            this.LblTitleSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitleSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleSelect.ForeColor = System.Drawing.Color.White;
            this.LblTitleSelect.Location = new System.Drawing.Point(0, 0);
            this.LblTitleSelect.Name = "LblTitleSelect";
            this.LblTitleSelect.OColor = System.Drawing.Color.Black;
            this.LblTitleSelect.Size = new System.Drawing.Size(375, 30);
            this.LblTitleSelect.TabIndex = 4;
            this.LblTitleSelect.Text = "Camera Selection";
            this.LblTitleSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlMiddleBottom
            // 
            this.PnlMiddleBottom.BDrawBorderBottom = false;
            this.PnlMiddleBottom.BDrawBorderLeft = false;
            this.PnlMiddleBottom.BDrawBorderRight = false;
            this.PnlMiddleBottom.BDrawBorderTop = false;
            this.PnlMiddleBottom.Controls.Add(this.DgvList);
            this.PnlMiddleBottom.Controls.Add(this.LblTitleList);
            this.PnlMiddleBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMiddleBottom.Location = new System.Drawing.Point(0, 204);
            this.PnlMiddleBottom.Name = "PnlMiddleBottom";
            this.PnlMiddleBottom.Size = new System.Drawing.Size(375, 195);
            this.PnlMiddleBottom.TabIndex = 8;
            // 
            // DgvList
            // 
            this.DgvList.AllowUserToAddRows = false;
            this.DgvList.AllowUserToDeleteRows = false;
            this.DgvList.AllowUserToResizeColumns = false;
            this.DgvList.AllowUserToResizeRows = false;
            this.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCompany,
            this.ColModel,
            this.ColIP,
            this.ColSerial,
            this.ColKey});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvList.DefaultCellStyle = dataGridViewCellStyle4;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DgvList.Location = new System.Drawing.Point(0, 30);
            this.DgvList.MultiSelect = false;
            this.DgvList.Name = "DgvList";
            this.DgvList.RowHeadersVisible = false;
            this.DgvList.RowTemplate.Height = 23;
            this.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvList.Size = new System.Drawing.Size(375, 165);
            this.DgvList.TabIndex = 4;
            // 
            // ColCompany
            // 
            this.ColCompany.DataPropertyName = "COMPANY";
            this.ColCompany.HeaderText = "Company";
            this.ColCompany.Name = "ColCompany";
            this.ColCompany.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColCompany.Visible = false;
            // 
            // ColModel
            // 
            this.ColModel.DataPropertyName = "MODEL";
            this.ColModel.HeaderText = "Model";
            this.ColModel.Name = "ColModel";
            this.ColModel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColIP
            // 
            this.ColIP.DataPropertyName = "IP";
            this.ColIP.HeaderText = "IP";
            this.ColIP.Name = "ColIP";
            this.ColIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColSerial
            // 
            this.ColSerial.DataPropertyName = "SERIAL";
            this.ColSerial.HeaderText = "Serial";
            this.ColSerial.Name = "ColSerial";
            this.ColSerial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColKey
            // 
            this.ColKey.DataPropertyName = "KEY";
            this.ColKey.HeaderText = "KEY";
            this.ColKey.Name = "ColKey";
            this.ColKey.Visible = false;
            // 
            // LblTitleList
            // 
            this.LblTitleList.BackColor = System.Drawing.Color.DimGray;
            this.LblTitleList.BDrawBorderBottom = false;
            this.LblTitleList.BDrawBorderLeft = false;
            this.LblTitleList.BDrawBorderRight = false;
            this.LblTitleList.BDrawBorderTop = true;
            this.LblTitleList.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitleList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.LblTitleList.ForeColor = System.Drawing.Color.White;
            this.LblTitleList.Location = new System.Drawing.Point(0, 0);
            this.LblTitleList.Name = "LblTitleList";
            this.LblTitleList.OColor = System.Drawing.Color.Black;
            this.LblTitleList.Size = new System.Drawing.Size(375, 30);
            this.LblTitleList.TabIndex = 3;
            this.LblTitleList.Text = "Camera List";
            this.LblTitleList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCameraSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 439);
            this.Controls.Add(this.PnlMiddleBottom);
            this.Controls.Add(this.PnlMiddleTop);
            this.Controls.Add(this.PnlButton);
            this.Controls.Add(this.PnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCameraSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera Selector";
            this.Load += new System.EventHandler(this.frmCameraSelector_Load);
            this.PnlTop.ResumeLayout(false);
            this.PnlButton.ResumeLayout(false);
            this.PnlMiddleTop.ResumeLayout(false);
            this.PnlMiddleBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Jhjo.Component.CPanel PnlTop;
        private Jhjo.Component.CLabel LblTitleCamSelector;
        private Jhjo.Component.CPanel PnlButton;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.Button BtnClose;
        private Jhjo.Component.CPanel PnlMiddleTop;
        private Jhjo.Component.CLabel LblHead4;
        private Jhjo.Component.CLabel LblHead3;
        private Jhjo.Component.CLabel LblHead2;
        private Jhjo.Component.CLabel LblHead1;
        private Jhjo.Component.CLabel LblTitleHead4;
        private Jhjo.Component.CLabel LblTitleHead3;
        private Jhjo.Component.CLabel LblTitleHead2;
        private Jhjo.Component.CLabel LblTitleHead1;
        private System.Windows.Forms.Button BtnSelectHead2;
        private System.Windows.Forms.Button BtnSelectHead1;
        private Jhjo.Component.CLabel LblTitleSelect;
        private System.Windows.Forms.Button BtnSelectHead4;
        private System.Windows.Forms.Button BtnSelectHead3;
        private Jhjo.Component.CPanel PnlMiddleBottom;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColKey;
        private Jhjo.Component.CLabel LblTitleList;
    }
}