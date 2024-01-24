using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Bridge.Cipher
{
    internal class ReverseCipherOperation : ICipherOperation
    {
        public string Decipher(string input) => (string)input.Reverse();

        [ExcludeFromCodeCoverage]
        public override string ToString() => "Reverse";
    }
}
