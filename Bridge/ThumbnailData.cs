using Lazy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using youtube369.Utils.Extensions;

namespace youtube369.Bridge
{
    internal class ThumbnailData(JsonElement content)
    {
    [Lazy]
    public string? Url => content.GetPropertyOrNull("url")?.GetStringOrNull();

    [Lazy]
    public int? Width => content.GetPropertyOrNull("width")?.GetInt32OrNull();

    [Lazy]
    public int? Height => content.GetPropertyOrNull("height")?.GetInt32OrNull();
}
}
