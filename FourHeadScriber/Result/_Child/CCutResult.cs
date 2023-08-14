using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CCutResult
    {
        #region VARIABLE
        private EVIEW m_EView = EVIEW.NONE;
        private CImageInfo m_OImageInfo = null;

        private bool m_BInspected = false;
        private bool m_BOK = false;

        private double m_F64StartX = 0;
        private double m_F64MidX = 0;
        private double m_F64EndX = 0;
        private double m_F64Length = 0;
        #endregion


        #region PROPERTIES
        public EVIEW EView
        {
            get { return this.m_EView; }
        }


        public CImageInfo OImageInfo
        {
            get { return this.m_OImageInfo; }
        }


        public bool BInspected
        {
            get { return this.m_BInspected; }
            set { this.m_BInspected = value; }
        }


        public bool BOK
        {
            get { return this.m_BOK; }
            set { this.m_BOK = value; }
        }


        public double F64StartX
        {
            get { return this.m_F64StartX; }
            set { this.m_F64StartX = value; }
        }


        public double F64MidX
        {
            get { return this.m_F64MidX; }
            set { this.m_F64MidX = value; }
        }


        public double F64EndX
        {
            get { return this.m_F64EndX; }
            set { this.m_F64EndX = value; }
        }


        public double F64Length
        {
            get { return this.m_F64Length; }
            set { this.m_F64Length = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CCutResult(EVIEW EView, CImageInfo OImageInfo)
        {
            try
            {
                this.m_EView = EView;
                this.m_OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
