using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils
{
    internal static class Hash
    {
        public static byte[] Compute(HashAlgorithm algorithm, byte[] data)
        {
            using (algorithm)
                return algorithm.ComputeHash(data);
        }
    }
}
