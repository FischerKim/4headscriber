using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.DB;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CDB : ADB
    {
        #region SIGNLE TON
        protected static CDB Src_It = null;


        public static CDB It
        {
            get
            {
                CDB OResult = null;

                try
                {
                    if (CDB.Src_It == null)
                    {
                        CDB.Src_It = new CDB();
                    }

                    OResult = CDB.Src_It;
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
        public const string TABLE_ENVIRONMENT = "ENVIRONMENT";
        public const string ENVIRONMENT_NAME = "NAME";
        public const string ENVIRONMENT_VALUE = "VALUE";

        public const string TABLE_SIGN = "SIGN";
        public const string SIGN_NAME = "NAME";
        public const string SIGN_VALUE = "VALUE";

        public const string TABLE_RECIPE_LIST = "RECIPE_LIST";
        public const string RECIPE_LIST_ID = "ID";
        public const string RECIPE_LIST_NAME = "NAME";

        public const string TABLE_RECIPE_INFO = "RECIPE_INFO";
        public const string RECIPE_INFO_ID = "ID";
        public const string RECIPE_INFO_RECIPE = "RECIPE";
        public const string RECIPE_INFO_ANGLE = "ANGLE";
        public const string RECIPE_INFO_STANDARD_POINT_HEAD1 = "STANDARD_POINT_HEAD1";
        public const string RECIPE_INFO_STANDARD_POINT_HEAD2 = "STANDARD_POINT_HEAD2";
        public const string RECIPE_INFO_STANDARD_POINT_HEAD3 = "STANDARD_POINT_HEAD3";
        public const string RECIPE_INFO_STANDARD_POINT_HEAD4 = "STANDARD_POINT_HEAD4";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1 = "ALIGNMENT_LIMIT_HEAD1";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2 = "ALIGNMENT_LIMIT_HEAD2";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3 = "ALIGNMENT_LIMIT_HEAD3";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4 = "ALIGNMENT_LIMIT_HEAD4";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_Y = "ALIGNMENT_LIMIT_Y";
        public const string RECIPE_INFO_ALIGNMENT_LIMIT_T = "ALIGNMENT_LIMIT_T";
        public const string RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1 = "DIRECT_CUT_LIMIT_HEAD1";
        public const string RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2 = "DIRECT_CUT_LIMIT_HEAD2";
        public const string RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3 = "DIRECT_CUT_LIMIT_HEAD3";
        public const string RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4 = "DIRECT_CUT_LIMIT_HEAD4";
        public const string RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1 = "CROSS_CUT_LIMIT_HEAD1";
        public const string RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2 = "CROSS_CUT_LIMIT_HEAD2";
        public const string RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3 = "CROSS_CUT_LIMIT_HEAD3";
        public const string RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4 = "CROSS_CUT_LIMIT_HEAD4";

        public const string RECIPE_INFO_PRODUCT_WIDTH = "WIDTH"; //0 degree
        public const string RECIPE_INFO_PRODUCT_HEIGHT = "HEIGHT"; //90 degree

        public const string TABLE_MOTION_HEAD1 = "MOTION_HEAD1";
        public const string TABLE_MOTION_HEAD2 = "MOTION_HEAD2";
        public const string TABLE_MOTION_HEAD3 = "MOTION_HEAD3";
        public const string TABLE_MOTION_HEAD4 = "MOTION_HEAD4";
        public const string MOTION_HEAD_INDEX = "INDEX";
        public const string MOTION_HEAD_MACHINE_MIN = "MACHINE_MIN";
        public const string MOTION_HEAD_MACHINE_MAX = "MACHINE_MAX";
        public const string MOTION_HEAD_LASER_MIN = "LASER_MIN";
        public const string MOTION_HEAD_LASER_MAX = "LASER_MAX";
        
        
        //20170410
        //overall
        public const string TABLE_REPORT_ALIGNMENT = "REPORT_ALIGNMENT";
        public const string REPORT_ALIGNMENT_DATETIME = "DATETIME";
        public const string REPORT_ALIGNMENT_RECIPE = "RECIPE";
        public const string REPORT_ALIGNMENT_ANGLE = "ANGLE";
        public const string REPORT_ALIGNMENT_RESULT = "RESULT";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD1_X = "MOVEMENT_HEAD1_X";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD2_X = "MOVEMENT_HEAD2_X";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD3_X = "MOVEMENT_HEAD3_X";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD4_X = "MOVEMENT_HEAD4_X";
        public const string REPORT_ALIGNMENT_MOVEMENT_Y = "MOVEMENT_Y";
        public const string REPORT_ALIGNMENT_MOVEMENT_T = "MOVEMENT_T";
        public const string REPORT_ALIGNMENT_HEAD1_RESULT = "HEAD1_RESULT";
        public const string REPORT_ALIGNMENT_HEAD1_PIXEL_X = "HEAD1_PIXEL_X";
        public const string REPORT_ALIGNMENT_HEAD1_PIXEL_Y = "HEAD1_PIXEL_Y";
        public const string REPORT_ALIGNMENT_HEAD1_FILE = "HEAD1_FILE";
        public const string REPORT_ALIGNMENT_HEAD2_RESULT = "HEAD2_RESULT";
        public const string REPORT_ALIGNMENT_HEAD2_PIXEL_X = "HEAD2_PIXEL_X";
        public const string REPORT_ALIGNMENT_HEAD2_PIXEL_Y = "HEAD2_PIXEL_Y";
        public const string REPORT_ALIGNMENT_HEAD2_FILE = "HEAD2_FILE";
        public const string REPORT_ALIGNMENT_HEAD3_RESULT = "HEAD3_RESULT";
        public const string REPORT_ALIGNMENT_HEAD3_PIXEL_X = "HEAD3_PIXEL_X";
        public const string REPORT_ALIGNMENT_HEAD3_PIXEL_Y = "HEAD3_PIXEL_Y";
        public const string REPORT_ALIGNMENT_HEAD3_FILE = "HEAD3_FILE";
        public const string REPORT_ALIGNMENT_HEAD4_RESULT = "HEAD4_RESULT";
        public const string REPORT_ALIGNMENT_HEAD4_PIXEL_X = "HEAD4_PIXEL_X";
        public const string REPORT_ALIGNMENT_HEAD4_PIXEL_Y = "HEAD4_PIXEL_Y";
        public const string REPORT_ALIGNMENT_HEAD4_FILE = "HEAD4_FILE";

        public const string TABLE_REPORT_CUT = "REPORT_CUT";
        public const string REPORT_CUT_DATETIME = "DATETIME";
        public const string REPORT_CUT_RECIPE = "RECIPE";
        public const string REPORT_CUT_ANGLE = "ANGLE";
        public const string REPORT_CUT_ITEM = "ITEM";
        public const string REPORT_CUT_RESULT = "RESULT";
        public const string REPORT_CUT_HEAD1_FOUND = "HEAD1_FOUND";
        public const string REPORT_CUT_HEAD1_STANDARD_AXIS = "HEAD1_STANDARD_AXIS";
        public const string REPORT_CUT_HEAD1_RESULT_AXIS = "HEAD1_RESULT_AXIS";
        public const string REPORT_CUT_HEAD1_RESULT = "HEAD1_RESULT";
        public const string REPORT_CUT_HEAD1_LENGTH = "HEAD1_LENGTH";
        public const string REPORT_CUT_HEAD1_FILE = "HEAD1_FILE";
        public const string REPORT_CUT_HEAD2_FOUND = "HEAD2_FOUND";
        public const string REPORT_CUT_HEAD2_STANDARD_AXIS = "HEAD2_STANDARD_AXIS";
        public const string REPORT_CUT_HEAD2_RESULT_AXIS = "HEAD2_RESULT_AXIS";
        public const string REPORT_CUT_HEAD2_RESULT = "HEAD2_RESULT";
        public const string REPORT_CUT_HEAD2_LENGTH = "HEAD2_LENGTH";
        public const string REPORT_CUT_HEAD2_FILE = "HEAD2_FILE";
        public const string REPORT_CUT_HEAD3_FOUND = "HEAD3_FOUND";
        public const string REPORT_CUT_HEAD3_STANDARD_AXIS = "HEAD3_STANDARD_AXIS";
        public const string REPORT_CUT_HEAD3_RESULT_AXIS = "HEAD3_RESULT_AXIS";
        public const string REPORT_CUT_HEAD3_RESULT = "HEAD3_RESULT";
        public const string REPORT_CUT_HEAD3_LENGTH = "HEAD3_LENGTH";
        public const string REPORT_CUT_HEAD3_FILE = "HEAD3_FILE";
        public const string REPORT_CUT_HEAD4_FOUND = "HEAD4_FOUND";
        public const string REPORT_CUT_HEAD4_STANDARD_AXIS = "HEAD4_STANDARD_AXIS";
        public const string REPORT_CUT_HEAD4_RESULT_AXIS = "HEAD4_RESULT_AXIS";
        public const string REPORT_CUT_HEAD4_RESULT = "HEAD4_RESULT";
        public const string REPORT_CUT_HEAD4_LENGTH = "HEAD4_LENGTH";
        public const string REPORT_CUT_HEAD4_FILE = "HEAD4_FILE";
        //NG
        public const string TABLE_REPORT_ALIGNMENT_NG = "REPORT_ALIGNMENT_NG";
        public const string REPORT_ALIGNMENT_DATETIME_NG = "DATETIME_NG";
        public const string REPORT_ALIGNMENT_RECIPE_NG = "RECIPE_NG";
        public const string REPORT_ALIGNMENT_ANGLE_NG = "ANGLE_NG";
        public const string REPORT_ALIGNMENT_RESULT_NG = "RESULT_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD1_X_NG = "MOVEMENT_HEAD1_X_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD2_X_NG = "MOVEMENT_HEAD2_X_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD3_X_NG = "MOVEMENT_HEAD3_X_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_HEAD4_X_NG = "MOVEMENT_HEAD4_X_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_Y_NG = "MOVEMENT_Y_NG";
        public const string REPORT_ALIGNMENT_MOVEMENT_T_NG = "MOVEMENT_T_NG";
        public const string REPORT_ALIGNMENT_HEAD1_RESULT_NG = "HEAD1_RESULT_NG";
        public const string REPORT_ALIGNMENT_HEAD1_PIXEL_X_NG = "HEAD1_PIXEL_X_NG";
        public const string REPORT_ALIGNMENT_HEAD1_PIXEL_Y_NG = "HEAD1_PIXEL_Y_NG";
        public const string REPORT_ALIGNMENT_HEAD1_FILE_NG = "HEAD1_FILE_NG";
        public const string REPORT_ALIGNMENT_HEAD2_RESULT_NG = "HEAD2_RESULT_NG";
        public const string REPORT_ALIGNMENT_HEAD2_PIXEL_X_NG = "HEAD2_PIXEL_X_NG";
        public const string REPORT_ALIGNMENT_HEAD2_PIXEL_Y_NG = "HEAD2_PIXEL_Y_NG";
        public const string REPORT_ALIGNMENT_HEAD2_FILE_NG = "HEAD2_FILE_NG";
        public const string REPORT_ALIGNMENT_HEAD3_RESULT_NG = "HEAD3_RESULT_NG";
        public const string REPORT_ALIGNMENT_HEAD3_PIXEL_X_NG = "HEAD3_PIXEL_X_NG";
        public const string REPORT_ALIGNMENT_HEAD3_PIXEL_Y_NG = "HEAD3_PIXEL_Y_NG";
        public const string REPORT_ALIGNMENT_HEAD3_FILE_NG = "HEAD3_FILE_NG";
        public const string REPORT_ALIGNMENT_HEAD4_RESULT_NG = "HEAD4_RESULT_NG";
        public const string REPORT_ALIGNMENT_HEAD4_PIXEL_X_NG = "HEAD4_PIXEL_X_NG";
        public const string REPORT_ALIGNMENT_HEAD4_PIXEL_Y_NG = "HEAD4_PIXEL_Y_NG";
        public const string REPORT_ALIGNMENT_HEAD4_FILE_NG = "HEAD4_FILE_NG";

        public const string TABLE_REPORT_CUT_NG = "REPORT_CUT_NG";
        public const string REPORT_CUT_DATETIME_NG = "DATETIME_NG";
        public const string REPORT_CUT_RECIPE_NG = "RECIPE_NG";
        public const string REPORT_CUT_ANGLE_NG = "ANGLE_NG";
        public const string REPORT_CUT_ITEM_NG = "ITEM_NG";
        public const string REPORT_CUT_RESULT_NG = "RESULT_NG";
        public const string REPORT_CUT_HEAD1_FOUND_NG = "HEAD1_FOUND_NG";
        public const string REPORT_CUT_HEAD1_STANDARD_AXIS_NG = "HEAD1_STANDARD_AXIS_NG";
        public const string REPORT_CUT_HEAD1_RESULT_AXIS_NG = "HEAD1_RESULT_AXIS_NG";
        public const string REPORT_CUT_HEAD1_RESULT_NG = "HEAD1_RESULT_NG";
        public const string REPORT_CUT_HEAD1_LENGTH_NG = "HEAD1_LENGTH_NG";
        public const string REPORT_CUT_HEAD1_FILE_NG = "HEAD1_FILE_NG";
        public const string REPORT_CUT_HEAD2_FOUND_NG = "HEAD2_FOUND_NG";
        public const string REPORT_CUT_HEAD2_STANDARD_AXIS_NG = "HEAD2_STANDARD_AXIS_NG";
        public const string REPORT_CUT_HEAD2_RESULT_AXIS_NG = "HEAD2_RESULT_AXIS_NG";
        public const string REPORT_CUT_HEAD2_RESULT_NG = "HEAD2_RESULT_NG";
        public const string REPORT_CUT_HEAD2_LENGTH_NG = "HEAD2_LENGTH_NG";
        public const string REPORT_CUT_HEAD2_FILE_NG = "HEAD2_FILE_NG";
        public const string REPORT_CUT_HEAD3_FOUND_NG = "HEAD3_FOUND_NG";
        public const string REPORT_CUT_HEAD3_STANDARD_AXIS_NG = "HEAD3_STANDARD_AXIS_NG";
        public const string REPORT_CUT_HEAD3_RESULT_AXIS_NG = "HEAD3_RESULT_AXIS_NG";
        public const string REPORT_CUT_HEAD3_RESULT_NG = "HEAD3_RESULT_NG";
        public const string REPORT_CUT_HEAD3_LENGTH_NG = "HEAD3_LENGTH_NG";
        public const string REPORT_CUT_HEAD3_FILE_NG = "HEAD3_FILE_NG";
        public const string REPORT_CUT_HEAD4_FOUND_NG = "HEAD4_FOUND_NG";
        public const string REPORT_CUT_HEAD4_STANDARD_AXIS_NG = "HEAD4_STANDARD_AXIS_NG";
        public const string REPORT_CUT_HEAD4_RESULT_AXIS_NG = "HEAD4_RESULT_AXIS_NG";
        public const string REPORT_CUT_HEAD4_RESULT_NG = "HEAD4_RESULT_NG";
        public const string REPORT_CUT_HEAD4_LENGTH_NG = "HEAD4_LENGTH_NG";
        public const string REPORT_CUT_HEAD4_FILE_NG = "HEAD4_FILE_NG";
        #endregion


        #region FUNCTION
        protected override void InitDB() { }
        #endregion
    }
}
