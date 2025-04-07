using FFMpegCore.Enums;

namespace H.Extensions.FFMpeg;
public interface IFFMpegOption
{
    string BinaryFolder { get; set; }
    FFMpegLogLevel FFMpegLogLevel { get; set; }
    string TemporaryFilesFolder { get; set; }
    bool UseCache { get; set; }
    bool UsingMultithreading { get; set; }
    string WorkingDirectory { get; set; }

    bool Load(out string message);
    void LoadDefault();
    bool Save(out string message);
}