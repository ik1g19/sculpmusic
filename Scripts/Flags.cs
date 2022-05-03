using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public enum Flags
{
    introriff,
    introriffdistortion,
    bass,
    clean1,
    clean2,
    bridge1,
    bridge2,
    end
}

// public class FlagHandling {
//     static public string labelMaker(Flags flag) {
//         switch (flag) {
//             case Flags.mellotron:
//                 return "Synthesiser (Mellotron)";
//                 break;
//             case Flags.synth:
//                 return "Synthesiser (Soft Bells)";
//                 break;
//             case Flags.lofiRhythm1:
//                 return "Slow, Lofi Rhythym";
//                 break;
//             case Flags.lofiRhythym2:
//                 return "Fast, Lofi Rhythym";
//                 break;
//             case Flags.CAC:
//                 return "Progression (CAC)";
//                 break;
//             case Flags.CDeCsus:
//                 return "Progression (CDeCsus)";
//                 break;
//             case Flags.lowpass:
//                 return "Lowpass Filter";
//                 break;
//             default:
//                 return "";
//                 break;
//         }
//     }
// }