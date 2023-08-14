using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PylonC.NET;
using Cognex.VisionPro;
using Jhjo.Common;

namespace FourHeadScriber
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool BPatternExist = CogMisc.IsLicensedFeatureEnabled("VxPatMax");
                bool BCaliperExist = CogMisc.IsLicensedFeatureEnabled("VxCaliper");
                if ((BPatternExist == false) || (BCaliperExist == false))
                {
                    CMsgBox.Warning("Please connect Cognex License!");
                    return;
                }


                Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "1000" /*ms*/);
                Pylon.Initialize();
                

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                frmLoad OWindow = new frmLoad();
                if (OWindow.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
    }
}
