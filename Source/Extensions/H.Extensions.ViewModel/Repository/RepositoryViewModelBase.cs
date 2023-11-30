// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Ioc;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace H.Extensions.ViewModel
{
    public class RepositoryViewModelBase : NotifyPropertyChanged
    {

    }

    public abstract class RepositoryViewModelBase<TViewModel, TEntity> : RepositoryViewModelBase, IRepositoryViewModelBase<TEntity> where TEntity : StringEntityBase, new() where TViewModel : SelectViewModel<TEntity>
    {
        protected virtual IEnumerable<string> GetIncludes()
        {
            return null;
        }

        protected override void Loaded(object obj)
        {
            if (this.Repository == null)
                return;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                IocMessage.Dialog.ShowWait(x =>
                {
                    this.RefreshData();
                    return true;
                });
            }));
        }

        public abstract void RefreshData(params string[] includes);

        #region - 属性 -
        private bool _checkedAll;
        public bool CheckedAll
        {
            get { return _checkedAll; }
            set
            {
                _checkedAll = value;
                RaisePropertyChanged();
            }
        }

        public Type ModelType => typeof(TEntity);

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMessage = true;
        /// <summary> 说明  </summary>
        public bool UseMessage
        {
            get { return _useMessage; }
            set
            {
                _useMessage = value;
                RaisePropertyChanged();
            }
        }

        public IStringRepository<TEntity> Repository => Ioc.GetService<IStringRepository<TEntity>>();
        //public IStringRepository<TEntity> Repository => ServiceRegistry.Instance.GetAllAssignableFrom<IStringRepository<TEntity>>()?.FirstOrDefault();
        private IObservableSource<TViewModel> _collection = new ObservableSource<TViewModel>();
        /// <summary> 说明  </summary>
        public IObservableSource<TViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region - 命令 -

        [Displayer(Name = "新增", GroupName = "操作")]
        public RelayCommand AddCommand => new RelayCommand(async l => await Add(l)) { Name = "新增" };

        [Displayer(Name = "编辑", GroupName = "操作")]
        public RelayCommand EditCommand => new RelayCommand(async l => await Edit(l), l => this.GetEntity(l) != null) { Name = $"编辑" };

        public TransactionCommand EditTransactionCommand => new TransactionCommand(async (s, e) =>
        {
            if (e is TEntity project)
            {
                TEntity temp;
                var r = await s.BeginEditAsync(() =>
                {
                    if (project.ModelState(out List<string> message) == false)
                    {
                        IocMessage.Snack?.ShowInfo(message?.FirstOrDefault());
                        return false;
                    }
                    return true;
                });
                if (r)
                {
                    if (this.Repository != null)
                        this.Repository.Save();
                }
                else
                {
                    //  Do ：rollback
                }
            }
        });

        [Displayer(Name = "删除", GroupName = "操作")]
        public RelayCommand DeleteCommand => new RelayCommand(async l => await Delete(l), l => this.GetEntity(l) != null) { Name = $"删除" };

        [Displayer(Name = "查看", GroupName = "操作")]
        public RelayCommand ViewCommand => new RelayCommand(async l => await View(l), l => this.GetEntity(l) != null) { Name = $"查看" };

        [Displayer(Name = "清空", GroupName = "操作")]
        public RelayCommand ClearCommand => new RelayCommand(async l => await Clear(l), l => this.Collection != null && this.Collection.Count > 0) { Name = $"清除" };

        [Displayer(Name = "保存", GroupName = "操作")]
        public RelayCommand SaveCommand => new RelayCommand(async l => await this.Save());

        [Displayer(Name = "导出", GroupName = "操作")]
        public RelayCommand ExportCommand => new RelayCommand(async l =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|所有文件|*.*";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "保存文件";
            if (saveFileDialog.ShowDialog() != true)
                return;

            await this.Export(saveFileDialog.FileName);


        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"导出" };

        [Displayer(Name = "下一个", GroupName = "操作")]
        public RelayCommand NextCommand => new RelayCommand((s, e) =>
        {
            this.Next();

        }, (s, e) => this.Collection.Count > 0);

        public virtual void Next()
        {
            this.Collection.Next();
        }

        [Displayer(Name = "上一个", GroupName = "操作")]
        public RelayCommand PreviousCommand => new RelayCommand((s, e) =>
        {
            this.Previous();
        }, (s, e) => this.Collection.Count > 0);


        public virtual void Previous()
        {
            this.Collection.Previous();
        }
        //public RelayCommand ImportCommand => new RelayCommand(async l =>
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    openFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";
        //    openFileDialog.FilterIndex = 2;
        //    openFileDialog.Title = "打开文件";
        //    openFileDialog.RestoreDirectory = true;
        //    openFileDialog.Multiselect = false;
        //    if (openFileDialog.ShowDialog() != true)
        //        return;

        //    await this.Import(openFileDialog.FileName);
        //}); 
        [Displayer(Name = "全选", GroupName = "操作")]
        public RelayCommand CheckedAllCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(K => K.IsSelected = b);
                this.CheckedAll = b;
            }
            else
            {
                this.Collection.Foreach(K => K.IsSelected = true);
                this.CheckedAll = true;
            }
        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"全选" };

        [Displayer(Name = "取消选则", GroupName = "操作")]
        public RelayCommand CheckedNoneCommand => new RelayCommand(l =>
        {
            this.Collection.Foreach(K => K.IsSelected = false);

        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"取消选则" };

        [Displayer(Name = "全选当前页", GroupName = "操作")]
        public RelayCommand CheckedAllCurrentPageCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(x => x.IsSelected = !b);
                this.Collection.Source.Foreach(K => K.IsSelected = b);

            }
            else
            {
                this.Collection.Foreach(x => x.IsSelected = false);
                this.Collection.Source.Foreach(K => K.IsSelected = true);
            }
        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"全选当前页" };


        [Displayer(Name = "全选当前过滤器", GroupName = "操作")]
        public RelayCommand CheckedAllFilterSourceCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(x => x.IsSelected = !b);
                this.Collection.FilterSource.Foreach(K => K.IsSelected = b);

            }
            else
            {
                this.Collection.Foreach(x => x.IsSelected = false);
                this.Collection.FilterSource.Foreach(K => K.IsSelected = true);
            }
        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"全选当前页" };

        [Displayer(Name = "删除选中", GroupName = "操作")]
        public RelayCommand DeleteCheckedCommand => new RelayCommand(async l => await DeleteAllChecked(l), l => this.Collection.Any(k => k.IsSelected)) { Name = $"删除选中" };

        //public RelayCommand PrintCommand => new RelayCommand(async (s, e) =>
        //{
        //    await this.Print();
        //}, (s, e) => this.Collection.Count > 0);

        //protected virtual async Task<bool> Print()
        //{
        //    var finds = this.Collection.Source.Select(x => x.Model);
        //    return await MessageProxy.Printer.PrintTable(finds);
        //}

        [Displayer(Name = "表格设置", GroupName = "操作", Icon = "\xe8a5")]
        public RelayCommand GridSetCommand => new RelayCommand(GridSet);

        protected async void GridSet(object obj)
        {
            //if (obj is IList source)
            //{
            //    await MessageProxy.AutoColumnPagedDataGrid.Show(source, null, "表格设置", x => x.Margin = new Thickness(50));
            //}
        }
        #endregion

        #region - 方法 -

        public virtual async Task Export(string path)
        {
            Ioc.GetService<IOperationService>().Log<TEntity>($"导出");
            var collection = this.Collection.Select(x => x.Model);
            string message = null;
            var r = Ioc<IExcelService>.Instance?.Export(collection, path, typeof(TEntity).Name, out message);
            if (r == false)
            {
                await IocMessage.Dialog.ShowMessage("导出错误," + message);
            }
            else
            {
                var rs = await IocMessage.Dialog.ShowMessage("导出成功，是否立即查看?", null, DialogButton.SumitAndCancel);
                if (rs == true)
                {
                    //Process.Start("explorer.exe", path);
                    Process.Start(new ProcessStartInfo("explorer.exe", path) { UseShellExecute = true });

                }
            }
        }

        //protected virtual async Task Import(string path)
        //{
        //    await MessageProxy.Messager.ShowWaitter(() =>
        //    {
        //        var models = XmlSerialize.Instance.Load<ObservableCollection<T>>(path);

        //        this.Collection.AddRange(models);
        //    });
        //}

        protected virtual object GetAddModel(TEntity entity)
        {
            return entity;
        }

        protected virtual object GetEditModel(TEntity entity)
        {
            return entity;
        }
        protected virtual object GetViewModel(TEntity entity)
        {
            return entity;
        }

        public virtual async Task Add(object obj)
        {
            var m = new TEntity();
            bool? dialog = await IocMessage.Form.ShowEdit(this.GetAddModel(m), null, null, null, "新增");
            if (dialog != true)
            {
                IocMessage.Snack?.ShowInfo("取消操作");
                return;
            }
            await this.Add(m);
            this.OnCollectionChanged(obj);
        }

        public abstract Task Add(params TEntity[] ms);

        protected TEntity GetEntity(object obj)
        {
            if (obj is SelectViewModel<TEntity> svm)
                return svm.Model;
            if (obj is TEntity entity)
                return entity;
            if (this.Collection.SelectedItem is SelectViewModel<TEntity> ssvm)
                return ssvm.Model;
            if (this.Collection.SelectedItem is TEntity sentity)
                return sentity;
            return null;
        }

        public virtual async Task Edit(object obj)
        {
            TEntity entity = this.GetEntity(obj);
            if (entity == null)
                return;
            bool? r = await IocMessage.Form.ShowEdit(this.GetEditModel(entity));
            if (r != true)
            {
                IocMessage.Snack?.ShowInfo("取消操作");
                return;
            }

            int rs = this.Repository == null ? 1 : await this.Repository.SaveAsync();

            if (rs > 0)
            {
                if (this.UseMessage)
                    IocMessage.Snack?.ShowInfo("保存成功");
            }
            else
            {
                IocMessage.Snack?.ShowInfo("保存失败，数据库保存错误");
            }
            Ioc<IOperationService>.Instance?.Log<TEntity>($"编辑", entity.ID,OperationType.Update);
        }

        public virtual async Task Delete(object obj)
        {
            TEntity entity = this.GetEntity(obj);
            if (entity == null)
                return;

            if (this.UseMessage)
            {
                var result = await IocMessage.Dialog.ShowMessage("确定删除数据？", "提示", DialogButton.SumitAndCancel);
                if (result != true)
                    return;
            }

            var r = this.Repository == null ? 1 : await this.Repository.DeleteAsync(entity);

            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.Snack?.ShowInfo("删除成功");
                var m = this.Collection.FirstOrDefault(x => x.Model == entity);
                this.Collection.Remove(m);
                this.Collection.SelectedItem = this.Collection.FirstOrDefault(x => true);
            }
            else
            {
                IocMessage.Snack?.ShowInfo("删除失败,数据库保存错误");
            }
            Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", entity.ID,OperationType.Delete);
            this.OnCollectionChanged(obj);
        }

        public virtual bool CanClear()
        {
            return this._collection.Count > 0;
        }

        public virtual async Task Clear(object obj = null)
        {
            var result = await IocMessage.Dialog.ShowMessage("确定清空数据？", "提示", DialogButton.SumitAndCancel);
            if (result != true)
                return;

            var r = this.Repository == null ? 1 : await this.Repository.ClearAsync();
            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.Snack?.ShowInfo("清空成功");
                this.Collection.Clear();
            }
            else
            {
                IocMessage.Snack?.ShowInfo("清空失败,数据库保存错误");
            }
            Ioc<IOperationService>.Instance?.Log<TEntity>($"清空",null,OperationType.Delete);
            this.OnCollectionChanged(obj);
        }

        public virtual async Task View(object obj)
        {
            TEntity entity = this.GetEntity(obj);
            if (entity == null)
                return;
            await IocMessage.Form.ShowView(this.GetViewModel(entity));
            Ioc<IOperationService>.Instance?.Log<TEntity>($"查看",entity.ID,OperationType.Search);
            this.OnCollectionChanged(obj);
        }

        public virtual async Task<int> Save()
        {
            var r = this.Repository == null ? 1 : await this.Repository.SaveAsync();
            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.Snack?.ShowInfo("保存成功");
            }
            else
            {
                IocMessage.Snack?.ShowInfo("保存失败,数据库保存错误");
            }
            return r;
        }


        protected virtual void OnCollectionChanged(object obj)
        {

        }

        protected virtual async Task DeleteAllChecked(object obj)
        {
            var result = await IocMessage.Dialog.ShowMessage("确定删除数据？", "提示", DialogButton.SumitAndCancel);
            if (result != true)
                return;

            var checks = this.Collection.Where(k => k.IsSelected).Select(x => x.Model)?.ToList();
            var r = this.Repository == null ? 1 : await this.Repository.DeleteAsync(x => checks.Contains(x));
            if (r > 0)
            {
                IocMessage.Snack?.ShowInfo($"删除成功,共计{checks.Count()}条");
                this.Collection.RemoveAll(x => checks.Contains(x.Model));
            }
            else
            {
                IocMessage.Snack?.ShowInfo("删除失败,数据库保存错误");
            }
            foreach (var item in checks)
            {
                Ioc<IOperationService>.Instance?.Log<TEntity>($"删除", item.ID, OperationType.Delete);
            }
            this.OnCollectionChanged(obj);
        }

        #endregion 
    }
}
