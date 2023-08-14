using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.Caliper;
using Jhjo.Common;
using Cognex.VisionPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Cognex.VisionPro.ToolBlock;

namespace FourHeadScriber
{
    public class CAnalysisRecipe : IDisposable
    {
        #region CONST
        private const string MARK1_TOOL = "MARK1";
        private const string MARK2_TOOL = "MARK2";
        private const string MARK3_TOOL = "MARK3";
        private const string MARK4_TOOL = "MARK4";
        private const string MARK5_TOOL = "MARK5";
        private const string DIRECT_CUT_TOOL = "DIRECT_CUT";
        private const string CROSS_CUT_TOOL = "CROSS_CUT";
        #endregion


        #region VARIABLE
        private EVIEW m_EView = EVIEW.NONE;
        private string m_StrName = null;
        private string m_StrDirectory = string.Empty;

        private ESTD_POINT m_EStdPoint = ESTD_POINT.LEFT;
        private CogPMAlignTool m_OMarkTool1 = null;
        private CogPMAlignTool m_OMarkTool2 = null;
        private CogPMAlignTool m_OMarkTool3 = null;
        private CogPMAlignTool m_OMarkTool4 = null;
        private CogPMAlignTool m_OMarkTool5 = null;
        private CogCaliperTool m_ODirectCutTool = null;
        private CogCaliperTool m_OCrossCutTool = null;
        #endregion


        #region PROPERTIES
        public EVIEW EView
        {
            get { return this.m_EView; }
        }


        public string StrName
        {
            get { return this.m_StrName; }
        }


        public string StrDirectory
        {
            get { return this.m_StrDirectory; }
        }


        public ESTD_POINT EStdPoint
        {
            get { return this.m_EStdPoint; }
            set { this.m_EStdPoint = value; }
        }


        public CogPMAlignTool OMarkTool1
        {
            get { return this.m_OMarkTool1; }
            set { this.m_OMarkTool1 = value; }
        }


        public CogPMAlignTool OMarkTool2
        {
            get { return this.m_OMarkTool2; }
            set { this.m_OMarkTool2 = value; }
        }


        public CogPMAlignTool OMarkTool3
        {
            get { return this.m_OMarkTool3; }
            set { this.m_OMarkTool3 = value; }
        }


        public CogPMAlignTool OMarkTool4
        {
            get { return this.m_OMarkTool4; }
            set { this.m_OMarkTool4 = value; }
        }


        public CogPMAlignTool OMarkTool5
        {
            get { return this.m_OMarkTool5; }
            set { this.m_OMarkTool5 = value; }
        }


        public CogCaliperTool ODirectCutTool
        {
            get { return this.m_ODirectCutTool; }
            set { this.m_ODirectCutTool = value; }
        }


        public CogCaliperTool OCrossCutTool
        {
            get { return this.m_OCrossCutTool; }
            set { this.m_OCrossCutTool = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CAnalysisRecipe(EVIEW EView, string StrName, string StrDirectory)
        {
            try
            {
                this.m_EView = EView;
                this.m_StrName = StrName;
                this.m_StrDirectory = StrDirectory;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CAnalysisRecipe(CAnalysisRecipe OSource)
        {
            try
            {
                this.m_EView = OSource.m_EView;
                this.m_StrName = OSource.m_StrName;
                this.m_StrDirectory = OSource.m_StrDirectory;

                this.m_EStdPoint = OSource.m_EStdPoint;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CAnalysisRecipe()
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
        public void Create()
        {
            try
            {
                this.m_OMarkTool1 = new CogPMAlignTool();
                this.m_OMarkTool1.RunParams.CoarseAcceptThresholdEnabled = true;
                this.m_OMarkTool1.RunParams.CoarseAcceptThreshold = 0.6;
                this.m_OMarkTool1.RunParams.AcceptThreshold = 0.6;
                this.m_OMarkTool1.RunParams.ScoreUsingClutter = false;

                this.m_OMarkTool2 = new CogPMAlignTool();
                this.m_OMarkTool2.RunParams.CoarseAcceptThresholdEnabled = true;
                this.m_OMarkTool2.RunParams.CoarseAcceptThreshold = 0.6;
                this.m_OMarkTool2.RunParams.AcceptThreshold = 0.6;
                this.m_OMarkTool2.RunParams.ScoreUsingClutter = false;

                this.m_OMarkTool3 = new CogPMAlignTool();
                this.m_OMarkTool3.RunParams.CoarseAcceptThresholdEnabled = true;
                this.m_OMarkTool3.RunParams.CoarseAcceptThreshold = 0.6;
                this.m_OMarkTool3.RunParams.AcceptThreshold = 0.6;
                this.m_OMarkTool3.RunParams.ScoreUsingClutter = false;

                this.m_OMarkTool4 = new CogPMAlignTool();
                this.m_OMarkTool4.RunParams.CoarseAcceptThresholdEnabled = true;
                this.m_OMarkTool4.RunParams.CoarseAcceptThreshold = 0.6;
                this.m_OMarkTool4.RunParams.AcceptThreshold = 0.6;
                this.m_OMarkTool4.RunParams.ScoreUsingClutter = false;

                this.m_OMarkTool5 = new CogPMAlignTool();
                this.m_OMarkTool5.RunParams.CoarseAcceptThresholdEnabled = true;
                this.m_OMarkTool5.RunParams.CoarseAcceptThreshold = 0.6;
                this.m_OMarkTool5.RunParams.AcceptThreshold = 0.6;
                this.m_OMarkTool5.RunParams.ScoreUsingClutter = false;

                this.m_ODirectCutTool = new CogCaliperTool();
                this.m_ODirectCutTool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.Pair;
                this.m_ODirectCutTool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
                this.m_ODirectCutTool.RunParams.Edge0Position = -5;
                this.m_ODirectCutTool.RunParams.Edge1Polarity = CogCaliperPolarityConstants.DarkToLight;
                this.m_ODirectCutTool.RunParams.Edge1Position = 5;

                this.m_OCrossCutTool = new CogCaliperTool();
                this.m_OCrossCutTool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.Pair;
                this.m_OCrossCutTool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
                this.m_OCrossCutTool.RunParams.Edge0Position = -5;
                this.m_OCrossCutTool.RunParams.Edge1Polarity = CogCaliperPolarityConstants.DarkToLight;
                this.m_OCrossCutTool.RunParams.Edge1Position = 5;
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
                CogToolBlock OToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(this.m_StrDirectory + "\\" + this.m_EView.ToString() + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.All);

                this.m_OMarkTool1 = new CogPMAlignTool((CogPMAlignTool)OToolBlock.Tools[MARK1_TOOL]);
                this.m_OMarkTool2 = new CogPMAlignTool((CogPMAlignTool)OToolBlock.Tools[MARK2_TOOL]);
                this.m_OMarkTool3 = new CogPMAlignTool((CogPMAlignTool)OToolBlock.Tools[MARK3_TOOL]);
                this.m_OMarkTool4 = new CogPMAlignTool((CogPMAlignTool)OToolBlock.Tools[MARK4_TOOL]);
                this.m_OMarkTool5 = new CogPMAlignTool((CogPMAlignTool)OToolBlock.Tools[MARK5_TOOL]);
                this.m_ODirectCutTool = new CogCaliperTool((CogCaliperTool)OToolBlock.Tools[DIRECT_CUT_TOOL]);
                this.m_OCrossCutTool = new CogCaliperTool((CogCaliperTool)OToolBlock.Tools[CROSS_CUT_TOOL]);

                if (OToolBlock != null)
                {
                    OToolBlock.Dispose();
                    OToolBlock = null;
                }
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
                this.m_OMarkTool1.Name = MARK1_TOOL;
                this.m_OMarkTool2.Name = MARK2_TOOL;
                this.m_OMarkTool3.Name = MARK3_TOOL;
                this.m_OMarkTool4.Name = MARK4_TOOL;
                this.m_OMarkTool5.Name = MARK5_TOOL;
                this.m_ODirectCutTool.Name = DIRECT_CUT_TOOL;
                this.m_OCrossCutTool.Name = CROSS_CUT_TOOL;

                CogToolBlock OToolBlock = new CogToolBlock();
                OToolBlock.Tools.Add(this.m_OMarkTool1);
                OToolBlock.Tools.Add(this.m_OMarkTool2);
                OToolBlock.Tools.Add(this.m_OMarkTool3);
                OToolBlock.Tools.Add(this.m_OMarkTool4);
                OToolBlock.Tools.Add(this.m_OMarkTool5);
                OToolBlock.Tools.Add(this.m_ODirectCutTool);
                OToolBlock.Tools.Add(this.m_OCrossCutTool);

                Directory.CreateDirectory(this.m_StrDirectory);
                CogSerializer.SaveObjectToFile(OToolBlock, this.m_StrDirectory + "\\" + this.m_EView.ToString() + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.All);

                if (OToolBlock != null)
                {
                    OToolBlock.Dispose();
                    OToolBlock = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Copy(CAnalysisRecipe OSource)
        {
            try
            {
                this.m_EStdPoint = OSource.m_EStdPoint;
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
                if (this.m_OMarkTool1 != null)
                {
                    this.m_OMarkTool1.Dispose();
                    this.m_OMarkTool1 = null;
                }
                if (this.m_OMarkTool2 != null)
                {
                    this.m_OMarkTool2.Dispose();
                    this.m_OMarkTool2 = null;
                }
                if (this.m_OMarkTool3 != null)
                {
                    this.m_OMarkTool3.Dispose();
                    this.m_OMarkTool3 = null;
                }
                if (this.m_OMarkTool4 != null)
                {
                    this.m_OMarkTool4.Dispose();
                    this.m_OMarkTool4 = null;
                }
                if (this.m_OMarkTool5 != null)
                {
                    this.m_OMarkTool5.Dispose();
                    this.m_OMarkTool5 = null;
                }
                if (this.m_ODirectCutTool != null)
                {
                    this.m_ODirectCutTool.Dispose();
                    this.m_ODirectCutTool = null;
                }
                if (this.m_OCrossCutTool != null)
                {
                    this.m_OCrossCutTool.Dispose();
                    this.m_OCrossCutTool = null;
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
