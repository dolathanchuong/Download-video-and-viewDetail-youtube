﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Utils.Extensions;

namespace youtube369.Common
{
    /// <summary>
    /// Generic collection of items returned by a single request.
    /// </summary>
    public class Batch<T>
        where T : IBatchItem
    {
        /// <summary>
        /// Items included in the batch.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Initializes an instance of <see cref="Batch{T}" />.
        /// </summary>
        public Batch(IReadOnlyList<T> items) => Items = items;
    }

    internal static class Batch
    {
        public static Batch<T> Create<T>(IReadOnlyList<T> items)
            where T : IBatchItem => new(items);
    }

    internal static class BatchExtensions
    {
        public static IAsyncEnumerable<T> FlattenAsync<T>(this IAsyncEnumerable<Batch<T>> source)
            where T : IBatchItem => source.SelectManyAsync(b => b.Items);
    }
}
