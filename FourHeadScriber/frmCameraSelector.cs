using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PylonC.NET;
using Daekhon.Common;
using Jhjo.Common;
using Jhjo.Tool;

namespace FourHeadScriber
{
    public partial class frmCameraSelector : Form
    {
        #region VARIABLE
        private CCameraInfo m_OHead1 = null;
        private CCameraInfo m_OHead2 = null;
        private CCameraInfo m_OHead3 = null;
        private CCameraInfo m_OHead4 = null;
        #endregion


        #region PROPERTIES
        public CCameraInfo OHead1
        {
            get { return this.m_OHead1; }
            set { this.m_OHead1 = value; }
        }


        public CCameraInfo OHead2
        {
            get { return this.m_OHead2; }
            set { this.m_OHead2 = value; }
        }


        public CCameraInfo OHead3
        {
            get { return this.m_OHead3; }
            set { this.m_OHead3 = value; }
        }


        public CCameraInfo OHead4
        {
            get { return this.m_OHead4; }
            set { this.m_OHead4 = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public frmCameraSelector()
        {
            InitializeComponent();
        }
        #endregion


        #region EVENT
        #region FORM EVENT
        private void frmCameraSelector_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable ODataSource = new DataTable();
                ODataSource.Columns.Add("COMPANY", typeof(string));
                ODataSource.Columns.Add("MODEL", typeof(string));
                ODataSource.Columns.Add("IP", typeof(string));
                ODataSource.Columns.Add("SERIAL", typeof(string));
                ODataSource.Columns.Add("KEY", typeof(uint));

                uint U32Count = Pylon.EnumerateDevices();
                for (uint _Index = 0; _Index < U32Count; _Index++)
                {
                    PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(_Index);

                    DataRow ORow = ODataSource.NewRow();
                    ORow["COMPANY"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                    ORow["MODEL"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                    ORow["IP"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                    ORow["SERIAL"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                    ORow["KEY"] = _Index;
                    ODataSource.Rows.Add(ORow);
                }

                this.DgvList.DataSource = ODataSource;


                if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
                if (this.m_OHead2 != null) this.LblHead2.Text = this.m_OHead2.StrIP;
                if (this.m_OHead3 != null) this.LblHead3.Text = this.m_OHead3.StrIP;
                if (this.m_OHead4 != null) this.LblHead4.Text = this.m_OHead4.StrIP;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region BUTTON EVENT
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DgvList.CurrentRow == null) return;

                string StrKind = (string)((Button)sender).Tag;
                string StrIP = (string)(((DataRowView)(this.DgvList.CurrentRow.DataBoundItem)).Row)["IP"];

                switch (StrKind)
                {
                    case "HEAD1":
                        this.LblHead1.Text = StrIP;
                        if (StrIP == this.LblHead2.Text) this.LblHead2.Text = string.Empty;
                        if (StrIP == this.LblHead3.Text) this.LblHead3.Text = string.Empty;
                        if (StrIP == this.LblHead4.Text) this.LblHead4.Text = string.Empty;
                        break;

                    case "HEAD2":
                        this.LblHead2.Text = StrIP;
                        if (StrIP == this.LblHead1.Text) this.LblHead1.Text = string.Empty;
                        if (StrIP == this.LblHead3.Text) this.LblHead3.Text = string.Empty;
                        if (StrIP == this.LblHead4.Text) this.LblHead4.Text = string.Empty;
                        break;

                    case "HEAD3":
                        this.LblHead3.Text = StrIP;
                        if (StrIP == this.LblHead1.Text) this.LblHead1.Text = string.Empty;
                        if (StrIP == this.LblHead2.Text) this.LblHead2.Text = string.Empty;
                        if (StrIP == this.LblHead4.Text) this.LblHead4.Text = string.Empty;
                        break;

                    case "HEAD4":
                        this.LblHead4.Text = StrIP;
                        if (StrIP == this.LblHead1.Text) this.LblHead1.Text = string.Empty;
                        if (StrIP == this.LblHead2.Text) this.LblHead2.Text = string.Empty;
                        if (StrIP == this.LblHead3.Text) this.LblHead3.Text = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.LblHead1.Text) == true) this.m_OHead1 = null;
                if (String.IsNullOrEmpty(this.LblHead2.Text) == true) this.m_OHead2 = null;
                if (String.IsNullOrEmpty(this.LblHead3.Text) == true) this.m_OHead3 = null;
                if (String.IsNullOrEmpty(this.LblHead4.Text) == true) this.m_OHead4 = null;


                DataTable ODataSource = (DataTable)this.DgvList.DataSource;

                foreach (DataRow _Item in ODataSource.Rows)
                {
                    if ((string)_Item["IP"] == this.LblHead1.Text)
                    {
                        if (this.m_OHead1 == null) this.m_OHead1 = new CCameraInfo();
                        this.m_OHead1.StrVender = (string)_Item["COMPANY"];
                        this.m_OHead1.StrModel = (string)_Item["MODEL"];
                        this.m_OHead1.StrIP = (string)_Item["IP"];
                        this.m_OHead1.StrSerial = (string)_Item["SERIAL"];
                        this.m_OHead1.OKey = (uint)_Item["KEY"];
                    }
                    else if ((string)_Item["IP"] == this.LblHead2.Text)
                    {
                        if (this.m_OHead2 == null) this.m_OHead2 = new CCameraInfo();
                        this.m_OHead2.StrVender = (string)_Item["COMPANY"];
                        this.m_OHead2.StrModel = (string)_Item["MODEL"];
                        this.m_OHead2.StrIP = (string)_Item["IP"];
                        this.m_OHead2.StrSerial = (string)_Item["SERIAL"];
                        this.m_OHead2.OKey = (uint)_Item["KEY"];
                    }
                    else if ((string)_Item["IP"] == this.LblHead3.Text)
                    {
                        if (this.m_OHead3 == null) this.m_OHead3 = new CCameraInfo();
                        this.m_OHead3.StrVender = (string)_Item["COMPANY"];
                        this.m_OHead3.StrModel = (string)_Item["MODEL"];
                        this.m_OHead3.StrIP = (string)_Item["IP"];
                        this.m_OHead3.StrSerial = (string)_Item["SERIAL"];
                        this.m_OHead3.OKey = (uint)_Item["KEY"];
                    }
                    else if ((string)_Item["IP"] == this.LblHead4.Text)
                    {
                        if (this.m_OHead4 == null) this.m_OHead4 = new CCameraInfo();
                        this.m_OHead4.StrVender = (string)_Item["COMPANY"];
                        this.m_OHead4.StrModel = (string)_Item["MODEL"];
                        this.m_OHead4.StrIP = (string)_Item["IP"];
                        this.m_OHead4.StrSerial = (string)_Item["SERIAL"];
                        this.m_OHead4.OKey = (uint)_Item["KEY"];
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
        #endregion
    }
}
