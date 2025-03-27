// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;
using H.Mvvm;
using System;
using System.Threading.Tasks;
using H.Extensions.DataBase;
using H.Mvvm.Commands;

namespace H.Extensions.DataBase.Repository
{
    public interface IRepositoryBindableBase<TEntity> : IRepositoryBindable where TEntity : StringEntityBase, new()
    {
        RelayCommand LoadedCommand { get; }
        IDisplayCommand AddCommand { get; }
        bool CheckedAll { get; set; }
        IDisplayCommand CheckedAllCommand { get; }
        IDisplayCommand ClearCommand { get; }
        IDisplayCommand DeleteCheckedCommand { get; }
        IDisplayCommand DeleteCommand { get; }
        IDisplayCommand EditCommand { get; }
        //RelayCommand EditTransactionCommand { get; }
        IDisplayCommand ExportCommand { get; }
        bool IsBusy { get; set; }
        bool UseMessage { get; set; }
        bool UseOperationLog { get; set; }
        Type ModelType { get; }
        IDisplayCommand NextCommand { get; }
        IDisplayCommand PreviousCommand { get; }
        IStringRepository<TEntity> Repository { get; }
        IDisplayCommand SaveCommand { get; }
        IDisplayCommand ViewCommand { get; }
        IDisplayCommand GridSetCommand { get; }

        Task Add(object obj);
        Task Add(params TEntity[] ms);
        Task Clear(object obj = null);
        bool CanClear();
        Task Delete(object obj);
        Task Edit(object obj);
        Task Export(string path);
        void RefreshData(params string[] includes);
        Task<int> Save();
        Task View(object obj);
        void Next();
        void Previous();
    }
}
