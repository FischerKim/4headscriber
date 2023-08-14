using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using smal_CS;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CAlignmentAlgorithm
    {
        #region VARIABLE
        private WrapStaticLibSmal m_OTool = null;

        private int m_I32Index = 0;
        private int m_I32ViewCount = 2;

        private List<List<double>> m_OCalibSource = null;
        private List<List<double>> m_OCalibResult = null;
        #endregion


        #region PROPERTIES
        public WrapStaticLibSmal OTool
        {
            get { return this.m_OTool; }
        }


        public int I32Index
        {
            get { return this.m_I32Index; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAlignmentAlgorithm(WrapStaticLibSmal OTool, int I32Index)
        {
            try
            {
                this.m_OTool = OTool;
                this.m_I32Index = I32Index;

                this.m_OCalibSource = new List<List<double>>();
                this.m_OCalibResult = new List<List<double>>();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void SetStageKind(EKindOfStage EKind)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                this.m_OTool.SmalSetStageType(this.m_I32Index, (int)EKind);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public EKindOfStage GetStageKind()
        {
            EKindOfStage EResult = EKindOfStage.NONE;

            try
            {
                CAlignmentSet.It.BeginSync();

                EResult = (EKindOfStage)Enum.ToObject(typeof(EKindOfStage), this.m_OTool.SmalGetStageType(this.m_I32Index));
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return EResult;
        }


        public void SetUVWParams(double F64Radius, List<double> LstF64BearAngle, List<double> LstF64SlideDirection)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                this.m_OTool.SmalSetUvwStageParams(this.m_I32Index, F64Radius, LstF64BearAngle, LstF64SlideDirection);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public void SetMaxViewCount(int I32Count)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                if (I32Count < 2)
                {
                    new Exception("To proceed alignment need over 2 views!");
                }
                else
                {
                    this.m_OTool.SmalSetMaxNumViews(this.m_I32Index, I32Count);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public void SetUseViewCount(int I32Count)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                if (I32Count < 2)
                {
                    new Exception("To proceed alignment need over 2 views!");
                }
                else
                {
                    this.m_I32ViewCount = I32Count;
                    this.m_OTool.SmalSetNumCameras(this.m_I32Index, I32Count);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public int LoadCalibration(double F64SectionXY, int I32PointXY, double F64SectionT, int I32PointT)
        {
            int I32Result = 0;

            try
            {
                CAlignmentSet.It.BeginSync();

                if ((I32PointXY < 3) || (I32PointT < 3))
                {
                    throw new Exception("The point count must be over 3 points!");
                }
                else
                {
                    this.m_OCalibSource.Add(new List<double>());
                    this.m_OCalibSource.Add(new List<double>());
                    this.m_OCalibSource.Add(new List<double>());

                    this.m_OTool.SmalSetCalibPosParams(this.m_I32Index, F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                    this.m_OTool.SmalGetCalibPos(this.m_I32Index, this.m_OCalibSource);
                    I32Result = this.m_OCalibSource[0].Count;

                    if (this.m_I32ViewCount == 2)
                    {
                        this.m_OCalibResult = new List<List<double>>();
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                    }
                    else if (this.m_I32ViewCount == 3)
                    {
                        this.m_OCalibResult = new List<List<double>>();
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                    }
                    else if (this.m_I32ViewCount == 4)
                    {
                        this.m_OCalibResult = new List<List<double>>();
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                        this.m_OCalibResult.Add(new List<double>());
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return I32Result;
        }


        public void GetCalibrationMovement(int I32CalibrationIndex, out double F64X, out double F64Y, out double F64T)
        {
            F64X = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                F64X = this.m_OCalibSource[0][I32CalibrationIndex];
                F64Y = this.m_OCalibSource[1][I32CalibrationIndex];
                F64T = this.m_OCalibSource[2][I32CalibrationIndex];
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SetCalibrationResult(double[] ArrF64X, double[] ArrF64Y)
        {
            try
            {
                if ((this.m_I32ViewCount != ArrF64X.Length) || (this.m_I32ViewCount != ArrF64Y.Length))
                {
                    throw new Exception("The used view count is different with inputed view count!");
                }
                else
                {
                    if (this.m_I32ViewCount == 2)
                    {
                        this.m_OCalibResult[0].Add(ArrF64X[0]);
                        this.m_OCalibResult[1].Add(ArrF64Y[0]);
                        this.m_OCalibResult[2].Add(ArrF64X[1]);
                        this.m_OCalibResult[3].Add(ArrF64Y[1]);
                    }
                    else if (this.m_I32ViewCount == 3)
                    {
                        this.m_OCalibResult[0].Add(ArrF64X[0]);
                        this.m_OCalibResult[1].Add(ArrF64Y[0]);
                        this.m_OCalibResult[2].Add(ArrF64X[1]);
                        this.m_OCalibResult[3].Add(ArrF64Y[1]);
                        this.m_OCalibResult[4].Add(ArrF64X[2]);
                        this.m_OCalibResult[5].Add(ArrF64Y[2]);
                    }
                    else if (this.m_I32ViewCount == 4)
                    {
                        this.m_OCalibResult[0].Add(ArrF64X[0]);
                        this.m_OCalibResult[1].Add(ArrF64Y[0]);
                        this.m_OCalibResult[2].Add(ArrF64X[1]);
                        this.m_OCalibResult[3].Add(ArrF64Y[1]);
                        this.m_OCalibResult[4].Add(ArrF64X[2]);
                        this.m_OCalibResult[5].Add(ArrF64Y[2]);
                        this.m_OCalibResult[6].Add(ArrF64X[3]);
                        this.m_OCalibResult[7].Add(ArrF64Y[3]);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public bool RunCalibration()
        {
            bool BResult = false;

            try
            {
                CAlignmentSet.It.BeginSync();

                this.m_OTool.SmalCalibrate(this.m_I32Index, this.m_OCalibResult);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public void SetViewOffset(int I32View, double F64XOffset, double F64YOffset)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                List<double> LstF64Offset = new List<double>();
                LstF64Offset.Add(F64XOffset);
                LstF64Offset.Add(F64YOffset);

                this.m_OTool.SmalSetCameraOffset(this.m_I32Index, I32View, LstF64Offset);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public void SetTarget(int I32View, double F64X, double F64Y)
        {
            try
            {
                CAlignmentSet.It.BeginSync();

                List<List<double>> OPosition = new List<List<double>>();
                OPosition.Add(new List<double>());
                OPosition.Add(new List<double>());
                OPosition[0].Add(F64X);
                OPosition[1].Add(F64Y);

                this.m_OTool.SmalSetGoalPos(this.m_I32Index, I32View, OPosition);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public List<List<double>> GetTarget()
        {
            List<List<double>> OResult = null;

            try
            {
                CAlignmentSet.It.BeginSync();

                OResult = this.m_OTool.SmalGetGoalPos(this.m_I32Index, this.m_I32ViewCount);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return OResult;
        }


        public void SetWeight(EHORIZONTAL EHorizontal, EVERTICAL EVertical)
        {
            try
            {
                CAlignmentSet.It.BeginSync();
                                
                List<double> LstF64Value1 = null;
                List<double> LstF64Value2 = null;
                List<double> LstF64Value3 = null;
                
                switch (this.m_I32ViewCount)
                {
                    case 2:
                        switch (EHorizontal)
                        {
                            case EHORIZONTAL.LEFT:
                                LstF64Value1 = new List<double>() { 1.0, 0.0 };
                                LstF64Value2 = new List<double>() { 0.5, 0.5 };
                                LstF64Value3 = new List<double>() { 0.5, 0.5 };
                                break;
                            case EHORIZONTAL.CENTER:
                                LstF64Value1 = new List<double>() { 0.5, 0.5 };
                                LstF64Value2 = new List<double>() { 0.5, 0.5 };
                                LstF64Value3 = new List<double>() { 0.5, 0.5 };

                                break;
                            case EHORIZONTAL.RIGHT:
                                LstF64Value1 = new List<double>() { 0.0, 1.0 };
                                LstF64Value2 = new List<double>() { 0.5, 0.5 };
                                LstF64Value3 = new List<double>() { 0.5, 0.5 };
                                break;
                        }
                        break;
                    case 3:
                        switch (EHorizontal)
                        {
                            case EHORIZONTAL.LEFT:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 1.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 1.0, 0.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 1.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.0, 0.0, 1.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 0.0, 1.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                }
                                break;
                            case EHORIZONTAL.CENTER:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0 };
                                        LstF64Value2 = new List<double>() { 1.0, 0.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 0.0, 1.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                }
                                break;
                            case EHORIZONTAL.RIGHT:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 0.0, 1.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 1.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 0.0, 1.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.0, 1.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 0.0, 1.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0 };
                                        break;
                                }
                                break;
                        }
                        break;
                    case 4:
                        switch (EHorizontal)
                        {
                            case EHORIZONTAL.LEFT:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 1.0, 0.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 1.0, 0.0, 0.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 1.0, 0.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.0, 0.0, 1.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 0.0, 1.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                }
                                break;
                            case EHORIZONTAL.CENTER:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 1.0, 0.0, 0.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.5, 0.5, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                }
                                break;
                            case EHORIZONTAL.RIGHT:
                                switch (EVertical)
                                {
                                    case EVERTICAL.TOP:
                                        LstF64Value1 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.MIDDLE:
                                        LstF64Value1 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        LstF64Value2 = new List<double>() { 0.5, 0.0, 0.5, 0.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                    case EVERTICAL.BOTTOM:
                                        LstF64Value1 = new List<double>() { 0.0, 0.0, 0.0, 1.0 };
                                        LstF64Value2 = new List<double>() { 0.0, 0.0, 0.0, 1.0 };
                                        LstF64Value3 = new List<double>() { 0.0, 1.0, 0.0, 0.0 };
                                        break;
                                }
                                break;
                        }
                        break;
                }

                List<List<double>> OWeight = new List<List<double>>();
                OWeight.Add(LstF64Value1);
                OWeight.Add(LstF64Value2);
                OWeight.Add(LstF64Value3);

                this.m_OTool.SmalSetAlignWeight(this.m_I32Index, this.m_I32ViewCount, OWeight);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }
        }


        public bool RunAlignment(double[] ArrF64X, double[] ArrF64Y, out double F64X, out double F64Y, out double F64T)
        {
            bool BResult = false;
            F64X = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                CAlignmentSet.It.BeginSync();


                List<int> LstI32ViewPri = null;
                List<int> LstI32TargetPri = null;
                List<List<double>> OPosition = new List<List<double>>() { ArrF64X.ToList(), ArrF64Y.ToList() };
                List<double> LstF64Result = new List<double>();

                switch (this.m_I32ViewCount)
                {
                    case 2:
                        LstI32ViewPri = new List<int>() { 0, 1 };
                        LstI32TargetPri = new List<int>() { 0, 0 };
                        break;

                    case 3:
                        LstI32ViewPri = new List<int>() { 0, 1, 2 };
                        LstI32TargetPri = new List<int>() { 0, 0, 0 };
                        break;

                    case 4:
                        LstI32ViewPri = new List<int>() { 0, 1, 2, 3 };
                        LstI32TargetPri = new List<int>() { 0, 0, 0, 0 };
                        break;
                }


                BResult = this.m_OTool.SmalAlign(this.m_I32Index, LstI32ViewPri, LstI32TargetPri, OPosition, LstF64Result);
                F64X = LstF64Result[0];
                F64Y = LstF64Result[1];
                F64T = LstF64Result[2];
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public bool LoadFile(string StrDirectory)
        {
            bool BResult = false;

            try
            {
                CAlignmentSet.It.BeginSync();

                string StrFile = String.Format("{0}\\Smal{1}.slc", StrDirectory, this.m_I32Index);

                if (File.Exists(StrFile) == true)
                {
                    BResult = this.m_OTool.SmalReadConfigFile(this.m_I32Index, StrFile);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public bool LoadFile(string StrDirectory, int I32Index)
        {
            bool BResult = false;

            try
            {
                CAlignmentSet.It.BeginSync();

                string StrFile = String.Format("{0}\\Smal{1}.slc", StrDirectory, I32Index);

                if (File.Exists(StrFile) == true)
                {
                    BResult = this.m_OTool.SmalReadConfigFile(this.m_I32Index, StrFile);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public bool SaveFile(string StrDirectory)
        {
            bool BResult = false;

            try
            {
                CAlignmentSet.It.BeginSync();

                Directory.CreateDirectory(StrDirectory);
                
                string StrFile = String.Format("{0}\\Smal{1}.slc", StrDirectory, this.m_I32Index);
                this.m_OTool.SmalWriteConfigFile(this.m_I32Index, StrFile);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public bool SaveFile(string StrDirectory, int I32Index)
        {
            bool BResult = false;

            try
            {
                CAlignmentSet.It.BeginSync();

                Directory.CreateDirectory(StrDirectory);

                string StrFile = String.Format("{0}\\Smal{1}.slc", StrDirectory, I32Index);
                this.m_OTool.SmalWriteConfigFile(this.m_I32Index, StrFile);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return BResult;
        }


        public string GetVersion()
        {
            string StrResult = string.Empty;

            try
            {
                CAlignmentSet.It.BeginSync();

                this.m_OTool.SmalGetVersion(ref StrResult);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                CAlignmentSet.It.EndSync();
            }

            return StrResult;
        }
        #endregion
    }


    #region ENUM
    public enum EKindOfStage : byte
    {
        XYT = 0,
        UVW,
        TXY,
        TX,
        NONE = 0xFF
    }


    public enum EKindOfView : byte
    {
        INDEX1 = 0,
        INDEX2,
        INDEX3,
        INDEX4
    }


    public enum EHORIZONTAL : byte
    {
        LEFT = 0,
        CENTER,
        RIGHT
    }


    public enum EVERTICAL : byte
    {
        TOP = 0,
        MIDDLE = 1,
        BOTTOM = 2
    }
    #endregion
}
