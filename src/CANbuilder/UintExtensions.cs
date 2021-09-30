using System;

namespace CANbuilder
{
    public static class UIntExtensions
    {
        public static uint[] SetBit(this uint[] bits, int indexFromLeft, bool value)
        {
            return bits;
        }

        public static uint SetBit(this uint bits, int indexFromLeft, bool value)
        {
            if (indexFromLeft < 0) throw new ArgumentOutOfRangeException(nameof(indexFromLeft), indexFromLeft, "must be > 0");
            if (indexFromLeft > 31) throw new ArgumentOutOfRangeException(nameof(indexFromLeft), indexFromLeft, "must be < 32");

            var pointer = (uint)(0b_1000_0000_0000_0000_0000_0000_0000_0000 >> indexFromLeft);
            return value ? EnableBit(bits, pointer) : DisableBit(bits, pointer);
        }

        private static uint EnableBit(uint bits, uint pointer)
        {
            return (uint)(bits | pointer);
        }

        private static uint DisableBit(uint bits, uint pointer)
        {
            pointer = ~pointer;
            return (uint)(bits & pointer);
        }

        public static bool GetBit(this uint bits, int indexFromLeft)
        {
            var pointer = (uint)(0b_1000_0000_0000_0000_0000_0000_0000_0000 >> indexFromLeft);

            return (bits & pointer) == pointer;
        }
    }
}