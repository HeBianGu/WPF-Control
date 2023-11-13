// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace H.Extensions.ViewModel
{

    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TreeRepositoryViewModel<TEntity> : RepositoryViewModelBase<TreeNodeBase<TEntity>, TEntity>, ITreeRepositoryViewModel<TEntity> where TEntity : StringEntityBase, ITreePath, new()
    {
        private TreeNodeBase<TEntity> _selectedTreeItem;
        /// <summary> 说明  </summary>
        public TreeNodeBase<TEntity> SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set
            {
                _selectedTreeItem = value;
                RaisePropertyChanged();
            }
        }

        public override void RefreshData(params string[] includes)
        {
            this.Collection.SelectedItem = null;
            this.IsBusy = true;
            try
            {
                var datas = this.Repository.GetList();
                var ssss = datas.Select(x => x.GetFullPath()).ToList();
                var roots = datas.Where(x => !x.GetFullPath().Contains(Path.DirectorySeparatorChar)).ToList();
                var rootVms = roots.Select(x => new TreeNodeBase<TEntity>(x)).ToList();

                Action<TreeNodeBase<TEntity>> builder = null;
                builder = p =>
                {
                    var items = datas.Where(x => Path.GetDirectoryName(x.GetFullPath()) == p.Model.GetFullPath());
                    var vms = items.Select(x => new TreeNodeBase<TEntity>(x));
                    foreach (var vm in vms)
                    {
                        p.AddNode(vm);
                        builder.Invoke(vm);
                    }
                };

                foreach (var root in rootVms)
                {
                    builder.Invoke(root);
                }

                //var collection = this.Repository.GetList().Select(x => new TreeNodeBase<TEntity>(x));
                //this.Collection = collection.Select(x => new SelectViewModel<TEntity>(x)).ToObservable();
                this.Collection.Load(rootVms);
            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
                IocMessage.Dialog.ShowMessage("加载数据错误:" + ex.Message);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public override async Task Add(params TEntity[] ms)
        {
            if (this.Repository == null)
            {
                foreach (var m in ms)
                {
                    if (this.SelectedTreeItem == null)
                        this.Collection.Add(new TreeNodeBase<TEntity>(m));
                    else
                        this.SelectedTreeItem.Nodes.Add(new TreeNodeBase<TEntity>(m));
                }
                IocMessage.Snack?.ShowTime("新增成功");
                return;
            }
            var r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                foreach (var m in ms)
                {
                    if (this.SelectedTreeItem == null)
                        this.Collection.Add(new TreeNodeBase<TEntity>(m));
                    else
                        this.SelectedTreeItem.Nodes.Add(new TreeNodeBase<TEntity>(m));
                }
                IocMessage.Snack?.ShowTime("新增成功");
            }
            else
            {
                IocMessage.Snack?.ShowTime("新增失败,数据库保存错误");
            }
        }

        public override async Task Delete(object obj)
        {
            TreeNodeBase<TEntity> entity = obj as TreeNodeBase<TEntity>;
            if (entity == null)
                return;
            var result = await IocMessage.Dialog.ShowMessage("确定删除数据？", "提示", DialogButton.SumitAndCancel);
            if (result != true)
                return;

            var all = entity.FindAll().ToList();
            all.Add(entity);
            foreach (var item in all)
            {
                await this.Repository.DeleteAsync(item.Model);
            }
            if (entity.Parent == null)
                this.Collection.Remove(entity);
            else
                entity.Parent.Nodes.Remove(entity);
            IocMessage.Snack?.ShowTime("删除成功");
            this.SelectedTreeItem = this.Collection.FirstOrDefault(x => true);
            this.OnCollectionChanged(obj);
        }

        public override async Task Clear(object obj)
        {
            await base.Clear(obj);
            this.SelectedTreeItem = this.Collection.FirstOrDefault(x => true);
        }

        public override async Task Add(object obj)
        {
            var m = new TEntity();
            if (this.SelectedTreeItem != null)
                m.SetPath(this.SelectedTreeItem.Model.GetFullPath());
            bool? dialog = await IocMessage.Form.ShowEdit(this.GetAddModel(m), null, null,null, "新增");
            if (dialog != true)
            {
                IocMessage.Snack?.ShowTime("取消操作");
                return;
            }
            await this.Add(m);
            this.OnCollectionChanged(obj);
        }
    }

    public interface ITreePath
    {
        string GetFullPath();
        void SetPath(string path);
    }
}
