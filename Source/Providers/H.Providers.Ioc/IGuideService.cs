
using System;
using System.Windows;

namespace H.Providers.Ioc
{
    public interface IGuideService
    {
        void Show(UIElement owner = null);
    }

    public class ShowGuideCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<IGuideService>.Instance.Show();
        }
    }

}
