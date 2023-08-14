using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Daekhon.Common;
using Jhjo.Common;
using System.Drawing;
using Cognex.VisionPro;

namespace FourHeadScriber
{
    public class CImageInfo
    {
        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;
        private CogImage8Grey m_OImage = null;
        #endregion


        #region PROPERTIES
        public DateTime OTime
        {
            get { return this.m_OTime; }
        }


        public CogImage8Grey OImage
        {
            get { return this.m_OImage; }
            set { this.m_OImage = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CImageInfo(CogImage8Grey OImage)
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_OImage = OImage;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CImageInfo(Bitmap OImage)
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_OImage = new CogImage8Grey(OImage);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
