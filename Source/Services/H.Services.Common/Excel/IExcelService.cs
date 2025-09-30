// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Common.Excel;

public interface IExcelService
{
    bool Export(IEnumerable<object> collection, string path, string sheetName, out string message);
    IEnumerable<T> Read<T>(string filePath, Func<object, T> convert, out string message);
}
