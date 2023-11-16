using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Diagram.Extension
{
    public class LineCardNodeData : CardNodeData
    {
        private bool _useLeft;
        [DefaultValue(true)]
        [Display(Name = "启用左侧", GroupName = "样式")]
        public bool UseLeft
        {
            get { return _useLeft; }
            set
            {
                _useLeft = value;
                RaisePropertyChanged();
            }
        }


        private bool _useRight;
        [DefaultValue(false)]
        [Display(Name = "启用右侧侧", GroupName = "样式")]
        public bool UseRight
        {
            get { return _useRight; }
            set
            {
                _useRight = value;
                RaisePropertyChanged();
            }
        }

        private double _flagLength;
        [DefaultValue(10)]
        [Display(Name = "标签宽度", GroupName = "样式")]
        public double FlagLength
        {
            get { return _flagLength; }
            set
            {
                _flagLength = value;
                RaisePropertyChanged();
            }
        }
    }
}
