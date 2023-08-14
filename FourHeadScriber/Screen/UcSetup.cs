using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jhjo.Common;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using System.Threading;

namespace FourHeadScriber
{
    public partial class UcSetup : UcScreen
    {
        #region VARIABLE
        private CSetupDisplayer m_OView1 = null;
        private CSetupDisplayer m_OView2 = null;
        private CSetupDisplayer m_OView3 = null;
        private CSetupDisplayer m_OView4 = null;

        private int m_I32HeadNo = 0;
        private CSetupDisplayer m_OHead1 = null;
        private CSetupDisplayer m_OHead2 = null;
        private CSetupDisplayer m_OHead3 = null;
        private CSetupDisplayer m_OHead4 = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcSetup()
        {
            InitializeComponent();

            try
            {
                this.m_OView1 = new CSetupDisplayer(this.CdpView1, this.CtbView1, this.CsbView1);
                this.m_OView2 = new CSetupDisplayer(this.CdpView2, this.CtbView2, this.CsbView2);
                this.m_OView3 = new CSetupDisplayer(this.CdpView3, this.CtbView3, this.CsbView3);
                this.m_OView4 = new CSetupDisplayer(this.CdpView4, this.CtbView4, this.CsbView4);


                this.NudCamHead1Gain.Minimum = CAcquisitionManager.It.OHead1.I32GainMin;
                this.NudCamHead1Gain.Maximum = CAcquisitionManager.It.OHead1.I32GainMax;
                this.NudCamHead1Gain.Value = CAcquisitionManager.It.OHead1.I32Gain;
                this.NudCamHead1ExpoTime.Minimum = CAcquisitionManager.It.OHead1.I32ExposureTimeMin;
                this.NudCamHead1ExpoTime.Maximum = CAcquisitionManager.It.OHead1.I32ExposureTimeMax;
                this.NudCamHead1ExpoTime.Value = CAcquisitionManager.It.OHead1.I32ExposureTime;
                this.NudCamHead2Gain.Minimum = CAcquisitionManager.It.OHead2.I32GainMin;
                this.NudCamHead2Gain.Maximum = CAcquisitionManager.It.OHead2.I32GainMax;
                this.NudCamHead2Gain.Value = CAcquisitionManager.It.OHead2.I32Gain;
                this.NudCamHead2ExpoTime.Minimum = CAcquisitionManager.It.OHead2.I32ExposureTimeMin;
                this.NudCamHead2ExpoTime.Maximum = CAcquisitionManager.It.OHead2.I32ExposureTimeMax;
                this.NudCamHead2ExpoTime.Value = CAcquisitionManager.It.OHead2.I32ExposureTime;
                this.NudCamHead3Gain.Minimum = CAcquisitionManager.It.OHead3.I32GainMin;
                this.NudCamHead3Gain.Maximum = CAcquisitionManager.It.OHead3.I32GainMax;
                this.NudCamHead3Gain.Value = CAcquisitionManager.It.OHead3.I32Gain;
                this.NudCamHead3ExpoTime.Minimum = CAcquisitionManager.It.OHead3.I32ExposureTimeMin;
                this.NudCamHead3ExpoTime.Maximum = CAcquisitionManager.It.OHead3.I32ExposureTimeMax;
                this.NudCamHead3ExpoTime.Value = CAcquisitionManager.It.OHead3.I32ExposureTime;
                this.NudCamHead4Gain.Minimum = CAcquisitionManager.It.OHead4.I32GainMin;
                this.NudCamHead4Gain.Maximum = CAcquisitionManager.It.OHead4.I32GainMax;
                this.NudCamHead4Gain.Value = CAcquisitionManager.It.OHead4.I32Gain;
                this.NudCamHead4ExpoTime.Minimum = CAcquisitionManager.It.OHead4.I32ExposureTimeMin;
                this.NudCamHead4ExpoTime.Maximum = CAcquisitionManager.It.OHead4.I32ExposureTimeMax;
                this.NudCamHead4ExpoTime.Value = CAcquisitionManager.It.OHead4.I32ExposureTime;


                this.BtnCalibrationStart.BackColor = SystemColors.Control;
                this.BtnCalibrationStop.BackColor = SystemColors.Control;
                this.BtnCalibrationStart.Enabled = false;
                this.BtnCalibrationStop.Enabled = false;

                this.BtnView1.Text = CEnvironment.It.I32HeadOfView1.ToString();
                this.BtnView2.Text = CEnvironment.It.I32HeadOfView2.ToString();
                this.BtnView3.Text = CEnvironment.It.I32HeadOfView3.ToString();
                this.BtnView4.Text = CEnvironment.It.I32HeadOfView4.ToString();
                this.BtnView1.BackColor = SystemColors.Control;
                this.BtnView2.BackColor = SystemColors.Control;
                this.BtnView3.BackColor = SystemColors.Control;
                this.BtnView4.BackColor = SystemColors.Control;
                this.BtnView1.Enabled = false;
                this.BtnView2.Enabled = false;
                this.BtnView3.Enabled = false;
                this.BtnView4.Enabled = false;

                this.BtnResetViewSetup.BackColor = Color.SteelBlue;
                this.BtnApplyViewSetup.BackColor = SystemColors.Control;
                this.BtnCancelViewSetup.BackColor = SystemColors.Control;
                this.BtnResetViewSetup.Enabled = true;
                this.BtnApplyViewSetup.Enabled = false;
                this.BtnCancelViewSetup.Enabled = false;

                this.BtnStart1.BackColor = Color.SteelBlue;
                this.BtnStart2.BackColor = Color.SteelBlue;
                this.BtnStop1.BackColor = SystemColors.Control;
                this.BtnStop2.BackColor = SystemColors.Control;
                this.BtnStart1.Enabled = true;
                this.BtnStart2.Enabled = true;
                this.BtnStop1.Enabled = false;
                this.BtnStop2.Enabled = false;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        
        #region EVENT
        #region BUTTON EVENT
        private void BtnShowChecker_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((string)((Control)sender).Tag)
                {
                    case "VIEW1":
                        this.m_OView1.ShowLengthChecker();
                        break;

                    case "VIEW2":
                        this.m_OView2.ShowLengthChecker();
                        break;

                    case "VIEW3":
                        this.m_OView3.ShowLengthChecker();
                        break;

                    case "VIEW4":
                        this.m_OView4.ShowLengthChecker();
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnClearChecker_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((string)((Control)sender).Tag)
                {
                    case "VIEW1":
                        this.m_OView1.HideLengthChecker();
                        break;

                    case "VIEW2":
                        this.m_OView2.HideLengthChecker();
                        break;

                    case "VIEW3":
                        this.m_OView3.HideLengthChecker();
                        break;

                    case "VIEW4":
                        this.m_OView4.HideLengthChecker();
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        

        private void BtnCalibrationStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (CTool.It.ORecipe == null)
                {
                    CMsgBox.Warning("Please assign a recipe to calibration!");
                    return;
                }
                //1212
                int I32Count = 0;
                if (this.BtnEnabled1.BackColor == Color.Lime)
                {
                    I32Count++;
                }
                if (this.BtnEnabled2.BackColor == Color.Lime)
                {
                    I32Count++;
                }
                if (this.BtnEnabled3.BackColor == Color.Lime)
                {
                    I32Count++;
                }
                if (this.BtnEnabled4.BackColor == Color.Lime)
                {
                    I32Count++;
                }

                if (I32Count < 2)
                { 
                    CMsgBox.Warning("At least 2 Cameras need to be activated to perform calibration.");
                    return;
                }

             
                    this.m_OHead1.InitCalibration(); //static graphic clear
               
                    this.m_OHead2.InitCalibration();
               
                    this.m_OHead3.InitCalibration();
             
                    this.m_OHead4.InitCalibration();
                


                CTool.It.OAlignmentTool.StartCalibration
                (
                    Convert.ToDouble(this.NudXYSection.Value),
                    Convert.ToInt32(this.NudXYPoint.Value),
                    Convert.ToDouble(this.NudTSection.Value),
                    Convert.ToInt32(this.NudTPoint.Value)
                );

                this.BtnCalibrationStart.BackColor = SystemColors.Control;
                this.BtnCalibrationStop.BackColor = Color.SteelBlue;
                this.BtnCalibrationStart.Enabled = false;
                this.BtnCalibrationStop.Enabled = true;
                this.BtnEnabled1.Enabled = false;
                this.BtnEnabled2.Enabled = false;
                this.BtnEnabled3.Enabled = false;
                this.BtnEnabled4.Enabled = false;
                this.BtnStop1.BackColor = SystemColors.Control;
                this.BtnStop2.BackColor = SystemColors.Control;
                this.BtnStop1.Enabled = false;
                this.BtnStop2.Enabled = false;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnCalibrationStop_Click(object sender, EventArgs e)
        {
            try
            {
                CTool.It.OAlignmentTool.StopCalibration();

                this.BtnCalibrationStart.BackColor = Color.SteelBlue;
                this.BtnCalibrationStop.BackColor = SystemColors.Control;
                this.BtnCalibrationStart.Enabled = true;
                this.BtnCalibrationStop.Enabled = false;
                this.BtnEnabled1.Enabled = true;
                this.BtnEnabled2.Enabled = true;
                this.BtnEnabled3.Enabled = true;
                this.BtnEnabled4.Enabled = true;
                this.BtnStop1.BackColor = Color.SteelBlue;
                this.BtnStop2.BackColor = Color.SteelBlue;
                this.BtnStop1.Enabled = true;
                this.BtnStop2.Enabled = true;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_I32HeadNo += 1;

                Control OControl = (Control)sender;
                OControl.Text = this.m_I32HeadNo.ToString();
                OControl.BackColor = SystemColors.Control;
                OControl.Enabled = false;

                if (this.m_I32HeadNo == 4)
                {
                    this.BtnApplyViewSetup.BackColor = Color.SteelBlue;
                    this.BtnApplyViewSetup.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnResetViewSetup_Click(object sender, EventArgs e)
        {
            try
            {
                frmPassword OWindow = new frmPassword();
                if (OWindow.ShowDialog() == DialogResult.OK)
                {
                    this.m_I32HeadNo = 0;

                    this.BtnView1.Text = "Press";
                    this.BtnView2.Text = "Press";
                    this.BtnView3.Text = "Press";
                    this.BtnView4.Text = "Press";
                    this.BtnView1.BackColor = Color.SteelBlue;
                    this.BtnView2.BackColor = Color.SteelBlue;
                    this.BtnView3.BackColor = Color.SteelBlue;
                    this.BtnView4.BackColor = Color.SteelBlue;
                    this.BtnView1.Enabled = true;
                    this.BtnView2.Enabled = true;
                    this.BtnView3.Enabled = true;
                    this.BtnView4.Enabled = true;

                    this.BtnApplyViewSetup.BackColor = SystemColors.Control;
                    this.BtnCancelViewSetup.BackColor = Color.SteelBlue;
                    this.BtnApplyViewSetup.Enabled = false;
                    this.BtnCancelViewSetup.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnApplyViewSetup_Click(object sender, EventArgs e)
        {
            try
            {
                CEnvironment.It.I32HeadOfView1 = Convert.ToInt32(this.BtnView1.Text);
                CEnvironment.It.I32HeadOfView2 = Convert.ToInt32(this.BtnView2.Text);
                CEnvironment.It.I32HeadOfView3 = Convert.ToInt32(this.BtnView3.Text);
                CEnvironment.It.I32HeadOfView4 = Convert.ToInt32(this.BtnView4.Text);
                CEnvironment.It.Commit();

                this.SetupView();

                this.BtnApplyViewSetup.BackColor = SystemColors.Control;
                this.BtnCancelViewSetup.BackColor = SystemColors.Control;
                this.BtnApplyViewSetup.Enabled = false;
                this.BtnCancelViewSetup.Enabled = false;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnCancelViewSetup_Click(object sender, EventArgs e)
        {
            try
            {
                this.BtnView1.Text = CEnvironment.It.I32HeadOfView1.ToString();
                this.BtnView2.Text = CEnvironment.It.I32HeadOfView2.ToString();
                this.BtnView3.Text = CEnvironment.It.I32HeadOfView3.ToString();
                this.BtnView4.Text = CEnvironment.It.I32HeadOfView4.ToString();
                this.BtnView1.BackColor = SystemColors.Control;
                this.BtnView2.BackColor = SystemColors.Control;
                this.BtnView3.BackColor = SystemColors.Control;
                this.BtnView4.BackColor = SystemColors.Control;
                this.BtnView1.Enabled = false;
                this.BtnView2.Enabled = false;
                this.BtnView3.Enabled = false;
                this.BtnView4.Enabled = false;

                this.BtnApplyViewSetup.BackColor = SystemColors.Control;
                this.BtnCancelViewSetup.BackColor = SystemColors.Control;
                this.BtnApplyViewSetup.Enabled = false;
                this.BtnCancelViewSetup.Enabled = false;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnApplyCam_Click(object sender, EventArgs e)
        {
            try
            {
                EVIEW EView = (EVIEW)Enum.Parse(typeof(EVIEW), (string)((Control)sender).Tag);

                int I32Gain = 0;
                int I32ExpoTime = 0;
                switch (EView)
                {
                    case EVIEW.HEAD1:
                        I32Gain = Convert.ToInt32(this.NudCamHead1Gain.Value);
                        I32ExpoTime = Convert.ToInt32(this.NudCamHead1ExpoTime.Value);

                        CAcquisitionManager.It.OHead1.I32Gain = I32Gain;
                        CAcquisitionManager.It.OHead1.I32ExposureTime = I32ExpoTime;

                        CEnvironment.It.I32CamHead1Gain = I32Gain;
                        CEnvironment.It.I32CamHead1ExpoTime = I32ExpoTime;
                        CEnvironment.It.Commit();
                        break;

                    case EVIEW.HEAD2:
                        I32Gain = Convert.ToInt32(this.NudCamHead2Gain.Value);
                        I32ExpoTime = Convert.ToInt32(this.NudCamHead2ExpoTime.Value);

                        CAcquisitionManager.It.OHead2.I32Gain = I32Gain;
                        CAcquisitionManager.It.OHead2.I32ExposureTime = I32ExpoTime;

                        CEnvironment.It.I32CamHead2Gain = I32Gain;
                        CEnvironment.It.I32CamHead2ExpoTime = I32ExpoTime;
                        CEnvironment.It.Commit();
                        break;

                    case EVIEW.HEAD3:
                        I32Gain = Convert.ToInt32(this.NudCamHead3Gain.Value);
                        I32ExpoTime = Convert.ToInt32(this.NudCamHead3ExpoTime.Value);

                        CAcquisitionManager.It.OHead3.I32Gain = I32Gain;
                        CAcquisitionManager.It.OHead3.I32ExposureTime = I32ExpoTime;

                        CEnvironment.It.I32CamHead3Gain = I32Gain;
                        CEnvironment.It.I32CamHead3ExpoTime = I32ExpoTime;
                        CEnvironment.It.Commit();
                        break;

                    case EVIEW.HEAD4:
                        I32Gain = Convert.ToInt32(this.NudCamHead4Gain.Value);
                        I32ExpoTime = Convert.ToInt32(this.NudCamHead4ExpoTime.Value);

                        CAcquisitionManager.It.OHead4.I32Gain = I32Gain;
                        CAcquisitionManager.It.OHead4.I32ExposureTime = I32ExpoTime;

                        CEnvironment.It.I32CamHead4Gain = I32Gain;
                        CEnvironment.It.I32CamHead4ExpoTime = I32ExpoTime;
                        CEnvironment.It.Commit();
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                CAcquisitionManager.It.OHead1.Start();
                CAcquisitionManager.It.OHead2.Start();
                CAcquisitionManager.It.OHead3.Start();
                CAcquisitionManager.It.OHead4.Start();

                this.BtnCalibrationStart.BackColor = Color.SteelBlue;
                this.BtnCalibrationStop.BackColor = SystemColors.Control;
                this.BtnCalibrationStart.Enabled = true;
                this.BtnCalibrationStop.Enabled = false;

                this.BtnView1.Text = CEnvironment.It.I32HeadOfView1.ToString();
                this.BtnView2.Text = CEnvironment.It.I32HeadOfView2.ToString();
                this.BtnView3.Text = CEnvironment.It.I32HeadOfView3.ToString();
                this.BtnView4.Text = CEnvironment.It.I32HeadOfView4.ToString();
                this.BtnView1.BackColor = SystemColors.Control;
                this.BtnView2.BackColor = SystemColors.Control;
                this.BtnView3.BackColor = SystemColors.Control;
                this.BtnView4.BackColor = SystemColors.Control;
                this.BtnResetViewSetup.BackColor = SystemColors.Control;
                this.BtnApplyViewSetup.BackColor = SystemColors.Control;
                this.BtnView1.Enabled = false;
                this.BtnView2.Enabled = false;
                this.BtnView3.Enabled = false;
                this.BtnView4.Enabled = false;
                this.BtnResetViewSetup.Enabled = false;
                this.BtnApplyViewSetup.Enabled = false;

                this.BtnStart1.BackColor = SystemColors.Control;
                this.BtnStart2.BackColor = SystemColors.Control;
                this.BtnStop1.BackColor = Color.SteelBlue;
                this.BtnStop2.BackColor = Color.SteelBlue;
                this.BtnStart1.Enabled = false;
                this.BtnStart2.Enabled = false;
                this.BtnStop1.Enabled = true;
                this.BtnStop2.Enabled = true;

                base.OnScreenFixed(true);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                CAcquisitionManager.It.OHead1.Stop();
                CAcquisitionManager.It.OHead2.Stop();
                CAcquisitionManager.It.OHead3.Stop();
                CAcquisitionManager.It.OHead4.Stop();

                this.BtnCalibrationStart.BackColor = SystemColors.Control;
                this.BtnCalibrationStop.BackColor = SystemColors.Control;
                this.BtnCalibrationStart.Enabled = false;
                this.BtnCalibrationStop.Enabled = false;

                this.BtnResetViewSetup.BackColor = Color.SteelBlue;
                this.BtnResetViewSetup.Enabled = true;

                this.BtnStart1.BackColor = Color.SteelBlue;
                this.BtnStart2.BackColor = Color.SteelBlue;
                this.BtnStop1.BackColor = SystemColors.Control;
                this.BtnStop2.BackColor = SystemColors.Control;
                this.BtnStart1.Enabled = true;
                this.BtnStart2.Enabled = true;
                this.BtnStop1.Enabled = false;
                this.BtnStop2.Enabled = false;

                base.OnScreenFixed(false);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
        

        #region ETC EVENT
        private void CdpImage_Click(object sender, EventArgs e)
        {
            try
            {
                CogDisplay OControl = ((CogDisplay)sender);

                Point OPoint = new Point(MousePosition.X, MousePosition.Y);
                OPoint = OControl.PointToClient(OPoint);

                switch ((string)OControl.Tag)
                {
                    case "VIEW1":
                        this.m_OView1.EstimateLength(OPoint);
                        break;

                    case "VIEW2":
                        this.m_OView2.EstimateLength(OPoint);
                        break;

                    case "VIEW3":
                        this.m_OView3.EstimateLength(OPoint);
                        break;

                    case "VIEW4":
                        this.m_OView4.EstimateLength(OPoint);
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void OHead1_OImageExported(CImageInfo OImageInfo)
        {
            try
            {
                this.m_OHead1.OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OHead2_OImageExported(CImageInfo OImageInfo)
        {
            try
            {
                this.m_OHead2.OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OHead3_OImageExported(CImageInfo OImageInfo)
        {
            try
            {
                this.m_OHead3.OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OHead4_OImageExported(CImageInfo OImageInfo)
        {
            try
            {
                this.m_OHead4.OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        //1213
        private void OAlignmentTool_OFinishCalibrationMovement(CCalibrationResult OResult)
        {
            try
            {
                if (OResult.OHead1 != null)
                {
                    this.m_OHead1.DrawCalibrationResult(OResult.OHead1);
                }
                if (OResult.OHead2 != null)
                {
                    this.m_OHead2.DrawCalibrationResult(OResult.OHead2);
                }
                if (OResult.OHead3 != null)
                {
                    this.m_OHead3.DrawCalibrationResult(OResult.OHead3);
                }
                if (OResult.OHead4 != null)
                {
                    this.m_OHead4.DrawCalibrationResult(OResult.OHead4);
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void OAlignmentTool_OFinishCalibration(bool BIsSuccess)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CAlignmentTool.FinishCalibrationHandler(this.OAlignmentTool_OFinishCalibration), BIsSuccess);
                }
                else
                {
                    if (BIsSuccess == false)
                    {
                        CMsgBox.Warning("Fail Calibration");
                    }

                    this.BtnCalibrationStart.BackColor = Color.SteelBlue;
                    this.BtnCalibrationStop.BackColor = SystemColors.Control;
                    this.BtnCalibrationStart.Enabled = true;
                    this.BtnCalibrationStop.Enabled = false;
                    this.BtnEnabled1.Enabled = true;
                    this.BtnEnabled2.Enabled = true;
                    this.BtnEnabled3.Enabled = true;
                    this.BtnEnabled4.Enabled = true;
                    this.BtnStop1.BackColor = Color.SteelBlue;
                    this.BtnStop2.BackColor = Color.SteelBlue;
                    this.BtnStop1.Enabled = true;
                    this.BtnStop2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void OLoggingTool_OSendMessage(string StrMsg)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CLoggingTool.SendMessageHandler(this.OLoggingTool_OSendMessage), StrMsg);
                }
                else
                {
                    this.LtbMessage.Items.Insert(0, StrMsg);

                    if (this.LtbMessage.Items.Count >= 300)
                    {
                        this.LtbMessage.Items.RemoveAt(this.LtbMessage.Items.Count - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        public override void Add()
        {
            try
            {
                //CALL BACK
                CTool.It.OAlignmentTool.OHead1.ODisplayer.OImageExported = this.OHead1_OImageExported;
                CTool.It.OAlignmentTool.OHead2.ODisplayer.OImageExported = this.OHead2_OImageExported;
                CTool.It.OAlignmentTool.OHead3.ODisplayer.OImageExported = this.OHead3_OImageExported;
                CTool.It.OAlignmentTool.OHead4.ODisplayer.OImageExported = this.OHead4_OImageExported;
                CTool.It.OAlignmentTool.OFinishCalibrationMovement = this.OAlignmentTool_OFinishCalibrationMovement;
                CTool.It.OAlignmentTool.OFinishCalibration = this.OAlignmentTool_OFinishCalibration;
                CLoggingTool.It.OSendMessage = this.OLoggingTool_OSendMessage;

                //SETUP VIEW
                this.SetupView();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void Remove()
        {
            try
            {
                CTool.It.OAlignmentTool.OHead1.ODisplayer.OImageExported = null;
                CTool.It.OAlignmentTool.OHead2.ODisplayer.OImageExported = null;
                CTool.It.OAlignmentTool.OHead3.ODisplayer.OImageExported = null;
                CTool.It.OAlignmentTool.OHead4.ODisplayer.OImageExported = null;
                CTool.It.OAlignmentTool.OFinishCalibrationMovement = null;
                CTool.It.OAlignmentTool.OFinishCalibration = null;
                CLoggingTool.It.OSendMessage = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void SetupView()
        {
            try
            {
                int I32HeadOfView1 = CEnvironment.It.I32HeadOfView1;
                int I32HeadOfView2 = CEnvironment.It.I32HeadOfView2;
                int I32HeadOfView3 = CEnvironment.It.I32HeadOfView3;
                int I32HeadOfView4 = CEnvironment.It.I32HeadOfView4;

                this.LblTitleView1.Text = "Head #" + I32HeadOfView1.ToString();
                this.LblTitleView2.Text = "Head #" + I32HeadOfView2.ToString();
                this.LblTitleView3.Text = "Head #" + I32HeadOfView3.ToString();
                this.LblTitleView4.Text = "Head #" + I32HeadOfView4.ToString();

                this.BtnEnabled1.Tag = I32HeadOfView1.ToString();
                this.BtnEnabled2.Tag = I32HeadOfView2.ToString();
                this.BtnEnabled3.Tag = I32HeadOfView3.ToString();
                this.BtnEnabled4.Tag = I32HeadOfView4.ToString();
                string StrEnabled1 = CEnvironment.It.StrEnabled1;
                string StrEnabled2 = CEnvironment.It.StrEnabled2;
                string StrEnabled3 = CEnvironment.It.StrEnabled3;
                string StrEnabled4 = CEnvironment.It.StrEnabled4;

                this.BtnEnabled1.BackColor = Color.DarkRed;
                this.BtnEnabled2.BackColor = Color.DarkRed;
                this.BtnEnabled3.BackColor = Color.DarkRed;
                this.BtnEnabled4.BackColor = Color.DarkRed;

                if (StrEnabled1 == "true")
                {
                    if (this.BtnEnabled1.Tag.ToString() == "1")
                    {
                        this.BtnEnabled1.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled2.Tag.ToString() == "1")
                    {
                        this.BtnEnabled2.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled3.Tag.ToString() == "1")
                    {
                        this.BtnEnabled3.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled4.Tag.ToString() == "1")
                    {
                        this.BtnEnabled4.BackColor = Color.Lime;
                    }
                }
                if (StrEnabled2 == "true")
                {
                    if (this.BtnEnabled1.Tag.ToString() == "2")
                    {
                        this.BtnEnabled1.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled2.Tag.ToString() == "2")
                    {
                        this.BtnEnabled2.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled3.Tag.ToString() == "2")
                    {
                        this.BtnEnabled3.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled4.Tag.ToString() == "2")
                    {
                        this.BtnEnabled4.BackColor = Color.Lime;
                    }
                }
                if (StrEnabled3 == "true")
                {
                    if (this.BtnEnabled1.Tag.ToString() == "3")
                    {
                        this.BtnEnabled1.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled2.Tag.ToString() == "3")
                    {
                        this.BtnEnabled2.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled3.Tag.ToString() == "3")
                    {
                        this.BtnEnabled3.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled4.Tag.ToString() == "3")
                    {
                        this.BtnEnabled4.BackColor = Color.Lime;
                    }
                }
                if (StrEnabled4 == "true")
                {
                    if (this.BtnEnabled1.Tag.ToString() == "4")
                    {
                        this.BtnEnabled1.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled2.Tag.ToString() == "4")
                    {
                        this.BtnEnabled2.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled3.Tag.ToString() == "4")
                    {
                        this.BtnEnabled3.BackColor = Color.Lime;
                    }
                    else if (this.BtnEnabled4.Tag.ToString() == "4")
                    {
                        this.BtnEnabled4.BackColor = Color.Lime;
                    }
                }
                int I32ViewOfHead1 = 0;
                int I32ViewOfHead2 = 0;
                int I32ViewOfHead3 = 0;
                int I32ViewOfHead4 = 0;
                if (I32HeadOfView1 == 1) I32ViewOfHead1 = 1;
                else if (I32HeadOfView1 == 2) I32ViewOfHead2 = 1;
                else if (I32HeadOfView1 == 3) I32ViewOfHead3 = 1;
                else if (I32HeadOfView1 == 4) I32ViewOfHead4 = 1;
                if (I32HeadOfView2 == 1) I32ViewOfHead1 = 2;
                else if (I32HeadOfView2 == 2) I32ViewOfHead2 = 2;
                else if (I32HeadOfView2 == 3) I32ViewOfHead3 = 2;
                else if (I32HeadOfView2 == 4) I32ViewOfHead4 = 2;
                if (I32HeadOfView3 == 1) I32ViewOfHead1 = 3;
                else if (I32HeadOfView3 == 2) I32ViewOfHead2 = 3;
                else if (I32HeadOfView3 == 3) I32ViewOfHead3 = 3;
                else if (I32HeadOfView3 == 4) I32ViewOfHead4 = 3;
                if (I32HeadOfView4 == 1) I32ViewOfHead1 = 4;
                else if (I32HeadOfView4 == 2) I32ViewOfHead2 = 4;
                else if (I32HeadOfView4 == 3) I32ViewOfHead3 = 4;
                else if (I32HeadOfView4 == 4) I32ViewOfHead4 = 4;

                this.m_OHead1 = this.GetViewOfHead(I32ViewOfHead1);
                this.m_OHead2 = this.GetViewOfHead(I32ViewOfHead2);
                this.m_OHead3 = this.GetViewOfHead(I32ViewOfHead3);
                this.m_OHead4 = this.GetViewOfHead(I32ViewOfHead4);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private CSetupDisplayer GetViewOfHead(int I32ViewOfHead)
        {
            CSetupDisplayer OResult = null;

            try
            {
                switch (I32ViewOfHead)
                {
                    case 1:
                        OResult = this.m_OView1;
                        OResult.Init();
                        OResult.F64CenterX = CAcquisitionManager.It.OHead1.I32Width / 2D;
                        OResult.F64CenterY = CAcquisitionManager.It.OHead1.I32Height / 2D;
                        break;

                    case 2:
                        OResult = this.m_OView2;
                        OResult.Init();
                        OResult.F64CenterX = CAcquisitionManager.It.OHead2.I32Width / 2D;
                        OResult.F64CenterY = CAcquisitionManager.It.OHead2.I32Height / 2D;
                        break;

                    case 3:
                        OResult = this.m_OView3;
                        OResult.Init();
                        OResult.F64CenterX = CAcquisitionManager.It.OHead3.I32Width / 2D;
                        OResult.F64CenterY = CAcquisitionManager.It.OHead3.I32Height / 2D;
                        break;

                    case 4:
                        OResult = this.m_OView4;
                        OResult.Init();
                        OResult.F64CenterX = CAcquisitionManager.It.OHead4.I32Width / 2D;
                        OResult.F64CenterY = CAcquisitionManager.It.OHead4.I32Height / 2D;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }
        #endregion

        //1212
        private void BtnEnabled_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((string)((Control)sender).Tag)
                {
                    case "1":
                        if (((Control)sender).BackColor == Color.Lime)
                        {
                            CEnvironment.It.StrEnabled1 = "false";
                        }
                        else
                        {
                            CEnvironment.It.StrEnabled1 = "true";
                        }
                        break;

                    case "2":
                        if (((Control)sender).BackColor == Color.Lime)
                        {
                            CEnvironment.It.StrEnabled2 = "false";
                        }
                        else
                        {
                            CEnvironment.It.StrEnabled2 = "true";
                        }
                        break;

                    case "3":
                        if (((Control)sender).BackColor == Color.Lime)
                        {
                            CEnvironment.It.StrEnabled3 = "false";
                        }
                        else
                        {
                            CEnvironment.It.StrEnabled3 = "true";
                        }
                        break;

                    case "4":
                        if (((Control)sender).BackColor == Color.Lime)
                        {
                            CEnvironment.It.StrEnabled4 = "false";
                        }
                        else
                        {
                            CEnvironment.It.StrEnabled4 = "true";
                        }
                        break;
                }
                CEnvironment.It.Commit();
                this.SetupView();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
    }
}
