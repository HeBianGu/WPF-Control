namespace H.Extensions.Log4net;

public interface ILog4netOptions
{
    string LogPath { get; set; }
    string tempPath { get; set; }
}