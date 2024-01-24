using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils
{
    internal static class Http
    {
        private static readonly Lazy<HttpClient> HttpClientLazy = new(() => new HttpClient());

        public static HttpClient Client => HttpClientLazy.Value;
    }
}
