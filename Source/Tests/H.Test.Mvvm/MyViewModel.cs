using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace H.Test.Mvvm
{
    public class MyViewModel : ObservableObject
    {
        public MyViewModel()
        {
            IncrementCounterCommand = new RelayCommand(IncrementCounter);
        }

        private int counter;

        public int Counter
        {
            get => counter;
            private set => SetProperty(ref counter, value);
        }

        public ICommand IncrementCounterCommand { get; }

        private void IncrementCounter() => Counter++;
    }
}
