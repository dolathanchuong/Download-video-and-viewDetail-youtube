﻿using Lazy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using youtube369.Utils;
using youtube369.Utils.Extensions;

namespace youtube369.Bridge
{
    internal partial class PlaylistBrowseResponse(JsonElement content) : IPlaylistData
    {
        [Lazy]
        private JsonElement? Sidebar =>
            content
                .GetPropertyOrNull("sidebar")
                ?.GetPropertyOrNull("playlistSidebarRenderer")
                ?.GetPropertyOrNull("items");

        [Lazy]
        private JsonElement? SidebarPrimary =>
            Sidebar
                ?.EnumerateArrayOrNull()
                ?.ElementAtOrNull(0)
                ?.GetPropertyOrNull("playlistSidebarPrimaryInfoRenderer");

        [Lazy]
        private JsonElement? SidebarSecondary =>
            Sidebar
                ?.EnumerateArrayOrNull()
                ?.ElementAtOrNull(1)
                ?.GetPropertyOrNull("playlistSidebarSecondaryInfoRenderer");

        [Lazy]
        public bool IsAvailable => Sidebar is not null;

        [Lazy]
        public string? Title =>
            SidebarPrimary
                ?.GetPropertyOrNull("title")
                ?.GetPropertyOrNull("simpleText")
                ?.GetStringOrNull()
            ?? SidebarPrimary
                ?.GetPropertyOrNull("title")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
                .WhereNotNull()
                .ConcatToString();

        [Lazy]
        private JsonElement? AuthorDetails =>
            SidebarSecondary?.GetPropertyOrNull("videoOwner")?.GetPropertyOrNull("videoOwnerRenderer");

        [Lazy]
        public string? Author =>
            AuthorDetails
                ?.GetPropertyOrNull("title")
                ?.GetPropertyOrNull("simpleText")
                ?.GetStringOrNull()
            ?? AuthorDetails
                ?.GetPropertyOrNull("title")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
                .WhereNotNull()
                .ConcatToString();

        [Lazy]
        public string? ChannelId =>
            AuthorDetails
                ?.GetPropertyOrNull("navigationEndpoint")
                ?.GetPropertyOrNull("browseEndpoint")
                ?.GetPropertyOrNull("browseId")
                ?.GetStringOrNull();

        [Lazy]
        public string? Description =>
            SidebarPrimary
                ?.GetPropertyOrNull("description")
                ?.GetPropertyOrNull("simpleText")
                ?.GetStringOrNull()
            ?? SidebarPrimary
                ?.GetPropertyOrNull("description")
                ?.GetPropertyOrNull("runs")
                ?.EnumerateArrayOrNull()
                ?.Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
                .WhereNotNull()
                .ConcatToString();

        [Lazy]
        public IReadOnlyList<ThumbnailData> Thumbnails =>
            SidebarPrimary
                ?.GetPropertyOrNull("thumbnailRenderer")
                ?.GetPropertyOrNull("playlistVideoThumbnailRenderer")
                ?.GetPropertyOrNull("thumbnail")
                ?.GetPropertyOrNull("thumbnails")
                ?.EnumerateArrayOrNull()
                ?.Select(j => new ThumbnailData(j))
                .ToArray()
            ?? SidebarPrimary
                ?.GetPropertyOrNull("thumbnailRenderer")
                ?.GetPropertyOrNull("playlistCustomThumbnailRenderer")
                ?.GetPropertyOrNull("thumbnail")
                ?.GetPropertyOrNull("thumbnails")
                ?.EnumerateArrayOrNull()
                ?.Select(j => new ThumbnailData(j))
                .ToArray()
            ?? Array.Empty<ThumbnailData>();
    }

    internal partial class PlaylistBrowseResponse
    {
        public static PlaylistBrowseResponse Parse(string raw) => new(Json.Parse(raw));
    }

}
