using FFMpegCore;

namespace H.Extensions.FFMpeg;

public interface IFFMpegService
{
    IMediaAnalysis GetMediaAnalysis(string url);
}