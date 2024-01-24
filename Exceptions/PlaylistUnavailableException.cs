using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Exceptions
{
    /// <summary>
    /// Exception thrown when the requested playlist is unavailable.
    /// </summary>
    public class PlaylistUnavailableException : YoutubeExplodeException
    {
        /// <summary>
        /// Initializes an instance of <see cref="PlaylistUnavailableException" />.
        /// </summary>
        public PlaylistUnavailableException(string message)
            : base(message) { }
    }
}
