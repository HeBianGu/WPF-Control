// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace H.Windows.Ribbon
{
    public class GalleryCategoryBindable<T> : ControlBindableBase
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<T> GalleryItemDataCollection
        {
            get
            {
                if (_controlDataCollection == null)
                {
                    _controlDataCollection = new ObservableCollection<T>();
                }
                return _controlDataCollection;
            }
        }
        private ObservableCollection<T> _controlDataCollection;
    }
}
