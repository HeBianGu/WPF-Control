namespace H.Providers.Ioc
{
    public class LogCommand : IocMarkupCommandBase
    {
        public string Message { get; set; }
        public LogType Type { get; set; }
        public override void Execute(object parameter)
        {
            if (this.Type == LogType.Debug)
                Ioc<ILogService>.Instance?.Debug(this.Message);
            if (this.Type == LogType.Info)
                Ioc<ILogService>.Instance?.Info(this.Message);
            if (this.Type == LogType.Error)
                Ioc<ILogService>.Instance?.Error(this.Message);
            if (this.Type == LogType.Warn)
                Ioc<ILogService>.Instance?.Warn(this.Message);
            if (this.Type == LogType.Fatal)
                Ioc<ILogService>.Instance?.Fatal(this.Message);
        }
    }
}
