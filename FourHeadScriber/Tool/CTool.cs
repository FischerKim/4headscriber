
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CTool : IDisposable
    {
        #region SINGLE TON
        private static CTool Src_It = null;


        public static CTool It
        {
            get
            {
                CTool OResult = null;

                try
                {
                    if (CTool.Src_It == null)
                    {
                        CTool.Src_It = new CTool();
                    }

                    OResult = CTool.Src_It;
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
        private CRecipe m_ORecipe = null;
        private CAlignmentTool m_OAlignmentTool = null;

        private RecipeChangedHandler m_ORecipeChanged = null;
        #endregion


        #region DELEGATE & EVENT
        public delegate void RecipeChangedHandler();
        #endregion


        #region PROPERTIES
        public CRecipe ORecipe
        {
            get { return this.m_ORecipe; }
            set 
            {
                try
                {
                    try
                    {
                        this.m_OAlignmentTool.BeginSync();

                        if (this.m_ORecipe != null)
                        {
                            this.m_ORecipe.Dispose();
                            this.m_ORecipe = null;
                        }

                        if (value != null) value.Load();
                        this.m_ORecipe = value;
                        this.m_OAlignmentTool.ORecipe = value;
                    }
                    catch (Exception ex)
                    {
                        CError.Throw(ex);
                    }
                    finally
                    {
                        this.m_OAlignmentTool.EndSync();
                    }

                    this.OnRecipeChanged();
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
            }
        }


        public CAlignmentTool OAlignmentTool
        {
            get { return this.m_OAlignmentTool; }
        }


        public RecipeChangedHandler ORecipeChanged
        {
            set { this.m_ORecipeChanged = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CTool()
        {
            try
            {
                CAlignmentSet.It.Load(12);

                C4HeadAlignAlgorithm OZeroAlgorithm = new C4HeadAlignAlgorithm();
                OZeroAlgorithm.OHead12 = CAlignmentSet.It.LstOAlogrithm[0];
                OZeroAlgorithm.OHead13 = CAlignmentSet.It.LstOAlogrithm[1];
                OZeroAlgorithm.OHead14 = CAlignmentSet.It.LstOAlogrithm[2];
                OZeroAlgorithm.OHead23 = CAlignmentSet.It.LstOAlogrithm[3];
                OZeroAlgorithm.OHead24 = CAlignmentSet.It.LstOAlogrithm[4];
                OZeroAlgorithm.OHead34 = CAlignmentSet.It.LstOAlogrithm[5];

                C4HeadAlignAlgorithm ONinetyAlgorithm = new C4HeadAlignAlgorithm();
                ONinetyAlgorithm.OHead12 = CAlignmentSet.It.LstOAlogrithm[6];
                ONinetyAlgorithm.OHead13 = CAlignmentSet.It.LstOAlogrithm[7];
                ONinetyAlgorithm.OHead14 = CAlignmentSet.It.LstOAlogrithm[8];
                ONinetyAlgorithm.OHead23 = CAlignmentSet.It.LstOAlogrithm[9];
                ONinetyAlgorithm.OHead24 = CAlignmentSet.It.LstOAlogrithm[10];
                ONinetyAlgorithm.OHead34 = CAlignmentSet.It.LstOAlogrithm[11];

                this.m_OAlignmentTool = new CAlignmentTool(OZeroAlgorithm, ONinetyAlgorithm);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CTool()
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
        private void OnRecipeChanged()
        {
            try
            {
                if (this.m_ORecipeChanged != null)
                {
                    this.m_ORecipeChanged();
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
                if (this.m_OAlignmentTool != null)
                {
                    this.m_OAlignmentTool.Dispose();
                    this.m_OAlignmentTool = null;
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
