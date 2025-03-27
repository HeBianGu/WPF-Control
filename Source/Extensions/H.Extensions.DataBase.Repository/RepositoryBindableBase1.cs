// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using Microsoft.Win32;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Windows.Threading;
global using H.Extensions.DataBase;
global using H.Services.Common.Excel;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels;
global using H.Services.Message;
global using H.Services.Message.Dialog;
using H.Services.Message;
using H.Services.Operation;
using H.Services.Common.Excel;
using H.Services.Common;
using H.Extensions.Common;
using H.Extensions.ObservableSource;


namespace H.Extensions.DataBase.Repository
{

    public abstract class RepositoryBindableBase<TViewModel, TEntity> : RepositoryBindableBase, IRepositoryBindableBase<TEntity> where TEntity : StringEntityBase, new() where TViewModel : SelectBindable<TEntity>
    {
        protected virtual IEnumerable<string> GetIncludes()
        {
            return null;
        }

        protected override async void Loaded(object obj)
        {
            base.Loaded(obj);
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(async () =>
            {
                if(IocMessage.Dialog==null)
                {
                    this.RefreshData();
                    return;
                }
                await IocMessage.Dialog.ShowWait(x =>
                {
                    this.RefreshData();
                    return true;
                }, x => x.DialogButton = DialogButton.None);


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
        public bool UseMessage
        {
            get { return _useMessage; }
            set
            {
                _useMessage = value;
                RaisePropertyChanged();
            }
        }

        private bool _useoperationLog = true;
        public bool UseOperationLog
        {
            get { return _useoperationLog; }
            set
            {
                _useoperationLog = value;
                RaisePropertyChanged();
            }
        }


        public IStringRepository<TEntity> Repository => DbIoc.GetService<IStringRepository<TEntity>>();
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

        [Display(Name = "新增", GroupName = "操作")]
        public IDisplayCommand AddCommand => new DisplayCommand(async l => await Add(l));

        [Display(Name = "编辑", GroupName = "操作")]
        public IDisplayCommand EditCommand => new DisplayCommand(async l => await Edit(l), l => this.GetEntity(l) != null);

        [Display(Name = "编辑", GroupName = "操作")]
        [Browsable(false)]
        public IDisplayCommand EditTransactionCommand => new DisplayCommand(async x =>
        {
            //if (e is TEntity project)
            //{
            //    bool r = await s.BeginEditAsync(() =>
            //    {
            //        if (project.ModelState(out List<string> message) == false)
            //        {
            //            IocMessage.Snack?.ShowInfo(message?.FirstOrDefault());
            //            return false;
            //        }
            //        return true;
            //    });
            //    if (r)
            //    {
            //        if (this.Repository != null)
            //            this.Repository.Save();
            //    }
            //    else
            //    {
            //        //  Do ：rollback
            //    }
            //}
        });

        [Display(Name = "删除", GroupName = "操作")]
        public IDisplayCommand DeleteCommand => new DisplayCommand(async l => await Delete(l), l => this.GetEntity(l) != null);

        [Display(Name = "查看", GroupName = "操作")]
        public IDisplayCommand ViewCommand => new DisplayCommand(async l => await View(l), l => this.GetEntity(l) != null);

        [Display(Name = "清空", GroupName = "操作")]
        public IDisplayCommand ClearCommand => new DisplayCommand(async l => await Clear(l), l => this.Collection != null && this.Collection.Count > 0);

        [Display(Name = "保存", GroupName = "操作")]
        public IDisplayCommand SaveCommand => new DisplayCommand(async l => await this.Save());

        [Display(Name = "导出", GroupName = "操作")]
        public IDisplayCommand ExportCommand => new DisplayCommand(async l =>
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


        }, l => this.Collection != null && this.Collection.Count > 0);

        [Display(Name = "下一个", GroupName = "操作")]
        public IDisplayCommand NextCommand => new DisplayCommand(x =>
        {
            this.Next();

        }, x => this.Collection.Count > 0);

        public virtual void Next()
        {
            this.Collection.Next();
        }

        [Display(Name = "上一个", GroupName = "操作")]
        public IDisplayCommand PreviousCommand => new DisplayCommand(x =>
        {
            this.Previous();
        }, x => this.Collection.Count > 0);


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
        [Display(Name = "全选", GroupName = "操作")]
        public IDisplayCommand CheckedAllCommand => new DisplayCommand(l =>
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
        }, l => this.Collection != null && this.Collection.Count > 0);

        [Display(Name = "取消选则", GroupName = "操作")]
        public IDisplayCommand CheckedNoneCommand => new DisplayCommand(l =>
        {
            this.Collection.Foreach(K => K.IsSelected = false);

        }, l => this.Collection != null && this.Collection.Count > 0);

        [Display(Name = "全选当前页", GroupName = "操作")]
        public IDisplayCommand CheckedAllCurrentPageCommand => new DisplayCommand(l =>
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
        }, l => this.Collection != null && this.Collection.Count > 0);


        [Display(Name = "全选当前过滤器", GroupName = "操作")]
        public IDisplayCommand CheckedAllFilterSourceCommand => new DisplayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(x => x.IsSelected = !b);
                foreach (var item in this.Collection.FilterSource)
                {
                    item.IsSelected = b;
                }
            }
            else
            {
                this.Collection.Foreach(x => x.IsSelected = false);
                foreach (var item in this.Collection.FilterSource)
                {
                    item.IsSelected = true;
                }
            }
        }, l => this.Collection != null && this.Collection.Count > 0);

        [Display(Name = "删除选中", GroupName = "操作")]
        public IDisplayCommand DeleteCheckedCommand => new DisplayCommand(async l => await DeleteAllChecked(l), l => this.Collection.Any(k => k.IsSelected));

        //public RelayCommand PrintCommand => new RelayCommand(async (s, e) =>
        //{
        //    await this.Print();
        //}, (s, e) => this.Collection.Count > 0);

        //protected virtual async Task<bool> Print()
        //{
        //    var finds = this.Collection.Source.Select(x => x.Model);
        //    return await MessageProxy.Printer.PrintTable(finds);
        //}

        [Display(Name = "表格设置", GroupName = "操作")]
        public IDisplayCommand GridSetCommand => new DisplayCommand(GridSet);

        protected void GridSet(object obj)
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
            IEnumerable<TEntity> collection = this.Collection.Select(x => x.Model);
            string message = null;
            bool? r = Ioc<IExcelService>.Instance?.Export(collection, path, typeof(TEntity).Name, out message);
            if (r == false)
            {
                await IocMessage.Dialog.Show("导出错误," + message);
            }
            else
            {
                bool? rs = await IocMessage.Dialog.Show("导出成功，是否立即查看?", x =>
                {
                    x.Title = "提示";
                    x.DialogButton = DialogButton.SumitAndCancel;
                });
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
            TEntity m = new TEntity();
            bool? dialog = await IocMessage.Form.ShowEdit(this.GetAddModel(m), x => x.Title = "新增");
            if (dialog != true)
            {
                IocMessage.ShowSnackInfo("取消操作");
                return;
            }
            await this.Add(m);
            this.OnCollectionChanged(obj);
        }

        public abstract Task Add(params TEntity[] ms);

        protected TEntity GetEntity(object obj)
        {
            if (obj is SelectBindable<TEntity> svm)
                return svm.Model;
            if (obj is TEntity entity)
                return entity;
            if (this.Collection.SelectedItem is SelectBindable<TEntity> ssvm)
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
                IocMessage.ShowSnackInfo("取消操作");
                return;
            }

            int rs = this.Repository == null ? 1 : await this.Repository.SaveAsync();

            if (rs >= 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("保存成功");
            }
            else
            {
                IocMessage.ShowSnackInfo("保存失败，数据库保存错误");
            }
            if (this.UseOperationLog)
                Ioc<IOperationService>.Instance?.Log<TEntity>($"编辑", entity.ID, OperationType.Update);
        }

        public virtual async Task Delete(object obj)
        {
            TEntity entity = this.GetEntity(obj);
            if (entity == null)
                return;

            if (this.UseMessage)
            {
                bool? result = await IocMessage.Dialog.Show("确定删除数据？", x =>
                {
                    x.Title = "提示";
                    x.DialogButton = DialogButton.SumitAndCancel;
                });
                if (result != true)
                    return;
            }

            int r = this.Repository == null ? 1 : await this.Repository.DeleteAsync(entity);

            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("删除成功");
                TViewModel m = this.Collection.FirstOrDefault(x => x.Model == entity);
                this.Collection.Remove(m);
                this.Collection.SelectedItem = this.Collection.FirstOrDefault(x => true);
            }
            else
            {
                IocMessage.ShowSnackInfo("删除失败,数据库保存错误");
            }
            if (this.UseOperationLog)
                Ioc<IOperationService>.Instance?.Log<TEntity>($"新增", entity.ID, OperationType.Delete);
            this.OnCollectionChanged(obj);
        }

        public virtual bool CanClear()
        {
            return this._collection.Count > 0;
        }

        public virtual async Task Clear(object obj = null)
        {
            bool? result = await IocMessage.Dialog.Show("确定删除数据？", x =>
            {
                x.Title = "提示";
                x.DialogButton = DialogButton.SumitAndCancel;
            });

            if (result != true)
                return;

            int r = this.Repository == null ? 1 : await this.Repository.ClearAsync();
            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("清空成功");
                this.Collection.Clear();
            }
            else
            {
                IocMessage.ShowSnackInfo("清空失败,数据库保存错误");
            }
            if (this.UseOperationLog)
                Ioc<IOperationService>.Instance?.Log<TEntity>($"清空", null, OperationType.Delete);
            this.OnCollectionChanged(obj);
        }

        public virtual async Task View(object obj)
        {
            TEntity entity = this.GetEntity(obj);
            if (entity == null)
                return;
            await IocMessage.Form.ShowView(this.GetViewModel(entity));
            if (this.UseOperationLog)
                Ioc<IOperationService>.Instance?.Log<TEntity>($"查看", entity.ID, OperationType.Search);
            this.OnCollectionChanged(obj);
        }

        public virtual async Task<int> Save()
        {
            int r = this.Repository == null ? 1 : await this.Repository.SaveAsync();
            if (r >= 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("保存成功");
            }
            else
            {
                IocMessage.ShowSnackInfo("保存失败，数据库保存错误");
            }
            return r;
        }


        protected virtual void OnCollectionChanged(object obj)
        {

        }

        protected virtual async Task DeleteAllChecked(object obj)
        {
            bool? result = await IocMessage.Dialog.Show("确定删除数据？", x =>
            {
                x.Title = "提示";
                x.DialogButton = DialogButton.SumitAndCancel;
            });
            if (result != true)
                return;

            List<TEntity> checks = this.Collection.Where(k => k.IsSelected).Select(x => x.Model)?.ToList();
            await this.DeleteAll(checks);
        }

        protected async Task DeleteAll(IEnumerable<TEntity> entities)
        {
            int r = this.Repository == null ? 1 : await this.Repository.DeleteAsync(x => entities.Contains(x));
            if (r > 0)
            {
                IocMessage.Snack?.ShowInfo($"删除成功,共计{entities.Count()}条");
                this.Collection.RemoveAll(x => entities.Contains(x.Model));
            }
            else
            {
                IocMessage.ShowSnackInfo("删除失败,数据库保存错误");
            }
            if (this.UseOperationLog)
                foreach (TEntity item in entities)
                {
                    Ioc<IOperationService>.Instance?.Log<TEntity>($"删除", item.ID, OperationType.Delete);
                }
            this.OnCollectionChanged(entities);
        }

        #endregion 
    }
}
