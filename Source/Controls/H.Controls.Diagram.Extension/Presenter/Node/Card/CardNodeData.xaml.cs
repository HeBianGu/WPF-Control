using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Diagram.Extension
{
    public class CardNodeData : FlowableNodeData
    {
        private double _cornerRadius;
        [DefaultValue(4)]
        [Display(Name = "圆角", GroupName = "样式")]
        public double CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                RaisePropertyChanged();
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Width = 180;
            this.Height = 60;
        }
    }
}
