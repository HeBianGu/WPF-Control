// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Providers.Mvvm;

namespace H.Controls.Diagram
{
    /// <summary>
    /// 默认端口要显示的效果
    /// </summary>
    public class DefaultLinkData : NotifyPropertyChanged, ILinkData
    {
        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string FromNodeID { get; set; }

        public string ToNodeID { get; set; }

        public string FromPortID { get; set; }

        public string ToPortID { get; set; }

        public void ApplayStyleTo(ILinkData node)
        {

        }
    }
}
