using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using Daekhon.Acquisition.Basler;

namespace FourHeadScriber
{
    public class CAcquisitionManager : IDisposable
    {
        #region SIGNLETON
        private static CAcquisitionManager Src_It = null;


        public static CAcquisitionManager It
        {
            get
            {
                CAcquisitionManager OResult = null;

                try
                {
                    if (CAcquisitionManager.Src_It == null)
                    {
                        CAcquisitionManager.Src_It = new CAcquisitionManager();
                    }

                    OResult = CAcquisitionManager.Src_It;
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
        private CBasler m_OHead1 = null;
        private CBasler m_OHead2 = null;
        private CBasler m_OHead3 = null;
        private CBasler m_OHead4 = null;
        #endregion


        #region PROPERTIES
        public CBasler OHead1
        {
            get { return this.m_OHead1; }
            set { this.m_OHead1 = value; }
        }


        public CBasler OHead2
        {
            get { return this.m_OHead2; }
            set { this.m_OHead2 = value; }
        }


        public CBasler OHead3
        {
            get { return this.m_OHead3; }
            set { this.m_OHead3 = value; }
        }


        public CBasler OHead4
        {
            get { return this.m_OHead4; }
            set { this.m_OHead4 = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CAcquisitionManager() { }


        ~CAcquisitionManager()
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
        public void Setup()
        {
            try
            {
                int I32Head1Gain = CEnvironment.It.I32CamHead1Gain;
                int I32Head2Gain = CEnvironment.It.I32CamHead2Gain;
                int I32Head3Gain = CEnvironment.It.I32CamHead3Gain;
                int I32Head4Gain = CEnvironment.It.I32CamHead4Gain;
                int I32Head1ExpoTime = CEnvironment.It.I32CamHead1ExpoTime;
                int I32Head2ExpoTime = CEnvironment.It.I32CamHead2ExpoTime;
                int I32Head3ExpoTime = CEnvironment.It.I32CamHead3ExpoTime;
                int I32Head4ExpoTime = CEnvironment.It.I32CamHead4ExpoTime;
                if ((I32Head1Gain >= this.m_OHead1.I32GainMin) && (I32Head1Gain <= this.m_OHead1.I32GainMax)) this.m_OHead1.I32Gain = I32Head1Gain;
                if ((I32Head2Gain >= this.m_OHead2.I32GainMin) && (I32Head2Gain <= this.m_OHead2.I32GainMax)) this.m_OHead2.I32Gain = I32Head2Gain;
                if ((I32Head3Gain >= this.m_OHead3.I32GainMin) && (I32Head3Gain <= this.m_OHead3.I32GainMax)) this.m_OHead3.I32Gain = I32Head3Gain;
                if ((I32Head4Gain >= this.m_OHead4.I32GainMin) && (I32Head4Gain <= this.m_OHead4.I32GainMax)) this.m_OHead4.I32Gain = I32Head4Gain;
                if ((I32Head1ExpoTime >= this.m_OHead1.I32ExposureTimeMin) && (I32Head1ExpoTime <= this.m_OHead1.I32ExposureTimeMax)) this.m_OHead1.I32ExposureTime = I32Head1ExpoTime;
                if ((I32Head2ExpoTime >= this.m_OHead2.I32ExposureTimeMin) && (I32Head2ExpoTime <= this.m_OHead2.I32ExposureTimeMax)) this.m_OHead2.I32ExposureTime = I32Head2ExpoTime;
                if ((I32Head3ExpoTime >= this.m_OHead3.I32ExposureTimeMin) && (I32Head3ExpoTime <= this.m_OHead3.I32ExposureTimeMax)) this.m_OHead3.I32ExposureTime = I32Head3ExpoTime;
                if ((I32Head4ExpoTime >= this.m_OHead4.I32ExposureTimeMin) && (I32Head4ExpoTime <= this.m_OHead4.I32ExposureTimeMax)) this.m_OHead4.I32ExposureTime = I32Head4ExpoTime;
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
