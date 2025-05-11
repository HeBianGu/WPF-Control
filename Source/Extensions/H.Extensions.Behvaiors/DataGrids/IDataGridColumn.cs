// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;

namespace H.Extensions.Behvaiors.DataGrids;

//public class mydatagrid:DataGrid
//{
//    protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
//    {
//        base.OnAutoGeneratingColumn(e);
//    }
//}

public interface IDataGridColumn
{
    string PropertyPath { get; set; }
    Type Template { get; set; }
    DataGridLength Width { get; set; }

    DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo);
}
