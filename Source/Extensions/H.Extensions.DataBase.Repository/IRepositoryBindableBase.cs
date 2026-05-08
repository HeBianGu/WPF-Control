// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;

namespace H.Extensions.DataBase.Repository
{
    public interface IRepositoryBindableBase<TEntity> : IRepositoryBindable where TEntity : StringEntityBase, new()
    {
        IStringRepository<TEntity> Repository { get; }
        RelayCommand LoadedCommand { get; }
        IDisplayCommand AddCommand { get; }
        bool? CheckedAll { get; set; }
        IDisplayCommand CheckedAllCommand { get; }
        IDisplayCommand ClearCommand { get; }
        IDisplayCommand DeleteCheckedCommand { get; }
        IDisplayCommand DeleteCommand { get; }
        IDisplayCommand EditCommand { get; }
        IDisplayCommand ExportCommand { get; }
        bool IsBusy { get; set; }
        bool UseMessage { get; set; }
        bool UseOperationLog { get; set; }
        Type ModelType { get; }
        IDisplayCommand NextCommand { get; }
        IDisplayCommand PreviousCommand { get; }
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
        void RefreshData(Action after = null, params string[] includes);
        Task<int> Save();
        Task View(object obj);
        void Next();
        void Previous();
    }
}
