using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CEnvironment
    {
        #region SINGLE TON
        private static CEnvironment Src_It = null;


        public static CEnvironment It
        {
            get
            {
                CEnvironment OResult = null;

                try
                {
                    if (CEnvironment.Src_It == null)
                    {
                        CEnvironment.Src_It = new CEnvironment();
                    }

                    OResult = CEnvironment.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion


        #region CONST
        public const double LEN_PER_PIXEL = 0.00375D;

        private const string HEAD_OF_VIEW1 = "HEAD_OF_VIEW1";
        private const string HEAD_OF_VIEW2 = "HEAD_OF_VIEW2";
        private const string HEAD_OF_VIEW3 = "HEAD_OF_VIEW3";
        private const string HEAD_OF_VIEW4 = "HEAD_OF_VIEW4";

        private const string ENABLED1 = "ENABLED1";
        private const string ENABLED2 = "ENABLED2";
        private const string ENABLED3 = "ENABLED3";
        private const string ENABLED4 = "ENABLED4";
        private const string ENABLEMARK = "ENABLEMARK";
        
        private const string MAX_TRY = "MAXTRY";
        private const string CAM_HEAD1_GAIN = "CAM_HEAD1_GAIN";
        private const string CAM_HEAD1_EXPOTIME = "CAM_HEAD1_EXPOTIME";
        private const string CAM_HEAD2_GAIN = "CAM_HEAD2_GAIN";
        private const string CAM_HEAD2_EXPOTIME = "CAM_HEAD2_EXPOTIME";
        private const string CAM_HEAD3_GAIN = "CAM_HEAD3_GAIN";
        private const string CAM_HEAD3_EXPOTIME = "CAM_HEAD3_EXPOTIME";
        private const string CAM_HEAD4_GAIN = "CAM_HEAD4_GAIN";
        private const string CAM_HEAD4_EXPOTIME = "CAM_HEAD4_EXPOTIME";

        private const string DIRECTION = "DIRECTION";
        private const string MARK_DISCREPANCY_ALLOWANCE = "MARK_DISCREPANCY_ALLOWANCE"; //자재 마크들이 얼마나 고르지 않은가를 나타내는 지수. 1이 Minimum
        #endregion


        #region STATIC VARIABLE
        public static int DIRECTION_INFO = 1;
        #endregion


        #region PROPERTIES
        public int Direction
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.DIRECTION)); }
        }

        //20170413
        public int I32Discrepancy
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.MARK_DISCREPANCY_ALLOWANCE)); }
        }


        public int I32HeadOfView1
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.HEAD_OF_VIEW1)); }
            set { this.SetData(CEnvironment.HEAD_OF_VIEW1, value); }
        }


        public int I32HeadOfView2
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.HEAD_OF_VIEW2)); }
            set { this.SetData(CEnvironment.HEAD_OF_VIEW2, value); }
        }


        public int I32HeadOfView3
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.HEAD_OF_VIEW3)); }
            set { this.SetData(CEnvironment.HEAD_OF_VIEW3, value); }
        }


        public int I32HeadOfView4
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.HEAD_OF_VIEW4)); }
            set { this.SetData(CEnvironment.HEAD_OF_VIEW4, value); }
        }


        //
        public string StrEnabled1
        {
            get { return Convert.ToString(this.GetData(CEnvironment.ENABLED1)); }
            set { this.SetData(CEnvironment.ENABLED1, value); }
        }


        public string StrEnabled2
        {
            get { return Convert.ToString(this.GetData(CEnvironment.ENABLED2)); }
            set { this.SetData(CEnvironment.ENABLED2, value); }
        }


        public string StrEnabled3
        {
            get { return Convert.ToString(this.GetData(CEnvironment.ENABLED3)); }
            set { this.SetData(CEnvironment.ENABLED3, value); }
        }


        public string StrEnabled4
        {
            get { return Convert.ToString(this.GetData(CEnvironment.ENABLED4)); }
            set { this.SetData(CEnvironment.ENABLED4, value); }
        }

        //public string StrEnableMark
        //{
        //    get { return Convert.ToString(this.GetData(CEnvironment.ENABLEMARK)); }
        //    set { this.SetData(CEnvironment.ENABLEMARK, value); }
        //}

        public int I32MaxTry
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.MAX_TRY)); }
            set { this.SetData(CEnvironment.MAX_TRY, value); }
        }
        //

        public int I32CamHead1Gain
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD1_GAIN)); }
            set { this.SetData(CEnvironment.CAM_HEAD1_GAIN, value); }
        }


        public int I32CamHead1ExpoTime
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD1_EXPOTIME)); }
            set { this.SetData(CEnvironment.CAM_HEAD1_EXPOTIME, value); }
        }


        public int I32CamHead2Gain
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD2_GAIN)); }
            set { this.SetData(CEnvironment.CAM_HEAD2_GAIN, value); }
        }


        public int I32CamHead2ExpoTime
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD2_EXPOTIME)); }
            set { this.SetData(CEnvironment.CAM_HEAD2_EXPOTIME, value); }
        }


        public int I32CamHead3Gain
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD3_GAIN)); }
            set { this.SetData(CEnvironment.CAM_HEAD3_GAIN, value); }
        }


        public int I32CamHead3ExpoTime
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD3_EXPOTIME)); }
            set { this.SetData(CEnvironment.CAM_HEAD3_EXPOTIME, value); }
        }


        public int I32CamHead4Gain
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD4_GAIN)); }
            set { this.SetData(CEnvironment.CAM_HEAD4_GAIN, value); }
        }


        public int I32CamHead4ExpoTime
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD4_EXPOTIME)); }
            set { this.SetData(CEnvironment.CAM_HEAD4_EXPOTIME, value); }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CEnvironment() 
        {
            try
            {
                DIRECTION_INFO = Convert.ToInt32(this.GetData(CEnvironment.DIRECTION));
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        private string GetData(string StrName)
        {
            string StrResult = string.Empty;

            try
            {
                int I32RowIndex = CDB.It[CDB.TABLE_ENVIRONMENT].SelectRowIndex(CDB.ENVIRONMENT_NAME, StrName);
                object OValue = CDB.It[CDB.TABLE_ENVIRONMENT].Select(I32RowIndex, CDB.ENVIRONMENT_VALUE);

                if (OValue != null) StrResult = (string)OValue;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }


        private void SetData(string StrName, object OValue)
        {
            try
            {
                int I32RowIndex = CDB.It[CDB.TABLE_ENVIRONMENT].SelectRowIndex(CDB.ENVIRONMENT_NAME, StrName);
                CDB.It[CDB.TABLE_ENVIRONMENT].Update(I32RowIndex, CDB.ENVIRONMENT_VALUE, OValue.ToString());
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Commit()
        {
            try
            {
                CDB.It[CDB.TABLE_ENVIRONMENT].Commit();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
