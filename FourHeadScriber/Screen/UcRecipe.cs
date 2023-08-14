using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Daekhon.Common;
using Jhjo.Common;
using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.Caliper;

namespace FourHeadScriber
{
    public partial class UcRecipe : UcScreen
    {
        #region CONST
        private const string STANDARD_POINT_LEFT = "LEFT";
        #endregion


        #region VARIABLE
        private EEDIT m_EEdit = EEDIT.NONE;
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EView = EVIEW.NONE;
        private ETOOL m_ETool = ETOOL.NONE;
        private EMARK m_EMark = EMARK.NONE;

        private CRecipe m_ORecipe = null;
        private CAlignmentRecipe m_OAlignmentRecipe = null;
        private CAnalysisRecipe m_OAnalysisRecipe = null;

        private bool m_BPreventEvent = false;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcRecipe()
        {
            InitializeComponent();

            try
            {
                this.m_BPreventEvent = true;

                this.CdpMark1.AutoFit = true;
                this.CdpMark1.BackColor = Color.Black;
                this.CdpMark1.HorizontalScrollBar = false;
                this.CdpMark1.VerticalScrollBar = false;
                this.CdpMark2.AutoFit = true;
                this.CdpMark2.BackColor = Color.Black;
                this.CdpMark2.HorizontalScrollBar = false;
                this.CdpMark2.VerticalScrollBar = false;
                this.CdpMark3.AutoFit = true;
                this.CdpMark3.BackColor = Color.Black;
                this.CdpMark3.HorizontalScrollBar = false;
                this.CdpMark3.VerticalScrollBar = false;
                this.CdpMark4.AutoFit = true;
                this.CdpMark4.BackColor = Color.Black;
                this.CdpMark4.HorizontalScrollBar = false;
                this.CdpMark4.VerticalScrollBar = false;
                this.CdpMark5.AutoFit = true;
                this.CdpMark5.BackColor = Color.Black;
                this.CdpMark5.HorizontalScrollBar = false;
                this.CdpMark5.VerticalScrollBar = false;

                this.UpdateList();

                if (this.LtbRecipe.Items.Count > 0)
                {
                    this.NudID.Value = 1;
                    this.TxtName.Text = String.Empty;
                    this.BtnAdd.BackColor = Color.SteelBlue;
                    this.BtnModify.BackColor = Color.SteelBlue;
                    this.BtnDelete.BackColor = Color.SteelBlue;
                    this.BtnSave.BackColor = SystemColors.Control;
                    this.BtnCancel.BackColor = SystemColors.Control;
                    this.BtnRename.BackColor = Color.SteelBlue;
                    this.BtnCopy.BackColor = Color.SteelBlue;
                    this.BtnApply.BackColor = Color.SteelBlue;
                    this.NudID.Enabled = true;
                    this.TxtName.Enabled = true;
                    this.BtnAdd.Enabled = true;
                    this.BtnModify.Enabled = true;
                    this.BtnDelete.Enabled = true;
                    this.BtnSave.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.BtnRename.Enabled = true;
                    this.BtnCopy.Enabled = true;
                    this.BtnApply.Enabled = true;
                }
                else
                {
                    this.NudID.Value = 1;
                    this.TxtName.Text = String.Empty;
                    this.BtnAdd.BackColor = Color.SteelBlue;
                    this.BtnModify.BackColor = SystemColors.Control;
                    this.BtnDelete.BackColor = SystemColors.Control;
                    this.BtnSave.BackColor = SystemColors.Control;
                    this.BtnCancel.BackColor = SystemColors.Control;
                    this.BtnRename.BackColor = SystemColors.Control;
                    this.BtnCopy.BackColor = SystemColors.Control;
                    this.BtnApply.BackColor = SystemColors.Control;
                    this.NudID.Enabled = true;
                    this.TxtName.Enabled = true;
                    this.BtnAdd.Enabled = true;
                    this.BtnModify.Enabled = false;
                    this.BtnDelete.Enabled = false;
                    this.BtnSave.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.BtnRename.Enabled = false;
                    this.BtnCopy.Enabled = false;
                    this.BtnApply.Enabled = false;
                }

                this.BtnStageAngleZero.BackColor = SystemColors.Control;
                this.BtnStageAngleNinety.BackColor = SystemColors.Control;
                this.BtnViewHead1.BackColor = SystemColors.Control;
                this.BtnViewHead2.BackColor = SystemColors.Control;
                this.BtnViewHead3.BackColor = SystemColors.Control;
                this.BtnViewHead4.BackColor = SystemColors.Control;
                this.BtnToolMark.BackColor = SystemColors.Control;
                this.BtnToolDirectCut.BackColor = SystemColors.Control;
                this.BtnToolCrossCut.BackColor = SystemColors.Control;
                this.BtnMark1.BackColor = SystemColors.Control;
                this.BtnMark2.BackColor = SystemColors.Control;
                this.BtnMark3.BackColor = SystemColors.Control;
                this.BtnMark4.BackColor = SystemColors.Control;
                this.BtnMark5.BackColor = SystemColors.Control;
                this.BtnImage.BackColor = SystemColors.Control;
                this.BtnDetail.BackColor = SystemColors.Control;
                this.BtnEntire.BackColor = SystemColors.Control;
                this.BtnStageAngleZero.Enabled = false;
                this.BtnStageAngleNinety.Enabled = false;
                this.BtnViewHead1.Enabled = false;
                this.BtnViewHead2.Enabled = false;
                this.BtnViewHead3.Enabled = false;
                this.BtnViewHead4.Enabled = false;
                this.BtnToolMark.Enabled = false;
                this.BtnToolDirectCut.Enabled = false;
                this.BtnToolCrossCut.Enabled = false;
                this.BtnMark1.Enabled = false;
                this.BtnMark2.Enabled = false;
                this.BtnMark3.Enabled = false;
                this.BtnMark4.Enabled = false;
                this.BtnMark5.Enabled = false;
                this.BtnImage.Enabled = false;
                this.BtnDetail.Enabled = false;
                this.BtnEntire.Enabled = false;

                this.CpeMark.Enabled = false;
                this.CceEdge.Enabled = false;

                this.ChkZeroStandardPointHead1.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead2.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead3.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead4.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead1.Checked = false;
                this.ChkZeroStandardPointHead2.Checked = false;
                this.ChkZeroStandardPointHead3.Checked = false;
                this.ChkZeroStandardPointHead4.Checked = false;
                this.ChkZeroStandardPointHead1.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead2.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead3.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead4.BackColor = SystemColors.Control;
                this.NudZeroAlignmentLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead4.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitY.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitT.Controls[1].Text = String.Empty;
                this.NudZeroLength.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead4.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead4.Controls[1].Text = String.Empty;
                this.ChkZeroStandardPointHead1.Enabled = false;
                this.ChkZeroStandardPointHead2.Enabled = false;
                this.ChkZeroStandardPointHead3.Enabled = false;
                this.ChkZeroStandardPointHead4.Enabled = false;
                this.NudZeroAlignmentLimitHead1.Enabled = false;
                this.NudZeroAlignmentLimitHead2.Enabled = false;
                this.NudZeroAlignmentLimitHead3.Enabled = false;
                this.NudZeroAlignmentLimitHead4.Enabled = false;
                this.NudZeroAlignmentLimitY.Enabled = false;
                this.NudZeroLength.Enabled = false;
                this.NudZeroAlignmentLimitT.Enabled = false;
                this.NudZeroDirectCutLimitHead1.Enabled = false;
                this.NudZeroDirectCutLimitHead2.Enabled = false;
                this.NudZeroDirectCutLimitHead3.Enabled = false;
                this.NudZeroDirectCutLimitHead4.Enabled = false;
                this.NudZeroCrossCutLimitHead1.Enabled = false;
                this.NudZeroCrossCutLimitHead2.Enabled = false;
                this.NudZeroCrossCutLimitHead3.Enabled = false;
                this.NudZeroCrossCutLimitHead4.Enabled = false;

                this.ChkNinetyStandardPointHead1.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead2.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead3.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead4.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead1.Checked = false;
                this.ChkNinetyStandardPointHead2.Checked = false;
                this.ChkNinetyStandardPointHead3.Checked = false;
                this.ChkNinetyStandardPointHead4.Checked = false;
                this.ChkNinetyStandardPointHead1.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead2.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead3.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead4.BackColor = SystemColors.Control;
                this.NudNinetyAlignmentLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead4.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitY.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitT.Controls[1].Text = String.Empty;
                this.NudNinetyLength.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead4.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead4.Controls[1].Text = String.Empty;
                this.ChkNinetyStandardPointHead1.Enabled = false;
                this.ChkNinetyStandardPointHead2.Enabled = false;
                this.ChkNinetyStandardPointHead3.Enabled = false;
                this.ChkNinetyStandardPointHead4.Enabled = false;
                this.NudNinetyAlignmentLimitHead1.Enabled = false;
                this.NudNinetyAlignmentLimitHead2.Enabled = false;
                this.NudNinetyAlignmentLimitHead3.Enabled = false;
                this.NudNinetyAlignmentLimitHead4.Enabled = false;
                this.NudNinetyAlignmentLimitY.Enabled = false;
                this.NudNinetyAlignmentLimitT.Enabled = false;
                this.NudNinetyLength.Enabled = false;
                this.NudNinetyDirectCutLimitHead1.Enabled = false;
                this.NudNinetyDirectCutLimitHead2.Enabled = false;
                this.NudNinetyDirectCutLimitHead3.Enabled = false;
                this.NudNinetyDirectCutLimitHead4.Enabled = false;
                this.NudNinetyCrossCutLimitHead1.Enabled = false;
                this.NudNinetyCrossCutLimitHead2.Enabled = false;
                this.NudNinetyCrossCutLimitHead3.Enabled = false;
                this.NudNinetyCrossCutLimitHead4.Enabled = false;
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
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_BPreventEvent = true;

                int I32ID = (int)this.NudID.Value;
                foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                {
                    if (_Item.I32ID == I32ID)
                    {
                        CMsgBox.Warning("Same recipe's ID already exists!");
                        return;
                    }
                }

                String StrName = this.TxtName.Text.Trim();
                if (String.IsNullOrEmpty(StrName) == true)
                {
                    CMsgBox.Warning("Please do not leave 'Name' section empty");
                    return;
                }


                this.m_EEdit = EEDIT.ADD;
                this.m_EAngle = ESTAGE_ANGLE.ZERO;
                this.m_EView = EVIEW.HEAD1;
                this.m_ETool = ETOOL.MARK;
                this.m_EMark = EMARK.INDEX1;

                this.m_ORecipe = new CRecipe(Convert.ToInt32(this.NudID.Value), this.TxtName.Text.Trim());
                this.m_ORecipe.Create();
                this.m_OAlignmentRecipe = this.m_ORecipe.OZero;
                this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead1;


                this.BtnAdd.BackColor = SystemColors.Control;
                this.BtnModify.BackColor = SystemColors.Control;
                this.BtnDelete.BackColor = SystemColors.Control;
                this.BtnSave.BackColor = Color.SteelBlue;
                this.BtnCancel.BackColor = Color.SteelBlue;
                this.BtnRename.BackColor = SystemColors.Control;
                this.BtnCopy.BackColor = SystemColors.Control;
                this.BtnApply.BackColor = SystemColors.Control;
                this.NudID.Enabled = false;
                this.TxtName.Enabled = false;
                this.BtnAdd.Enabled = false;
                this.BtnModify.Enabled = false;
                this.BtnDelete.Enabled = false;
                this.BtnSave.Enabled = true;
                this.BtnCancel.Enabled = true;
                this.BtnRename.Enabled = false;
                this.BtnCopy.Enabled = false;
                this.BtnApply.Enabled = false;

                this.BtnStageAngleZero.BackColor = Color.ForestGreen;
                this.BtnStageAngleNinety.BackColor = Color.SteelBlue;
                this.BtnViewHead1.BackColor = Color.ForestGreen;
                this.BtnViewHead2.BackColor = Color.SteelBlue;
                this.BtnViewHead3.BackColor = Color.SteelBlue;
                this.BtnViewHead4.BackColor = Color.SteelBlue;
                this.BtnToolMark.BackColor = Color.ForestGreen;
                this.BtnToolDirectCut.BackColor = Color.SteelBlue;
                this.BtnToolCrossCut.BackColor = Color.SteelBlue;
                this.BtnMark1.BackColor = Color.ForestGreen;
                this.BtnMark2.BackColor = Color.SteelBlue;
                this.BtnMark3.BackColor = Color.SteelBlue;
                this.BtnMark4.BackColor = Color.SteelBlue;
                this.BtnMark5.BackColor = Color.SteelBlue;
                this.BtnImage.BackColor = Color.SteelBlue;
                this.BtnDetail.BackColor = Color.SteelBlue;
                this.BtnEntire.BackColor = Color.SteelBlue;
                this.BtnStageAngleZero.Enabled = true;
                this.BtnStageAngleNinety.Enabled = true;
                this.BtnViewHead1.Enabled = true;
                this.BtnViewHead2.Enabled = true;
                this.BtnViewHead3.Enabled = true;
                this.BtnViewHead4.Enabled = true;
                this.BtnToolMark.Enabled = true;
                this.BtnToolDirectCut.Enabled = true;
                this.BtnToolCrossCut.Enabled = true;
                this.BtnMark1.Enabled = true;
                this.BtnMark2.Enabled = true;
                this.BtnMark3.Enabled = true;
                this.BtnMark4.Enabled = true;
                this.BtnMark5.Enabled = true;
                this.BtnImage.Enabled = true;
                this.BtnDetail.Enabled = true;
                this.BtnEntire.Enabled = true;

                this.CpeMark.Subject = this.m_ORecipe.OZero.OHead1.OMarkTool1;
                this.CpeMark.BringToFront();
                this.CpeMark.Enabled = true;
                this.CceEdge.Enabled = true;

                this.ChkZeroStandardPointHead1.Text = this.m_ORecipe.OZero.OHead1.EStdPoint.ToString();
                this.ChkZeroStandardPointHead2.Text = this.m_ORecipe.OZero.OHead2.EStdPoint.ToString();
                this.ChkZeroStandardPointHead3.Text = this.m_ORecipe.OZero.OHead3.EStdPoint.ToString();
                this.ChkZeroStandardPointHead4.Text = this.m_ORecipe.OZero.OHead4.EStdPoint.ToString();
                this.ChkZeroStandardPointHead1.Checked = (this.m_ORecipe.OZero.OHead1.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead2.Checked = (this.m_ORecipe.OZero.OHead2.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead3.Checked = (this.m_ORecipe.OZero.OHead3.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead4.Checked = (this.m_ORecipe.OZero.OHead4.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead1.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead2.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead3.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead4.BackColor = Color.SteelBlue;
                this.NudZeroAlignmentLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead1);
                this.NudZeroAlignmentLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead2);
                this.NudZeroAlignmentLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead3);
                this.NudZeroAlignmentLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead4);
                this.NudZeroAlignmentLimitY.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitY);
                this.NudZeroAlignmentLimitT.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitT);
                this.NudZeroLength.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentProductLength);
                this.NudZeroAlignmentLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead1.ToString("0.000");
                this.NudZeroAlignmentLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead2.ToString("0.000");
                this.NudZeroAlignmentLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead3.ToString("0.000");
                this.NudZeroAlignmentLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead4.ToString("0.000");
                this.NudZeroAlignmentLimitY.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitY.ToString("0.000");
                this.NudZeroAlignmentLimitT.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitT.ToString("0.00000");
                this.NudZeroLength.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentProductLength.ToString("0.000");

                this.NudZeroDirectCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead1);
                this.NudZeroDirectCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead2);
                this.NudZeroDirectCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead3);
                this.NudZeroDirectCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead4);
                this.NudZeroDirectCutLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead1.ToString("0.000");
                this.NudZeroDirectCutLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead2.ToString("0.000");
                this.NudZeroDirectCutLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead3.ToString("0.000");
                this.NudZeroDirectCutLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead4.ToString("0.000");
                this.NudZeroCrossCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead1);
                this.NudZeroCrossCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead2);
                this.NudZeroCrossCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead3);
                this.NudZeroCrossCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead4);
                this.NudZeroCrossCutLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead1.ToString("0.000");
                this.NudZeroCrossCutLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead2.ToString("0.000");
                this.NudZeroCrossCutLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead3.ToString("0.000");
                this.NudZeroCrossCutLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead4.ToString("0.000");
                this.ChkZeroStandardPointHead1.Enabled = true;
                this.ChkZeroStandardPointHead2.Enabled = true;
                this.ChkZeroStandardPointHead3.Enabled = true;
                this.ChkZeroStandardPointHead4.Enabled = true;
                this.NudZeroAlignmentLimitHead1.Enabled = true;
                this.NudZeroAlignmentLimitHead2.Enabled = true;
                this.NudZeroAlignmentLimitHead3.Enabled = true;
                this.NudZeroAlignmentLimitHead4.Enabled = true;
                this.NudZeroAlignmentLimitY.Enabled = true;
                this.NudZeroAlignmentLimitT.Enabled = true;
                this.NudZeroLength.Enabled = true;
                this.NudZeroDirectCutLimitHead1.Enabled = true;
                this.NudZeroDirectCutLimitHead2.Enabled = true;
                this.NudZeroDirectCutLimitHead3.Enabled = true;
                this.NudZeroDirectCutLimitHead4.Enabled = true;
                this.NudZeroCrossCutLimitHead1.Enabled = true;
                this.NudZeroCrossCutLimitHead2.Enabled = true;
                this.NudZeroCrossCutLimitHead3.Enabled = true;
                this.NudZeroCrossCutLimitHead4.Enabled = true;

                this.ChkNinetyStandardPointHead1.Text = this.m_ORecipe.ONinety.OHead1.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead2.Text = this.m_ORecipe.ONinety.OHead2.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead3.Text = this.m_ORecipe.ONinety.OHead3.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead4.Text = this.m_ORecipe.ONinety.OHead4.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead1.Checked = (this.m_ORecipe.ONinety.OHead1.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead2.Checked = (this.m_ORecipe.ONinety.OHead2.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead3.Checked = (this.m_ORecipe.ONinety.OHead3.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead4.Checked = (this.m_ORecipe.ONinety.OHead4.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead1.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead2.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead3.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead4.BackColor = Color.SteelBlue;
                this.NudNinetyAlignmentLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead1);
                this.NudNinetyAlignmentLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead2);
                this.NudNinetyAlignmentLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead3);
                this.NudNinetyAlignmentLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead4);
                this.NudNinetyAlignmentLimitY.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitY);
                this.NudNinetyAlignmentLimitT.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitT);
                this.NudNinetyLength.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentProductLength);
                this.NudNinetyAlignmentLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead1.ToString("0.000");
                this.NudNinetyAlignmentLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead2.ToString("0.000");
                this.NudNinetyAlignmentLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead3.ToString("0.000");
                this.NudNinetyAlignmentLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead4.ToString("0.000");
                this.NudNinetyAlignmentLimitY.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitY.ToString("0.000");
                this.NudNinetyAlignmentLimitT.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitT.ToString("0.00000");
                this.NudNinetyLength.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentProductLength.ToString("0.000");
                this.NudNinetyDirectCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead1);
                this.NudNinetyDirectCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead2);
                this.NudNinetyDirectCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead3);
                this.NudNinetyDirectCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead4);
                this.NudNinetyDirectCutLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead1.ToString("0.000");
                this.NudNinetyDirectCutLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead2.ToString("0.000");
                this.NudNinetyDirectCutLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead3.ToString("0.000");
                this.NudNinetyDirectCutLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead4.ToString("0.000");
                this.NudNinetyCrossCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead1);
                this.NudNinetyCrossCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead2);
                this.NudNinetyCrossCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead3);
                this.NudNinetyCrossCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead4);
                this.NudNinetyCrossCutLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead1.ToString("0.000");
                this.NudNinetyCrossCutLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead2.ToString("0.000");
                this.NudNinetyCrossCutLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead3.ToString("0.000");
                this.NudNinetyCrossCutLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead4.ToString("0.000");
                this.ChkNinetyStandardPointHead1.Enabled = true;
                this.ChkNinetyStandardPointHead2.Enabled = true;
                this.ChkNinetyStandardPointHead3.Enabled = true;
                this.ChkNinetyStandardPointHead4.Enabled = true;
                this.NudNinetyAlignmentLimitHead1.Enabled = true;
                this.NudNinetyAlignmentLimitHead2.Enabled = true;
                this.NudNinetyAlignmentLimitHead3.Enabled = true;
                this.NudNinetyAlignmentLimitHead4.Enabled = true;
                this.NudNinetyAlignmentLimitY.Enabled = true;
                this.NudNinetyAlignmentLimitT.Enabled = true;
                this.NudNinetyLength.Enabled = true;
                this.NudNinetyDirectCutLimitHead1.Enabled = true;
                this.NudNinetyDirectCutLimitHead2.Enabled = true;
                this.NudNinetyDirectCutLimitHead3.Enabled = true;
                this.NudNinetyDirectCutLimitHead4.Enabled = true;
                this.NudNinetyCrossCutLimitHead1.Enabled = true;
                this.NudNinetyCrossCutLimitHead2.Enabled = true;
                this.NudNinetyCrossCutLimitHead3.Enabled = true;
                this.NudNinetyCrossCutLimitHead4.Enabled = true;

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


        private void BtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_BPreventEvent = true;

                String StrItem = (String)LtbRecipe.SelectedItem;
                if (String.IsNullOrEmpty(StrItem) == true)
                {
                    CMsgBox.Warning("Please select a recipe to modify!");
                    return;
                }


                int I32ID = Convert.ToInt32(StrItem.Substring(0, StrItem.IndexOf(".")));
                foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                {
                    if (_Item.I32ID == I32ID)
                    {
                        this.m_ORecipe = new CRecipe(_Item);
                        this.m_ORecipe.Load();
                        this.m_OAlignmentRecipe = this.m_ORecipe.OZero;
                        this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead1;
                    }
                }
                this.m_EEdit = EEDIT.MODIFY;
                this.m_EAngle = ESTAGE_ANGLE.ZERO;
                this.m_EView = EVIEW.HEAD1;
                this.m_ETool = ETOOL.MARK;
                this.m_EMark = EMARK.INDEX1;


                this.NudID.Value = this.m_ORecipe.I32ID;
                this.TxtName.Text = this.m_ORecipe.StrName;
                this.BtnAdd.BackColor = SystemColors.Control;
                this.BtnModify.BackColor = SystemColors.Control;
                this.BtnDelete.BackColor = SystemColors.Control;
                this.BtnSave.BackColor = Color.SteelBlue;
                this.BtnCancel.BackColor = Color.SteelBlue;
                this.BtnRename.BackColor = SystemColors.Control;
                this.BtnCopy.BackColor = SystemColors.Control;
                this.BtnApply.BackColor = SystemColors.Control;
                this.NudID.Enabled = false;
                this.TxtName.Enabled = false;
                this.BtnAdd.Enabled = false;
                this.BtnModify.Enabled = false;
                this.BtnDelete.Enabled = false;
                this.BtnSave.Enabled = true;
                this.BtnCancel.Enabled = true;
                this.BtnRename.Enabled = false;
                this.BtnCopy.Enabled = false;
                this.BtnApply.Enabled = false;

                this.BtnStageAngleZero.BackColor = Color.ForestGreen;
                this.BtnStageAngleNinety.BackColor = Color.SteelBlue;
                this.BtnViewHead1.BackColor = Color.ForestGreen;
                this.BtnViewHead2.BackColor = Color.SteelBlue;
                this.BtnViewHead3.BackColor = Color.SteelBlue;
                this.BtnViewHead4.BackColor = Color.SteelBlue;
                this.BtnToolMark.BackColor = Color.ForestGreen;
                this.BtnToolDirectCut.BackColor = Color.SteelBlue;
                this.BtnToolCrossCut.BackColor = Color.SteelBlue;
                this.BtnMark1.BackColor = Color.ForestGreen;
                this.BtnMark2.BackColor = Color.SteelBlue;
                this.BtnMark3.BackColor = Color.SteelBlue;
                this.BtnMark4.BackColor = Color.SteelBlue;
                this.BtnMark5.BackColor = Color.SteelBlue;
                this.BtnImage.BackColor = Color.SteelBlue;
                this.BtnDetail.BackColor = Color.SteelBlue;
                this.BtnEntire.BackColor = Color.SteelBlue;
                this.BtnStageAngleZero.Enabled = true;
                this.BtnStageAngleNinety.Enabled = true;
                this.BtnViewHead1.Enabled = true;
                this.BtnViewHead2.Enabled = true;
                this.BtnViewHead3.Enabled = true;
                this.BtnViewHead4.Enabled = true;
                this.BtnToolMark.Enabled = true;
                this.BtnToolDirectCut.Enabled = true;
                this.BtnToolCrossCut.Enabled = true;
                this.BtnMark1.Enabled = true;
                this.BtnMark2.Enabled = true;
                this.BtnMark3.Enabled = true;
                this.BtnMark4.Enabled = true;
                this.BtnMark5.Enabled = true;
                this.BtnImage.Enabled = true;
                this.BtnDetail.Enabled = true;
                this.BtnEntire.Enabled = true;

                this.CpeMark.Subject = this.m_ORecipe.OZero.OHead1.OMarkTool1;
                this.CpeMark.BringToFront();
                this.DisplayMarks();
                this.CpeMark.Enabled = true;
                this.CceEdge.Enabled = true;

                this.ChkZeroStandardPointHead1.Text = this.m_ORecipe.OZero.OHead1.EStdPoint.ToString();
                this.ChkZeroStandardPointHead2.Text = this.m_ORecipe.OZero.OHead2.EStdPoint.ToString();
                this.ChkZeroStandardPointHead3.Text = this.m_ORecipe.OZero.OHead3.EStdPoint.ToString();
                this.ChkZeroStandardPointHead4.Text = this.m_ORecipe.OZero.OHead4.EStdPoint.ToString();
                this.ChkZeroStandardPointHead1.Checked = (this.m_ORecipe.OZero.OHead1.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead2.Checked = (this.m_ORecipe.OZero.OHead2.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead3.Checked = (this.m_ORecipe.OZero.OHead3.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead4.Checked = (this.m_ORecipe.OZero.OHead4.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkZeroStandardPointHead1.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead2.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead3.BackColor = Color.SteelBlue;
                this.ChkZeroStandardPointHead4.BackColor = Color.SteelBlue;
                this.NudZeroAlignmentLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead1);
                this.NudZeroAlignmentLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead2);
                this.NudZeroAlignmentLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead3);
                this.NudZeroAlignmentLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitHead4);
                this.NudZeroAlignmentLimitY.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitY);
                this.NudZeroAlignmentLimitT.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentLimitT);
                this.NudZeroLength.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64AlignmentProductLength);
                this.NudZeroAlignmentLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead1.ToString("0.000");
                this.NudZeroAlignmentLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead2.ToString("0.000");
                this.NudZeroAlignmentLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead3.ToString("0.000");
                this.NudZeroAlignmentLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitHead4.ToString("0.000");
                this.NudZeroAlignmentLimitY.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitY.ToString("0.000");
                this.NudZeroAlignmentLimitT.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentLimitT.ToString("0.00000");
                this.NudZeroLength.Controls[1].Text = this.m_ORecipe.OZero.F64AlignmentProductLength.ToString("0.000");
                this.NudZeroDirectCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead1);
                this.NudZeroDirectCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead2);
                this.NudZeroDirectCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead3);
                this.NudZeroDirectCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64DirectCutLimitHead4);
                this.NudZeroDirectCutLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead1.ToString("0.000");
                this.NudZeroDirectCutLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead2.ToString("0.000");
                this.NudZeroDirectCutLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead3.ToString("0.000");
                this.NudZeroDirectCutLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64DirectCutLimitHead4.ToString("0.000");
                this.NudZeroCrossCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead1);
                this.NudZeroCrossCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead2);
                this.NudZeroCrossCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead3);
                this.NudZeroCrossCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.OZero.F64CrossCutLimitHead4);
                this.NudZeroCrossCutLimitHead1.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead1.ToString("0.000");
                this.NudZeroCrossCutLimitHead2.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead2.ToString("0.000");
                this.NudZeroCrossCutLimitHead3.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead3.ToString("0.000");
                this.NudZeroCrossCutLimitHead4.Controls[1].Text = this.m_ORecipe.OZero.F64CrossCutLimitHead4.ToString("0.000");
                this.ChkZeroStandardPointHead1.Enabled = true;
                this.ChkZeroStandardPointHead2.Enabled = true;
                this.ChkZeroStandardPointHead3.Enabled = true;
                this.ChkZeroStandardPointHead4.Enabled = true;
                this.NudZeroAlignmentLimitHead1.Enabled = true;
                this.NudZeroAlignmentLimitHead2.Enabled = true;
                this.NudZeroAlignmentLimitHead3.Enabled = true;
                this.NudZeroAlignmentLimitHead4.Enabled = true;
                this.NudZeroAlignmentLimitY.Enabled = true;
                this.NudZeroAlignmentLimitT.Enabled = true;
                this.NudZeroLength.Enabled = true;
                this.NudZeroDirectCutLimitHead1.Enabled = true;
                this.NudZeroDirectCutLimitHead2.Enabled = true;
                this.NudZeroDirectCutLimitHead3.Enabled = true;
                this.NudZeroDirectCutLimitHead4.Enabled = true;
                this.NudZeroCrossCutLimitHead1.Enabled = true;
                this.NudZeroCrossCutLimitHead2.Enabled = true;
                this.NudZeroCrossCutLimitHead3.Enabled = true;
                this.NudZeroCrossCutLimitHead4.Enabled = true;

                this.ChkNinetyStandardPointHead1.Text = this.m_ORecipe.ONinety.OHead1.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead2.Text = this.m_ORecipe.ONinety.OHead2.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead3.Text = this.m_ORecipe.ONinety.OHead3.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead4.Text = this.m_ORecipe.ONinety.OHead4.EStdPoint.ToString();
                this.ChkNinetyStandardPointHead1.Checked = (this.m_ORecipe.ONinety.OHead1.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead2.Checked = (this.m_ORecipe.ONinety.OHead2.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead3.Checked = (this.m_ORecipe.ONinety.OHead3.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead4.Checked = (this.m_ORecipe.ONinety.OHead4.EStdPoint == ESTD_POINT.RIGHT);
                this.ChkNinetyStandardPointHead1.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead2.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead3.BackColor = Color.SteelBlue;
                this.ChkNinetyStandardPointHead4.BackColor = Color.SteelBlue;
                this.NudNinetyAlignmentLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead1);
                this.NudNinetyAlignmentLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead2);
                this.NudNinetyAlignmentLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead3);
                this.NudNinetyAlignmentLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitHead4);
                this.NudNinetyAlignmentLimitY.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitY);
                this.NudNinetyAlignmentLimitT.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentLimitT);
                this.NudNinetyLength.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64AlignmentProductLength);
                this.NudNinetyAlignmentLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead1.ToString("0.000");
                this.NudNinetyAlignmentLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead2.ToString("0.000");
                this.NudNinetyAlignmentLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead3.ToString("0.000");
                this.NudNinetyAlignmentLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitHead4.ToString("0.000");
                this.NudNinetyAlignmentLimitY.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitY.ToString("0.000");
                this.NudNinetyAlignmentLimitT.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentLimitT.ToString("0.00000");
                this.NudNinetyLength.Controls[1].Text = this.m_ORecipe.ONinety.F64AlignmentProductLength.ToString("0.000");
                this.NudNinetyDirectCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead1);
                this.NudNinetyDirectCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead2);
                this.NudNinetyDirectCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead3);
                this.NudNinetyDirectCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64DirectCutLimitHead4);
                this.NudNinetyDirectCutLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead1.ToString("0.000");
                this.NudNinetyDirectCutLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead2.ToString("0.000");
                this.NudNinetyDirectCutLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead3.ToString("0.000");
                this.NudNinetyDirectCutLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64DirectCutLimitHead4.ToString("0.000");
                this.NudNinetyCrossCutLimitHead1.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead1);
                this.NudNinetyCrossCutLimitHead2.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead2);
                this.NudNinetyCrossCutLimitHead3.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead3);
                this.NudNinetyCrossCutLimitHead4.Value = Convert.ToDecimal(this.m_ORecipe.ONinety.F64CrossCutLimitHead4);
                this.NudNinetyCrossCutLimitHead1.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead1.ToString("0.000");
                this.NudNinetyCrossCutLimitHead2.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead2.ToString("0.000");
                this.NudNinetyCrossCutLimitHead3.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead3.ToString("0.000");
                this.NudNinetyCrossCutLimitHead4.Controls[1].Text = this.m_ORecipe.ONinety.F64CrossCutLimitHead4.ToString("0.000");
                this.ChkNinetyStandardPointHead1.Enabled = true;
                this.ChkNinetyStandardPointHead2.Enabled = true;
                this.ChkNinetyStandardPointHead3.Enabled = true;
                this.ChkNinetyStandardPointHead4.Enabled = true;
                this.NudNinetyAlignmentLimitHead1.Enabled = true;
                this.NudNinetyAlignmentLimitHead2.Enabled = true;
                this.NudNinetyAlignmentLimitHead3.Enabled = true;
                this.NudNinetyAlignmentLimitHead4.Enabled = true;
                this.NudNinetyAlignmentLimitY.Enabled = true;
                this.NudNinetyAlignmentLimitT.Enabled = true;
                this.NudNinetyLength.Enabled = true;
                this.NudNinetyDirectCutLimitHead1.Enabled = true;
                this.NudNinetyDirectCutLimitHead2.Enabled = true;
                this.NudNinetyDirectCutLimitHead3.Enabled = true;
                this.NudNinetyDirectCutLimitHead4.Enabled = true;
                this.NudNinetyCrossCutLimitHead1.Enabled = true;
                this.NudNinetyCrossCutLimitHead2.Enabled = true;
                this.NudNinetyCrossCutLimitHead3.Enabled = true;
                this.NudNinetyCrossCutLimitHead4.Enabled = true;

                base.OnScreenFixed(true);

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


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                String StrItem = (String)LtbRecipe.SelectedItem;
                if (String.IsNullOrEmpty(StrItem) == true)
                {
                    CMsgBox.Warning("Please select a recipe to delete!");
                    return;
                }

                int I32ID = Convert.ToInt32(StrItem.Substring(0, StrItem.IndexOf(".")));
                if (CTool.It.ORecipe != null && CTool.It.ORecipe.I32ID == I32ID)
                {
                    CMsgBox.Warning("Can't delete a recipe that is in use.");
                    return;
                }

                if (CMsgBox.OKCancel("Do you really want to delete '" + StrItem + "' recipe?") == DialogResult.OK)
                {
                    CDB.It[CDB.TABLE_RECIPE_LIST].DeleteRow(CDB.RECIPE_LIST_ID, I32ID);
                    CDB.It[CDB.TABLE_RECIPE_INFO].DeleteRow(CDB.RECIPE_INFO_ID, I32ID);
                    CDB.It[CDB.TABLE_RECIPE_LIST].Commit();
                    CDB.It[CDB.TABLE_RECIPE_INFO].Commit();

                    for (int _Index = 0; _Index < CRecipeManager.It.LstORecipe.Count(); _Index++)
                    {
                        if (CRecipeManager.It.LstORecipe[_Index].I32ID != I32ID) continue;

                        CRecipeManager.It.LstORecipe[_Index].Delete();
                        CRecipeManager.It.LstORecipe.RemoveAt(_Index);
                        break;
                    }

                    this.UpdateList();
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_ORecipe.OZero.OHead1.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkZeroStandardPointHead1.Text);
                this.m_ORecipe.OZero.OHead2.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkZeroStandardPointHead2.Text);
                this.m_ORecipe.OZero.OHead3.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkZeroStandardPointHead3.Text);
                this.m_ORecipe.OZero.OHead4.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkZeroStandardPointHead4.Text);
                this.m_ORecipe.ONinety.OHead1.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkNinetyStandardPointHead1.Text);
                this.m_ORecipe.ONinety.OHead2.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkNinetyStandardPointHead2.Text);
                this.m_ORecipe.ONinety.OHead3.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkNinetyStandardPointHead3.Text);
                this.m_ORecipe.ONinety.OHead4.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), this.ChkNinetyStandardPointHead4.Text);

                this.m_ORecipe.OZero.F64AlignmentLimitHead1 = Convert.ToDouble(this.NudZeroAlignmentLimitHead1.Value);
                this.m_ORecipe.OZero.F64AlignmentLimitHead2 = Convert.ToDouble(this.NudZeroAlignmentLimitHead2.Value);
                this.m_ORecipe.OZero.F64AlignmentLimitHead3 = Convert.ToDouble(this.NudZeroAlignmentLimitHead3.Value);
                this.m_ORecipe.OZero.F64AlignmentLimitHead4 = Convert.ToDouble(this.NudZeroAlignmentLimitHead4.Value);
                this.m_ORecipe.OZero.F64AlignmentLimitY = Convert.ToDouble(this.NudZeroAlignmentLimitY.Value);
                this.m_ORecipe.OZero.F64AlignmentLimitT = Convert.ToDouble(this.NudZeroAlignmentLimitT.Value);
                this.m_ORecipe.OZero.F64AlignmentProductLength = Convert.ToDouble(this.NudZeroLength.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitHead1 = Convert.ToDouble(this.NudNinetyAlignmentLimitHead1.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitHead2 = Convert.ToDouble(this.NudNinetyAlignmentLimitHead2.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitHead3 = Convert.ToDouble(this.NudNinetyAlignmentLimitHead3.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitHead4 = Convert.ToDouble(this.NudNinetyAlignmentLimitHead4.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitY = Convert.ToDouble(this.NudNinetyAlignmentLimitY.Value);
                this.m_ORecipe.ONinety.F64AlignmentLimitT = Convert.ToDouble(this.NudNinetyAlignmentLimitT.Value);
                this.m_ORecipe.ONinety.F64AlignmentProductLength = Convert.ToDouble(this.NudNinetyLength.Value);

                this.m_ORecipe.OZero.F64DirectCutLimitHead1 = Convert.ToDouble(this.NudZeroDirectCutLimitHead1.Value);
                this.m_ORecipe.OZero.F64DirectCutLimitHead2 = Convert.ToDouble(this.NudZeroDirectCutLimitHead2.Value);
                this.m_ORecipe.OZero.F64DirectCutLimitHead3 = Convert.ToDouble(this.NudZeroDirectCutLimitHead3.Value);
                this.m_ORecipe.OZero.F64DirectCutLimitHead4 = Convert.ToDouble(this.NudZeroDirectCutLimitHead4.Value);
                this.m_ORecipe.ONinety.F64DirectCutLimitHead1 = Convert.ToDouble(this.NudNinetyDirectCutLimitHead1.Value);
                this.m_ORecipe.ONinety.F64DirectCutLimitHead2 = Convert.ToDouble(this.NudNinetyDirectCutLimitHead2.Value);
                this.m_ORecipe.ONinety.F64DirectCutLimitHead3 = Convert.ToDouble(this.NudNinetyDirectCutLimitHead3.Value);
                this.m_ORecipe.ONinety.F64DirectCutLimitHead4 = Convert.ToDouble(this.NudNinetyDirectCutLimitHead4.Value);

                this.m_ORecipe.OZero.F64CrossCutLimitHead1 = Convert.ToDouble(this.NudZeroCrossCutLimitHead1.Value);
                this.m_ORecipe.OZero.F64CrossCutLimitHead2 = Convert.ToDouble(this.NudZeroCrossCutLimitHead2.Value);
                this.m_ORecipe.OZero.F64CrossCutLimitHead3 = Convert.ToDouble(this.NudZeroCrossCutLimitHead3.Value);
                this.m_ORecipe.OZero.F64CrossCutLimitHead4 = Convert.ToDouble(this.NudZeroCrossCutLimitHead4.Value);
                this.m_ORecipe.ONinety.F64CrossCutLimitHead1 = Convert.ToDouble(this.NudNinetyCrossCutLimitHead1.Value);
                this.m_ORecipe.ONinety.F64CrossCutLimitHead2 = Convert.ToDouble(this.NudNinetyCrossCutLimitHead2.Value);
                this.m_ORecipe.ONinety.F64CrossCutLimitHead3 = Convert.ToDouble(this.NudNinetyCrossCutLimitHead3.Value);
                this.m_ORecipe.ONinety.F64CrossCutLimitHead4 = Convert.ToDouble(this.NudNinetyCrossCutLimitHead4.Value);
                this.m_ORecipe.Save();

                if (this.m_EEdit == EEDIT.ADD)
                {
                    int I32RowIndex = CDB.It[CDB.TABLE_RECIPE_LIST].InsertRow();
                    CDB.It[CDB.TABLE_RECIPE_LIST].Update(I32RowIndex, CDB.RECIPE_LIST_ID, this.m_ORecipe.I32ID);
                    CDB.It[CDB.TABLE_RECIPE_LIST].Update(I32RowIndex, CDB.RECIPE_LIST_NAME, this.m_ORecipe.StrName);

                    I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].InsertRow();
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ID, this.m_ORecipe.I32ID);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_RECIPE, this.m_ORecipe.StrName);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ANGLE, this.m_ORecipe.OZero.EAngle.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, this.m_ORecipe.OZero.OHead1.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, this.m_ORecipe.OZero.OHead2.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, this.m_ORecipe.OZero.OHead3.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, this.m_ORecipe.OZero.OHead4.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64AlignmentLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64AlignmentLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64AlignmentLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64AlignmentLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, this.m_ORecipe.OZero.F64AlignmentLimitY);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, this.m_ORecipe.OZero.F64AlignmentLimitT);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_WIDTH, this.m_ORecipe.OZero.F64AlignmentProductLength);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64DirectCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64DirectCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64DirectCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64DirectCutLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64CrossCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64CrossCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64CrossCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64CrossCutLimitHead4);

                    I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].InsertRow();
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ID, this.m_ORecipe.I32ID);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_RECIPE, this.m_ORecipe.StrName);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ANGLE, this.m_ORecipe.ONinety.EAngle.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, this.m_ORecipe.ONinety.OHead1.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, this.m_ORecipe.ONinety.OHead2.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, this.m_ORecipe.ONinety.OHead3.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, this.m_ORecipe.ONinety.OHead4.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64AlignmentLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64AlignmentLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64AlignmentLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64AlignmentLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, this.m_ORecipe.ONinety.F64AlignmentLimitY);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, this.m_ORecipe.ONinety.F64AlignmentLimitT);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_HEIGHT, this.m_ORecipe.ONinety.F64AlignmentProductLength);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64DirectCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64DirectCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64DirectCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64DirectCutLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64CrossCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64CrossCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64CrossCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64CrossCutLimitHead4);

                    CDB.It[CDB.TABLE_RECIPE_LIST].Commit();
                    CDB.It[CDB.TABLE_RECIPE_INFO].Commit();

                    CRecipeManager.It.LstORecipe.Add(new CRecipe(this.m_ORecipe));

                    this.UpdateList();
                }
                else if (this.m_EEdit == EEDIT.MODIFY)
                {
                    int I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].SelectRowIndex
                    (
                        new string[] { CDB.RECIPE_INFO_ID, CDB.RECIPE_INFO_RECIPE, CDB.RECIPE_INFO_ANGLE },
                        new object[] { this.m_ORecipe.I32ID, this.m_ORecipe.StrName, this.m_ORecipe.OZero.EAngle.ToString() }
                    );
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, this.m_ORecipe.OZero.OHead1.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, this.m_ORecipe.OZero.OHead2.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, this.m_ORecipe.OZero.OHead3.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, this.m_ORecipe.OZero.OHead4.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64AlignmentLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64AlignmentLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64AlignmentLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64AlignmentLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, this.m_ORecipe.OZero.F64AlignmentLimitY);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, this.m_ORecipe.OZero.F64AlignmentLimitT);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_WIDTH, this.m_ORecipe.OZero.F64AlignmentProductLength);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64DirectCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64DirectCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64DirectCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64DirectCutLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, this.m_ORecipe.OZero.F64CrossCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, this.m_ORecipe.OZero.F64CrossCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, this.m_ORecipe.OZero.F64CrossCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, this.m_ORecipe.OZero.F64CrossCutLimitHead4);

                    I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].SelectRowIndex
                    (
                        new string[] { CDB.RECIPE_INFO_ID, CDB.RECIPE_INFO_RECIPE, CDB.RECIPE_INFO_ANGLE },
                        new object[] { this.m_ORecipe.I32ID, this.m_ORecipe.StrName, this.m_ORecipe.ONinety.EAngle.ToString() }
                    );
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, this.m_ORecipe.ONinety.OHead1.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, this.m_ORecipe.ONinety.OHead2.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, this.m_ORecipe.ONinety.OHead3.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, this.m_ORecipe.ONinety.OHead4.EStdPoint.ToString());
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64AlignmentLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64AlignmentLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64AlignmentLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64AlignmentLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, this.m_ORecipe.ONinety.F64AlignmentLimitY);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, this.m_ORecipe.ONinety.F64AlignmentLimitT);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_HEIGHT, this.m_ORecipe.ONinety.F64AlignmentProductLength);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64DirectCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64DirectCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64DirectCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64DirectCutLimitHead4);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, this.m_ORecipe.ONinety.F64CrossCutLimitHead1);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, this.m_ORecipe.ONinety.F64CrossCutLimitHead2);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, this.m_ORecipe.ONinety.F64CrossCutLimitHead3);
                    CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, this.m_ORecipe.ONinety.F64CrossCutLimitHead4);

                    CDB.It[CDB.TABLE_RECIPE_INFO].Commit();


                    for (int _Index = 0; _Index < CRecipeManager.It.LstORecipe.Count; _Index++)
                    {
                        if (CRecipeManager.It.LstORecipe[_Index].I32ID == this.m_ORecipe.I32ID)
                        {
                            CRecipeManager.It.LstORecipe[_Index] = new CRecipe(this.m_ORecipe);
                            break;
                        }
                    }

                    if ((CTool.It.ORecipe != null) && (CTool.It.ORecipe.I32ID == this.m_ORecipe.I32ID))
                    {
                        CTool.It.ORecipe = new CRecipe(this.m_ORecipe);
                    }
                }

                this.BtnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_BPreventEvent = true;


                if (this.m_ORecipe != null)
                {
                    this.m_ORecipe.Dispose();
                    this.m_ORecipe = null;
                }

                this.m_EEdit = EEDIT.NONE;
                this.m_EAngle = ESTAGE_ANGLE.NONE;
                this.m_EView = EVIEW.NONE;
                this.m_ETool = ETOOL.NONE;
                this.m_EMark = EMARK.NONE;

                if (this.LtbRecipe.Items.Count > 0)
                {
                    this.NudID.Value = 1;
                    this.TxtName.Text = String.Empty;
                    this.BtnAdd.BackColor = Color.SteelBlue;
                    this.BtnModify.BackColor = Color.SteelBlue;
                    this.BtnDelete.BackColor = Color.SteelBlue;
                    this.BtnSave.BackColor = SystemColors.Control;
                    this.BtnCancel.BackColor = SystemColors.Control;
                    this.BtnRename.BackColor = Color.SteelBlue;
                    this.BtnCopy.BackColor = Color.SteelBlue;
                    this.BtnApply.BackColor = Color.SteelBlue;
                    this.NudID.Enabled = true;
                    this.TxtName.Enabled = true;
                    this.BtnAdd.Enabled = true;
                    this.BtnModify.Enabled = true;
                    this.BtnDelete.Enabled = true;
                    this.BtnSave.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.BtnRename.Enabled = true;
                    this.BtnCopy.Enabled = true;
                    this.BtnApply.Enabled = true;
                }
                else
                {
                    this.NudID.Value = 1;
                    this.TxtName.Text = String.Empty;
                    this.BtnAdd.BackColor = Color.SteelBlue;
                    this.BtnModify.BackColor = SystemColors.Control;
                    this.BtnDelete.BackColor = SystemColors.Control;
                    this.BtnSave.BackColor = SystemColors.Control;
                    this.BtnCancel.BackColor = SystemColors.Control;
                    this.BtnRename.BackColor = SystemColors.Control;
                    this.BtnCopy.BackColor = SystemColors.Control;
                    this.BtnApply.BackColor = SystemColors.Control;
                    this.NudID.Enabled = true;
                    this.TxtName.Enabled = true;
                    this.BtnAdd.Enabled = true;
                    this.BtnModify.Enabled = false;
                    this.BtnDelete.Enabled = false;
                    this.BtnSave.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.BtnRename.Enabled = false;
                    this.BtnCopy.Enabled = false;
                    this.BtnApply.Enabled = false;
                }

                this.BtnStageAngleZero.BackColor = SystemColors.Control;
                this.BtnStageAngleNinety.BackColor = SystemColors.Control;
                this.BtnViewHead1.BackColor = SystemColors.Control;
                this.BtnViewHead2.BackColor = SystemColors.Control;
                this.BtnViewHead3.BackColor = SystemColors.Control;
                this.BtnViewHead4.BackColor = SystemColors.Control;
                this.BtnToolMark.BackColor = SystemColors.Control;
                this.BtnToolDirectCut.BackColor = SystemColors.Control;
                this.BtnToolCrossCut.BackColor = SystemColors.Control;
                this.BtnMark1.BackColor = SystemColors.Control;
                this.BtnMark2.BackColor = SystemColors.Control;
                this.BtnMark3.BackColor = SystemColors.Control;
                this.BtnMark4.BackColor = SystemColors.Control;
                this.BtnMark5.BackColor = SystemColors.Control;
                this.BtnImage.BackColor = SystemColors.Control;
                this.BtnDetail.BackColor = SystemColors.Control;
                this.BtnEntire.BackColor = SystemColors.Control;
                this.BtnStageAngleZero.Enabled = false;
                this.BtnStageAngleNinety.Enabled = false;
                this.BtnViewHead1.Enabled = false;
                this.BtnViewHead2.Enabled = false;
                this.BtnViewHead3.Enabled = false;
                this.BtnViewHead4.Enabled = false;
                this.BtnToolMark.Enabled = false;
                this.BtnToolDirectCut.Enabled = false;
                this.BtnToolCrossCut.Enabled = false;
                this.BtnMark1.Enabled = false;
                this.BtnMark2.Enabled = false;
                this.BtnMark3.Enabled = false;
                this.BtnMark4.Enabled = false;
                this.BtnMark5.Enabled = false;
                this.BtnImage.Enabled = false;
                this.BtnDetail.Enabled = false;
                this.BtnEntire.Enabled = false;

                this.CdpMark1.Image = null;
                this.CdpMark2.Image = null;
                this.CdpMark3.Image = null;
                this.CdpMark4.Image = null;
                this.CdpMark5.Image = null;

                this.CpeMark.Subject = null;
                this.CceEdge.Subject = null;
                this.CpeMark.Enabled = false;
                this.CceEdge.Enabled = false;
                this.PnlMarkEdit.BringToFront();

                this.ChkZeroStandardPointHead1.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead2.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead3.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead4.Text = STANDARD_POINT_LEFT;
                this.ChkZeroStandardPointHead1.Checked = false;
                this.ChkZeroStandardPointHead2.Checked = false;
                this.ChkZeroStandardPointHead3.Checked = false;
                this.ChkZeroStandardPointHead4.Checked = false;
                this.ChkZeroStandardPointHead1.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead2.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead3.BackColor = SystemColors.Control;
                this.ChkZeroStandardPointHead4.BackColor = SystemColors.Control;
                this.NudZeroAlignmentLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitHead4.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitY.Controls[1].Text = String.Empty;
                this.NudZeroAlignmentLimitT.Controls[1].Text = String.Empty;
                this.NudZeroLength.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroDirectCutLimitHead4.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudZeroCrossCutLimitHead4.Controls[1].Text = String.Empty;
                this.ChkZeroStandardPointHead1.Enabled = false;
                this.ChkZeroStandardPointHead2.Enabled = false;
                this.ChkZeroStandardPointHead3.Enabled = false;
                this.ChkZeroStandardPointHead4.Enabled = false;
                this.NudZeroAlignmentLimitHead1.Enabled = false;
                this.NudZeroAlignmentLimitHead2.Enabled = false;
                this.NudZeroAlignmentLimitHead3.Enabled = false;
                this.NudZeroAlignmentLimitHead4.Enabled = false;
                this.NudZeroAlignmentLimitY.Enabled = false;
                this.NudZeroLength.Enabled = false;
                this.NudZeroAlignmentLimitT.Enabled = false;
                this.NudZeroDirectCutLimitHead1.Enabled = false;
                this.NudZeroDirectCutLimitHead2.Enabled = false;
                this.NudZeroDirectCutLimitHead3.Enabled = false;
                this.NudZeroDirectCutLimitHead4.Enabled = false;
                this.NudZeroCrossCutLimitHead1.Enabled = false;
                this.NudZeroCrossCutLimitHead2.Enabled = false;
                this.NudZeroCrossCutLimitHead3.Enabled = false;
                this.NudZeroCrossCutLimitHead4.Enabled = false;

                this.ChkNinetyStandardPointHead1.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead2.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead3.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead4.Text = STANDARD_POINT_LEFT;
                this.ChkNinetyStandardPointHead1.Checked = false;
                this.ChkNinetyStandardPointHead2.Checked = false;
                this.ChkNinetyStandardPointHead3.Checked = false;
                this.ChkNinetyStandardPointHead4.Checked = false;
                this.ChkNinetyStandardPointHead1.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead2.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead3.BackColor = SystemColors.Control;
                this.ChkNinetyStandardPointHead4.BackColor = SystemColors.Control;
                this.NudNinetyAlignmentLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitHead4.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitY.Controls[1].Text = String.Empty;
                this.NudNinetyAlignmentLimitT.Controls[1].Text = String.Empty;
                this.NudNinetyLength.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyDirectCutLimitHead4.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead1.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead2.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead3.Controls[1].Text = String.Empty;
                this.NudNinetyCrossCutLimitHead4.Controls[1].Text = String.Empty;
                this.ChkNinetyStandardPointHead1.Enabled = false;
                this.ChkNinetyStandardPointHead2.Enabled = false;
                this.ChkNinetyStandardPointHead3.Enabled = false;
                this.ChkNinetyStandardPointHead4.Enabled = false;
                this.NudNinetyAlignmentLimitHead1.Enabled = false;
                this.NudNinetyAlignmentLimitHead2.Enabled = false;
                this.NudNinetyAlignmentLimitHead3.Enabled = false;
                this.NudNinetyAlignmentLimitHead4.Enabled = false;
                this.NudNinetyAlignmentLimitY.Enabled = false;
                this.NudNinetyAlignmentLimitT.Enabled = false;
                this.NudNinetyLength.Enabled = false;
                this.NudNinetyDirectCutLimitHead1.Enabled = false;
                this.NudNinetyDirectCutLimitHead2.Enabled = false;
                this.NudNinetyDirectCutLimitHead3.Enabled = false;
                this.NudNinetyDirectCutLimitHead4.Enabled = false;
                this.NudNinetyCrossCutLimitHead1.Enabled = false;
                this.NudNinetyCrossCutLimitHead2.Enabled = false;
                this.NudNinetyCrossCutLimitHead3.Enabled = false;
                this.NudNinetyCrossCutLimitHead4.Enabled = false;

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


        private void BtnRename_Click(object sender, EventArgs e)
        {
            try
            {
                string StrItem = (String)this.LtbRecipe.SelectedItem;
                if (string.IsNullOrEmpty(StrItem) == true)
                {
                    CMsgBox.Warning("Please select a recipe for renaming");
                    return;
                }
                int I32ID = Convert.ToInt32(StrItem.Substring(0, StrItem.IndexOf(".")));
                if (CTool.It.ORecipe != null && CTool.It.ORecipe.I32ID == I32ID)
                {
                    CMsgBox.Warning("Cannot rename a recipe that is currently on the use.");
                    return;
                }
                string StrNewName = this.TxtName.Text.Trim();
                if (String.IsNullOrEmpty(StrNewName) == true)
                {
                    CMsgBox.Warning("Please write a new name in the 'Name' section.");
                    return;
                }

                if (CMsgBox.OKCancel("Do you want to rename '" + StrItem + "'recipe?") == DialogResult.OK)
                {
                    int I32RowIndex = CDB.It[CDB.TABLE_RECIPE_LIST].SelectRowIndex(CDB.RECIPE_LIST_ID, I32ID);
                    CDB.It[CDB.TABLE_RECIPE_LIST].Update(I32RowIndex, CDB.RECIPE_LIST_NAME, StrNewName);

                    List<int> LstI32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].SelectRowIndexs(CDB.RECIPE_INFO_ID, I32ID);
                    for (int _Index = 0; _Index < LstI32RowIndex.Count; _Index++)
                    {
                        CDB.It[CDB.TABLE_RECIPE_INFO].Update(LstI32RowIndex[_Index], CDB.RECIPE_INFO_RECIPE, StrNewName);
                    }

                    CDB.It[CDB.TABLE_RECIPE_LIST].Commit();
                    CDB.It[CDB.TABLE_RECIPE_INFO].Commit();


                    for (int _Index = 0; _Index < CRecipeManager.It.LstORecipe.Count; _Index++)
                    {
                        if (CRecipeManager.It.LstORecipe[_Index].I32ID != I32ID) continue;

                        CRecipeManager.It.LstORecipe[_Index].StrName = StrNewName;
                        break;
                    }

                    this.UpdateList();
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try 
            {
                int I32ID = Convert.ToInt32(this.NudID.Value);
                foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                {
                    if (_Item.I32ID != I32ID) continue;

                    CMsgBox.Warning("A recipe with the same ID already exists.");
                    return;
                }
                string StrName = this.TxtName.Text.Trim();
                if (String.IsNullOrEmpty(StrName) == true)
                {
                    CMsgBox.Warning("Please do not leave the 'Name' section empty.");
                    return;
                }
                string StrItem = (string)this.LtbRecipe.SelectedItem;
                if (String.IsNullOrEmpty(StrItem) == true)
                {
                    CMsgBox.Warning("Please select a recipe to copy.");
                    return;
                }


                int I32SourceID = Convert.ToInt32(StrItem.Substring(0, StrItem.IndexOf(".")));
                CRecipe ORecipe = null;
                foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                {
                    if (_Item.I32ID != I32SourceID) continue;

                    ORecipe = new CRecipe(I32ID, StrName);
                    ORecipe.Copy(_Item);
                    break;
                }


                int I32RowIndex = CDB.It[CDB.TABLE_RECIPE_LIST].InsertRow();
                CDB.It[CDB.TABLE_RECIPE_LIST].Update(I32RowIndex, CDB.RECIPE_LIST_ID, ORecipe.I32ID);
                CDB.It[CDB.TABLE_RECIPE_LIST].Update(I32RowIndex, CDB.RECIPE_LIST_NAME, ORecipe.StrName);

                I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].InsertRow();
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ID, ORecipe.I32ID);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_RECIPE, ORecipe.StrName);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ANGLE, ORecipe.OZero.EAngle.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, ORecipe.OZero.OHead1.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, ORecipe.OZero.OHead2.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, ORecipe.OZero.OHead3.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, ORecipe.OZero.OHead4.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, ORecipe.OZero.F64AlignmentLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, ORecipe.OZero.F64AlignmentLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, ORecipe.OZero.F64AlignmentLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, ORecipe.OZero.F64AlignmentLimitHead4);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, ORecipe.OZero.F64AlignmentLimitY);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, ORecipe.OZero.F64AlignmentLimitT);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_WIDTH, ORecipe.OZero.F64AlignmentProductLength);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, ORecipe.OZero.F64DirectCutLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, ORecipe.OZero.F64DirectCutLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, ORecipe.OZero.F64DirectCutLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, ORecipe.OZero.F64DirectCutLimitHead4);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, ORecipe.OZero.F64CrossCutLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, ORecipe.OZero.F64CrossCutLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, ORecipe.OZero.F64CrossCutLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, ORecipe.OZero.F64CrossCutLimitHead4);

                I32RowIndex = CDB.It[CDB.TABLE_RECIPE_INFO].InsertRow();
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ID, ORecipe.I32ID);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_RECIPE, ORecipe.StrName);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ANGLE, ORecipe.ONinety.EAngle.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD1, ORecipe.ONinety.OHead1.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD2, ORecipe.ONinety.OHead2.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD3, ORecipe.ONinety.OHead3.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_STANDARD_POINT_HEAD4, ORecipe.ONinety.OHead4.EStdPoint.ToString());
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1, ORecipe.ONinety.F64AlignmentLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2, ORecipe.ONinety.F64AlignmentLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3, ORecipe.ONinety.F64AlignmentLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4, ORecipe.ONinety.F64AlignmentLimitHead4);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y, ORecipe.ONinety.F64AlignmentLimitY);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T, ORecipe.ONinety.F64AlignmentLimitT);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_PRODUCT_HEIGHT, ORecipe.ONinety.F64AlignmentProductLength);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1, ORecipe.ONinety.F64DirectCutLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2, ORecipe.ONinety.F64DirectCutLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3, ORecipe.ONinety.F64DirectCutLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4, ORecipe.ONinety.F64DirectCutLimitHead4);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1, ORecipe.ONinety.F64CrossCutLimitHead1);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2, ORecipe.ONinety.F64CrossCutLimitHead2);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3, ORecipe.ONinety.F64CrossCutLimitHead3);
                CDB.It[CDB.TABLE_RECIPE_INFO].Update(I32RowIndex, CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4, ORecipe.ONinety.F64CrossCutLimitHead4);

                CDB.It[CDB.TABLE_RECIPE_LIST].Commit();
                CDB.It[CDB.TABLE_RECIPE_INFO].Commit();


                CRecipeManager.It.LstORecipe.Add(ORecipe);
                this.UpdateList();
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
                string StrItem = (string)this.LtbRecipe.SelectedItem;
                if (String.IsNullOrEmpty(StrItem) == true)
                {
                    CMsgBox.Warning("Please select a recipe to apply");
                    return;
                }

                int I32ID = Convert.ToInt32(StrItem.Substring(0, StrItem.IndexOf(".")));
                if ((CTool.It.ORecipe == null) || (CTool.It.ORecipe.I32ID != I32ID))
                {
                    foreach (CRecipe _Item in CRecipeManager.It.LstORecipe)
                    {
                        if (_Item.I32ID != I32ID) continue;

                        CTool.It.ORecipe = new CRecipe(_Item);
                        CMotionController.It.OPLC.I32VisionRecipeID = _Item.I32ID;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnStageAngle_Click(object sender, EventArgs e)
        {
            try
            {
                ESTAGE_ANGLE EAngle = (ESTAGE_ANGLE)Enum.Parse(typeof(ESTAGE_ANGLE), (string)((Control)sender).Tag);
                if (this.m_EAngle == EAngle) return;

                this.m_EAngle = EAngle;
                this.DisplayTool();
                this.DisplayMarks();

                if (this.m_EAngle == ESTAGE_ANGLE.ZERO)
                {
                    this.BtnStageAngleZero.BackColor = Color.ForestGreen;
                    this.BtnStageAngleNinety.BackColor = Color.SteelBlue;
                }
                else if (this.m_EAngle == ESTAGE_ANGLE.NINETY)
                {
                    this.BtnStageAngleZero.BackColor = Color.SteelBlue;
                    this.BtnStageAngleNinety.BackColor = Color.ForestGreen;
                }
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
                EVIEW EView = (EVIEW)Enum.Parse(typeof(EVIEW), (string)((Control)sender).Tag);
                if (this.m_EView == EView) return;

                this.m_EView = EView;
                this.DisplayTool();
                this.DisplayMarks();

                if (this.m_EView == EVIEW.HEAD1)
                {
                    this.BtnViewHead1.BackColor = Color.ForestGreen;
                    this.BtnViewHead2.BackColor = Color.SteelBlue;
                    this.BtnViewHead3.BackColor = Color.SteelBlue;
                    this.BtnViewHead4.BackColor = Color.SteelBlue;
                }
                else if (this.m_EView == EVIEW.HEAD2)
                {
                    this.BtnViewHead1.BackColor = Color.SteelBlue;
                    this.BtnViewHead2.BackColor = Color.ForestGreen;
                    this.BtnViewHead3.BackColor = Color.SteelBlue;
                    this.BtnViewHead4.BackColor = Color.SteelBlue;
                }
                else if (this.m_EView == EVIEW.HEAD3)
                {
                    this.BtnViewHead1.BackColor = Color.SteelBlue;
                    this.BtnViewHead2.BackColor = Color.SteelBlue;
                    this.BtnViewHead3.BackColor = Color.ForestGreen;
                    this.BtnViewHead4.BackColor = Color.SteelBlue;
                }
                else if (this.m_EView == EVIEW.HEAD4)
                {
                    this.BtnViewHead1.BackColor = Color.SteelBlue;
                    this.BtnViewHead2.BackColor = Color.SteelBlue;
                    this.BtnViewHead3.BackColor = Color.SteelBlue;
                    this.BtnViewHead4.BackColor = Color.ForestGreen;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnTool_Click(object sender, EventArgs e)
        {
            try
            {
                ETOOL ETool = (ETOOL)Enum.Parse(typeof(ETOOL), ((Control)sender).Tag.ToString());
                if (ETool == this.m_ETool) return;

                this.m_ETool = ETool;
                this.m_EMark = EMARK.INDEX1;
                this.DisplayTool();
                this.DisplayMarks();

                if (this.m_ETool == ETOOL.MARK)
                {
                    this.BtnToolMark.BackColor = Color.ForestGreen;
                    this.BtnToolDirectCut.BackColor = Color.SteelBlue;
                    this.BtnToolCrossCut.BackColor = Color.SteelBlue;

                    this.BtnMark1.BackColor = Color.ForestGreen;
                    this.BtnMark2.BackColor = Color.SteelBlue;
                    this.BtnMark3.BackColor = Color.SteelBlue;
                    this.BtnMark4.BackColor = Color.SteelBlue;
                    this.BtnMark5.BackColor = Color.SteelBlue;
                    this.BtnMark1.Enabled = true;
                    this.BtnMark2.Enabled = true;
                    this.BtnMark3.Enabled = true;
                    this.BtnMark4.Enabled = true;
                    this.BtnMark5.Enabled = true;

                    this.BtnDetail.BackColor = Color.SteelBlue;
                    this.BtnDetail.Enabled = true;
                }
                else if (this.m_ETool == ETOOL.DIRECT_CUT)
                {
                    this.BtnToolMark.BackColor = Color.SteelBlue;
                    this.BtnToolDirectCut.BackColor = Color.ForestGreen;
                    this.BtnToolCrossCut.BackColor = Color.SteelBlue;

                    this.BtnMark1.BackColor = SystemColors.Control;
                    this.BtnMark2.BackColor = SystemColors.Control;
                    this.BtnMark3.BackColor = SystemColors.Control;
                    this.BtnMark4.BackColor = SystemColors.Control;
                    this.BtnMark5.BackColor = SystemColors.Control;
                    this.BtnMark1.Enabled = false;
                    this.BtnMark2.Enabled = false;
                    this.BtnMark3.Enabled = false;
                    this.BtnMark4.Enabled = false;
                    this.BtnMark5.Enabled = false;

                    this.BtnDetail.BackColor = SystemColors.Control;
                    this.BtnDetail.Enabled = false;
                }
                else if (this.m_ETool == ETOOL.CROSS_CUT)
                {
                    this.BtnToolMark.BackColor = Color.SteelBlue;
                    this.BtnToolDirectCut.BackColor = Color.SteelBlue;
                    this.BtnToolCrossCut.BackColor = Color.ForestGreen;

                    this.BtnMark1.BackColor = SystemColors.Control;
                    this.BtnMark2.BackColor = SystemColors.Control;
                    this.BtnMark3.BackColor = SystemColors.Control;
                    this.BtnMark4.BackColor = SystemColors.Control;
                    this.BtnMark5.BackColor = SystemColors.Control;
                    this.BtnMark1.Enabled = false;
                    this.BtnMark2.Enabled = false;
                    this.BtnMark3.Enabled = false;
                    this.BtnMark4.Enabled = false;
                    this.BtnMark5.Enabled = false;

                    this.BtnDetail.BackColor = SystemColors.Control;
                    this.BtnDetail.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnIndex_Click(object sender, EventArgs e)
        {

            try
            {
                EMARK EMark = (EMARK)Enum.Parse(typeof(EMARK), ((Control)sender).Tag.ToString());
                if (EMark == this.m_EMark) return;

                this.m_EMark = EMark;
                this.DisplayTool();
                this.DisplayMarks();

                switch (this.m_EMark)
                {
                    case EMARK.INDEX1:
                        this.BtnMark1.BackColor = Color.ForestGreen;
                        this.BtnMark2.BackColor = Color.SteelBlue;
                        this.BtnMark3.BackColor = Color.SteelBlue;
                        this.BtnMark4.BackColor = Color.SteelBlue;
                        this.BtnMark5.BackColor = Color.SteelBlue;
                        break;

                    case EMARK.INDEX2:
                        this.BtnMark1.BackColor = Color.SteelBlue;
                        this.BtnMark2.BackColor = Color.ForestGreen;
                        this.BtnMark3.BackColor = Color.SteelBlue;
                        this.BtnMark4.BackColor = Color.SteelBlue;
                        this.BtnMark5.BackColor = Color.SteelBlue;
                        break;

                    case EMARK.INDEX3:
                        this.BtnMark1.BackColor = Color.SteelBlue;
                        this.BtnMark2.BackColor = Color.SteelBlue;
                        this.BtnMark3.BackColor = Color.ForestGreen;
                        this.BtnMark4.BackColor = Color.SteelBlue;
                        this.BtnMark5.BackColor = Color.SteelBlue;
                        break;

                    case EMARK.INDEX4:
                        this.BtnMark1.BackColor = Color.SteelBlue;
                        this.BtnMark2.BackColor = Color.SteelBlue;
                        this.BtnMark3.BackColor = Color.SteelBlue;
                        this.BtnMark4.BackColor = Color.ForestGreen;
                        this.BtnMark5.BackColor = Color.SteelBlue;
                        break;

                    case EMARK.INDEX5:
                        this.BtnMark1.BackColor = Color.SteelBlue;
                        this.BtnMark2.BackColor = Color.SteelBlue;
                        this.BtnMark3.BackColor = Color.SteelBlue;
                        this.BtnMark4.BackColor = Color.SteelBlue;
                        this.BtnMark5.BackColor = Color.ForestGreen;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnImage_Click(object sender, EventArgs e)
        {
            try
            {
                CogImage8Grey OImage = null;
                switch (this.m_EView)
                {
                    case EVIEW.HEAD1:
                        OImage = CTool.It.OAlignmentTool.OHead1.ODisplayer.OCurrentImage;
                        break;

                    case EVIEW.HEAD2:
                        OImage = CTool.It.OAlignmentTool.OHead2.ODisplayer.OCurrentImage;
                        break;

                    case EVIEW.HEAD3:
                        OImage = CTool.It.OAlignmentTool.OHead3.ODisplayer.OCurrentImage;
                        break;

                    case EVIEW.HEAD4:
                        OImage = CTool.It.OAlignmentTool.OHead4.ODisplayer.OCurrentImage;
                        break;
                }

                if (this.m_ETool == ETOOL.MARK) this.CpeMark.Subject.InputImage = OImage;
                else this.CceEdge.Subject.InputImage = OImage;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.CpeMark.Subject == null) || (this.CpeMark.Subject.Pattern.TrainImage == null))
                {
                    CMsgBox.Warning("Please set train image to current tool");
                    return;
                }

                frmMarkDetail OWindow = new frmMarkDetail();
                OWindow.OImage = this.CpeMark.Subject.Pattern.TrainImage;
                OWindow.OBound = this.CpeMark.Subject.Pattern.TrainRegion.EnclosingRectangle(CogCopyShapeConstants.All);
                if (OWindow.ShowDialog() == DialogResult.OK)
                {
                    this.CpeMark.Subject.Pattern.TrainRegion.FitToBoundingBox(OWindow.OBound);
                    this.CpeMark.Subject.Pattern.Origin.TranslationX = OWindow.OBound.CenterX;
                    this.CpeMark.Subject.Pattern.Origin.TranslationY = OWindow.OBound.CenterY;
                    this.CpeMark.Subject.Pattern.Train();
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnEntire_Click(object sender, EventArgs e)
        {
            try
            {
                frmOtherRecipeSelect OWindow = new frmOtherRecipeSelect();
                OWindow.EAngle = this.m_EAngle;
                OWindow.EView = this.m_EView;
                if (OWindow.ShowDialog() != DialogResult.OK) return;

                if (this.m_ETool == ETOOL.MARK)
                {
                    if (this.m_EMark == EMARK.INDEX1)
                    {
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                        {
                            this.m_ORecipe.OZero.OHead1.OMarkTool1.Dispose();
                            this.m_ORecipe.OZero.OHead1.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                        {
                            this.m_ORecipe.OZero.OHead2.OMarkTool1.Dispose();
                            this.m_ORecipe.OZero.OHead2.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                        {
                            this.m_ORecipe.OZero.OHead3.OMarkTool1.Dispose();
                            this.m_ORecipe.OZero.OHead3.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                        {
                            this.m_ORecipe.OZero.OHead4.OMarkTool1.Dispose();
                            this.m_ORecipe.OZero.OHead4.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                        {
                            this.m_ORecipe.ONinety.OHead1.OMarkTool1.Dispose();
                            this.m_ORecipe.ONinety.OHead1.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                        {
                            this.m_ORecipe.ONinety.OHead2.OMarkTool1.Dispose();
                            this.m_ORecipe.ONinety.OHead2.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                        {
                            this.m_ORecipe.ONinety.OHead3.OMarkTool1.Dispose();
                            this.m_ORecipe.ONinety.OHead3.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                        {
                            this.m_ORecipe.ONinety.OHead4.OMarkTool1.Dispose();
                            this.m_ORecipe.ONinety.OHead4.OMarkTool1 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                    }
                    else if (this.m_EMark == EMARK.INDEX2)
                    {
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                        {
                            this.m_ORecipe.OZero.OHead1.OMarkTool2.Dispose();
                            this.m_ORecipe.OZero.OHead1.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                        {
                            this.m_ORecipe.OZero.OHead2.OMarkTool2.Dispose();
                            this.m_ORecipe.OZero.OHead2.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                        {
                            this.m_ORecipe.OZero.OHead3.OMarkTool2.Dispose();
                            this.m_ORecipe.OZero.OHead3.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                        {
                            this.m_ORecipe.OZero.OHead4.OMarkTool2.Dispose();
                            this.m_ORecipe.OZero.OHead4.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                        {
                            this.m_ORecipe.ONinety.OHead1.OMarkTool2.Dispose();
                            this.m_ORecipe.ONinety.OHead1.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                        {
                            this.m_ORecipe.ONinety.OHead2.OMarkTool2.Dispose();
                            this.m_ORecipe.ONinety.OHead2.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                        {
                            this.m_ORecipe.ONinety.OHead3.OMarkTool2.Dispose();
                            this.m_ORecipe.ONinety.OHead3.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                        {
                            this.m_ORecipe.ONinety.OHead4.OMarkTool2.Dispose();
                            this.m_ORecipe.ONinety.OHead4.OMarkTool2 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                    }
                    else if (this.m_EMark == EMARK.INDEX3)
                    {
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                        {
                            this.m_ORecipe.OZero.OHead1.OMarkTool3.Dispose();
                            this.m_ORecipe.OZero.OHead1.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                        {
                            this.m_ORecipe.OZero.OHead2.OMarkTool3.Dispose();
                            this.m_ORecipe.OZero.OHead2.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                        {
                            this.m_ORecipe.OZero.OHead3.OMarkTool3.Dispose();
                            this.m_ORecipe.OZero.OHead3.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                        {
                            this.m_ORecipe.OZero.OHead4.OMarkTool3.Dispose();
                            this.m_ORecipe.OZero.OHead4.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                        {
                            this.m_ORecipe.ONinety.OHead1.OMarkTool3.Dispose();
                            this.m_ORecipe.ONinety.OHead1.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                        {
                            this.m_ORecipe.ONinety.OHead2.OMarkTool3.Dispose();
                            this.m_ORecipe.ONinety.OHead2.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                        {
                            this.m_ORecipe.ONinety.OHead3.OMarkTool3.Dispose();
                            this.m_ORecipe.ONinety.OHead3.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                        {
                            this.m_ORecipe.ONinety.OHead4.OMarkTool3.Dispose();
                            this.m_ORecipe.ONinety.OHead4.OMarkTool3 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                    }
                    else if (this.m_EMark == EMARK.INDEX4)
                    {
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                        {
                            this.m_ORecipe.OZero.OHead1.OMarkTool4.Dispose();
                            this.m_ORecipe.OZero.OHead1.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                        {
                            this.m_ORecipe.OZero.OHead2.OMarkTool4.Dispose();
                            this.m_ORecipe.OZero.OHead2.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                        {
                            this.m_ORecipe.OZero.OHead3.OMarkTool4.Dispose();
                            this.m_ORecipe.OZero.OHead3.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                        {
                            this.m_ORecipe.OZero.OHead4.OMarkTool4.Dispose();
                            this.m_ORecipe.OZero.OHead4.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                        {
                            this.m_ORecipe.ONinety.OHead1.OMarkTool4.Dispose();
                            this.m_ORecipe.ONinety.OHead1.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                        {
                            this.m_ORecipe.ONinety.OHead2.OMarkTool4.Dispose();
                            this.m_ORecipe.ONinety.OHead2.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                        {
                            this.m_ORecipe.ONinety.OHead3.OMarkTool4.Dispose();
                            this.m_ORecipe.ONinety.OHead3.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                        {
                            this.m_ORecipe.ONinety.OHead4.OMarkTool4.Dispose();
                            this.m_ORecipe.ONinety.OHead4.OMarkTool4 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                    }
                    else if (this.m_EMark == EMARK.INDEX5)
                    {
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                        {
                            this.m_ORecipe.OZero.OHead1.OMarkTool5.Dispose();
                            this.m_ORecipe.OZero.OHead1.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                        {
                            this.m_ORecipe.OZero.OHead2.OMarkTool5.Dispose();
                            this.m_ORecipe.OZero.OHead2.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                        {
                            this.m_ORecipe.OZero.OHead3.OMarkTool5.Dispose();
                            this.m_ORecipe.OZero.OHead3.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                        {
                            this.m_ORecipe.OZero.OHead4.OMarkTool5.Dispose();
                            this.m_ORecipe.OZero.OHead4.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                        {
                            this.m_ORecipe.ONinety.OHead1.OMarkTool5.Dispose();
                            this.m_ORecipe.ONinety.OHead1.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                        {
                            this.m_ORecipe.ONinety.OHead2.OMarkTool5.Dispose();
                            this.m_ORecipe.ONinety.OHead2.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                        {
                            this.m_ORecipe.ONinety.OHead3.OMarkTool5.Dispose();
                            this.m_ORecipe.ONinety.OHead3.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                        if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                        {
                            this.m_ORecipe.ONinety.OHead4.OMarkTool5.Dispose();
                            this.m_ORecipe.ONinety.OHead4.OMarkTool5 = new CogPMAlignTool(this.CpeMark.Subject);
                        }
                    }
                }
                else if (this.m_ETool == ETOOL.DIRECT_CUT)
                {
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                    {
                        this.m_ORecipe.OZero.OHead1.ODirectCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead1.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                    {
                        this.m_ORecipe.OZero.OHead2.ODirectCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead2.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                    {
                        this.m_ORecipe.OZero.OHead3.ODirectCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead3.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                    {
                        this.m_ORecipe.OZero.OHead4.ODirectCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead4.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                    {
                        this.m_ORecipe.ONinety.OHead1.ODirectCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead1.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                    {
                        this.m_ORecipe.ONinety.OHead2.ODirectCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead2.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                    {
                        this.m_ORecipe.ONinety.OHead3.ODirectCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead3.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                    {
                        this.m_ORecipe.ONinety.OHead4.ODirectCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead4.ODirectCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                }
                else if (this.m_ETool == ETOOL.CROSS_CUT)
                {
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BZeroHead1 == true))
                    {
                        this.m_ORecipe.OZero.OHead1.OCrossCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead1.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BZeroHead2 == true))
                    {
                        this.m_ORecipe.OZero.OHead2.OCrossCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead2.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BZeroHead3 == true))
                    {
                        this.m_ORecipe.OZero.OHead3.OCrossCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead3.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.ZERO) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BZeroHead4 == true))
                    {
                        this.m_ORecipe.OZero.OHead4.OCrossCutTool.Dispose();
                        this.m_ORecipe.OZero.OHead4.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD1)) && (OWindow.BNinetyHead1 == true))
                    {
                        this.m_ORecipe.ONinety.OHead1.OCrossCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead1.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD2)) && (OWindow.BNinetyHead2 == true))
                    {
                        this.m_ORecipe.ONinety.OHead2.OCrossCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead2.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD3)) && (OWindow.BNinetyHead3 == true))
                    {
                        this.m_ORecipe.ONinety.OHead3.OCrossCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead3.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                    if (((this.m_EAngle != ESTAGE_ANGLE.NINETY) || (this.m_EView != EVIEW.HEAD4)) && (OWindow.BNinetyHead4 == true))
                    {
                        this.m_ORecipe.ONinety.OHead4.OCrossCutTool.Dispose();
                        this.m_ORecipe.ONinety.OHead4.OCrossCutTool = new CogCaliperTool(this.CceEdge.Subject);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region ETC EVENT
        private void ChkStandardPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;

            try
            {
                CheckBox OControl = (CheckBox)sender;

                if (OControl.Checked == true) OControl.Text = ESTD_POINT.RIGHT.ToString();
                else OControl.Text = ESTD_POINT.LEFT.ToString();
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        private void UpdateList()
        {
            try
            {
                List<int> LstI32ID = new List<int>();
                foreach(CRecipe _Item in CRecipeManager.It.LstORecipe) 
                {
                    LstI32ID.Add(_Item.I32ID);
                }
                LstI32ID.Sort();


                this.LtbRecipe.Items.Clear();

                foreach (int _Item1 in LstI32ID)
                {
                    foreach (CRecipe _Item2 in CRecipeManager.It.LstORecipe)
                    {
                        if (_Item2.I32ID != _Item1) continue;

                        this.LtbRecipe.Items.Add(_Item2.I32ID + "." + _Item2.StrName);
                        break;
                    }
                }

                if (this.LtbRecipe.Items.Count > 0)
                {
                    this.LtbRecipe.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void DisplayTool()
        {
            try
            {
                switch (this.m_EAngle)
                {
                    case ESTAGE_ANGLE.ZERO:
                        this.m_OAlignmentRecipe = this.m_ORecipe.OZero;
                        break;

                    case ESTAGE_ANGLE.NINETY:
                        this.m_OAlignmentRecipe = this.m_ORecipe.ONinety;
                        break;
                }

                switch (this.m_EView)
                {
                    case EVIEW.HEAD1:
                        this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead1;
                        break;
                    case EVIEW.HEAD2:
                        this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead2;
                        break;
                    case EVIEW.HEAD3:
                        this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead3;
                        break;
                    case EVIEW.HEAD4:
                        this.m_OAnalysisRecipe = this.m_OAlignmentRecipe.OHead4;
                        break;
                }

                switch (this.m_ETool)
                {
                    case ETOOL.MARK:
                        switch (this.m_EMark)
                        {
                            case EMARK.INDEX1:
                                this.CpeMark.Subject = this.m_OAnalysisRecipe.OMarkTool1;
                                break;

                            case EMARK.INDEX2:
                                this.CpeMark.Subject = this.m_OAnalysisRecipe.OMarkTool2;
                                break;

                            case EMARK.INDEX3:
                                this.CpeMark.Subject = this.m_OAnalysisRecipe.OMarkTool3;
                                break;

                            case EMARK.INDEX4:
                                this.CpeMark.Subject = this.m_OAnalysisRecipe.OMarkTool4;
                                break;

                            case EMARK.INDEX5:
                                this.CpeMark.Subject = this.m_OAnalysisRecipe.OMarkTool5;
                                break;
                        }
                        this.PnlMarkEdit.BringToFront();
                        break;

                    case ETOOL.DIRECT_CUT:
                        this.CceEdge.Subject = this.m_OAnalysisRecipe.ODirectCutTool;
                        this.PnlCaliperEdit.BringToFront();
                        break;

                    case ETOOL.CROSS_CUT:
                        this.CceEdge.Subject = this.m_OAnalysisRecipe.OCrossCutTool;
                        this.PnlCaliperEdit.BringToFront();
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void DisplayMarks()
        {
            try
            {
                if (this.m_OAnalysisRecipe.OMarkTool1.Pattern.Trained == true)
                {
                    this.CdpMark1.Image = this.m_OAnalysisRecipe.OMarkTool1.Pattern.GetTrainedPatternImage();
                }
                else
                {
                    this.CdpMark1.Image = null;
                }
                if (this.m_OAnalysisRecipe.OMarkTool2.Pattern.Trained == true)
                {
                    this.CdpMark2.Image = this.m_OAnalysisRecipe.OMarkTool2.Pattern.GetTrainedPatternImage();
                }
                else
                {
                    this.CdpMark2.Image = null;
                }
                if (this.m_OAnalysisRecipe.OMarkTool3.Pattern.Trained == true)
                {
                    this.CdpMark3.Image = this.m_OAnalysisRecipe.OMarkTool3.Pattern.GetTrainedPatternImage();
                }
                else
                {
                    this.CdpMark3.Image = null;
                }
                if (this.m_OAnalysisRecipe.OMarkTool4.Pattern.Trained == true)
                {
                    this.CdpMark4.Image = this.m_OAnalysisRecipe.OMarkTool4.Pattern.GetTrainedPatternImage();
                }
                else
                {
                    this.CdpMark4.Image = null;
                }
                if (this.m_OAnalysisRecipe.OMarkTool5.Pattern.Trained == true)
                {
                    this.CdpMark5.Image = this.m_OAnalysisRecipe.OMarkTool5.Pattern.GetTrainedPatternImage();
                }
                else
                {
                    this.CdpMark5.Image = null;
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
