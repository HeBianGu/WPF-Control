// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.ObservableSource;

namespace H.Extensions.DataBase.Repository
{

    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ObservableSourceRepositoryBindable<TEntity> : ObservableSourceRepositoryBindable<SelectBindable<TEntity>, TEntity>, IObservableSourceRepositoryBindable<TEntity> where TEntity : StringEntityBase, new()
    {
        protected override SelectBindable<TEntity> CreateSelectBindable(TEntity entity)
        {
            return new SelectBindable<TEntity>(entity);
        }
    }
}
