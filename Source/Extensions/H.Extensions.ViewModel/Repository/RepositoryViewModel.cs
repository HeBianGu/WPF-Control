// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H.Extensions.ViewModel
{
    public interface IRepositoryViewModel<TEntity> : IRepositoryViewModelBase<TEntity>
     where TEntity : StringEntityBase, new()
    {
        IObservableSource<SelectViewModel<TEntity>> Collection { get; set; }
    }

    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryViewModel<TEntity> : RepositoryViewModelBase<SelectViewModel<TEntity>, TEntity>, IRepositoryViewModel<TEntity> where TEntity : StringEntityBase, new()
    {
        public override void RefreshData(params string[] includes)
        {
            //this.IsBusy = true;
            //try
            //{
            includes = includes ?? this.GetIncludes()?.ToArray();
            IEnumerable<SelectViewModel<TEntity>> collection = includes == null ? this.Repository.GetList().Where(x=>this.Where(x)).Select(x => new SelectViewModel<TEntity>(x))
            : this.Repository.GetList(includes).Where(x => this.Where(x)).Select(x => new SelectViewModel<TEntity>(x));
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
                    this.Collection.Add(new SelectViewModel<TEntity>(m));
                    Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
                }
                IocMessage.Snack?.ShowInfo("新增成功");
                return;
            }
            int r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                this.Collection.Add(ms.Select(x => new SelectViewModel<TEntity>(x)).ToArray());
                IocMessage.Snack?.ShowInfo("新增成功");
            }
            else
            {
                IocMessage.Snack?.ShowInfo("新增失败,数据库保存错误");
            }
            foreach (TEntity m in ms)
            {
                Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", m.ID, OperationType.Add);
            }
        }
    }

    public class DateRepositoryViewModel<TEntity> : RepositoryViewModel<TEntity> where TEntity : DbModelBase, new()
    {
        public override void RefreshData(params string[] includes)
        {
            includes = includes ?? this.GetIncludes().ToArray();
            IEnumerable<SelectViewModel<TEntity>> collection = includes == null ? this.Repository.GetList(x => x.CDATE > this.StartTime && x.CDATE < this.EndTime).Select(x => new SelectViewModel<TEntity>(x))
            : this.Repository.GetList(includes).Select(x => new SelectViewModel<TEntity>(x));
            this.Collection.Load(collection);
        }

        private DateTime _startTime = DateTime.Now.AddDays(-7).Date;
        /// <summary> 说明  </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _endTime = DateTime.Now.AddDays(1).Date;
        /// <summary> 说明  </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
