using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Utils.Extensions;

namespace youtube369.Utils
{
    internal abstract class ClientDelegatingHandler(HttpClient http, bool disposeClient = false)
    : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            // Clone the request to reset its completion status, which is required
            // in order to pass the request from one HttpClient to another.
            using var clonedRequest = request.Clone();

            return await http.SendAsync(
                clonedRequest,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && disposeClient)
                http.Dispose();

            base.Dispose(disposing);
        }
    }
}
