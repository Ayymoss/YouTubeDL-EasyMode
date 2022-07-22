using System.Reflection;
using YoutubeDLSharp;

namespace YouTubeDL_Test;

public static class YouTubeDlTest
{
    public static void Main()
    {
        Console.WriteLine("Example: https://www.youtube.com/watch?v=ybGOT4d2Hs8");
        var youTubeUrl = "Enter YouTube URL".PromptString();
        var workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        const string ffmpegName = @"Lib\ffmpeg.exe";
        const string youtubeDlName = @"Lib\youtube-dl.exe";
        const string outputDirectory = "Downloaded";

        var youTubeDl = new YoutubeDL
        {
            YoutubeDLPath = Path.Join(workingDirectory, youtubeDlName),
            FFmpegPath = Path.Join(workingDirectory, ffmpegName),
            OutputFolder = Path.Join(workingDirectory, outputDirectory)
        };

        var progress = new Progress<DownloadProgress>(progress =>
        {
            Console.WriteLine(
                $"Progress: {progress.State}: {progress.Progress * 100f:N0}% @ {progress.DownloadSpeed}");
        });

        var download = youTubeDl.RunVideoDownload(youTubeUrl, progress: progress).Result;
        var path = download.Data;
        Console.WriteLine($"Download complete. File location: {path}");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
