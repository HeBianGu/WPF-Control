// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;

namespace H.Data.Test
{
    public class Students : ObservableCollection<Student>
    {
        public Students()
        {

        }
        public Students(IEnumerable<Student> collection) : base(collection)
        {

        }
    }
}
