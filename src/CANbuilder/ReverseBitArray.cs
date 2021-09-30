using System;
using System.Collections;
using System.Collections.Generic;

namespace CANbuilder
{
    /// <summary>
    /// Represents  bits taken from an underlying byte[] as collection of booleans.
    /// Index 0 points to the leftmost bit while Length -1 is the right mos bit of the byte[].
    /// The is the oppsite behavior as the frameworks <see cref="BitArray"/>
    /// </summary>
    public sealed class ReverseBitArray : IEnumerable<bool>
    {
        private readonly byte[] underlyingByteArray;

        public ReverseBitArray(byte[] underlyingByteArray)
        {
            this.underlyingByteArray = underlyingByteArray;
        }

        public bool this[int index]
        {
            get => this.Get(index);
            set => this.Set(index, value);
        }

        //TODO: perf: cache length!?
        public int Length => this.underlyingByteArray.Length * 8;

        public IEnumerator<bool> GetEnumerator() => new BitEnumerator(this);

        private bool Get(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), index, "must be > 0");
            if (index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index), index, "must be < Length");

            var byteNo = Math.DivRem(index, 8, out var bitIndex);

            return this.underlyingByteArray[byteNo].GetBit(bitIndex);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Set(int index, bool value)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), index, "must be > 0");
            if (index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index), index, "must be < Length");

            var byteNo = Math.DivRem(index, 8, out var bitIndex);

            this.underlyingByteArray[byteNo] = this.underlyingByteArray[byteNo].SetBit(bitIndex, value);
        }

        internal byte[] Bytes() => this.underlyingByteArray;

        private sealed class BitEnumerator : IEnumerator<bool>
        {
            private ReverseBitArray underlyingBitArray;
            private bool? current = null;
            private int bitNo = 0;

            public BitEnumerator(ReverseBitArray underlyingBitArray)
            {
                this.underlyingBitArray = underlyingBitArray;
            }

            public bool Current => this.current ?? throw new InvalidOperationException("MoveNext() first");

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
                this.underlyingBitArray = null;
            }

            public bool MoveNext()
            {
                if (this.current is not null)
                {
                    this.bitNo++;
                }

                if (this.bitNo >= this.underlyingBitArray.Length)
                    return false;

                this.current = this.underlyingBitArray[this.bitNo];
                return true;
            }

            public void Reset()
            {
                this.current = null;
                this.bitNo = 0;
            }
        }
    }
}