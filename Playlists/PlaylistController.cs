﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using youtube369.Bridge;
using youtube369.Exceptions;
using youtube369.Videos;

namespace youtube369.Playlists
{
    internal class PlaylistController(HttpClient http)
    {
        // Works only with user-made playlists
        public async ValueTask<PlaylistBrowseResponse> GetPlaylistBrowseResponseAsync(
            PlaylistId playlistId,
            CancellationToken cancellationToken = default
        )
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://www.youtube.com/youtubei/v1/browse"
            )
            {
                Content = new StringContent(
                    // lang=json
                    $$"""
                {
                    "browseId": "VL{{playlistId}}",
                    "context": {
                        "client": {
                            "clientName": "WEB",
                            "clientVersion": "2.20210408.08.00",
                            "hl": "en",
                            "gl": "US",
                            "utcOffsetMinutes": 0
                        }
                    }
                }
                """
                )
            };

            using var response = await http.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var playlistResponse = PlaylistBrowseResponse.Parse(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            if (!playlistResponse.IsAvailable)
                throw new PlaylistUnavailableException($"Playlist '{playlistId}' is not available.");

            return playlistResponse;
        }

        // Works on all playlists, but contains limited metadata
        public async ValueTask<PlaylistNextResponse> GetPlaylistNextResponseAsync(
            PlaylistId playlistId,
            VideoId? videoId = null,
            int index = 0,
            string? visitorData = null,
            CancellationToken cancellationToken = default
        )
        {
            for (var retriesRemaining = 5; ; retriesRemaining--)
            {
                using var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://www.youtube.com/youtubei/v1/next"
                )
                {
                    Content = new StringContent(
                        // lang=json
                        $$"""
                    {
                        "playlistId": "{{playlistId}}",
                        "videoId": "{{videoId}}",
                        "playlistIndex": {{index}},
                        "context": {
                            "client": {
                                "clientName": "WEB",
                                "clientVersion": "2.20210408.08.00",
                                "hl": "en",
                                "gl": "US",
                                "utcOffsetMinutes": 0,
                                "visitorData": "{{visitorData}}"
                            }
                        }
                    }
                    """
                    )
                };

                using var response = await http.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var playlistResponse = PlaylistNextResponse.Parse(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                );

                if (!playlistResponse.IsAvailable)
                {
                    // Retry if this is not the first request, meaning that the previous requests were successful,
                    // and that the playlist is probably not actually unavailable.
                    if (index > 0 && !string.IsNullOrWhiteSpace(visitorData) && retriesRemaining > 0)
                        continue;

                    throw new PlaylistUnavailableException(
                        $"Playlist '{playlistId}' is not available."
                    );
                }

                return playlistResponse;
            }
        }

        public async ValueTask<IPlaylistData> GetPlaylistResponseAsync(
            PlaylistId playlistId,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                return await GetPlaylistBrowseResponseAsync(playlistId, cancellationToken);
            }
            catch (PlaylistUnavailableException)
            {
                return await GetPlaylistNextResponseAsync(playlistId, null, 0, null, cancellationToken);
            }
        }
    }
}
