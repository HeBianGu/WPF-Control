using CommunityToolkit.Mvvm.ComponentModel;

namespace H.Test.Mvvm
{
    public class User : ObservableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }
}
