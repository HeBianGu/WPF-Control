using FFMpegCore;

namespace H.Extensions.FFMpeg;

public class FFMpegService : IFFMpegService
{

    public IMediaAnalysis GetMediaAnalysis(string url)
    {
        try
        {
            return FFProbe.Analyse(url);
        }
        catch (Exception ex)
        {
            IocLog.Instance?.Error(ex);
        }
        return null;
    }
    //public IMediaAnalysis RefreshProbe(string url)
    //{
    //    try
    //    {
    //        var mediaInfo = FFProbe.Analyse(model.Url);

    //        model.Duration = mediaInfo.Duration.Ticks;
    //        var video = mediaInfo.VideoStreams?.FirstOrDefault();
    //        if (video == null) return;
    //        model.Resoluction = video.PixelFormat;
    //        model.Bitrate = video.BitRate.ToString();
    //        model.MediaCode = video.CodecName;
    //        model.Aspect = $"{video.Width}×{video.Height}";
    //        model.Rate = video.FrameRate.ToString();
    //        model.VedioType = video.CodecLongName;
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.Instance?.Error("生成影片时长错误:RefreshProbe");
    //        Logger.Instance?.Error(ex);
    //    }
    //}

}
