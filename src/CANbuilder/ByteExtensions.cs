using System;

namespace CANbuilder
{
    public static class ByteExtensions
    {
        public static byte[] SetBit(this byte[] bits, int indexFromLeft, bool value)
        {
            return bits;
        }

        public static byte SetBit(this byte bits, int indexFromLeft, bool value)
        {
            if (indexFromLeft < 0) throw new ArgumentOutOfRangeException(nameof(indexFromLeft), indexFromLeft, "must be > 0");
            if (indexFromLeft > 7) throw new ArgumentOutOfRangeException(nameof(indexFromLeft), indexFromLeft, "must be < 8");

            var pointer = 0b_10000000 >> indexFromLeft;
            return value ? EnableBit(bits, pointer) : DisableBit(bits, pointer);
        }

        private static byte EnableBit(byte bits, int pointer)
        {
            return (byte)(bits | pointer);
        }

        private static byte DisableBit(byte bits, int pointer)
        {
            pointer = ~pointer;
            return (byte)(bits & pointer);
        }

        public static bool GetBit(this byte bits, int indexFromLeft)
        {
            var pointer = 0b_10000000 >> indexFromLeft;
            return (bits & pointer) == pointer;
        }
    }
}