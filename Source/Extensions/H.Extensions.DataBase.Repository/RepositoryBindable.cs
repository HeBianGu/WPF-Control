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
    public class RepositoryBindable<TViewModel, TEntity> : RepositoryBindableBase<TViewModel, TEntity>, IRepositoryBindable<TViewModel, TEntity> where TEntity : StringEntityBase, new() where TViewModel : SelectBindable<TEntity>
    {
        protected virtual TViewModel CreateSelectBindable(TEntity entity)
        {
            return (TViewModel)Activator.CreateInstance(typeof(TViewModel), entity);
        }

        public override async Task Add(params TEntity[] ms)
        {
            if (this.Repository == null)
            {
                foreach (TEntity m in ms)
                {
                    this.Collection.Add(this.CreateSelectBindable(m));
                    if (this.UseOperationLog)
                        Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
                }
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_OperationSucceeded);
                return;
            }
            int r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                this.Collection.Add(ms.Select(x => this.CreateSelectBindable(x)).ToArray());
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_OperationSucceeded);
            }
            else
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_OperationFailed);
            }

            if (this.UseOperationLog)
                foreach (TEntity m in ms)
                {
                    Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
                }
        }
        public override void RefreshData(params string[] includes)
        {
            includes = includes ?? this.GetIncludes()?.ToArray();
            IEnumerable<TViewModel> collection = includes == null
                ? this.Repository.GetList().Where(x => this.Where(x)).Select(x => this.CreateSelectBindable(x))
                : this.Repository.GetList(includes).Where(x => this.Where(x)).Select(x => this.CreateSelectBindable(x));
            this.Collection.Load(collection);
        }
        protected virtual bool Where(TEntity entity)
        {
            return true;
        }


    }

    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBindable<TEntity> : RepositoryBindable<SelectBindable<TEntity>, TEntity>, IRepositoryBindable<TEntity> where TEntity : StringEntityBase, new()
    {
        protected override SelectBindable<TEntity> CreateSelectBindable(TEntity entity)
        {
            return new SelectBindable<TEntity>(entity);
        }
    }
}
