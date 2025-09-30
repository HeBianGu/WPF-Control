// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Revertible;

public class RevertibleDisposer : IDisposable
{
    private IRevertibleService _service;
    public RevertibleDisposer()
    {

    }
    public RevertibleDisposer(IRevertibleService service)
    {
        _service = service;
    }

    public void Dispose()
    {
        if (_service == null)
            return;
        _service.Commit();
    }
}
