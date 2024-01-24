using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Search
{
    /// <summary>
    /// Filter applied to a YouTube search query.
    /// </summary>
    public enum SearchFilter
    {
        /// <summary>
        /// No filter applied.
        /// </summary>
        None,

        /// <summary>
        /// Only search for videos.
        /// </summary>
        Video,

        /// <summary>
        /// Only search for playlists.
        /// </summary>
        Playlist,

        /// <summary>
        /// Only search for channels.
        /// </summary>
        Channel
    }
}
