using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Enums
{
    public enum Round
    {
        [Description("Qualification")]
        Qualification,

        [Description("Final")]
        Final,

        [Description("Small Final")]
        SmallFinal,

        [Description("Semi Final")]
        SemiFinal,

        [Description("Quarter Final")]
        QuarterFinal,

        [Description("Eighth Final")]
        EighthFinal,

        [Description("Sixteenth Final")]
        SixteenthFinal,

        [Description("Thirty Second Final")]
        ThirtySecondFinal,

        [Description("Sixty Fourth Final")]
        SixtyFourthFinal,

        [Description("Hundred Twenty Eighth Final")]
        HundredTwentyEighthFinal
    }
}
