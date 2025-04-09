namespace H.Services.Logger;

/// <summary>
/// 提供日志记录功能的接口。
/// </summary>
public interface ILogService
{
    /// <summary>
    /// 记录调试级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Debug(params string[] messages);

    /// <summary>
    /// 记录错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Error(params Exception[] messages);

    /// <summary>
    /// 记录错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Error(params string[] messages);

    /// <summary>
    /// 记录致命错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Fatal(params string[] messages);

    /// <summary>
    /// 记录致命错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Fatal(params Exception[] messages);

    /// <summary>
    /// 记录信息级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Info(params string[] messages);

    /// <summary>
    /// 记录跟踪级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Trace(params string[] messages);

    /// <summary>
    /// 记录警告级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    void Warn(params string[] messages);
}
