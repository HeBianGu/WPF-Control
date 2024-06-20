// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.Windows.Media;

namespace H.Windows.Ribbon
{
    public class QATItem
    {
        public QATItem()
        {
        }

        public QATItem(object instance, bool isSplitHeader)
        {
            this.Instance = instance;
            this.IsSplitHeader = isSplitHeader;
        }

        public int TabIndex { get; set; }
        public int GroupIndex { get; set; }

        public Int32Collection ControlIndices
        {
            get
            {
                if (_controlIndices == null)
                {
                    _controlIndices = new Int32Collection();
                }
                return _controlIndices;
            }
            set
            {
                _controlIndices = value;
            }
        }

        private Int32Collection _controlIndices;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Instance { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSplitHeader { get; set; }
    }
}
