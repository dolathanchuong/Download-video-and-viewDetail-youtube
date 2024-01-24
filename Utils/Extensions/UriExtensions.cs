using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils.Extensions
{
    internal static class UriExtensions
    {
        public static string GetDomain(this Uri uri) => uri.Scheme + Uri.SchemeDelimiter + uri.Host;
    }
}
