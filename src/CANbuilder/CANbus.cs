using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANbuilder
{
    public static class CANbus
    {
        /// <summary>
        /// 11-bit can bus identifier mask.
        /// </summary>
        public const int CobId_11bit_Mask = 0x7FF;

        /// <summary>
        /// CANbus allows 8 bytes as payload of a CANbus telegram.
        /// </summary>
        public const byte MaxPayloadLength = 8;
    }
}
