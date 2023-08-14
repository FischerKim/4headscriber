using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace FourHeadScriber
{
    public class ClassSmartAlign
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // enumeration
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum enumStageNo { STAGE_1 = 0, STAGE_2, STAGE_3, STAGE_4, STAGE_5, STAGE_6, STAGE_7, STAGE_8 };
        public enum enumStageType { STAGE_XYT = 0, STAGE_UVW, STAGE_TXY, STAGE_TX };
        public enum enumStageView { HALF_VIEW = 0, FULL_VIEW };
        public enum enumUVWStage { UVW_U = 0, UVW_V, UVW_W };
        public enum enumCameraNo { CAMERA_1 = 0, CAMERA_2, CAMERA_3, CAMERA_4 };
        public enum enumCameraView { TOP_VIEW = 0, BOT_VIEW };
        public enum enumCameraAlignUse { CAMERA_USE_1 = 1, CAMERA_USE_2, CAMERA_USE_3, CAMERA_USE_4 };
        public enum enumAlignmentHorizon { HORIZON_LEFT = 0, HORIZON_CENTER, HORIZON_RIGHT };
        public enum enumAlignmentVertical { VERTICAL_TOP = 0, VERTICAL_CENTER, VERTICAL_BOT };

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // structure
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public struct SMARTALIGN_PARAMETER
        {
            public string strVersion;
            public enumStageType objEnumStageType;
            public enumCameraAlignUse m_objEnumCameraAlignUse;
            public string[] strCameraResolution;
            public string[] strCameraPosition;
            public double dSectionXY;
            public int iPointXY;
            public double dSectionT;
            public int iPointT;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private smal_CS.WrapStaticLibSmal m_objSmartAlign;
        private Thread m_ThreadCalibration;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // protected
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string m_strRecipePath;
        protected bool m_bThreadExit;
        protected int m_iStageCount;
        protected int m_iCameraCount;
        protected int[] m_iCalibrationRows;
        protected int[] m_iCalibrationCols;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SMARTALIGN_PARAMETER m_objSmartAlignParameter;
        //Calibrate Buffer
        public List<List<double>>[] m_objCalibrateData;
        public List<List<double>>[] m_objCalibrateDataTop;
        public List<List<double>>[] m_objCalibrateDataBot;

        public List<List<double>>[] m_objPixelPosition;

        /******************************************************************************************************************************************************************
        * 제목 : 초기화
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool Initialize(int iStageCount, int iCameraCount, string strRecipePath)
        {
            bool bReturn = false;

            do
            {
                m_bThreadExit = false;

                m_iStageCount = iStageCount; m_iCameraCount = iCameraCount; m_strRecipePath = strRecipePath;
                m_objSmartAlign = null;
                m_objSmartAlign = new smal_CS.WrapStaticLibSmal();
                m_objSmartAlignParameter = new SMARTALIGN_PARAMETER();

                //캘리브레이션 행, 열 설정
                m_iCalibrationRows = new int[m_iStageCount]; m_iCalibrationCols = new int[m_iStageCount];
                m_objCalibrateData = new List<List<double>>[m_iStageCount];
                m_objCalibrateDataTop = new List<List<double>>[m_iStageCount]; m_objCalibrateDataBot = new List<List<double>>[m_iStageCount];

                m_objPixelPosition = new List<List<double>>[m_iStageCount];

                for (int iLoopCount = 0; iLoopCount < m_iStageCount; iLoopCount++)
                {
                    m_iCalibrationRows[iLoopCount] = new int();
                    m_iCalibrationCols[iLoopCount] = new int();

                    m_objPixelPosition[iLoopCount] = new List<List<double>>();
                    m_objCalibrateData[iLoopCount] = new List<List<double>>();
                    m_objCalibrateDataTop[iLoopCount] = new List<List<double>>(); m_objCalibrateDataBot[iLoopCount] = new List<List<double>>();
                }

                SetMaxStageCount(iStageCount);

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 해제
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public void DeInitialize()
        {
            m_bThreadExit = true;

            if (null != m_ThreadCalibration)
            {
                m_ThreadCalibration.Abort(); m_ThreadCalibration = null;
            }

            m_objSmartAlign.Dispose();
        }

        /******************************************************************************************************************************************************************
        * 제목 : 얼라인 정렬 포지션을 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string GetAlignmentPosition(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse)
        {
            string strReturn = "";

            try
            {
                strReturn = GetListToString(GetTranspose(m_objSmartAlign.SmalGetAlignWeight((int)enumIndexStage, (int)enumIndexCameraAlignUse)));
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetAlignmentPosition : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 얼라인 정렬 포지션을 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public List<int> GetStringToArray(string strArray)
        {
            List<int> objReturn = new List<int>();
            MemoryStream objMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strArray));
            StreamReader objStreamReader = new StreamReader(objMemoryStream);

            try
            {
                string[] szArray = null;
                string strData = "";

                strData = objStreamReader.ReadLine();
                szArray = strData.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string strArrayData in szArray)
                {
                    objReturn.Add(int.Parse(strArrayData));
                }

                objStreamReader.Close();
                objMemoryStream.Close();
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetStringToArray : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objReturn;
        }
        /******************************************************************************************************************************************************************
        * 제목 : 얼라인 정렬 포지션을 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string GetCalibrateCameraPosition(enumStageNo enumIndexStage)
        {
            string strReturn = "";

            try
            {
                strReturn = GetListToString(GetTranspose(m_objSmartAlign.SmalRecallCalibData((int)enumIndexStage)));
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetCalibrateCameraPosition : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 캘리브레이션 포지션을 가져온다
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool GetCalibPosition(enumStageNo enumIndexStage)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    m_objSmartAlign.SmalGetCalibPos((int)enumIndexStage, m_objCalibrateData[(int)enumIndexStage]);
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error GetCalibPosition : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 캘리브레이션 포지션 카운트를 가져온다
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public int GetCalibrationCols(enumStageNo enumIndexStage)
        {
            return m_objCalibrateData[(int)enumIndexStage].ElementAt(0).Count;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 캘리브레이션 이동량을 가져온다
        * 인자 : U = X, V = Y, W = T
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool GetCalibrationMoveData(enumStageNo enumIndexStage, ref double dMoveU, ref double dMoveV, ref double dMoveW, int iLoopCount)
        {
            bool bReturn = false;

            do
            {
                string strData = "";

                strData = String.Format("{0:F3}", m_objCalibrateData[(int)enumIndexStage].ElementAt(0).ElementAt(iLoopCount));
                dMoveU = Convert.ToDouble(strData);
                strData = String.Format("{0:F3}", m_objCalibrateData[(int)enumIndexStage].ElementAt(1).ElementAt(iLoopCount));
                dMoveV = Convert.ToDouble(strData);
                strData = String.Format("{0:F3}", m_objCalibrateData[(int)enumIndexStage].ElementAt(2).ElementAt(iLoopCount));
                dMoveW = Convert.ToDouble(strData);

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 회전 중심으로부터 카메라의 위치를 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool GetCameraPosition(enumStageNo enumIndexStage, ref string strPosition)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    List<List<double>> objPosition = new List<List<double>>();

                    if (false == m_objSmartAlign.SmalGetCameraPos((int)enumIndexStage, objPosition)) break;

                    strPosition = GetListToString(GetTranspose(objPosition));
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error GetCameraPosition : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 선택된 카메라의 Pixel Resolution 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string GetCameraResolution(enumStageNo enumIndexStage, enumCameraNo enumIndexCameraNo)
        {
            string strReturn = "";

            try
            {
                List<double> objData = new List<double>();

                if (true == m_objSmartAlign.SmalCameraResolution((int)enumIndexStage, (int)enumIndexCameraNo, objData))
                {
                    List<List<double>> objCameraResolution = new List<List<double>>();
                    objCameraResolution.Add(objData);
                    strReturn = GetListToString(objCameraResolution);
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetCameraResolution : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 선택된 카메라의 Goal Position 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string GetGoalPosition(enumStageNo enumIndexStage, enumCameraNo enumIndexCameraNo)
        {
            string strReturn = "";

            try
            {
                List<List<double>> objGoalPosition = new List<List<double>>();
                objGoalPosition = m_objSmartAlign.SmalGetGoalPos((int)enumIndexStage, (int)enumIndexCameraNo);

                if (0 < objGoalPosition.Count)
                {
                    strReturn = GetListToString(GetTranspose(objGoalPosition));
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetGoalPosition : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 카메라 수를 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public int GetMaxCameraCount(enumStageNo enumIndexStage)
        {
            int iReturn = 0;

            try
            {
                iReturn = m_objSmartAlign.SmalGetNumCameras((int)enumIndexStage);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetMaxCameraCount : " + ex.Message + " ->" + ex.StackTrace);
            }

            return iReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Matrix 변환
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        private List<List<double>> GetMatrixDouble(string strMatrix)
        {
            string strReturn = "";

            List<List<double>> objReturn = new List<List<double>>();
            MemoryStream objMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strMatrix));
            StreamReader objStreamReader = new StreamReader(objMemoryStream);

            try
            {
                int iRows, iCols;
                iRows = iCols = 0;
                string[] szArray = null;
                string strData = "";

                while (false == objStreamReader.EndOfStream)
                {
                    iCols = 0;
                    strData = objStreamReader.ReadLine();
                    szArray = strData.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> objList = new List<double>();

                    foreach (string strArrayData in szArray)
                    {
                        objList.Add(double.Parse(strArrayData));
                        iCols++;
                    }

                    iRows++;
                    objReturn.Add(objList);
                }

                objStreamReader.Close();
                objMemoryStream.Close();

            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetMatrixDouble : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Matrix 변환
        * 인자 : 
        * 리턴 : 정수형 중첩리스트
        * 설명 : 
        ******************************************************************************************************************************************************************/
        private List<List<int>> GetMatrixInteger(string strMatrix)
        {
            string strReturn = "";

            List<List<int>> objReturn = new List<List<int>>();
            MemoryStream objMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strMatrix));
            StreamReader objStreamReader = new StreamReader(objMemoryStream);

            try
            {
                int iRows, iCols;
                iRows = iCols = 0;
                string[] szArray = null;
                string strData = "";

                while (false == objStreamReader.EndOfStream)
                {
                    iCols = 0;
                    strData = objStreamReader.ReadLine();
                    szArray = strData.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> objList = new List<int>();

                    foreach (string strArrayData in szArray)
                    {
                        objList.Add(int.Parse(strArrayData));
                        iCols++;
                    }

                    iRows++;
                    objReturn.Add(objList);
                }

                objStreamReader.Close();
                objMemoryStream.Close();

            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetMatrixInteger : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Matrix2x 변환
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        private List<List<double>> GetMatrix2x(string strMatrix)
        {
            List<List<double>> objReturn = new List<List<double>>();
            MemoryStream objMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strMatrix));
            StreamReader objStreamReader = new StreamReader(objMemoryStream);

            try
            {
                int iSize = 0;
                string[] szArray = null;
                string strData = "";
                List<double> objList = new List<double>();

                while (false == objStreamReader.EndOfStream)
                {
                    strData = objStreamReader.ReadLine();
                    szArray = strData.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);


                    foreach (string strArrayData in szArray)
                    {
                        objList.Add(double.Parse(strArrayData));
                        iSize++;
                    }
                }

                objStreamReader.Close();
                objMemoryStream.Close();

                int iMaxSize = iSize / 2;
                List<double> objList1 = new List<double>();
                List<double> objList2 = new List<double>();

                for (int iLoopCount = 0; iLoopCount < iMaxSize; iLoopCount++)
                {
                    objList1.Add(objList[iLoopCount * 2]); objList2.Add(objList[iLoopCount * 2 + 1]);
                }

                objReturn.Add(objList1); objReturn.Add(objList2);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetMatrix2x : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 카메라 수를 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public int GetMaxStageCount()
        {
            int iReturn = 0;

            try
            {
                iReturn = m_objSmartAlign.SmalGetNumStage();
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetMaxStageCount : " + ex.Message + " ->" + ex.StackTrace);
            }

            return iReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지의 종류를 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public enumStageType GetStageType(enumStageNo enumIndexStage)
        {
            enumStageType objIndex = new enumStageType();

            try
            {
                objIndex = (enumStageType)m_objSmartAlign.SmalGetStageType((int)enumIndexStage);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetStageType : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objIndex;
        }

        /******************************************************************************************************************************************************************
        * 제목 : SmartAlign dll 버전 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string GetSmartAlignVersion()
        {
            string strReturn = "";

            do
            {
                try
                {
                    if (false == m_objSmartAlign.SmalGetVersion(ref strReturn)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error GetSmartAlignVersion : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }
            } while (false);

            return strReturn;
        }
        /******************************************************************************************************************************************************************
        * 제목 : UVW 스테이지 정보를 리턴
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool GetUVWStageParameter(enumStageNo enumIndexStage, ref double dRadius, ref string[] strPosition, ref string[] strDirection)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    List<double> objPosition = new List<double>();
                    List<double> objDirection = new List<double>();

                    if (true == m_objSmartAlign.SmalGetUvwStageParams((int)enumIndexStage, ref dRadius, objPosition, objDirection))
                    {
                        strPosition[(int)enumUVWStage.UVW_U] = objPosition[(int)enumUVWStage.UVW_U].ToString();
                        strPosition[(int)enumUVWStage.UVW_V] = objPosition[(int)enumUVWStage.UVW_V].ToString();
                        strPosition[(int)enumUVWStage.UVW_W] = objPosition[(int)enumUVWStage.UVW_W].ToString();

                        strDirection[(int)enumUVWStage.UVW_U] = objDirection[(int)enumUVWStage.UVW_U].ToString();
                        strDirection[(int)enumUVWStage.UVW_V] = objDirection[(int)enumUVWStage.UVW_V].ToString();
                        strDirection[(int)enumUVWStage.UVW_W] = objDirection[(int)enumUVWStage.UVW_W].ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error GetUVWStageParameter : " + ex.Message + " ->" + ex.StackTrace);
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Alignment Parsing
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string ParsingAlignment(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse, enumAlignmentHorizon enumIndexAlignmentHorizon, enumAlignmentVertical enumIndexAlignmentVertical, double[] dPositionX, double[] dPositionY)
        {
            string strReturn = "";

            bool[] bNotFoundCheck = new bool[m_iCameraCount];

            try
            {
                ////Pixel Position Check
                //for (int iLoopCount = 0; iLoopCount < m_iCameraCount; iLoopCount++)
                //{
                //    if (-1 == dPositionX[iLoopCount] || -1 == dPositionX[iLoopCount]) bNotFoundCheck[iLoopCount] = true;
                //    else bNotFoundCheck[iLoopCount] = false;
                //}
                //SetCameraFailIndex(enumIndexStage, bNotFoundCheck);



                //정상일때
                //if (false == bNotFoundCheck[0] && false == bNotFoundCheck[1])// && false == bNotFoundCheck[2] && false == bNotFoundCheck[3])
                //{
                if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strReturn = "";
                }
                else if (enumCameraAlignUse.CAMERA_USE_2 == enumIndexCameraAlignUse)
                {
                    if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon) strReturn = "1 1 0\r\n 0 0 1";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon) strReturn = "0.5 0.5 0\r\n 0.5 0.5 1";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon) strReturn = "0 0 0\r\n 1 1 1";
                }
                else if (enumCameraAlignUse.CAMERA_USE_3 == enumIndexCameraAlignUse)
                {
                    if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "1 1 0 \r\n0 0 1\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "1 0.5 0 \r\n0 0 1\r\n 0 0.5 0";
                    else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n0 0 1 \r\n1 1 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "0.5 1 0 \r\n0.5 0 1\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "0.5 0.5 0 \r\n0.5 0 1\r\n 0 0.5 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0.5 0 0\r\n 0.5 0 1 \r\n0 1 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n1 1 1 \r\n0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "0 0.5 0 \r\n1 0 1 \r\n0 0.5 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n1 0 1\r\n 0 1 0";
                }
                else if (enumCameraAlignUse.CAMERA_USE_4 == enumIndexCameraAlignUse)
                {
                    if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "1 1 0\r\n0 0 1\r\n0 0 0\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "1 0.5 0\r\n 0 0 1 \r\n0 0.5 0\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n0 0 1 \r\n1 1 0 \r\n0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "0.5 1 0 \r\n0.5 0 1\r\n 0 0 0 \r\n0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "0.5 0.5 0\r\n 0.5 0 1 \r\n0 0.5 0\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0.5 0 0 \r\n0.5 0 1\r\n 0 1 0\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n1 1 1 \r\n0 0 0 \r\n0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "0 0.5 0 \r\n1 0 1\r\n 0 0.5 0\r\n 0 0 0";
                    else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "0 0 0 \r\n0 0 1\r\n 0 0 0 \r\n1 1 0";
                }

                SetAlignmentPosition(enumIndexStage, enumIndexCameraAlignUse, strReturn);
                //}
                ////픽셀 포지션 NG 발생일때
                //else
                //{
                //    if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                //    {

                //    }
                //    else if (enumCameraAlignUse.CAMERA_USE_2 == enumIndexCameraAlignUse)
                //    {

                //    }
                //    else if (enumCameraAlignUse.CAMERA_USE_3 == enumIndexCameraAlignUse)
                //    {
                //        if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_3])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "1 1 0\r\n0 0 1\r\n0 0 0";
                //        }
                //        else if (true == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "1 1 0\r\n0 0 0\r\n0 0 1";
                //        }
                //    }
                //    else if (enumCameraAlignUse.CAMERA_USE_4 == enumIndexCameraAlignUse)
                //    {
                //        if (true == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "-1 0 0\r\n 0 1 0\r\n 1 0 -1\r\n 0 0 1";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "-1 0 0 \r\n0 0.5 0\r\n 1 0 -1\r\n 0 0.5 1";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "-1 0 0 \r\n0 0 0\r\n 1 1 -1\r\n 0 0 1";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "-1 0 0\r\n 0 1 0\r\n 0.5 0 -1\r\n 0.5 0 1";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "-1 0 0 \r\n0 0.5 0\r\n 0.5 0 -1\r\n 0.5 0.5 1";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "-1 0 0\r\n 0 0 0\r\n 0.5 1 -1 \r\n0.5 0 1";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = "-1 0 0 \r\n1 1 0\r\n 0 0 -1\r\n 0 0 1";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = "-1 0 0\r\n 1 0.5 0\r\n 0 0 -1 \r\n0 0.5 1";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = "-1 0 0\r\n 0 0 0\r\n 0 0 -1 \r\n1 1 1";
                //        }
                //        else if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 0 \r\n-1 0 0 \r\n0 0 -1 \r\n0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0.5 0\r\n -1 0 0\r\n 1 0.5 -1 \r\n0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 0 \r\n-1 0 0\r\n 1 1 -1 \r\n0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0 1 0 \r\n-1 0 0\r\n 0.5 0 -1\r\n 0.5 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0.5 0\r\n -1 0 0 \r\n0.5 0.5 -1 \r\n0.5 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 0 \r\n-1 0 0 \r\n0.5 1 -1 \r\n0.5 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0 1 0 \r\n-1 0 0\r\n 0 0 -1\r\n 1 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0.5 0\r\n -1 0 0\r\n 0 0.5 -1\r\n 1 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 0 \r\n-1 0 0\r\n 0 1 -1\r\n 1 0 1 ";
                //        }
                //        else if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1 \r\n0 0 1 \r\n-1 0 0 \r\n0 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 1 0 -1\r\n 0 0.5 1\r\n -1 0 0 \r\n0 0.5 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 1 0 -1 \r\n0 0 1 \r\n-1 0 0 \r\n0 1 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0.5 1 -1\r\n 0.5 0 1\r\n -1 0 0\r\n 0 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0 -1 \r\n0.5 0.5 1\r\n -1 0 0 \r\n0 0.5 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0.5 0 -1\r\n 0.5 0 1\r\n -1 0 0\r\n 0 1 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0 1 -1\r\n 1 0 1 \r\n-1 0 0 \r\n0 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n1 0.5 1 \r\n-1 0 0\r\n 0 0.5 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n0 0 1\r\n -1 0 0\r\n 1 1 0 ";
                //        }
                //        else if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1 \r\n0 0 1\r\n 0 0 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 1 0.5 -1 \r\n0 0 1\r\n 0 0.5 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n0 0 1 \r\n1 1 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0.5 1 -1 \r\n0.5 0 1\r\n 0 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n 0.5 0 1\r\n 0 0.5 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0.5 0 -1 \r\n0.5 0 1 \r\n0 1 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0 1 -1\r\n 1 0 1\r\n 0 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0.5 -1\r\n 1 0 1 \r\n0 0.5 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n1 0 1\r\n 0 1 0 \r\n-1 0 0 ";
                //        }
                //        else if (true == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0\r\n -1 0 0\r\n 1 1 -1\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0 \r\n0.5 0.5 -1 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0\r\n 0 0 -1\r\n 1 1 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0\r\n -1 0 0\r\n 1 1 -1\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0 \r\n0.5 0.5 -1 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0\r\n 0 0 -1\r\n 1 1 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0\r\n -1 0 0\r\n 1 1 -1\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0 \r\n0.5 0.5 -1 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n-1 0 0\r\n 0 0 -1\r\n 1 1 1 ";

                //        }
                //        else if (true == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n1 1 -1\r\n -1 0 0\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0.5 0.5 -1 \r\n-1 0 0 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0 0 -1\r\n -1 0 0\r\n 1 1 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n1 1 -1\r\n -1 0 0\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0.5 0.5 -1 \r\n-1 0 0 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0 0 -1\r\n -1 0 0\r\n 1 1 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n1 1 -1\r\n -1 0 0\r\n 0 0 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0.5 0.5 -1 \r\n-1 0 0 \r\n0.5 0.5 1 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " -1 0 0 \r\n0 0 -1\r\n -1 0 0\r\n 1 1 1 ";
                //        }
                //        else if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1\r\n -1 0 0 \r\n0 0 1 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n -1 0 0\r\n 0.5 0.5 1\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n-1 0 0\r\n 1 1 1 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1\r\n -1 0 0 \r\n0 0 1 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n -1 0 0\r\n 0.5 0.5 1\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n-1 0 0\r\n 1 1 1 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1\r\n -1 0 0 \r\n0 0 1 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n -1 0 0\r\n 0.5 0.5 1\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n-1 0 0\r\n 1 1 1 \r\n-1 0 0 ";
                //        }
                //        else if (false == bNotFoundCheck[(int)enumCameraNo.CAMERA_1] && false == bNotFoundCheck[(int)enumCameraNo.CAMERA_2] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_3] && true == bNotFoundCheck[(int)enumCameraNo.CAMERA_4])
                //        {
                //            if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 1 1 -1 \r\n0 0 1\r\n -1 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n 0.5 0.5 1\r\n -1 0 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_TOP == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n1 1 1\r\n -1 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 1 1 -1 \r\n0 0 1\r\n -1 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n 0.5 0.5 1\r\n -1 0 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_BOT == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n1 1 1\r\n -1 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_LEFT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 1 1 -1 \r\n0 0 1\r\n -1 0 0 \r\n-1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_CENTER == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0.5 0.5 -1\r\n 0.5 0.5 1\r\n -1 0 0\r\n -1 0 0 ";
                //            else if (enumAlignmentHorizon.HORIZON_RIGHT == enumIndexAlignmentHorizon && enumAlignmentVertical.VERTICAL_CENTER == enumIndexAlignmentVertical) strReturn = " 0 0 -1 \r\n1 1 1\r\n -1 0 0 \r\n-1 0 0 ";
                //        }
                //    }

                //    SetAlignmentPositionEx(enumIndexStage, enumIndexCameraAlignUse, strReturn);

                //}
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error ParsingAlignment : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 카메라 인덱스 Parsing
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string ParsingCamera(enumCameraAlignUse enumIndexCameraAlignUse)
        {
            string strReturn = "";

            try
            {
                if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse) strReturn = "";
                else if (enumCameraAlignUse.CAMERA_USE_2 == enumIndexCameraAlignUse) strReturn = "0 1";
                else if (enumCameraAlignUse.CAMERA_USE_3 == enumIndexCameraAlignUse) strReturn = "0 1 2";
                else if (enumCameraAlignUse.CAMERA_USE_4 == enumIndexCameraAlignUse) strReturn = "0 1 2 3";
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error ParsingCamera : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Goal Position Parsing
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public string ParsingGoalPosition(enumCameraAlignUse enumIndexCameraAlignUse)
        {
            string strReturn = "";

            try
            {
                if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse) strReturn = "0";
                else if (enumCameraAlignUse.CAMERA_USE_2 == enumIndexCameraAlignUse) strReturn = "0 0";
                else if (enumCameraAlignUse.CAMERA_USE_3 == enumIndexCameraAlignUse) strReturn = "0 0 0";
                else if (enumCameraAlignUse.CAMERA_USE_4 == enumIndexCameraAlignUse) strReturn = "0 0 0 0";
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error ParsingGoalPosition : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Align Run
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool RunAlignXYT(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse, double[] dPixelPositionX, double[] dPixelPositionY, ref double dX, ref double dY, ref double dT)
        {
            bool bReturn = false;

            do
            {
                string strAlignCamera = ParsingCamera(enumIndexCameraAlignUse);
                string strGoalPosition = ParsingGoalPosition(enumIndexCameraAlignUse);
                string strCurrentPosition = "";

                if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format
                    (
                        "{0:F3} {1:F3}",
                        dPixelPositionX[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_1]
                    );
                }
                else if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format
                    (
                        "{0:F3} {1:F3} {2:F3} {3:F3}",
                        dPixelPositionX[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_2],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_2]
                    );
                }
                else if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format
                    (
                        "{0:F3} {1:F3} {2:F3} {3:F3} {4:F3} {5:F3}",
                        dPixelPositionX[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_2],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_2],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_3],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_3]
                    );
                }
                else if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format
                    (
                        "{0:F3} {1:F3} {2:F3} {3:F3} {4:F3} {5:F3} {6:F3} {7:F3} ",
                        dPixelPositionX[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_1],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_2],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_2],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_3],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_3],
                        dPixelPositionX[(int)enumCameraNo.CAMERA_4],
                        dPixelPositionY[(int)enumCameraNo.CAMERA_4]
                    );
                }


                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Align Run
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool RunAlignUVW(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse, enumAlignmentHorizon enumIndexHorizon, enumAlignmentVertical enumIndexVertical, double[] dPixelPositionX, double[] dPixelPositionY, ref double dU, ref double dV, ref double dW)
        {
            bool bReturn = false;

            do
            {
                string strAlignCamera = ParsingCamera(enumIndexCameraAlignUse);
                string strGoalPosition = ParsingGoalPosition(enumIndexCameraAlignUse);
                string strAlignment = ParsingAlignment(enumIndexStage, enumIndexCameraAlignUse, enumIndexHorizon, enumIndexVertical, dPixelPositionX, dPixelPositionY);
                string strCurrentPosition = "";
                List<int> objAlignCamera = new List<int>(); List<int> objGoalPosition = new List<int>(); List<List<double>> objCurrentPosition = new List<List<double>>();
                List<double> objRevisionData = new List<double>();

                if (enumCameraAlignUse.CAMERA_USE_1 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format("{0:F3} {1:F3}", dPixelPositionX[(int)enumCameraNo.CAMERA_1], dPixelPositionY[(int)enumCameraNo.CAMERA_1]);
                }
                else if (enumCameraAlignUse.CAMERA_USE_2 == enumIndexCameraAlignUse)
                {
                    strCurrentPosition = String.Format("{0:F3} {1:F3} {2:F3} {3:F3}", dPixelPositionX[(int)enumCameraNo.CAMERA_1], dPixelPositionY[(int)enumCameraNo.CAMERA_1], dPixelPositionX[(int)enumCameraNo.CAMERA_2], dPixelPositionY[(int)enumCameraNo.CAMERA_2]);
                }
                else if (enumCameraAlignUse.CAMERA_USE_3 == enumIndexCameraAlignUse) strCurrentPosition = String.Format("{0:F3} {1:F3} {2:F3} {3:F3} {4:F3} {5:F3}", dPixelPositionX[(int)enumCameraNo.CAMERA_1], dPixelPositionY[(int)enumCameraNo.CAMERA_1], dPixelPositionX[(int)enumCameraNo.CAMERA_2], dPixelPositionY[(int)enumCameraNo.CAMERA_2], dPixelPositionX[(int)enumCameraNo.CAMERA_3], dPixelPositionY[(int)enumCameraNo.CAMERA_3]);
                else if (enumCameraAlignUse.CAMERA_USE_4 == enumIndexCameraAlignUse) strCurrentPosition = String.Format("{0:F3} {1:F3} {2:F3} {3:F3} {4:F3} {5:F3} {6:F3} {7:F3} ", dPixelPositionX[(int)enumCameraNo.CAMERA_1], dPixelPositionY[(int)enumCameraNo.CAMERA_1], dPixelPositionX[(int)enumCameraNo.CAMERA_2], dPixelPositionY[(int)enumCameraNo.CAMERA_2], dPixelPositionX[(int)enumCameraNo.CAMERA_3], dPixelPositionY[(int)enumCameraNo.CAMERA_3], dPixelPositionX[(int)enumCameraNo.CAMERA_4], dPixelPositionY[(int)enumCameraNo.CAMERA_4]);

                strCurrentPosition = strCurrentPosition.Replace("-1.000", "");

                try
                {
                    objAlignCamera = GetStringToArray(strAlignCamera); objGoalPosition = GetStringToArray(strGoalPosition); objCurrentPosition = GetMatrix2x(strCurrentPosition);
                    if (false == m_objSmartAlign.SmalAlign((int)enumIndexStage, objAlignCamera, objGoalPosition, objCurrentPosition, objRevisionData)) break;

                    dU = objRevisionData[(int)enumUVWStage.UVW_U]; dV = objRevisionData[(int)enumUVWStage.UVW_V]; dW = objRevisionData[(int)enumUVWStage.UVW_W];
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error RunAlignUVW : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Run Calibrate
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool RunCalibrate(enumStageNo enumIndexStage)
        {
            bool bReturn = false;

            do
            {
                List<List<double>> objData = new List<List<double>>();

                //전치
                objData = GetTranspose(m_objPixelPosition[(int)enumIndexStage]);
                if (false == m_objSmartAlign.SmalCalibrate((int)enumIndexStage, objData)) break;

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 카메라 정상동작 시 정렬 방법 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetAlignmentPosition(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse, string strAlignment)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    List<List<double>> objList = new List<List<double>>(); List<List<double>> objTranspose = new List<List<double>>();
                    objList = GetMatrixDouble(strAlignment);
                    objTranspose = GetTranspose(objList);

                    if (false == m_objSmartAlign.SmalSetAlignWeight((int)enumIndexStage, (int)enumIndexCameraAlignUse, objTranspose)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetAlignmentPosition : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
       * 제목 : 카메라 인식 실패 시 정렬 방법 설정 
       * 인자 : 
       * 리턴 : 
       * 설명 : 
       ******************************************************************************************************************************************************************/
        public bool SetAlignmentPositionEx(enumStageNo enumIndexStage, enumCameraAlignUse enumIndexCameraAlignUse, string strAlignment)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    m_objSmartAlign.SmalSetNumWeightCF((int)enumIndexStage, 1);

                    List<List<double>> objList = new List<List<double>>(); List<List<double>> objTranspose = new List<List<double>>();
                    objList = GetMatrixDouble(strAlignment);
                    objTranspose = GetTranspose(objList);

                    if (false == m_objSmartAlign.SmalSetAlignWeightCF((int)enumIndexStage, 0, objTranspose)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetAlignmentPositionEx : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 카메라 인식 실패 시 정렬 방법 설정 
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetCameraFailIndex(enumStageNo enumIndexStage, bool[] bNotFoundCheck)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    List<int> iCameraFail = new List<int>();
                    string strData = String.Format("{0} {1} {2} {3}",
                        false == bNotFoundCheck[0] ? 1 : -1,
                        false == bNotFoundCheck[1] ? 1 : -1,
                        false == bNotFoundCheck[2] ? 1 : -1,
                        false == bNotFoundCheck[3] ? 1 : -1
                        );

                    iCameraFail = GetStringToArray(strData);

                    m_objSmartAlign.SmalSetCameraFailData((int)enumIndexStage, iCameraFail);
                }
                catch (System.Exception ex)
                {
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }


        /******************************************************************************************************************************************************************
        * 제목 : Calibration Position 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetCalibratePosition(enumStageNo enumIndexStage, double dSectionXY, int iPointXY, double dSectionT, int iSectionT)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    if (false == m_objSmartAlign.SmalSetCalibPosParams((int)enumIndexStage, dSectionXY, iPointXY, dSectionT, iSectionT)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetCalibratePosition : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Goal Position 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetGoalPosition(enumStageNo enumIndexStage, enumCameraNo enumIndexCameraNo, string strGoalPosition)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    List<List<double>> objList = new List<List<double>>();
                    List<List<double>> objTranspose = new List<List<double>>();

                    objList = GetMatrixDouble(strGoalPosition);
                    objTranspose = GetTranspose(objList);

                    if (false == m_objSmartAlign.SmalSetGoalPos((int)enumIndexStage, (int)enumIndexCameraNo, objTranspose)) break;

                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetGoalPosition : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 캘리브레이션에 사용할 카메라 수 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetCalibrationCameraCount(enumStageNo enumIndexStage, int iCalibrationCameraCount)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    if (false == m_objSmartAlign.SmalSetNumCameras((int)enumIndexStage, iCalibrationCameraCount)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetCalibrationCameraCount : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Align에 사용할 카메라 수 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetAlignUseCameraCount(enumStageNo enumIndexStage, int iAlignUseCameraCount)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    if (false == m_objSmartAlign.SmalSetMaxNumViews((int)enumIndexStage, iAlignUseCameraCount)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetAlignUseCameraCount : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 스테이지 수 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetMaxStageCount(int iMaxStageCount)
        {
            bool bReturn = false;

            do
            {
                if (false == m_objSmartAlign.SmalSetNumStage(iMaxStageCount)) break;

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Stage Type 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetStageType(enumStageNo enumIndexStage, enumStageType enumIndexStageType)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    if (false == m_objSmartAlign.SmalSetStageType((int)enumIndexStage, (int)enumIndexStageType)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetStageType : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : UVW 스테이지 파라미터 설정
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SetUVWStageParameter(enumStageNo enumIndexStage, double dRadius, double[] dBearingAngle, double[] dSlideDirection)
        {
            bool bReturn = false;

            do
            {
                List<double> objBearingAngle = new List<double>(); List<double> objSlideDirection = new List<double>();

                try
                {
                    objBearingAngle.Add(dBearingAngle[(int)enumUVWStage.UVW_U]);
                    objBearingAngle.Add(dBearingAngle[(int)enumUVWStage.UVW_V]);
                    objBearingAngle.Add(dBearingAngle[(int)enumUVWStage.UVW_W]);

                    objSlideDirection.Add(dSlideDirection[(int)enumUVWStage.UVW_U]);
                    objSlideDirection.Add(dSlideDirection[(int)enumUVWStage.UVW_V]);
                    objSlideDirection.Add(dSlideDirection[(int)enumUVWStage.UVW_W]);

                    if (false == m_objSmartAlign.SmalSetUvwStageParams((int)enumIndexStage, dRadius, objBearingAngle, objSlideDirection)) break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SetUVWStageParameter : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : Calibration Position Update
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public void UpdateCalibrationPosition(enumStageNo enumIndexStage, enumCameraNo enumIndexCameraNo, int iIndex, double[] dPositionX, double[] dPositionY)
        {
            string strData = "";
            List<double> objData = new List<double>();
            if (0 == iIndex)
            {
                m_objPixelPosition[(int)enumIndexStage].Clear();
            }

            if (enumCameraNo.CAMERA_1 <= enumIndexCameraNo)
            {
                objData.Add(dPositionX[0]); objData.Add(dPositionY[0]);
            }

            if (enumCameraNo.CAMERA_2 <= enumIndexCameraNo)
            {
                objData.Add(dPositionX[1]); objData.Add(dPositionY[1]);
            }

            if (enumCameraNo.CAMERA_3 <= enumIndexCameraNo)
            {
                objData.Add(dPositionX[2]); objData.Add(dPositionY[2]);
            }

            if (enumCameraNo.CAMERA_4 <= enumIndexCameraNo)
            {
                objData.Add(dPositionX[3]); objData.Add(dPositionY[3]);
            }

            m_objPixelPosition[(int)enumIndexStage].Add(objData);
        }

        /******************************************************************************************************************************************************************
        * 제목 : 데이터 치환
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        private List<List<double>> GetTranspose(List<List<double>> objMatrix)
        {
            List<List<double>> objList = new List<List<double>>();

            int iMaxRowCount, iMaxColCount, iLoopRow, iLoopCol;
            iMaxRowCount = iMaxColCount = iLoopRow = iLoopCol = 0;

            try
            {
                //Max Row
                iMaxRowCount = objMatrix.Count;

                for (iLoopRow = 0; iLoopRow < iMaxRowCount; iLoopRow++)
                {
                    ////Max Col
                    //iMaxColCount = objMatrix[iLoopRow].Count;

                    //최초 진입 시 메모리 생성
                    if (0 == iLoopRow)
                    {
                        iMaxColCount = objMatrix[0].Count;

                        for (iLoopCol = 0; iLoopCol < iMaxColCount; iLoopCol++)
                        {
                            List<double> objAdd = new List<double>();

                            for (int iLoopAdd = 0; iLoopAdd < iMaxRowCount; iLoopAdd++)
                            {
                                objAdd.Add(0.0);
                            }

                            objList.Add(objAdd);
                        }
                    }

                    for (iLoopCol = 0; iLoopCol < iMaxColCount; iLoopCol++)
                    {
                        //치환
                        objList[iLoopCol][iLoopRow] = objMatrix[iLoopRow][iLoopCol];
                    }
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetTranspose : " + ex.Message + " ->" + ex.StackTrace);
            }

            return objList;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 문자열 변환
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        private string GetListToString(List<List<double>> objMatrix)
        {
            string strReturn = null;
            int iMaxRowCount, iMaxColCount, iLoopRow, iLoopCol;
            iMaxRowCount = iMaxColCount = iLoopRow = iLoopCol = 0;

            try
            {
                //Max Row
                iMaxRowCount = objMatrix.Count;

                for (iLoopRow = 0; iLoopRow < iMaxRowCount; iLoopRow++)
                {
                    //Max Col
                    iMaxColCount = objMatrix[iLoopRow].Count;

                    for (iLoopCol = 0; iLoopCol < iMaxColCount; iLoopCol++)
                    {
                        strReturn += objMatrix[iLoopRow][iLoopCol].ToString("0.000");
                        //공백 추가
                        if (iLoopCol < iMaxColCount - 1) strReturn += " ";
                    }
                    //줄바꿈
                    if (iLoopRow < iMaxRowCount - 1) strReturn += "\r\n";
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("Error GetListToString : " + ex.Message + " ->" + ex.StackTrace);
            }

            return strReturn;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 설정 파일 불러오기
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool LoadFile(string strRecipeName)
        {
            bool bReturn = false;

            do
            {
                string strFileName = "";

                try
                {
                    for (int iLoopCount = 0; iLoopCount < m_iStageCount; iLoopCount++)
                    {
                        strFileName = String.Format("{0}smal {1}.slc", m_strRecipePath + strRecipeName + "\\", iLoopCount);
                        m_objSmartAlign.SmalReadConfigFile(iLoopCount, strFileName);
                    }
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error LoadFile : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }


        public bool LoadAlignFile(string StrPath)
        {
            bool BResult = false;

            do
            {
                string strFileName = "";

                try
                {
                    for (int iLoopCount = 0; iLoopCount < m_iStageCount; iLoopCount++)
                    {
                        strFileName = String.Format("{0}smal {1}.slc", StrPath + "\\", iLoopCount);
                        if (File.Exists(strFileName) == true)
                        {
                            m_objSmartAlign.SmalReadConfigFile(iLoopCount, strFileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error LoadFile : " + ex.Message + " -> " + ex.StackTrace);
                    break;
                }

                BResult = true;
            }
            while (false);

            return BResult;
        }

        /******************************************************************************************************************************************************************
        * 제목 : 설정 파일 저장
        * 인자 : 
        * 리턴 : 
        * 설명 : 
        ******************************************************************************************************************************************************************/
        public bool SaveFile(string strRecipeName)
        {
            bool bReturn = false;

            do
            {
                string strFileName = "";

                try
                {
                    for (int iLoopCount = 0; iLoopCount < m_iStageCount; iLoopCount++)
                    {
                        strFileName = String.Format("{0}\\smal {1}.slc", m_strRecipePath + "\\" + strRecipeName, iLoopCount);
                        m_objSmartAlign.SmalWriteConfigFile(iLoopCount, strFileName);
                    }
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SaveFile : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }


        public bool SaveAlignFile(string StrPath)
        {
            bool bReturn = false;

            do
            {
                string strFileName = "";

                try
                {
                    for (int iLoopCount = 0; iLoopCount < m_iStageCount; iLoopCount++)
                    {
                        strFileName = String.Format("{0}\\smal {1}.slc", StrPath, iLoopCount);
                        m_objSmartAlign.SmalWriteConfigFile(iLoopCount, strFileName);
                    }
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SaveFile : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;
            } while (false);

            return bReturn;
        }


        public bool SaveFile(enumStageNo EStage, string StrDirectory)
        {
            bool bReturn = false;

            do
            {
                try
                {
                    string StrFile = String.Format("{0}\\smal {1}.slc", StrDirectory, (int)EStage);
                    m_objSmartAlign.SmalWriteConfigFile((int)EStage, StrFile);
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Error SaveFile : " + ex.Message + " ->" + ex.StackTrace);
                    break;
                }

                bReturn = true;

            } while (false);

            return bReturn;
        }



        public bool LoadFile(enumStageNo EStage, string StrDirectory)
        {
            bool BResult = false;

            do
            {
                try
                {
                    string StrFile = String.Format("{0}\\smal {1}.slc", StrDirectory, (int)EStage);

                    if (File.Exists(StrFile) == true)
                    {
                        m_objSmartAlign.SmalReadConfigFile((int)EStage, StrFile);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error LoadFile : " + ex.Message + " -> " + ex.StackTrace);
                    break;
                }
            }
            while (false);

            return BResult;
        }


        public void SetCameraOffset(enumStageNo enumIndexStage, enumCameraNo enumIndexCameraNo, List<double> LstF64Offset)
        {
            try
            {
                this.m_objSmartAlign.SmalSetCameraOffset((int)enumIndexStage, (int)enumIndexCameraNo, LstF64Offset);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error LoadFile : " + ex.Message + " -> " + ex.StackTrace);
            }
        }
    }
}
