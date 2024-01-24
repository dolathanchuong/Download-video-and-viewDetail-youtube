using Lazy;
using System.Xml.Linq;
using youtube369.Utils;
using youtube369.Utils.Extensions;

namespace youtube369.Bridge
{
    internal partial class ClosedCaptionTrackResponse(XElement content)
    {
        [Lazy]
        public IReadOnlyList<CaptionData> Captions =>
            content.Descendants("p").Select(x => new CaptionData(x)).ToArray();
    }

    internal partial class ClosedCaptionTrackResponse
    {
        public class CaptionData(XElement content)
        {
            [Lazy]
            public string? Text => (string?)content;

            [Lazy]
            public TimeSpan? Offset =>
                ((double?)content.Attribute("t"))?.Pipe(TimeSpan.FromMilliseconds);

            [Lazy]
            public TimeSpan? Duration =>
                ((double?)content.Attribute("d"))?.Pipe(TimeSpan.FromMilliseconds);

            [Lazy]
            public IReadOnlyList<PartData> Parts =>
                content.Elements("s").Select(x => new PartData(x)).ToArray();
        }
    }

    internal partial class ClosedCaptionTrackResponse
    {
        public class PartData(XElement content)
        {
            [Lazy]
            public string? Text => (string?)content;

            [Lazy]
            public TimeSpan? Offset =>
                ((double?)content.Attribute("t"))?.Pipe(TimeSpan.FromMilliseconds)
                ?? ((double?)content.Attribute("ac"))?.Pipe(TimeSpan.FromMilliseconds)
                ?? TimeSpan.Zero;
        }
    }

    internal partial class ClosedCaptionTrackResponse
    {
        public static ClosedCaptionTrackResponse Parse(string raw) => new(Xml.Parse(raw));
    }
}
