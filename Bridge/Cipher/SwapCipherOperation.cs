using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Utils.Extensions;

namespace youtube369.Bridge.Cipher
{
    internal class SwapCipherOperation(int index) : ICipherOperation
    {
        public string Decipher(string input) => input.SwapChars(0, index);

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Swap ({index})";
    }
}
