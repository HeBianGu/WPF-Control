// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace H.Controls.FavoriteBox
{
    public class FavoriteItem : BindableBase, IFavoriteItem
    {
        private string _name;
        [Required]
        [Display(Name = "名称")]
        public string Path
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

        public override string ToString() => this.Path;
    }
}
