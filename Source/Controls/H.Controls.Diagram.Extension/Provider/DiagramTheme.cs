

using H.Providers.Mvvm;

namespace H.Controls.Diagram.Extension
{
    public class DiagramTheme : NotifyPropertyChangedBase
    {
        private TextNodeData _note = new TextNodeData();
        public TextNodeData Note
        {
            get { return _note; }
            set
            {
                _note = value;
                RaisePropertyChanged();
            }
        }

        private TextLinkData _link = new TextLinkData();
        public TextLinkData Link
        {
            get { return _link; }
            set
            {
                _link = value;
                RaisePropertyChanged();
            }
        }


        private TextPortData _port = new TextPortData();
        public TextPortData Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }
    }
}
