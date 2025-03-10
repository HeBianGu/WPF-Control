namespace H.Services.Common
{
    public class ShowGuideCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<IGuideService>.Instance.Show();
        }
    }

    public class ShowNewGuideCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            var sss= Assembly.GetEntryAssembly().GetName().Version.ToString();
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>()?.Version;
            Ioc<IGuideService>.Instance.Show(sss);
        }
    }

}
