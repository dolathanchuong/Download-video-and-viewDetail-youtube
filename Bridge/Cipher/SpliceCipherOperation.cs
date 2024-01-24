using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Bridge.Cipher
{
    internal class SpliceCipherOperation(int index) : ICipherOperation
    {
        public string Decipher(string input) => input[index..];

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Splice ({index})";
    }
}
