using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jhjo.Common;

namespace FourHeadScriber
{
    public partial class frmMain : Form
    {
        #region VARIABLE
        private UcScreen m_OScreen = null;
        private UcHome m_OHome = null;
        private UcSetup m_OSetup = null;
        private UcRecipe m_ORecipe = null;
        private UcReport m_OReport = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public frmMain()
        {
            InitializeComponent();

            try
            {
                base.Size = new Size(2560, 1024);

                this.ReadyCamera();
                this.ReadyRecipe();
                this.ReadyScaleInfo();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region EVENT
        #region FORM EVENT
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                CMotionController.It.OPLC.ODisconnected = this.OPLC_Disconnected;
                CMotionController.It.OPLC.OChangeRecipeRequested = this.OPLC_OChangeRecipeRequested;
                CMotionController.It.OPLC.OChangeProductLengthRequested = this.OPLC_OChangeProductLengthRequested;
                
                CTool.It.ORecipeChanged = this.OTool_ORecipeChanged;

                this.m_OHome = new UcHome();
                this.m_OSetup = new UcSetup();
                this.m_ORecipe = new UcRecipe();
                this.m_OReport = new UcReport();
                this.m_OHome.Dock = DockStyle.Fill;
                this.m_OSetup.Dock = DockStyle.Fill;
                this.m_ORecipe.Dock = DockStyle.Fill;
                this.m_OReport.Dock = DockStyle.Fill;
                this.m_OHome.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_OSetup.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_ORecipe.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_OReport.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.SetScreen(this.m_OHome);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CMotionController.It.Close();
                if (CAcquisitionManager.It.OHead1 != null) CAcquisitionManager.It.OHead1.Stop();
                if (CAcquisitionManager.It.OHead2 != null) CAcquisitionManager.It.OHead2.Stop();
                if (CAcquisitionManager.It.OHead3 != null) CAcquisitionManager.It.OHead3.Stop();
                if (CAcquisitionManager.It.OHead4 != null) CAcquisitionManager.It.OHead4.Stop();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
                

        #region BUTTON EVENT
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnScreen_Click(object sender, EventArgs e)
        {
            try
            {
                base.Size = new Size(2560, 1024);

                switch ((string)((Control)sender).Tag)
                {
                    case "HOME":
                        this.SetScreen(this.m_OHome);
                        break;

                    case "SETUP":
                        this.SetScreen(this.m_OSetup);
                        break;

                    case "RECIPE":
                        frmPassword OWindow = new frmPassword();
                        if (OWindow.ShowDialog() == DialogResult.OK)
                        {
                            this.SetScreen(this.m_ORecipe);
                        }
                        break;

                    case "REPORT":
                        this.SetScreen(this.m_OReport);
                        break;
                }
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
                this.Close();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region ETC EVENT
        private void Timer1000_Tick(object sender, EventArgs e)
        {
            try
            {
                this.LblTime1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.LblTime2.Text = this.LblTime1.Text;

                CTool.It.OAlignmentTool.OHead1.ODisplayer.CheckFrameCount();
                CTool.It.OAlignmentTool.OHead2.ODisplayer.CheckFrameCount();
                CTool.It.OAlignmentTool.OHead3.ODisplayer.CheckFrameCount();
                CTool.It.OAlignmentTool.OHead4.ODisplayer.CheckFrameCount();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void OScreen_ScreenFixed(bool BFixed)
        {
            try
            {
                this.BtnHome1.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnSetup1.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnRecipe1.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnReport1.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnExit1.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnHome2.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnSetup2.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnRecipe2.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnReport2.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnExit2.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnHome1.Enabled = !BFixed;
                this.BtnSetup1.Enabled = !BFixed;
                this.BtnRecipe1.Enabled = !BFixed;
                this.BtnReport1.Enabled = !BFixed;
                this.BtnExit1.Enabled = !BFixed;
                this.BtnHome2.Enabled = !BFixed;
                this.BtnSetup2.Enabled = !BFixed;
                this.BtnRecipe2.Enabled = !BFixed;
                this.BtnReport2.Enabled = !BFixed;
                this.BtnExit2.Enabled = !BFixed;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OTool_ORecipeChanged()
        {
            try
            {
                if (CTool.It.ORecipe != null)
                {
                    this.LblRecipe1.Text = CTool.It.ORecipe.I32ID + "." + CTool.It.ORecipe.StrName;
                    this.LblRecipe2.Text = this.LblRecipe1.Text;
                }

                this.m_OScreen.NotifyRecipeChanged();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void OPLC_Disconnected(int I32Connection)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    this.Invoke(new CPLC.DisconnectedHandler(this.OPLC_Disconnected), I32Connection);
                }
                else
                {
                    CMsgBox.Error("Disconnected PLC : 0x" + I32Connection.ToString("X"));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void ODisplayer_OCameraDisconnected(string StrDisplayName)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    this.Invoke(new CDisplayTool.CameraDisconnectedHandler(this.ODisplayer_OCameraDisconnected), StrDisplayName);
                }
                else
                {
                    CMsgBox.Error("Disconnected Camera : " + StrDisplayName);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        private void OPLC_OChangeRecipeRequested(int I32ID)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CPLC.ChangeRecipeRequestedHandler(this.OPLC_OChangeRecipeRequested), I32ID);
                }
                else
                {
                    if ((CTool.It.ORecipe == null) || (CTool.It.ORecipe.I32ID != I32ID))
                    {
                        if (CTool.It.OAlignmentTool.CanChangeRecipe() == true)
                        {
                            foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                            {
                                if (_Item.I32ID != I32ID) continue;

                                CTool.It.ORecipe = new CRecipe(_Item);
                                CMotionController.It.OPLC.I32VisionRecipeID = I32ID;
                                break;
                            }
                        }
                    }               
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OPLC_OChangeProductLengthRequested(double F64ZeroLength, double F64NinetyLength)
        {
            try
            {
                //if (base.InvokeRequired == true)
                //{
                //    base.BeginInvoke(new CPLC.ChangeProductLengthRequestedHandler(this.OPLC_OChangeProductLengthRequested), F64ZeroLength, F64NinetyLength);
                //}
                //else
                //{
                //    if ((CTool.It.ORecipe != null) && (CTool.It.ORecipe.OZero != null) && CTool.It.ORecipe.ONinety != null)
                //    {
                //        CTool.It.ORecipe.OZero.F64AlignmentProductLength = F64ZeroLength;
                //        CTool.It.ORecipe.ONinety.F64AlignmentProductLength = F64NinetyLength;
                //    }
                //}
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        private void ReadyCamera()
        {
            try
            {
                CTool.It.OAlignmentTool.OHead1.ODisplayer.StrDisplayName = "HEAD #1";
                CTool.It.OAlignmentTool.OHead2.ODisplayer.StrDisplayName = "HEAD #2";
                CTool.It.OAlignmentTool.OHead3.ODisplayer.StrDisplayName = "HEAD #3";
                CTool.It.OAlignmentTool.OHead4.ODisplayer.StrDisplayName = "HEAD #4";

                CTool.It.OAlignmentTool.OHead1.ODisplayer.OView = CAcquisitionManager.It.OHead1;
                CTool.It.OAlignmentTool.OHead2.ODisplayer.OView = CAcquisitionManager.It.OHead2;
                CTool.It.OAlignmentTool.OHead3.ODisplayer.OView = CAcquisitionManager.It.OHead3;
                CTool.It.OAlignmentTool.OHead4.ODisplayer.OView = CAcquisitionManager.It.OHead4;

                CTool.It.OAlignmentTool.OHead1.ODisplayer.OCameraDisconnected = this.ODisplayer_OCameraDisconnected;
                CTool.It.OAlignmentTool.OHead2.ODisplayer.OCameraDisconnected = this.ODisplayer_OCameraDisconnected;
                CTool.It.OAlignmentTool.OHead3.ODisplayer.OCameraDisconnected = this.ODisplayer_OCameraDisconnected;
                CTool.It.OAlignmentTool.OHead4.ODisplayer.OCameraDisconnected = this.ODisplayer_OCameraDisconnected;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void ReadyRecipe()
        {
            try
            {
                int I32ID = CMotionController.It.OPLC.GetMotionRecipeID();

                if (I32ID > 0)
                {
                    foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                    {
                        if (_Item.I32ID != I32ID) continue;

                        CTool.It.ORecipe = new CRecipe(_Item);
                        this.LblRecipe1.Text = _Item.I32ID + "." + _Item.StrName;
                        this.LblRecipe2.Text = _Item.I32ID + "." + _Item.StrName;
                        CMotionController.It.OPLC.I32VisionRecipeID = I32ID;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void ReadyScaleInfo()
        {
            try
            {
                CTool.It.OAlignmentTool.OScaleHead1 = CDB.It[CDB.TABLE_MOTION_HEAD1].Select();
                CTool.It.OAlignmentTool.OScaleHead2 = CDB.It[CDB.TABLE_MOTION_HEAD2].Select();
                CTool.It.OAlignmentTool.OScaleHead3 = CDB.It[CDB.TABLE_MOTION_HEAD3].Select();
                CTool.It.OAlignmentTool.OScaleHead4 = CDB.It[CDB.TABLE_MOTION_HEAD4].Select();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void SetScreen(UcScreen OScreen)
        {
            try
            {
                if (this.m_OScreen == null)
                {
                    OScreen.Add();
                    this.PnlBody.Controls.Add(OScreen);
                    this.m_OScreen = OScreen;
                }
                else
                {
                    if (this.m_OScreen.GetType() != OScreen.GetType())
                    {
                        this.m_OScreen.Remove();
                        OScreen.Add();

                        this.PnlBody.Controls.Add(OScreen);
                        OScreen.BringToFront();
                        this.PnlBody.Controls.Remove(this.m_OScreen);

                        this.m_OScreen = OScreen;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
