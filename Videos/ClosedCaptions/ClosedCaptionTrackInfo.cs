﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Videos.ClosedCaptions
{
    /// <summary>
    /// Metadata associated with a closed caption track of a YouTube video.
    /// </summary>
    public class ClosedCaptionTrackInfo
    {
        /// <summary>
        /// Track URL.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Track language.
        /// </summary>
        public Language Language { get; }

        /// <summary>
        /// Whether the track was automatically generated.
        /// </summary>
        public bool IsAutoGenerated { get; }

        /// <summary>
        /// Initializes an instance of <see cref="ClosedCaptionTrackInfo" />.
        /// </summary>
        public ClosedCaptionTrackInfo(string url, Language language, bool isAutoGenerated)
        {
            Url = url;
            Language = language;
            IsAutoGenerated = isAutoGenerated;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"CC Track ({Language})";
    }
}
