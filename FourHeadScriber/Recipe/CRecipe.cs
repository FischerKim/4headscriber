using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Jhjo.Common;

namespace FourHeadScriber
{
    public class CRecipe : IDisposable
    {
        #region CONST
        private const string RECIPE_ROOT_DIR = ".\\Recipe";
        #endregion


        #region VARIABLE
        private int m_I32ID = 0;
        private string m_StrName = string.Empty;
        private string m_StrDirectory = string.Empty;

        private CAlignmentRecipe m_OZero = null;
        private CAlignmentRecipe m_ONinety = null;
        #endregion


        #region PROPERTIES
        public int I32ID
        {
            get { return this.m_I32ID; }
        }


        public string StrName
        {
            get { return this.m_StrName; }
            set { this.m_StrName = value; }
        }


        public string StrDirectory
        {
            get { return this.m_StrDirectory; }
            set { this.m_StrDirectory = value; }
        }


        public CAlignmentRecipe OZero
        {
            get { return this.m_OZero; }
            set { this.m_OZero = value; }
        }


        public CAlignmentRecipe ONinety
        {
            get { return this.m_ONinety; }
            set { this.m_ONinety = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CRecipe(int I32ID, string StrName)
        {
            try
            {
                this.m_I32ID = I32ID;
                this.m_StrName = StrName;
                this.m_StrDirectory = RECIPE_ROOT_DIR + "\\" + this.m_I32ID;

                this.m_OZero = new CAlignmentRecipe(ESTAGE_ANGLE.ZERO, this.m_StrName, this.m_StrDirectory);
                this.m_ONinety = new CAlignmentRecipe(ESTAGE_ANGLE.NINETY, this.m_StrName, this.m_StrDirectory);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CRecipe(CRecipe OSource)
        {
            try
            {
                this.m_I32ID = OSource.m_I32ID;
                this.m_StrName = OSource.m_StrName;
                this.m_StrDirectory = OSource.m_StrDirectory;

                this.m_OZero = new CAlignmentRecipe(OSource.m_OZero);
                this.m_ONinety = new CAlignmentRecipe(OSource.m_ONinety);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Create()
        {
            try
            {
                this.m_OZero.Create();
                this.m_ONinety.Create();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Load()
        {
            try
            {
                this.m_OZero.Load();
                this.m_ONinety.Load();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Save()
        {
            try
            {
                this.m_OZero.Save();
                this.m_ONinety.Save();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Delete()
        {
            try
            {
                Directory.Delete(this.m_StrDirectory, true);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Copy(CRecipe OSource)
        {
            try
            {
                FileSystem.CopyDirectory(OSource.m_StrDirectory, this.m_StrDirectory, true);

                this.m_OZero.Copy(OSource.m_OZero);
                this.m_ONinety.Copy(OSource.m_ONinety);
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
                if (this.m_OZero != null)
                {
                    this.m_OZero.Dispose();
                    this.m_OZero = null;
                }
                if (this.m_ONinety != null)
                {
                    this.m_ONinety.Dispose();
                    this.m_ONinety = null;
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
