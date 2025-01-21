// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using System.Threading;
using System.Threading.Tasks;

namespace H.Extensions.Command
{
    public class AsyncPercentCommand : AsyncMarkupCommandBase
    {
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.Value = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
