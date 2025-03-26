// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Extensions.DataBase;
using H.Mvvm.ViewModels.Tree;
using H.Extensions.ObservableSource;

namespace H.Extensions.DataBase.Repository
{
    public interface ITreeRepositoryBindable<TEntity> : IRepositoryBindableBase<TEntity> where TEntity : StringEntityBase, new()
    {
        IObservableSource<TreeNodeBase<TEntity>> Collection { get; set; }
        TreeNodeBase<TEntity> SelectedTreeItem { get; set; }
    }
}
