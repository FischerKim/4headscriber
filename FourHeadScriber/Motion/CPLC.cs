using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using System.Threading;

namespace FourHeadScriber
{
    public class CPLC : APLC, IDisposable
    {
        #region VARIABLE
        private short m_I16Value = 0;
        private short[] m_ArrI16Motion = null;
        private ushort[] m_ArrU16Motion = null;
        private ushort[] m_ArrU16BeforeMotion = null;

        private bool m_BMotionAlignmentStart = false;
        private bool m_BMotionAlignmentIsFirst = false;
        private bool m_BMotionDirectCutStart = false;
        private bool m_BMotionCrossCutStart = false;
        //private bool m_BMotionHead1ZeroEnabled = false;
        //private bool m_BMotionHead2ZeroEnabled = false;
        //private bool m_BMotionHead3ZeroEnabled = false;
        //private bool m_BMotionHead4ZeroEnabled = false;
        //private bool m_BMotionHead1NinetyEnabled = false;
        //private bool m_BMotionHead2NinetyEnabled = false;
        //private bool m_BMotionHead3NinetyEnabled = false;
        //private bool m_BMotionHead4NinetyEnabled = false;
        private bool m_BMotionCalibrationPossible = false;
        private bool m_BMotionCalibrationImpossible = false;
        private bool m_BMotionCalibrationMovementFinish = false;
        private bool m_BMotionScaleCalculation = false;

        private bool m_BBeforeMotionAlignmentStart = false;
        private bool m_BBeforeMotionDirectCutStart = false;
        private bool m_BBeforeMotionCrossCutStart = false;
        private bool m_BBeforeMotionCalibrationPossible = false;
        private bool m_BBeforeMotionCalibrationImpossible = false;
        private bool m_BBeforeMotionCalibrationMovementFinish = false;
        private bool m_BBeforeMotionScaleCalculation = false;

        private int m_I32VisionRecipeID = 0;
        private int m_I32MotionRecipeID = 0;
        private double m_F64MotionHead1Wheel = 0;
        private double m_F64MotionHead2Wheel = 0;
        private double m_F64MotionHead3Wheel = 0;
        private double m_F64MotionHead4Wheel = 0;
        private double m_F64MotionHead1ScaleValue = 0;
        private double m_F64MotionHead2ScaleValue = 0;
        private double m_F64MotionHead3ScaleValue = 0;
        private double m_F64MotionHead4ScaleValue = 0;
        private double m_F64BeforeMotionHead1Wheel = 0;
        private double m_F64BeforeMotionHead2Wheel = 0;
        private double m_F64BeforeMotionHead3Wheel = 0;
        private double m_F64BeforeMotionHead4Wheel = 0;

        private double m_F64ZeroLength = -1;
        private double m_F64NinetyLength = -1;
        private double m_F64BeforeZeroLength = -1;
        private double m_F64BeforeNinetyLength = -1;

        private bool m_BIsManualAlignment = false;
        
        private AlignmentRequestedHandler m_OAlignmentRequested = null;
        private ManualAlignmentRequestedHandler m_OManualAlignmentRequested = null;
        private DirectCutRequestedHandler m_ODirectCutRequested = null;
        private CrossCutRequestedHandler m_OCrossCutRequested = null;
        private CalibrationMoveRequestedHandler m_OCalibrationMovementRequested = null;
        private ChangeRecipeRequestedHandler m_OChangeRecipeRequested = null;
        private ChangeProductLengthRequestedHandler m_OChangeProductLengthRequested = null;
        private WheelPositionChangedHandler m_OWheelPositionChanged = null;
        private ScaleCalculationHandler m_OScaleCalculationRequested = null;
        #endregion


        #region DELEGATE & EVENT
        public delegate void ChangeRecipeRequestedHandler(int I32Index);
        public delegate void ChangeProductLengthRequestedHandler(double F64ZeroLength, double F64NinetyLength);
        public delegate void CalibrationMoveRequestedHandler(bool BEnabled);
        public delegate void AlignmentRequestedHandler(ESTAGE_ANGLE EAngle, EVIEW EView, bool BIsFirst);
        public delegate void ManualAlignmentRequestedHandler(ESTAGE_ANGLE EAngle, EVIEW EView);
        public delegate void DirectCutRequestedHandler(ESTAGE_ANGLE EAngle, EVIEW EView);
        public delegate void CrossCutRequestedHandler(ESTAGE_ANGLE EAngle, EVIEW EView);
        public delegate void WheelPositionChangedHandler(double F64Head1Wheel, double F64Head2Wheel, double F64Head3Wheel, double F64Head4Wheel);
        public delegate void ScaleCalculationHandler(double F64Head1, double F64Head2, double F64Head3, double F64Head4);
        #endregion


        #region PROPERTIES
        public int I32VisionRecipeID
        {
            get { return this.m_I32VisionRecipeID; }
            set { this.m_I32VisionRecipeID = value; }
        }


        public int I32MotionRecipeID
        {
            get { return this.m_I32MotionRecipeID; }
        }


        public double F64Head1Wheel
        {
            get { return this.m_F64BeforeMotionHead1Wheel; }
        }


        public double F64Head2Wheel
        {
            get { return this.m_F64BeforeMotionHead2Wheel; }
        }


        public double F64Head3Wheel
        {
            get { return this.m_F64BeforeMotionHead3Wheel; }
        }


        public double F64Head4Wheel
        {
            get { return this.m_F64BeforeMotionHead4Wheel; }
        }


        public double F64ZeroLength
        {
            get { return this.m_F64BeforeZeroLength;  }
        }


        public double F64NinetyLength
        {
            get { return this.m_F64BeforeNinetyLength; }
        }


        public bool BIsManualAlignment
        {
            get { return this.m_BIsManualAlignment; }
            set { this.m_BIsManualAlignment = value; }
        }


        public AlignmentRequestedHandler OAlignmentRequested
        {
            set { this.m_OAlignmentRequested = value; }
        }


        public ManualAlignmentRequestedHandler OManualAlignmentRequested
        {
            set { this.m_OManualAlignmentRequested = value; }
        }


        public DirectCutRequestedHandler ODirectCutRequested
        {
            set { this.m_ODirectCutRequested = value; }
        }


        public CrossCutRequestedHandler OCrossCutRequested
        {
            set { this.m_OCrossCutRequested = value; }
        }


        public CalibrationMoveRequestedHandler OCalibrationMovementRequested
        {
            set { this.m_OCalibrationMovementRequested = value; }
        }


        public ScaleCalculationHandler OScaleCalculationRequested
        {
            set { this.m_OScaleCalculationRequested = value; }
        }


        public ChangeRecipeRequestedHandler OChangeRecipeRequested
        {
            set { this.m_OChangeRecipeRequested = value; }
        }


        public ChangeProductLengthRequestedHandler OChangeProductLengthRequested
        {
            set { this.m_OChangeProductLengthRequested = value; }
        }


        public WheelPositionChangedHandler OWheelPositionChanged
        {
            set { this.m_OWheelPositionChanged = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CPLC(ConnectionCompletedHandler OConnectionCompleted)
            : base(OConnectionCompleted) { }
        #endregion


        #region FUNCTION
        protected override void Initialize()
        {
            try
            {
                this.m_ArrI16Motion = new short[6];
                this.m_ArrU16Motion = new ushort[6];
                this.m_ArrU16BeforeMotion = new ushort[6];

                Array.Clear(this.m_ArrI16Motion, 0, 6);
                Array.Clear(this.m_ArrU16Motion, 0, 6);
                Array.Clear(this.m_ArrU16BeforeMotion, 0, 6);

                this.m_I16Value = 0;
                base.m_ODevice.WriteDeviceRandom2("M8500", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8501", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8502", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8503", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8504", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8505", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8506", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8510", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8520", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8521", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8550", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8551", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8552", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8553", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        protected override void Read()
        {
            try
            {
                this.ReadBit();
                this.ReadWord();
                this.IsCameraDown();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void ReadBit()
        {
            try
            {
                int I32Connection = 0;
                bool BResult = this.GetMotionValue(out I32Connection);

                if (I32Connection == 0)
                {
                    if (BResult == false) return;

                    this.m_BMotionAlignmentStart = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.ALIGNMENT_CMD, EMOTION_BIT.ALIGNMENT_CMD);
                    this.m_BMotionAlignmentIsFirst = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.ALIGNMENT_IS_FIRST, EMOTION_BIT.ALIGNMENT_IS_FIRST);
                    this.m_BMotionScaleCalculation = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.SCALE_CALCULATION, EMOTION_BIT.SCALE_CALCULATION);
                    this.m_BMotionCrossCutStart = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.CROSS_CUT_CMD, EMOTION_BIT.CROSS_CUT_CMD);
                    this.m_BMotionDirectCutStart = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.DIRECT_CUT_CMD, EMOTION_BIT.DIRECT_CUT_CMD);
                    //this.m_BMotionHead1ZeroEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD1_ZERO_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD1_ZERO_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead2ZeroEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD2_ZERO_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD2_ZERO_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead3ZeroEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD3_ZERO_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD3_ZERO_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead4ZeroEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD4_ZERO_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD4_ZERO_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead1NinetyEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD1_NINETY_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD1_NINETY_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead2NinetyEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD2_NINETY_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD2_NINETY_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead3NinetyEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD3_NINETY_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD3_NINETY_ALIGNMENT_ENABLED);
                    //this.m_BMotionHead4NinetyEnabled = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.HEAD4_NINETY_ALIGNMENT_ENABLED, EMOTION_BIT.HEAD4_NINETY_ALIGNMENT_ENABLED);
                    this.m_BMotionCalibrationPossible = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.CALIBRATION_POSSIBLE, EMOTION_BIT.CALIBRATION_POSSIBLE);
                    this.m_BMotionCalibrationImpossible = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.CALIBRATION_IMPOSSIBLE, EMOTION_BIT.CALIBRATION_IMPOSSIBLE);
                    this.m_BMotionCalibrationMovementFinish = base.GetMotionBit(this.m_ArrU16Motion, EMOTION_INDEX.CALIBRATION_MOVE_COMPLETED, EMOTION_BIT.CALIBRATION_MOVE_COMPLETED);

                    //CALIBRATION 요청 기능 - 성공
                    if (this.m_BMotionCalibrationPossible != this.m_BBeforeMotionCalibrationPossible)
                    {
                        this.m_BBeforeMotionCalibrationPossible = this.m_BMotionCalibrationPossible;

                        if (this.m_BMotionCalibrationPossible == true)
                        {
                            this.OnCalibrationMoveRequested(true);
                        }
                        else if (this.m_BMotionCalibrationImpossible == false)
                        {
                            this.SetFinishCalibration();
                        }
                    }

                    //CALIBRATION 요청 기능 - 실패
                    if (this.m_BMotionCalibrationImpossible != this.m_BBeforeMotionCalibrationImpossible)
                    {
                        this.m_BBeforeMotionCalibrationImpossible = this.m_BMotionCalibrationImpossible;

                        if (this.m_BMotionCalibrationImpossible == true)
                        {
                            this.OnCalibrationMoveRequested(false);
                            this.SetFinishCalibration();
                        }
                    }

                    //CALIBRATION 이동 완료
                    if (this.m_BMotionCalibrationMovementFinish != this.m_BBeforeMotionCalibrationMovementFinish)
                    {
                        this.m_BBeforeMotionCalibrationMovementFinish = this.m_BMotionCalibrationMovementFinish;

                        if (this.m_BMotionCalibrationMovementFinish == true)
                        {
                            this.SetFinishCalibrationMovement();
                        }
                        else if (this.m_BMotionCalibrationMovementFinish == false)
                        {
                            this.OnCalibrationMoveRequested(true);
                        }
                    }


                    //ALIGNMENT 요청
                    if (this.m_BMotionAlignmentStart != this.m_BBeforeMotionAlignmentStart)
                    {
                        this.m_BBeforeMotionAlignmentStart = this.m_BMotionAlignmentStart;

                        if (this.m_BMotionAlignmentStart == true)
                        {
                            ESTAGE_ANGLE EAngle = this.GetStageAngle();
                            EVIEW EView = EVIEW.NONE;
                        
                                if (CEnvironment.It.StrEnabled1 == "true") EView |= EVIEW.HEAD1;
                                if (CEnvironment.It.StrEnabled2 == "true") EView |= EVIEW.HEAD2;
                                if (CEnvironment.It.StrEnabled3 == "true") EView |= EVIEW.HEAD3;
                                if (CEnvironment.It.StrEnabled4 == "true") EView |= EVIEW.HEAD4;
                            

                            if (this.m_BIsManualAlignment == false)
                            {
                                this.OnAlignmentRequested(EAngle, EView, this.m_BMotionAlignmentIsFirst);
                            }
                            else
                            {
                                this.m_BIsManualAlignment = false;
                                this.OnManualAlignmentRequested(EAngle, EView);
                            }
                        }
                        else if (this.m_BMotionAlignmentStart == false)
                        {
                            this.SetBusyOff();
                        }
                    }

                    //DIRECT CUT 요청
                    if (this.m_BMotionDirectCutStart != this.m_BBeforeMotionDirectCutStart)
                    {
                        this.m_BBeforeMotionDirectCutStart = this.m_BMotionDirectCutStart;

                        if (this.m_BMotionDirectCutStart == true)
                        {
                            ESTAGE_ANGLE EAngle = this.GetStageAngle();
                            EVIEW EView = EVIEW.NONE;

                            if (CEnvironment.It.StrEnabled1 == "true") EView |= EVIEW.HEAD1;
                            if (CEnvironment.It.StrEnabled2 == "true") EView |= EVIEW.HEAD2;
                            if (CEnvironment.It.StrEnabled3 == "true") EView |= EVIEW.HEAD3;
                            if (CEnvironment.It.StrEnabled4 == "true") EView |= EVIEW.HEAD4;
                           

                            this.OnDirectCutRequested(EAngle, EView);
                        }
                        else if (this.m_BMotionDirectCutStart == false)
                        {
                            this.SetBusyOff();
                        }
                    }

                    //CROSS CUT 요청
                    if (this.m_BMotionCrossCutStart != this.m_BBeforeMotionCrossCutStart)
                    {
                        this.m_BBeforeMotionCrossCutStart = this.m_BMotionCrossCutStart;

                        if (this.m_BMotionCrossCutStart == true)
                        {
                            ESTAGE_ANGLE EAngle = this.GetStageAngle();
                            EVIEW EView = EVIEW.NONE;

                            if (CEnvironment.It.StrEnabled1 == "true") EView |= EVIEW.HEAD1;
                            if (CEnvironment.It.StrEnabled2 == "true") EView |= EVIEW.HEAD2;
                            if (CEnvironment.It.StrEnabled3 == "true") EView |= EVIEW.HEAD3;
                            if (CEnvironment.It.StrEnabled4 == "true") EView |= EVIEW.HEAD4;

                            this.OnCrossCutRequested(EAngle, EView);
                        }
                        else if (this.m_BMotionCrossCutStart == false)
                        {
                            this.SetBusyOff();
                        }
                    }

                    //SCALE 연산 요청
                    if (this.m_BMotionScaleCalculation != this.m_BBeforeMotionScaleCalculation)
                    {
                        this.m_BBeforeMotionScaleCalculation = this.m_BMotionScaleCalculation;

                        if (this.m_BMotionScaleCalculation == true)
                        {
                            this.GetScaleValue
                            (
                                out this.m_F64MotionHead1ScaleValue,
                                out this.m_F64MotionHead2ScaleValue,
                                out this.m_F64MotionHead3ScaleValue,
                                out this.m_F64MotionHead4ScaleValue
                            );
                            this.OnScaleCalculationRequested
                            (
                                this.m_F64MotionHead1ScaleValue,
                                this.m_F64MotionHead2ScaleValue,
                                this.m_F64MotionHead3ScaleValue,
                                this.m_F64MotionHead4ScaleValue
                            );
                        }
                        else
                        {
                            this.ClearFinishScaleCalculation();
                            this.SetBusyOff();
                        }
                    }
                }
                else
                {
                    base.OnDisconnected(I32Connection);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private bool GetMotionValue(out int I32Connection)
        {
            bool BResult = false;
            I32Connection = 0;

            try
            {
                base.BeginSync();

                I32Connection = base.m_ODevice.ReadDeviceBlock2("M8000", 6, out this.m_ArrI16Motion[0]);

                for (int _Index = 0; _Index < this.m_ArrI16Motion.Length; _Index++)
                {
                    this.m_ArrU16Motion[_Index] = (ushort)this.m_ArrI16Motion[_Index];

                    if (this.m_ArrU16Motion[_Index] != this.m_ArrU16BeforeMotion[_Index])
                    {
                        this.m_ArrU16BeforeMotion[_Index] = this.m_ArrU16Motion[_Index];
                        BResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }

            return BResult;
        }


        private void IsCameraDown()
        {
            try
            {
                if (CEnvironment.It.StrEnabled1 == "false" ||
                    CEnvironment.It.StrEnabled2 == "false" ||
                    CEnvironment.It.StrEnabled3 == "false" ||
                    CEnvironment.It.StrEnabled4 == "false")
                {
                    base.m_ODevice.WriteDeviceRandom2("M8507", 1, ref this.m_I16Value);
                }
                else
                {
                    base.m_ODevice.WriteDeviceRandom2("M8507", 0, ref this.m_I16Value);
                }
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
                base.BeginSync();

                this.m_I16Value = 1;
                base.m_ODevice.WriteDeviceRandom2("M8500", 1, ref this.m_I16Value);
                CLoggingTool.It.ReadyOn();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void EndInspection()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 0;
                base.m_ODevice.WriteDeviceRandom2("M8500", 1, ref this.m_I16Value);
                CLoggingTool.It.ReadyOff();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetBusyOn()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                this.m_ODevice.WriteDeviceRandom2("M8501", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void SetBusyOff()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 0;
                this.m_ODevice.WriteDeviceRandom2("M8501", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetStartCalibration()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                this.m_ODevice.WriteDeviceRandom2("M8550", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetCalibrationMovement()
        {
            try
            {
                base.BeginSync();
                Thread.Sleep(300);
                this.m_I16Value = 1;
                this.m_ODevice.WriteDeviceRandom2("M8551", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void SetFinishCalibrationMovement()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 0;
                base.m_ODevice.WriteDeviceRandom2("M8551", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetSuccessCalibration()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                base.m_ODevice.WriteDeviceRandom2("M8552", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetFailCalibration()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                base.m_ODevice.WriteDeviceRandom2("M8553", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void SetFinishCalibration()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 0;
                base.m_ODevice.WriteDeviceRandom2("M8552", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8553", 1, ref this.m_I16Value);
                base.m_ODevice.WriteDeviceRandom2("M8550", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void FinishAlignment(ERESULT EResult)
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;

                switch (EResult)
                {
                    case ERESULT.RETRY:
                        base.m_ODevice.WriteDeviceRandom2("M8502", 1, ref this.m_I16Value);
                        break;

                    case ERESULT.OK:
                        base.m_ODevice.WriteDeviceRandom2("M8503", 1, ref this.m_I16Value);
                        break;

                    case ERESULT.LEFT_NG:
                        base.m_ODevice.WriteDeviceRandom2("M8504", 1, ref this.m_I16Value);
                        break;

                    case ERESULT.RIGHT_NG:
                        base.m_ODevice.WriteDeviceRandom2("M8505", 1, ref this.m_I16Value);
                        break;

                    case ERESULT.NG:
                        base.m_ODevice.WriteDeviceRandom2("M8506", 1, ref this.m_I16Value);
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void FinishCut(ERESULT EResult)
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                if (EResult == ERESULT.OK)
                {
                    base.m_ODevice.WriteDeviceRandom2("M8520", 1, ref this.m_I16Value);
                }
                else if (EResult == ERESULT.NG)
                {
                    base.m_ODevice.WriteDeviceRandom2("M8521", 1, ref this.m_I16Value);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void GetStageCurrent(out double F64Head1X, out double F64Head2X, out double F64Head3X, out double F64Head4X, out double F64Y, out double F64T)
        {
            F64Head1X = 0;
            F64Head2X = 0;
            F64Head3X = 0;
            F64Head4X = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[12];
                this.m_ODevice.ReadDeviceBlock2("D8000", 12, out ArrI16Value[0]);

                short[] ArrI16Head1X = new short[2];
                short[] ArrI16Head2X = new short[2];
                short[] ArrI16Head3X = new short[2];
                short[] ArrI16Head4X = new short[2];
                short[] ArrI16Y = new short[2];
                short[] ArrI16T = new short[2];
                ArrI16Y[0] = ArrI16Value[0];
                ArrI16Y[1] = ArrI16Value[1];
                ArrI16T[0] = ArrI16Value[2];
                ArrI16T[1] = ArrI16Value[3];
                ArrI16Head1X[0] = ArrI16Value[4];
                ArrI16Head1X[1] = ArrI16Value[5];
                ArrI16Head2X[0] = ArrI16Value[6];
                ArrI16Head2X[1] = ArrI16Value[7];
                ArrI16Head3X[0] = ArrI16Value[8];
                ArrI16Head3X[1] = ArrI16Value[9];
                ArrI16Head4X[0] = ArrI16Value[10];
                ArrI16Head4X[1] = ArrI16Value[11];

                int I32Head1X = this.ConvertValue(ArrI16Head1X);
                int I32Head2X = this.ConvertValue(ArrI16Head2X);
                int I32Head3X = this.ConvertValue(ArrI16Head3X);
                int I32Head4X = this.ConvertValue(ArrI16Head4X);
                int I32Y = this.ConvertValue(ArrI16Y);
                int I32T = this.ConvertValue(ArrI16T);

                F64Head1X = this.ConvertDecimal(I32Head1X, 4);
                F64Head2X = this.ConvertDecimal(I32Head2X, 4);
                F64Head3X = this.ConvertDecimal(I32Head3X, 4);
                F64Head4X = this.ConvertDecimal(I32Head4X, 4);
                F64Y = this.ConvertDecimal(I32Y, 4);
                F64T = this.ConvertDecimal(I32T, 5);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public ESTAGE_ANGLE GetStageAngle()
        {
            ESTAGE_ANGLE EResult = ESTAGE_ANGLE.NONE;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[2];
                base.m_ODevice.ReadDeviceBlock2("D8002", 2, out ArrI16Value[0]);

                int I32Value = this.ConvertValue(ArrI16Value);
                double F64Value = base.ConvertDecimal(I32Value, 5);

                EResult = (F64Value < 45) ? ESTAGE_ANGLE.ZERO : ESTAGE_ANGLE.NINETY;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }

            return EResult;
        }


        public void SetStageMovement(double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y, double F64T)
        {
            try
            {
                base.BeginSync();

                int I32Head1X = this.ConvertDecimal(F64Head1X, 4);
                int I32Head2X = this.ConvertDecimal(F64Head2X, 4);
                int I32Head3X = this.ConvertDecimal(F64Head3X, 4);
                int I32Head4X = this.ConvertDecimal(F64Head4X, 4);
                int I32Y = this.ConvertDecimal(F64Y, 4);
                int I32T = this.ConvertDecimal(F64T, 5);

                int[] ArrI32Head1X = this.ConvertValue(I32Head1X);
                int[] ArrI32Head2X = this.ConvertValue(I32Head2X);
                int[] ArrI32Head3X = this.ConvertValue(I32Head3X);
                int[] ArrI32Head4X = this.ConvertValue(I32Head4X);
                int[] ArrI32Y = this.ConvertValue(I32Y);
                int[] ArrI32T = this.ConvertValue(I32T);

                short[] ArrI16Value = new short[12];
                ArrI16Value[0] = (short)ArrI32Y[0];
                ArrI16Value[1] = (short)ArrI32Y[1];
                ArrI16Value[2] = (short)ArrI32T[0];
                ArrI16Value[3] = (short)ArrI32T[1];
                ArrI16Value[4] = (short)ArrI32Head1X[0];
                ArrI16Value[5] = (short)ArrI32Head1X[1];
                ArrI16Value[6] = (short)ArrI32Head2X[0];
                ArrI16Value[7] = (short)ArrI32Head2X[1];
                ArrI16Value[8] = (short)ArrI32Head3X[0];
                ArrI16Value[9] = (short)ArrI32Head3X[1];
                ArrI16Value[10] = (short)ArrI32Head4X[0];
                ArrI16Value[11] = (short)ArrI32Head4X[1];
                base.m_ODevice.WriteDeviceBlock2("D8500", 12, ref ArrI16Value[0]);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }

     
        public void SetStageMovement_ThreePoints(double F64Head1X, double F64Head2X, double F64Head3X, double F64Y, double F64T)
        {
            try
            {
                base.BeginSync();

                int I32Head1X = this.ConvertDecimal(F64Head1X, 4);
                int I32Head2X = this.ConvertDecimal(F64Head2X, 4);
                int I32Head3X = this.ConvertDecimal(F64Head3X, 4);
            //    int I32Head4X = this.ConvertDecimal(F64Head4X, 4);
                int I32Y = this.ConvertDecimal(F64Y, 4);
                int I32T = this.ConvertDecimal(F64T, 5);

                int[] ArrI32Head1X = this.ConvertValue(I32Head1X);
                int[] ArrI32Head2X = this.ConvertValue(I32Head2X);
                int[] ArrI32Head3X = this.ConvertValue(I32Head3X);
              //  int[] ArrI32Head4X = this.ConvertValue(I32Head4X);
                int[] ArrI32Y = this.ConvertValue(I32Y);
                int[] ArrI32T = this.ConvertValue(I32T);

                short[] ArrI16Value = new short[12];
                ArrI16Value[0] = (short)ArrI32Y[0];
                ArrI16Value[1] = (short)ArrI32Y[1];
                ArrI16Value[2] = (short)ArrI32T[0];
                ArrI16Value[3] = (short)ArrI32T[1];
                ArrI16Value[4] = (short)ArrI32Head1X[0];
                ArrI16Value[5] = (short)ArrI32Head1X[1];
                ArrI16Value[6] = (short)ArrI32Head2X[0];
                ArrI16Value[7] = (short)ArrI32Head2X[1];
                ArrI16Value[8] = (short)ArrI32Head3X[0];
                ArrI16Value[9] = (short)ArrI32Head3X[1];
                ArrI16Value[10] = 0; //(short)ArrI32Head4X[0];
                ArrI16Value[11] = 0; //(short)ArrI32Head4X[1];
                base.m_ODevice.WriteDeviceBlock2("D8500", 12, ref ArrI16Value[0]);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }

  

        public void GetScaleValue(out double F64Head1, out double F64Head2, out double F64Head3, out double F64Head4)
        {
            F64Head1 = 0;
            F64Head2 = 0;
            F64Head3 = 0;
            F64Head4 = 0;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[8];
                this.m_ODevice.ReadDeviceBlock2("D8050", 8, out ArrI16Value[0]);

                short[] ArrI16Head1 = new short[2];
                short[] ArrI16Head2 = new short[2];
                short[] ArrI16Head3 = new short[2];
                short[] ArrI16Head4 = new short[2];
                ArrI16Head1[0] = ArrI16Value[0];
                ArrI16Head1[1] = ArrI16Value[1];
                ArrI16Head2[0] = ArrI16Value[2];
                ArrI16Head2[1] = ArrI16Value[3];
                ArrI16Head3[0] = ArrI16Value[4];
                ArrI16Head3[1] = ArrI16Value[5];
                ArrI16Head4[0] = ArrI16Value[6];
                ArrI16Head4[1] = ArrI16Value[7];

                int I32Head1 = this.ConvertValue(ArrI16Head1);
                int I32Head2 = this.ConvertValue(ArrI16Head2);
                int I32Head3 = this.ConvertValue(ArrI16Head3);
                int I32Head4 = this.ConvertValue(ArrI16Head4);

                F64Head1 = this.ConvertDecimal(I32Head1, 4);
                F64Head2 = this.ConvertDecimal(I32Head2, 4);
                F64Head3 = this.ConvertDecimal(I32Head3, 4);
                F64Head4 = this.ConvertDecimal(I32Head4, 4);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void SetScaleResult(double F64Head1, double F64Head2, double F64Head3, double F64Head4)
        {
            try
            {
                base.BeginSync();

                int I32Head1 = this.ConvertDecimal(F64Head1, 4);
                int I32Head2 = this.ConvertDecimal(F64Head2, 4);
                int I32Head3 = this.ConvertDecimal(F64Head3, 4);
                int I32Head4 = this.ConvertDecimal(F64Head4, 4);

                int[] ArrI32Head1 = this.ConvertValue(I32Head1);
                int[] ArrI32Head2 = this.ConvertValue(I32Head2);
                int[] ArrI32Head3 = this.ConvertValue(I32Head3);
                int[] ArrI32Head4 = this.ConvertValue(I32Head4);

                short[] ArrI16Value = new short[8];
                ArrI16Value[0] = (short)ArrI32Head1[0];
                ArrI16Value[1] = (short)ArrI32Head1[1];
                ArrI16Value[2] = (short)ArrI32Head2[0];
                ArrI16Value[3] = (short)ArrI32Head2[1];
                ArrI16Value[4] = (short)ArrI32Head3[0];
                ArrI16Value[5] = (short)ArrI32Head3[1];
                ArrI16Value[6] = (short)ArrI32Head4[0];
                ArrI16Value[7] = (short)ArrI32Head4[1];
                base.m_ODevice.WriteDeviceBlock2("D8550", 8, ref ArrI16Value[0]);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void FinishScaleCalculation()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 1;
                base.m_ODevice.WriteDeviceRandom2("M8510", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void ClearFinishScaleCalculation()
        {
            try
            {
                base.BeginSync();

                this.m_I16Value = 0;
                base.m_ODevice.WriteDeviceRandom2("M8510", 1, ref this.m_I16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void ReadWord()
        {
            try
            {
                //레시피 변경 요청
                this.m_I32MotionRecipeID = this.GetMotionRecipeID();
                if (this.m_I32VisionRecipeID != this.m_I32MotionRecipeID)
                {
                    this.OnChangeRecipeRequested(this.m_I32MotionRecipeID);
                }
                //1217
                this.GetProductLength(out this.m_F64BeforeZeroLength, out this.m_F64BeforeNinetyLength);
                if (this.m_F64ZeroLength != this.m_F64BeforeZeroLength ||
                    this.m_F64NinetyLength != this.m_F64BeforeNinetyLength)
                {
                    this.m_F64ZeroLength = this.m_F64BeforeZeroLength;
                    this.m_F64NinetyLength = this.m_F64BeforeNinetyLength;

                    if (this.m_F64ZeroLength > 0 && this.m_F64NinetyLength > 0)
                    {
                        this.OnChangeProductLengthRequested(this.m_F64ZeroLength, this.F64NinetyLength);
                    }
                }

                //Wheel 위치 변경 요청
                this.GetWheelPosition
                (
                    out this.m_F64MotionHead1Wheel,
                    out this.m_F64MotionHead2Wheel,
                    out this.m_F64MotionHead3Wheel,
                    out this.m_F64MotionHead4Wheel
                );
                if ((this.m_F64MotionHead1Wheel != this.m_F64BeforeMotionHead1Wheel) ||
                    (this.m_F64MotionHead2Wheel != this.m_F64BeforeMotionHead2Wheel) ||
                    (this.m_F64MotionHead3Wheel != this.m_F64BeforeMotionHead3Wheel) ||
                    (this.m_F64MotionHead4Wheel != this.m_F64BeforeMotionHead4Wheel))
                {
                    this.m_F64BeforeMotionHead1Wheel = this.m_F64MotionHead1Wheel;
                    this.m_F64BeforeMotionHead2Wheel = this.m_F64MotionHead2Wheel;
                    this.m_F64BeforeMotionHead3Wheel = this.m_F64MotionHead3Wheel;
                    this.m_F64BeforeMotionHead4Wheel = this.m_F64MotionHead4Wheel;

                    this.OnWheelPositionChanged
                    (
                        this.m_F64MotionHead1Wheel,
                        this.m_F64MotionHead2Wheel,
                        this.m_F64MotionHead3Wheel,
                        this.m_F64MotionHead4Wheel
                    );
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public int GetMotionRecipeID()
        {
            int I32Result = 0;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[2];
                base.m_ODevice.ReadDeviceBlock2("D8030", 2, out ArrI16Value[0]);

                I32Result = this.ConvertValue(ArrI16Value);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }

            return I32Result;
        }


        //1217
        public void GetProductLength(out double F64ZeroLength, out double F64NinetyLength)
        {
            F64ZeroLength = 0;
            F64NinetyLength = 0;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[4];
              //  base.m_ODevice.ReadDeviceBlock2("D8020??", 4, out ArrI16Value[0]); //asdf

                short[] ArrZeroLength = new short[2];
                short[] ArrNinetyLength = new short[2];
                ArrZeroLength[0] = ArrI16Value[0];
                ArrZeroLength[1] = ArrI16Value[1];
                ArrNinetyLength[0] = ArrI16Value[2];
                ArrNinetyLength[1] = ArrI16Value[3];
  
                int I32ZeroLength = this.ConvertValue(ArrZeroLength);
                int I32NinetyLength = this.ConvertValue(ArrNinetyLength);

                F64ZeroLength = this.ConvertDecimal(I32ZeroLength, 4);
                F64NinetyLength = this.ConvertDecimal(I32NinetyLength, 4);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        public void GetWheelPosition(out double F64Head1Wheel, out double F64Head2Wheel, out double F64Head3Wheel, out double F64Head4Wheel)
        {
            F64Head1Wheel = 0;
            F64Head2Wheel = 0;
            F64Head3Wheel = 0;
            F64Head4Wheel = 0;

            try
            {
                base.BeginSync();

                short[] ArrI16Value = new short[8];
                base.m_ODevice.ReadDeviceBlock2("D8020", 8, out ArrI16Value[0]);

                short[] ArrHead1Wheel = new short[2];
                short[] ArrHead2Wheel = new short[2];
                short[] ArrHead3Wheel = new short[2];
                short[] ArrHead4Wheel = new short[2];
                ArrHead1Wheel[0] = ArrI16Value[0];
                ArrHead1Wheel[1] = ArrI16Value[1];
                ArrHead2Wheel[0] = ArrI16Value[2];
                ArrHead2Wheel[1] = ArrI16Value[3];
                ArrHead3Wheel[0] = ArrI16Value[4];
                ArrHead3Wheel[1] = ArrI16Value[5];
                ArrHead4Wheel[0] = ArrI16Value[6];
                ArrHead4Wheel[1] = ArrI16Value[7];

                int I32Head1Wheel = this.ConvertValue(ArrHead1Wheel);
                int I32Head2Wheel = this.ConvertValue(ArrHead2Wheel);
                int I32Head3Wheel = this.ConvertValue(ArrHead3Wheel);
                int I32Head4Wheel = this.ConvertValue(ArrHead4Wheel);

                F64Head1Wheel = this.ConvertDecimal(I32Head1Wheel, 4);
                F64Head2Wheel = this.ConvertDecimal(I32Head2Wheel, 4);
                F64Head3Wheel = this.ConvertDecimal(I32Head3Wheel, 4);
                F64Head4Wheel = this.ConvertDecimal(I32Head4Wheel, 4);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                base.EndSync();
            }
        }


        private void OnChangeRecipeRequested(int I32RecipeID)
        {
            try
            {
                if (this.m_OChangeRecipeRequested != null)
                {
                    this.m_OChangeRecipeRequested(I32RecipeID);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnChangeProductLengthRequested(double ZeroLength, double NinetyLength)
        {
            try
            {
                if (this.m_OChangeProductLengthRequested != null)
                {
                    this.m_OChangeProductLengthRequested(ZeroLength, NinetyLength);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnCalibrationMoveRequested(bool BEnabled)
        {
            try
            {
                if (this.m_OCalibrationMovementRequested != null)
                {
                    this.m_OCalibrationMovementRequested(BEnabled);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnAlignmentRequested(ESTAGE_ANGLE EAngle, EVIEW EView, bool BIsFirst)
        {
            try
            {
                if (this.m_OAlignmentRequested != null)
                {
                    this.m_OAlignmentRequested(EAngle, EView, BIsFirst);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnManualAlignmentRequested(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                if (this.m_OManualAlignmentRequested != null)
                {
                    this.m_OManualAlignmentRequested(EAngle, EView);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnDirectCutRequested(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                if (this.m_ODirectCutRequested != null)
                {
                    this.m_ODirectCutRequested(EAngle, EView);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnCrossCutRequested(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                if (this.m_OCrossCutRequested != null)
                {
                    this.m_OCrossCutRequested(EAngle, EView);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnScaleCalculationRequested(double F64Head1, double F64Head2, double F64Head3, double F64Head4)
        {
            try
            {
                if (this.m_OScaleCalculationRequested != null)
                {
                    this.m_OScaleCalculationRequested(F64Head1, F64Head2, F64Head3, F64Head4);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnWheelPositionChanged(double F64Head1Wheel, double F64Head2Wheel, double F64Head3Wheel, double F64Head4Wheel)
        {
            try
            {
                if (this.m_OWheelPositionChanged != null)
                {
                    this.m_OWheelPositionChanged(F64Head1Wheel, F64Head2Wheel, F64Head3Wheel, F64Head4Wheel);
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
