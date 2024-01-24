using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Bridge
{
    internal interface IPlaylistData
    {
        string? Title { get; }

        string? Author { get; }

        string? ChannelId { get; }

        string? Description { get; }

        IReadOnlyList<ThumbnailData> Thumbnails { get; }
    }
}
