using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FourHeadScriber
{
    public class ActEasyIF2
    {
        public int ActLogicalStationNumber
        {
            get { return 0; }
            set { }
        }


        public int Open()
        {
            return 0;
        }


        public void Close()
        {
        }


        public void WriteDeviceBlock(string StrAddr, int I32Size, ref int I32Value)
        {
        }


        public void WriteDeviceBlock2(string StrAddr, int I32Size, ref short I16Value)
        {
        }


        public void ReadDeviceBlock(string StrAddr, int I32Size, out int I32Value)
        {
            I32Value = 0;
        }


        public int ReadDeviceBlock2(string StrAddr, int I32Size, out short I16Value)
        {
            I16Value = 0;
            return 0;
        }


        public void WriteDeviceRandom2(string StrAddr, int I32Size, ref short I16Value)
        {
            I16Value = 0;
        }



        public void ReadDeviceRandom2(string StrAddr, int I32Size, out short I16Value)
        {
            I16Value = 0;
        }

        public void ReadDeviceBlock2(string StrAddr, int I32Size, out ushort U16Value)
        {
            U16Value = 0;
        }

        public void WriteDeviceBlock2(string StrAddr, int I32Size, ref ushort I32Value)
        {
        }
    }
}
