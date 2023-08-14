using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CMotionController : IDisposable
    {
        #region SINGLE TON
        private static CMotionController Src_It = null;

        public static CMotionController It
        {
            get
            {
                CMotionController OResult = null;

                try
                {
                    if (CMotionController.Src_It == null)
                    {
                        CMotionController.Src_It = new CMotionController();
                    }

                    OResult = CMotionController.Src_It;
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
        private CPLC m_OPLC = null;
        #endregion


        #region PROPERTIES
        public CPLC OPLC
        {
            get { return this.m_OPLC; }
            set { this.m_OPLC = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CMotionController() { }
        #endregion


        #region FUNCTION
        public void Open(CPLC.ConnectionCompletedHandler OConnectionCompleted)
        {
            try
            {
                this.m_OPLC = new CPLC(OConnectionCompleted);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Close()
        {
            try
            {
                if (this.m_OPLC != null)
                {
                    this.m_OPLC.AbortWork();
                    this.m_OPLC.Dispose();
                    this.m_OPLC = null;
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
                if (this.m_OPLC != null)
                {
                    this.m_OPLC.Dispose();
                    this.m_OPLC = null;
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
