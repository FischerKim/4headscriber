using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using Jhjo.Tool;

namespace FourHeadScriber
{
    public class CLoggingTool
    {
        #region SINGLE TON
        protected static CLoggingTool Src_It = null;


        public static CLoggingTool It
        {
            get
            {
                CLoggingTool OResult = null;

                try
                {
                    if (CLoggingTool.Src_It == null)
                    {
                        CLoggingTool.Src_It = new CLoggingTool();
                    }

                    OResult = CLoggingTool.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion


        #region VARIABLE
        private CLogSaveFile m_OFile = null;
        private SendMessageHandler m_OSendMessage = null;
        #endregion


        #region DELEGATE & EVENT
        public delegate void SendMessageHandler(string StrMsg);
        #endregion


        #region PROPERTIES
        public SendMessageHandler OSendMessage
        {
            set { this.m_OSendMessage = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CLoggingTool()
        {
            try
            {
                this.m_OFile = new CLogSaveFile(".\\Log");
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void StartCalibration(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Calibration", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void NextCalibrationMovement(ESTAGE_ANGLE EAngle, int I32Index, int I32Count)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Next Calibration Movement({1}/{2})", (int)EAngle, I32Index, I32Count);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        public void StartCalibrationMovement(ESTAGE_ANGLE EAngle, int I32Index, int I32Count,
                                             double F64Head1X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Calibration Movement({1}/{2})", (int)EAngle, I32Index, I32Count);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, Math.Round(F64Head1X, 4));
                this.OnSendMessage(StrMsg);
                
                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, Math.Round(F64Head4X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, Math.Round(F64Y, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        public void StartCalibrationMovement(ESTAGE_ANGLE EAngle, int I32Index, int I32Count,
                                       double F64Head1X, double F64Head2X, double F64Head3X,double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Calibration Movement({1}/{2})", (int)EAngle, I32Index, I32Count);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, Math.Round(F64Head1X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, Math.Round(F64Head2X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, Math.Round(F64Head3X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, Math.Round(F64Y, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartCalibrationMovement(ESTAGE_ANGLE EAngle, int I32Index, int I32Count, 
                                             double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Calibration Movement({1}/{2})", (int)EAngle, I32Index, I32Count);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, Math.Round(F64Head1X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, Math.Round(F64Head2X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, Math.Round(F64Head3X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, Math.Round(F64Head4X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, Math.Round(F64Y, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        //1213
        public void FinishCalibrationMovement(ESTAGE_ANGLE EAngle, int I32Index, int I32Count, double[] ArrF64X, double[] ArrF64Y)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Calibration Axis({1}/{2})", (int)EAngle, I32Index, I32Count);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 Axis (X = {1}, Y = {2})", (int)EAngle, Math.Round(ArrF64X[0], 3), Math.Round(ArrF64Y[0], 3));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 Axis (X = {1}, Y = {2})", (int)EAngle, Math.Round(ArrF64X[1], 3), Math.Round(ArrF64Y[1], 3));
                this.OnSendMessage(StrMsg);

                if (ArrF64X.Length > 2 && ArrF64Y.Length > 2)
                {
                    StrMsg = String.Format("[{0}]Head3 Axis (X = {1}, Y = {2})", (int)EAngle, Math.Round(ArrF64X[2], 3), Math.Round(ArrF64Y[2], 3));
                    this.OnSendMessage(StrMsg);

                    if (ArrF64X.Length > 3 && ArrF64Y.Length > 3)
                    {
                        StrMsg = String.Format("[{0}]Head4 Axis (X = {1}, Y = {2})", (int)EAngle, Math.Round(ArrF64X[3], 3), Math.Round(ArrF64Y[3], 3));
                        this.OnSendMessage(StrMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SuccessCalibration(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Finish Calibration Successfully", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCalibrationByMotion(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Calibration due to Motion is not ready!", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCalibrationByTimeOut(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Calibration due to Motion time out!", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCalibrationByUserOperation(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Calibration due to User operation", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCalibrationByUnknownPoint(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Calibration due to Unknown point", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void BeginInspection()
        {
            try
            {
                string StrMsg = String.Format("Begin Inspection");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartAlignment(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowAlignmentAxis(ESTAGE_ANGLE EAngle, EVIEW EView, double F64X, double F64Y)
        {
            try
            {
                string StrMsg = String.Format("[{0}]{1} Alignment Axis (X = {2}, Y = {3})", (int)EAngle, EView.ToString(), Math.Round(F64X, 3), Math.Round(F64Y, 3));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowAlignmentMotionCurrent(ESTAGE_ANGLE EAngle, double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Motion Current Position", (int)EAngle);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, F64Head1X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, F64Head2X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, F64Head3X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, F64Head4X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, F64Y);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowAlignmentMotionMovement(ESTAGE_ANGLE EAngle, double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Send Alignment Result", (int)EAngle);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, Math.Round(F64Head1X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, Math.Round(F64Head2X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, Math.Round(F64Head3X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, Math.Round(F64Head4X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, Math.Round(F64Y, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle,F64T.ToString("0.00000")); //Math.Round(F64T, 5));
                this.OnSendMessage(StrMsg);
                
                if (F64Y < 0.00001 && F64T < 0.00001)
                {
                    StrMsg = String.Format("Y, T OK; Moving Xs only");
                    this.OnSendMessage(StrMsg);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RetryAlignment(ESTAGE_ANGLE EAngle, int currentcount)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Retry[{1}] Alignment", (int)EAngle, (int) currentcount);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SuccessAlignment(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Success Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByNotReady(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Alignment due to Vision not ready", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByNotMatchedRecipe(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Alignment due to Not matched recipe", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByViewCount(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Alignment due to need more than 2 Views for Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByUnknownPoint(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Alignment due to {1} Unknown point", (int)EAngle, EView.ToString());
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByMaxRetryLimitReached()
        {
            try
            {
                string StrMsg = String.Format("Fail Alignment: Max Try Limit Reached");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailAlignmentByNeedCalibration(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Alignment due to Need calibration", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartManualAlignment(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Manual Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowManualAlignmentAxis(ESTAGE_ANGLE EAngle, EVIEW EView, double F64X, double F64Y)
        {
            try
            {
                string StrMsg = String.Format("[{0}]{1} Manual Alignment Axis (X = {2}, Y = {3})", (int)EAngle, EView.ToString(), Math.Round(F64X, 3), Math.Round(F64Y, 3));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowManualAlignmentMotionCurrent(ESTAGE_ANGLE EAngle, double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Motion Current Position(Manual Alignment)", (int)EAngle);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, F64Head1X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, F64Head2X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, F64Head3X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, F64Head4X);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, F64Y);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowManualAlignmentMotionMovement(ESTAGE_ANGLE EAngle, double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Send Manual Alignment Result", (int)EAngle);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head1 X = {1}", (int)EAngle, Math.Round(F64Head1X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head2 X = {1}", (int)EAngle, Math.Round(F64Head2X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head3 X = {1}", (int)EAngle, Math.Round(F64Head3X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Head4 X = {1}", (int)EAngle, Math.Round(F64Head4X, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]Y = {1}", (int)EAngle, Math.Round(F64Y, 4));
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("[{0}]T = {1}", (int)EAngle, F64T.ToString("0.00000"));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RetryManualAlignment(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Retry Manual Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SuccessManualAlignment(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Success Manual Alignment", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailManualAlignmentByNotManualState(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Manual Alignment due to Not Manual State", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailManualAlignmentByNeedCalibration(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Manual Alignment due to Need calibration", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartDirectCut(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Direct Cut", (int)EAngle);
                this.OnSendMessage(StrMsg);

                if ((EView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    StrMsg = String.Format("[{0}] Direct Cut Requested : Head #1 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    StrMsg = String.Format("[{0}] Direct Cut Requested : Head #2 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    StrMsg = String.Format("[{0}] Direct Cut Requested : Head #3 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    StrMsg = String.Format("[{0}] Direct Cut Requested : Head #4 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowDirectCut(ESTAGE_ANGLE EAngle, EVIEW EView, double F64Length)
        {
            try
            {
                string StrMsg = String.Format("[{0}]{1} Direct Cut Length = {2}", (int)EAngle, EView.ToString(), Math.Round(F64Length, 3));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowDirectCutNotFound(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Cannot found {1} Direct Cut Length", (int)EAngle, EView.ToString());
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SuccessDirectCut(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Success Direct Cut!", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailDirectCutByNotReady(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Direct cut due to Vision not ready", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailDirectCutByNotMatchedRecipe(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Direct cut due to Not matched recipe", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailDirectCutByViewCount(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Direct cut by need more than 1 View for Direct Cut", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailDirectCutByOutOfLength(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Direct Cut due to Length out of ranged", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailDirectCutByCannotFoundCutLine(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Direct Cut due to Cannot found cut line", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartCrossCut(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Start Cross Cut", (int)EAngle);
                this.OnSendMessage(StrMsg);

                if ((EView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    StrMsg = String.Format("[{0}] Cross Cut Requested : Head #1 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    StrMsg = String.Format("[{0}] Cross Cut Requested : Head #2 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    StrMsg = String.Format("[{0}] Cross Cut Requested : Head #3 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
                if ((EView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    StrMsg = String.Format("[{0}] Cross Cut Requested : Head #4 ", (int)EAngle);
                    this.OnSendMessage(StrMsg);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowCrossCut(ESTAGE_ANGLE EAngle, EVIEW EView, double F64Length)
        {
            try
            {
                string StrMsg = String.Format("[{0}]{1} Cross Cut Length = {2}", (int)EAngle, EView.ToString(), Math.Round(F64Length, 3));
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ShowCrossCutNotFound(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Cannot found {1} Cross Cut Length", (int)EAngle, EView.ToString());
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SuccessCrossCut(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Success Cross Cut!", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByNotReady(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross Cut due to Vision not ready", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByNotMatchedRecipe(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross cut due to Not matched recipe", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByViewCount(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross cut due to need more than 1 View for Cross Cut", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByNotExistedDirectCutResult(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross cut due to not existed Direct Cut Result", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByOutOfLength(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross Cut due to Length out of ranged", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FailCrossCutByCannotFoundCutLine(ESTAGE_ANGLE EAngle)
        {
            try
            {
                string StrMsg = String.Format("[{0}]Fail Cross Cut due to Cannot found cut line", (int)EAngle);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartScaleCalculation()
        {
            try
            {
                string StrMsg = String.Format("Start Scale Calculation");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void CalcScaleCalculation
        (
            string StrObject, 
            double F64LaserMin, double F64LaserMax, 
            double F64MachineMin, double F64MachineMax, 
            double F64Value, double F64Result
        )
        {
            try
            {
                string StrMsg = String.Format("{0} Scale Info", StrObject);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("{0} Laser Min = {1}, Max = {2}", StrObject, F64LaserMin, F64LaserMax);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("{0} Machine Min = {1}, Max = {2}", StrObject, F64MachineMin, F64MachineMax);
                this.OnSendMessage(StrMsg);

                StrMsg = String.Format("{0} Want&Result Min = {1}, Max = {2}", StrObject, F64Value, F64Result);
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void FinishScaleCalculation()
        {
            try
            {
                string StrMsg = String.Format("Finish Scale Calculation");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndInspection()
        {
            try
            {
                string StrMsg = String.Format("End Inspection");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ReadyOn()
        {
            try
            {
                string StrMsg = String.Format("Ready On");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void ReadyOff()
        {
            try
            {
                string StrMsg = String.Format("Ready Off");
                this.OnSendMessage(StrMsg);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnSendMessage(string StrMsg)
        {
            try
            {
                CLog OItem = new CLog(StrMsg);
                this.m_OFile.SetLog(OItem);

                if (this.m_OSendMessage != null)
                {
                    StrMsg = String.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), StrMsg);
                    this.m_OSendMessage(StrMsg);
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
