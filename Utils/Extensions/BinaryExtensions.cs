using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils.Extensions
{
    internal static class BinaryExtensions
    {
        public static string ToHex(this byte[] data, bool isUpperCase = true)
        {
            var buffer = new StringBuilder(2 * data.Length);

            foreach (var b in data)
            {
                buffer.Append(b.ToString(isUpperCase ? "X2" : "x2", CultureInfo.InvariantCulture));
            }

            return buffer.ToString();
        }
    }
}
