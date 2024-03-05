// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Extensions.ViewModel
{
    public interface ITreeRepositoryViewModel<TEntity> : IRepositoryViewModelBase<TEntity> where TEntity : StringEntityBase, new()
    {
        IObservableSource<TreeNodeBase<TEntity>> Collection { get; set; }
        TreeNodeBase<TEntity> SelectedTreeItem { get; set; }
    }
}
