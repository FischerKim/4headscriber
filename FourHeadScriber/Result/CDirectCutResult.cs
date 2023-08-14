using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CDirectCutResult
    {
        #region VARIABLE
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EView = EVIEW.NONE;

        private bool m_BInspected = false;
        private ERESULT m_EResult = ERESULT.NONE;
        
        private CCutResult m_OHead1 = null;
        private CCutResult m_OHead2 = null;
        private CCutResult m_OHead3 = null;
        private CCutResult m_OHead4 = null;

        private bool m_BHead1OK = false;
        private bool m_BHead2OK = false;
        private bool m_BHead3OK = false;
        private bool m_BHead4OK = false;
        private double m_F64Head1Standard = 0;
        private double m_F64Head2Standard = 0;
        private double m_F64Head3Standard = 0;
        private double m_F64Head4Standard = 0;
        private double m_F64Head1Length = 0;
        private double m_F64Head2Length = 0;
        private double m_F64Head3Length = 0;
        private double m_F64Head4Length = 0;
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


        public bool BInspected
        {
            get { return this.m_BInspected; }
            set { this.m_BInspected = value; }
        }


        public ERESULT EResult
        {
            get { return this.m_EResult; }
            set { this.m_EResult = value; }
        }


        public CCutResult OHead1
        {
            get { return this.m_OHead1; }
            set { this.m_OHead1 = value; }
        }


        public CCutResult OHead2
        {
            get { return this.m_OHead2; }
            set { this.m_OHead2 = value; }
        }


        public CCutResult OHead3
        {
            get { return this.m_OHead3; }
            set { this.m_OHead3 = value; }
        }


        public CCutResult OHead4
        {
            get { return this.m_OHead4; }
            set { this.m_OHead4 = value; }
        }


        public bool BHead1OK
        {
            get { return this.m_BHead1OK; }
            set { this.m_BHead1OK = value; }
        }


        public bool BHead2OK
        {
            get { return this.m_BHead2OK; }
            set { this.m_BHead2OK = value; }
        }


        public bool BHead3OK
        {
            get { return this.m_BHead3OK; }
            set { this.m_BHead3OK = value; }
        }


        public bool BHead4OK
        {
            get { return this.m_BHead4OK; }
            set { this.m_BHead4OK = value; }
        }


        public double F64Head1Standard
        {
            get { return this.m_F64Head1Standard; }
            set { this.m_F64Head1Standard = value; }
        }


        public double F64Head2Standard
        {
            get { return this.m_F64Head2Standard; }
            set { this.m_F64Head2Standard = value; }
        }


        public double F64Head3Standard
        {
            get { return this.m_F64Head3Standard; }
            set { this.m_F64Head3Standard = value; }
        }


        public double F64Head4Standard
        {
            get { return this.m_F64Head4Standard; }
            set { this.m_F64Head4Standard = value; }
        }


        public double F64Head1Length
        {
            get { return this.m_F64Head1Length; }
            set { this.m_F64Head1Length = value; }
        }


        public double F64Head2Length
        {
            get { return this.m_F64Head2Length; }
            set { this.m_F64Head2Length = value; }
        }


        public double F64Head3Length
        {
            get { return this.m_F64Head3Length; }
            set { this.m_F64Head3Length = value; }
        }


        public double F64Head4Length
        {
            get { return this.m_F64Head4Length; }
            set { this.m_F64Head4Length = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CDirectCutResult(ESTAGE_ANGLE EAngle, EVIEW EView)
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
