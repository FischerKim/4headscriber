using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CRecipeManager
    {
        #region SIGNLE TON
        protected static CRecipeManager Src_It = null;
        public static CRecipeManager It
        {
            get
            {
                CRecipeManager OResult = null;

                try
                {
                    if (CRecipeManager.Src_It == null)
                    {
                        CRecipeManager.Src_It = new CRecipeManager();
                    }

                    OResult = CRecipeManager.Src_It;
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
        private List<CRecipe> m_LstORecipe = null;
        #endregion
        

        #region PROPERTIES
        public List<CRecipe> LstORecipe
        {
            get { return this.m_LstORecipe; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CRecipeManager()
        {
            try
            {
                this.m_LstORecipe = new List<CRecipe>();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Load()
        {
            try
            {
                this.LoadRecipeList();
                this.LoadRecipeInfo();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void LoadRecipeList()
        {
            try
            {
                DataTable ODataSource = CDB.It[CDB.TABLE_RECIPE_LIST].Select();

                foreach (DataRow _Item in ODataSource.Rows)
                {
                    this.m_LstORecipe.Add(new CRecipe((int)_Item[CDB.RECIPE_LIST_ID], (string)_Item[CDB.RECIPE_LIST_NAME]));
                }

                if (ODataSource != null) 
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private void LoadRecipeInfo()
        {
            try
            {
                DataTable ODataSource = CDB.It[CDB.TABLE_RECIPE_INFO].Select();

                foreach (DataRow _Item1 in ODataSource.Rows)
                {
                    foreach (CRecipe _Item2 in this.m_LstORecipe)
                    {
                        if ((int)_Item1[CDB.RECIPE_INFO_ID] != _Item2.I32ID) continue;

                        if ((string)_Item1[CDB.RECIPE_INFO_ANGLE] == ESTAGE_ANGLE.ZERO.ToString())
                        {
                            _Item2.OZero.OHead1.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD1]);
                            _Item2.OZero.OHead2.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD2]);
                            _Item2.OZero.OHead3.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD3]);
                            _Item2.OZero.OHead4.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD4]);
                            _Item2.OZero.F64AlignmentLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1];
                            _Item2.OZero.F64AlignmentLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2];
                            _Item2.OZero.F64AlignmentLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3];
                            _Item2.OZero.F64AlignmentLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4];
                            _Item2.OZero.F64AlignmentLimitY = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y];
                            _Item2.OZero.F64AlignmentLimitT = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T];
                            _Item2.OZero.F64AlignmentProductLength = (double)_Item1[CDB.RECIPE_INFO_PRODUCT_WIDTH];
                            _Item2.OZero.F64DirectCutLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1];
                            _Item2.OZero.F64DirectCutLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2];
                            _Item2.OZero.F64DirectCutLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3];
                            _Item2.OZero.F64DirectCutLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4];
                            _Item2.OZero.F64CrossCutLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1];
                            _Item2.OZero.F64CrossCutLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2];
                            _Item2.OZero.F64CrossCutLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3];
                            _Item2.OZero.F64CrossCutLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4];
                        }
                        else if ((string)_Item1[CDB.RECIPE_INFO_ANGLE] == ESTAGE_ANGLE.NINETY.ToString())
                        {
                            _Item2.ONinety.OHead1.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD1]);
                            _Item2.ONinety.OHead2.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD2]);
                            _Item2.ONinety.OHead3.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD3]);
                            _Item2.ONinety.OHead4.EStdPoint = (ESTD_POINT)Enum.Parse(typeof(ESTD_POINT), (string)_Item1[CDB.RECIPE_INFO_STANDARD_POINT_HEAD4]);
                            _Item2.ONinety.F64AlignmentLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD1];
                            _Item2.ONinety.F64AlignmentLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD2];
                            _Item2.ONinety.F64AlignmentLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD3];
                            _Item2.ONinety.F64AlignmentLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_HEAD4];
                            _Item2.ONinety.F64AlignmentLimitY = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_Y];
                            _Item2.ONinety.F64AlignmentLimitT = (double)_Item1[CDB.RECIPE_INFO_ALIGNMENT_LIMIT_T];
                            _Item2.ONinety.F64AlignmentProductLength = (double)_Item1[CDB.RECIPE_INFO_PRODUCT_HEIGHT];
                            _Item2.ONinety.F64DirectCutLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD1];
                            _Item2.ONinety.F64DirectCutLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD2];
                            _Item2.ONinety.F64DirectCutLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD3];
                            _Item2.ONinety.F64DirectCutLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_DIRECT_CUT_LIMIT_HEAD4];
                            _Item2.ONinety.F64CrossCutLimitHead1 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD1];
                            _Item2.ONinety.F64CrossCutLimitHead2 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD2];
                            _Item2.ONinety.F64CrossCutLimitHead3 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD3];
                            _Item2.ONinety.F64CrossCutLimitHead4 = (double)_Item1[CDB.RECIPE_INFO_CROSS_CUT_LIMIT_HEAD4];
                        }
                        break;
                    }
                }
                if (ODataSource == null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
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
