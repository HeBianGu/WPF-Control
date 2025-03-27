// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using H.Extensions.DataBase;
using H.Mvvm.ViewModels.Tree;
using H.Services.Logger;

namespace H.Extensions.DataBase.Repository
{
    public class TreeRepositoryBindable<TEntity> : RepositoryBindableBase<TreeNodeBase<TEntity>, TEntity>, ITreeRepositoryBindable<TEntity> where TEntity : StringEntityBase, ITreePath, new()
    {
        private TreeNodeBase<TEntity> _selectedTreeItem;
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
                System.Collections.Generic.List<TEntity> datas = this.Repository.GetList();
                System.Collections.Generic.List<string> ssss = datas.Select(x => x.GetFullPath()).ToList();
                System.Collections.Generic.List<TEntity> roots = datas.Where(x => !x.GetFullPath().Contains(Path.DirectorySeparatorChar)).ToList();
                System.Collections.Generic.List<TreeNodeBase<TEntity>> rootVms = roots.Select(x => new TreeNodeBase<TEntity>(x)).ToList();

                Action<TreeNodeBase<TEntity>> builder = null;
                builder = p =>
                {
                    System.Collections.Generic.IEnumerable<TEntity> items = datas.Where(x => Path.GetDirectoryName(x.GetFullPath()) == p.Model.GetFullPath());
                    System.Collections.Generic.IEnumerable<TreeNodeBase<TEntity>> vms = items.Select(x => new TreeNodeBase<TEntity>(x));
                    foreach (TreeNodeBase<TEntity> vm in vms)
                    {
                        p.AddNode(vm);
                        builder.Invoke(vm);
                    }
                };

                foreach (TreeNodeBase<TEntity> root in rootVms)
                {
                    builder.Invoke(root);
                }
                this.Collection.Load(rootVms);
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
                IocMessage.Dialog.Show("加载数据错误:" + ex.Message);
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
                foreach (TEntity m in ms)
                {
                    if (this.SelectedTreeItem == null)
                        this.Collection.Add(new TreeNodeBase<TEntity>(m));
                    else
                        this.SelectedTreeItem.Nodes.Add(new TreeNodeBase<TEntity>(m));
                }
                IocMessage.ShowSnackInfo("新增成功");
                return;
            }
            int r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                foreach (TEntity m in ms)
                {
                    if (this.SelectedTreeItem == null)
                        this.Collection.Add(new TreeNodeBase<TEntity>(m));
                    else
                        this.SelectedTreeItem.Nodes.Add(new TreeNodeBase<TEntity>(m));
                }
                IocMessage.ShowSnackInfo("新增成功");
            }
            else
            {
                IocMessage.ShowSnackInfo("新增失败,数据库保存错误");
            }
        }

        public override async Task Delete(object obj)
        {
            TreeNodeBase<TEntity> entity = obj as TreeNodeBase<TEntity>;
            if (entity == null)
                return;
            bool? result = await IocMessage.Dialog.Show("确定删除数据？", x =>
            {
                x.Title = "提示";
                x.DialogButton = DialogButton.SumitAndCancel;
            });
            if (result != true)
                return;

            System.Collections.Generic.List<TreeNodeBase<TEntity>> all = entity.FindAll().ToList();
            all.Add(entity);
            foreach (TreeNodeBase<TEntity> item in all)
            {
                await this.Repository.DeleteAsync(item.Model);
            }
            if (entity.Parent == null)
                this.Collection.Remove(entity);
            else
                entity.Parent.Nodes.Remove(entity);
            IocMessage.ShowSnackInfo("删除成功");
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
            TEntity m = new TEntity();
            if (this.SelectedTreeItem != null)
                m.SetPath(this.SelectedTreeItem.Model.GetFullPath());
            bool? dialog = await IocMessage.Form.ShowEdit(this.GetAddModel(m), x => x.Title = "新增");
            if (dialog != true)
            {
                IocMessage.ShowSnackInfo("取消操作");
                return;
            }
            await this.Add(m);
            this.OnCollectionChanged(obj);
        }
    }
}
