// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.ParameterInfo.Base;

/// <summary>
/// 参数访问权限。
/// </summary>
[Flags]
public enum ParameterAccess
{
    None = 0,
    Read = 1,
    Write = 2,
    ReadWrite = Read | Write
}


