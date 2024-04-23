// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Ribbon;

namespace H.Windows.Ribbon
{
    public class GroupBindable : ControlBindableBase
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<ControlBindableBase> ControlDataCollection
        {
            get
            {
                if (_controlDataCollection == null)
                    _controlDataCollection = new ObservableCollection<ControlBindableBase>();
                return _controlDataCollection;
            }
        }
        private ObservableCollection<ControlBindableBase> _controlDataCollection;
        private bool _isCollapsed = false;
        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set
            {
                if (_isCollapsed == value) return;

                _isCollapsed = value;
                RaisePropertyChanged();
            }
        }


        private RibbonGroupSizeDefinitionBaseCollection _groupSizeDefinitions;

        public RibbonGroupSizeDefinitionBaseCollection GroupSizeDefinitions
        {
            get { return _groupSizeDefinitions; }
            set
            {
                if (_groupSizeDefinitions != value)
                {
                    _groupSizeDefinitions = value;

                    RaisePropertyChanged();
                }
            }
        }
    }
}
