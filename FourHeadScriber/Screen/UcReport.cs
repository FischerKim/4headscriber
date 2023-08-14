using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jhjo.Common;
using Jhjo.DB;
using Cognex.VisionPro;
using System.IO;

namespace FourHeadScriber
{
    public partial class UcReport : UcScreen
    {
        #region VARIABLE
        private CogLine[] m_ArrOAlignmentXLine = null;
        private CogLine[] m_ArrOAlignmentYLine = null;
        private CogPointMarker[] m_ArrOAlignmentMark = null;

        private CogLine[] m_ArrOCutXLine = null;
        private CogLine[] m_ArrOCutYLine = null;
        private CogLine[] m_ArrOStandardLine = null;
        private CogLine[] m_ArrOResultLine = null;

        private bool m_BPreventEvent = false;

        private bool ChkOKAlignment = true;
        private bool ChkOKCut = true;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcReport()
        {
            InitializeComponent();

            try
            {
                this.CdpAlignmentHead1.AutoFit = true;
                this.CdpAlignmentHead1.BackColor = Color.Black;
                this.CdpAlignmentHead1.HorizontalScrollBar = false;
                this.CdpAlignmentHead1.VerticalScrollBar = false;
                this.CdpAlignmentHead2.AutoFit = true;
                this.CdpAlignmentHead2.BackColor = Color.Black;
                this.CdpAlignmentHead2.HorizontalScrollBar = false;
                this.CdpAlignmentHead2.VerticalScrollBar = false;
                this.CdpAlignmentHead3.AutoFit = true;
                this.CdpAlignmentHead3.BackColor = Color.Black;
                this.CdpAlignmentHead3.HorizontalScrollBar = false;
                this.CdpAlignmentHead3.VerticalScrollBar = false;
                this.CdpAlignmentHead4.AutoFit = true;
                this.CdpAlignmentHead4.BackColor = Color.Black;
                this.CdpAlignmentHead4.HorizontalScrollBar = false;
                this.CdpAlignmentHead4.VerticalScrollBar = false;
                this.CdpCutHead1.AutoFit = true;
                this.CdpCutHead1.BackColor = Color.Black;
                this.CdpCutHead1.HorizontalScrollBar = false;
                this.CdpCutHead1.VerticalScrollBar = false;
                this.CdpCutHead2.AutoFit = true;
                this.CdpCutHead2.BackColor = Color.Black;
                this.CdpCutHead2.HorizontalScrollBar = false;
                this.CdpCutHead2.VerticalScrollBar = false;
                this.CdpCutHead3.AutoFit = true;
                this.CdpCutHead3.BackColor = Color.Black;
                this.CdpCutHead3.HorizontalScrollBar = false;
                this.CdpCutHead3.VerticalScrollBar = false;
                this.CdpCutHead4.AutoFit = true;
                this.CdpCutHead4.BackColor = Color.Black;
                this.CdpCutHead4.HorizontalScrollBar = false;
                this.CdpCutHead4.VerticalScrollBar = false;

                this.DgvAlignmentReport.AutoGenerateColumns = false;
                this.DgvCutReport.AutoGenerateColumns = false;


                this.m_ArrOAlignmentXLine = new CogLine[4];
                this.m_ArrOAlignmentYLine = new CogLine[4];
                this.m_ArrOAlignmentMark = new CogPointMarker[4];
                for (int _Index = 0; _Index < 4; _Index++)
                {
                    this.m_ArrOAlignmentXLine[_Index] = new CogLine();
                    this.m_ArrOAlignmentXLine[_Index].Rotation = CogMisc.DegToRad(90);
                    this.m_ArrOAlignmentXLine[_Index].Color = CogColorConstants.Yellow;
                    this.m_ArrOAlignmentXLine[_Index].LineStyle = CogGraphicLineStyleConstants.Dot;
                    this.m_ArrOAlignmentXLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;

                    this.m_ArrOAlignmentYLine[_Index] = new CogLine();
                    this.m_ArrOAlignmentYLine[_Index].Rotation = CogMisc.DegToRad(0);
                    this.m_ArrOAlignmentYLine[_Index].Color = CogColorConstants.Yellow;
                    this.m_ArrOAlignmentYLine[_Index].LineStyle = CogGraphicLineStyleConstants.Dot;
                    this.m_ArrOAlignmentYLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;

                    this.m_ArrOAlignmentMark[_Index] = new CogPointMarker();
                    this.m_ArrOAlignmentMark[_Index].Color = CogColorConstants.Red;
                    this.m_ArrOAlignmentMark[_Index].SizeInScreenPixels = 100;
                    this.m_ArrOAlignmentMark[_Index].Visible = false;
                    this.m_ArrOAlignmentMark[_Index].Interactive = false;
                    this.m_ArrOAlignmentMark[_Index].GraphicDOFEnable = CogPointMarkerDOFConstants.None;
                }
                this.CdpAlignmentHead1.InteractiveGraphics.Add(this.m_ArrOAlignmentXLine[0], "LINE X", true);
                this.CdpAlignmentHead1.InteractiveGraphics.Add(this.m_ArrOAlignmentYLine[0], "LINE Y", true);
                this.CdpAlignmentHead1.InteractiveGraphics.Add(this.m_ArrOAlignmentMark[0], "RESULT", true);
                this.CdpAlignmentHead2.InteractiveGraphics.Add(this.m_ArrOAlignmentXLine[1], "LINE X", true);
                this.CdpAlignmentHead2.InteractiveGraphics.Add(this.m_ArrOAlignmentYLine[1], "LINE Y", true);
                this.CdpAlignmentHead2.InteractiveGraphics.Add(this.m_ArrOAlignmentMark[1], "RESULT", true);
                this.CdpAlignmentHead3.InteractiveGraphics.Add(this.m_ArrOAlignmentXLine[2], "LINE X", true);
                this.CdpAlignmentHead3.InteractiveGraphics.Add(this.m_ArrOAlignmentYLine[2], "LINE Y", true);
                this.CdpAlignmentHead3.InteractiveGraphics.Add(this.m_ArrOAlignmentMark[2], "RESULT", true);
                this.CdpAlignmentHead4.InteractiveGraphics.Add(this.m_ArrOAlignmentXLine[3], "LINE X", true);
                this.CdpAlignmentHead4.InteractiveGraphics.Add(this.m_ArrOAlignmentYLine[3], "LINE Y", true);
                this.CdpAlignmentHead4.InteractiveGraphics.Add(this.m_ArrOAlignmentMark[3], "RESULT", true);

                this.m_ArrOCutXLine = new CogLine[4];
                this.m_ArrOCutYLine = new CogLine[4];
                this.m_ArrOStandardLine = new CogLine[4];
                this.m_ArrOResultLine = new CogLine[4];
                for (int _Index = 0; _Index < 4; _Index++)
                {
                    this.m_ArrOCutXLine[_Index] = new CogLine();
                    this.m_ArrOCutXLine[_Index].Rotation = CogMisc.DegToRad(90);
                    this.m_ArrOCutXLine[_Index].Color = CogColorConstants.Yellow;
                    this.m_ArrOCutXLine[_Index].LineStyle = CogGraphicLineStyleConstants.Dot;
                    this.m_ArrOCutXLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;

                    this.m_ArrOCutYLine[_Index] = new CogLine();
                    this.m_ArrOCutYLine[_Index].Rotation = CogMisc.DegToRad(0);
                    this.m_ArrOCutYLine[_Index].Color = CogColorConstants.Yellow;
                    this.m_ArrOCutYLine[_Index].LineStyle = CogGraphicLineStyleConstants.Dot;
                    this.m_ArrOCutYLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;

                    this.m_ArrOStandardLine[_Index] = new CogLine();
                    this.m_ArrOStandardLine[_Index].Rotation = CogMisc.DegToRad(90);
                    this.m_ArrOStandardLine[_Index].Color = CogColorConstants.Orange;
                    this.m_ArrOStandardLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;

                    this.m_ArrOResultLine[_Index] = new CogLine();
                    this.m_ArrOResultLine[_Index].Rotation = CogMisc.DegToRad(90);
                    this.m_ArrOResultLine[_Index].Color = CogColorConstants.Blue;
                    this.m_ArrOResultLine[_Index].GraphicDOFEnable = CogLineDOFConstants.None;
                }
                this.CdpCutHead1.InteractiveGraphics.Add(this.m_ArrOCutXLine[0], "LINE X", true);
                this.CdpCutHead1.InteractiveGraphics.Add(this.m_ArrOCutYLine[0], "LINE Y", true);
                this.CdpCutHead1.InteractiveGraphics.Add(this.m_ArrOStandardLine[0], "STANDARD", true);
                this.CdpCutHead1.InteractiveGraphics.Add(this.m_ArrOResultLine[0], "RESULT", true);
                this.CdpCutHead2.InteractiveGraphics.Add(this.m_ArrOCutXLine[1], "LINE X", true);
                this.CdpCutHead2.InteractiveGraphics.Add(this.m_ArrOCutYLine[1], "LINE Y", true);
                this.CdpCutHead2.InteractiveGraphics.Add(this.m_ArrOStandardLine[1], "STANDARD", true);
                this.CdpCutHead2.InteractiveGraphics.Add(this.m_ArrOResultLine[1], "RESULT", true);
                this.CdpCutHead3.InteractiveGraphics.Add(this.m_ArrOCutXLine[2], "LINE X", true);
                this.CdpCutHead3.InteractiveGraphics.Add(this.m_ArrOCutYLine[2], "LINE Y", true);
                this.CdpCutHead3.InteractiveGraphics.Add(this.m_ArrOStandardLine[2], "STANDARD", true);
                this.CdpCutHead3.InteractiveGraphics.Add(this.m_ArrOResultLine[2], "RESULT", true);
                this.CdpCutHead4.InteractiveGraphics.Add(this.m_ArrOCutXLine[3], "LINE X", true);
                this.CdpCutHead4.InteractiveGraphics.Add(this.m_ArrOCutYLine[3], "LINE Y", true);
                this.CdpCutHead4.InteractiveGraphics.Add(this.m_ArrOStandardLine[3], "STANDARD", true);
                this.CdpCutHead4.InteractiveGraphics.Add(this.m_ArrOResultLine[3], "RESULT", true);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        //20170410
        #region EVENT
        #region BUTTON EVENT
        private void BtnSearchAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                //20170412
                this.ChkOKAlignment = true;

                DataTable ODataSource = null;
                if (this.DgvAlignmentReport.DataSource != null)
                {
                    ODataSource = (DataTable)this.DgvAlignmentReport.DataSource;
                }

                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_ALIGNMENT);
                this.DgvAlignmentReport.DataSource = OTable.Select(this.DtpAlignment.Value, true);
        
                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnSearchNGAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                
                DataTable ODataSource = null;
                if (this.DgvAlignmentReport.DataSource != null)
                {
                    ODataSource = (DataTable)this.DgvAlignmentReport.DataSource;
                }

                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_ALIGNMENT_NG);
                
                this.DgvAlignmentReport.DataSource = OTable.Select(this.DtpAlignment.Value, true);
                //20170412
                this.ChkOKAlignment = false;

                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnSearchLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.ChkOKCut = true;

                DataTable ODataSource = null;
                if (this.DgvCutReport.DataSource != null)
                {
                    ODataSource = (DataTable)this.DgvCutReport.DataSource;
                }

                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_CUT);
                this.DgvCutReport.DataSource = OTable.Select(this.DtpCut.Value, true);

                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnSearchNGCut_Click(object sender, EventArgs e)
        {
            try
            {
                this.ChkOKCut = false;

                DataTable ODataSource = null;
                if (this.DgvCutReport.DataSource != null)
                {
                    ODataSource = (DataTable)this.DgvCutReport.DataSource;
                }


                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_CUT_NG);
                this.DgvCutReport.DataSource = OTable.Select(this.DtpCut.Value, true);

                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region ETC EVENT
        private void DgvAlignmentReport_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;
            if (this.DgvAlignmentReport.CurrentRow == null) return;

            if (ChkOKAlignment == true)
            {
                try
                {
                    this.CdpAlignmentHead1.DrawingEnabled = false;
                    this.CdpAlignmentHead2.DrawingEnabled = false;
                    this.CdpAlignmentHead3.DrawingEnabled = false;
                    this.CdpAlignmentHead4.DrawingEnabled = false;

                    DataRow ORow = ((DataRowView)this.DgvAlignmentReport.CurrentRow.DataBoundItem).Row;

                    this.LblAlignmentDataHead1X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD1_X].ToString();
                    this.LblAlignmentDataHead2X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD2_X].ToString();
                    this.LblAlignmentDataHead3X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD3_X].ToString();
                    this.LblAlignmentDataHead4X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD4_X].ToString();
                    this.LblAlignmentDataY.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_Y].ToString();
                    this.LblAlignmentDataT.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_T].ToString();

                    this.CdpAlignmentHead1.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD1_FILE].ToString());
                    this.CdpAlignmentHead2.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD2_FILE].ToString());
                    this.CdpAlignmentHead3.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD3_FILE].ToString());
                    this.CdpAlignmentHead4.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD4_FILE].ToString());

                    if (this.CdpAlignmentHead1.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[0].X = this.CdpAlignmentHead1.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[0].Y = this.CdpAlignmentHead1.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD1_RESULT] == true)
                        {
                            this.m_ArrOAlignmentMark[0].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_X];
                            this.m_ArrOAlignmentMark[0].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_Y];
                            this.m_ArrOAlignmentMark[0].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[0].Visible = false;
                    }
                    if (this.CdpAlignmentHead2.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[1].X = this.CdpAlignmentHead2.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[1].Y = this.CdpAlignmentHead2.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD2_RESULT] == true)
                        {
                            this.m_ArrOAlignmentMark[1].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_X];
                            this.m_ArrOAlignmentMark[1].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_Y];
                            this.m_ArrOAlignmentMark[1].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[1].Visible = false;
                    }
                    if (this.CdpAlignmentHead3.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[2].X = this.CdpAlignmentHead3.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[2].Y = this.CdpAlignmentHead3.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD3_RESULT] == true)
                        {
                            this.m_ArrOAlignmentMark[2].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_X];
                            this.m_ArrOAlignmentMark[2].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_Y];
                            this.m_ArrOAlignmentMark[2].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[2].Visible = false;
                    }
                    if (this.CdpAlignmentHead4.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[3].X = this.CdpAlignmentHead4.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[3].Y = this.CdpAlignmentHead4.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD4_RESULT] == true)
                        {
                            this.m_ArrOAlignmentMark[3].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_X];
                            this.m_ArrOAlignmentMark[3].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_Y];
                            this.m_ArrOAlignmentMark[3].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[3].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    CError.Show(ex);
                }
                finally
                {
                    this.CdpAlignmentHead1.DrawingEnabled = true;
                    this.CdpAlignmentHead2.DrawingEnabled = true;
                    this.CdpAlignmentHead3.DrawingEnabled = true;
                    this.CdpAlignmentHead4.DrawingEnabled = true;
                }
            }
            else
            {
                try
                {
                    this.CdpAlignmentHead1.DrawingEnabled = false;
                    this.CdpAlignmentHead2.DrawingEnabled = false;
                    this.CdpAlignmentHead3.DrawingEnabled = false;
                    this.CdpAlignmentHead4.DrawingEnabled = false;

                    DataRow ORow = ((DataRowView)this.DgvAlignmentReport.CurrentRow.DataBoundItem).Row;

                    this.LblAlignmentDataHead1X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD1_X_NG].ToString();
                    this.LblAlignmentDataHead2X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD2_X_NG].ToString();
                    this.LblAlignmentDataHead3X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD3_X_NG].ToString();
                    this.LblAlignmentDataHead4X.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD4_X_NG].ToString();
                    this.LblAlignmentDataY.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_Y_NG].ToString();
                    this.LblAlignmentDataT.Text = ORow[CDB.REPORT_ALIGNMENT_MOVEMENT_T_NG].ToString();

                    this.CdpAlignmentHead1.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD1_FILE_NG].ToString());
                    this.CdpAlignmentHead2.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD2_FILE_NG].ToString());
                    this.CdpAlignmentHead3.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD3_FILE_NG].ToString());
                    this.CdpAlignmentHead4.Image = this.GetImage(ORow[CDB.REPORT_ALIGNMENT_HEAD4_FILE_NG].ToString());

                    if (this.CdpAlignmentHead1.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[0].X = this.CdpAlignmentHead1.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[0].Y = this.CdpAlignmentHead1.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD1_RESULT_NG] == true)
                        {
                            this.m_ArrOAlignmentMark[0].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_X_NG];
                            this.m_ArrOAlignmentMark[0].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_Y_NG];
                            this.m_ArrOAlignmentMark[0].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[0].Visible = false;
                    }
                    if (this.CdpAlignmentHead2.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[1].X = this.CdpAlignmentHead2.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[1].Y = this.CdpAlignmentHead2.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD2_RESULT_NG] == true)
                        {
                            this.m_ArrOAlignmentMark[1].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_X_NG];
                            this.m_ArrOAlignmentMark[1].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_Y_NG];
                            this.m_ArrOAlignmentMark[1].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[1].Visible = false;
                    }
                    if (this.CdpAlignmentHead3.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[2].X = this.CdpAlignmentHead3.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[2].Y = this.CdpAlignmentHead3.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD3_RESULT_NG] == true)
                        {
                            this.m_ArrOAlignmentMark[2].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_X_NG];
                            this.m_ArrOAlignmentMark[2].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_Y_NG];
                            this.m_ArrOAlignmentMark[2].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[2].Visible = false;
                    }
                    if (this.CdpAlignmentHead4.Image != null)
                    {
                        this.m_ArrOAlignmentXLine[3].X = this.CdpAlignmentHead4.Image.Width / 2D;
                        this.m_ArrOAlignmentYLine[3].Y = this.CdpAlignmentHead4.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_ALIGNMENT_HEAD4_RESULT_NG] == true)
                        {
                            this.m_ArrOAlignmentMark[3].X = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_X_NG];
                            this.m_ArrOAlignmentMark[3].Y = (double)ORow[CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_Y_NG];
                            this.m_ArrOAlignmentMark[3].Visible = true;
                        }
                        else this.m_ArrOAlignmentMark[3].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    CError.Show(ex);
                }
                finally
                {
                    this.CdpAlignmentHead1.DrawingEnabled = true;
                    this.CdpAlignmentHead2.DrawingEnabled = true;
                    this.CdpAlignmentHead3.DrawingEnabled = true;
                    this.CdpAlignmentHead4.DrawingEnabled = true;
                }
            }
        }


        private void DgvCutReport_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;
            if (this.DgvCutReport.CurrentRow == null) return;

            if (this.ChkOKCut == true)
            {
                try
                {
                    this.CdpCutHead1.DrawingEnabled = false;
                    this.CdpCutHead2.DrawingEnabled = false;
                    this.CdpCutHead3.DrawingEnabled = false;
                    this.CdpCutHead4.DrawingEnabled = false;

                    DataRow ORow = ((DataRowView)this.DgvCutReport.CurrentRow.DataBoundItem).Row;

                    this.LblCutDataHead1.Text = ORow[CDB.REPORT_CUT_HEAD1_LENGTH].ToString();
                    this.LblCutDataHead2.Text = ORow[CDB.REPORT_CUT_HEAD2_LENGTH].ToString();
                    this.LblCutDataHead3.Text = ORow[CDB.REPORT_CUT_HEAD3_LENGTH].ToString();
                    this.LblCutDataHead4.Text = ORow[CDB.REPORT_CUT_HEAD4_LENGTH].ToString();

                    this.CdpCutHead1.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD1_FILE].ToString());
                    this.CdpCutHead2.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD2_FILE].ToString());
                    this.CdpCutHead3.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD3_FILE].ToString());
                    this.CdpCutHead4.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD4_FILE].ToString());

                    for (int _Index = 0; _Index < 4; _Index++)
                    {
                        this.m_ArrOStandardLine[_Index].Visible = false;
                        this.m_ArrOResultLine[_Index].Visible = false;
                    }


                    if (this.CdpCutHead1.Image != null)
                    {
                        this.m_ArrOCutXLine[0].X = this.CdpCutHead1.Image.Width / 2D;
                        this.m_ArrOCutYLine[0].Y = this.CdpCutHead1.Image.Height / 2D;

                        if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "DIRECT CUT")
                        {
                            this.m_ArrOStandardLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_STANDARD_AXIS];
                            this.m_ArrOResultLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_RESULT_AXIS];
                            this.m_ArrOStandardLine[0].Color = CogColorConstants.Red;
                            this.m_ArrOResultLine[0].Color = CogColorConstants.Orange;
                            this.m_ArrOStandardLine[0].Visible = true;
                            this.m_ArrOResultLine[0].Visible = true;
                        }
                        else if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "CROSS CUT")
                        {
                            this.m_ArrOStandardLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_STANDARD_AXIS];
                            this.m_ArrOResultLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_RESULT_AXIS];
                            this.m_ArrOStandardLine[0].Color = CogColorConstants.Orange;
                            this.m_ArrOResultLine[0].Color = CogColorConstants.Blue;
                            this.m_ArrOStandardLine[0].Visible = true;
                            this.m_ArrOResultLine[0].Visible = true;
                        }
                    }
                    if (this.CdpCutHead2.Image != null)
                    {
                        this.m_ArrOCutXLine[1].X = this.CdpCutHead2.Image.Width / 2D;
                        this.m_ArrOCutYLine[1].Y = this.CdpCutHead2.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD2_FOUND] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_STANDARD_AXIS];
                                this.m_ArrOResultLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_RESULT_AXIS];
                                this.m_ArrOStandardLine[1].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[1].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[1].Visible = true;
                                this.m_ArrOResultLine[1].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_STANDARD_AXIS];
                                this.m_ArrOResultLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_RESULT_AXIS];
                                this.m_ArrOStandardLine[1].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[1].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[1].Visible = true;
                                this.m_ArrOResultLine[1].Visible = true;
                            }
                        }
                    }
                    if (this.CdpCutHead3.Image != null)
                    {
                        this.m_ArrOCutXLine[2].X = this.CdpCutHead3.Image.Width / 2D;
                        this.m_ArrOCutYLine[2].Y = this.CdpCutHead3.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD3_FOUND] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_STANDARD_AXIS];
                                this.m_ArrOResultLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_RESULT_AXIS];
                                this.m_ArrOStandardLine[2].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[2].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[2].Visible = true;
                                this.m_ArrOResultLine[2].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_STANDARD_AXIS];
                                this.m_ArrOResultLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_RESULT_AXIS];
                                this.m_ArrOStandardLine[2].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[2].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[2].Visible = true;
                                this.m_ArrOResultLine[2].Visible = true;
                            }
                        }
                    }
                    if (this.CdpCutHead4.Image != null)
                    {
                        this.m_ArrOCutXLine[3].X = this.CdpCutHead4.Image.Width / 2D;
                        this.m_ArrOCutYLine[3].Y = this.CdpCutHead4.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD4_FOUND] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_STANDARD_AXIS];
                                this.m_ArrOResultLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_RESULT_AXIS];
                                this.m_ArrOStandardLine[3].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[3].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[3].Visible = true;
                                this.m_ArrOResultLine[3].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_STANDARD_AXIS];
                                this.m_ArrOResultLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_RESULT_AXIS];
                                this.m_ArrOStandardLine[3].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[3].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[3].Visible = true;
                                this.m_ArrOResultLine[3].Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CError.Show(ex);
                }
                finally
                {
                    this.CdpCutHead1.DrawingEnabled = true;
                    this.CdpCutHead2.DrawingEnabled = true;
                    this.CdpCutHead3.DrawingEnabled = true;
                    this.CdpCutHead4.DrawingEnabled = true;
                }
            }
            else
            {
                try
                {
                    this.CdpCutHead1.DrawingEnabled = false;
                    this.CdpCutHead2.DrawingEnabled = false;
                    this.CdpCutHead3.DrawingEnabled = false;
                    this.CdpCutHead4.DrawingEnabled = false;

                    DataRow ORow = ((DataRowView)this.DgvCutReport.CurrentRow.DataBoundItem).Row;

                    this.LblCutDataHead1.Text = ORow[CDB.REPORT_CUT_HEAD1_LENGTH_NG].ToString();
                    this.LblCutDataHead2.Text = ORow[CDB.REPORT_CUT_HEAD2_LENGTH_NG].ToString();
                    this.LblCutDataHead3.Text = ORow[CDB.REPORT_CUT_HEAD3_LENGTH_NG].ToString();
                    this.LblCutDataHead4.Text = ORow[CDB.REPORT_CUT_HEAD4_LENGTH_NG].ToString();

                    this.CdpCutHead1.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD1_FILE_NG].ToString());
                    this.CdpCutHead2.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD2_FILE_NG].ToString());
                    this.CdpCutHead3.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD3_FILE_NG].ToString());
                    this.CdpCutHead4.Image = this.GetImage(ORow[CDB.REPORT_CUT_HEAD4_FILE_NG].ToString());

                    for (int _Index = 0; _Index < 4; _Index++)
                    {
                        this.m_ArrOStandardLine[_Index].Visible = false;
                        this.m_ArrOResultLine[_Index].Visible = false;
                    }


                    if (this.CdpCutHead1.Image != null)
                    {
                        this.m_ArrOCutXLine[0].X = this.CdpCutHead1.Image.Width / 2D;
                        this.m_ArrOCutYLine[0].Y = this.CdpCutHead1.Image.Height / 2D;

                        if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "DIRECT CUT")
                        {
                            this.m_ArrOStandardLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_STANDARD_AXIS_NG];
                            this.m_ArrOResultLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_RESULT_AXIS_NG];
                            this.m_ArrOStandardLine[0].Color = CogColorConstants.Red;
                            this.m_ArrOResultLine[0].Color = CogColorConstants.Orange;
                            this.m_ArrOStandardLine[0].Visible = true;
                            this.m_ArrOResultLine[0].Visible = true;
                        }
                        else if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "CROSS CUT")
                        {
                            this.m_ArrOStandardLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_STANDARD_AXIS_NG];
                            this.m_ArrOResultLine[0].X = (double)ORow[CDB.REPORT_CUT_HEAD1_RESULT_AXIS_NG];
                            this.m_ArrOStandardLine[0].Color = CogColorConstants.Orange;
                            this.m_ArrOResultLine[0].Color = CogColorConstants.Blue;
                            this.m_ArrOStandardLine[0].Visible = true;
                            this.m_ArrOResultLine[0].Visible = true;
                        }
                    }
                    if (this.CdpCutHead2.Image != null)
                    {
                        this.m_ArrOCutXLine[1].X = this.CdpCutHead2.Image.Width / 2D;
                        this.m_ArrOCutYLine[1].Y = this.CdpCutHead2.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD2_FOUND_NG] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[1].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[1].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[1].Visible = true;
                                this.m_ArrOResultLine[1].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[1].X = (double)ORow[CDB.REPORT_CUT_HEAD2_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[1].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[1].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[1].Visible = true;
                                this.m_ArrOResultLine[1].Visible = true;
                            }
                        }
                    }
                    if (this.CdpCutHead3.Image != null)
                    {
                        this.m_ArrOCutXLine[2].X = this.CdpCutHead3.Image.Width / 2D;
                        this.m_ArrOCutYLine[2].Y = this.CdpCutHead3.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD3_FOUND_NG] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[2].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[2].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[2].Visible = true;
                                this.m_ArrOResultLine[2].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[2].X = (double)ORow[CDB.REPORT_CUT_HEAD3_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[2].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[2].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[2].Visible = true;
                                this.m_ArrOResultLine[2].Visible = true;
                            }
                        }
                    }
                    if (this.CdpCutHead4.Image != null)
                    {
                        this.m_ArrOCutXLine[3].X = this.CdpCutHead4.Image.Width / 2D;
                        this.m_ArrOCutYLine[3].Y = this.CdpCutHead4.Image.Height / 2D;

                        if ((bool)ORow[CDB.REPORT_CUT_HEAD4_FOUND_NG] == true)
                        {
                            if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "DIRECT CUT")
                            {
                                this.m_ArrOStandardLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[3].Color = CogColorConstants.Red;
                                this.m_ArrOResultLine[3].Color = CogColorConstants.Orange;
                                this.m_ArrOStandardLine[3].Visible = true;
                                this.m_ArrOResultLine[3].Visible = true;
                            }
                            else if (ORow[CDB.REPORT_CUT_ITEM_NG].ToString() == "CROSS CUT")
                            {
                                this.m_ArrOStandardLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_STANDARD_AXIS_NG];
                                this.m_ArrOResultLine[3].X = (double)ORow[CDB.REPORT_CUT_HEAD4_RESULT_AXIS_NG];
                                this.m_ArrOStandardLine[3].Color = CogColorConstants.Orange;
                                this.m_ArrOResultLine[3].Color = CogColorConstants.Blue;
                                this.m_ArrOStandardLine[3].Visible = true;
                                this.m_ArrOResultLine[3].Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CError.Show(ex);
                }
                finally
                {
                    this.CdpCutHead1.DrawingEnabled = true;
                    this.CdpCutHead2.DrawingEnabled = true;
                    this.CdpCutHead3.DrawingEnabled = true;
                    this.CdpCutHead4.DrawingEnabled = true;
                }
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        public override void Remove()
        {
            try
            {
                this.m_BPreventEvent = true;


                this.DtpAlignment.Value = DateTime.Now;
                this.DtpCut.Value = DateTime.Now;


                this.LblAlignmentDataHead1X.Text = string.Empty;
                this.LblAlignmentDataHead2X.Text = string.Empty;
                this.LblAlignmentDataHead3X.Text = string.Empty;
                this.LblAlignmentDataHead4X.Text = string.Empty;
                this.LblAlignmentDataY.Text = string.Empty;
                this.LblAlignmentDataT.Text = string.Empty;

                this.LblCutDataHead1.Text = string.Empty;
                this.LblCutDataHead2.Text = string.Empty;
                this.LblCutDataHead3.Text = string.Empty;
                this.LblCutDataHead4.Text = string.Empty;


                this.CdpAlignmentHead1.Image = null;
                this.CdpAlignmentHead2.Image = null;
                this.CdpAlignmentHead3.Image = null;
                this.CdpAlignmentHead4.Image = null;

                this.CdpCutHead1.Image = null;
                this.CdpCutHead2.Image = null;
                this.CdpCutHead3.Image = null;
                this.CdpCutHead4.Image = null;


                if (this.DgvAlignmentReport.DataSource != null)
                {
                    DataTable ODataSource = (DataTable)this.DgvAlignmentReport.DataSource;
                    this.DgvAlignmentReport.DataSource = null;
                    ODataSource.Dispose();
                    ODataSource = null;
                }
                if (this.DgvCutReport.DataSource != null)
                {
                    DataTable ODataSource = (DataTable)this.DgvCutReport.DataSource;
                    this.DgvCutReport.DataSource = null;
                    ODataSource.Dispose();
                    ODataSource = null;
                }
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


        private CogImage8Grey GetImage(string StrFile)
        {
            CogImage8Grey OResult = null;

            try
            {
                if (String.IsNullOrEmpty(StrFile) == false)
                {
                    if (File.Exists(StrFile) == true)
                    {
                        Bitmap OSource = new Bitmap(StrFile);
                        OResult = new CogImage8Grey(OSource);
                        OSource.Dispose();
                        OSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }
        #endregion
    }
}
