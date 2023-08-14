using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CCalibrationResult
    {
        #region VARIABLE
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private CMarkResult m_OHead1 = null;
        private CMarkResult m_OHead2 = null;
        private CMarkResult m_OHead3 = null;
        private CMarkResult m_OHead4 = null;
        #endregion


        #region PROPERTIES
        public ESTAGE_ANGLE EAngle
        {
            get { return this.m_EAngle; }
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
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CCalibrationResult(ESTAGE_ANGLE EAngle)
        {
            try
            {
                this.m_EAngle = EAngle;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
