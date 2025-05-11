// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Runtime.CompilerServices;

namespace H.Services.Operation;

public interface IOperationService
{
    void Log<T>(string title, string message = null, OperationType operationType = OperationType.Default, bool result = true, [CallerMemberName] string methodName = null);
}