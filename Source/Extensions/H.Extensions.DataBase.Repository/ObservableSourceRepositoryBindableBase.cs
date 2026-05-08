// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.Commands;
global using H.Extensions.Mvvm.ViewModels;
global using H.Services.Common.Excel;
global using H.Services.Message;
global using H.Services.Message.Dialog;
global using Microsoft.Win32;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Windows.Threading;
using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Extensions.ObservableSource;
using H.Globalization.Properties;
using H.Mvvm.Commands;
using H.Services.Common;

namespace H.Extensions.DataBase.Repository
{

    public abstract class ObservableSourceRepositoryBindableBase<TViewModel, TEntity> : RepositoryBindableBase, IObservableSourceRepositoryBindable<TViewModel, TEntity> where TEntity : StringEntityBase, new() where TViewModel : SelectBindable<TEntity>
    {
        public IStringRepository<TEntity> Repository => DbIoc.GetService<IStringRepository<TEntity>>();

        private IObservableSource<TViewModel> _ObservableSource = new ObservableSource<TViewModel>();
        public IObservableSource<TViewModel> ObservableSource
        {
            get { return _ObservableSource; }
            set
            {
                _ObservableSource = value;
                RaisePropertyChanged();
            }
        }

        protected virtual IEnumerable<string> GetIncludes()
        {
            return null;
        }

        protected override async void Loaded(object obj)
        {
            base.Loaded(obj);
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(async () =>
            {
                if (IocMessage.Dialog == null)
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
        private bool? _checkedAll = false;
        public bool? CheckedAll
        {
            get { return _checkedAll; }
            set
            {
                _checkedAll = value;
                RaisePropertyChanged();
                if (value.HasValue)
                {
                    this.ObservableSource.Foreach(K => K.IsSelected = value.Value);
                    this.UpdateCheckedAll();
                }
            }
        }

        private void UpdateCheckedAll()
        {
            var all = this.ObservableSource.FilterSource.All(x => x.IsSelected);
            if (all)
                _checkedAll = all;
            else
            {
                var any = this.ObservableSource.FilterSource.Any(x => x.IsSelected);
                if (any)
                    _checkedAll = null;
                else
                    _checkedAll = any;
            }
            RaisePropertyChanged(nameof(CheckedAll));
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

        #endregion

        #region - 命令 -

        [Icon(FontIcons.Add)]
        [Display(Name = "新增", GroupName = "操作,菜单栏")]
        public IDisplayCommand AddCommand => new DisplayCommand(async l => await Add(l));

        [Icon(FontIcons.Edit)]
        [Display(Name = "编辑", GroupName = "操作,菜单栏")]
        public IDisplayCommand EditCommand => new DisplayCommand(async l => await Edit(l), l => this.GetEntity(l) != null);

        public RelayCommand UpdateCheckAllCommand => new RelayCommand(x =>
        {
            this.UpdateCheckedAll();
        });
        //[Icon(FontIcons.Edit)]
        //[Display(Name = "编辑", GroupName = "操作,菜单栏")]
        //[Browsable(false)]
        //public IDisplayCommand EditTransactionCommand => new DisplayCommand(x =>
        //{
        //    //if (e is TEntity project)
        //    //{
        //    //    bool r = await s.BeginEditAsync(() =>
        //    //    {
        //    //        if (project.ModelState(out List<string> message) == false)
        //    //        {
        //    //            IocMessage.Snack?.ShowInfo(message?.FirstOrDefault());
        //    //            return false;
        //    //        }
        //    //        return true;
        //    //    });
        //    //    if (r)
        //    //    {
        //    //        if (this.Repository != null)
        //    //            this.Repository.Save();
        //    //    }
        //    //    else
        //    //    {
        //    //        //  Do ：rollback
        //    //    }
        //    //}
        //});

        [Icon(FontIcons.Delete)]
        [Display(Name = "删除", GroupName = "操作,菜单栏")]
        public IDisplayCommand DeleteCommand => new DisplayCommand(async l => await Delete(l), l => this.GetEntity(l) != null);

        [Icon(FontIcons.View)]
        [Display(Name = "查看", GroupName = "操作,菜单栏")]
        public IDisplayCommand ViewCommand => new DisplayCommand(async l => await View(l), l => this.GetEntity(l) != null);

        [Icon(FontIcons.Clear)]
        [Display(Name = "清空", GroupName = "操作,菜单栏")]
        public IDisplayCommand ClearCommand => new DisplayCommand(async l => await Clear(l), l => this.ObservableSource != null && this.ObservableSource.Count > 0);

        [Icon(FontIcons.Save)]
        [Display(Name = "保存", GroupName = "操作,菜单栏")]
        public IDisplayCommand SaveCommand => new DisplayCommand(async l => await this.Save());

        [Icon(FontIcons.Export)]
        [Display(Name = "导出", GroupName = "操作,菜单栏")]
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

        }, l => this.ObservableSource != null && this.ObservableSource.Count > 0);


        [Icon(FontIcons.Previous)]
        [Display(Name = "上一个", GroupName = "操作,菜单栏,工具栏")]
        public IDisplayCommand PreviousCommand => new DisplayCommand(x =>
        {
            this.Previous();
        }, x => this.ObservableSource.Count > 0);

        public virtual void Previous()
        {
            this.ObservableSource.Previous();
        }

        [Icon(FontIcons.Next)]
        [Display(Name = "下一个", GroupName = "操作,菜单栏,工具栏")]
        public IDisplayCommand NextCommand => new DisplayCommand(x =>
        {
            this.Next();

        }, x => this.ObservableSource.Count > 0);

        public virtual void Next()
        {
            this.ObservableSource.Next();
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

        [Icon(FontIcons.CheckboxComposite)]
        [Display(Name = "全选", GroupName = "操作,菜单栏")]
        public IDisplayCommand CheckedAllCommand => new DisplayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.ObservableSource.Foreach(K => K.IsSelected = b);
                this.UpdateCheckedAll();
            }
            else
            {
                this.ObservableSource.Foreach(K => K.IsSelected = true);
                this.UpdateCheckedAll();
            }
        }, l => this.ObservableSource != null && this.ObservableSource.Count > 0);

        [Icon(FontIcons.Checkbox)]
        [Display(Name = "取消选则", GroupName = "操作,菜单栏")]
        public IDisplayCommand CheckedNoneCommand => new DisplayCommand(l =>
        {
            this.ObservableSource.Foreach(K => K.IsSelected = false);
            this.UpdateCheckedAll();
        }, l => this.ObservableSource != null && this.ObservableSource.Count > 0);

        [Icon(FontIcons.CheckboxComposite)]
        [Display(Name = "全选当前页", GroupName = "操作,菜单栏")]
        public IDisplayCommand CheckedAllCurrentPageCommand => new DisplayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.ObservableSource.Foreach(x => x.IsSelected = !b);
                this.ObservableSource.Source.Foreach(K => K.IsSelected = b);

            }
            else
            {
                this.ObservableSource.Foreach(x => x.IsSelected = false);
                this.ObservableSource.Source.Foreach(K => K.IsSelected = true);
            }
        }, l => this.ObservableSource != null && this.ObservableSource.Count > 0);

        [Icon(FontIcons.CheckboxComposite)]
        [Display(Name = "全选当前过滤器", GroupName = "操作,菜单栏")]
        public IDisplayCommand CheckedAllFilterSourceCommand => new DisplayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.ObservableSource.Foreach(x => x.IsSelected = !b);
                foreach (var item in this.ObservableSource.FilterSource)
                {
                    item.IsSelected = b;
                }
            }
            else
            {
                this.ObservableSource.Foreach(x => x.IsSelected = false);
                foreach (var item in this.ObservableSource.FilterSource)
                {
                    item.IsSelected = true;
                }
            }
        }, l => this.ObservableSource != null && this.ObservableSource.Count > 0);

        [Icon(FontIcons.Delete)]
        [Display(Name = "删除选中", GroupName = "操作,菜单栏")]
        public IDisplayCommand DeleteCheckedCommand => new DisplayCommand(async l => await DeleteAllChecked(l), l => this.ObservableSource.Any(k => k.IsSelected));

        //public RelayCommand PrintCommand => new RelayCommand(async (s, e) =>
        //{
        //    await this.Print();
        //}, (s, e) => this.Collection.Count > 0);

        //protected virtual async Task<bool> Print()
        //{
        //    var finds = this.Collection.Source.Select(x => x.Model);
        //    return await MessageProxy.Printer.PrintTable(finds);
        //}

        [Icon(FontIcons.GridView)]
        [Display(Name = "表格设置", GroupName = "操作,菜单栏")]
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
            IEnumerable<TEntity> collection = this.ObservableSource.Select(x => x.Model);
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
            bool? dialog = await IocMessage.Form.ShowEdit(this.GetAddModel(m), x => x.Title = Resources.Common_Add);
            if (dialog != true)
            {
                IocMessage.ShowSnackInfo(Resources.Common_OperationCancel);
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
            if (this.ObservableSource.SelectedItem is SelectBindable<TEntity> ssvm)
                return ssvm.Model;
            if (this.ObservableSource.SelectedItem is TEntity sentity)
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
                IocMessage.ShowSnackInfo(Resources.Common_OperationCancel);
                return;
            }

            int rs = this.Repository == null ? 1 : await this.Repository.SaveAsync();

            if (rs >= 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_SaveSucceeded);
            }
            else
            {
                IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_SaveFailed);
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
                bool? result = await IocMessage.Dialog.Show(Resources.Dialog_Message_ConfirmDelete, x =>
                {
                    x.DialogButton = DialogButton.SumitAndCancel;
                });
                if (result != true)
                    return;
            }

            int r = this.Repository == null ? 1 : await this.Repository.DeleteAsync(entity);

            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_OperationSucceeded);
                TViewModel m = this.ObservableSource.FirstOrDefault(x => x.Model == entity);
                this.ObservableSource.Remove(m);
                this.ObservableSource.SelectedItem = this.ObservableSource.FirstOrDefault(x => true);
            }
            else
            {
                IocMessage.ShowSnackInfo(H.Globalization.Properties.Resources.Common_OperationFailed);
            }
            if (this.UseOperationLog)
                Ioc<IOperationService>.Instance?.Log<TEntity>($"删除", entity.ID, OperationType.Delete);
            this.OnCollectionChanged(obj);
        }

        public virtual bool CanClear()
        {
            return this._ObservableSource.Count > 0;
        }

        public virtual async Task Clear(object obj = null)
        {
            bool? result = await IocMessage.Dialog.Show(Resources.Dialog_Message_ConfirmDelete, x =>
            {
                x.DialogButton = DialogButton.SumitAndCancel;
            });

            if (result != true)
                return;

            int r = this.Repository == null ? 1 : await this.Repository.ClearAsync();
            if (r > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(Resources.Common_OperationSucceeded);
                this.ObservableSource.Clear();
            }
            else
            {
                IocMessage.ShowSnackInfo(Resources.Common_OperationFailed);
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
                    IocMessage.ShowSnackInfo(Resources.Common_SaveSucceeded);
            }
            else
            {
                IocMessage.ShowSnackInfo(Resources.Common_SaveFailed);
            }
            return r;
        }

        protected virtual void OnCollectionChanged(object obj)
        {

        }

        protected virtual async Task DeleteAllChecked(object obj)
        {
            bool? result = await IocMessage.Dialog.Show(Resources.Dialog_Message_ConfirmDelete, x =>
            {
                x.DialogButton = DialogButton.SumitAndCancel;
            });
            if (result != true)
                return;

            List<TEntity> checks = this.ObservableSource.Where(k => k.IsSelected).Select(x => x.Model)?.ToList();
            this.DeleteAll(checks);
        }

        protected void DeleteAll(IEnumerable<TEntity> entities)
        {
            int r = this.Repository == null ? 1 : this.Repository.DeleteRange(entities.ToArray());
            if (r > 0)
            {
                //IocMessage.Snack?.ShowInfo($"删除成功,共计{entities.Count()}条");
                IocMessage.ShowSnackInfo(Resources.Common_OperationSucceeded);
                this.ObservableSource.RemoveAll(x => entities.Contains(x.Model));
            }
            else
            {
                IocMessage.ShowSnackInfo(Resources.Common_OperationFailed);
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
