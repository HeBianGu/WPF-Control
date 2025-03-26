// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Services.Common;
using H.Mvvm;
using H.Extensions.DataBase;
using H.Extensions.ObservableSource;
namespace H.Extensions.DataBase.Repository
{
    public interface IRepositoryBindable<TEntity> : IRepositoryBindableBase<TEntity>
     where TEntity : StringEntityBase, new()
    {
        IObservableSource<SelectBindable<TEntity>> Collection { get; set; }
    }
}
