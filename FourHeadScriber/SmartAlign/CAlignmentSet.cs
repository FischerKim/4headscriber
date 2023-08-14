using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smal_CS;
using Jhjo.Common;
using System.Threading;


namespace FourHeadScriber
{
    public class CAlignmentSet : IDisposable
    {
        #region SINGLE TON
        private static CAlignmentSet Src_It = null;


        public static CAlignmentSet It
        {
            get
            {
                CAlignmentSet OResult = null;

                try
                {
                    if (CAlignmentSet.Src_It == null)
                    {
                        CAlignmentSet.Src_It = new CAlignmentSet();
                    }

                    OResult = CAlignmentSet.Src_It;
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
        private WrapStaticLibSmal m_OTool = null;
        private List<CAlignmentAlgorithm> m_LstOAlogrithm = null;
        private object m_OInterrupt = null;
        #endregion


        #region PROPERTIES
        public WrapStaticLibSmal OTool
        {
            get { return this.m_OTool; }
        }


        public List<CAlignmentAlgorithm> LstOAlogrithm
        {
            get { return this.m_LstOAlogrithm; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CAlignmentSet()
        {
            try
            {
                this.m_OTool = new WrapStaticLibSmal();
                this.m_OTool.SmalInitialize();
                this.m_LstOAlogrithm = new List<CAlignmentAlgorithm>();
                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CAlignmentSet()
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
        public void Load(int I32Count)
        {
            try
            {
                this.m_OTool.SmalSetNumStage(I32Count);

                for (int _Index = 0; _Index < I32Count; _Index++)
                {
                    this.m_LstOAlogrithm.Add(new CAlignmentAlgorithm(this.m_OTool, _Index));
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void BeginSync()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndSync()
        {
            try
            {
                Monitor.Exit(this.m_OInterrupt);
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
                if (this.m_OTool != null)
                {
                    this.m_OTool.Dispose();
                    this.m_OTool = null;
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion
    }
}
