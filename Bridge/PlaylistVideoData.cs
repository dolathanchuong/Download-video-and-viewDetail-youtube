﻿using Lazy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using youtube369.Utils.Extensions;

namespace youtube369.Bridge
{
    internal class PlaylistVideoData(JsonElement content)
    {
        [Lazy]
        public int? Index =>
            content
                .GetPropertyOrNull("navigationEndpoint")
                ?.GetPropertyOrNull("watchEndpoint")
                ?.GetPropertyOrNull("index")
                ?.GetInt32OrNull();

        [Lazy]
        public string? Id => content.GetPropertyOrNull("videoId")?.GetStringOrNull();

        [Lazy]
        public string? Title =>
            content.GetPropertyOrNull("title")?.GetPropertyOrNull("simpleText")?.GetStringOrNull()
            ?? content
                .GetPropertyOrNull("title")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
                .WhereNotNull()
                .ConcatToString();

        [Lazy]
        private JsonElement? AuthorDetails =>
            content
                .GetPropertyOrNull("longBylineText")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.ElementAtOrNull(0)
            ?? content
                .GetPropertyOrNull("shortBylineText")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.ElementAtOrNull(0);

        [Lazy]
        public string? Author => AuthorDetails?.GetPropertyOrNull("text")?.GetStringOrNull();

        [Lazy]
        public string? ChannelId =>
            AuthorDetails
                ?.GetPropertyOrNull("navigationEndpoint")
                ?.GetPropertyOrNull("browseEndpoint")
                ?.GetPropertyOrNull("browseId")
                ?.GetStringOrNull();

        [Lazy]
        public TimeSpan? Duration =>
            content
                .GetPropertyOrNull("lengthSeconds")
                ?.GetStringOrNull()
                ?.ParseDoubleOrNull()
                ?.Pipe(TimeSpan.FromSeconds)
            ?? content
                .GetPropertyOrNull("lengthText")
                ?.GetPropertyOrNull("simpleText")
                ?.GetStringOrNull()
                ?.ParseTimeSpanOrNull(new[] { @"m\:ss", @"mm\:ss", @"h\:mm\:ss", @"hh\:mm\:ss" })
            ?? content
                .GetPropertyOrNull("lengthText")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
                .WhereNotNull()
                .ConcatToString()
                .ParseTimeSpanOrNull(new[] { @"m\:ss", @"mm\:ss", @"h\:mm\:ss", @"hh\:mm\:ss" });

        [Lazy]
        public IReadOnlyList<ThumbnailData> Thumbnails =>
            content
                .GetPropertyOrNull("thumbnail")
                ?.GetPropertyOrNull("thumbnails")
                ?.EnumerateArrayOrNull()
                ?.Select(j => new ThumbnailData(j))
                .ToArray() ?? Array.Empty<ThumbnailData>();
    }
}
