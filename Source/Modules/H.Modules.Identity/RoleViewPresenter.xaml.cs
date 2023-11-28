// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace H.Modules.Identity
{
    public class RoleEditPresenter : ModelViewModel<hi_dd_role>
    {
        public RoleEditPresenter(hi_dd_role model) : base(model)
        {
            this.Authors = Ioc.GetService<IStringRepository<hi_dd_author>>().GetList();
        }

        private List<hi_dd_author> _authors = new List<hi_dd_author>();
        public List<hi_dd_author> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                RaisePropertyChanged();
            }
        }
    }

    public class RoleRepositoryViewModel : RepositoryViewModel<hi_dd_role>
    {
        public override async Task Add(object obj)
        {
            var m = new hi_dd_role();
            RoleEditPresenter roleViewModel = new RoleEditPresenter(m);
            bool? dialog = await IocMessage.Dialog.Show(roleViewModel, null, DialogButton.Sumit, "新增");
            if (dialog != true)
            {
                IocMessage.Snack?.ShowInfo("取消操作");
                return;
            }
            await this.Add(m);
            this.OnCollectionChanged(obj);
        }

        public override async Task Edit(object obj)
        {
            hi_dd_role entity = this.GetEntity(obj);
            if (entity == null)
                return;
            RoleEditPresenter roleViewModel = new RoleEditPresenter(entity);
            bool? r = await IocMessage.Dialog.Show(roleViewModel, null, DialogButton.Sumit, "编辑");
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
        }
    }

    [Display(Name = "角色管理", GroupName = SystemSetting.GroupAuthority, Description = "应用此功能进行角色管理")]
    public class RoleViewPresenter : IRoleViewPresenter
    {


    }
}
