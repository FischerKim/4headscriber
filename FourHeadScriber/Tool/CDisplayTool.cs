using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Daekhon.Common;
using Cognex.VisionPro;
using System.Threading;
using Jhjo.Common;
using System.Drawing;

namespace FourHeadScriber
{
    public class CDisplayTool : IDisposable
    {
        #region VARIABLE
        private string m_StrDisplayName = string.Empty;
        private IImageExporter m_OView = null;
        private List<CImageInfo> m_LstOImageInfo = null;
        private List<long> m_LstI64Tick = null;
        private int m_I32NoImageCountPerSec = 0;
        private int m_I32ViewerResetCount = 0;
        private CogImage8Grey m_OCurrentImage = null;
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;

        private ImageExportedHandler m_OImageExported = null;
        private CameraDisconnectedHandler m_OCameraDisconnected = null;
        #endregion


        #region DELEGATE & EVENT
        public delegate void ImageExportedHandler(CImageInfo OImageInfo);
        public delegate void CameraDisconnectedHandler(string StrDisplayName);
        #endregion


        #region PROPERTIES
        public string StrDisplayName
        {
            get { return this.m_StrDisplayName; }
            set { this.m_StrDisplayName = value; }
        }


        public IImageExporter OView
        {
            get { return this.m_OView; }
            set
            {
                try
                {
                    if (this.m_OView != null)
                    {
                        this.m_OView.Exported -= new ExportedHandler(OView_Exported);
                    }

                    this.m_OView = value;
                    if (this.m_OView != null)
                    {
                        this.m_OView.Exported += new ExportedHandler(OView_Exported);
                    }
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
            }
        }


        public CogImage8Grey OCurrentImage
        {
            get { return this.m_OCurrentImage; }
        }


        public ImageExportedHandler OImageExported
        {
            set { this.m_OImageExported = value; }
        }


        public CameraDisconnectedHandler OCameraDisconnected
        {
            set { this.m_OCameraDisconnected = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CDisplayTool()
        {
            try
            {
                this.m_LstOImageInfo = new List<CImageInfo>();
                this.m_LstI64Tick = new List<long>();
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CDisplayTool()
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


        #region EVENT
        private void OView_Exported(object OSource)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CImageInfo OImageInfo = new CImageInfo((Bitmap)OSource);
                this.m_LstOImageInfo.Add(OImageInfo);
                this.m_LstI64Tick.Add(OImageInfo.OTime.Ticks);
                this.m_OCurrentImage = OImageInfo.OImage;
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
                        CImageInfo OImageInfo = this.GetImageInfoToDisplay();

                        if (OImageInfo != null)
                        {
                            this.OnImageExported(OImageInfo);
                        }

                        if (this.CheckCameraConntion() == false)
                        {
                            if (this.m_I32ViewerResetCount < 3)
                            {
                                this.m_I32NoImageCountPerSec = 0;
                                this.m_I32ViewerResetCount++;

                                this.m_OView.Stop();
                                this.m_OView.Start();
                            }
                            else this.OnCameraDisconnected();
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }

                    Thread.Sleep(1);
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


        private CImageInfo GetImageInfoToDisplay()
        {
            CImageInfo OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstOImageInfo.Count >= 2)
                {
                    this.m_LstOImageInfo.RemoveRange(0, this.m_LstOImageInfo.Count - 2);

                    OResult = this.m_LstOImageInfo[0];
                    this.m_LstOImageInfo.RemoveAt(0);
                }
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


        public CImageInfo GetImageInfoToAnalysis()
        {
            CImageInfo OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstOImageInfo.Count > 0)
                {
                    OResult = this.m_LstOImageInfo[this.m_LstOImageInfo.Count - 1];
                    this.m_LstOImageInfo.RemoveAt(this.m_LstOImageInfo.Count - 1);
                }
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


        public void CheckFrameCount()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                int I32FrameCount = this.m_LstI64Tick.Count;
                this.m_LstI64Tick.Clear();

                if (this.m_OView != null)
                {
                    if (this.m_OView.BRun == true)
                    {
                        if (I32FrameCount == 0)
                        {
                            this.m_I32NoImageCountPerSec += 1;
                        }
                        else
                        {
                            this.m_I32NoImageCountPerSec = 0;
                            this.m_I32ViewerResetCount = 0;
                        }
                    }
                    else this.m_I32NoImageCountPerSec = 0;
                }
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


        private bool CheckCameraConntion()
        {
            bool BResult = true;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_I32NoImageCountPerSec == 3)
                {
                    this.m_I32NoImageCountPerSec = 0;
                    BResult = false;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return BResult;
        }


        private void OnImageExported(CImageInfo OImageInfo)
        {
            try
            {
                if (this.m_OImageExported != null)
                {
                    this.m_OImageExported(OImageInfo);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnCameraDisconnected()
        {
            try
            {
                if (this.m_OCameraDisconnected != null)
                {
                    this.m_OCameraDisconnected(this.m_StrDisplayName);
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
                this.EndWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
