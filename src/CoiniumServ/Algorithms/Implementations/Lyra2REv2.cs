using System;
using System.Runtime.InteropServices;

namespace CoiniumServ.Algorithms.Implementations
{
    public sealed class Lyra2REv2 : IHashAlgorithm
    {
        public uint Multiplier { get; private set; }

        public Lyra2REv2()
        {
            Multiplier = (UInt32) Math.Pow(2, 8);
        }

        [DllImport("lyra2rev2.so")]
        static extern void lyra2rev2_hash(IntPtr state, IntPtr input);    
        
        public byte[] Hash(byte[] input)
        {
            unsafe
            {
                IntPtr ptr = Marshal.AllocHGlobal(32);
                fixed (byte* p = input)
                {
                    IntPtr inp = new IntPtr((void*) p);
                    lyra2rev2_hash(ptr, inp);
                }

                byte[] res = new byte[32];
                Marshal.Copy(ptr, res, 0, 32);
                return res;
            }
        }
    }
}