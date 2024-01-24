using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils.Extensions
{
    internal static class StreamExtensions
    {
        public static async ValueTask CopyToAsync(
        this Stream source,
        Stream destination,
        IProgress<double>? progress = null,
        CancellationToken cancellationToken = default
    )
        {
            using var buffer = MemoryPool<byte>.Shared.Rent(81920);

            var totalBytesRead = 0L;
            while (true)
            {
                var bytesRead = await source.ReadAsync(buffer.Memory, cancellationToken);
                if (bytesRead <= 0)
                    break;

                await destination.WriteAsync(buffer.Memory[..bytesRead], cancellationToken);

                totalBytesRead += bytesRead;
                progress?.Report(1.0 * totalBytesRead / source.Length);
            }
        }
    }
}
