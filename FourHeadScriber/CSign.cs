using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CSign
    {  
        #region SINGLE TON
        private static CSign Src_It = null;


        public static CSign It
        {
            get
            {
                CSign OResult = null;

                try
                {
                    if (CSign.Src_It == null)
                    {
                        CSign.Src_It = new CSign();
                    }

                    OResult = CSign.Src_It;
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
        private const string CALIBRATION_HEAD1X = "CALIBRATION_HEAD1X";
        private const string CALIBRATION_HEAD2X = "CALIBRATION_HEAD2X";
        private const string CALIBRATION_HEAD3X = "CALIBRATION_HEAD3X";
        private const string CALIBRATION_HEAD4X = "CALIBRATION_HEAD4X";
        private const string CALIBRATION_Y = "CALIBRATION_Y";
        private const string CALIBRATION_T = "CALIBRATION_T";

        private const string ALIGNSHARP_HEAD1X_AUTO = "ALIGNSHARP_HEAD1X_AUTO";
        private const string ALIGNSHARP_HEAD2X_AUTO = "ALIGNSHARP_HEAD2X_AUTO";
        private const string ALIGNSHARP_HEAD3X_AUTO = "ALIGNSHARP_HEAD3X_AUTO";
        private const string ALIGNSHARP_HEAD4X_AUTO = "ALIGNSHARP_HEAD4X_AUTO";
        private const string ALIGNSHARP_Y_AUTO = "ALIGNSHARP_Y_AUTO";
        private const string ALIGNSHARP_T_AUTO = "ALIGNSHARP_T_AUTO";

        private const string ALIGNX_HEAD1X_AUTO = "ALIGNX_HEAD1X_AUTO";
        private const string ALIGNX_HEAD2X_AUTO = "ALIGNX_HEAD2X_AUTO";
        private const string ALIGNX_HEAD3X_AUTO = "ALIGNX_HEAD3X_AUTO";
        private const string ALIGNX_HEAD4X_AUTO = "ALIGNX_HEAD4X_AUTO";
        private const string ALIGNX_Y_AUTO = "ALIGNX_Y_AUTO";
        private const string ALIGNX_T_AUTO = "ALIGNX_T_AUTO";

        private const string ALIGNSHARP_HEAD1X_MANUAL = "ALIGNSHARP_HEAD1X_MANUAL";
        private const string ALIGNSHARP_HEAD2X_MANUAL = "ALIGNSHARP_HEAD2X_MANUAL";
        private const string ALIGNSHARP_HEAD3X_MANUAL = "ALIGNSHARP_HEAD3X_MANUAL";
        private const string ALIGNSHARP_HEAD4X_MANUAL = "ALIGNSHARP_HEAD4X_MANUAL";
        private const string ALIGNSHARP_Y_MANUAL = "ALIGNSHARP_Y_MANUAL";
        private const string ALIGNSHARP_T_MANUAL = "ALIGNSHARP_T_MANUAL";

        private const string ALIGNX_HEAD1X_MANUAL = "ALIGNX_HEAD1X_MANUAL";
        private const string ALIGNX_HEAD2X_MANUAL = "ALIGNX_HEAD2X_MANUAL";
        private const string ALIGNX_HEAD3X_MANUAL = "ALIGNX_HEAD3X_MANUAL";
        private const string ALIGNX_HEAD4X_MANUAL = "ALIGNX_HEAD4X_MANUAL";
        private const string ALIGNX_Y_MANUAL = "ALIGNX_Y_MANUAL";
        private const string ALIGNX_T_MANUAL = "ALIGNX_T_MANUAL";
        //1219
        private const string ODDCHECK_X_S_AUTO = "ODDCHECK_X_S_AUTO";
        private const string ODDCHECK_Y_S_AUTO = "ODDCHECK_Y_S_AUTO";
        private const string ODDCHECK_T_S_AUTO = "ODDCHECK_T_S_AUTO";

        private const string ODDCHECK_X_X_AUTO = "ODDCHECK_X_X_AUTO";
        private const string ODDCHECK_Y_X_AUTO = "ODDCHECK_Y_X_AUTO";
        private const string ODDCHECK_T_X_AUTO = "ODDCHECK_T_X_AUTO";

        private const string ODDCHECK_X_S_MANUAL = "ODDCHECK_X_S_MANUAL";
        private const string ODDCHECK_Y_S_MANUAL = "ODDCHECK_Y_S_MANUAL";
        private const string ODDCHECK_T_S_MANUAL = "ODDCHECK_T_S_MANUAL";

        private const string ODDCHECK_X_X_MANUAL = "ODDCHECK_X_X_MANUAL";
        private const string ODDCHECK_Y_X_MANUAL = "ODDCHECK_Y_X_MANUAL";
        private const string ODDCHECK_T_X_MANUAL = "ODDCHECK_T_X_MANUAL";
        #endregion


        #region STATIC VARIABLE
        public static int CALIBRATION_HEAD1X_SIGN = 1;
        public static int CALIBRATION_HEAD2X_SIGN = 1;
        public static int CALIBRATION_HEAD3X_SIGN = 1;
        public static int CALIBRATION_HEAD4X_SIGN = 1;
        public static int CALIBRATION_Y_SIGN = 1;
        public static int CALIBRATION_T_SIGN = 1;

        public static int ALIGNSHARP_HEAD1X_AUTO_SIGN = 1;
        public static int ALIGNSHARP_HEAD2X_AUTO_SIGN = 1;
        public static int ALIGNSHARP_HEAD3X_AUTO_SIGN = 1;
        public static int ALIGNSHARP_HEAD4X_AUTO_SIGN = 1;
        public static int ALIGNSHARP_Y_AUTO_SIGN = 1;
        public static int ALIGNSHARP_T_AUTO_SIGN = 1;

        public static int ALIGNX_HEAD1X_AUTO_SIGN = 1;
        public static int ALIGNX_HEAD2X_AUTO_SIGN = 1;
        public static int ALIGNX_HEAD3X_AUTO_SIGN = 1;
        public static int ALIGNX_HEAD4X_AUTO_SIGN = 1;
        public static int ALIGNX_Y_SIGN_AUTO = 1;
        public static int ALIGNX_T_SIGN_AUTO = 1;

        public static int ALIGNSHARP_HEAD1X_MANUAL_SIGN = 1;
        public static int ALIGNSHARP_HEAD2X_MANUAL_SIGN = 1;
        public static int ALIGNSHARP_HEAD3X_MANUAL_SIGN = 1;
        public static int ALIGNSHARP_HEAD4X_MANUAL_SIGN = 1;
        public static int ALIGNSHARP_Y_MANUAL_SIGN = 1;
        public static int ALIGNSHARP_T_MANUAL_SIGN = 1;

        public static int ALIGNX_HEAD1X_MANUAL_SIGN = 1;
        public static int ALIGNX_HEAD2X_MANUAL_SIGN = 1;
        public static int ALIGNX_HEAD3X_MANUAL_SIGN = 1;
        public static int ALIGNX_HEAD4X_MANUAL_SIGN = 1;
        public static int ALIGNX_Y_SIGN_MANUAL = 1;
        public static int ALIGNX_T_SIGN_MANUAL = 1;
        //1219
        public static int ODDCHECK_X_S_AUTO_SIGN = -1;
        public static int ODDCHECK_Y_S_AUTO_SIGN = 1;
        public static int ODDCHECK_T_S_AUTO_SIGN = 1;

        public static int ODDCHECK_X_X_AUTO_SIGN = 1;
        public static int ODDCHECK_Y_X_AUTO_SIGN = -1;
        public static int ODDCHECK_T_X_AUTO_SIGN = -1;

        public static int ODDCHECK_X_S_MANUAL_SIGN = -1;
        public static int ODDCHECK_Y_S_MANUAL_SIGN = 1;
        public static int ODDCHECK_T_S_MANUAL_SIGN = 1;

        public static int ODDCHECK_X_X_MANUAL_SIGN = -1;
        public static int ODDCHECK_Y_X_MANUAL_SIGN = 1;
        public static int ODDCHECK_T_X_MANUAL_SIGN = -1;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        protected CSign() 
        {
            try
            {
                CALIBRATION_HEAD1X_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_HEAD1X));
                CALIBRATION_HEAD2X_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_HEAD2X));
                CALIBRATION_HEAD3X_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_HEAD3X));
                CALIBRATION_HEAD4X_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_HEAD4X));
                CALIBRATION_Y_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_Y));
                CALIBRATION_T_SIGN = Convert.ToInt32(this.GetData(CSign.CALIBRATION_T));

                ALIGNSHARP_HEAD1X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD1X_AUTO));
                ALIGNSHARP_HEAD2X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD2X_AUTO));
                ALIGNSHARP_HEAD3X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD3X_AUTO));
                ALIGNSHARP_HEAD4X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD4X_AUTO));
                ALIGNSHARP_Y_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_Y_AUTO));
                ALIGNSHARP_T_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_T_AUTO));

                ALIGNX_HEAD1X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD1X_AUTO));
                ALIGNX_HEAD2X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD2X_AUTO));
                ALIGNX_HEAD3X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD3X_AUTO));
                ALIGNX_HEAD4X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD4X_AUTO));
                ALIGNX_Y_SIGN_AUTO = Convert.ToInt32(this.GetData(CSign.ALIGNX_Y_AUTO));
                ALIGNX_T_SIGN_AUTO = Convert.ToInt32(this.GetData(CSign.ALIGNX_T_AUTO));

                ALIGNSHARP_HEAD1X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD1X_MANUAL));
                ALIGNSHARP_HEAD2X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD2X_MANUAL));
                ALIGNSHARP_HEAD3X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD3X_MANUAL));
                ALIGNSHARP_HEAD4X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_HEAD4X_MANUAL));
                ALIGNSHARP_Y_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_Y_MANUAL));
                ALIGNSHARP_T_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNSHARP_T_MANUAL));

                ALIGNX_HEAD1X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD1X_MANUAL));
                ALIGNX_HEAD2X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD2X_MANUAL));
                ALIGNX_HEAD3X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD3X_MANUAL));
                ALIGNX_HEAD4X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ALIGNX_HEAD4X_MANUAL));
                ALIGNX_Y_SIGN_MANUAL = Convert.ToInt32(this.GetData(CSign.ALIGNX_Y_MANUAL));
                ALIGNX_T_SIGN_MANUAL = Convert.ToInt32(this.GetData(CSign.ALIGNX_T_MANUAL));

                ODDCHECK_X_S_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_X_S_AUTO));
                ODDCHECK_Y_S_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_Y_S_AUTO));
                ODDCHECK_T_S_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_T_S_AUTO));

                ODDCHECK_X_X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_X_X_AUTO));
                ODDCHECK_Y_X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_Y_X_AUTO));
                ODDCHECK_T_X_AUTO_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_T_X_AUTO));

                ODDCHECK_X_S_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_X_S_MANUAL));
                ODDCHECK_Y_S_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_Y_S_MANUAL));
                ODDCHECK_T_S_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_T_S_MANUAL));

                ODDCHECK_X_X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_X_X_MANUAL));
                ODDCHECK_Y_X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_Y_X_MANUAL));
                ODDCHECK_T_X_MANUAL_SIGN = Convert.ToInt32(this.GetData(CSign.ODDCHECK_T_X_MANUAL));

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
                int I32RowIndex = CDB.It[CDB.TABLE_SIGN].SelectRowIndex(CDB.SIGN_NAME, StrName);
                object OValue = CDB.It[CDB.TABLE_SIGN].Select(I32RowIndex, CDB.SIGN_VALUE);

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
                int I32RowIndex = CDB.It[CDB.TABLE_SIGN].SelectRowIndex(CDB.SIGN_NAME, StrName);
                CDB.It[CDB.TABLE_SIGN].Update(I32RowIndex, CDB.SIGN_VALUE, OValue.ToString());
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
                CDB.It[CDB.TABLE_SIGN].Commit();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        public void Init()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
