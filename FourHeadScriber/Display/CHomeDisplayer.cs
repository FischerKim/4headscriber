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
    public class CHomeDisplayer
    {
        #region VARIABLE
        private EVIEW m_EHead = EVIEW.NONE;

        private CogDisplay m_OCdpDisplay = null;
        private CogDisplayToolbarV2 m_OToolBar = null;
        private CogDisplayStatusBarV2 m_OStatusBar = null;

        private CogLine m_OXVisionLine = null;
        private CogLine m_OYVisionLine = null;
        private CogGraphicLabel m_OVisionLineText = null;
        private CogPointMarker m_OTarget = null;

        private CogLine m_OWheelLine = null;
        private CogGraphicLabel m_OWheelLineText = null;

        private bool m_BIsSecond = false;
        private CogLineSegment m_OLengthLineSegment = null;
        private CogGraphicLabel m_OLengthWidthText = null;
        private CogGraphicLabel m_OLengthHeightText = null;
        private CogGraphicLabel m_OLengthText = null;

        private CogPointMarker m_OAlignmentPointMarker = null;
        private CogLine m_ODirectCutLine = null;
        private CogGraphicLabel m_ODirectCutText = null;
        private CogLine m_OCrossCutLine = null;
        private CogGraphicLabel m_OCrossCutText = null;

        private CogPointMarker m_OAlignmentManualPoint = null;
        //20170227
        private CogLine m_OManualLineX = null;
        private CogLine m_OManualLineY = null;
        private bool m_BFound = false;
        private bool m_BFixOK = true;

        private object m_OInterrupt = null;
        //20170413
        private bool m_BOverride_NoFix = true; //when the user wants to unfix Y.
        #endregion


        #region PROPERTIES
        public EVIEW EHead
        {
            get { return this.m_EHead; }
            set { this.m_EHead = value; }
        }


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
                this.m_OTarget.X = value;
            }
        }


        public double F64CenterY
        {
            set
            {
                this.m_OYVisionLine.Y = value;
                this.m_OTarget.Y = value;
            }
        }


        public bool BOverride_NoFix
        {
            get { return this.m_BOverride_NoFix; }
            set
            {
                this.m_BOverride_NoFix = value;
            }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CHomeDisplayer(CogDisplay OCdpImage, CogDisplayToolbarV2 OToolBar, CogDisplayStatusBarV2 OStatusBar)
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
                this.m_OTarget = new CogPointMarker();
                this.m_OTarget.Color = CogColorConstants.Green;
                this.m_OTarget.SizeInScreenPixels = 100;
                this.m_OTarget.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OTarget, "Target", true);

                this.m_OWheelLine = new CogLine();
                this.m_OWheelLine.Rotation = CogMisc.DegToRad(90);
                this.m_OWheelLine.Color = CogColorConstants.Red;
                this.m_OWheelLine.Visible = false;
                this.m_OWheelLine.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OWheelLine, "WheelLine", true);
                this.m_OWheelLineText = new CogGraphicLabel();
                this.m_OWheelLineText.Text = "WL";
                this.m_OWheelLineText.Font = new Font("Microsoft Sans Serif", 10.0F);
                this.m_OWheelLineText.X = this.m_OWheelLine.X - 30;
                this.m_OWheelLineText.Y = 30;
                this.m_OWheelLineText.Color = CogColorConstants.Red;
                this.m_OWheelLineText.Visible = false;
                this.m_OWheelLineText.Interactive = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OWheelLineText, "WheelLineText", true);               

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
                
                this.m_OAlignmentPointMarker = new CogPointMarker();
                this.m_OAlignmentPointMarker.Color = CogColorConstants.Red;
                this.m_OAlignmentPointMarker.SizeInScreenPixels = 100;
                
                this.m_ODirectCutLine = new CogLine();
                this.m_ODirectCutLine.Rotation = CogMisc.DegToRad(90);
                this.m_ODirectCutLine.Color = CogColorConstants.Orange;
                this.m_ODirectCutText = new CogGraphicLabel();
                this.m_ODirectCutText.Text = "DL";
                this.m_ODirectCutText.Font = new Font("Microsoft Sans Serif", 10.0F);
                this.m_ODirectCutText.X = this.m_ODirectCutLine.X - 30;
                this.m_ODirectCutText.Y = 950;
                this.m_ODirectCutText.Color = CogColorConstants.Orange;

                this.m_OCrossCutLine = new CogLine();
                this.m_OCrossCutLine.Rotation = CogMisc.DegToRad(90);
                this.m_OCrossCutLine.Color = CogColorConstants.Blue;
                this.m_OCrossCutText = new CogGraphicLabel();
                this.m_OCrossCutText.Text = "CL";
                this.m_OCrossCutText.Font = new Font("Microsoft Sans Serif", 10.0F);
                this.m_OCrossCutText.X = this.m_OCrossCutLine.X - 30;
                this.m_OCrossCutText.Y = 950;
                this.m_OCrossCutText.Color = CogColorConstants.Blue;

                this.m_OAlignmentManualPoint = new CogPointMarker();
                this.m_OAlignmentManualPoint.Color = CogColorConstants.Blue;
                this.m_OAlignmentManualPoint.SelectedColor = CogColorConstants.Blue;
                this.m_OAlignmentManualPoint.DragColor = CogColorConstants.Blue;

                //0228
                //20170227
                //let this also be draggable and make the m_OAlignmentManualPoint follow.
                this.m_OManualLineX = new CogLine(); //which represents the x value, therefore vertical.
                this.m_OManualLineX.Rotation = CogMisc.DegToRad(90);
                this.m_OManualLineX.Color = CogColorConstants.None;
                this.m_OManualLineX.Visible = false;
                this.m_OManualLineX.Interactive = true;
                this.m_OManualLineX.LineStyle = CogGraphicLineStyleConstants.Dot;
                this.m_OManualLineX.Changed += new CogChangedEventHandler(WhileDraggingVerticalLine);
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OManualLineX, "ManualLineX", true);
                //20170227
                this.m_OManualLineY = new CogLine();
                this.m_OManualLineY.Rotation = CogMisc.DegToRad(0);
                this.m_OManualLineY.Color = CogColorConstants.None;
                this.m_OManualLineY.Interactive = false; //unmovable
                this.m_OManualLineY.Visible = false;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OManualLineY, "ManualLineY", true);

                this.m_OAlignmentManualPoint.Changed += new CogChangedEventHandler(WhileDraggingPointMarker);
                this.m_OAlignmentManualPoint.SizeInScreenPixels = 100;
                this.m_OAlignmentManualPoint.Visible = false;
                this.m_OAlignmentManualPoint.Interactive = true;
                this.m_OAlignmentManualPoint.GraphicDOFEnable = CogPointMarkerDOFConstants.All;
                this.m_OCdpDisplay.InteractiveGraphics.Add(this.m_OAlignmentManualPoint, "Alignment_Manual_Point", true);
                
                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        #region PROPERTIES
        public bool BFound
        {
            get { return this.m_BFound; } //whether the mark is found at the moment
        }

        public double F64Y
        {
            get { return this.m_OAlignmentManualPoint.Y; }
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


        //20170227 //0228
        private void WhileDraggingPointMarker(object sender, Cognex.VisionPro.CogChangedEventArgs e)
        {
            try
            {
                 //20170413
                if (this.BOverride_NoFix == false)
                {
                    if (this.m_BFixOK == true && this.m_BFound == false) //at least one mark should be found for m_OManualLineY value to have any meaning.
                    {
                        //if (e.DragGraphic == null) return;
                        //if (e.DragGraphic.GetType() == typeof(CogPointMarker)) return;

                        this.m_OAlignmentManualPoint.Y = this.m_OManualLineY.Y; //that is, fixing the Y
                        this.m_OManualLineX.X = this.m_OAlignmentManualPoint.X; //updating the vertical line to the marker's x.
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        //20170227 //0228
        private void WhileDraggingVerticalLine(object sender, Cognex.VisionPro.CogChangedEventArgs e)
        {
            try
            {
                // if (e.DragGraphic == null) return;
                // if (e.DragGraphic.GetType() == typeof(CogLine)) return;

                this.m_OAlignmentManualPoint.X = this.m_OManualLineX.X;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        #region LENGTH CHECKER
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
        #endregion

        #region WHEEL LINE
        public void ShowWheelLine()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OWheelLine.Visible = true;
                this.m_OWheelLineText.Visible = true;
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


        public void SetWheel(double F64Wheel)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                double F64WheelPixel = (F64Wheel / CEnvironment.LEN_PER_PIXEL);

                this.m_OWheelLine.X = this.m_OXVisionLine.X - F64WheelPixel;
                this.m_OWheelLineText.X = this.m_OXVisionLine.X - F64WheelPixel - 30;
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


        public void HideWheelLine()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OWheelLine.Visible = false;
                this.m_OWheelLineText.Visible = false;
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

        //20170227
        public void DrawMarkResult(CMarkResult OResult) //this gets called everytime an alignment is finished.
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                this.m_OCdpDisplay.StaticGraphics.Clear();

                if (OResult != null)
                {
                    if ((OResult.BInspected == true) && (OResult.BOK == true))
                    {
                        this.m_OAlignmentPointMarker.X = OResult.F64X;
                        this.m_OAlignmentPointMarker.Y = OResult.F64Y;
                        this.m_OCdpDisplay.StaticGraphics.Add(this.m_OAlignmentPointMarker, "ALIGNMENT_RESULT");

                        this.m_BFound = true;
                    }
                    else
                    {
                        this.m_OAlignmentPointMarker.X = this.m_OTarget.X;
                        this.m_OAlignmentPointMarker.Y = this.m_OTarget.Y;

                        this.m_BFound = false;
                    }
                }
                else
                {
                    this.m_BFound = false;
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

        #region CUT
        public void DrawDirectCutGraphic(CCutResult OResult)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                
                if (OResult != null)
                {
                    if (OResult.BOK == true)
                    {
                        //20170411 NG 여도 그래픽 지워지지않게 하기
                        this.m_OCdpDisplay.StaticGraphics.Clear();

                        this.m_ODirectCutLine.X = OResult.F64MidX;
                        this.m_ODirectCutText.X = this.m_ODirectCutLine.X - 30;

                        this.m_OCdpDisplay.StaticGraphics.Add(this.m_ODirectCutLine, "DIRECT_CUT_LINE");
                        this.m_OCdpDisplay.StaticGraphics.Add(this.m_ODirectCutText, "DIRECT_CUT_TEXT");
                    }
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


        public void DrawCrossCutGraphic(CCutResult OResult)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.m_OCdpDisplay.DrawingEnabled = false;

                if (OResult != null)
                {
                    if (OResult.BOK == true)
                    {
                        this.m_OCrossCutLine.X = OResult.F64MidX;
                        this.m_OCrossCutText.X = this.m_OCrossCutLine.X - 30;

                        this.m_OCdpDisplay.StaticGraphics.Add(this.m_OCrossCutLine, "CROSS_CUT_LINE");
                        this.m_OCdpDisplay.StaticGraphics.Add(this.m_OCrossCutText, "CROSS_CUT_TEXT");
                    }
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

        //20170227
        //when [manual] button is clicked, make PointMarker, Line X and Y visible, and set PointMarker and LineX to the designated location.
        public void VisibleAlignmentManualPoint(bool BVisible)
        {
            try
            {
                //0228
                Monitor.Enter(this.m_OInterrupt);
                //this.m_OCdpDisplay.DrawingEnabled = false;
                this.m_BFixOK = false;
                if (BVisible == true)
                {
                    this.m_OAlignmentManualPoint.X = this.m_OAlignmentPointMarker.X;//center by default, matched pattern mark location if inspected.

                    this.m_OAlignmentManualPoint.Y = this.m_OAlignmentPointMarker.Y;
                    this.m_OManualLineX.X = this.m_OAlignmentPointMarker.X;
                    this.m_OManualLineX.Visible = BVisible;
                    this.m_OManualLineY.Visible = BVisible;
                }
                this.m_OAlignmentManualPoint.Visible = BVisible;
                this.m_OManualLineX.Visible = BVisible;
                this.m_OManualLineY.Visible = BVisible;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_BFixOK = true;
                this.m_OCdpDisplay.DrawingEnabled = true;
                Monitor.Exit(this.m_OInterrupt);
            }
        }
        

        //20170227
        public void FixYLine(double F64Y)
        {
            try
            {
                this.m_BFixOK = true;
                this.m_OManualLineY.Y = F64Y;
                if (this.m_BFound == false) //So.. only if the mark is not found, fix the point to the line.
                {
                    this.m_OAlignmentManualPoint.Y = F64Y; //for setting starting point only
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void NoMarksAtAll() //then the Y of the PointMarker should be movable
        {
            try
            {
                this.m_BFixOK = false;
                this.m_OManualLineY.Visible = false;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CMarkResult GetManualAlignmentInfo(EVIEW EView)
        {
            CMarkResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if ((EView & this.m_EHead) == this.m_EHead)
                {
                    if (this.m_OAlignmentManualPoint.Visible == true)
                    {
                        CImageInfo OImageInfo = new CImageInfo((CogImage8Grey)this.m_OCdpDisplay.Image);

                        OResult = new CMarkResult(this.m_EHead, OImageInfo);
                        OResult.BInspected = true;
                        OResult.BOK = true;
                        OResult.F64S = 100;
                        OResult.F64X = this.m_OAlignmentManualPoint.X;
                        OResult.F64Y = this.m_OAlignmentManualPoint.Y;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }
        #endregion
    }
}
