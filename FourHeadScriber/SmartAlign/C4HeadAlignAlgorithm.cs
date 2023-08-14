using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using System.Runtime.InteropServices;
using System.IO;

namespace FourHeadScriber
{
    public class C4HeadAlignAlgorithm
    {
        #region CONST
        private const string EQUIP_INI = "{0}\\EquipInfo.ini";
        #endregion


        #region VARIABLE
        private EVIEW m_EViewEnabled = EVIEW.NONE;
        private CAlignmentAlgorithm m_OHead12 = null;
        private CAlignmentAlgorithm m_OHead13 = null;
        private CAlignmentAlgorithm m_OHead14 = null;
        private CAlignmentAlgorithm m_OHead23 = null;
        private CAlignmentAlgorithm m_OHead24 = null;
        private CAlignmentAlgorithm m_OHead34 = null;
        
        private double m_F64StandardHead1X = 0;
        private double m_F64StandardHead2X = 0;
        private double m_F64StandardHead3X = 0;
        private double m_F64StandardHead4X = 0;
        private double m_F64StandardY = 0;
        private double m_F64StandardT = 0;
        #endregion


        #region PROPERTIES
        public EVIEW EViewEnabled
        {
            get { return this.m_EViewEnabled; }
            set { this.m_EViewEnabled = value; }
        }


        public CAlignmentAlgorithm OHead12
        {
            get { return this.m_OHead12; }
            set { this.m_OHead12 = value; }
        }


        public CAlignmentAlgorithm OHead13
        {
            get { return this.m_OHead13; }
            set { this.m_OHead13 = value; }
        }


        public CAlignmentAlgorithm OHead14
        {
            get { return this.m_OHead14; }
            set { this.m_OHead14 = value; }
        }


        public CAlignmentAlgorithm OHead23
        {
            get { return this.m_OHead23; }
            set { this.m_OHead23 = value; }
        }


        public CAlignmentAlgorithm OHead24
        {
            get { return this.m_OHead24; }
            set { this.m_OHead24 = value; }
        }


        public CAlignmentAlgorithm OHead34
        {
            get { return this.m_OHead34; }
            set { this.m_OHead34 = value; }
        }


        public double F64StandardHead1X
        {
            get { return this.m_F64StandardHead1X; }
            set { this.m_F64StandardHead1X = value; }
        }


        public double F64StandardHead2X
        {
            get { return this.m_F64StandardHead2X; }
            set { this.m_F64StandardHead2X = value; }
        }


        public double F64StandardHead3X
        {
            get { return this.m_F64StandardHead3X; }
            set { this.m_F64StandardHead3X = value; }
        }


        public double F64StandardHead4X
        {
            get { return this.m_F64StandardHead4X; }
            set { this.m_F64StandardHead4X = value; }
        }


        public double F64StandardY
        {
            get { return this.m_F64StandardY; }
            set { this.m_F64StandardY = value; }
        }


        public double F64StandardT
        {
            get { return this.m_F64StandardT; }
            set { this.m_F64StandardT = value; }
        }
        #endregion


        #region FUNCTION
        public void SetStageKind(EKindOfStage EKind)
        {
            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetStageKind(EKind);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetStageKind(EKind);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetStageKind(EKind);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetStageKind(EKind);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetStageKind(EKind);
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetStageKind(EKind);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SetMaxViewCount(int I32Count)
        {
            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetMaxViewCount(I32Count);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetMaxViewCount(I32Count);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetMaxViewCount(I32Count);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetMaxViewCount(I32Count);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetMaxViewCount(I32Count);
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetMaxViewCount(I32Count);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SetUseViewCount(int I32Count)
        {
            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetUseViewCount(I32Count);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetUseViewCount(I32Count);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetUseViewCount(I32Count);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetUseViewCount(I32Count);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetUseViewCount(I32Count);
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetUseViewCount(I32Count);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public int LoadCalibration(double F64SectionXY, int I32PointXY, double F64SectionT, int I32PointT)
        {
            int I32Result = 0;

            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") I32Result = this.m_OHead12.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") I32Result = this.m_OHead13.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") I32Result = this.m_OHead14.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                 else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") I32Result = this.m_OHead23.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") I32Result = this.m_OHead24.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
               else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") I32Result = this.m_OHead34.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
               
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
                 if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.LoadCalibration(F64SectionXY, I32PointXY, F64SectionT, I32PointT);
               }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        public void GetCalibrationMovement(int I32Index, out double F64X, out double F64Y, out double F64T)
        {
            F64X = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
                else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
                else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
                else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
                else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
                else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.GetCalibrationMovement(I32Index, out F64X, out F64Y, out F64T);
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
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetCalibrationResult(new double[] { ArrF64X[0], ArrF64X[1] }, new double[] { ArrF64Y[0], ArrF64Y[1] });
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetCalibrationResult(new double[] { ArrF64X[0], ArrF64X[2] }, new double[] { ArrF64Y[0], ArrF64Y[2] });
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetCalibrationResult(new double[] { ArrF64X[0], ArrF64X[3] }, new double[] { ArrF64Y[0], ArrF64Y[3] });
                 if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetCalibrationResult(new double[] { ArrF64X[1], ArrF64X[2] }, new double[] { ArrF64Y[1], ArrF64Y[2] });
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetCalibrationResult(new double[] { ArrF64X[1], ArrF64X[3] }, new double[] { ArrF64Y[1], ArrF64Y[3] });
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetCalibrationResult(new double[] { ArrF64X[2], ArrF64X[3] }, new double[] { ArrF64Y[2], ArrF64Y[3] });
                }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RunCalibration()
        {
            try
            {
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.RunCalibration();
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.RunCalibration();
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.RunCalibration();
               if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.RunCalibration();
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.RunCalibration();
                 if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.RunCalibration();
                    }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SetViewOffset(double F64Head1X, double F64Head2X, double F64Head3X, double F64Head4X, double F64Y)
        {
            try
            {
                F64Head1X = F64Head1X - this.m_F64StandardHead1X; //→+
                //F64Head1X = (F64Head1X - this.m_F64StandardHead1X) * -1; //←+
                F64Head2X = F64Head2X - this.m_F64StandardHead2X;
                F64Head3X = F64Head3X - this.m_F64StandardHead3X;
                F64Head4X = F64Head4X - this.m_F64StandardHead4X;
                F64Y = (F64Y - this.m_F64StandardY) * -1; //↑+
                //F64Y = F64Y - this.m_F64StandardY; //↓+

                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetViewOffset(0, F64Head1X, F64Y);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetViewOffset(1, F64Head2X, F64Y);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetViewOffset(0, F64Head1X, F64Y);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetViewOffset(1, F64Head3X, F64Y);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetViewOffset(0, F64Head1X, F64Y);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetViewOffset(1, F64Head4X, F64Y);
               
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetViewOffset(0, F64Head2X, F64Y);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetViewOffset(1, F64Head3X, F64Y);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetViewOffset(0, F64Head2X, F64Y);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetViewOffset(1, F64Head4X, F64Y);
              
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetViewOffset(0, F64Head3X, F64Y);
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetViewOffset(1, F64Head4X, F64Y);
              
                  }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SetTarget(EVIEW EView, double F64X, double F64Y)
        {
            try
            {
                if (EView == EVIEW.HEAD1)
                {
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetTarget(0, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetTarget(0, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetTarget(0, F64X, F64Y);
                }
                if (EView == EVIEW.HEAD2)
                {
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SetTarget(1, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetTarget(0, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetTarget(0, F64X, F64Y);
                 }
                if (EView == EVIEW.HEAD3)
                {
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SetTarget(1, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SetTarget(1, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetTarget(0, F64X, F64Y);
                 }
                if (EView == EVIEW.HEAD4)
                {
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SetTarget(1, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SetTarget(1, F64X, F64Y);
                    if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SetTarget(1, F64X, F64Y);
                  }
                
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RunAlignment
        (
            EVIEW EView,
            double[] ArrF64X,
            double[] ArrF64Y,
            out double F64Head1X,
            out double F64Head2X,
            out double F64Head3X,
            out double F64Head4X,
            out double F64Y,
            out double F64T
        )
        {
            F64Head1X = 0;
            F64Head2X = 0;
            F64Head3X = 0;
            F64Head4X = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                if ((EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2) == true) ||
                    (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3) == true) ||
                    (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD4) == true) ||
                    (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3) == true) ||
                    (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD4) == true) ||
                    (EView.Equals(EVIEW.HEAD3 | EVIEW.HEAD4) == true))
                {
                    if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2) == true && (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true")) this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                    else if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3) == true && (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true")) this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                    else if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD4) == true && (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true")) this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                    else if (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3) == true && (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true")) this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                    else if (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD4) == true && (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true")) this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                    else if (EView.Equals(EVIEW.HEAD3 | EVIEW.HEAD4) == true && (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")) this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                }
                else if ((EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3) == true) ||
                         (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD4) == true) ||
                         (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3 | EVIEW.HEAD4) == true) ||
                         (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true))
                {
                    if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3) == true)  //1-2, 1-3
                    {
                        if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true")
                        {
                            this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        }
                        //2
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                 
                    }
                    else if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD4) == true) //2-4, 1-4
                    {
                        if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        }
                        //2
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                 
                    }
                    else if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD3 | EVIEW.HEAD4) == true) //1-3, 1-4
                    {
                        if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        }
                        //2
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                 
                    }
                    else if (EView.Equals(EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true) //2-3, 2-4
                    {
                        if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        }
                        //2
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                
                    }
                    
                }
                else if ((EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true))
                {
                    if (EView.Equals(EVIEW.HEAD1 | EVIEW.HEAD2 | EVIEW.HEAD3 | EVIEW.HEAD4) == true)// 2-3, 1-4
                    {
                        //4
                        if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        }
                        //3
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true")
                        {
                            this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        }
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        }
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        }
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true")
                        {
                            this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                            this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        }
                        //2
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.RunAlignment(this.m_OHead12, ArrF64X, ArrF64Y, 0, 1, out F64Head1X, out F64Head2X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead13, ArrF64X, ArrF64Y, 0, 2, out F64Head1X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead14, ArrF64X, ArrF64Y, 0, 3, out F64Head1X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.RunAlignment(this.m_OHead23, ArrF64X, ArrF64Y, 1, 2, out F64Head2X, out F64Head3X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead24, ArrF64X, ArrF64Y, 1, 3, out F64Head2X, out F64Head4X, out F64Y, out F64T);
                        else if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.RunAlignment(this.m_OHead34, ArrF64X, ArrF64Y, 2, 3, out F64Head3X, out F64Head4X, out F64Y, out F64T);
                    }  
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void RunAlignment(CAlignmentAlgorithm OAlgorithm, double[] ArrF64X, double[] ArrF64Y, int I32Left, int I32Right, out double F64LX, out double F64RX, out double F64Y, out double F64T)
        {
            F64LX = 0;
            F64RX = 0;
            F64Y = 0;
            F64T = 0;

            try
            {
                OAlgorithm.SetWeight(EHORIZONTAL.LEFT, EVERTICAL.TOP);
                OAlgorithm.RunAlignment
                (
                    new double[] { ArrF64X[I32Left], ArrF64X[I32Right] },
                    new double[] { ArrF64Y[I32Left], ArrF64Y[I32Right] },
                    out F64LX, out F64Y, out F64T
                );

                OAlgorithm.SetWeight(EHORIZONTAL.RIGHT, EVERTICAL.TOP);
                OAlgorithm.RunAlignment
                (
                    new double[] { ArrF64X[I32Left], ArrF64X[I32Right] },
                    new double[] { ArrF64Y[I32Left], ArrF64Y[I32Right] },
                    out F64RX, out F64Y, out F64T
                );
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void LoadFile(string StrDirectory)
        {
            try
            {
                string StrEquipFile = String.Format(EQUIP_INI, StrDirectory);

                if (File.Exists(StrEquipFile) == true)
                {
                    StringBuilder OHead1X = new StringBuilder();
                    StringBuilder OHead2X = new StringBuilder();
                    StringBuilder OHead3X = new StringBuilder();
                    StringBuilder OHead4X = new StringBuilder();
                    StringBuilder OY = new StringBuilder();
                    StringBuilder OT = new StringBuilder();
                    GetPrivateProfileString("INFO", "HEAD1_X", String.Empty, OHead1X, 255, StrEquipFile);
                    GetPrivateProfileString("INFO", "HEAD2_X", String.Empty, OHead2X, 255, StrEquipFile);
                    GetPrivateProfileString("INFO", "HEAD3_X", String.Empty, OHead3X, 255, StrEquipFile);
                    GetPrivateProfileString("INFO", "HEAD4_X", String.Empty, OHead4X, 255, StrEquipFile);
                    GetPrivateProfileString("INFO", "Y", String.Empty, OY, 255, StrEquipFile);
                    GetPrivateProfileString("INFO", "T", String.Empty, OT, 255, StrEquipFile);

                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.LoadFile(StrDirectory, 1);
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.LoadFile(StrDirectory, 2);
                    if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.LoadFile(StrDirectory, 3);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.LoadFile(StrDirectory, 4);
                    if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.LoadFile(StrDirectory, 5);
                    if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.LoadFile(StrDirectory, 6);


                    this.m_F64StandardHead1X = Convert.ToDouble(OHead1X.ToString());
                    this.m_F64StandardHead2X = Convert.ToDouble(OHead2X.ToString());
                    this.m_F64StandardHead3X = Convert.ToDouble(OHead3X.ToString());
                    this.m_F64StandardHead4X = Convert.ToDouble(OHead4X.ToString());
                    this.m_F64StandardY = Convert.ToDouble(OY.ToString());
                    this.m_F64StandardT = Convert.ToDouble(OT.ToString());
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void SaveFile(string StrDirectory)
        {
            try
            {
                string StrFile = String.Format(EQUIP_INI, StrDirectory);

                if (File.Exists(StrFile) == false)
                {
                    File.WriteAllText(StrFile, FourHeadScriber.Properties.Resources.EquipInfo);
                }

                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled2 == "true") this.m_OHead12.SaveFile(StrDirectory, 1);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead13.SaveFile(StrDirectory, 2);
                if (CEnvironment.It.StrEnabled1 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead14.SaveFile(StrDirectory, 3);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled3 == "true") this.m_OHead23.SaveFile(StrDirectory, 4);
                if (CEnvironment.It.StrEnabled2 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead24.SaveFile(StrDirectory, 5);
                if (CEnvironment.It.StrEnabled3 == "true" && CEnvironment.It.StrEnabled4 == "true") this.m_OHead34.SaveFile(StrDirectory, 6);

                WritePrivateProfileString("INFO", "HEAD1_X", this.m_F64StandardHead1X.ToString(), StrFile);
                WritePrivateProfileString("INFO", "HEAD2_X", this.m_F64StandardHead2X.ToString(), StrFile);
                WritePrivateProfileString("INFO", "HEAD3_X", this.m_F64StandardHead3X.ToString(), StrFile);
                WritePrivateProfileString("INFO", "HEAD4_X", this.m_F64StandardHead4X.ToString(), StrFile);
                WritePrivateProfileString("INFO", "Y", this.m_F64StandardY.ToString(), StrFile);
                WritePrivateProfileString("INFO", "T", this.m_F64StandardT.ToString(), StrFile);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        #region EXTERNAL FUNCTION
        [DllImport("KERNEL32")]
        public static extern bool GetPrivateProfileString(string StrAppName, string StrKey, string StrDefault, StringBuilder StrValue, int I32Size, string StrFile);


        [DllImport("KERNEL32")]
        public static extern bool WritePrivateProfileString(string StrAppName, string StrKey, string StrValue, string StrFile);
        #endregion
        #endregion
    }
}
