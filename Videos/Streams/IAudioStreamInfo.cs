using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Videos.Streams
{
    /// <summary>
    /// Metadata associated with a media stream that contains audio.
    /// </summary>
    public interface IAudioStreamInfo : IStreamInfo
    {
        /// <summary>
        /// Audio codec.
        /// </summary>
        string AudioCodec { get; }
    }
}
