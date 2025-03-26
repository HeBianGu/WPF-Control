global using H.Extensions.Common;
global using H.Mvvm.ViewModels.Base;
global using System;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;
global using System.Windows.Media;

namespace H.Controls.TagBox
{
    public class Tag : BindableBase, ITag
    {
        private string _name;
        [Required]
        [Display(Name = "名称")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private Brush _background = Brushes.Orange;
        [JsonConverter(typeof(TypeJsonConverter<Brush>))]
        [Display(Name = "颜色")]
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        [Display(Name = "说明")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        [Browsable(false)]
        [Display(Name = "分组")]
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

        private int _order;
        [Browsable(false)]
        [Display(Name = "排序")]
        public int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }

        public override string ToString() => this.Name;
    }
}
