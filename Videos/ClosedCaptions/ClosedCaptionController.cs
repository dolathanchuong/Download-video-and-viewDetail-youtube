using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Bridge;
using youtube369.Utils;
using youtube369.Utils.Extensions;

namespace youtube369.Videos.ClosedCaptions
{
    internal class ClosedCaptionController(HttpClient http) : VideoController(http)
    {
        public async ValueTask<ClosedCaptionTrackResponse> GetClosedCaptionTrackResponseAsync(
            string url,
            CancellationToken cancellationToken = default
        )
        {
            // Enforce known format
            var urlWithFormat = url.Pipe(s => UrlEx.SetQueryParameter(s, "format", "3"))
                .Pipe(s => UrlEx.SetQueryParameter(s, "fmt", "3"));

            return ClosedCaptionTrackResponse.Parse(
                await Http.GetStringAsync(urlWithFormat, cancellationToken)
            );
        }
    }
}
