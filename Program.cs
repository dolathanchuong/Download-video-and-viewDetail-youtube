using youtube369.TestData;
using youtube369.Videos;
using System;
using youtube369.Exceptions;
using Xunit;
using Xunit.Abstractions;
using youtube369.Common;
using System.Text;
using youtube369.Videos.Streams;
using AngleSharp.Common;
using youtube369.Utils.Extensions;

namespace youtube369
{
    public static class Program
    {
        private static async Task Main()
        {
            Console.Title = "YoutubeExplode Demo";

            var youtube = new YoutubeClient();

            // Get the video ID
            Console.Write("Enter YouTube video ID or URL: ");
            var videoId = VideoId.Parse(Console.ReadLine() ?? "");

            // Get available streams and choose the best muxed (audio + video) stream
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
            if (streamInfo is null)
            {
                // Available streams vary depending on the video and it's possible
                // there may not be any muxed streams at all.
                // See the readme to learn how to handle adaptive streams.
                Console.Error.WriteLine("This video has no muxed streams.");
                return;
            }

            // Download the stream
            var fileName = $"{videoId}.{streamInfo.Container.Name}";

            Console.Write(
                $"Downloading stream: {streamInfo.VideoQuality.Label} / {streamInfo.Container.Name}... "
            );

            using (var progress = new ConsoleProgress())
                await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName, progress);

            Console.WriteLine("Done");
            Console.WriteLine($"Video saved to '{fileName}'");


            //youtubeClients();
            Console.ReadLine();
        }
        private static async void youtubeClients()
        {
            // Arrange
            var youtube = new YoutubeClient();
            // Act
            var video = await youtube.Videos.GetAsync(VideoIds.Normal);
            // Assert

            var ViewCount = video.Engagement.ViewCount;
            var LikeCount = video.Engagement.LikeCount;
            var DislikeCount = video.Engagement.DislikeCount;
            var AverageRating = video.Engagement.AverageRating;
            Console.WriteLine("Hello 369...");
            Console.WriteLine("\n");
            Console.WriteLine(" DislikeCount : " + DislikeCount);
            Console.WriteLine("     ViewCount: " + ViewCount );
            Console.WriteLine("         LikeCount: " + LikeCount);
            Console.WriteLine("             AverageRating: " + AverageRating);
            Console.WriteLine("                 UploadDate: " + video.UploadDate.Date);
            Console.WriteLine("                     VideoId: " + video.Id.Value);
            Console.WriteLine("                         VideoUrl: " + video.Url);
            Console.WriteLine("                             VideoTitle: " + video.Title);
            Console.WriteLine("                                 ChannelId: " + video.Author.ChannelId.Value);
            Console.WriteLine("                                 ChannelUrl: " + video.Author.ChannelUrl);
            Console.WriteLine("                                 ChannelTitle: " + video.Author.ChannelTitle);
            Console.WriteLine(" Description : " + video.Description);
            Console.WriteLine(" Duration : " + video.Duration);
            Console.WriteLine(" Thumbnails : " + video.Thumbnails.GetWithHighestResolution().Url);
            Console.WriteLine(" Keywords : " + video.Keywords);
            ////////////////////////////////////////////////////
            // Act
            var videocn = await youtube.Channels.GetAsync(video.Author.ChannelId.Value);
            var _arr = videocn.ToDictionary().ToArray();
            Console.WriteLine(" video Channel ===================================================> ");
            foreach(var item in _arr)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}