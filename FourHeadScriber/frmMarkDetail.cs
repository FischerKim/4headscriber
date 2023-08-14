using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cognex.VisionPro;
using Jhjo.Common;
using Cognex.VisionPro.Caliper;

namespace FourHeadScriber
{
    public partial class frmMarkDetail : Form
    {
        #region VARIABLE
        private CogImage8Grey m_OImage = null;
        private CogRectangle m_OBound = null;

        private CogCaliperTool m_OTool = null;
        private CogRectangleAffine m_OROI = null;
        #endregion


        #region PROPERTIES
        public CogImage8Grey OImage
        {
            set { this.m_OImage = value; }
        }


        public CogRectangle OBound
        {
            get { return this.m_OBound; }
            set { this.m_OBound = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public frmMarkDetail()
        {
            InitializeComponent();

            try
            {
                this.CdpFind.BackColor = Color.Blue;
                this.CdpFind.HorizontalScrollBar = false;
                this.CdpFind.VerticalScrollBar = false;
                this.CdpFind.AutoFit = true;
                this.CdpResult.BackColor = Color.Blue;
                this.CdpResult.HorizontalScrollBar = false;
                this.CdpResult.VerticalScrollBar = false;
                this.CdpResult.AutoFit = true;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region EVENT
        #region FORM EVENT
        private void frmMarkDetail_Load(object sender, EventArgs e)
        {
            try
            {
                this.m_OROI = new CogRectangleAffine();
                this.m_OROI.SetCenterLengthsRotationSkew(100, 100, 200, 200, 0, 0);
                this.m_OROI.XDirectionAdornment = CogRectangleAffineDirectionAdornmentConstants.SolidArrow;
                this.m_OROI.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size;
                this.m_OROI.Interactive = true;

                this.m_OTool = new CogCaliperTool();
                this.m_OTool.InputImage = this.m_OImage;
                this.m_OTool.Region = this.m_OROI;
                this.m_OTool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.Pair;
                this.m_OTool.RunParams.MaxResults = 1;

                this.CdpFind.Image = this.m_OImage;
                this.CdpFind.InteractiveGraphics.Add(this.m_OROI, "ROI", true);


                this.m_OBound.GraphicDOFEnable = CogRectangleDOFConstants.All;
                this.m_OBound.Interactive = true;

                this.CdpResult.Image = this.m_OImage;
                this.CdpResult.InteractiveGraphics.Add(this.m_OBound, "BOUND", true);


                this.CmbDirection.SelectedIndex = 0;
                this.CmbPolarity0.SelectedIndex = 0;
                this.CmbPolarity1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region BUTTON EVENT
        private void BtnInspect_Click(object sender, EventArgs e)
        {
            try
            {
                this.CdpFind.StaticGraphics.Clear();

                this.m_OTool.RunParams.Edge0Polarity = this.GetPolarity(this.CmbPolarity0.Text);
                this.m_OTool.RunParams.Edge0Position = (Convert.ToInt32(this.NudEdgeLength.Value) / 2D) * -1;
                this.m_OTool.RunParams.Edge1Polarity = this.GetPolarity(this.CmbPolarity1.Text);
                this.m_OTool.RunParams.Edge1Position = Convert.ToInt32(this.NudEdgeLength.Value) / 2D;
                this.m_OTool.RunParams.ContrastThreshold = Convert.ToInt32(this.NudThreshold.Value);
                this.m_OTool.RunParams.FilterHalfSizeInPixels = Convert.ToInt32(this.NudHalfPixel.Value);
                this.m_OTool.Run();

                if ((this.m_OTool.Results != null) && (this.m_OTool.Results.Count > 0))
                {
                    this.CdpFind.StaticGraphics.Add(this.m_OTool.Results[0].CreateResultGraphics(CogCaliperResultGraphicConstants.All), "RESULT");
                }
                else CMsgBox.Warning("Cannot found result!");
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
                this.m_OTool.RunParams.Edge0Polarity = this.GetPolarity(this.CmbPolarity0.Text);
                this.m_OTool.RunParams.Edge0Position = (Convert.ToInt32(this.NudEdgeLength.Value) / 2D) * -1;
                this.m_OTool.RunParams.Edge1Polarity = this.GetPolarity(this.CmbPolarity1.Text);
                this.m_OTool.RunParams.Edge1Position = Convert.ToInt32(this.NudEdgeLength.Value) / 2D;
                this.m_OTool.RunParams.ContrastThreshold = Convert.ToInt32(this.NudThreshold.Value);
                this.m_OTool.RunParams.FilterHalfSizeInPixels = Convert.ToInt32(this.NudHalfPixel.Value);
                this.m_OTool.Run();

                if ((this.m_OTool.Results != null) && (this.m_OTool.Results.Count > 0))
                {
                    if (this.CmbDirection.Text == "HORIZONTAL")
                    {
                        if (this.m_OTool.Results[0].Edge0.PositionX < this.m_OTool.Results[0].Edge1.PositionX)
                        {
                            this.m_OBound.X = this.m_OTool.Results[0].Edge0.PositionX;
                            this.m_OBound.Width = this.m_OTool.Results[0].Edge1.PositionX - this.m_OTool.Results[0].Edge0.PositionX;
                        }
                        else
                        {
                            this.m_OBound.X = this.m_OTool.Results[0].Edge1.PositionX;
                            this.m_OBound.Width = this.m_OTool.Results[0].Edge0.PositionX - this.m_OTool.Results[0].Edge1.PositionX;
                        }
                    }
                    else
                    {
                        if (this.m_OTool.Results[0].Edge0.PositionY < this.m_OTool.Results[0].Edge1.PositionY)
                        {
                            this.m_OBound.Y = this.m_OTool.Results[0].Edge0.PositionY;
                            this.m_OBound.Height = this.m_OTool.Results[0].Edge1.PositionY - this.m_OTool.Results[0].Edge0.PositionY;
                        }
                        else
                        {
                            this.m_OBound.Y = this.m_OTool.Results[0].Edge1.PositionY;
                            this.m_OBound.Height = this.m_OTool.Results[0].Edge0.PositionY - this.m_OTool.Results[0].Edge1.PositionY;
                        }
                    }
                }
                else CMsgBox.Warning("Cannot found result for mark!");
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_OTool != null)
                {
                    this.m_OTool.Dispose();
                    this.m_OTool = null;
                }

                base.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region ETC EVENT
        private void CmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CmbDirection.Text == "HORIZONTAL")
                {
                    this.m_OROI.SetCenterLengthsRotationSkew
                    (
                        this.m_OROI.CenterX,
                        this.m_OROI.CenterY,
                        this.m_OROI.SideXLength,
                        this.m_OROI.SideYLength,
                        CogMisc.DegToRad(0),
                        0
                    );
                }
                else
                {
                    this.m_OROI.SetCenterLengthsRotationSkew
                    (
                        this.m_OROI.CenterX,
                        this.m_OROI.CenterY,
                        this.m_OROI.SideXLength,
                        this.m_OROI.SideYLength,
                        CogMisc.DegToRad(90),
                        0
                    );
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
        private CogCaliperPolarityConstants GetPolarity(string StrPolarity)
        {
            CogCaliperPolarityConstants EResult = CogCaliperPolarityConstants.DarkToLight;

            try
            {
                switch (StrPolarity)
                {
                    case "Dark To Light":
                        EResult = CogCaliperPolarityConstants.DarkToLight;
                        break;

                    case "Light To Dark":
                        EResult = CogCaliperPolarityConstants.LightToDark;
                        break;

                    case "Don't Care":
                        EResult = CogCaliperPolarityConstants.DontCare;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return EResult;
        }
        #endregion
    }
}
