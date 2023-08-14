using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using System.Threading;
using Jhjo.Tool;
using System.Drawing.Imaging;
using Jhjo.DB;
using System.Data;
using Cognex.VisionPro;

namespace FourHeadScriber
{
    public class CAlignmentTool : IDisposable
    {
        #region CONST
        private const long WAIT_TICK = 100000000;
        #endregion


        #region VARIABLE
        private bool m_flag = true;
        //BASE
        private CAnalysisTool m_OHead1 = null;
        private CAnalysisTool m_OHead2 = null;
        private CAnalysisTool m_OHead3 = null;
        private CAnalysisTool m_OHead4 = null;
        private C4HeadAlignAlgorithm m_OZeroAlgorithm = null;
        private C4HeadAlignAlgorithm m_ONinetyAlgorithm = null;
        private CRecipe m_ORecipe = null;
        private object m_OInterrupt = null;


        //COMMON
        private EANALYSIS m_EAnalysis = EANALYSIS.NONE;


        //CALIBRATION
        private ESTAGE_ANGLE m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
        private CAlignmentRecipe m_OCalibrationRecipe = null;
        private C4HeadAlignAlgorithm m_OCalibrationAlgorithm = null;
        private bool m_BStartCalibration = false;
        private bool m_BRunCalibration = false;
        private int m_I32CalibrationCount = 0;
        private int m_I32CalibrationIndex = 0;
        private long m_I64StartMovementTick = long.MaxValue;

        private double m_F64CalibrationMotionStandardHead1X = 0;
        private double m_F64CalibrationMotionStandardHead2X = 0;
        private double m_F64CalibrationMotionStandardHead3X = 0;
        private double m_F64CalibrationMotionStandardHead4X = 0;
        private double m_F64CalibrationMotionStandardY = 0;
        private double m_F64CalibrationMotionStandardT = 0;
        
        private double m_F64CalibrationX = 0;
        private double m_F64CalibrationY = 0;
        private double m_F64CalibrationT = 0;
        private double m_F64BeforeCalibrationX = 0;
        private double m_F64BeforeCalibrationY = 0;
        private double m_F64BeforeCalibrationT = 0;
        private double m_F64CalibrationMotionHead1X = 0;
        private double m_F64CalibrationMotionHead2X = 0;
        private double m_F64CalibrationMotionHead3X = 0;
        private double m_F64CalibrationMotionHead4X = 0;
        private double m_F64CalibrationMotionY = 0;
        private double m_F64CalibrationMotionT = 0;


        //ALIGNMENT
        private ESTAGE_ANGLE m_EAlignmentAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EAlignmentView = EVIEW.NONE;
        private CAlignmentRecipe m_OAlignmentRecipe = null;
        private C4HeadAlignAlgorithm m_OAlignmentAlgorithm = null;
        private bool m_BRunAlignment = false;
        private long m_I64AlignmentRequestedTick = long.MaxValue;
        private bool m_BUseAlignSharp = true;
        private double m_F64AlignmentMotionCurrentHead1X = 0;
        private double m_F64AlignmentMotionCurrentHead2X = 0;
        private double m_F64AlignmentMotionCurrentHead3X = 0;
        private double m_F64AlignmentMotionCurrentHead4X = 0;
        private double m_F64AlignmentMotionCurrentY = 0;
        private double m_F64AlignmentMotionCurrentT = 0;
        private double m_F64AlignmentMotionHead1X = 0;
        private double m_F64AlignmentMotionHead2X = 0;
        private double m_F64AlignmentMotionHead3X = 0;
        private double m_F64AlignmentMotionHead4X = 0;
        private double m_F64AlignmentMotionY = 0;
        private double m_F64AlignmentMotionT = 0;
        //private bool m_BAlignSharp = true;

        //1217
        //private double m_F64AlignmentBeforeMotionHead1X = 0;
        //private double m_F64AlignmentBeforeMotionHead2X = 0;
        //private double m_F64AlignmentBeforeMotionHead3X = 0;
        //private double m_F64AlignmentBeforeMotionHead4X = 0;
        //private double m_F64AlignmentBeforeMotionY = 0;
        //private double m_F64AlignmentBeforeMotionT = 360;
        //private int m_I32CountBefore = 1;

        private int m_I32AutoCheckX_AlignS_auto = 1;
        private int m_I32AutoCheckT_AlignS_auto = 1;
        private int m_I32AutoCheckY_AlignS_auto = 1;
        private int m_I32AutoCheckX_AlignX_auto = 1;
        private int m_I32AutoCheckT_AlignX_auto = 1;
        private int m_I32AutoCheckY_AlignX_auto = 1;

        //private int m_I32AutoCheckX_AlignS_auto = 1;
        //private int m_I32AutoCheckT_AlignS_manual = 1;
        //private int m_I32AutoCheckY_AlignS_manual = 1;
        //private int m_I32AutoCheckX_AlignX_auto = 1;
        //private int m_I32AutoCheckT_AlignX_manual = 1;
        //private int m_I32AutoCheckY_AlignX_manual = 1;
        //DIRECT CUT
        private ESTAGE_ANGLE m_EDirectCutAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EDirectCutView = EVIEW.NONE;
        private CAlignmentRecipe m_ODirectCutRecipe = null;
        private bool m_BRunDirectCut = false;
        private CDirectCutResult m_ODirectCutResult = null;


        //CROSS CUT
        private ESTAGE_ANGLE m_ECrossCutAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_ECrossCutView = EVIEW.NONE;
        private CAlignmentRecipe m_OCrossCutRecipe = null;
        private bool m_BRunCrossCut = false;


        //SCALE
        private DataTable m_OScaleHead1 = null;
        private DataTable m_OScaleHead2 = null;
        private DataTable m_OScaleHead3 = null;
        private DataTable m_OScaleHead4 = null;


        //THREAD
        private Thread m_OWorker = null;
        private bool m_BDoWork = false;


        //CALLBACK
        private FinishCalibrationMovementHandler m_OFinishCalibrationMovement = null;
        private FinishCalibrationHandler m_OFinishCalibration = null;
        private FinishAlignmentHandler m_OFinishAlignment = null;
        private FinishDirectCutHandler m_OFinishDirectCut = null;
        private FinishCrossCutHandler m_OFinishCrossCut = null;

        private int MAXRETRY = 8;
        private int CURRENTRETRY = 0;
        private bool m_BIndividual = false;
        #endregion


        #region DELEGATE & EVENT
        public delegate void FinishCalibrationMovementHandler(CCalibrationResult OResult);
        public delegate void FinishCalibrationHandler(bool BIsSuccess);
        public delegate void FinishAlignmentHandler(CAlignmentResult OResult);
        public delegate void FinishDirectCutHandler(CDirectCutResult OResult);
        public delegate void FinishCrossCutHandler(CCrossCutResult OResult);
        #endregion


        #region PROPERTIES
        public bool Flag
        {
            set
            {
                this.m_flag = value;
            }
        }
        public bool BIndividual
        {
            set
            {
                this.m_BIndividual = value;
            }
        }
        public int IntCURRENTRETRY
        {
            set
            {
                this.CURRENTRETRY = value;
            }
        }

        public int IntMAXTRY
        {
            get { return this.MAXRETRY; }
            set
            {
                this.MAXRETRY = value;
            }
        }


        //public bool BAlignSharp
        //{
        //    set
        //    {
        //        this.m_BAlignSharp = value;
        //    }
        //}

        public int I32AutoCheckX_AlignS_auto
        {
            set
            {
                this.m_I32AutoCheckX_AlignS_auto = value;
            }
        }

        //public int I32AutoCheckX_AlignS_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckX_AlignS_auto = value;
        //    }
        //}

        public int I32AutoCheckX_AlignX_auto
        {
            set
            {
                this.m_I32AutoCheckX_AlignX_auto = value;
            }
        }

        //public int I32AutoCheckX_AlignX_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckX_AlignX_auto = value;
        //    }
        //}

        public int I32AutoCheckY_AlignS_auto
        {
            set
            {
                this.m_I32AutoCheckY_AlignS_auto = value;
            }
        }

        public int I32AutoCheckY_AlignX_auto
        {
            set
            {
                this.m_I32AutoCheckY_AlignX_auto = value;
            }
        }

        //public int I32AutoCheckY_AlignS_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckY_AlignS_manual = value;
        //    }
        //}

        //public int I32AutoCheckY_AlignX_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckY_AlignX_manual = value;
        //    }
        //}

        public int I32AutoCheckT_AlignS_auto
        {
            set
            {
                this.m_I32AutoCheckT_AlignS_auto = value;
            }
        }

        public int I32AutoCheckT_AlignX_auto
        {
            set
            {
                this.m_I32AutoCheckT_AlignX_auto = value;
            }
        }

        //public int I32AutoCheckT_AlignS_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckT_AlignS_manual = value;
        //    }
        //}

        //public int I32AutoCheckT_AlignX_manual
        //{
        //    set
        //    {
        //        this.m_I32AutoCheckT_AlignX_manual = value;
        //    }
        //}
        public CAnalysisTool OHead1
        {
            get { return this.m_OHead1; }
        }
        

        public CAnalysisTool OHead2
        {
            get { return this.m_OHead2; }
        }


        public CAnalysisTool OHead3
        {
            get { return this.m_OHead3; }
        }


        public CAnalysisTool OHead4
        {
            get { return this.m_OHead4; }
        }


        public CRecipe ORecipe
        {
            set 
            {
                try
                {
                    this.m_ORecipe = value;

                    if (this.m_ORecipe != null)
                    {
                        this.m_OZeroAlgorithm.LoadFile(this.m_ORecipe.OZero.StrDirectory);
                        this.m_ONinetyAlgorithm.LoadFile(this.m_ORecipe.ONinety.StrDirectory);
                    }
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
            }
        }


        public DataTable OScaleHead1
        {
            set { this.m_OScaleHead1 = value; }
        }


        public DataTable OScaleHead2
        {
            set { this.m_OScaleHead2 = value; }
        }


        public DataTable OScaleHead3
        {
            set { this.m_OScaleHead3 = value; }
        }


        public DataTable OScaleHead4
        {
            set { this.m_OScaleHead4 = value; }
        }


        public FinishCalibrationMovementHandler OFinishCalibrationMovement
        {
            set { this.m_OFinishCalibrationMovement = value; }
        }


        public FinishCalibrationHandler OFinishCalibration
        {
            set { this.m_OFinishCalibration = value; }
        }


        public FinishAlignmentHandler OFinishAlignment
        {
            set { this.m_OFinishAlignment = value; }
        }


        public FinishDirectCutHandler OFinishDirectCut
        {
            set { this.m_OFinishDirectCut = value; }
        }


        public FinishCrossCutHandler OFinishCrossCut
        {
            set { this.m_OFinishCrossCut = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAlignmentTool(C4HeadAlignAlgorithm OZeroAlgorithm, C4HeadAlignAlgorithm ONinetyAlgorithm)
        {
            try
            {
                this.m_OHead1 = new CAnalysisTool(EVIEW.HEAD1);
                this.m_OHead2 = new CAnalysisTool(EVIEW.HEAD2);
                this.m_OHead3 = new CAnalysisTool(EVIEW.HEAD3);
                this.m_OHead4 = new CAnalysisTool(EVIEW.HEAD4);
                this.m_OZeroAlgorithm = OZeroAlgorithm;
                this.m_OZeroAlgorithm.SetStageKind(EKindOfStage.XYT);
                this.m_OZeroAlgorithm.SetMaxViewCount(2);
                this.m_OZeroAlgorithm.SetUseViewCount(2);
                this.m_ONinetyAlgorithm = ONinetyAlgorithm;
                this.m_ONinetyAlgorithm.SetStageKind(EKindOfStage.XYT);
                this.m_ONinetyAlgorithm.SetMaxViewCount(2);
                this.m_ONinetyAlgorithm.SetUseViewCount(2);
                this.m_OInterrupt = new object();

                CMotionController.It.OPLC.OCalibrationMovementRequested = this.OPLC_OCalibrationMovementRequested;
                CMotionController.It.OPLC.OAlignmentRequested = this.OPLC_OAlignmentRequested;
                CMotionController.It.OPLC.ODirectCutRequested = this.OPLC_ODirectCutRequested;
                CMotionController.It.OPLC.OCrossCutRequested = this.OPLC_OCrossCutRequested;
                CMotionController.It.OPLC.OScaleCalculationRequested = this.OPLC_OScaleCalculationRequested;

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CAlignmentTool()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region EVENT
        private void OPLC_OCalibrationMovementRequested(bool BEnabled)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_EAnalysis != EANALYSIS.CALIBRATION) return;
                
                if (BEnabled == true)
                {
                    if (CEnvironment.It.StrEnabled1 == "true")
                    {
                        this.m_OHead1.RequestMark();
                    }
                    if (CEnvironment.It.StrEnabled2 == "true")
                    {
                        this.m_OHead2.RequestMark();
                    }
                    if (CEnvironment.It.StrEnabled3 == "true")
                    {
                        this.m_OHead3.RequestMark();
                    }
                    if (CEnvironment.It.StrEnabled4 == "true")
                    {
                        this.m_OHead4.RequestMark();
                    }
                    this.m_BRunCalibration = true; //here
                    CLoggingTool.It.NextCalibrationMovement(this.m_ECalibrationAngle, this.m_I32CalibrationIndex, this.m_I32CalibrationCount);
                }
                else if (BEnabled == false)
                {
                    CLoggingTool.It.FailCalibrationByMotion(this.m_ECalibrationAngle);

                    this.m_EAnalysis = EANALYSIS.NONE;
                    this.m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
                    this.m_OCalibrationAlgorithm = null;
                    this.m_I32CalibrationCount = 0;
                    this.m_I32CalibrationIndex = 0;
                    this.m_BStartCalibration = false;
                    this.m_BRunCalibration = false;
                    this.m_I64StartMovementTick = long.MaxValue;
                    this.OnFinishCalibration(false);
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
        }


        private void OPLC_OAlignmentRequested(ESTAGE_ANGLE EAlignmentAngle, EVIEW EAlignmentView, bool BIsFirst)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CMotionController.It.OPLC.SetBusyOn();
                CLoggingTool.It.StartAlignment(EAlignmentAngle);
                Thread.Sleep(100);

                //if (BIsFirst == true) 20170411
                    this.m_BUseAlignSharp = true;
                this.m_I64AlignmentRequestedTick = DateTime.Now.Ticks;

                if (CMotionController.It.OPLC.I32VisionRecipeID != CMotionController.It.OPLC.I32MotionRecipeID)
                {
                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                    CLoggingTool.It.FailAlignmentByNotMatchedRecipe(EAlignmentAngle);
                    return;
                }
                if ((this.m_EAnalysis != EANALYSIS.INSPECTION) ||
                    (this.m_BRunAlignment == true) ||
                    (this.m_BRunDirectCut == true) ||
                    (this.m_BRunCrossCut == true))
                {
                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                    CLoggingTool.It.FailAlignmentByNotReady(EAlignmentAngle);
                    return;
                }
                if ((EAlignmentView == EVIEW.NONE) ||
                    (EAlignmentView == EVIEW.HEAD1) ||
                    (EAlignmentView == EVIEW.HEAD2) ||
                    (EAlignmentView == EVIEW.HEAD3) ||
                    (EAlignmentView == EVIEW.HEAD4))
                {
                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                    CLoggingTool.It.FailAlignmentByViewCount(EAlignmentAngle);
                    return;
                }


                if (EAlignmentAngle == ESTAGE_ANGLE.ZERO)
                {
                    this.m_EAlignmentAngle = EAlignmentAngle;
                    this.m_EAlignmentView = EAlignmentView;
                    this.m_OAlignmentRecipe = this.m_ORecipe.OZero;
                    this.m_OAlignmentAlgorithm = this.m_OZeroAlgorithm;
                    this.m_BRunAlignment = true;
                }
                else if (EAlignmentAngle == ESTAGE_ANGLE.NINETY)
                {
                    this.m_EAlignmentAngle = EAlignmentAngle;
                    this.m_EAlignmentView = EAlignmentView;
                    this.m_OAlignmentRecipe = this.m_ORecipe.ONinety;
                    this.m_OAlignmentAlgorithm = this.m_ONinetyAlgorithm;
                    this.m_BRunAlignment = true;
                }

                if ((EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    this.m_OHead1.ORecipe = this.m_OAlignmentRecipe.OHead1;
                    this.m_OHead1.RequestMark();
                }
                if ((EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    this.m_OHead2.ORecipe = this.m_OAlignmentRecipe.OHead2;
                    this.m_OHead2.RequestMark();
                }
                if ((EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    this.m_OHead3.ORecipe = this.m_OAlignmentRecipe.OHead3;
                    this.m_OHead3.RequestMark();
                }
                if ((EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    this.m_OHead4.ORecipe = this.m_OAlignmentRecipe.OHead4;
                    this.m_OHead4.RequestMark();
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
        }


        private void OPLC_ODirectCutRequested(ESTAGE_ANGLE EDirectCutAngle, EVIEW EDirectCutView)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CMotionController.It.OPLC.SetBusyOn();
                CLoggingTool.It.StartDirectCut(EDirectCutAngle, EDirectCutView);

                if (CMotionController.It.OPLC.I32VisionRecipeID != CMotionController.It.OPLC.I32MotionRecipeID)
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailDirectCutByNotMatchedRecipe(EDirectCutAngle);
                    return;
                }
                if ((this.m_EAnalysis != EANALYSIS.INSPECTION) ||
                    (this.m_BRunAlignment == true) ||
                    (this.m_BRunDirectCut == true) ||
                    (this.m_BRunCrossCut == true))
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailDirectCutByNotReady(EDirectCutAngle);
                    return;
                }
                if (EDirectCutView == EVIEW.NONE)
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailDirectCutByViewCount(EDirectCutAngle);
                    return;
                }

                if (EDirectCutAngle == ESTAGE_ANGLE.ZERO)
                {
                    this.m_EDirectCutAngle = EDirectCutAngle;
                    this.m_EDirectCutView = EDirectCutView;
                    this.m_ODirectCutRecipe = this.m_ORecipe.OZero;
                    this.m_BRunDirectCut = true;
                }
                else if (EDirectCutAngle == ESTAGE_ANGLE.NINETY)
                {
                    this.m_EDirectCutAngle = EDirectCutAngle;
                    this.m_EDirectCutView = EDirectCutView;
                    this.m_ODirectCutRecipe = this.m_ORecipe.ONinety;
                    this.m_BRunDirectCut = true;
                }

                this.m_OHead1.SetNullToDirectCutResult();
                this.m_OHead2.SetNullToDirectCutResult();
                this.m_OHead3.SetNullToDirectCutResult();
                this.m_OHead4.SetNullToDirectCutResult();

                if ((EDirectCutView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    this.m_OHead1.ORecipe = this.m_ODirectCutRecipe.OHead1;
                    this.m_OHead1.RequestDirectCut();
                }
                if ((EDirectCutView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    this.m_OHead2.ORecipe = this.m_ODirectCutRecipe.OHead2;
                    this.m_OHead2.RequestDirectCut();
                }
                if ((EDirectCutView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    this.m_OHead3.ORecipe = this.m_ODirectCutRecipe.OHead3;
                    this.m_OHead3.RequestDirectCut();
                }
                if ((EDirectCutView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    this.m_OHead4.ORecipe = this.m_ODirectCutRecipe.OHead4;
                    this.m_OHead4.RequestDirectCut();
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
        }


        private void OPLC_OCrossCutRequested(ESTAGE_ANGLE ECrossCutAngle, EVIEW ECrossCutView)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CMotionController.It.OPLC.SetBusyOn();
                CLoggingTool.It.StartCrossCut(ECrossCutAngle, ECrossCutView);

                if (CMotionController.It.OPLC.I32VisionRecipeID != CMotionController.It.OPLC.I32MotionRecipeID)
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailCrossCutByNotMatchedRecipe(ECrossCutAngle);
                    return;
                }
                if ((this.m_EAnalysis != EANALYSIS.INSPECTION) ||
                    (this.m_BRunAlignment == true) ||
                    (this.m_BRunDirectCut == true) ||
                    (this.m_BRunCrossCut == true))
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailCrossCutByNotReady(ECrossCutAngle);
                    return;
                }
                if (ECrossCutView == EVIEW.NONE)
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailCrossCutByViewCount(ECrossCutAngle);
                    return;
                }

                if ((this.m_ODirectCutResult == null) ||
                    ((this.m_ODirectCutResult.EAngle != ECrossCutAngle) || 
                    (this.m_ODirectCutResult.EView != ECrossCutView) || 
                    (this.m_ODirectCutResult.EResult == ERESULT.NG)))
                {
                    CMotionController.It.OPLC.FinishCut(ERESULT.NG);
                    CLoggingTool.It.FailCrossCutByNotExistedDirectCutResult(ECrossCutAngle);
                    return;
                }
                if (ECrossCutAngle == ESTAGE_ANGLE.ZERO)
                {
                    this.m_ECrossCutAngle = ECrossCutAngle;
                    this.m_ECrossCutView = ECrossCutView;
                    this.m_OCrossCutRecipe = this.m_ORecipe.OZero;
                    this.m_BRunCrossCut = true;
                }
                else if (ECrossCutAngle == ESTAGE_ANGLE.NINETY)
                {
                    this.m_ECrossCutAngle = ECrossCutAngle;
                    this.m_ECrossCutView = ECrossCutView;
                    this.m_OCrossCutRecipe = this.m_ORecipe.ONinety;
                    this.m_BRunCrossCut = true;
                }

                this.m_OHead1.SetNullToCrossCutResult();
                this.m_OHead2.SetNullToCrossCutResult();
                this.m_OHead3.SetNullToCrossCutResult();
                this.m_OHead4.SetNullToCrossCutResult();

                if ((ECrossCutView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    this.m_OHead1.ORecipe = this.m_OCrossCutRecipe.OHead1;
                    this.m_OHead1.RequestCrossCut();
                }
                if ((ECrossCutView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    this.m_OHead2.ORecipe = this.m_OCrossCutRecipe.OHead2;
                    this.m_OHead2.RequestCrossCut();
                }
                if ((ECrossCutView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    this.m_OHead3.ORecipe = this.m_OCrossCutRecipe.OHead3;
                    this.m_OHead3.RequestCrossCut();
                }
                if ((ECrossCutView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    this.m_OHead4.ORecipe = this.m_OCrossCutRecipe.OHead4;
                    this.m_OHead4.RequestCrossCut();
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
        }


        private void OPLC_OScaleCalculationRequested(double F64XOfHead1, double F64XOfHead2, double F64XOfHead3, double F64XOfHead4)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CMotionController.It.OPLC.SetBusyOn();
                CLoggingTool.It.StartScaleCalculation();

                //X = LASER
                //Y = MACHINE
                double F64XOfLaserMin = 0;
                double F64XOfLaserMax = 0;
                double F64YOfMachineMin = 0;
                double F64YOfMachineMax = 0;
                double F64YOfHead1Result = 0;
                double F64YOfHead2Result = 0;
                double F64YOfHead3Result = 0;
                double F64YOfHead4Result = 0;
                for (int _Index = 0; _Index < this.m_OScaleHead1.Rows.Count; _Index++)
                {
                    if (F64XOfHead1 <= (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN]) continue;
                    if (F64XOfHead1 > (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX]) continue;

                    F64XOfLaserMin = (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN];
                    F64XOfLaserMax = (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX];
                    F64YOfMachineMin = (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MIN];
                    F64YOfMachineMax = (double)this.m_OScaleHead1.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MAX];

                    F64YOfHead1Result = (F64YOfMachineMax - F64YOfMachineMin) / (F64XOfLaserMax - F64XOfLaserMin);
                    F64YOfHead1Result = F64YOfHead1Result * (F64XOfHead1 - F64XOfLaserMin);
                    F64YOfHead1Result = Math.Round(F64YOfHead1Result + F64YOfMachineMin - F64XOfHead1, 4);

                    CLoggingTool.It.CalcScaleCalculation("Head #1", F64XOfLaserMin, F64XOfLaserMax, F64YOfMachineMin, F64YOfMachineMax, F64XOfHead1, F64YOfHead1Result);
                    break;
                }
                for (int _Index = 0; _Index < this.m_OScaleHead2.Rows.Count; _Index++)
                {
                    if (F64XOfHead2 <= (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN]) continue;
                    if (F64XOfHead2 > (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX]) continue;

                    F64XOfLaserMin = (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN];
                    F64XOfLaserMax = (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX];
                    F64YOfMachineMin = (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MIN];
                    F64YOfMachineMax = (double)this.m_OScaleHead2.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MAX];

                    F64YOfHead2Result = (F64YOfMachineMax - F64YOfMachineMin) / (F64XOfLaserMax - F64XOfLaserMin);
                    F64YOfHead2Result = F64YOfHead2Result * (F64XOfHead2 - F64XOfLaserMin);
                    F64YOfHead2Result = Math.Round(F64YOfHead2Result + F64YOfMachineMin - F64XOfHead2, 4);

                    CLoggingTool.It.CalcScaleCalculation("Head #2", F64XOfLaserMin, F64XOfLaserMax, F64YOfMachineMin, F64YOfMachineMax, F64XOfHead2, F64YOfHead2Result);
                    break;
                }
                for (int _Index = 0; _Index < this.m_OScaleHead3.Rows.Count; _Index++)
                {
                    if (F64XOfHead3 <= (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN]) continue;
                    if (F64XOfHead3 > (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX]) continue;

                    F64XOfLaserMin = (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN];
                    F64XOfLaserMax = (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX];
                    F64YOfMachineMin = (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MIN];
                    F64YOfMachineMax = (double)this.m_OScaleHead3.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MAX];

                    F64YOfHead3Result = (F64YOfMachineMax - F64YOfMachineMin) / (F64XOfLaserMax - F64XOfLaserMin);
                    F64YOfHead3Result = F64YOfHead3Result * (F64XOfHead3 - F64XOfLaserMin);
                    F64YOfHead3Result = Math.Round(F64YOfHead3Result + F64YOfMachineMin - F64XOfHead3, 4);

                    CLoggingTool.It.CalcScaleCalculation("Head #3", F64XOfLaserMin, F64XOfLaserMax, F64YOfMachineMin, F64YOfMachineMax, F64XOfHead3, F64YOfHead3Result);
                    break;
                }
                for (int _Index = 0; _Index < this.m_OScaleHead4.Rows.Count; _Index++)
                {
                    if (F64XOfHead4 <= (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN]) continue;
                    if (F64XOfHead4 > (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX]) continue;

                    F64XOfLaserMin = (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_LASER_MIN];
                    F64XOfLaserMax = (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_LASER_MAX];
                    F64YOfMachineMin = (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MIN];
                    F64YOfMachineMax = (double)this.m_OScaleHead4.Rows[_Index][CDB.MOTION_HEAD_MACHINE_MAX];

                    F64YOfHead4Result = (F64YOfMachineMax - F64YOfMachineMin) / (F64XOfLaserMax - F64XOfLaserMin);
                    F64YOfHead4Result = F64YOfHead4Result * (F64XOfHead4 - F64XOfLaserMin);
                    F64YOfHead4Result = Math.Round(F64YOfHead4Result + F64YOfMachineMin - F64XOfHead4, 4);

                    CLoggingTool.It.CalcScaleCalculation("Head #4", F64XOfLaserMin, F64XOfLaserMax, F64YOfMachineMin, F64YOfMachineMax, F64XOfHead4, F64YOfHead4Result);
                    break;
                }

                CMotionController.It.OPLC.SetScaleResult(F64YOfHead1Result, F64YOfHead2Result, F64YOfHead3Result, F64YOfHead4Result);
                CMotionController.It.OPLC.FinishScaleCalculation();
                CLoggingTool.It.FinishScaleCalculation();
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
        #endregion


        #region FUNCTION
        private void BeginWork()
        {
            try
            {
                if (this.m_OWorker == null)
                {
                    this.m_BDoWork = true;

                    this.m_OWorker = new Thread(this.Work);
                    this.m_OWorker.Priority = ThreadPriority.Highest;
                    this.m_OWorker.IsBackground = true;
                    this.m_OWorker.Start();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void Work()
        {
            try
            {
                while (this.m_BDoWork == true)
                {
                    try
                    {
                        Monitor.Enter(this.m_OInterrupt);

                        if (this.m_EAnalysis == EANALYSIS.CALIBRATION)
                        {
                            this.RunCalibration();
                        }
                        else if (this.m_EAnalysis == EANALYSIS.INSPECTION)
                        {
                            if (this.m_BRunAlignment == true)
                            {
                                this.MAXRETRY = CEnvironment.It.I32MaxTry;
                               this.RunAlignment();
                            }
                            if (this.m_BRunDirectCut == true)
                            {
                                this.RunDirectCut();
                            }
                            if (this.m_BRunCrossCut == true)
                            {
                                this.RunCrossCut();
                            }
                        }

                        if (DateTime.Now.Ticks - this.m_I64AlignmentRequestedTick > 30000000)
                        {
                            this.m_BUseAlignSharp = true;
                            this.m_I64AlignmentRequestedTick = long.MaxValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }
                    finally
                    {
                        Monitor.Exit(this.m_OInterrupt);
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        private void EndWork()
        {
            try
            {
                if (this.m_OWorker != null)
                {
                    this.m_BDoWork = false;

                    this.m_OWorker.Join();
                    this.m_OWorker = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StartCalibration(double F64SectionXY, int I32PointXY, double F64SectionT, int I32PointT)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                

                CMotionController.It.OPLC.GetStageCurrent
                (
                    out this.m_F64CalibrationMotionStandardHead1X,
                    out this.m_F64CalibrationMotionStandardHead2X,
                    out this.m_F64CalibrationMotionStandardHead3X,
                    out this.m_F64CalibrationMotionStandardHead4X,
                    out this.m_F64CalibrationMotionStandardY,
                    out this.m_F64CalibrationMotionStandardT
                );

                if (this.m_F64CalibrationMotionStandardT < 45)
                {
                    this.m_EAnalysis = EANALYSIS.CALIBRATION;
                    this.m_ECalibrationAngle = ESTAGE_ANGLE.ZERO;
                    this.m_OCalibrationAlgorithm = this.m_OZeroAlgorithm;
                    this.m_I32CalibrationCount = this.m_OCalibrationAlgorithm.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                    this.m_I32CalibrationIndex = 0;
                    this.m_BStartCalibration = true;
                    this.m_I64StartMovementTick = DateTime.Now.Ticks;

                    this.m_OCalibrationRecipe = this.m_ORecipe.OZero;
                    this.m_OHead1.ORecipe = this.m_ORecipe.OZero.OHead1;
                    this.m_OHead2.ORecipe = this.m_ORecipe.OZero.OHead2;
                    this.m_OHead3.ORecipe = this.m_ORecipe.OZero.OHead3;
                    this.m_OHead4.ORecipe = this.m_ORecipe.OZero.OHead4;

                    CMotionController.It.OPLC.SetStartCalibration();
                    CLoggingTool.It.StartCalibration(this.m_ECalibrationAngle);
                }
                else
                {
                    this.m_EAnalysis = EANALYSIS.CALIBRATION;
                    this.m_ECalibrationAngle = ESTAGE_ANGLE.NINETY;
                    this.m_OCalibrationAlgorithm = this.m_ONinetyAlgorithm;
                    this.m_I32CalibrationCount = this.m_OCalibrationAlgorithm.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                    this.m_I32CalibrationIndex = 0;
                    this.m_BStartCalibration = true;
                    this.m_I64StartMovementTick = DateTime.Now.Ticks;

                    this.m_OCalibrationRecipe = this.m_ORecipe.ONinety;
                    this.m_OHead1.ORecipe = this.m_ORecipe.ONinety.OHead1;
                    this.m_OHead2.ORecipe = this.m_ORecipe.ONinety.OHead2;
                    this.m_OHead3.ORecipe = this.m_ORecipe.ONinety.OHead3;
                    this.m_OHead4.ORecipe = this.m_ORecipe.ONinety.OHead4;

                    CMotionController.It.OPLC.SetStartCalibration();
                    CLoggingTool.It.StartCalibration(this.m_ECalibrationAngle);
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
        }


        private void RunCalibration()
        {
            try
            {
                if ((this.m_BRunCalibration == false) && (DateTime.Now.Ticks - this.m_I64StartMovementTick > WAIT_TICK))
                {
                    //모션 이동이 지나치게 긴 경우(10초)
                    CMotionController.It.OPLC.SetFailCalibration();
                    CLoggingTool.It.FailCalibrationByTimeOut(this.m_ECalibrationAngle);

                    this.m_EAnalysis = EANALYSIS.NONE;
                    this.m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
                    this.m_OCalibrationAlgorithm = null;
                    this.m_I32CalibrationCount = 0;
                    this.m_I32CalibrationIndex = 0;
                    this.m_BStartCalibration = false;
                    this.m_BRunCalibration = false;
                    this.m_I64StartMovementTick = long.MaxValue;
                    this.OnFinishCalibration(false);
                }


                if (this.m_BRunCalibration == true)
                {
                    if (this.m_BStartCalibration == false)
                    {
                        CMarkResult OHead1Result = null;
                        CMarkResult OHead2Result = null;
                        CMarkResult OHead3Result = null;
                        CMarkResult OHead4Result = null;

                        if (CEnvironment.It.StrEnabled1 == "true")
                        {
                            OHead1Result = this.m_OHead1.GetMarkResult();
                        }
                        if (CEnvironment.It.StrEnabled2 == "true")
                        {
                            OHead2Result = this.m_OHead2.GetMarkResult();
                        }
                        if (CEnvironment.It.StrEnabled3 == "true")
                        {
                            OHead3Result = this.m_OHead3.GetMarkResult();
                        }
                        if (CEnvironment.It.StrEnabled4 == "true")
                        {
                            OHead4Result = this.m_OHead4.GetMarkResult();
                        }
                    

                        if (((OHead1Result == null) && (CEnvironment.It.StrEnabled1 == "true")) ||
                            ((OHead2Result == null) && (CEnvironment.It.StrEnabled2 == "true")) ||
                            ((OHead3Result == null) && (CEnvironment.It.StrEnabled3 == "true")) ||
                            ((OHead4Result == null) && (CEnvironment.It.StrEnabled4 == "true"))) return;


                        if ((((OHead1Result != null) && (OHead1Result.BInspected == true) && (OHead1Result.BOK == true)) || (CEnvironment.It.StrEnabled1 == "false")) &&
                            (((OHead2Result != null) && (OHead2Result.BInspected == true) && (OHead2Result.BOK == true)) || (CEnvironment.It.StrEnabled2 == "false")) &&
                            (((OHead3Result != null) && (OHead3Result.BInspected == true) && (OHead3Result.BOK == true)) || (CEnvironment.It.StrEnabled3 == "false")) &&
                            (((OHead4Result != null) && (OHead4Result.BInspected == true) && (OHead4Result.BOK == true)) || (CEnvironment.It.StrEnabled4 == "false")))
                        {
                            //정상적으로 마크를 찾은 경우
                            double[] ArrF64X = new double[4];
                            double[] ArrF64Y = new double[4];
                            if (OHead1Result != null)
                            {
                                ArrF64X[0] = OHead1Result.F64X;
                                ArrF64Y[0] = OHead1Result.F64Y;
                            }
                            if (OHead2Result != null)
                            {
                                ArrF64X[1] = OHead2Result.F64X;
                                ArrF64Y[1] = OHead2Result.F64Y;
                            }
                            if (OHead3Result != null)
                            {
                                ArrF64X[2] = OHead3Result.F64X;
                                ArrF64Y[2] = OHead3Result.F64Y;
                            }
                            if (OHead4Result != null)
                            {
                                ArrF64X[3] = OHead4Result.F64X;
                                ArrF64Y[3] = OHead4Result.F64Y;
                            }
                            

                            this.m_OCalibrationAlgorithm.SetCalibrationResult(ArrF64X, ArrF64Y);
                            CLoggingTool.It.FinishCalibrationMovement(this.m_ECalibrationAngle, this.m_I32CalibrationIndex, this.m_I32CalibrationCount, ArrF64X, ArrF64Y);

                            CCalibrationResult OResult = new CCalibrationResult(this.m_ECalibrationAngle);
                            OResult.OHead1 = OHead1Result;
                            OResult.OHead2 = OHead2Result;
                            OResult.OHead3 = OHead3Result;
                            OResult.OHead4 = OHead4Result;
                            this.OnFinishCalibrationMovement(OResult);

                            this.m_I32CalibrationIndex++;
                        }
                        else
                        {
                            //정상적으로 마크를 찾지 못한 경우
                            CMotionController.It.OPLC.SetFailCalibration();
                            CLoggingTool.It.FailCalibrationByUnknownPoint(this.m_ECalibrationAngle);

                            this.m_EAnalysis = EANALYSIS.NONE;
                            this.m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
                            this.m_OCalibrationAlgorithm = null;
                            this.m_I32CalibrationCount = 0;
                            this.m_I32CalibrationIndex = 0;
                            this.m_BStartCalibration = false;
                            this.m_BRunCalibration = false;
                            this.m_I64StartMovementTick = long.MaxValue;
                            this.OnFinishCalibration(false);
                            return;
                        }
                    }


                    if (this.m_I32CalibrationIndex < this.m_I32CalibrationCount)
                    {
                        //다음 스텝의 모션 이동량을 보냄
                        this.m_OCalibrationAlgorithm.GetCalibrationMovement
                        (
                            this.m_I32CalibrationIndex,
                            out this.m_F64CalibrationX, out this.m_F64CalibrationY, out this.m_F64CalibrationT
                        );

                        if (this.m_I32CalibrationIndex == 0)
                        {
                            this.m_F64CalibrationMotionHead1X = this.m_F64CalibrationX;
                            this.m_F64CalibrationMotionHead2X = this.m_F64CalibrationX;
                            this.m_F64CalibrationMotionHead3X = this.m_F64CalibrationX;
                            this.m_F64CalibrationMotionHead4X = this.m_F64CalibrationX;
                            this.m_F64CalibrationMotionY = this.m_F64CalibrationY;
                            this.m_F64CalibrationMotionT = this.m_F64CalibrationT;

                            this.m_F64BeforeCalibrationX = this.m_F64CalibrationX;
                            this.m_F64BeforeCalibrationY = this.m_F64CalibrationY;
                            this.m_F64BeforeCalibrationT = this.m_F64CalibrationT;
                        }
                        else
                        {
                            this.m_F64CalibrationMotionHead1X = this.m_F64CalibrationX - this.m_F64BeforeCalibrationX;
                            this.m_F64CalibrationMotionHead2X = this.m_F64CalibrationMotionHead1X;
                            this.m_F64CalibrationMotionHead3X = this.m_F64CalibrationMotionHead1X;
                            this.m_F64CalibrationMotionHead4X = this.m_F64CalibrationMotionHead1X;
                            this.m_F64CalibrationMotionY = this.m_F64CalibrationY - this.m_F64BeforeCalibrationY;
                            this.m_F64CalibrationMotionT = this.m_F64CalibrationT - this.m_F64BeforeCalibrationT;

                            this.m_F64BeforeCalibrationX = this.m_F64CalibrationX;
                            this.m_F64BeforeCalibrationY = this.m_F64CalibrationY;
                            this.m_F64BeforeCalibrationT = this.m_F64CalibrationT;
                        }
                         
                        //calibration
                        CMotionController.It.OPLC.SetStageMovement
                        (
                            CSign.CALIBRATION_HEAD1X_SIGN * this.m_F64CalibrationMotionHead1X,
                            CSign.CALIBRATION_HEAD2X_SIGN * this.m_F64CalibrationMotionHead2X,
                            CSign.CALIBRATION_HEAD3X_SIGN * this.m_F64CalibrationMotionHead3X,
                            CSign.CALIBRATION_HEAD4X_SIGN * this.m_F64CalibrationMotionHead4X,
                            CSign.CALIBRATION_Y_SIGN * this.m_F64CalibrationMotionY,
                            CSign.CALIBRATION_T_SIGN * this.m_F64CalibrationMotionT
                        );
                        CMotionController.It.OPLC.SetCalibrationMovement();


                        CLoggingTool.It.StartCalibrationMovement
                        (
                            this.m_ECalibrationAngle,
                            this.m_I32CalibrationIndex,
                            this.m_I32CalibrationCount,
                            CSign.CALIBRATION_HEAD1X_SIGN * this.m_F64CalibrationMotionHead1X,
                            CSign.CALIBRATION_HEAD2X_SIGN * this.m_F64CalibrationMotionHead2X,
                            CSign.CALIBRATION_HEAD3X_SIGN * this.m_F64CalibrationMotionHead3X,
                            CSign.CALIBRATION_HEAD4X_SIGN * this.m_F64CalibrationMotionHead4X,
                            CSign.CALIBRATION_Y_SIGN * this.m_F64CalibrationMotionY,
                            CSign.CALIBRATION_T_SIGN * this.m_F64CalibrationMotionT
                        );

                        this.m_BStartCalibration = false;
                        this.m_BRunCalibration = false;
                        this.m_I64StartMovementTick = DateTime.Now.Ticks;
                    }
                    else if (this.m_I32CalibrationIndex == this.m_I32CalibrationCount)
                    {
                        //캘리브레이션 완료 스텝                        
                        this.m_OCalibrationAlgorithm.F64StandardHead1X = this.m_F64CalibrationMotionStandardHead1X;
                        this.m_OCalibrationAlgorithm.F64StandardHead2X = this.m_F64CalibrationMotionStandardHead2X;
                        this.m_OCalibrationAlgorithm.F64StandardHead3X = this.m_F64CalibrationMotionStandardHead3X;
                        this.m_OCalibrationAlgorithm.F64StandardHead4X = this.m_F64CalibrationMotionStandardHead4X;
                        this.m_OCalibrationAlgorithm.F64StandardY = this.m_F64CalibrationMotionStandardY;
                        this.m_OCalibrationAlgorithm.F64StandardT = this.m_F64CalibrationMotionStandardT;
                        this.m_OCalibrationAlgorithm.RunCalibration();
                        this.m_OCalibrationAlgorithm.SaveFile(this.m_OCalibrationRecipe.StrDirectory);

                        CMotionController.It.OPLC.SetSuccessCalibration();
                        CLoggingTool.It.SuccessCalibration(this.m_ECalibrationAngle);

                        this.m_EAnalysis = EANALYSIS.NONE;
                        this.m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
                        this.m_I32CalibrationIndex = 0;
                        this.m_I32CalibrationCount = 0;
                        this.m_BStartCalibration = false;
                        this.m_BRunCalibration = false;
                        this.m_I64StartMovementTick = long.MaxValue;
                        this.OnFinishCalibration(true);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void StopCalibration()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CMotionController.It.OPLC.SetFailCalibration();
                CLoggingTool.It.FailCalibrationByUserOperation(this.m_ECalibrationAngle);

                this.m_EAnalysis = EANALYSIS.NONE;
                this.m_ECalibrationAngle = ESTAGE_ANGLE.NONE;
                this.m_OCalibrationAlgorithm = null;
                this.m_I32CalibrationCount = 0;
                this.m_I32CalibrationIndex = 0;
                this.m_BStartCalibration = false;
                this.m_BRunCalibration = false;
                this.m_I64StartMovementTick = long.MaxValue;
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


        public void BeginInspection()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_EAnalysis = EANALYSIS.INSPECTION;
                CLoggingTool.It.BeginInspection();

                CMotionController.It.OPLC.BeginInspection();
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

        private void RunAlignment()
        {
            try
            {
                    //20170410
                    #region Get image and inspect
                    CMarkResult OHead1Result = null;
                    CMarkResult OHead2Result = null;
                    CMarkResult OHead3Result = null;
                    CMarkResult OHead4Result = null;
                    if (CEnvironment.It.StrEnabled1 == "true") {OHead1Result = this.m_OHead1.GetMarkResult();}
                    if (CEnvironment.It.StrEnabled2 == "true") {OHead2Result = this.m_OHead2.GetMarkResult();}
                    if (CEnvironment.It.StrEnabled3 == "true") {OHead3Result = this.m_OHead3.GetMarkResult();}
                    if (CEnvironment.It.StrEnabled4 == "true") {OHead4Result = this.m_OHead4.GetMarkResult();}
                    if (((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1) && (OHead1Result == null) && (CEnvironment.It.StrEnabled1 == "true")) return;
                    if (((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2) && (OHead2Result == null) && (CEnvironment.It.StrEnabled2 == "true")) return;
                    if (((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3) && (OHead3Result == null) && (CEnvironment.It.StrEnabled3 == "true")) return;
                    if (((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4) && (OHead4Result == null) && (CEnvironment.It.StrEnabled4 == "true")) return;

                    ERESULT EResult = ERESULT.NONE;
                    EVIEW EView = EVIEW.NONE;
                    this.m_BRunAlignment = false;
                    #endregion

                    //(this.m_EAlignmentView & EVIEW.HEAD1) != EVIEW.HEAD1) 는 헤드1이 EAlignmentView에 포함되어 있되, HEAD1만 있지는 않을 경우를 뜻함.
                    // 카메라 헤드1이 포함되어있되 혼자있으면 안되고, 카메라1의 결과가 있거나, 카메라1이 비활성화 and   <---3개중 하나는 충족해야
                    // 카메라 헤드2이 포함되어있되 혼자있으면 안되고, 카메라2의 결과가 있거나, 카메라2이 비활성화 and
                    // 카메라 헤드3이 포함되어있되 혼자있으면 안되고, 카메라3의 결과가 있거나, 카메라3이 비활성화 and
                    // 카메라 헤드4이 포함되어있되 혼자있으면 안되고, 카메라4의 결과가 있거나, 카메라4이 비활성화 and
                    if ((
                        (((this.m_EAlignmentView & EVIEW.HEAD1) != EVIEW.HEAD1) || ((OHead1Result != null) && (OHead1Result.BInspected == true) && (OHead1Result.BOK == true)) || (CEnvironment.It.StrEnabled1 == "false")) &&
                        (((this.m_EAlignmentView & EVIEW.HEAD2) != EVIEW.HEAD2) || ((OHead2Result != null) && (OHead2Result.BInspected == true) && (OHead2Result.BOK == true)) || (CEnvironment.It.StrEnabled2 == "false")) &&
                        (((this.m_EAlignmentView & EVIEW.HEAD3) != EVIEW.HEAD3) || ((OHead3Result != null) && (OHead3Result.BInspected == true) && (OHead3Result.BOK == true)) || (CEnvironment.It.StrEnabled3 == "false")) &&
                        (((this.m_EAlignmentView & EVIEW.HEAD4) != EVIEW.HEAD4) || ((OHead4Result != null) && (OHead4Result.BInspected == true) && (OHead4Result.BOK == true)) || (CEnvironment.It.StrEnabled4 == "false"))
                        ))
                    { 
                        if (OHead1Result != null)
                        {
                            if ((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1) CLoggingTool.It.ShowAlignmentAxis(this.m_EAlignmentAngle, EVIEW.HEAD1, OHead1Result.F64X, OHead1Result.F64Y);
                        }
                        if (OHead2Result != null)
                        {
                            if ((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2) CLoggingTool.It.ShowAlignmentAxis(this.m_EAlignmentAngle, EVIEW.HEAD2, OHead2Result.F64X, OHead2Result.F64Y);
                        }
                        if (OHead3Result != null)
                        {
                            if ((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3) CLoggingTool.It.ShowAlignmentAxis(this.m_EAlignmentAngle, EVIEW.HEAD3, OHead3Result.F64X, OHead3Result.F64Y);
                        }
                        if (OHead4Result != null)
                        {
                            if ((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4) CLoggingTool.It.ShowAlignmentAxis(this.m_EAlignmentAngle, EVIEW.HEAD4, OHead4Result.F64X, OHead4Result.F64Y);
                        }


                        #region 얼라인샵 사용
                        if (this.m_BUseAlignSharp == true)
                        {
                            //모션 위치 오차 설정
                            CMotionController.It.OPLC.GetStageCurrent
                            (
                                out this.m_F64AlignmentMotionCurrentHead1X,
                                out this.m_F64AlignmentMotionCurrentHead2X,
                                out this.m_F64AlignmentMotionCurrentHead3X,
                                out this.m_F64AlignmentMotionCurrentHead4X,
                                out this.m_F64AlignmentMotionCurrentY,
                                out this.m_F64AlignmentMotionCurrentT
                            );
                            this.m_OAlignmentAlgorithm.SetViewOffset
                            (
                                this.m_F64AlignmentMotionCurrentHead1X,
                                this.m_F64AlignmentMotionCurrentHead2X,
                                this.m_F64AlignmentMotionCurrentHead3X,
                                this.m_F64AlignmentMotionCurrentHead4X,
                                this.m_F64AlignmentMotionCurrentY
                            );
                            CLoggingTool.It.ShowAlignmentMotionCurrent
                            (
                                this.m_EAlignmentAngle,
                                this.m_F64AlignmentMotionCurrentHead1X,
                                this.m_F64AlignmentMotionCurrentHead2X,
                                this.m_F64AlignmentMotionCurrentHead3X,
                                this.m_F64AlignmentMotionCurrentHead4X,
                                this.m_F64AlignmentMotionCurrentY,
                                this.m_F64AlignmentMotionCurrentT
                            );

                            //Align # : 목표 정렬 지점 설정
                            if ((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1 && (CEnvironment.It.StrEnabled1 == "true"))
                            {
                                this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD1, OHead1Result.OImageInfo.OImage.Width / 2D, OHead1Result.OImageInfo.OImage.Height / 2D);
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2 && (CEnvironment.It.StrEnabled2 == "true"))
                            {
                                this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD2, OHead2Result.OImageInfo.OImage.Width / 2D, OHead2Result.OImageInfo.OImage.Height / 2D);
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3 && (CEnvironment.It.StrEnabled3 == "true"))
                            {
                                this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD3, OHead3Result.OImageInfo.OImage.Width / 2D, OHead3Result.OImageInfo.OImage.Height / 2D);
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4 && (CEnvironment.It.StrEnabled4 == "true"))
                            {
                                this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD4, OHead4Result.OImageInfo.OImage.Width / 2D, OHead4Result.OImageInfo.OImage.Height / 2D);
                            }

                            double[] ArrF64X = new double[4];
                            double[] ArrF64Y = new double[4];
                            if ((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1 && OHead1Result != null)
                            {
                                ArrF64X[0] = OHead1Result.F64X;
                                ArrF64Y[0] = OHead1Result.F64Y;
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2 && OHead2Result != null)
                            {
                                ArrF64X[1] = OHead2Result.F64X;
                                ArrF64Y[1] = OHead2Result.F64Y;
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3 && OHead3Result != null)
                            {
                                ArrF64X[2] = OHead3Result.F64X;
                                ArrF64Y[2] = OHead3Result.F64Y;
                            }
                            if ((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4 && OHead4Result != null)
                            {
                                ArrF64X[3] = OHead4Result.F64X;
                                ArrF64Y[3] = OHead4Result.F64Y;
                            }

                            //Align # : 정렬 값 계산
                            this.m_OAlignmentAlgorithm.RunAlignment
                            (
                                this.m_EAlignmentView,
                                ArrF64X,
                                ArrF64Y,
                                out this.m_F64AlignmentMotionHead1X,
                                out this.m_F64AlignmentMotionHead2X,
                                out this.m_F64AlignmentMotionHead3X,
                                out this.m_F64AlignmentMotionHead4X,
                                out this.m_F64AlignmentMotionY,
                                out this.m_F64AlignmentMotionT
                            );
                        

                            #region additional calculation & modification
                            //20170305
                            //we cannot trust align# upon calulating Y.. we'll just subtract pixels.
                            int HowManyCameras = 0;
                            double F64Y1Diff = 0;
                            double F64Y2Diff = 0;
                            double F64Y3Diff = 0;
                            double F64Y4Diff = 0;
                            double F64AvgY = 0;
                            if (OHead1Result != null && (CEnvironment.It.StrEnabled1 == "true"))
                            {
                                F64Y1Diff = ((OHead1Result.OImageInfo.OImage.Height / 2D) - OHead1Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                                HowManyCameras++;
                            }
                            if (OHead2Result != null && (CEnvironment.It.StrEnabled2 == "true"))
                            {
                                F64Y2Diff = ((OHead2Result.OImageInfo.OImage.Height / 2D) - OHead2Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                                HowManyCameras++;
                            }
                            if (OHead3Result != null && (CEnvironment.It.StrEnabled3 == "true"))
                            {
                                F64Y3Diff = ((OHead3Result.OImageInfo.OImage.Height / 2D) - OHead3Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                                HowManyCameras++;
                            }
                            if (OHead4Result != null && (CEnvironment.It.StrEnabled4 == "true"))
                            {
                                F64Y4Diff = ((OHead4Result.OImageInfo.OImage.Height / 2D) - OHead4Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                                HowManyCameras++;
                            }
                            if (HowManyCameras != 0)
                            {
                                F64AvgY = (F64Y1Diff + F64Y2Diff + F64Y3Diff + F64Y4Diff) / HowManyCameras;
                            }
                            this.m_F64AlignmentMotionY = F64AvgY; 
                        

                            //only uncomment this part if one wants to subtract pixels regarding x axis.
                            //double F64Head1Differ = 0;
                            //double F64Head2Differ = 0;
                            //double F64Head3Differ = 0;
                            //double F64Head4Differ = 0;
                            
                            //if (OHead1Result != null && (CEnvironment.It.StrEnabled1 == "false")) F64Head1Differ = ((OHead1Result.OImageInfo.OImage.Width / 2D) - OHead1Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            //if (OHead2Result != null && (CEnvironment.It.StrEnabled2 == "false")) F64Head2Differ = ((OHead2Result.OImageInfo.OImage.Width / 2D) - OHead2Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            //if (OHead3Result != null && (CEnvironment.It.StrEnabled3 == "false")) F64Head3Differ = ((OHead3Result.OImageInfo.OImage.Width / 2D) - OHead3Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            //if (OHead4Result != null && (CEnvironment.It.StrEnabled4 == "false")) F64Head4Differ = ((OHead4Result.OImageInfo.OImage.Width / 2D) - OHead4Result.F64X) * CEnvironment.LEN_PER_PIXEL;

                            //if (OHead1Result != null && (CEnvironment.It.StrEnabled1 == "false")) this.m_F64AlignmentMotionHead1X = F64Head1Differ;
                            //if (OHead2Result != null && (CEnvironment.It.StrEnabled2 == "false")) this.m_F64AlignmentMotionHead2X = F64Head2Differ;
                            //if (OHead3Result != null && (CEnvironment.It.StrEnabled3 == "false")) this.m_F64AlignmentMotionHead3X = F64Head3Differ;
                            //if (OHead4Result != null && (CEnvironment.It.StrEnabled4 == "false")) this.m_F64AlignmentMotionHead4X = F64Head4Differ;
                            
                            if (OHead1Result == null) this.m_F64AlignmentMotionHead1X = 0;
                            if (OHead2Result == null) this.m_F64AlignmentMotionHead2X = 0;
                            if (OHead3Result == null) this.m_F64AlignmentMotionHead3X = 0;
                            if (OHead4Result == null) this.m_F64AlignmentMotionHead4X = 0;
                            #endregion
                            
                            
                            //오차 범위 확인 : Y와 세타만 해당
                            //(if any Xs are disabled, it will have a zero value, which is not sufficient for entering the loop)
                            //if (((Math.Abs(this.m_F64AlignmentMotionHead1X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead1) ||
                            //    (Math.Abs(this.m_F64AlignmentMotionHead2X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead2) ||
                            //    (Math.Abs(this.m_F64AlignmentMotionHead3X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead3) ||
                            //    (Math.Abs(this.m_F64AlignmentMotionHead4X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead4) ||
                            //    (Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY) ||
                            //    (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT)))
                            //{
                          

                            if (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT || 
                                Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY)
                            {
                                //if the vectors are too large
                                if (((Math.Abs(this.m_F64AlignmentMotionHead1X) > 5) && (CEnvironment.It.StrEnabled1 == "true")) ||
                                    ((Math.Abs(this.m_F64AlignmentMotionHead2X) > 5) && (CEnvironment.It.StrEnabled2 == "true")) ||
                                    ((Math.Abs(this.m_F64AlignmentMotionHead3X) > 5) && (CEnvironment.It.StrEnabled3 == "true")) ||
                                    ((Math.Abs(this.m_F64AlignmentMotionHead4X) > 5) && (CEnvironment.It.StrEnabled4 == "true")) ||
                                    (Math.Abs(this.m_F64AlignmentMotionY) > 5) ||
                                    (Math.Abs(this.m_F64AlignmentMotionT) > 5))
                                {
                                  
                                    //NG - Calibration
                                    EResult = ERESULT.NG;
                                    EView = EVIEW.NONE;
                                    this.m_BUseAlignSharp = true;
                                    this.m_I64AlignmentRequestedTick = long.MaxValue;

                                    CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                                    CLoggingTool.It.FailAlignmentByNeedCalibration(this.m_EAlignmentAngle);
                                }
                                else if (this.MAXRETRY <= this.CURRENTRETRY)
                                {
                                    EResult = ERESULT.NG;
                                    EView = EVIEW.NONE;
                                    this.CURRENTRETRY = 0;
                                    this.m_BUseAlignSharp = true;
                                    this.m_I64AlignmentRequestedTick = long.MaxValue;
                                    CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                                    CLoggingTool.It.FailAlignmentByMaxRetryLimitReached();
                                }
                                else //we need to send current vectors to PLC
                                {
                                    #region RETRY COUNTING
                                    this.CURRENTRETRY++;
                                    EResult = ERESULT.RETRY;
                                    EView = EVIEW.NONE;
                                    #endregion
                                    #region SIGN
                                    int I32Head1Sign = 1;
                                    int I32Head2Sign = 1;
                                    int I32Head3Sign = 1;
                                    int I32Head4Sign = 1;
                                    if (CEnvironment.It.StrEnabled1 == "true") { I32Head1Sign = CSign.ALIGNSHARP_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                    else { I32Head1Sign = CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; }
                                    if (CEnvironment.It.StrEnabled2 == "true") { I32Head2Sign = CSign.ALIGNSHARP_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                    else { I32Head2Sign = CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; }
                                    if (CEnvironment.It.StrEnabled3 == "true") { I32Head3Sign = CSign.ALIGNSHARP_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                    else { I32Head3Sign = CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; }
                                    if (CEnvironment.It.StrEnabled4 == "true") { I32Head4Sign = CSign.ALIGNSHARP_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                    else { I32Head4Sign = CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; }
                                    #endregion

                                    #region SENDING VECTORS(MOTION MOVEMENT) TO PLC
                                    #region AVERAGE X CALCULATION
                                    //if cameras are allowed to move "individually," m_BIndividual would be true; if not, it will all move by an average
                                    int I32Count = 0;
                                    if (OHead1Result != null) I32Count++;
                                    if (OHead2Result != null) I32Count++;
                                    if (OHead3Result != null) I32Count++;
                                    if (OHead4Result != null) I32Count++;
                                    //double AvgSumX = (Math.Abs(this.m_F64AlignmentMotionHead1X) + Math.Abs(this.m_F64AlignmentMotionHead2X) + Math.Abs(this.m_F64AlignmentMotionHead3X) + Math.Abs(this.m_F64AlignmentMotionHead4X)) / I32Count;
                                    double averageX = (this.m_F64AlignmentMotionHead1X + m_F64AlignmentMotionHead2X + m_F64AlignmentMotionHead3X + m_F64AlignmentMotionHead4X) / I32Count;
                                    #endregion

                                    if (this.m_BIndividual == false)
                                    {
                                        CMotionController.It.OPLC.SetStageMovement
                                        (
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head1Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head2Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head3Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head4Sign,
                                            this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                            this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                        );
                                        CLoggingTool.It.ShowAlignmentMotionMovement
                                        (
                                            this.m_EAlignmentAngle,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head1Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head2Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head3Sign,
                                            averageX * CEnvironment.DIRECTION_INFO * I32Head4Sign,
                                            this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                            this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                        );
                                    }
                                    else
                                    {
                                        CMotionController.It.OPLC.SetStageMovement
                                        (
                                            this.m_F64AlignmentMotionHead1X * CEnvironment.DIRECTION_INFO * I32Head1Sign,
                                            this.m_F64AlignmentMotionHead2X * CEnvironment.DIRECTION_INFO * I32Head2Sign,
                                            this.m_F64AlignmentMotionHead3X * CEnvironment.DIRECTION_INFO * I32Head3Sign,
                                            this.m_F64AlignmentMotionHead4X * CEnvironment.DIRECTION_INFO * I32Head4Sign,
                                            this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                            this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                        );
                                        CLoggingTool.It.ShowAlignmentMotionMovement
                                        (
                                            this.m_EAlignmentAngle,
                                            this.m_F64AlignmentMotionHead1X * CEnvironment.DIRECTION_INFO * I32Head1Sign,
                                            this.m_F64AlignmentMotionHead2X * CEnvironment.DIRECTION_INFO * I32Head2Sign,
                                            this.m_F64AlignmentMotionHead3X * CEnvironment.DIRECTION_INFO * I32Head3Sign,
                                            this.m_F64AlignmentMotionHead4X * CEnvironment.DIRECTION_INFO * I32Head4Sign,
                                            this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                            this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                        );
                                    }
                                    #endregion
                                    CMotionController.It.OPLC.FinishAlignment(ERESULT.RETRY);
                                    CLoggingTool.It.RetryAlignment(this.m_EAlignmentAngle, this.CURRENTRETRY);
                                }
                            }
                            //위 루프에서 X를 부분적으로 보정하다가 Y 랑 세타가 잡히면 얼라인샵을 안쓰고 계산하는 방식임. (얼라인샵은 보정량이 너무 가까우면 계산을 못해서 이게 젤 빠를거임)
                            //거의 그럴 가능성은 없겠지만 X를 보정하면서 세타가 틀어지면 다시 위 루프를 거침.
                            else 
                            {
                                //if ((this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD2 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD3 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true) ||
                                //    (this.m_EAlignmentView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true)
                                //    ) //unnecessary?
                                //{
                                    double F64Head1Differ_2 = 0;
                                    double F64Head2Differ_2 = 0;
                                    double F64Head3Differ_2 = 0;
                                    double F64Head4Differ_2 = 0;

                                    if (OHead1Result != null) F64Head1Differ_2 = ((OHead1Result.OImageInfo.OImage.Width / 2D) - OHead1Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                                    if (OHead2Result != null) F64Head2Differ_2 = ((OHead2Result.OImageInfo.OImage.Width / 2D) - OHead2Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                                    if (OHead3Result != null) F64Head3Differ_2 = ((OHead3Result.OImageInfo.OImage.Width / 2D) - OHead3Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                                    if (OHead4Result != null) F64Head4Differ_2 = ((OHead4Result.OImageInfo.OImage.Width / 2D) - OHead4Result.F64X) * CEnvironment.LEN_PER_PIXEL;

                                    this.m_F64AlignmentMotionHead1X = F64Head1Differ_2;
                                    this.m_F64AlignmentMotionHead2X = F64Head2Differ_2;
                                    this.m_F64AlignmentMotionHead3X = F64Head3Differ_2;
                                    this.m_F64AlignmentMotionHead4X = F64Head4Differ_2;
                                    //20170304 //위에서 계산한 Y 평균치값과 얼라인샵이 준 세타값을 그대로 가져가서 이 루프 안에서도 보정. 0으로 초기화 시키지 말기!
                                    //this.m_F64AlignmentMotionY = 0;
                                    //this.m_F64AlignmentMotionT = 0;

                                    if ((Math.Abs(F64Head1Differ_2) > this.m_OAlignmentRecipe.F64AlignmentLimitHead1) ||
                                        (Math.Abs(F64Head2Differ_2) > this.m_OAlignmentRecipe.F64AlignmentLimitHead2) ||
                                        (Math.Abs(F64Head3Differ_2) > this.m_OAlignmentRecipe.F64AlignmentLimitHead3) ||
                                        (Math.Abs(F64Head4Differ_2) > this.m_OAlignmentRecipe.F64AlignmentLimitHead4) ||
                                        (Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY) ||
                                        (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT))
                                    {
                                            this.CURRENTRETRY++;
                                            EResult = ERESULT.RETRY;
                                            EView = EVIEW.NONE;
                                           // this.m_BUseAlignSharp = false; //유일하게 얼라인샵이 false가 되는 구간인데, 이걸 지워버려도 될듯. 20170411
                                            if (OHead1Result == null) this.m_F64AlignmentMotionHead1X = 0;
                                            if (OHead2Result == null) this.m_F64AlignmentMotionHead2X = 0;
                                            if (OHead3Result == null) this.m_F64AlignmentMotionHead3X = 0;
                                            if (OHead4Result == null) this.m_F64AlignmentMotionHead4X = 0;

                                            #region SEND VECTORS TO PLC
                                            //align_X
                                            int I32Count = 0;
                                            if (OHead1Result != null) I32Count++;
                                            if (OHead2Result != null) I32Count++;
                                            if (OHead3Result != null) I32Count++;
                                            if (OHead4Result != null) I32Count++;
                                       
                                            double averageX = (this.m_F64AlignmentMotionHead1X + m_F64AlignmentMotionHead2X + m_F64AlignmentMotionHead3X + m_F64AlignmentMotionHead4X) / I32Count;
                                            
                                            
                                            if (this.m_BIndividual == false)
                                            {
                                                CMotionController.It.OPLC.SetStageMovement
                                                (
                                                    averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                                    this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                                );
                                                CLoggingTool.It.ShowAlignmentMotionMovement
                                                (
                                                    this.m_EAlignmentAngle,
                                                    averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO,
                                                    this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                                );
                                            }
                                            else
                                            {
                                                CMotionController.It.OPLC.SetStageMovement
                                                (
                                                    this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                                    this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                                );
                                                CLoggingTool.It.ShowAlignmentMotionMovement
                                                (
                                                    this.m_EAlignmentAngle,
                                                    this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                                    this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO,
                                                    this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                                );
                                            }
                                            #endregion

                                            CMotionController.It.OPLC.FinishAlignment(ERESULT.RETRY);
                                            CLoggingTool.It.RetryAlignment(this.m_EAlignmentAngle, this.CURRENTRETRY);
                                    }
                                    else 
                                    {
                                        //20170413 whenever the user might use the manual align we stop the data retrieval for learning purpose,
                                        // only when it turns out to be OK, on either in the auto align loop or in the manual loop, we initialize the default value for the m_BInterferanceOnLearningDueToManualAlign.
                                        // 
                                    
                                        EResult = ERESULT.OK;
                                        EView = EVIEW.NONE;
                                        this.m_BUseAlignSharp = true;
                                        this.m_I64AlignmentRequestedTick = long.MaxValue;
                                        CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//initialization 20170313
                                        CMotionController.It.OPLC.FinishAlignment(ERESULT.OK);
                                        this.CURRENTRETRY = 0;
                                        CLoggingTool.It.SuccessAlignment(this.m_EAlignmentAngle);
                                    }
                                //}
                                //else //I am not sure whether it can even enter this loop...
                                //{
                                //    //OK
                                //    EResult = ERESULT.OK;
                                //    EView = EVIEW.NONE;
                                //    this.m_BUseAlignSharp = true;
                                //    this.m_I64AlignmentRequestedTick = long.MaxValue;
                                //    CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//initialization 20170313
                                //    CMotionController.It.OPLC.FinishAlignment(ERESULT.OK);
                                //    this.CURRENTRETRY = 0;
                                //    CLoggingTool.It.SuccessAlignment(this.m_EAlignmentAngle);
                                //}
                            }
                        }
              #endregion
                        #region 얼라인 샵을 사용안할경우 (사용하지 않을거임)
                            //여기서는 X값만을 픽셀을 뺴는 형식으로 계산해서 보정을 해주는데, Y와 T는 건드리지 않고 있음. 
                            //그러므로 위에서 Y,T 루프를 빠져나왔을때 들어가는 루틴과 동일함으로 얼라인샵 사용유무 boolean을 always true 로 놓는것이 맞는듯함.
                        else 
                        {
                            double F64Head1Differ = 0;
                            double F64Head2Differ = 0;
                            double F64Head3Differ = 0;
                            double F64Head4Differ = 0;

                            if (OHead1Result != null) F64Head1Differ = ((OHead1Result.OImageInfo.OImage.Width / 2D) - OHead1Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            if (OHead2Result != null) F64Head2Differ = ((OHead2Result.OImageInfo.OImage.Width / 2D) - OHead2Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            if (OHead3Result != null) F64Head3Differ = ((OHead3Result.OImageInfo.OImage.Width / 2D) - OHead3Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                            if (OHead4Result != null) F64Head4Differ = ((OHead4Result.OImageInfo.OImage.Width / 2D) - OHead4Result.F64X) * CEnvironment.LEN_PER_PIXEL;

                            this.m_F64AlignmentMotionHead1X = F64Head1Differ;
                            this.m_F64AlignmentMotionHead2X = F64Head2Differ;
                            this.m_F64AlignmentMotionHead3X = F64Head3Differ;
                            this.m_F64AlignmentMotionHead4X = F64Head4Differ;
                            //this.m_F64AlignmentMotionY = 0;
                            //this.m_F64AlignmentMotionT = 0;

                            if ((Math.Abs(F64Head1Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead1) ||
                                (Math.Abs(F64Head2Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead2) ||
                                (Math.Abs(F64Head3Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead3) ||
                                (Math.Abs(F64Head4Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead4) ||
                                (Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY) ||
                                (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT))
                            {
                                this.CURRENTRETRY++;
                                EResult = ERESULT.RETRY;
                                EView = EVIEW.NONE;
                                if (OHead1Result == null) this.m_F64AlignmentMotionHead1X = 0;
                                if (OHead2Result == null) this.m_F64AlignmentMotionHead2X = 0;
                                if (OHead3Result == null) this.m_F64AlignmentMotionHead3X = 0;
                                if (OHead4Result == null) this.m_F64AlignmentMotionHead4X = 0;
                                
                                int I32Count = 0;
                                if (OHead1Result != null) I32Count++;
                                if (OHead2Result != null) I32Count++;
                                if (OHead3Result != null) I32Count++;
                                if (OHead4Result != null) I32Count++;
                                double averageX = (this.m_F64AlignmentMotionHead1X + m_F64AlignmentMotionHead2X + m_F64AlignmentMotionHead3X + m_F64AlignmentMotionHead4X) / I32Count;
                                if (this.m_BIndividual == false)
                                {
                                    CMotionController.It.OPLC.SetStageMovement
                                    (
                                        averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                    );
                                    CLoggingTool.It.ShowAlignmentMotionMovement
                                    (
                                        this.m_EAlignmentAngle,
                                       averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                    );
                                }
                                else
                                {
                                    CMotionController.It.OPLC.SetStageMovement
                                    (
                                        this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                    );
                                    CLoggingTool.It.ShowAlignmentMotionMovement
                                    (
                                        this.m_EAlignmentAngle,
                                       this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                                    );
                                }
                                CMotionController.It.OPLC.FinishAlignment(ERESULT.RETRY);
                                CLoggingTool.It.RetryAlignment(this.m_EAlignmentAngle, this.CURRENTRETRY);
                            }
                            else
                            {
                                EResult = ERESULT.OK;
                                EView = EVIEW.NONE;
                                this.m_BUseAlignSharp = true;
                                this.m_I64AlignmentRequestedTick = long.MaxValue;
                                CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                                CMotionController.It.OPLC.FinishAlignment(ERESULT.OK);
                                this.CURRENTRETRY = 0;
                                CLoggingTool.It.SuccessAlignment(this.m_EAlignmentAngle);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        
                        //NG - Unknown Point
                        this.m_BUseAlignSharp = true;
                        this.m_I64AlignmentRequestedTick = long.MaxValue;
                        if (((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1) && ((OHead1Result == null) || (OHead1Result.BInspected == false) || (OHead1Result.BOK == false)))
                        {
                            EResult = ERESULT.LEFT_NG;
                            EView = EVIEW.HEAD1;
                        }
                        else if (((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2) && ((OHead2Result == null) || (OHead2Result.BInspected == false) || (OHead2Result.BOK == false)))
                        {
                            EResult = ERESULT.LEFT_NG;
                            EView = EVIEW.HEAD2;
                        }
                        else if (((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3) && ((OHead3Result == null) || (OHead3Result.BInspected == false) || (OHead3Result.BOK == false)))
                        {
                            EResult = ERESULT.RIGHT_NG;
                            EView = EVIEW.HEAD3;
                        }
                        else if (((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4) && ((OHead4Result == null) || (OHead4Result.BInspected == false) || (OHead4Result.BOK == false)))
                        {
                            EResult = ERESULT.RIGHT_NG;
                            EView = EVIEW.HEAD4;
                        }

                        CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                        CMotionController.It.OPLC.FinishAlignment(EResult);
                        CLoggingTool.It.FailAlignmentByUnknownPoint(this.m_EAlignmentAngle, EView);
                    }


                    CAlignmentResult OResult = new CAlignmentResult(this.m_EAlignmentAngle, this.m_EAlignmentView);
                    OResult.EResult = EResult;
                    OResult.ENGView = EView;
                    OResult.OHead1 = OHead1Result;
                    OResult.OHead2 = OHead2Result;
                    OResult.OHead3 = OHead3Result;
                    OResult.OHead4 = OHead4Result;
                    OResult.F64Head1X = this.m_F64AlignmentMotionHead1X;
                    OResult.F64Head2X = this.m_F64AlignmentMotionHead2X;
                    OResult.F64Head3X = this.m_F64AlignmentMotionHead3X;
                    OResult.F64Head4X = this.m_F64AlignmentMotionHead4X;
                    OResult.F64Y = this.m_F64AlignmentMotionY;
                    OResult.F64T = this.m_F64AlignmentMotionT;

                    this.OnFinishAlignment(OResult);
                    this.SendAlignmentToFile(OResult);
              //  }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RunManualAlignment
        (
            ESTAGE_ANGLE EAlignmentAngle, EVIEW EAlignmentView, EMANUAL EManual,
            CMarkResult OHead1Result, CMarkResult OHead2Result, CMarkResult OHead3Result, CMarkResult OHead4Result
        )
        {
            try
            {
                CMotionController.It.OPLC.SetBusyOn();
                CLoggingTool.It.StartManualAlignment(EAlignmentAngle);

                ERESULT EResult = ERESULT.NONE;
                EVIEW EView = EVIEW.NONE;
                if ((((EAlignmentView & EVIEW.HEAD1) != EVIEW.HEAD1) || ((OHead1Result != null) && (OHead1Result.BInspected == true) && (OHead1Result.BOK == true)) || (CEnvironment.It.StrEnabled1 == "false")) &&
                    (((EAlignmentView & EVIEW.HEAD2) != EVIEW.HEAD2) || ((OHead2Result != null) && (OHead2Result.BInspected == true) && (OHead2Result.BOK == true)) || (CEnvironment.It.StrEnabled2 == "false")) &&
                    (((EAlignmentView & EVIEW.HEAD3) != EVIEW.HEAD3) || ((OHead3Result != null) && (OHead3Result.BInspected == true) && (OHead3Result.BOK == true)) || (CEnvironment.It.StrEnabled3 == "false")) &&
                    (((EAlignmentView & EVIEW.HEAD4) != EVIEW.HEAD4) || ((OHead4Result != null) && (OHead4Result.BInspected == true) && (OHead4Result.BOK == true)) || (CEnvironment.It.StrEnabled4 == "false")))
                {
                    //Ready
                    if (EAlignmentAngle == ESTAGE_ANGLE.ZERO)
                    {
                        this.m_OAlignmentRecipe = this.m_ORecipe.OZero;
                        this.m_OAlignmentAlgorithm = this.m_OZeroAlgorithm;
                    }
                    else if (EAlignmentAngle == ESTAGE_ANGLE.NINETY)
                    {
                        this.m_OAlignmentRecipe = this.m_ORecipe.ONinety;
                        this.m_OAlignmentAlgorithm = this.m_ONinetyAlgorithm;
                    }

                    //이미지상 마크 위치 표현
                    if ((this.m_EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1 && OHead1Result != null) CLoggingTool.It.ShowManualAlignmentAxis(EAlignmentAngle, EVIEW.HEAD1, OHead1Result.F64X, OHead1Result.F64Y);
                    if ((this.m_EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2 && OHead2Result != null) CLoggingTool.It.ShowManualAlignmentAxis(EAlignmentAngle, EVIEW.HEAD2, OHead2Result.F64X, OHead2Result.F64Y);
                    if ((this.m_EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3 && OHead3Result != null) CLoggingTool.It.ShowManualAlignmentAxis(EAlignmentAngle, EVIEW.HEAD3, OHead3Result.F64X, OHead3Result.F64Y);
                    if ((this.m_EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4 && OHead4Result != null) CLoggingTool.It.ShowManualAlignmentAxis(EAlignmentAngle, EVIEW.HEAD4, OHead4Result.F64X, OHead4Result.F64Y);
                   
                    if (EManual == EMANUAL.ALIGN_SHARP)
                    {
                        #region MANUAL ALIGN SHARP
                        //모션 위치 오차 설정
                        CMotionController.It.OPLC.GetStageCurrent
                        (
                            out this.m_F64AlignmentMotionCurrentHead1X,
                            out this.m_F64AlignmentMotionCurrentHead2X,
                            out this.m_F64AlignmentMotionCurrentHead3X,
                            out this.m_F64AlignmentMotionCurrentHead4X,
                            out this.m_F64AlignmentMotionCurrentY,
                            out this.m_F64AlignmentMotionCurrentT
                        );
                        this.m_OAlignmentAlgorithm.SetViewOffset
                        (
                            this.m_F64AlignmentMotionCurrentHead1X,
                            this.m_F64AlignmentMotionCurrentHead2X,
                            this.m_F64AlignmentMotionCurrentHead3X,
                            this.m_F64AlignmentMotionCurrentHead4X,
                            this.m_F64AlignmentMotionCurrentY
                        );
                        CLoggingTool.It.ShowManualAlignmentMotionCurrent
                        (
                            EAlignmentAngle,
                            this.m_F64AlignmentMotionCurrentHead1X,
                            this.m_F64AlignmentMotionCurrentHead2X,
                            this.m_F64AlignmentMotionCurrentHead3X,
                            this.m_F64AlignmentMotionCurrentHead4X,
                            this.m_F64AlignmentMotionCurrentY,
                            this.m_F64AlignmentMotionCurrentT
                        );

                        //목표 정렬 지점 설정
                        if ((EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1 && OHead1Result != null) this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD1, OHead1Result.OImageInfo.OImage.Width / 2D, OHead1Result.OImageInfo.OImage.Height / 2D);
                        if ((EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2 && OHead2Result != null) this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD2, OHead2Result.OImageInfo.OImage.Width / 2D, OHead2Result.OImageInfo.OImage.Height / 2D);
                        if ((EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3 && OHead3Result != null) this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD3, OHead3Result.OImageInfo.OImage.Width / 2D, OHead3Result.OImageInfo.OImage.Height / 2D);
                        if ((EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4 && OHead4Result != null) this.m_OAlignmentAlgorithm.SetTarget(EVIEW.HEAD4, OHead4Result.OImageInfo.OImage.Width / 2D, OHead4Result.OImageInfo.OImage.Height / 2D);
                   
                        //정렬 값 계산
                        double[] ArrF64X = new double[6];
                        double[] ArrF64Y = new double[6];
                        if ((EAlignmentView & EVIEW.HEAD1) == EVIEW.HEAD1 && OHead1Result != null)
                        {
                            ArrF64X[0] = OHead1Result.F64X;
                            ArrF64Y[0] = OHead1Result.F64Y;
                        }
                        if ((EAlignmentView & EVIEW.HEAD2) == EVIEW.HEAD2 && OHead2Result != null)
                        {
                            ArrF64X[1] = OHead2Result.F64X;
                            ArrF64Y[1] = OHead2Result.F64Y;
                        }
                        if ((EAlignmentView & EVIEW.HEAD3) == EVIEW.HEAD3 && OHead3Result != null)
                        {
                            ArrF64X[2] = OHead3Result.F64X;
                            ArrF64Y[2] = OHead3Result.F64Y;
                        }
                        if ((EAlignmentView & EVIEW.HEAD4) == EVIEW.HEAD4 && OHead4Result != null)
                        {
                            ArrF64X[3] = OHead4Result.F64X;
                            ArrF64Y[3] = OHead4Result.F64Y;
                        }
                        
                        this.m_OAlignmentAlgorithm.RunAlignment
                        (
                            EAlignmentView, ArrF64X, ArrF64Y,
                            out this.m_F64AlignmentMotionHead1X,
                            out this.m_F64AlignmentMotionHead2X,
                            out this.m_F64AlignmentMotionHead3X,
                            out this.m_F64AlignmentMotionHead4X,
                            out this.m_F64AlignmentMotionY,
                            out this.m_F64AlignmentMotionT
                        );
                        CLoggingTool.It.ShowManualAlignmentMotionMovement
                        (
                            EAlignmentAngle,
                            this.m_F64AlignmentMotionHead1X,
                            this.m_F64AlignmentMotionHead2X,
                            this.m_F64AlignmentMotionHead3X,
                            this.m_F64AlignmentMotionHead4X,
                            this.m_F64AlignmentMotionY,
                            this.m_F64AlignmentMotionT
                        );

                        //20170305
                        //we cannot trust align#, we will use the abs(sum of delta y1~4)
                        int HowManyCameras = 0;
                        double F64Y1Diff = 0;
                        double F64Y2Diff = 0;
                        double F64Y3Diff = 0;
                        double F64Y4Diff = 0;
                        double F64AvgY = 0;
                        if (OHead1Result != null && (CEnvironment.It.StrEnabled1 == "true"))
                        {
                            F64Y1Diff = ((OHead1Result.OImageInfo.OImage.Height / 2D) - OHead1Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                            HowManyCameras++;
                        }
                        if (OHead2Result != null && (CEnvironment.It.StrEnabled2 == "true"))
                        {
                            F64Y2Diff = ((OHead2Result.OImageInfo.OImage.Height / 2D) - OHead2Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                            HowManyCameras++;
                        }
                        if (OHead3Result != null && (CEnvironment.It.StrEnabled3 == "true"))
                        {
                            F64Y3Diff = ((OHead3Result.OImageInfo.OImage.Height / 2D) - OHead3Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                            HowManyCameras++;
                        }
                        if (OHead4Result != null && (CEnvironment.It.StrEnabled4 == "true"))
                        {
                            F64Y4Diff = ((OHead4Result.OImageInfo.OImage.Height / 2D) - OHead4Result.F64Y) * CEnvironment.LEN_PER_PIXEL;
                            HowManyCameras++;
                        }
                        if (HowManyCameras != 0)
                        {
                            F64AvgY = (F64Y1Diff + F64Y2Diff + F64Y3Diff + F64Y4Diff) / HowManyCameras;
                        }
                       
                        this.m_F64AlignmentMotionY = F64AvgY; //this gets later turned into absolute value.
                        //20170305end


                        //before we continue, we need to subtract pixels for those who are not using align #
                        double F64Head1Differ = 0;
                        double F64Head2Differ = 0;
                        double F64Head3Differ = 0;
                        double F64Head4Differ = 0;

                        if (OHead1Result != null || (CEnvironment.It.StrEnabled1 == "true")) F64Head1Differ = ((OHead1Result.OImageInfo.OImage.Width / 2D) - OHead1Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                        if (OHead2Result != null || (CEnvironment.It.StrEnabled2 == "true")) F64Head2Differ = ((OHead2Result.OImageInfo.OImage.Width / 2D) - OHead2Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                        if (OHead3Result != null || (CEnvironment.It.StrEnabled3 == "true")) F64Head3Differ = ((OHead3Result.OImageInfo.OImage.Width / 2D) - OHead3Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                        if (OHead4Result != null || (CEnvironment.It.StrEnabled4 == "true")) F64Head4Differ = ((OHead4Result.OImageInfo.OImage.Width / 2D) - OHead4Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                   
                        if (OHead1Result != null) this.m_F64AlignmentMotionHead1X = F64Head1Differ;
                        if (OHead2Result != null) this.m_F64AlignmentMotionHead2X = F64Head2Differ;
                        if (OHead3Result != null) this.m_F64AlignmentMotionHead3X = F64Head3Differ;
                        if (OHead4Result != null) this.m_F64AlignmentMotionHead4X = F64Head4Differ;
                        //this.m_F64AlignmentMotionY = 0;
                        //this.m_F64AlignmentMotionT = 0;
                        //
                        if (CEnvironment.It.StrEnabled1 == "false") this.m_F64AlignmentMotionHead1X = 0;
                        if (CEnvironment.It.StrEnabled2 == "false") this.m_F64AlignmentMotionHead2X = 0;
                        if (CEnvironment.It.StrEnabled3 == "false") this.m_F64AlignmentMotionHead3X = 0;
                        if (CEnvironment.It.StrEnabled4 == "false") this.m_F64AlignmentMotionHead4X = 0;
                        ////오차 범위 확인

                        //20170413 
                        int I32DiscrepancyAllowance = 1;
                        if (CEnvironment.It.I32Discrepancy > 1)
                        {
                            I32DiscrepancyAllowance = CEnvironment.It.I32Discrepancy;
                        }

                        if ((Math.Abs(this.m_F64AlignmentMotionHead1X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead1) ||
                            (Math.Abs(this.m_F64AlignmentMotionHead2X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead2) ||
                            (Math.Abs(this.m_F64AlignmentMotionHead3X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead3) ||
                            (Math.Abs(this.m_F64AlignmentMotionHead4X) > this.m_OAlignmentRecipe.F64AlignmentLimitHead4) ||
                            (Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY) ||
                            (Math.Abs(F64Y1Diff) > this.m_OAlignmentRecipe.F64AlignmentLimitY * I32DiscrepancyAllowance) ||
                            (Math.Abs(F64Y2Diff) > this.m_OAlignmentRecipe.F64AlignmentLimitY * I32DiscrepancyAllowance) ||
                            (Math.Abs(F64Y3Diff) > this.m_OAlignmentRecipe.F64AlignmentLimitY * I32DiscrepancyAllowance) ||
                            (Math.Abs(F64Y4Diff) > this.m_OAlignmentRecipe.F64AlignmentLimitY * I32DiscrepancyAllowance) ||
                            (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT)
                            )
                        {
                        //if (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT || Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY)
                        //{
                            if ((Math.Abs(this.m_F64AlignmentMotionHead1X) > 5 && CEnvironment.It.StrEnabled1 == "true") ||
                                (Math.Abs(this.m_F64AlignmentMotionHead2X) > 5 && CEnvironment.It.StrEnabled2 == "true") ||
                                (Math.Abs(this.m_F64AlignmentMotionHead3X) > 5 && CEnvironment.It.StrEnabled3 == "true") ||
                                (Math.Abs(this.m_F64AlignmentMotionHead4X) > 5 && CEnvironment.It.StrEnabled4 == "true") ||
                                (Math.Abs(F64Y1Diff) > 5) || (Math.Abs(F64Y2Diff) > 5) || (Math.Abs(F64Y3Diff) > 5) || (Math.Abs(F64Y4Diff) > 5) ||
                                (Math.Abs(this.m_F64AlignmentMotionT) > 5))
                            {
                                EResult = ERESULT.NG;
                                EView = EVIEW.NONE;
                                CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                                CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                                CLoggingTool.It.FailManualAlignmentByNeedCalibration(EAlignmentAngle);
                            }
                            else
                            {
                                EResult = ERESULT.RETRY;
                                EView = EVIEW.NONE;
                                //mixed
                                int I32Head1Sign = 1;
                                int I32Head2Sign = 1;
                                int I32Head3Sign = 1;
                                int I32Head4Sign = 1;
                                //if (CEnvironment.It.StrEnabled1 == "true") { I32Head1Sign = CSign.ALIGNSHARP_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                //else { 
                                    I32Head1Sign = CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto;
                                //}
                                //if (CEnvironment.It.StrEnabled2 == "true") { I32Head2Sign = CSign.ALIGNSHARP_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                //else { 
                                    I32Head2Sign = CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; 
                                //}
                                //if (CEnvironment.It.StrEnabled3 == "true") { I32Head3Sign = CSign.ALIGNSHARP_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                //else { 
                                    I32Head3Sign = CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; 
                                //}
                                //if (CEnvironment.It.StrEnabled4 == "true") { I32Head4Sign = CSign.ALIGNSHARP_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignS_auto; }
                                //else { 
                                    I32Head4Sign = CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto; 
                                //}

                                //1217
                                int I32Count = 0;
                                if (OHead1Result != null) I32Count++;
                                if (OHead2Result != null) I32Count++;
                                if (OHead3Result != null) I32Count++;
                                if (OHead4Result != null) I32Count++;
                                
                                if (OHead1Result == null) this.m_F64AlignmentMotionHead1X = 0;
                                if (OHead2Result == null) this.m_F64AlignmentMotionHead2X = 0;
                                if (OHead3Result == null) this.m_F64AlignmentMotionHead3X = 0;
                                if (OHead4Result == null) this.m_F64AlignmentMotionHead4X = 0;

                                double averageX = (this.m_F64AlignmentMotionHead1X + m_F64AlignmentMotionHead2X + m_F64AlignmentMotionHead3X + m_F64AlignmentMotionHead4X) / I32Count;
                                if (this.m_BIndividual == false)
                                {
                                    CMotionController.It.OPLC.SetStageMovement
                                    (
                                        averageX * I32Head1Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head2Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head3Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head4Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                    );
                                    CLoggingTool.It.ShowManualAlignmentMotionMovement
                                    (
                                        EAlignmentAngle,
                                        averageX * I32Head1Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head2Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head3Sign * CEnvironment.DIRECTION_INFO,
                                        averageX * I32Head4Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                    );
                                }
                                else
                                {
                                    CMotionController.It.OPLC.SetStageMovement
                                   (
                                       this.m_F64AlignmentMotionHead1X * I32Head1Sign * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead2X * I32Head2Sign * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead3X * I32Head3Sign * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionHead4X * I32Head4Sign * CEnvironment.DIRECTION_INFO,
                                       this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                       this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                   );
                                    CLoggingTool.It.ShowManualAlignmentMotionMovement
                                    (
                                        EAlignmentAngle,
                                        this.m_F64AlignmentMotionHead1X * I32Head1Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionHead2X * I32Head2Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionHead3X * I32Head3Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionHead4X * I32Head4Sign * CEnvironment.DIRECTION_INFO,
                                        this.m_F64AlignmentMotionY * CSign.ALIGNSHARP_Y_AUTO_SIGN * this.m_I32AutoCheckY_AlignS_auto,
                                        this.m_F64AlignmentMotionT * CSign.ALIGNSHARP_T_AUTO_SIGN * this.m_I32AutoCheckT_AlignS_auto
                                    );
                                }

                                CMotionController.It.OPLC.FinishAlignment(ERESULT.RETRY);
                                CLoggingTool.It.RetryManualAlignment(EAlignmentAngle);
                            }
                        }
                        else
                        {
                            //20170413 whenever the user might use the manual align we stop the data retrieval for learning purpose,
                            // only when it turns out to be OK, on either in the auto align loop or in the manual loop, we initialize the default value for the m_BManualInterferanceOnLearning.
                        
                            EResult = ERESULT.OK;
                            EView = EVIEW.NONE;
                            CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                            CMotionController.It.OPLC.FinishAlignment(ERESULT.OK);
                            this.CURRENTRETRY = 0;
                            CLoggingTool.It.SuccessManualAlignment(EAlignmentAngle);
                        }
                        #endregion
                    }
                    #region EMANUAL.ALIGN_X
                    //else if (EManual == EMANUAL.ALIGN_X)
                    //{
                    //    double F64Head1Differ = 0;
                    //    double F64Head2Differ = 0;
                    //    double F64Head3Differ = 0;
                    //    double F64Head4Differ = 0;

                    //    if (OHead1Result != null) F64Head1Differ = ((OHead1Result.OImageInfo.OImage.Width / 2D) - OHead1Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                    //    if (OHead2Result != null) F64Head2Differ = ((OHead2Result.OImageInfo.OImage.Width / 2D) - OHead2Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                    //    if (OHead3Result != null) F64Head3Differ = ((OHead3Result.OImageInfo.OImage.Width / 2D) - OHead3Result.F64X) * CEnvironment.LEN_PER_PIXEL;
                    //    if (OHead4Result != null) F64Head4Differ = ((OHead4Result.OImageInfo.OImage.Width / 2D) - OHead4Result.F64X) * CEnvironment.LEN_PER_PIXEL;

                    //    this.m_F64AlignmentMotionHead1X = F64Head1Differ;
                    //    this.m_F64AlignmentMotionHead2X = F64Head2Differ;
                    //    this.m_F64AlignmentMotionHead3X = F64Head3Differ;
                    //    this.m_F64AlignmentMotionHead4X = F64Head4Differ;
                    //    //this.m_F64AlignmentMotionT = 0;
                    //    //this.m_F64AlignmentMotionY = 0;
                            
                        
                    //    if ((Math.Abs(F64Head1Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead1) ||
                    //        (Math.Abs(F64Head2Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead2) ||
                    //        (Math.Abs(F64Head3Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead3) ||
                    //        (Math.Abs(F64Head4Differ) > this.m_OAlignmentRecipe.F64AlignmentLimitHead4) ||
                    //        (Math.Abs(this.m_F64AlignmentMotionY) > this.m_OAlignmentRecipe.F64AlignmentLimitY) ||
                    //        (Math.Abs(this.m_F64AlignmentMotionT) > this.m_OAlignmentRecipe.F64AlignmentLimitT))
                    //    {
                    //        this.CURRENTRETRY++;
                    //        EResult = ERESULT.RETRY;
                    //        EView = EVIEW.NONE;
                    //        this.m_BUseAlignSharp = false;
                    //        this.m_I64AlignmentRequestedTick = DateTime.Now.Ticks;

                    //        if (OHead1Result == null) this.m_F64AlignmentMotionHead1X = 0;
                    //        if (OHead2Result == null) this.m_F64AlignmentMotionHead2X = 0;
                    //        if (OHead3Result == null) this.m_F64AlignmentMotionHead3X = 0;
                    //        if (OHead4Result == null) this.m_F64AlignmentMotionHead4X = 0;
                    //        //allgn_X
                    //        int I32Count = 0;
                    //        if (OHead1Result != null) I32Count++;
                    //        if (OHead2Result != null) I32Count++;
                    //        if (OHead3Result != null) I32Count++;
                    //        if (OHead4Result != null) I32Count++;
                    //        double averageX = (this.m_F64AlignmentMotionHead1X + m_F64AlignmentMotionHead2X + m_F64AlignmentMotionHead3X + m_F64AlignmentMotionHead4X) / I32Count;
                    //        if (this.m_BIndividual == false)
                    //        {
                    //            CMotionController.It.OPLC.SetStageMovement
                    //            (
                    //                averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                    //                this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                    //            );
                    //            CLoggingTool.It.ShowManualAlignmentMotionMovement
                    //            (
                    //                EAlignmentAngle,
                    //                averageX * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                averageX * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                    //                this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                    //            );
                    //        }
                    //        else
                    //        {
                    //            CMotionController.It.OPLC.SetStageMovement
                    //          (
                    //              this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //              this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //              this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //              this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //              this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                    //              this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                    //          );
                    //            CLoggingTool.It.ShowManualAlignmentMotionMovement
                    //            (
                    //                EAlignmentAngle,
                    //                this.m_F64AlignmentMotionHead1X * CSign.ALIGNX_HEAD1X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                this.m_F64AlignmentMotionHead2X * CSign.ALIGNX_HEAD2X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //               this.m_F64AlignmentMotionHead3X * CSign.ALIGNX_HEAD3X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                this.m_F64AlignmentMotionHead4X * CSign.ALIGNX_HEAD4X_AUTO_SIGN * this.m_I32AutoCheckX_AlignX_auto * CEnvironment.DIRECTION_INFO,
                    //                this.m_F64AlignmentMotionY * CSign.ALIGNX_Y_SIGN_AUTO * this.m_I32AutoCheckY_AlignX_auto,
                    //                this.m_F64AlignmentMotionT * CSign.ALIGNX_T_SIGN_AUTO * this.m_I32AutoCheckT_AlignX_auto
                    //            );
                    //        }
                    //        CMotionController.It.OPLC.FinishAlignment(ERESULT.RETRY);
                    //        CLoggingTool.It.RetryManualAlignment(EAlignmentAngle);
                    //    }
                    //    else
                    //    {
                    //        EResult = ERESULT.OK;
                    //        EView = EVIEW.NONE;
                    //        this.m_BUseAlignSharp = true;
                    //        this.m_I64AlignmentRequestedTick = long.MaxValue;

                    //        CMotionController.It.OPLC.FinishAlignment(ERESULT.OK);
                    //        this.CURRENTRETRY = 0;
                    //        CLoggingTool.It.SuccessManualAlignment(EAlignmentAngle);
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    EResult = ERESULT.NG;
                    EView = EVIEW.NONE;
                    this.m_BUseAlignSharp = true;
                    this.m_I64AlignmentRequestedTick = long.MaxValue;
                    CMotionController.It.OPLC.SetStageMovement(0, 0, 0, 0, 0, 0);//init 20170313
                    CMotionController.It.OPLC.FinishAlignment(ERESULT.NG);
                    CLoggingTool.It.FailManualAlignmentByNotManualState(EAlignmentAngle);
                }

                CAlignmentResult OResult = new CAlignmentResult(EAlignmentAngle, EAlignmentView);
                OResult.EResult = EResult;
                OResult.ENGView = EView;
                OResult.OHead1 = OHead1Result;
                OResult.OHead2 = OHead2Result;
                OResult.OHead3 = OHead3Result;
                OResult.OHead4 = OHead4Result;
                OResult.F64Head1X = this.m_F64AlignmentMotionHead1X;
                OResult.F64Head2X = this.m_F64AlignmentMotionHead2X;
                OResult.F64Head3X = this.m_F64AlignmentMotionHead3X;
                OResult.F64Head4X = this.m_F64AlignmentMotionHead4X;
                OResult.F64Y = this.m_F64AlignmentMotionY;
                OResult.F64T = this.m_F64AlignmentMotionT;

                this.OnFinishAlignment(OResult);
                this.SendAlignmentToFile(OResult);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

       


        private void RunDirectCut()
        {
            try
            {
                //이미지 검사 완료 여부 확인
                CCutResult OHead1Result = null;
                CCutResult OHead2Result = null;
                CCutResult OHead3Result = null;
                CCutResult OHead4Result = null;

                if ((CEnvironment.It.StrEnabled1 == "true")) OHead1Result = this.m_OHead1.GetDirectCutResult();
                if ((CEnvironment.It.StrEnabled2 == "true")) OHead2Result = this.m_OHead2.GetDirectCutResult();
                if ((CEnvironment.It.StrEnabled3 == "true")) OHead3Result = this.m_OHead3.GetDirectCutResult();
                if ((CEnvironment.It.StrEnabled4 == "true")) OHead4Result = this.m_OHead4.GetDirectCutResult();

                if (((this.m_EDirectCutView & EVIEW.HEAD1) == EVIEW.HEAD1) && (OHead1Result == null)) return;
                if (((this.m_EDirectCutView & EVIEW.HEAD2) == EVIEW.HEAD2) && (OHead2Result == null)) return;
                if (((this.m_EDirectCutView & EVIEW.HEAD3) == EVIEW.HEAD3) && (OHead3Result == null)) return;
                if (((this.m_EDirectCutView & EVIEW.HEAD4) == EVIEW.HEAD4) && (OHead4Result == null)) return;

                this.m_BRunDirectCut = false;

                //이미지 검사 결과 측정
                this.m_ODirectCutResult = new CDirectCutResult(this.m_EDirectCutAngle, this.m_EDirectCutView);
                this.m_ODirectCutResult.BInspected = true;
                this.m_ODirectCutResult.OHead1 = OHead1Result;
                this.m_ODirectCutResult.OHead2 = OHead2Result;
                this.m_ODirectCutResult.OHead3 = OHead3Result;
                this.m_ODirectCutResult.OHead4 = OHead4Result;

                if ((this.m_EDirectCutView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    if ((this.m_ODirectCutResult.OHead1.BInspected == true) && (this.m_ODirectCutResult.OHead1.BOK == true))
                    {
                        double F64Standard = CMotionController.It.OPLC.F64Head1Wheel;
                        double F64Length = (this.m_ODirectCutResult.OHead1.F64Length + F64Standard) * CEnvironment.DIRECTION_INFO;

                        this.m_ODirectCutResult.BHead1OK = (Math.Abs(F64Length) <= this.m_ODirectCutRecipe.F64DirectCutLimitHead1);
                        this.m_ODirectCutResult.F64Head1Standard = this.m_ODirectCutResult.OHead1.OImageInfo.OImage.Width / 2D + F64Standard / CEnvironment.LEN_PER_PIXEL;
                        this.m_ODirectCutResult.F64Head1Length = F64Length;

                        CLoggingTool.It.ShowDirectCut(this.m_EDirectCutAngle, EVIEW.HEAD1, F64Length);
                    }
                    else CLoggingTool.It.ShowDirectCutNotFound(this.m_EDirectCutAngle, EVIEW.HEAD1);
                }
                if ((this.m_EDirectCutView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    if ((this.m_ODirectCutResult.OHead2.BInspected == true) && (this.m_ODirectCutResult.OHead2.BOK == true))
                    {
                        double F64Standard = CMotionController.It.OPLC.F64Head2Wheel;
                        double F64Length = (this.m_ODirectCutResult.OHead2.F64Length + F64Standard) * CEnvironment.DIRECTION_INFO;

                        this.m_ODirectCutResult.BHead2OK = (Math.Abs(F64Length) <= this.m_ODirectCutRecipe.F64DirectCutLimitHead2);
                        this.m_ODirectCutResult.F64Head2Standard = this.m_ODirectCutResult.OHead2.OImageInfo.OImage.Width / 2D + F64Standard / CEnvironment.LEN_PER_PIXEL;
                        this.m_ODirectCutResult.F64Head2Length = F64Length;

                        CLoggingTool.It.ShowDirectCut(this.m_EDirectCutAngle, EVIEW.HEAD2, F64Length);
                    }
                    else CLoggingTool.It.ShowDirectCutNotFound(this.m_EDirectCutAngle, EVIEW.HEAD2);
                }
                if ((this.m_EDirectCutView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    if ((this.m_ODirectCutResult.OHead3.BInspected == true) && (this.m_ODirectCutResult.OHead3.BOK == true))
                    {
                        double F64Standard = CMotionController.It.OPLC.F64Head3Wheel;
                        double F64Length = (this.m_ODirectCutResult.OHead3.F64Length + F64Standard) * CEnvironment.DIRECTION_INFO;

                        this.m_ODirectCutResult.BHead3OK = (Math.Abs(F64Length) <= this.m_ODirectCutRecipe.F64DirectCutLimitHead3);
                        this.m_ODirectCutResult.F64Head3Standard = this.m_ODirectCutResult.OHead3.OImageInfo.OImage.Width / 2D + F64Standard / CEnvironment.LEN_PER_PIXEL;
                        this.m_ODirectCutResult.F64Head3Length = F64Length;

                        CLoggingTool.It.ShowDirectCut(this.m_EDirectCutAngle, EVIEW.HEAD3, F64Length);
                    }
                    else CLoggingTool.It.ShowDirectCutNotFound(this.m_EDirectCutAngle, EVIEW.HEAD3);
                }
                if ((this.m_EDirectCutView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    if ((this.m_ODirectCutResult.OHead4.BInspected == true) && (this.m_ODirectCutResult.OHead4.BOK == true))
                    {
                        double F64Standard = CMotionController.It.OPLC.F64Head4Wheel;
                        double F64Length = (this.m_ODirectCutResult.OHead4.F64Length + F64Standard) * CEnvironment.DIRECTION_INFO;

                        this.m_ODirectCutResult.BHead4OK = (Math.Abs(F64Length) <= this.m_ODirectCutRecipe.F64DirectCutLimitHead4);
                        this.m_ODirectCutResult.F64Head4Standard = this.m_ODirectCutResult.OHead4.OImageInfo.OImage.Width / 2D + F64Standard / CEnvironment.LEN_PER_PIXEL;
                        this.m_ODirectCutResult.F64Head4Length = F64Length;

                        CLoggingTool.It.ShowDirectCut(this.m_EDirectCutAngle, EVIEW.HEAD4, F64Length);
                    }
                    else CLoggingTool.It.ShowDirectCutNotFound(this.m_EDirectCutAngle, EVIEW.HEAD4);
                }


                //이미지 검사 결과 종합 판단 및 모션 전달
                if ((((this.m_EDirectCutView & EVIEW.HEAD1) != EVIEW.HEAD1) || (this.m_ODirectCutResult.BHead1OK == true)) &&
                    (((this.m_EDirectCutView & EVIEW.HEAD2) != EVIEW.HEAD2) || (this.m_ODirectCutResult.BHead2OK == true)) &&
                    (((this.m_EDirectCutView & EVIEW.HEAD3) != EVIEW.HEAD3) || (this.m_ODirectCutResult.BHead3OK == true)) &&
                    (((this.m_EDirectCutView & EVIEW.HEAD4) != EVIEW.HEAD4) || (this.m_ODirectCutResult.BHead4OK == true)))
                {
                    this.m_ODirectCutResult.EResult = ERESULT.OK;

                    CMotionController.It.OPLC.FinishCut(this.m_ODirectCutResult.EResult);
                    CLoggingTool.It.SuccessDirectCut(this.m_EDirectCutAngle);
                }
                else
                {
                    this.m_ODirectCutResult.EResult = ERESULT.NG;

                    CMotionController.It.OPLC.FinishCut(this.m_ODirectCutResult.EResult);

                    if ((((this.m_EDirectCutView & EVIEW.HEAD1) != EVIEW.HEAD1) || ((this.m_ODirectCutResult.OHead1.BInspected == true) && (this.m_ODirectCutResult.OHead1.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD2) != EVIEW.HEAD2) || ((this.m_ODirectCutResult.OHead2.BInspected == true) && (this.m_ODirectCutResult.OHead2.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD3) != EVIEW.HEAD3) || ((this.m_ODirectCutResult.OHead3.BInspected == true) && (this.m_ODirectCutResult.OHead3.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD4) != EVIEW.HEAD4) || ((this.m_ODirectCutResult.OHead4.BInspected == true) && (this.m_ODirectCutResult.OHead4.BOK == true))))
                    {
                        CLoggingTool.It.FailDirectCutByOutOfLength(this.m_EDirectCutAngle);
                    }
                    else CLoggingTool.It.FailDirectCutByCannotFoundCutLine(this.m_EDirectCutAngle);
                }

                //화면 표현 및 리포트 파일 저장
                this.OnFinishDirectCut(this.m_ODirectCutResult);
                this.SendDirectCutToFile(this.m_ODirectCutResult);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void RunCrossCut()
        {
            try
            {
                //이미지 검사 완료 여부 확인
                CCutResult OHead1Result = null;
                CCutResult OHead2Result = null;
                CCutResult OHead3Result = null;
                CCutResult OHead4Result = null;

                if ((CEnvironment.It.StrEnabled1 == "true")) OHead1Result = this.m_OHead1.GetCrossCutResult();
                if ((CEnvironment.It.StrEnabled2 == "true")) OHead2Result = this.m_OHead2.GetCrossCutResult();
                if ((CEnvironment.It.StrEnabled3 == "true")) OHead3Result = this.m_OHead3.GetCrossCutResult();
                if ((CEnvironment.It.StrEnabled4 == "true")) OHead4Result = this.m_OHead4.GetCrossCutResult();

                //CCutResult OHead1Result = this.m_OHead1.GetCrossCutResult();
                //CCutResult OHead2Result = this.m_OHead2.GetCrossCutResult();
                //CCutResult OHead3Result = this.m_OHead3.GetCrossCutResult();
                //CCutResult OHead4Result = this.m_OHead4.GetCrossCutResult();
                if (((this.m_ECrossCutView & EVIEW.HEAD1) == EVIEW.HEAD1) && (OHead1Result == null)) return;
                if (((this.m_ECrossCutView & EVIEW.HEAD2) == EVIEW.HEAD2) && (OHead2Result == null)) return;
                if (((this.m_ECrossCutView & EVIEW.HEAD3) == EVIEW.HEAD3) && (OHead3Result == null)) return;
                if (((this.m_ECrossCutView & EVIEW.HEAD4) == EVIEW.HEAD4) && (OHead4Result == null)) return;

                this.m_BRunCrossCut = false;

                //이미지 검사 결과 측정
                CCrossCutResult OResult = new CCrossCutResult(this.m_ECrossCutAngle, this.m_ECrossCutView);
                OResult.BInspected = true;
                OResult.OHead1 = OHead1Result;
                OResult.OHead2 = OHead2Result;
                OResult.OHead3 = OHead3Result;
                OResult.OHead4 = OHead4Result;

                if ((this.m_ECrossCutView & EVIEW.HEAD1) == EVIEW.HEAD1)
                {
                    if ((OResult.OHead1.BInspected == true) && (OResult.OHead1.BOK == true))
                    {
                        double F64Standard = this.m_ODirectCutResult.OHead1.F64Length;
                        double F64Length = (OResult.OHead1.F64Length - F64Standard) * CEnvironment.DIRECTION_INFO;

                        OResult.BHead1OK = (Math.Abs(F64Length) <= this.m_OCrossCutRecipe.F64CrossCutLimitHead1);
                        OResult.F64Head1Standard = this.m_ODirectCutResult.OHead1.F64MidX;
                        OResult.F64Head1Length = F64Length;

                        CLoggingTool.It.ShowCrossCut(this.m_ECrossCutAngle, EVIEW.HEAD1, F64Length);
                    }
                    else CLoggingTool.It.ShowCrossCutNotFound(this.m_ECrossCutAngle, EVIEW.HEAD1);
                }
                if ((this.m_ECrossCutView & EVIEW.HEAD2) == EVIEW.HEAD2)
                {
                    if ((OResult.OHead2.BInspected == true) && (OResult.OHead2.BOK == true))
                    {
                        double F64Standard = this.m_ODirectCutResult.OHead2.F64Length;
                        double F64Length = (OResult.OHead2.F64Length - F64Standard) * CEnvironment.DIRECTION_INFO;

                        OResult.BHead2OK = (Math.Abs(F64Length) <= this.m_OCrossCutRecipe.F64CrossCutLimitHead2);
                        OResult.F64Head2Standard = this.m_ODirectCutResult.OHead2.F64MidX;
                        OResult.F64Head2Length = F64Length;

                        CLoggingTool.It.ShowCrossCut(this.m_ECrossCutAngle, EVIEW.HEAD2, F64Length);
                    }
                    else CLoggingTool.It.ShowCrossCutNotFound(this.m_ECrossCutAngle, EVIEW.HEAD2);
                }
                if ((this.m_ECrossCutView & EVIEW.HEAD3) == EVIEW.HEAD3)
                {
                    if ((OResult.OHead3.BInspected == true) && (OResult.OHead3.BOK == true))
                    {
                        double F64Standard = this.m_ODirectCutResult.OHead3.F64Length;
                        double F64Length = (OResult.OHead3.F64Length - F64Standard) * CEnvironment.DIRECTION_INFO;

                        OResult.BHead3OK = (Math.Abs(F64Length) <= this.m_OCrossCutRecipe.F64CrossCutLimitHead3);
                        OResult.F64Head3Standard = this.m_ODirectCutResult.OHead3.F64MidX;
                        OResult.F64Head3Length = F64Length;

                        CLoggingTool.It.ShowCrossCut(this.m_ECrossCutAngle, EVIEW.HEAD3, F64Length);
                    }
                    else CLoggingTool.It.ShowCrossCutNotFound(this.m_ECrossCutAngle, EVIEW.HEAD3);
                }
                if ((this.m_ECrossCutView & EVIEW.HEAD4) == EVIEW.HEAD4)
                {
                    if ((OResult.OHead4.BInspected == true) && (OResult.OHead4.BOK == true))
                    {
                        double F64Standard = this.m_ODirectCutResult.OHead4.F64Length;
                        double F64Length = (OResult.OHead4.F64Length - F64Standard) * CEnvironment.DIRECTION_INFO;

                        OResult.BHead4OK = (Math.Abs(F64Length) <= this.m_OCrossCutRecipe.F64CrossCutLimitHead4);
                        OResult.F64Head4Standard = this.m_ODirectCutResult.OHead4.F64MidX;
                        OResult.F64Head4Length = F64Length;

                        CLoggingTool.It.ShowCrossCut(this.m_ECrossCutAngle, EVIEW.HEAD4, F64Length);
                    }
                    else CLoggingTool.It.ShowCrossCutNotFound(this.m_ECrossCutAngle, EVIEW.HEAD4);
                }


                //이미지 검사 결과 종합 판단 및 모션 전달
                if ((((this.m_ECrossCutView & EVIEW.HEAD1) != EVIEW.HEAD1) || (OResult.BHead1OK == true)) &&
                    (((this.m_ECrossCutView & EVIEW.HEAD2) != EVIEW.HEAD2) || (OResult.BHead2OK == true)) &&
                    (((this.m_ECrossCutView & EVIEW.HEAD3) != EVIEW.HEAD3) || (OResult.BHead3OK == true)) &&
                    (((this.m_ECrossCutView & EVIEW.HEAD4) != EVIEW.HEAD4) || (OResult.BHead4OK == true)))
                {
                    OResult.EResult = ERESULT.OK;

                    CMotionController.It.OPLC.FinishCut(OResult.EResult);
                    CLoggingTool.It.SuccessCrossCut(this.m_ECrossCutAngle);
                }
                else
                {
                    OResult.EResult = ERESULT.NG;

                    CMotionController.It.OPLC.FinishCut(OResult.EResult);

                    if ((((this.m_EDirectCutView & EVIEW.HEAD1) != EVIEW.HEAD1) || ((OResult.OHead1.BInspected == true) && (OResult.OHead1.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD2) != EVIEW.HEAD2) || ((OResult.OHead2.BInspected == true) && (OResult.OHead2.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD3) != EVIEW.HEAD3) || ((OResult.OHead3.BInspected == true) && (OResult.OHead3.BOK == true))) &&
                        (((this.m_EDirectCutView & EVIEW.HEAD4) != EVIEW.HEAD4) || ((OResult.OHead4.BInspected == true) && (OResult.OHead4.BOK == true))))
                    {
                        CLoggingTool.It.FailCrossCutByOutOfLength(this.m_ECrossCutAngle);
                    }
                    else CLoggingTool.It.FailCrossCutByCannotFoundCutLine(this.m_ECrossCutAngle);
                }


                //화면 표현 및 리포트 파일 저장
                this.OnFinishCrossCut(OResult);
                this.SendCrossCutToFile(OResult);
                this.m_ODirectCutResult = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public bool CanChangeRecipe()
        {
            bool BResult = false;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_EAnalysis == EANALYSIS.INSPECTION)
                {
                    if ((this.m_BRunAlignment == false) &&
                        (this.m_BRunDirectCut == false) &&
                        (this.m_BRunCrossCut == false))
                    {
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
                Monitor.Exit(this.m_OInterrupt);
            }

            return BResult;
        }
        

        public void EndInspection()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                this.CURRENTRETRY = 0;
                this.Flag = true;
                CMotionController.It.OPLC.EndInspection();

                this.m_EAnalysis = EANALYSIS.NONE;
         
                CLoggingTool.It.EndInspection();
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

        //20170410
        private void SendAlignmentToFile(CAlignmentResult OResult)
        {
            try
            {
                DateTime OTime = DateTime.Now;
                string StrHead1 = string.Empty;
                string StrHead2 = string.Empty;
                string StrHead3 = string.Empty;
                string StrHead4 = string.Empty;

                if (OResult.OHead1 != null)
                {
                    CImageSaveFile OHead1File = new CImageSaveFile(OTime, OResult.OHead1.OImageInfo.OImage.ToBitmap());
                    OHead1File.StrDirectory = ".\\Image\\Alignment\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead1File.StrFile = "Head1.jpg";
                    OHead1File.OFormat = ImageFormat.Jpeg;

                    StrHead1 = OHead1File.GetFile();
                    CImageSaveTool.It.Set(OHead1File);
                }
                if (OResult.OHead2 != null)
                {
                    CImageSaveFile OHead2File = new CImageSaveFile(OTime, OResult.OHead2.OImageInfo.OImage.ToBitmap());
                    OHead2File.StrDirectory = ".\\Image\\Alignment\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead2File.StrFile = "Head2.jpg";
                    OHead2File.OFormat = ImageFormat.Jpeg;

                    StrHead2 = OHead2File.GetFile();
                    CImageSaveTool.It.Set(OHead2File);
                }
                if (OResult.OHead3 != null)
                {
                    CImageSaveFile OHead3File = new CImageSaveFile(OTime, OResult.OHead3.OImageInfo.OImage.ToBitmap());
                    OHead3File.StrDirectory = ".\\Image\\Alignment\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead3File.StrFile = "Head3.jpg";
                    OHead3File.OFormat = ImageFormat.Jpeg;

                    StrHead3 = OHead3File.GetFile();
                    CImageSaveTool.It.Set(OHead3File);
                }
                if (OResult.OHead4 != null)
                {
                    CImageSaveFile OHead4File = new CImageSaveFile(OTime, OResult.OHead4.OImageInfo.OImage.ToBitmap());
                    OHead4File.StrDirectory = ".\\Image\\Alignment\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead4File.StrFile = "Head4.jpg";
                    OHead4File.OFormat = ImageFormat.Jpeg;

                    StrHead4 = OHead4File.GetFile();
                    CImageSaveTool.It.Set(OHead4File);
                }
                //if ((OResult.EResult == ERESULT.RETRY) || (OResult.EResult == ERESULT.OK))
                //{
                    IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_ALIGNMENT);

                    try
                    {
                        OTable.BeginSyncData();

                        int I32RowIndex = OTable.InsertRow();
                        OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_DATETIME, OTime.ToString("yyyy.MM.dd HH:mm:ss fff"));
                        OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_RECIPE, this.m_ORecipe.StrName);
                        OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_ANGLE, ((int)OResult.EAngle).ToString());
                        OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_RESULT, OResult.EResult.ToString());
                        if ((OResult.EResult == ERESULT.RETRY) || (OResult.EResult == ERESULT.OK))
                        {
                            if ((OResult.EView & EVIEW.HEAD1) == EVIEW.HEAD1 && OResult.OHead1 != null) OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD1_X, Math.Round(OResult.F64Head1X, 4));
                            if ((OResult.EView & EVIEW.HEAD2) == EVIEW.HEAD2 && OResult.OHead2 != null) OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD2_X, Math.Round(OResult.F64Head2X, 4));
                            if ((OResult.EView & EVIEW.HEAD3) == EVIEW.HEAD3 && OResult.OHead3 != null) OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD3_X, Math.Round(OResult.F64Head3X, 4));
                            if ((OResult.EView & EVIEW.HEAD4) == EVIEW.HEAD4 && OResult.OHead4 != null) OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_HEAD4_X, Math.Round(OResult.F64Head4X, 4));
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_Y, Math.Round(OResult.F64Y, 4));
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_MOVEMENT_T, Math.Round(OResult.F64T, 5));
                        }
                        if ((OResult.EView & EVIEW.HEAD1) == EVIEW.HEAD1 && OResult.OHead1 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD1_RESULT, OResult.OHead1.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD1_FILE, StrHead1);

                            if ((OResult.OHead1.BInspected == true) && (OResult.OHead1.BOK == true))
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_X, OResult.OHead1.F64X);
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD1_PIXEL_Y, OResult.OHead1.F64Y);
                            }
                        }
                        if ((OResult.EView & EVIEW.HEAD2) == EVIEW.HEAD2 && OResult.OHead2 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD2_RESULT, OResult.OHead2.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD2_FILE, StrHead2);

                            if ((OResult.OHead2.BInspected == true) && (OResult.OHead2.BOK == true))
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_X, OResult.OHead2.F64X);
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD2_PIXEL_Y, OResult.OHead2.F64Y);
                            }
                        }
                        if ((OResult.EView & EVIEW.HEAD3) == EVIEW.HEAD3 && OResult.OHead3 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD3_RESULT, OResult.OHead3.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD3_FILE, StrHead3);

                            if ((OResult.OHead3.BInspected == true) && (OResult.OHead3.BOK == true))
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_X, OResult.OHead3.F64X);
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD3_PIXEL_Y, OResult.OHead3.F64Y);
                            }
                        }
                        if ((OResult.EView & EVIEW.HEAD4) == EVIEW.HEAD4 && OResult.OHead4 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD4_RESULT, OResult.OHead4.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD4_FILE, StrHead4);

                            if ((OResult.OHead4.BInspected == true) && (OResult.OHead4.BOK == true))
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_X, OResult.OHead4.F64X);
                                OTable.Update(I32RowIndex, CDB.REPORT_ALIGNMENT_HEAD4_PIXEL_Y, OResult.OHead4.F64Y);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Throw(ex);
                    }
                    finally
                    {
                        OTable.EndSyncData();
                    }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


       //20170410
        private void SendDirectCutToFile(CDirectCutResult OResult)
        {
            try
            {
                DateTime OTime = DateTime.Now;
                string StrHead1 = string.Empty;
                string StrHead2 = string.Empty;
                string StrHead3 = string.Empty;
                string StrHead4 = string.Empty;

                if (OResult.OHead1 != null)
                {
                    CImageSaveFile OHead1File = new CImageSaveFile(OTime, OResult.OHead1.OImageInfo.OImage.ToBitmap());
                    //20170410
                    OHead1File.StrDirectory = ".\\Image\\DirectCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}"; 
                    OHead1File.StrFile = "Head1.jpg";
                    OHead1File.OFormat = ImageFormat.Jpeg;

                    StrHead1 = OHead1File.GetFile();
                    CImageSaveTool.It.Set(OHead1File);
                }
                if (OResult.OHead2 != null)
                {
                    CImageSaveFile OHead2File = new CImageSaveFile(OTime, OResult.OHead2.OImageInfo.OImage.ToBitmap());
                    OHead2File.StrDirectory = ".\\Image\\DirectCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}"; 
                    OHead2File.StrFile = "Head2.jpg";
                    OHead2File.OFormat = ImageFormat.Jpeg;

                    StrHead2 = OHead2File.GetFile();
                    CImageSaveTool.It.Set(OHead2File);
                }
                if (OResult.OHead3 != null)
                {
                    CImageSaveFile OHead3File = new CImageSaveFile(OTime, OResult.OHead3.OImageInfo.OImage.ToBitmap());
                    OHead3File.StrDirectory = ".\\Image\\DirectCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}"; 
                    OHead3File.StrFile = "Head3.jpg";
                    OHead3File.OFormat = ImageFormat.Jpeg;

                    StrHead3 = OHead3File.GetFile();
                    CImageSaveTool.It.Set(OHead3File);
                }
                if (OResult.OHead4 != null)
                {
                    CImageSaveFile OHead4File = new CImageSaveFile(OTime, OResult.OHead4.OImageInfo.OImage.ToBitmap());
                    OHead4File.StrDirectory = ".\\Image\\DirectCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}"; 
                    OHead4File.StrFile = "Head4.jpg";
                    OHead4File.OFormat = ImageFormat.Jpeg;

                    StrHead4 = OHead4File.GetFile();
                    CImageSaveTool.It.Set(OHead4File);
                }

                //if ((OResult.EResult == ERESULT.RETRY) || (OResult.EResult == ERESULT.OK))
                //{
                    IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_CUT);

                    try
                    {
                        OTable.BeginSyncData();

                        int I32RowIndex = OTable.InsertRow();
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_DATETIME, OTime.ToString("yyyy.MM.dd HH:mm:ss fff"));
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_RECIPE, this.m_ORecipe.StrName);
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_ANGLE, ((int)OResult.EAngle).ToString());
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_ITEM, "DIRECT CUT");
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_RESULT, OResult.EResult.ToString());

                        if ((OResult.EView & EVIEW.HEAD1) == EVIEW.HEAD1 && OResult.OHead1 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_FOUND, OResult.OHead1.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_FILE, StrHead1);

                            if (OResult.OHead1.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_STANDARD_AXIS, OResult.F64Head1Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT_AXIS, OResult.OHead1.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT, (OResult.BHead1OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_LENGTH, Math.Round(OResult.F64Head1Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT, "NG");
                        }

                        if ((OResult.EView & EVIEW.HEAD2) == EVIEW.HEAD2 && OResult.OHead2 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_FOUND, OResult.OHead2.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_FILE, StrHead2);

                            if (OResult.OHead2.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_STANDARD_AXIS, OResult.F64Head2Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT_AXIS, OResult.OHead2.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT, (OResult.BHead2OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_LENGTH, Math.Round(OResult.F64Head2Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT, "NG");
                        }
                        if ((OResult.EView & EVIEW.HEAD3) == EVIEW.HEAD3 && OResult.OHead3 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_FOUND, OResult.OHead3.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_FILE, StrHead3);

                            if (OResult.OHead3.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_STANDARD_AXIS, OResult.F64Head3Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT_AXIS, OResult.OHead3.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT, (OResult.BHead3OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_LENGTH, Math.Round(OResult.F64Head3Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT, "NG");
                        }
                        if ((OResult.EView & EVIEW.HEAD4) == EVIEW.HEAD4 && OResult.OHead4 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_FOUND, OResult.OHead4.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_FILE, StrHead4);

                            if (OResult.OHead4.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_STANDARD_AXIS, OResult.F64Head4Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT_AXIS, OResult.OHead4.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT, (OResult.BHead4OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_LENGTH, Math.Round(OResult.F64Head4Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT, "NG");
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Throw(ex);
                    }
                    finally
                    {
                        OTable.EndSyncData();
                    }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

        }

        //20170410
        private void SendCrossCutToFile(CCrossCutResult OResult)
        {
            try
            {
                DateTime OTime = DateTime.Now;
                string StrHead1 = string.Empty;
                string StrHead2 = string.Empty;
                string StrHead3 = string.Empty;
                string StrHead4 = string.Empty;

                if (OResult.OHead1 != null)
                {
                    CImageSaveFile OHead1File = new CImageSaveFile(OTime, OResult.OHead1.OImageInfo.OImage.ToBitmap());
                    //20170410
                    OHead1File.StrDirectory = ".\\Image\\CrossCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead1File.StrFile = "Head1.jpg";
                    OHead1File.OFormat = ImageFormat.Jpeg;

                    StrHead1 = OHead1File.GetFile();
                    CImageSaveTool.It.Set(OHead1File);
                }
                if (OResult.OHead2 != null)
                {
                    CImageSaveFile OHead2File = new CImageSaveFile(OTime, OResult.OHead2.OImageInfo.OImage.ToBitmap());
                    OHead2File.StrDirectory = ".\\Image\\CrossCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead2File.StrFile = "Head2.jpg";
                    OHead2File.OFormat = ImageFormat.Jpeg;

                    StrHead2 = OHead2File.GetFile();
                    CImageSaveTool.It.Set(OHead2File);
                }
                if (OResult.OHead3 != null)
                {
                    CImageSaveFile OHead3File = new CImageSaveFile(OTime, OResult.OHead3.OImageInfo.OImage.ToBitmap());
                    OHead3File.StrDirectory = ".\\Image\\CrossCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead3File.StrFile = "Head3.jpg";
                    OHead3File.OFormat = ImageFormat.Jpeg;

                    StrHead3 = OHead3File.GetFile();
                    CImageSaveTool.It.Set(OHead3File);
                }
                if (OResult.OHead4 != null)
                {
                    CImageSaveFile OHead4File = new CImageSaveFile(OTime, OResult.OHead4.OImageInfo.OImage.ToBitmap());
                    OHead4File.StrDirectory = ".\\Image\\CrossCut\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead4File.StrFile = "Head4.jpg";
                    OHead4File.OFormat = ImageFormat.Jpeg;

                    StrHead4 = OHead4File.GetFile();
                    CImageSaveTool.It.Set(OHead4File);
                }

                //if ((OResult.EResult == ERESULT.RETRY) || (OResult.EResult == ERESULT.OK))
                //{
                    IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT_CUT);

                    try
                    {
                        OTable.BeginSyncData();

                        int I32RowIndex = OTable.InsertRow();
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_DATETIME, OTime.ToString("yyyy.MM.dd HH:mm:ss fff"));
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_RECIPE, this.m_ORecipe.StrName);
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_ANGLE, ((int)OResult.EAngle).ToString());
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_ITEM, "CROSS CUT");
                        OTable.Update(I32RowIndex, CDB.REPORT_CUT_RESULT, OResult.EResult.ToString());

                        if ((OResult.EView & EVIEW.HEAD1) == EVIEW.HEAD1 && OResult.OHead1 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_FOUND, OResult.OHead1.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_FILE, StrHead1);

                            if (OResult.OHead1.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_STANDARD_AXIS, OResult.F64Head1Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT_AXIS, OResult.OHead1.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT, (OResult.BHead1OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_LENGTH, Math.Round(OResult.F64Head1Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD1_RESULT, "NG");
                        }

                        if ((OResult.EView & EVIEW.HEAD2) == EVIEW.HEAD2 && OResult.OHead2 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_FOUND, OResult.OHead2.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_FILE, StrHead2);

                            if (OResult.OHead2.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_STANDARD_AXIS, OResult.F64Head2Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT_AXIS, OResult.OHead2.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT, (OResult.BHead2OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_LENGTH, Math.Round(OResult.F64Head2Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD2_RESULT, "NG");
                        }
                        if ((OResult.EView & EVIEW.HEAD3) == EVIEW.HEAD3 && OResult.OHead3 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_FOUND, OResult.OHead3.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_FILE, StrHead3);

                            if (OResult.OHead3.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_STANDARD_AXIS, OResult.F64Head3Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT_AXIS, OResult.OHead3.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT, (OResult.BHead3OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_LENGTH, Math.Round(OResult.F64Head3Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD3_RESULT, "NG");
                        }
                        if ((OResult.EView & EVIEW.HEAD4) == EVIEW.HEAD4 && OResult.OHead4 != null)
                        {
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_FOUND, OResult.OHead4.BOK);
                            OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_FILE, StrHead4);

                            if (OResult.OHead4.BOK == true)
                            {
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_STANDARD_AXIS, OResult.F64Head4Standard);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT_AXIS, OResult.OHead4.F64MidX);
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT, (OResult.BHead4OK == true) ? "OK" : "NG");
                                OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_LENGTH, Math.Round(OResult.F64Head4Length, 3).ToString());
                            }
                            else OTable.Update(I32RowIndex, CDB.REPORT_CUT_HEAD4_RESULT, "NG");
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Throw(ex);
                    }
                    finally
                    {
                        OTable.EndSyncData();
                    }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void BeginSync()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndSync()
        {
            try
            {
                Monitor.Exit(this.m_OInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void OnFinishCalibrationMovement(CCalibrationResult OResult)
        {
            try
            {
                if (this.m_OFinishCalibrationMovement != null)
                {
                    this.m_OFinishCalibrationMovement(OResult);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void OnFinishCalibration(bool BIsSuccess)
        {
            try
            {
                if (this.m_OFinishCalibration != null)
                {
                    this.m_OFinishCalibration(BIsSuccess);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void OnFinishAlignment(CAlignmentResult OResult)
        {
            try
            {
                if (this.m_OFinishAlignment != null)
                {
                    this.m_OFinishAlignment(OResult);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void OnFinishDirectCut(CDirectCutResult OResult)
        {
            try
            {
                if (this.m_OFinishDirectCut != null)
                {
                    this.m_OFinishDirectCut(OResult);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void OnFinishCrossCut(CCrossCutResult OResult)
        {
            try
            {
                if (this.m_OFinishCrossCut != null)
                {
                    this.m_OFinishCrossCut(OResult);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Dispose()
        {
            try
            {
                this.EndWork();

                if (this.m_OHead1 != null)
                {
                    this.m_OHead1.Dispose();
                    this.m_OHead1 = null;
                }
                if (this.m_OHead2 != null)
                {
                    this.m_OHead2.Dispose();
                    this.m_OHead2 = null;
                }
                if (this.m_OHead3 != null)
                {
                    this.m_OHead3.Dispose();
                    this.m_OHead3 = null;
                }
                if (this.m_OHead4 != null)
                {
                    this.m_OHead4.Dispose();
                    this.m_OHead4 = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region ENUM
        private enum EANALYSIS : byte
        {
            NONE = 0x00,
            CALIBRATION,
            INSPECTION
        }
        #endregion
    }
}
