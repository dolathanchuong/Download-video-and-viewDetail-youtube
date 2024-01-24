using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Channels;

namespace youtube369.Common
{
    public class Author
    {
        /// <summary>
        /// Channel ID.
        /// </summary>
        public ChannelId ChannelId { get; }

        /// <summary>
        /// Channel URL.
        /// </summary>
        public string ChannelUrl => $"https://www.youtube.com/channel/{ChannelId}";

        /// <summary>
        /// Channel title.
        /// </summary>
        public string ChannelTitle { get; }

        /// <inheritdoc cref="ChannelTitle" />
        [Obsolete("Use ChannelTitle instead."), ExcludeFromCodeCoverage]
        public string Title => ChannelTitle;

        /// <summary>
        /// Initializes an instance of <see cref="Author" />.
        /// </summary>
        public Author(ChannelId channelId, string channelTitle)
        {
            ChannelId = channelId;
            ChannelTitle = channelTitle;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => ChannelTitle;
    }
}
