using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public enum Flags
{
    mellotron,
    synth,
    lofiRhythm1,
    lofiRhythym2,
    CAC,
    CDeCsus,
    lowpass,
    unreachable
}

// static public string labelMaker(Flags flag) {
//     switch (flag) {
//         case mellotron:
//             return "Synthesiser (Mellotron)";
//             break;
//         case synth:
//             return "Synthesiser (Soft Bells)";
//             break;
//         case lofiRhythm1:
//             return "Slow, Lofi Rhythym";
//             break;
//         case lofiRhythym2:
//             return "Fast, Lofi Rhythym";
//             break;
//         case CAC:
//             return "Progression (CAC)";
//             break;
//         case CDeCsus:
//             return "Progression (CDeCsus)";
//             break;
//         case lowpass:
//             return "Lowpass Filter";
//             break;
//     }
// }