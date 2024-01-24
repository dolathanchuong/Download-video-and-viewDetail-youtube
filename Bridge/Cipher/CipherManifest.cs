using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Bridge.Cipher
{
    internal class CipherManifest(string signatureTimestamp, IReadOnlyList<ICipherOperation> operations)
    {
        public string SignatureTimestamp { get; } = signatureTimestamp;

        public IReadOnlyList<ICipherOperation> Operations { get; } = operations;

        public string Decipher(string input) =>
            Operations.Aggregate(input, (acc, op) => op.Decipher(acc));
    }
}
