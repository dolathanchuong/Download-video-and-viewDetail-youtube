﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Common;
using youtube369.Videos;

namespace youtube369.Search
{
    /// <summary>
    /// Metadata associated with a YouTube video returned by a search query.
    /// </summary>
    public class VideoSearchResult : ISearchResult, IVideo
    {
        /// <inheritdoc />
        public VideoId Id { get; }

        /// <inheritdoc cref="IVideo.Url" />
        public string Url => $"https://www.youtube.com/watch?v={Id}";

        /// <inheritdoc cref="IVideo.Title" />
        public string Title { get; }

        /// <inheritdoc />
        public Author Author { get; }

        /// <inheritdoc />
        public TimeSpan? Duration { get; }

        /// <inheritdoc />
        public IReadOnlyList<Thumbnail> Thumbnails { get; }

        /// <summary>
        /// Initializes an instance of <see cref="VideoSearchResult" />.
        /// </summary>
        public VideoSearchResult(
            VideoId id,
            string title,
            Author author,
            TimeSpan? duration,
            IReadOnlyList<Thumbnail> thumbnails
        )
        {
            Id = id;
            Title = title;
            Author = author;
            Duration = duration;
            Thumbnails = thumbnails;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Video ({Title})";
    }

}
