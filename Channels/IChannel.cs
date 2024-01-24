using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using youtube369.Common;

namespace youtube369.Channels
{
    public interface IChannel
    {
        /// <summary>
        /// Channel ID.
        /// </summary>
        ChannelId Id { get; }

        /// <summary>
        /// Channel URL.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Channel title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Channel thumbnails.
        /// </summary>
        IReadOnlyList<Thumbnail> Thumbnails { get; }
    }
}
