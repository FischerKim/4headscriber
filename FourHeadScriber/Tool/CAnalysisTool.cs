using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using System.Threading;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro;

namespace FourHeadScriber
{
    public class CAnalysisTool : IDisposable
    {
        #region VARIABLE
        private EVIEW m_EView = EVIEW.NONE;
        private CDisplayTool m_ODisplayer = null;

        private CAnalysisRecipe m_ORecipe = null;
        private object m_ORecipeInterrupt = null;

        private bool m_BInspectMark = false;
        private bool m_BInspectDirectCut = false;
        private bool m_BInspectCrossCut = false;
        private CMarkResult m_OMarkResult = null;
        private CCutResult m_ODirectCutResult = null;
        private CCutResult m_OCrossCutResult = null;        
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;
        #endregion


        #region PROPERTIES
        public EVIEW EView
        {
            get { return this.m_EView; }
        }


        public CDisplayTool ODisplayer
        {
            get { return this.m_ODisplayer; }
        }


        public CAnalysisRecipe ORecipe
        {
            get { return this.m_ORecipe; }
            set 
            {
                try
                {
                    Monitor.Enter(this.m_ORecipeInterrupt);

                    this.m_ORecipe = value;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
                finally
                {
                    Monitor.Exit(this.m_ORecipeInterrupt);
                }
            }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAnalysisTool(EVIEW EView)
        {
            try
            {
                this.m_EView = EView;
                this.m_ODisplayer = new CDisplayTool();
                this.m_ORecipeInterrupt = new object();
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CAnalysisTool()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        private void BeginWork()
        {
            try
            {
                if (this.m_OWorker == null)
                {
                    this.m_BDoWork = true;

                    this.m_OWorker = new Thread(this.Work);
                    this.m_OWorker.Priority = ThreadPriority.Highest;
                    this.m_OWorker.IsBackground = true;
                    this.m_OWorker.Start();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void Work()
        {
            try
            {
                while (this.m_BDoWork == true)
                {
                    try
                    {
                        Monitor.Enter(this.m_OInterrupt);

                        if (this.m_BInspectMark == true)
                        {
                            CImageInfo OImageInfo = this.m_ODisplayer.GetImageInfoToAnalysis();

                            if (OImageInfo != null)
                            {
                                this.m_BInspectMark = false;
                                this.m_OMarkResult = this.DoInspectMark(OImageInfo);
                            }
                        }
                        if (this.m_BInspectDirectCut == true)
                        {
                            CImageInfo OImageInfo = this.m_ODisplayer.GetImageInfoToAnalysis();

                            if (OImageInfo != null)
                            {
                                this.m_BInspectDirectCut = false;
                                this.m_ODirectCutResult = this.DoInspectDirectCut(OImageInfo);
                            }
                        }
                        if (this.m_BInspectCrossCut == true)
                        {
                            CImageInfo OImageInfo = this.m_ODisplayer.GetImageInfoToAnalysis();

                            if (OImageInfo != null)
                            {
                                this.m_BInspectCrossCut = false;
                                this.m_OCrossCutResult = this.DoInspectCrossCut(OImageInfo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }
                    finally
                    {
                        Monitor.Exit(this.m_OInterrupt);
                    }

                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        private void EndWork()
        {
            try
            {
                if (this.m_OWorker != null)
                {
                    this.m_BDoWork = false;

                    this.m_OWorker.Join();
                    this.m_OWorker = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void RequestMark()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_BInspectMark = true;
                this.m_OMarkResult = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private CMarkResult DoInspectMark(CImageInfo OImageInfo)
        {
            CMarkResult OResult = null;

            try
            {
                Monitor.Enter(this.m_ORecipeInterrupt);

                if (this.m_ORecipe != null)
                {
                    if (this.m_ORecipe.OMarkTool1.Pattern.Trained == true)
                    {
                        OResult = this.DoInspectMark(this.m_ORecipe.OMarkTool1, OImageInfo);
                        if (OResult != null) return OResult;
                    }
                    if (this.m_ORecipe.OMarkTool2.Pattern.Trained == true)
                    {
                        OResult = this.DoInspectMark(this.m_ORecipe.OMarkTool2, OImageInfo);
                        if (OResult != null) return OResult;
                    }
                    if (this.m_ORecipe.OMarkTool3.Pattern.Trained == true)
                    {
                        OResult = this.DoInspectMark(this.m_ORecipe.OMarkTool3, OImageInfo);
                        if (OResult != null) return OResult;
                    }
                    if (this.m_ORecipe.OMarkTool4.Pattern.Trained == true)
                    {
                        OResult = this.DoInspectMark(this.m_ORecipe.OMarkTool4, OImageInfo);
                        if (OResult != null) return OResult;
                    }
                    if (this.m_ORecipe.OMarkTool5.Pattern.Trained == true)
                    {
                        OResult = this.DoInspectMark(this.m_ORecipe.OMarkTool5, OImageInfo);
                        if (OResult != null) return OResult;
                    }

                    OResult = new CMarkResult(this.m_EView, OImageInfo);
                    OResult.BInspected = true;
                    OResult.BOK = false;
                }
                else
                {
                    OResult = new CMarkResult(this.m_EView, OImageInfo);
                    OResult.BInspected = false;
                    OResult.BOK = false;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_ORecipeInterrupt);
            }

            return OResult;
        }


        private CMarkResult DoInspectMark(CogPMAlignTool OTool, CImageInfo OImageInfo)
        {
            CMarkResult OResult = null;

            try
            {
                OTool.InputImage = OImageInfo.OImage;
                OTool.Run();

                if (OTool.Results != null)
                {
                    if (OTool.Results.Count > 0)
                    {
                        CogTransform2DLinear OToolResult = OTool.Results[0].GetPose();

                        OResult = new CMarkResult(this.m_EView, OImageInfo);
                        OResult.BInspected = true;
                        OResult.BOK = true;
                        OResult.F64S = OTool.Results[0].Score;
                        OResult.F64X = OToolResult.TranslationX;
                        OResult.F64Y = OToolResult.TranslationY;
                        

                        if (this.m_ORecipe.EStdPoint == ESTD_POINT.LEFT)
                        {
                            for (int _Index = 1; _Index < OTool.Results.Count; _Index++)
                            {
                                OToolResult = OTool.Results[_Index].GetPose();
                                if (OResult.F64X < OToolResult.TranslationX) continue;

                                OResult.F64S = OTool.Results[_Index].Score;
                                OResult.F64X = OToolResult.TranslationX;
                                OResult.F64Y = OToolResult.TranslationY;
                            }
                        }
                        else if (this.m_ORecipe.EStdPoint == ESTD_POINT.RIGHT)
                        {
                            for (int _Index = 1; _Index < OTool.Results.Count; _Index++)
                            {
                                OToolResult = OTool.Results[_Index].GetPose();
                                if (OResult.F64X > OToolResult.TranslationX) continue;

                                OResult.F64S = OTool.Results[_Index].Score;
                                OResult.F64X = OToolResult.TranslationX;
                                OResult.F64Y = OToolResult.TranslationY;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        public CMarkResult GetMarkResult()
        {
            CMarkResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                OResult = this.m_OMarkResult;
              // this.m_OMarkResult = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public void SetNullToDirectCutResult()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_ODirectCutResult = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void RequestDirectCut()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_BInspectDirectCut = true;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private CCutResult DoInspectDirectCut(CImageInfo OImageInfo)
        {
            CCutResult OResult = null;

            try
            {
                Monitor.Enter(this.m_ORecipeInterrupt);

                OResult = new CCutResult(this.m_EView, OImageInfo);

                if (this.m_ORecipe != null)
                {
                    this.m_ORecipe.ODirectCutTool.InputImage = OImageInfo.OImage;
                    this.m_ORecipe.ODirectCutTool.Run();

                    OResult.BInspected = true;
                    if ((this.m_ORecipe.ODirectCutTool.Results != null) &&(this.m_ORecipe.ODirectCutTool.Results.Count > 0))
                    {
                        OResult.BOK = true;
                        OResult.F64StartX = this.m_ORecipe.ODirectCutTool.Results[0].Edge0.PositionX;
                        OResult.F64EndX = this.m_ORecipe.ODirectCutTool.Results[0].Edge1.PositionX;

                        //if (OResult.F64StartX < OResult.F64EndX) OResult.F64MidX = OResult.F64StartX + Math.Abs((OResult.F64EndX - OResult.F64StartX) / 2);
                        //else OResult.F64MidX = OResult.F64EndX + Math.Abs((OResult.F64StartX - OResult.F64EndX) / 2);
                        //OResult.F64Length = (OResult.F64MidX - (OImageInfo.OImage.Width / 2D)) * CEnvironment.LEN_PER_PIXEL;
                        OResult.F64MidX = (OResult.F64EndX + OResult.F64StartX) / 2;
                        OResult.F64Length = (OResult.F64MidX - (OImageInfo.OImage.Width / 2D)) * CEnvironment.LEN_PER_PIXEL;
                
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_ORecipeInterrupt);
            }

            return OResult;
        }


        public CCutResult GetDirectCutResult()
        {
            CCutResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                OResult = this.m_ODirectCutResult;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public void SetNullToCrossCutResult()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_OCrossCutResult = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void RequestCrossCut()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_BInspectCrossCut = true;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private CCutResult DoInspectCrossCut(CImageInfo OImageInfo)
        {
            CCutResult OResult = null;

            try
            {
                Monitor.Enter(this.m_ORecipeInterrupt);

                OResult = new CCutResult(this.m_EView, OImageInfo);

                if (this.m_ORecipe != null)
                {
                    this.m_ORecipe.OCrossCutTool.InputImage = OImageInfo.OImage;
                    this.m_ORecipe.OCrossCutTool.Run();

                    OResult.BInspected = true;
                    if ((this.m_ORecipe.OCrossCutTool.Results != null) && (this.m_ORecipe.OCrossCutTool.Results.Count > 0))
                    {
                        OResult.BOK = true;
                        OResult.F64StartX = this.m_ORecipe.OCrossCutTool.Results[0].Edge0.PositionX;
                        OResult.F64EndX = this.m_ORecipe.OCrossCutTool.Results[0].Edge1.PositionX;
                        //20170408
                        //if (OResult.F64StartX < OResult.F64EndX) OResult.F64MidX = OResult.F64StartX + Math.Abs((OResult.F64EndX - OResult.F64StartX) / 2);
                        //else OResult.F64MidX = OResult.F64EndX + Math.Abs((OResult.F64StartX - OResult.F64EndX) / 2);
                        //OResult.F64Length = (OResult.F64MidX - (OImageInfo.OImage.Width / 2D)) * CEnvironment.LEN_PER_PIXEL;
                        OResult.F64MidX = (OResult.F64EndX + OResult.F64StartX) / 2;
                        OResult.F64Length = (OResult.F64MidX - (OImageInfo.OImage.Width / 2D)) * CEnvironment.LEN_PER_PIXEL;
                
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_ORecipeInterrupt);
            }

            return OResult;
        }


        public CCutResult GetCrossCutResult()
        {
            CCutResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                OResult = this.m_OCrossCutResult;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public void Dispose()
        {
            try
            {
                this.EndWork();

                if (this.m_ODisplayer != null)
                {
                    this.m_ODisplayer.Dispose();
                    this.m_ODisplayer = null;
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
