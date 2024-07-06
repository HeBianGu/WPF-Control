// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Threading.Tasks;
using H.Extensions.DataBase;

namespace H.Extensions.ViewModel
{
    public interface IRepositoryBindableBase<TEntity> : IRepositoryBindable where TEntity : StringEntityBase, new()
    {
        RelayCommand LoadedCommand { get; }
        RelayCommand AddCommand { get; }
        bool CheckedAll { get; set; }
        RelayCommand CheckedAllCommand { get; }
        RelayCommand ClearCommand { get; }
        RelayCommand DeleteCheckedCommand { get; }
        RelayCommand DeleteCommand { get; }
        RelayCommand EditCommand { get; }
        TransactionCommand EditTransactionCommand { get; }
        RelayCommand ExportCommand { get; }
        bool IsBusy { get; set; }
        bool UseMessage { get; set; }
        bool UseOperationLog { get; set; }
        Type ModelType { get; }
        RelayCommand NextCommand { get; }
        RelayCommand PreviousCommand { get; }
        IStringRepository<TEntity> Repository { get; }
        RelayCommand SaveCommand { get; }
        RelayCommand ViewCommand { get; }
        RelayCommand GridSetCommand { get; }

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
