// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.TagBox
{
    public class TagsPresenter : BindableBase
    {
        private ObservableCollection<ITag> _collection = new ObservableCollection<ITag>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ITag> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        /// <summary> 说明  </summary>
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }
    }
}
