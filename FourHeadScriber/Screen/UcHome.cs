using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Jhjo.Common;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using System.Threading;

namespace FourHeadScriber
{
    public partial class UcHome : UcScreen
    {
        #region CONST
        private const string TXT_MANUAL_ALIGN_SHARP = "M - Align#";
        private const string TXT_MANUAL_ALIGN_X = "M - X";
        #endregion


        #region VARIABLE
        private CHomeDisplayer m_OView1 = null;
        private CHomeDisplayer m_OView2 = null;
        private CHomeDisplayer m_OView3 = null;
        private CHomeDisplayer m_OView4 = null;

        private CHomeDisplayer m_OHead1 = null;
        private CHomeDisplayer m_OHead2 = null;
        private CHomeDisplayer m_OHead3 = null;
        private CHomeDisplayer m_OHead4 = null;

        private double m_F64Head1Wheel = 0;
        private double m_F64Head2Wheel = 0;
        private double m_F64Head3Wheel = 0;
        private double m_F64Head4Wheel = 0;

        private bool m_BPreventEvent = false;

        private bool m_BManual = false;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcHome()
        {
            InitializeComponent();

            try
            {
                this.m_BPreventEvent = true;

                this.m_OView1 = new CHomeDisplayer(this.CdpView1, this.CtbView1, this.CsbView1);
                this.m_OView2 = new CHomeDisplayer(this.CdpView2, this.CtbView2, this.CsbView2);
                this.m_OView3 = new CHomeDisplayer(this.CdpView3, this.CtbView3, this.CsbView3);
                this.m_OView4 = new CHomeDisplayer(this.CdpView4, this.CtbView4, this.CsbView4);

                this.m_OHead1 = m_OView1;
                this.m_OHead2 = m_OView2;
                this.m_OHead3 = m_OView3;
                this.m_OHead4 = m_OView4;
                this.m_OHead1.EHead = EVIEW.HEAD1;
                this.m_OHead2.EHead = EVIEW.HEAD2;
                this.m_OHead3.EHead = EVIEW.HEAD3;
                this.m_OHead4.EHead = EVIEW.HEAD4;

                this.BtnStart1.BackColor = Color.SteelBlue;
                this.BtnStart2.BackColor = Color.SteelBlue;
                this.BtnStop1.BackColor = SystemColors.Control;
                this.BtnStop2.BackColor = SystemColors.Control;
                
                this.m_BManual = false;
                this.BtnManual1.BackColor = SystemColors.Control;
                this.BtnManual2.BackColor = SystemColors.Control;

                this.BtnStart1.Enabled = true;
                this.BtnStart2.Enabled = true;
                this.BtnStop1.Enabled = false;
                this.BtnStop2.Enabled = false;
                this.BtnManual1.Enabled = false;
                this.BtnManual2.Enabled = false;
                this.ChkIndividual.Checked = true;
                this.ChkIndividual.BackColor = Color.Lime;
                this.ChkIndividual.Text = "SafeLock Off";

                //20170413
                //in normal cases, it has to fix Y.
                this.ChkManualYUnFix.Enabled = true;
                this.ChkManualYUnFix.Checked = true;
                this.ChkManualYUnFix.Text = "No Fixing Y";
                this.ChkManualYUnFix.BackColor = Color.Firebrick;
                this.m_OHead1.BOverride_NoFix = true;
                this.m_OHead2.BOverride_NoFix = true;
                this.m_OHead3.BOverride_NoFix = true;
                this.m_OHead4.BOverride_NoFix = true;

                if (CEnvironment.It.Direction == 1)
                {
                    this.LblTitle1.Text = "Home : Unreflected (본칭)";
                    this.LblTitle2.Text = "Home : Unreflected (본칭)";
                }
                else
                {
                    this.LblTitle1.Text = "Home : Reflected (대칭)";
                    this.LblTitle2.Text = "Home : Reflected (대칭)";
                }
               // CEnvironment.It.StrEnableMark = "false";
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
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


        private void BtnStart_Click(object sender, EventArgs e)
        {
            //20170412
            if(e == EventArgs.Empty) return;

            try
            {
                this.m_BPreventEvent = true;

                int I32ID = CMotionController.It.OPLC.GetMotionRecipeID();
                if ((CTool.It.ORecipe == null) || (I32ID != CTool.It.ORecipe.I32ID))
                {
                    foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                    {
                        if (_Item.I32ID != I32ID) continue;

                        CTool.It.ORecipe = new CRecipe(_Item);
                        CMotionController.It.OPLC.I32VisionRecipeID = _Item.I32ID;
                        break;
                    }
                }

                CAcquisitionManager.It.OHead1.Start();
                CAcquisitionManager.It.OHead2.Start();
                CAcquisitionManager.It.OHead3.Start();
                CAcquisitionManager.It.OHead4.Start();
                CTool.It.OAlignmentTool.BeginInspection();

                this.BtnStart1.BackColor = SystemColors.Control;
                this.BtnStart2.BackColor = SystemColors.Control;
                this.BtnStop1.BackColor = Color.SteelBlue;
                this.BtnStop2.BackColor = Color.SteelBlue;
                this.BtnManual1.BackColor = Color.SteelBlue;
                this.BtnManual2.BackColor = Color.SteelBlue;
                this.BtnStart1.Enabled = false;
                this.BtnStart2.Enabled = false;
                this.BtnStop1.Enabled = true;
                this.BtnStop2.Enabled = true;
                this.BtnManual1.Enabled = true;
                this.BtnManual2.Enabled = true;

                base.OnScreenFixed(true);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }


        private void BtnStop_Click(object sender, EventArgs e)
        {
            //20170412
            if (e == EventArgs.Empty) return;

            try
            {
                this.m_BPreventEvent = true;

                CTool.It.OAlignmentTool.EndInspection();
              
                CMotionController.It.OPLC.BIsManualAlignment = false;

                CAcquisitionManager.It.OHead1.Stop();
                CAcquisitionManager.It.OHead2.Stop();
                CAcquisitionManager.It.OHead3.Stop();
                CAcquisitionManager.It.OHead4.Stop();

                //20170412
                if (this.m_BManual == true)
                {
                    CMotionController.It.OPLC.BIsManualAlignment = false;
                    this.m_OHead1.VisibleAlignmentManualPoint(false);
                    this.m_OHead2.VisibleAlignmentManualPoint(false);
                    this.m_OHead3.VisibleAlignmentManualPoint(false);
                    this.m_OHead4.VisibleAlignmentManualPoint(false);
                }

                this.BtnStart1.BackColor = Color.SteelBlue;
                this.BtnStart2.BackColor = Color.SteelBlue;
                this.BtnStop1.BackColor = SystemColors.Control;
                this.BtnStop2.BackColor = SystemColors.Control;
                this.m_BManual = false;
               
                this.BtnManual1.BackColor = SystemColors.Control;
                this.BtnManual2.BackColor = SystemColors.Control;
                this.BtnStart1.Enabled = true;
                this.BtnStart2.Enabled = true;
                this.BtnStop1.Enabled = false;
                this.BtnStop2.Enabled = false;
                this.BtnManual1.Enabled = false;
                this.BtnManual2.Enabled = false;               

                base.OnScreenFixed(false);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }


        private void BtnWheel_Click(object sender, EventArgs e)
        {
            //20170412
            if (e == EventArgs.Empty) return;

            try
            {
                if (this.BtnWheel1.Text == "Wheel On")
                {
                    this.BtnWheel1.Text = "Wheel Off";
                    this.BtnWheel2.Text = "Wheel Off";

                    this.m_OHead1.ShowWheelLine();
                    this.m_OHead2.ShowWheelLine();
                    this.m_OHead3.ShowWheelLine();
                    this.m_OHead4.ShowWheelLine();
                }
                else
                {
                    this.BtnWheel1.Text = "Wheel On";
                    this.BtnWheel2.Text = "Wheel On";

                    this.m_OHead1.HideWheelLine();
                    this.m_OHead2.HideWheelLine();
                    this.m_OHead3.HideWheelLine();
                    this.m_OHead4.HideWheelLine();
                }
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


        private void BtnManual_Click(object sender, EventArgs e)
        {
            //20170412
            if (e == EventArgs.Empty) return;
            if (this.m_BPreventEvent == true) return;

            try
            {
                this.m_BPreventEvent = true;

                if (this.m_BManual == false)
                {
                    this.m_BManual = true;
                    CMotionController.It.OPLC.BIsManualAlignment = true;
                    //20170227 3rd Revision: we need to average out the Y
                    // fix the Y point(with a graphical representation by drawing a horizontal line for each display)
                    // and only allow user to modify the x value of an manual point if the mark is not found.

                    this.ChkIndividual.Checked = true; //0228
                    //201700415
                   if(CEnvironment.It.StrEnabled1 == "true") this.m_OHead1.VisibleAlignmentManualPoint(true);
                   if (CEnvironment.It.StrEnabled2 == "true") this.m_OHead2.VisibleAlignmentManualPoint(true);
                   if (CEnvironment.It.StrEnabled3 == "true") this.m_OHead3.VisibleAlignmentManualPoint(true);
                   if (CEnvironment.It.StrEnabled4 == "true") this.m_OHead4.VisibleAlignmentManualPoint(true);

                    //calculate the average Y from the found marks
                    int I32MarkInspectedCount = 0;
                    double F64AvgY = 0;
                    if (this.m_OHead1.BFound == true)
                    {
                        F64AvgY += this.m_OHead1.F64Y;
                        I32MarkInspectedCount++;
                    }
                    if (this.m_OHead2.BFound == true)
                    {
                        F64AvgY += this.m_OHead2.F64Y;
                        I32MarkInspectedCount++;
                    }
                    if (this.m_OHead3.BFound == true)
                    {
                        F64AvgY += this.m_OHead3.F64Y;
                        I32MarkInspectedCount++;
                    }
                    if (this.m_OHead4.BFound == true)
                    {
                        F64AvgY += this.m_OHead4.F64Y;
                        I32MarkInspectedCount++;
                    }
                    if (I32MarkInspectedCount != 0)
                    {
                        double tmp = F64AvgY / (double)I32MarkInspectedCount;
                        this.m_OHead1.FixYLine(tmp);
                        this.m_OHead2.FixYLine(tmp);
                        this.m_OHead3.FixYLine(tmp);
                        this.m_OHead4.FixYLine(tmp);
                    }
                    else //if all marks are not available...
                    {
                        this.m_OHead1.NoMarksAtAll();
                        this.m_OHead2.NoMarksAtAll();
                        this.m_OHead3.NoMarksAtAll();
                        this.m_OHead4.NoMarksAtAll();
                    }

                    this.BtnManual1.BackColor = Color.ForestGreen;
                    this.BtnManual2.BackColor = Color.ForestGreen;
                }
                else if (this.m_BManual == true)
                {
                    this.m_BManual = false;
                    CMotionController.It.OPLC.BIsManualAlignment = false;
                    this.m_OHead1.VisibleAlignmentManualPoint(false);
                    this.m_OHead2.VisibleAlignmentManualPoint(false);
                    this.m_OHead3.VisibleAlignmentManualPoint(false);
                    this.m_OHead4.VisibleAlignmentManualPoint(false);

                    this.BtnManual1.BackColor = Color.SteelBlue;
                    this.BtnManual2.BackColor = Color.SteelBlue;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }



        //20170227
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //}


        //private void ChkManualAlignKind_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.m_BPreventEvent == true) return;

        //    try
        //    {
        //        this.m_BPreventEvent = true;

        //        CheckBox OControl = (CheckBox)sender;
        //        ESCREEN EScreen = (ESCREEN)Enum.Parse(typeof(ESCREEN), (string)OControl.Tag);

        //        if (OControl.Checked == true)
        //        {
        //            //if (EScreen == ESCREEN.SCREEN1) this.ChkManualAlignKind2.Checked = true;
        //            //else if (EScreen == ESCREEN.SCREEN2) this.ChkManualAlignKind1.Checked = true;
        //            //this.ChkManualAlignKind1.Text = TXT_MANUAL_ALIGN_X;
        //            //this.ChkManualAlignKind2.Text = TXT_MANUAL_ALIGN_X;
        //            CTool.It.OAlignmentTool.BAlignSharp = false;
        //        }
        //        else if (OControl.Checked == false)
        //        {
        //            //if (EScreen == ESCREEN.SCREEN1) this.ChkManualAlignKind2.Checked = false;
        //            //else if (EScreen == ESCREEN.SCREEN2) this.ChkManualAlignKind1.Checked = false;
        //            //this.ChkManualAlignKind1.Text = TXT_MANUAL_ALIGN_SHARP;
        //            //this.ChkManualAlignKind2.Text = TXT_MANUAL_ALIGN_SHARP;
        //            CTool.It.OAlignmentTool.BAlignSharp = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CError.Show(ex);
        //    }
        //    finally
        //    {
        //        this.m_BPreventEvent = false;
        //    }
        //}


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


        private void OAlignmentTool_OFinishAlignment(CAlignmentResult OResult)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CAlignmentTool.FinishAlignmentHandler(this.OAlignmentTool_OFinishAlignment), OResult);
                }
                else
                {
                    this.m_OHead1.DrawMarkResult(OResult.OHead1);
                    this.m_OHead2.DrawMarkResult(OResult.OHead2);
                    this.m_OHead3.DrawMarkResult(OResult.OHead3);
                    this.m_OHead4.DrawMarkResult(OResult.OHead4);

                    if (OResult.EAngle == ESTAGE_ANGLE.ZERO)
                    {
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD1, OResult.EResult, this.LblZeroAlignmentHead1XCurrent, OResult.F64Head1X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD2, OResult.EResult, this.LblZeroAlignmentHead2XCurrent, OResult.F64Head2X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD3, OResult.EResult, this.LblZeroAlignmentHead3XCurrent, OResult.F64Head3X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD4, OResult.EResult, this.LblZeroAlignmentHead4XCurrent, OResult.F64Head4X);
                        this.DrawAlignmentYText(OResult.EResult, this.LblZeroAlignmentYCurrent, OResult.F64Y);
                        this.DrawAlignmentTText(OResult.EResult, this.LblZeroAlignmentTCurrent, OResult.F64T);
                    }
                    else if (OResult.EAngle == ESTAGE_ANGLE.NINETY)
                    {
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD1, OResult.EResult, this.LblNinetyAlignmentHead1XCurrent, OResult.F64Head1X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD2, OResult.EResult, this.LblNinetyAlignmentHead2XCurrent, OResult.F64Head2X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD3, OResult.EResult, this.LblNinetyAlignmentHead3XCurrent, OResult.F64Head3X);
                        this.DrawAlignmentHeadText(OResult.EView, EVIEW.HEAD4, OResult.EResult, this.LblNinetyAlignmentHead4XCurrent, OResult.F64Head4X);
                        this.DrawAlignmentYText(OResult.EResult, this.LblNinetyAlignmentYCurrent, OResult.F64Y);
                        this.DrawAlignmentTText(OResult.EResult, this.LblNinetyAlignmentTCurrent, OResult.F64T);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OAlignmentTool_OFinishDirectCut(CDirectCutResult OResult)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    this.m_OHead1.DrawDirectCutGraphic(OResult.OHead1);
                    this.m_OHead2.DrawDirectCutGraphic(OResult.OHead2);
                    this.m_OHead3.DrawDirectCutGraphic(OResult.OHead3);
                    this.m_OHead4.DrawDirectCutGraphic(OResult.OHead4);

                    base.BeginInvoke(new CAlignmentTool.FinishDirectCutHandler(this.OAlignmentTool_OFinishDirectCut), OResult);
                }
                else
                {
                    if (OResult.EAngle == ESTAGE_ANGLE.ZERO)
                    {
                        this.DrawCutText(OResult.EView, EVIEW.HEAD1, OResult.BHead1OK, OResult.F64Head1Length, OResult.OHead1, this.LblZeroDirectCutHead1);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD2, OResult.BHead2OK, OResult.F64Head2Length, OResult.OHead2, this.LblZeroDirectCutHead2);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD3, OResult.BHead3OK, OResult.F64Head3Length, OResult.OHead3, this.LblZeroDirectCutHead3);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD4, OResult.BHead4OK, OResult.F64Head4Length, OResult.OHead4, this.LblZeroDirectCutHead4);
                    }
                    else if (OResult.EAngle == ESTAGE_ANGLE.NINETY)
                    {
                        this.DrawCutText(OResult.EView, EVIEW.HEAD1, OResult.BHead1OK, OResult.F64Head1Length, OResult.OHead1, this.LblNinetyDirectCutHead1);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD2, OResult.BHead2OK, OResult.F64Head2Length, OResult.OHead2, this.LblNinetyDirectCutHead2);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD3, OResult.BHead3OK, OResult.F64Head3Length, OResult.OHead3, this.LblNinetyDirectCutHead3);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD4, OResult.BHead4OK, OResult.F64Head4Length, OResult.OHead4, this.LblNinetyDirectCutHead4);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OAlignmentTool_OFinishCrossCut(CCrossCutResult OResult)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    this.m_OHead1.DrawCrossCutGraphic(OResult.OHead1);
                    this.m_OHead2.DrawCrossCutGraphic(OResult.OHead2);
                    this.m_OHead3.DrawCrossCutGraphic(OResult.OHead3);
                    this.m_OHead4.DrawCrossCutGraphic(OResult.OHead4);

                    base.BeginInvoke(new CAlignmentTool.FinishCrossCutHandler(this.OAlignmentTool_OFinishCrossCut), OResult);
                }
                else
                {
                    if (OResult.EAngle == ESTAGE_ANGLE.ZERO)
                    {
                        this.DrawCutText(OResult.EView, EVIEW.HEAD1, OResult.BHead1OK, OResult.F64Head1Length, OResult.OHead1, this.LblZeroCrossCutHead1);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD2, OResult.BHead2OK, OResult.F64Head2Length, OResult.OHead2, this.LblZeroCrossCutHead2);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD3, OResult.BHead3OK, OResult.F64Head3Length, OResult.OHead3, this.LblZeroCrossCutHead3);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD4, OResult.BHead4OK, OResult.F64Head4Length, OResult.OHead4, this.LblZeroCrossCutHead4);
                    }
                    else if (OResult.EAngle == ESTAGE_ANGLE.NINETY)
                    {
                        this.DrawCutText(OResult.EView, EVIEW.HEAD1, OResult.BHead1OK, OResult.F64Head1Length, OResult.OHead1, this.LblNinetyCrossCutHead1);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD2, OResult.BHead2OK, OResult.F64Head2Length, OResult.OHead2, this.LblNinetyCrossCutHead2);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD3, OResult.BHead3OK, OResult.F64Head3Length, OResult.OHead3, this.LblNinetyCrossCutHead3);
                        this.DrawCutText(OResult.EView, EVIEW.HEAD4, OResult.BHead4OK, OResult.F64Head4Length, OResult.OHead4, this.LblNinetyCrossCutHead4);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OPLC_OManualAlignmentRequested(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CPLC.ManualAlignmentRequestedHandler(this.OPLC_OManualAlignmentRequested), EAngle, EView);
                }
                else
                {
                    this.PerformManualAlignment(EAngle, EView);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OPLC_OWheelPositionChanged(double F64Head1Wheel, double F64Head2Wheel, double F64Head3Wheel, double F64Head4Wheel)
        {
            try
            {
                this.m_OHead1.SetWheel(F64Head1Wheel);
                this.m_OHead2.SetWheel(F64Head2Wheel);
                this.m_OHead3.SetWheel(F64Head3Wheel);
                this.m_OHead4.SetWheel(F64Head4Wheel);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
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
                CError.Throw(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        public override void Add()
        {
            try
            {
                //Call Back
                CTool.It.OAlignmentTool.OHead1.ODisplayer.OImageExported = this.OHead1_OImageExported;
                CTool.It.OAlignmentTool.OHead2.ODisplayer.OImageExported = this.OHead2_OImageExported;
                CTool.It.OAlignmentTool.OHead3.ODisplayer.OImageExported = this.OHead3_OImageExported;
                CTool.It.OAlignmentTool.OHead4.ODisplayer.OImageExported = this.OHead4_OImageExported;
                CTool.It.OAlignmentTool.OFinishAlignment = this.OAlignmentTool_OFinishAlignment;
                CTool.It.OAlignmentTool.OFinishDirectCut = this.OAlignmentTool_OFinishDirectCut;
                CTool.It.OAlignmentTool.OFinishCrossCut = this.OAlignmentTool_OFinishCrossCut;
                CMotionController.It.OPLC.OManualAlignmentRequested = this.OPLC_OManualAlignmentRequested;
                CMotionController.It.OPLC.OWheelPositionChanged = this.OPLC_OWheelPositionChanged;
                CLoggingTool.It.OSendMessage = this.OLoggingTool_OSendMessage;

                //View
                this.SetupView();

                //Wheel
                CMotionController.It.OPLC.GetWheelPosition(out this.m_F64Head1Wheel, out this.m_F64Head2Wheel, out this.m_F64Head3Wheel, out this.m_F64Head4Wheel);
                this.m_OHead1.SetWheel(this.m_F64Head1Wheel);
                this.m_OHead2.SetWheel(this.m_F64Head2Wheel);
                this.m_OHead3.SetWheel(this.m_F64Head3Wheel);
                this.m_OHead4.SetWheel(this.m_F64Head4Wheel);

                //Target
                if (CTool.It.ORecipe != null)
                {
                    this.LblZeroAlignmentHead1XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead1.ToString("0.0000");
                    this.LblZeroAlignmentHead2XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead2.ToString("0.0000");
                    this.LblZeroAlignmentHead3XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead3.ToString("0.0000");
                    this.LblZeroAlignmentHead4XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead4.ToString("0.0000");
                    this.LblZeroAlignmentYTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitY.ToString("0.0000");
                    this.LblZeroAlignmentTTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitT.ToString("0.00000");
                   
                    this.LblNinetyAlignmentHead1XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead1.ToString("0.0000");
                    this.LblNinetyAlignmentHead2XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead2.ToString("0.0000");
                    this.LblNinetyAlignmentHead3XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead3.ToString("0.0000");
                    this.LblNinetyAlignmentHead4XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead4.ToString("0.0000");
                    this.LblNinetyAlignmentYTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitY.ToString("0.0000");
                    this.LblNinetyAlignmentTTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitT.ToString("0.00000");
                }

                this.NudMAXTRY.Value = CTool.It.OAlignmentTool.IntMAXTRY;
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
                CTool.It.OAlignmentTool.OFinishAlignment = null;
                CTool.It.OAlignmentTool.OFinishDirectCut = null;
                CTool.It.OAlignmentTool.OFinishCrossCut = null;
                CMotionController.It.OPLC.OManualAlignmentRequested = null;
                CMotionController.It.OPLC.OWheelPositionChanged = null;
                CLoggingTool.It.OSendMessage = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void NotifyRecipeChanged()
        {
            try
            {
                if (CTool.It.ORecipe != null)
                {
                    this.LblZeroAlignmentHead1XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead1.ToString("0.0000");
                    this.LblZeroAlignmentHead2XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead2.ToString("0.0000");
                    this.LblZeroAlignmentHead3XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead3.ToString("0.0000");
                    this.LblZeroAlignmentHead4XTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitHead4.ToString("0.0000");
                    this.LblZeroAlignmentYTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitY.ToString("0.0000");
                    this.LblZeroAlignmentTTarget.Text = CTool.It.ORecipe.OZero.F64AlignmentLimitT.ToString("0.00000");

                    this.LblNinetyAlignmentHead1XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead1.ToString("0.0000");
                    this.LblNinetyAlignmentHead2XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead2.ToString("0.0000");
                    this.LblNinetyAlignmentHead3XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead3.ToString("0.0000");
                    this.LblNinetyAlignmentHead4XTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitHead4.ToString("0.0000");
                    this.LblNinetyAlignmentYTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitY.ToString("0.0000");
                    this.LblNinetyAlignmentTTarget.Text = CTool.It.ORecipe.ONinety.F64AlignmentLimitT.ToString("0.00000");
                }
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
                //1219
                //if (CEnvironment.It.StrEnableMark == "true")
                //{
                //    this.BtnIgnoreMark.BackColor = Color.Indigo;
                //    this.BtnIgnoreMark.Text = "Mark O";
                //}
                //else
                //{
                //    this.BtnIgnoreMark.BackColor = Color.DarkRed;
                //    this.BtnIgnoreMark.Text = "Mark X";
                //}

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

                //1217
                int I32Count = 0;
                int I32Mod = 0;
                if (StrEnabled1 == "true") I32Count++;
                if (StrEnabled2 == "true") I32Count++;
                if (StrEnabled3 == "true") I32Count++;
                if (StrEnabled4 == "true") I32Count++;
                Math.DivRem(I32Count, 2, out I32Mod);
                if (I32Mod == 1)
                {
                    CTool.It.OAlignmentTool.I32AutoCheckX_AlignS_auto = CSign.ODDCHECK_X_S_AUTO_SIGN; //-1;
                    CTool.It.OAlignmentTool.I32AutoCheckX_AlignX_auto = CSign.ODDCHECK_X_X_AUTO_SIGN;//1;
                    //CTool.It.OAlignmentTool.I32AutoCheckX_AlignS_manual = CSign.ODDCHECK_X_S_MANUAL_SIGN;//-1;
                    //CTool.It.OAlignmentTool.I32AutoCheckX_AlignX_manual = CSign.ODDCHECK_X_X_MANUAL_SIGN;//-1;

                    CTool.It.OAlignmentTool.I32AutoCheckY_AlignS_auto = CSign.ODDCHECK_Y_S_AUTO_SIGN;//1;
                    CTool.It.OAlignmentTool.I32AutoCheckY_AlignX_auto = CSign.ODDCHECK_Y_X_AUTO_SIGN;//-1;
                    //CTool.It.OAlignmentTool.I32AutoCheckY_AlignS_manual = CSign.ODDCHECK_Y_S_MANUAL_SIGN;//1;
                    //CTool.It.OAlignmentTool.I32AutoCheckY_AlignX_manual = CSign.ODDCHECK_Y_X_MANUAL_SIGN;//1;

                    CTool.It.OAlignmentTool.I32AutoCheckT_AlignS_auto = CSign.ODDCHECK_T_S_AUTO_SIGN;//1;
                    CTool.It.OAlignmentTool.I32AutoCheckT_AlignX_auto = CSign.ODDCHECK_T_X_AUTO_SIGN;//-1;
                    //CTool.It.OAlignmentTool.I32AutoCheckT_AlignS_manual = CSign.ODDCHECK_T_S_MANUAL_SIGN;//1;
                    //CTool.It.OAlignmentTool.I32AutoCheckT_AlignX_manual = CSign.ODDCHECK_T_X_MANUAL_SIGN;//-1;
                }
                else
                {
                    CTool.It.OAlignmentTool.I32AutoCheckX_AlignS_auto = CSign.ODDCHECK_X_S_AUTO_SIGN;// *-1; 
                    CTool.It.OAlignmentTool.I32AutoCheckX_AlignX_auto = CSign.ODDCHECK_X_X_AUTO_SIGN;// -1;
                    //CTool.It.OAlignmentTool.I32AutoCheckX_AlignS_manual = CSign.ODDCHECK_X_S_MANUAL_SIGN; //* -1;
                    //CTool.It.OAlignmentTool.I32AutoCheckX_AlignX_manual = CSign.ODDCHECK_X_X_MANUAL_SIGN; //* -1;

                    CTool.It.OAlignmentTool.I32AutoCheckY_AlignS_auto = CSign.ODDCHECK_Y_S_AUTO_SIGN; //* -1;
                    CTool.It.OAlignmentTool.I32AutoCheckY_AlignX_auto = CSign.ODDCHECK_Y_X_AUTO_SIGN;// *-1;
                    //CTool.It.OAlignmentTool.I32AutoCheckY_AlignS_manual = CSign.ODDCHECK_Y_S_MANUAL_SIGN;// * -1;
                    //CTool.It.OAlignmentTool.I32AutoCheckY_AlignX_manual = CSign.ODDCHECK_Y_X_MANUAL_SIGN;// *-1;

                    CTool.It.OAlignmentTool.I32AutoCheckT_AlignS_auto = CSign.ODDCHECK_T_S_AUTO_SIGN;// * -1;
                    CTool.It.OAlignmentTool.I32AutoCheckT_AlignX_auto = CSign.ODDCHECK_T_X_AUTO_SIGN;// *-1;
                    //CTool.It.OAlignmentTool.I32AutoCheckT_AlignS_manual = CSign.ODDCHECK_T_S_MANUAL_SIGN;// * -1;
                    //CTool.It.OAlignmentTool.I32AutoCheckT_AlignX_manual = CSign.ODDCHECK_T_X_MANUAL_SIGN; //* -1;

                }
               

                if(StrEnabled1 == "true")
                {
                    if(this.BtnEnabled1.Tag.ToString() == "1")
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
                this.m_OHead1.EHead = EVIEW.HEAD1;
                this.m_OHead2.EHead = EVIEW.HEAD2;
                this.m_OHead3.EHead = EVIEW.HEAD3;
                this.m_OHead4.EHead = EVIEW.HEAD4;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private CHomeDisplayer GetViewOfHead(int I32ViewOfHead)
        {
            CHomeDisplayer OResult = null;

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
        

        private void DrawAlignmentHeadText(EVIEW ERequestView, EVIEW ETargetView, ERESULT EResult, Label OControl, double F64Value)
        {
            try
            {
                if ((ERequestView & ETargetView) == ETargetView)
                {
                    if (EResult == ERESULT.RETRY)
                    {
                        OControl.Text = Math.Round(F64Value, 4).ToString("0.0000");
                        OControl.ForeColor = Color.Black;
                        OControl.BackColor = Color.White;
                    }
                    else if (EResult == ERESULT.OK)
                    {
                        OControl.Text = Math.Round(F64Value, 4).ToString("0.0000");
                        OControl.ForeColor = Color.White;
                        OControl.BackColor = Color.ForestGreen;
                    }
                    else //NG
                    {
                        OControl.Text = "NG";
                        OControl.ForeColor = Color.White;
                        OControl.BackColor = Color.Red;
                    }
                }
                else
                {
                    OControl.Text = "-";
                    OControl.ForeColor = Color.Black;
                    OControl.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void DrawAlignmentYText(ERESULT EResult, Label OControl, double F64Value)
        {
            try
            {
                if (EResult == ERESULT.RETRY)
                {
                    OControl.Text = Math.Round(F64Value, 4).ToString("0.0000");
                    OControl.ForeColor = Color.Black;
                    OControl.BackColor = Color.White;
                }
                else if (EResult == ERESULT.OK)
                {
                    OControl.Text = Math.Round(F64Value, 4).ToString("0.0000");
                    OControl.ForeColor = Color.White;
                    OControl.BackColor = Color.ForestGreen;
                }
                else
                {
                    OControl.Text = "NG";
                    OControl.ForeColor = Color.White;
                    OControl.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void DrawAlignmentTText(ERESULT EResult, Label OControl, double F64Value)
        {
            try
            {
                if (EResult == ERESULT.RETRY)
                {
                    OControl.Text = Math.Round(F64Value, 5).ToString("0.00000");
                    OControl.ForeColor = Color.Black;
                    OControl.BackColor = Color.White;
                }
                else if (EResult == ERESULT.OK)
                {
                    OControl.Text = Math.Round(F64Value, 5).ToString("0.00000");
                    OControl.ForeColor = Color.White;
                    OControl.BackColor = Color.ForestGreen;
                }
                else
                {
                    OControl.Text = "NG";
                    OControl.ForeColor = Color.White;
                    OControl.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void DrawCutText(EVIEW ERequestView, EVIEW ETargetView, bool BOK, double F64Length, CCutResult OResult, Label OControl)
        {
            try
            {
                if ((ERequestView & ETargetView) == ETargetView)
                {
                    if (BOK == true)
                    {
                        OControl.Text = Math.Round(F64Length, 3).ToString("0.000");
                        OControl.ForeColor = Color.White;
                        OControl.BackColor = Color.ForestGreen;
                    }
                    else
                    {
                        if ((OResult.BInspected == true) && (OResult.BOK == true))
                        {
                            OControl.Text = Math.Round(F64Length, 3).ToString("0.000");
                            OControl.ForeColor = Color.White;
                            OControl.BackColor = Color.Red;
                        }
                        else
                        {
                            OControl.Text = "NG";
                            OControl.ForeColor = Color.White;
                            OControl.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    OControl.Text = "-";
                    OControl.ForeColor = Color.Black;
                    OControl.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void PerformManualAlignment(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                this.m_BPreventEvent = true;

              //  EMANUAL EManual = (this.ChkManualAlignKind1.Checked == true) ? EMANUAL.ALIGN_X : EMANUAL.ALIGN_SHARP;
                EMANUAL EManual = EMANUAL.ALIGN_SHARP;
                CMarkResult OHead1Result = null;
                CMarkResult OHead2Result = null;
                CMarkResult OHead3Result = null;
                CMarkResult OHead4Result = null;
             
               if (CEnvironment.It.StrEnabled1 =="true") OHead1Result = this.m_OHead1.GetManualAlignmentInfo(EView);
               if (CEnvironment.It.StrEnabled2 == "true") OHead2Result = this.m_OHead2.GetManualAlignmentInfo(EView);
               if (CEnvironment.It.StrEnabled3 == "true") OHead3Result = this.m_OHead3.GetManualAlignmentInfo(EView);
               if (CEnvironment.It.StrEnabled4 == "true") OHead4Result = this.m_OHead4.GetManualAlignmentInfo(EView);

               CTool.It.OAlignmentTool.RunManualAlignment(EAngle, EView, EManual, OHead1Result, OHead2Result, OHead3Result, OHead4Result);
               
                this.m_OHead1.VisibleAlignmentManualPoint(false);
                this.m_OHead2.VisibleAlignmentManualPoint(false);
                this.m_OHead3.VisibleAlignmentManualPoint(false);
                this.m_OHead4.VisibleAlignmentManualPoint(false);

                this.m_BManual = false;
                this.BtnManual1.BackColor = Color.SteelBlue;
                this.BtnManual2.BackColor = Color.SteelBlue;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }


        private void ChkManualYUnFix_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkManualYUnFix.Checked == true) //then override to unfix
            {
                this.ChkManualYUnFix.Text = "No Fixing Y";
                this.ChkManualYUnFix.BackColor = Color.Firebrick;

                this.m_OHead1.BOverride_NoFix = true;
                this.m_OHead2.BOverride_NoFix = true;
                this.m_OHead3.BOverride_NoFix = true;
                this.m_OHead4.BOverride_NoFix = true;
            }
            else
            {
                this.ChkManualYUnFix.Text = "Fix Y";
                this.ChkManualYUnFix.BackColor = Color.ForestGreen;

                this.m_OHead1.BOverride_NoFix = false;
                this.m_OHead2.BOverride_NoFix = false;
                this.m_OHead3.BOverride_NoFix = false;
                this.m_OHead4.BOverride_NoFix = false;
            }
        }
        #endregion

        private void BtnEnabled_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((string)((Control)sender).Tag)
                {
                    case "5":
                        //if (((Control)sender).BackColor == Color.Indigo)
                        //{
                        //    ((Control)sender).BackColor = Color.DarkRed;
                        //    ((Control)sender).Text = "Mark X";
                        //    CEnvironment.It.StrEnableMark = "false";
                        //}
                        //else
                        //{
                        //    ((Control)sender).BackColor = Color.Indigo;
                        //         ((Control)sender).Text = "Mark O";
                        //    CEnvironment.It.StrEnableMark = "true";
                        //}
                        break;
                }
                CEnvironment.It.Commit();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private void ChkFitImage_CheckedChanged(object sender, EventArgs e)
        {

            this.CdpView1.Fit(true);
            this.CdpView2.Fit(true);
            this.CdpView3.Fit(true);
            this.CdpView4.Fit(true);
        }


        private void NudMAXTRY_ValueChanged(object sender, EventArgs e)
        {
            CEnvironment.It.I32MaxTry = (int)this.NudMAXTRY.Value;
        }


        private void BtnGoLeft_Click(object sender, EventArgs e)
        {

            //even if the head is deactivated, the job should be applied
         //   double x = -Convert.ToDouble(this.NudXJog.Value);
          //  CMotionController.It.OPLC.SetStageMovement
          //(
          //   x * CEnvironment.DIRECTION_INFO, 
          //    x* CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    0,
          //    0
          //);
          //  CLoggingTool.It.ShowAlignmentMotionMovement
          //  (
          //      ESTAGE_ANGLE.NONE,
          //       x * CEnvironment.DIRECTION_INFO, 
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    0,
          //    0
          //  );
        }


        private void BtnGoRight_Click(object sender, EventArgs e)
        {
            //even if the head is deactivated, the job should be applied
         
           // double x = Convert.ToDouble(this.NudXJog.Value);
          //  CMotionController.It.OPLC.SetStageMovement
          //(
          //   x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    0,
          //    0
          //);
          //  CLoggingTool.It.ShowAlignmentMotionMovement
          //  (
          //      ESTAGE_ANGLE.NONE,
          //       x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    x * CEnvironment.DIRECTION_INFO,
          //    0,
          //    0
          //  );
        }


        private void ChkIndividual_CheckedChanged(object sender, EventArgs e)
        {
            //20170412
           // if (e == EventArgs.Empty) return;

            if(this.ChkIndividual.Checked == true)
            {
                this.ChkIndividual.BackColor = Color.Lime;
                this.ChkIndividual.Text = "SafeLock Off";
                CTool.It.OAlignmentTool.BIndividual = true;
            }
            else if (this.ChkIndividual.Checked == false)
            {
                this.ChkIndividual.BackColor = Color.SteelBlue;
                this.ChkIndividual.Text = "SafeLock On";
                CTool.It.OAlignmentTool.BIndividual = false;
            }
        }
    }
}
