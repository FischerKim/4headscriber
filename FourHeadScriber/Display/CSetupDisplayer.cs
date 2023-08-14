using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Jhjo.Common;
using System.Threading;

namespace FourHeadScriber
{
    public class CSetupDisplayer
    {
        #region VARIABLE
        private CogDisplay m_OCdpDisplay = null;
        private CogDisplayToolbarV2 m_OToolBar = null;
        private CogDisplayStatusBarV2 m_OStatusBar = null;

        private CogLine m_OXVisionLine = null;
        private CogLine m_OYVisionLine = null;
        private CogGraphicLabel m_OVisionLineText = null;

        private bool m_BIsSecond = false;
        private CogLineSegment m_OLengthLineSegment = null;
        private CogGraphicLabel m_OLengthWidthText = null;
        private CogGraphicLabel m_OLengthHeightText = null;
        private CogGraphicLabel m_OLengthText = null;

        private object m_OInterrupt = null;
        #endregion


        #region PROPERTIES
        public CImageInfo OImageInfo
        {
            set
            {
                try
                {
                    Monitor.Enter(this.m_OInterrupt);

                    this.m_OCdpDisplay.Image = value.OImage;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
                finally
                {
                    Monitor.Exit(this.m_OInterrupt);
                }
            }
        }


        public double F64CenterX
        {
            set
            {
                this.m_OXVisionLine.X = value;
                this.m_OVisionLineText.X = value - 30;
            }
        }


        public double F64CenterY
        {
            set { this.m_OYVisionLine.Y = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CSetupDisplayer(CogDisplay OCdpImage, CogDisplayToolbarV2 OToolBar, CogDisplayStatusBarV2 OStatusBar)
        {
            try
            {
                this.m_OCdpDisplay = OCdpImage;
                this.m_OCdpDisplay.AutoFit = true;
                this.m_OCdpDisplay.BackColor = Color.Black;
                this.m_OCdpDisplay.HorizontalScrollBar = false;
                this.m_OCdpDisplay.VerticalScrollBar = false;

                this.m_OToolBar = OToolBar;
                this.m_OToolBar.Display = OCdpImage;

                this.m_OStatusBar = OStatusBar;
                this.m_OStatusBar.Display = OCdpImage;


                this.m_OXVisionLine = new CogLine();
                this.m_OXVisionLine.Rotation = CogMisc.DegToRad(90);
                this.m_OXVisionLine.Color = CogColorConstants.Yellow;
                this.m_OXVisionLine.LineStyle = CogGraphicLineStyleConstants.Dot;
                this.m_OXVisionLine.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OXVisionLine, "VisionLineX", true);
                this.m_OYVisionLine = new CogLine();
                this.m_OYVisionLine.Rotation = CogMisc.DegToRad(0);
                this.m_OYVisionLine.Color = CogColorConstants.Yellow;
                this.m_OYVisionLine.LineStyle = CogGraphicLineStyleConstants.Dot;
                this.m_OYVisionLine.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OYVisionLine, "VisionLineY", true);
                this.m_OVisionLineText = new CogGraphicLabel();
                this.m_OVisionLineText.Text = "VL";
                this.m_OVisionLineText.Font = new Font("Microsoft Sans Serif", 10.0F);
                this.m_OVisionLineText.Y = 30;
                this.m_OVisionLineText.Color = CogColorConstants.Yellow;
                this.m_OVisionLineText.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OVisionLineText, "VisionLineText", true);            

                this.m_OLengthLineSegment = new CogLineSegment();
                this.m_OLengthLineSegment.StartX = 20;
                this.m_OLengthLineSegment.StartY = 150;
                this.m_OLengthLineSegment.EndX = 120;
                this.m_OLengthLineSegment.EndY = 150;
                this.m_OLengthLineSegment.Color = CogColorConstants.Magenta;
                this.m_OLengthLineSegment.Visible = false;
                this.m_OLengthLineSegment.Interactive = false;
                this.m_OLengthLineSegment.GraphicDOFEnable = CogLineSegmentDOFConstants.All;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OLengthLineSegment, "LengthLineSegment", true);                
                this.m_OLengthWidthText = new CogGraphicLabel();
                this.m_OLengthWidthText.Text = "Width :" + (CEnvironment.LEN_PER_PIXEL * 100 * 1000).ToString() + "um(100pix)";
                this.m_OLengthWidthText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft;
                this.m_OLengthWidthText.Color = CogColorConstants.Magenta;
                this.m_OLengthWidthText.X = 20;
                this.m_OLengthWidthText.Y = 50;
                this.m_OLengthWidthText.Visible = false;
                this.m_OLengthWidthText.Interactive = false;
                this.m_OLengthWidthText.GraphicDOFEnable = CogGraphicLabelDOFConstants.None;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OLengthWidthText, "LengthWidthText", true);
                this.m_OLengthHeightText = new CogGraphicLabel();
                this.m_OLengthHeightText.Text = "Height : 0um(0pix)";
                this.m_OLengthHeightText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft;
                this.m_OLengthHeightText.Color = CogColorConstants.Magenta;
                this.m_OLengthHeightText.X = 20;
                this.m_OLengthHeightText.Y = 100;
                this.m_OLengthHeightText.Visible = false;
                this.m_OLengthHeightText.Interactive = false;
                this.m_OLengthHeightText.GraphicDOFEnable = CogGraphicLabelDOFConstants.None;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OLengthHeightText, "LengthHeightText", true);
                this.m_OLengthText = new CogGraphicLabel();
                this.m_OLengthText.Text = "Length :" + (CEnvironment.LEN_PER_PIXEL * 100 * 1000).ToString() + "um(100pix)";
                this.m_OLengthText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft;
                this.m_OLengthText.Color = CogColorConstants.Magenta;
                this.m_OLengthText.X = 20;
                this.m_OLengthText.Y = 150;
                this.m_OLengthText.Visible = false;
                this.m_OLengthText.Interactive = false;
                this.m_OLengthText.GraphicDOFEnable = CogGraphicLabelDOFConstants.None;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OLengthText, "LengthText", true);

                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Init()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OCdpDisplay.Image = null;
                this.m_OCdpDisplay.StaticGraphics.Clear();

                this.m_OLengthLineSegment.Visible = false;
                this.m_OLengthWidthText.Visible = false;
                this.m_OLengthHeightText.Visible = false;
                this.m_OLengthHeightText.Visible = false;
                this.m_OLengthText.Visible = false;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void InitCalibration()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_OCdpDisplay.StaticGraphics.Clear();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void ShowLengthChecker()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OLengthLineSegment.Visible = true;
                this.m_OLengthWidthText.Visible = true;
                this.m_OLengthHeightText.Visible = true;
                this.m_OLengthText.Visible = true;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void EstimateLength(Point OPoint)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                if (this.m_OLengthLineSegment.Visible == true)
                {
                    double F64X = 0;
                    double F64Y = 0;
                    CogTransform2DLinear OTransform = (CogTransform2DLinear)this.m_OCdpDisplay.GetTransform("#", "*");
                    OTransform.MapPoint(OPoint.X, OPoint.Y, out F64X, out F64Y);

                    this.m_BIsSecond = !this.m_BIsSecond;
                    if (this.m_BIsSecond == false) this.m_OLengthLineSegment.SetStartEnd(F64X, F64Y, this.m_OLengthLineSegment.EndX, this.m_OLengthLineSegment.EndY);
                    else this.m_OLengthLineSegment.SetStartEnd(this.m_OLengthLineSegment.StartX, this.m_OLengthLineSegment.StartY, F64X, F64Y);

                    double F64Width = Math.Abs(this.m_OLengthLineSegment.StartX - this.m_OLengthLineSegment.EndX);
                    double F64Height = Math.Abs(this.m_OLengthLineSegment.StartY - this.m_OLengthLineSegment.EndY);
                    double F64Length = Math.Sqrt(Math.Pow(F64Width, 2) + Math.Pow(F64Height, 2));
                    double F64RealWidth = Math.Round(F64Width * CEnvironment.LEN_PER_PIXEL * 1000);
                    double F64RealHeight = Math.Round(F64Height * CEnvironment.LEN_PER_PIXEL * 1000);
                    double F64RealLength = Math.Round(Math.Sqrt(Math.Pow(F64RealWidth, 2) + Math.Pow(F64RealHeight, 2)));

                    this.m_OLengthWidthText.Text = "Width : " + F64RealWidth + "um(" + Math.Round(F64Width, 3) + "pix)";
                    this.m_OLengthHeightText.Text = "Height : " + F64RealHeight + "um(" + Math.Round(F64Height, 3) + "pix)";
                    this.m_OLengthText.Text = "Length : " + F64RealLength + "um(" + Math.Round(F64Length, 3) + "pix)";
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void HideLengthChecker()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OLengthLineSegment.Visible = false;
                this.m_OLengthWidthText.Visible = false;
                this.m_OLengthHeightText.Visible = false;
                this.m_OLengthText.Visible = false;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void DrawCalibrationResult(CMarkResult OResult)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                if ((OResult.BInspected == true) && (OResult.BOK == true))
                {
                    CogPointMarker OMarker = new CogPointMarker();
                    OMarker.X = OResult.F64X;
                    OMarker.Y = OResult.F64Y;
               
                    OMarker.Color = CogColorConstants.Green;
                    OMarker.SizeInScreenPixels = 100;

                    this.m_OCdpDisplay.StaticGraphics.Add(OMarker, "CalibrationResult");
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }
        #endregion
    }
}
