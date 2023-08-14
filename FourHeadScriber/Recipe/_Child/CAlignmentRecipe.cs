using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CAlignmentRecipe : IDisposable
    {
        #region VARIABLE
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private string m_StrName = string.Empty;
        private string m_StrDirectory = string.Empty;

        private CAnalysisRecipe m_OHead1 = null;
        private CAnalysisRecipe m_OHead2 = null;
        private CAnalysisRecipe m_OHead3 = null;
        private CAnalysisRecipe m_OHead4 = null;
        
        private double m_F64AlignmentLimitHead1 = 0.010;
        private double m_F64AlignmentLimitHead2 = 0.010;
        private double m_F64AlignmentLimitHead3 = 0.010;
        private double m_F64AlignmentLimitHead4 = 0.010;
        private double m_F64AlignmentLimitY = 0.010;
        private double m_F64AlignmentLimitT = 0.0001;
        private double m_F64AlignmentProductLength = -1;

        private double m_F64DirectCutLimitHead1 = 0.025D;
        private double m_F64DirectCutLimitHead2 = 0.025D;
        private double m_F64DirectCutLimitHead3 = 0.025D;
        private double m_F64DirectCutLimitHead4 = 0.025D;

        private double m_F64CrossCutLimitHead1 = 0.025D;
        private double m_F64CrossCutLimitHead2 = 0.025D;
        private double m_F64CrossCutLimitHead3 = 0.025D;
        private double m_F64CrossCutLimitHead4 = 0.025D;
        #endregion


        #region PROPERTIES
        public ESTAGE_ANGLE EAngle
        {
            get { return this.m_EAngle; }
        }


        public string StrName
        {
            get { return this.m_StrName; }
        }


        public string StrDirectory
        {
            get { return this.m_StrDirectory; }
        }


        public CAnalysisRecipe OHead1
        {
            get { return this.m_OHead1; }
        }


        public CAnalysisRecipe OHead2
        {
            get { return this.m_OHead2; }
        }


        public CAnalysisRecipe OHead3
        {
            get { return this.m_OHead3; }
        }


        public CAnalysisRecipe OHead4
        {
            get { return this.m_OHead4; }
        }
      

        public double F64AlignmentLimitHead1
        {
            get { return this.m_F64AlignmentLimitHead1; }
            set { this.m_F64AlignmentLimitHead1 = value; }
        }


        public double F64AlignmentLimitHead2
        {
            get { return this.m_F64AlignmentLimitHead2; }
            set { this.m_F64AlignmentLimitHead2 = value; }
        }


        public double F64AlignmentLimitHead3
        {
            get { return this.m_F64AlignmentLimitHead3; }
            set { this.m_F64AlignmentLimitHead3 = value; }
        }


        public double F64AlignmentLimitHead4
        {
            get { return this.m_F64AlignmentLimitHead4; }
            set { this.m_F64AlignmentLimitHead4 = value; }
        }


        public double F64AlignmentLimitY
        {
            get { return this.m_F64AlignmentLimitY; }
            set { this.m_F64AlignmentLimitY = value; }
        }


        public double F64AlignmentLimitT
        {
            get { return this.m_F64AlignmentLimitT; }
            set { this.m_F64AlignmentLimitT = value; }
        }

        public double F64AlignmentProductLength
        {
            get { return this.m_F64AlignmentProductLength; }
            set { this.m_F64AlignmentProductLength = value; }
        }

        public double F64DirectCutLimitHead1
        {
            get { return this.m_F64DirectCutLimitHead1; }
            set { this.m_F64DirectCutLimitHead1 = value; }
        }


        public double F64DirectCutLimitHead2
        {
            get { return this.m_F64DirectCutLimitHead2; }
            set { this.m_F64DirectCutLimitHead2 = value; }
        }


        public double F64DirectCutLimitHead3
        {
            get { return this.m_F64DirectCutLimitHead3; }
            set { this.m_F64DirectCutLimitHead3 = value; }
        }


        public double F64DirectCutLimitHead4
        {
            get { return this.m_F64DirectCutLimitHead4; }
            set { this.m_F64DirectCutLimitHead4 = value; }
        }


        public double F64CrossCutLimitHead1
        {
            get { return this.m_F64CrossCutLimitHead1; }
            set { this.m_F64CrossCutLimitHead1 = value; }
        }


        public double F64CrossCutLimitHead2
        {
            get { return this.m_F64CrossCutLimitHead2; }
            set { this.m_F64CrossCutLimitHead2 = value; }
        }


        public double F64CrossCutLimitHead3
        {
            get { return this.m_F64CrossCutLimitHead3; }
            set { this.m_F64CrossCutLimitHead3 = value; }
        }


        public double F64CrossCutLimitHead4
        {
            get { return this.m_F64CrossCutLimitHead4; }
            set { this.m_F64CrossCutLimitHead4 = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAlignmentRecipe(ESTAGE_ANGLE EAngle, string StrName, string StrDirectory)
        {
            try
            {
                this.m_EAngle = EAngle;
                this.m_StrName = StrName;
                this.m_StrDirectory = StrDirectory + "\\" + EAngle.ToString();

                this.m_OHead1 = new CAnalysisRecipe(EVIEW.HEAD1, StrName, this.m_StrDirectory);
                this.m_OHead2 = new CAnalysisRecipe(EVIEW.HEAD2, StrName, this.m_StrDirectory);
                this.m_OHead3 = new CAnalysisRecipe(EVIEW.HEAD3, StrName, this.m_StrDirectory);
                this.m_OHead4 = new CAnalysisRecipe(EVIEW.HEAD4, StrName, this.m_StrDirectory);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CAlignmentRecipe(CAlignmentRecipe OSource)
        {
            try
            {
                this.m_EAngle = OSource.EAngle;
                this.m_StrName = OSource.StrName;
                this.m_StrDirectory = OSource.m_StrDirectory;

                this.m_OHead1 = new CAnalysisRecipe(OSource.m_OHead1);
                this.m_OHead2 = new CAnalysisRecipe(OSource.m_OHead2);
                this.m_OHead3 = new CAnalysisRecipe(OSource.m_OHead3);
                this.m_OHead4 = new CAnalysisRecipe(OSource.m_OHead4);

                this.m_F64AlignmentLimitHead1 = OSource.m_F64AlignmentLimitHead1;
                this.m_F64AlignmentLimitHead2 = OSource.m_F64AlignmentLimitHead2;
                this.m_F64AlignmentLimitHead3 = OSource.m_F64AlignmentLimitHead3;
                this.m_F64AlignmentLimitHead4 = OSource.m_F64AlignmentLimitHead4;
                this.m_F64AlignmentLimitY = OSource.m_F64AlignmentLimitY;
                this.m_F64AlignmentLimitT = OSource.m_F64AlignmentLimitT;
                this.m_F64AlignmentProductLength = OSource.m_F64AlignmentProductLength;

                this.m_F64DirectCutLimitHead1 = OSource.m_F64DirectCutLimitHead1;
                this.m_F64DirectCutLimitHead2 = OSource.m_F64DirectCutLimitHead2;
                this.m_F64DirectCutLimitHead3 = OSource.m_F64DirectCutLimitHead3;
                this.m_F64DirectCutLimitHead4 = OSource.m_F64DirectCutLimitHead4;

                this.m_F64CrossCutLimitHead1 = OSource.m_F64CrossCutLimitHead1;
                this.m_F64CrossCutLimitHead2 = OSource.m_F64CrossCutLimitHead2;
                this.m_F64CrossCutLimitHead3 = OSource.m_F64CrossCutLimitHead3;
                this.m_F64CrossCutLimitHead4 = OSource.m_F64CrossCutLimitHead4;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CAlignmentRecipe()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Create()
        {
            try
            {
                this.m_OHead1.Create();
                this.m_OHead2.Create();
                this.m_OHead3.Create();
                this.m_OHead4.Create();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Load()
        {
            try
            {
                this.m_OHead1.Load();
                this.m_OHead2.Load();
                this.m_OHead3.Load();
                this.m_OHead4.Load();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Save()
        {
            try
            {
                this.m_OHead1.Save();
                this.m_OHead2.Save();
                this.m_OHead3.Save();
                this.m_OHead4.Save();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Copy(CAlignmentRecipe OSource)
        {
            try
            {
                this.m_OHead1.Copy(OSource.m_OHead1);
                this.m_OHead2.Copy(OSource.m_OHead2);
                this.m_OHead3.Copy(OSource.m_OHead3);
                this.m_OHead4.Copy(OSource.m_OHead4);

                this.m_F64AlignmentLimitHead1 = OSource.m_F64AlignmentLimitHead1;
                this.m_F64AlignmentLimitHead2 = OSource.m_F64AlignmentLimitHead2;
                this.m_F64AlignmentLimitHead3 = OSource.m_F64AlignmentLimitHead3;
                this.m_F64AlignmentLimitHead4 = OSource.m_F64AlignmentLimitHead4;
                this.m_F64AlignmentLimitY = OSource.m_F64AlignmentLimitY;
                this.m_F64AlignmentLimitT = OSource.m_F64AlignmentLimitT;
                this.m_F64AlignmentProductLength = OSource.m_F64AlignmentProductLength;

                this.m_F64DirectCutLimitHead1 = OSource.m_F64DirectCutLimitHead1;
                this.m_F64DirectCutLimitHead2 = OSource.m_F64DirectCutLimitHead2;
                this.m_F64DirectCutLimitHead3 = OSource.m_F64DirectCutLimitHead3;
                this.m_F64DirectCutLimitHead4 = OSource.m_F64DirectCutLimitHead4;

                this.m_F64CrossCutLimitHead1 = OSource.m_F64CrossCutLimitHead1;
                this.m_F64CrossCutLimitHead2 = OSource.m_F64CrossCutLimitHead2;
                this.m_F64CrossCutLimitHead3 = OSource.m_F64CrossCutLimitHead3;
                this.m_F64CrossCutLimitHead4 = OSource.m_F64CrossCutLimitHead4;
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
    }
}
