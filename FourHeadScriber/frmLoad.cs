using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Jhjo.Tool;
using PylonC.NET;
using Daekhon.Common;
using Jhjo.Common;
using Daekhon.Acquisition.Basler;

namespace FourHeadScriber
{
    public partial class frmLoad : Form
    {
        #region VARIABLE
        private CCameraInfo m_OHead1 = null;
        private CCameraInfo m_OHead2 = null;
        private CCameraInfo m_OHead3 = null;
        private CCameraInfo m_OHead4 = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public frmLoad()
        {
            InitializeComponent();
        }
        #endregion


        #region EVENT
        #region FORM EVENT
        private void frmLoad_Load(object sender, EventArgs e)
        {
            try
            {
                CImageSaveTool.USE_DELETE = true;
                CImageSaveTool.DELETE_TIME_UNIT = ETIME_UNIT.DAY;
                CImageSaveTool.IMAGE_MANAGE_DIR = new List<string>();
                CImageSaveTool.IMAGE_MANAGE_DIR.Add(".\\Image\\Alignment\\{0:yyyy}\\{0:MM}\\{0:dd}");
                CImageSaveTool.IMAGE_MANAGE_DIR.Add(".\\Image\\DirectCut\\{0:yyyy}\\{0:MM}\\{0:dd}");
                CImageSaveTool.IMAGE_MANAGE_DIR.Add(".\\Image\\CrossCut\\{0:yyyy}\\{0:MM}\\{0:dd}");
                CImageSaveTool.IMAGE_MANAGE_PERIOD = 7;
                CImageSaveTool.It.Load();

                if (File.Exists(".\\System.ini") == false)
                {
                    File.WriteAllText(".\\System.ini", FourHeadScriber.Properties.Resources.System);
                }

                StringBuilder OHead1 = new StringBuilder();
                StringBuilder OHead2 = new StringBuilder();
                StringBuilder OHead3 = new StringBuilder();
                StringBuilder OHead4 = new StringBuilder();
                frmLoad.GetPrivateProfileString("HEAD1", "IP", string.Empty, OHead1, 255, ".\\System.ini");
                frmLoad.GetPrivateProfileString("HEAD2", "IP", string.Empty, OHead2, 255, ".\\System.ini");
                frmLoad.GetPrivateProfileString("HEAD3", "IP", string.Empty, OHead3, 255, ".\\System.ini");
                frmLoad.GetPrivateProfileString("HEAD4", "IP", string.Empty, OHead4, 255, ".\\System.ini");
              

                uint U32Count = Pylon.EnumerateDevices();
                if (U32Count != 0)
                {
                    string StrIP = string.Empty;

                    for (uint _Index = 0; _Index < U32Count; _Index++)
                    {
                        PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(_Index);
                        StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");

                        if (StrIP == OHead1.ToString())
                        {
                            this.m_OHead1 = new CCameraInfo();
                            this.m_OHead1.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                            this.m_OHead1.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                            this.m_OHead1.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                            this.m_OHead1.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                            this.m_OHead1.OKey = _Index;
                        }
                        if (StrIP == OHead2.ToString())
                        {
                            this.m_OHead2 = new CCameraInfo();
                            this.m_OHead2.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                            this.m_OHead2.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                            this.m_OHead2.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                            this.m_OHead2.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                            this.m_OHead2.OKey = _Index;
                        }
                        if (StrIP == OHead3.ToString())
                        {
                            this.m_OHead3 = new CCameraInfo();
                            this.m_OHead3.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                            this.m_OHead3.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                            this.m_OHead3.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                            this.m_OHead3.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                            this.m_OHead3.OKey = _Index;
                        }
                        if (StrIP == OHead4.ToString())
                        {
                            this.m_OHead4 = new CCameraInfo();
                            this.m_OHead4.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                            this.m_OHead4.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                            this.m_OHead4.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                            this.m_OHead4.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                            this.m_OHead4.OKey = _Index;
                        }
                    }
                }

                if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
                if (this.m_OHead2 != null) this.LblHead2.Text = this.m_OHead2.StrIP;
                if (this.m_OHead3 != null) this.LblHead3.Text = this.m_OHead3.StrIP;
                if (this.m_OHead4 != null) this.LblHead4.Text = this.m_OHead4.StrIP;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region BUTTON EVENT
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                frmPassword OWindow1 = new frmPassword();
                if (OWindow1.ShowDialog() == DialogResult.OK)
                {
                    frmCameraSelector OWindow2 = new frmCameraSelector();
                    OWindow2.OHead1 = this.m_OHead1;
                    OWindow2.OHead2 = this.m_OHead2;
                    OWindow2.OHead3 = this.m_OHead3;
                    OWindow2.OHead4 = this.m_OHead4;

                    if (OWindow2.ShowDialog() == DialogResult.OK)
                    {
                        this.m_OHead1 = OWindow2.OHead1;
                        this.m_OHead2 = OWindow2.OHead2;
                        this.m_OHead3 = OWindow2.OHead3;
                        this.m_OHead4 = OWindow2.OHead4;

                        if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
                        if (this.m_OHead2 != null) this.LblHead2.Text = this.m_OHead2.StrIP;
                        if (this.m_OHead3 != null) this.LblHead3.Text = this.m_OHead3.StrIP;
                        if (this.m_OHead4 != null) this.LblHead4.Text = this.m_OHead4.StrIP;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.m_OHead1 == null) ||
                    (this.m_OHead2 == null) ||
                    (this.m_OHead3 == null) ||
                    (this.m_OHead4 == null))
                {
                    CMsgBox.Warning("Please select camera for Vision");
                    return;
                }
                
                if (this.m_OHead1 != null)
                {
                    frmLoad.WritePrivateProfileString("HEAD1", "Company", this.m_OHead1.StrVender, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "Product", this.m_OHead1.StrModel, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "Serial", this.m_OHead1.StrSerial, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "IP", this.m_OHead1.StrIP, ".\\System.ini");
                }
                if (this.m_OHead2 != null)
                {
                    frmLoad.WritePrivateProfileString("HEAD2", "Company", this.m_OHead2.StrVender, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD2", "Product", this.m_OHead2.StrModel, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD2", "Serial", this.m_OHead2.StrSerial, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD2", "IP", this.m_OHead2.StrIP, ".\\System.ini");
                }
                if (this.m_OHead3 != null)
                {
                    frmLoad.WritePrivateProfileString("HEAD3", "Company", this.m_OHead3.StrVender, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD3", "Product", this.m_OHead3.StrModel, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD3", "Serial", this.m_OHead3.StrSerial, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD3", "IP", this.m_OHead3.StrIP, ".\\System.ini");
                }
                if (this.m_OHead4 != null)
                {
                    frmLoad.WritePrivateProfileString("HEAD4", "Company", this.m_OHead4.StrVender, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD4", "Product", this.m_OHead4.StrModel, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD4", "Serial", this.m_OHead4.StrSerial, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD4", "IP", this.m_OHead4.StrIP, ".\\System.ini");
                }
                
                CDB.It.Load();
                CSign.It.Init();
               // CMsgBox.Warning(CSign.CALIBRATION_HEAD1X_SIGN.ToString());
                
                CRecipeManager.It.Load();

                CAcquisitionManager.It.OHead1 = new CBasler(this.m_OHead1);
                CAcquisitionManager.It.OHead2 = new CBasler(this.m_OHead2);
                CAcquisitionManager.It.OHead3 = new CBasler(this.m_OHead3);
                CAcquisitionManager.It.OHead4 = new CBasler(this.m_OHead4);
                CAcquisitionManager.It.Setup();

                this.BtnEnter.BackColor = SystemColors.Control;
                this.BtnExit.BackColor = SystemColors.Control;
                this.BtnEnter.Enabled = false;
                this.BtnExit.Enabled = false;

                CMotionController.It.Open(this.OPLC_ConnectionCompleted);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnExit_Click(object sender, EventArgs e)
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


        #region ETC EVENT
        private void OPLC_ConnectionCompleted(int I32Connection)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CPLC.ConnectionCompletedHandler(this.OPLC_ConnectionCompleted), I32Connection);
                }
                else
                {
                    if (I32Connection == 0)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        CMsgBox.Warning("Cannot connect to PLC : 0x" + I32Connection.ToString("X"));
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        #region EXTERNAL FUNCTION
        [DllImport("kernel32")]
        public static extern bool GetPrivateProfileString(string StrAppName, string StrKey, string StrDefault, StringBuilder StrValue, int I32Size, string StrFile);


        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string StrAppName, string StrKey, string StrValue, string StrFile);
        #endregion
        #endregion   
    }
}
