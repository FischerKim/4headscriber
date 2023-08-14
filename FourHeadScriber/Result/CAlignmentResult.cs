using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CAlignmentResult
    {
        #region VARIABLE
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EView = EVIEW.NONE;

        private ERESULT m_EResult = ERESULT.NONE;
        private EVIEW m_ENGView = EVIEW.NONE;

        private CMarkResult m_OHead1 = null;
        private CMarkResult m_OHead2 = null;
        private CMarkResult m_OHead3 = null;
        private CMarkResult m_OHead4 = null;

        private double m_F64Head1X = 0;
        private double m_F64Head2X = 0;
        private double m_F64Head3X = 0;
        private double m_F64Head4X = 0;
        private double m_F64Y = 0;
        private double m_F64T = 0;
        #endregion


        #region PROPERTIES
        public ESTAGE_ANGLE EAngle
        {
            get { return this.m_EAngle; }
        }


        public EVIEW EView
        {
            get { return this.m_EView; }
        }


        public ERESULT EResult
        {
            get { return this.m_EResult; }
            set { this.m_EResult = value; }
        }


        public EVIEW ENGView
        {
            get { return this.m_ENGView; }
            set { this.m_ENGView = value; }
        }


        public CMarkResult OHead1
        {
            get { return this.m_OHead1; }
            set { this.m_OHead1 = value; }
        }


        public CMarkResult OHead2
        {
            get { return this.m_OHead2; }
            set { this.m_OHead2 = value; }
        }


        public CMarkResult OHead3
        {
            get { return this.m_OHead3; }
            set { this.m_OHead3 = value; }
        }


        public CMarkResult OHead4
        {
            get { return this.m_OHead4; }
            set { this.m_OHead4 = value; }
        }


        public double F64Head1X
        {
            get { return this.m_F64Head1X; }
            set { this.m_F64Head1X = value; }
        }


        public double F64Head2X
        {
            get { return this.m_F64Head2X; }
            set { this.m_F64Head2X = value; }
        }


        public double F64Head3X
        {
            get { return this.m_F64Head3X; }
            set { this.m_F64Head3X = value; }
        }


        public double F64Head4X
        {
            get { return this.m_F64Head4X; }
            set { this.m_F64Head4X = value; }
        }


        public double F64Y
        {
            get { return this.m_F64Y; }
            set { this.m_F64Y = value; }
        }


        public double F64T
        {
            get { return this.m_F64T; }
            set { this.m_F64T = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAlignmentResult(ESTAGE_ANGLE EAngle, EVIEW EView)
        {
            try
            {
                this.m_EAngle = EAngle;
                this.m_EView = EView;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
