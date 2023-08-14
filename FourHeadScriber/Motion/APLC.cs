using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jhjo.Common;
using System.Threading;
using ACTMULTILib;

namespace FourHeadScriber
{
    public abstract class APLC : IDisposable
    {
        #region VARIABLE
        protected ActEasyIF m_ODevice = null;
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;

        private ConnectionCompletedHandler m_OConnectionCompleted = null;
        public DisconnectedHandler m_ODisconnected = null;
        #endregion


        #region DELEGATE & EVENT
        public delegate void ConnectionCompletedHandler(int I32Connection);
        public delegate void DisconnectedHandler(int I32Connection);
        #endregion


        #region PROPERTIES
        public DisconnectedHandler ODisconnected
        {
            set { this.m_ODisconnected = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public APLC(ConnectionCompletedHandler OConnectionCompleted)
        {
            try
            {
                this.m_OConnectionCompleted = OConnectionCompleted;
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~APLC()
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
        protected void BeginWork()
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


        protected virtual void Work()
        {
            try
            {
                int I32Connection = this.Connect();
                this.OnConnectionCompleted(I32Connection);

                while (this.m_BDoWork == true)
                {
                    try
                    {
                        this.Read();
                        this.Write();
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        protected void EndWork()
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


        public void AbortWork()
        {
            try
            {
                if (this.m_OWorker != null)
                {
                    this.m_BDoWork = false;

                    this.m_OWorker.Abort();
                    this.m_OWorker = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private int Connect()
        {
            int I32Result = 0;

            try
            {
                this.m_ODevice = new ActEasyIF();
                this.m_ODevice.ActLogicalStationNumber = 1;
                I32Result = this.m_ODevice.Open();

                if (I32Result == 0)
                {
                    this.Initialize();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        protected virtual void Initialize() { }


        protected virtual void Read() { }


        protected virtual void Write() { }


        protected void Disconnect()
        {
            try
            {
                if (this.m_ODevice != null)
                {
                    this.m_ODevice.Close();
                    this.m_ODevice = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public bool GetMotionBit(ushort[] ArrU16Value, Enum EArray, Enum EBit)
        {
            bool BResult = false;

            try
            {
                uint U32Array = Convert.ToUInt32(ArrU16Value[Convert.ToInt32(EArray)]);
                uint U32Bit = Convert.ToUInt32(Math.Pow(2, Convert.ToInt32(EBit)));

                BResult = ((U32Array & U32Bit) == U32Bit);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return BResult;
        }


        protected int ConvertValue(short[] ArrI16Value)
        {
            int I32Result = 0;

            try
            {
                I32Result = (ushort)ArrI16Value[0] + (ArrI16Value[1] << 16);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        protected int[] ConvertValue(int I32Value)
        {
            int[] ArrI32Result = null;

            try
            {
                ArrI32Result = new int[2];
                ArrI32Result[0] = I32Value & 0x0000FFFF;
                ArrI32Result[1] = I32Value >> 16;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return ArrI32Result;
        }


        protected double ConvertDecimal(int I32Value, int I32Place)
        {
            double F64Result = 0;

            try
            {
                F64Result = I32Value / (float)Math.Pow(10, I32Place);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return F64Result;
        }


        protected int ConvertDecimal(double F64Value, int I32Place)
        {
            int I32Result = 0;

            try
            {
                I32Result = Convert.ToInt32(F64Value * Math.Pow(10, I32Place));
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        private void OnConnectionCompleted(int I32Connection)
        {
            try
            {
                if (this.m_OConnectionCompleted != null)
                {
                    this.m_OConnectionCompleted(I32Connection);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        protected void OnDisconnected(int I32Connection)
        {
            try
            {
                if (this.m_ODisconnected != null)
                {
                    this.m_ODisconnected(I32Connection);
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
                this.EndWork();
                this.Disconnect();
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion
    }
}
