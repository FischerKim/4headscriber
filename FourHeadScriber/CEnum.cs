using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FourHeadScriber
{
    public enum EANALYSIS : byte
    {
        NONE = 0x00,
        CALIBRATION,
        INSPECTION
    }


    public enum EEDIT : byte
    {
        NONE = 0,
        ADD,
        MODIFY,
        DELETE
    }


    public enum ESTAGE_ANGLE : byte
    {
        ZERO = 0,
        NINETY = 90,
        NONE = 0xFF
    }


    [Flags]
    public enum EVIEW : byte
    {
        NONE = 0x00,
        HEAD1 = 0x01,
        HEAD2 = 0x02,
        HEAD3 = 0x04,
        HEAD4 = 0x08
    }


    public enum ETOOL : byte
    {
        NONE = 0,
        MARK,
        DIRECT_CUT,
        CROSS_CUT
    }


    public enum EMARK : byte
    {
        NONE = 0,
        INDEX1,
        INDEX2,
        INDEX3,
        INDEX4,
        INDEX5
    }


    public enum ESTD_POINT : byte
    {
        LEFT = 0,
        RIGHT,
        NONE = 0xFF
    }


    public enum ERESULT : byte
    {
        OK,
        NG,
        RETRY,
        LEFT_NG,
        RIGHT_NG,
        NONE
    }


    public enum EMANUAL : byte
    {
        ALIGN_SHARP,
        ALIGN_X
    }


    public enum ESCREEN : byte
    {
        SCREEN1,
        SCREEN2
    }


    public enum EMOTION_INDEX
    {
        ALIGNMENT_CMD = 0,
        ALIGNMENT_IS_FIRST = 0,
        SCALE_CALCULATION = 0,
        CROSS_CUT_CMD = 1,
        DIRECT_CUT_CMD = 1,
        HEAD1_ZERO_ALIGNMENT_ENABLED = 1,
        HEAD2_ZERO_ALIGNMENT_ENABLED = 1,
        HEAD3_ZERO_ALIGNMENT_ENABLED = 2,
        HEAD4_ZERO_ALIGNMENT_ENABLED = 2,
        HEAD1_NINETY_ALIGNMENT_ENABLED = 2,
        HEAD2_NINETY_ALIGNMENT_ENABLED = 2,
        HEAD3_NINETY_ALIGNMENT_ENABLED = 2,
        HEAD4_NINETY_ALIGNMENT_ENABLED = 2,
        CALIBRATION_POSSIBLE = 3,
        CALIBRATION_IMPOSSIBLE = 3,
        CALIBRATION_MOVE_COMPLETED = 3
    }


    public enum EMOTION_BIT
    {
        ALIGNMENT_CMD = 0,
        ALIGNMENT_IS_FIRST = 1,
        SCALE_CALCULATION = 10,
        CROSS_CUT_CMD = 5,
        DIRECT_CUT_CMD = 8,
        HEAD1_ZERO_ALIGNMENT_ENABLED = 14,
        HEAD2_ZERO_ALIGNMENT_ENABLED = 15,
        HEAD3_ZERO_ALIGNMENT_ENABLED = 0,
        HEAD4_ZERO_ALIGNMENT_ENABLED = 1,
        HEAD1_NINETY_ALIGNMENT_ENABLED = 2,
        HEAD2_NINETY_ALIGNMENT_ENABLED = 3,
        HEAD3_NINETY_ALIGNMENT_ENABLED = 4,
        HEAD4_NINETY_ALIGNMENT_ENABLED = 5,
        CALIBRATION_POSSIBLE = 2,
        CALIBRATION_IMPOSSIBLE = 3,
        CALIBRATION_MOVE_COMPLETED = 4
    }
}
