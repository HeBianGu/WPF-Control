// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H.Extensions.DataBase;

namespace H.Extensions.DataBase.Repository
{
    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBindable<TEntity> : RepositoryBindableBase<SelectBindable<TEntity>, TEntity>, IRepositoryBindable<TEntity> where TEntity : StringEntityBase, new()
    {
        public override void RefreshData(params string[] includes)
        {
            //this.IsBusy = true;
            //try
            //{
            includes = includes ?? this.GetIncludes()?.ToArray();
            IEnumerable<SelectBindable<TEntity>> collection = includes == null ? this.Repository.GetList().Where(x => this.Where(x)).Select(x => new SelectBindable<TEntity>(x))
            : this.Repository.GetList(includes).Where(x => this.Where(x)).Select(x => new SelectBindable<TEntity>(x));
            //this.Collection = collection.Select(x => new SelectViewModel<TEntity>(x)).ToObservable();
            this.Collection.Load(collection);
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(ex);
            //    IocMessage.Dialog.Show("加载数据错误:" + ex.Message);
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }

        //public override async void RefreshData(params string[] includes)
        //{
        //    using (var sp = Ioc.Services.CreateScope())
        //    {
        //        var repository = sp.ServiceProvider.GetRequiredService<IStringRepository<TEntity>>();
        //        includes = includes ?? this.GetIncludes()?.ToArray();
        //        IEnumerable<SelectViewModel<TEntity>> collection = null;
        //        if (includes == null)
        //        {
        //            var datas = await repository.GetListAsync();
        //            collection = datas.Select(x => new SelectViewModel<TEntity>(x));
        //        }
        //        else
        //        {
        //            var datas = await repository.GetListAsync(includes);
        //            collection = datas.Select(x => new SelectViewModel<TEntity>(x));
        //        }
        //        this.Collection.Load(collection);
        //    }
        //}

        protected virtual bool Where(TEntity entity)
        {
            return true;
        }

        public override async Task Add(params TEntity[] ms)
        {
            if (this.Repository == null)
            {
                foreach (TEntity m in ms)
                {
                    this.Collection.Add(new SelectBindable<TEntity>(m));
                    if (this.UseOperationLog)
                        Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
                }
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("新增成功");
                return;
            }
            int r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                this.Collection.Add(ms.Select(x => new SelectBindable<TEntity>(x)).ToArray());
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("新增成功");
            }
            else
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("新增失败,数据库保存错误");
            }

            if (this.UseOperationLog)
                foreach (TEntity m in ms)
                {
                    Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
                }
        }
    }
}
