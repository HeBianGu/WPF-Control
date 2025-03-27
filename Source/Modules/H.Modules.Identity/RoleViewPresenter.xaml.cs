// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Extensions.DataBase;
global using H.Services.Identity.Role;
global using H.Services.Setting;
global using H.Services.Message;
global using H.Services.Message.Dialog;

namespace H.Modules.Identity
{
    public class RoleEditPresenter : ModelBindable<hi_dd_role>
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

    public class RoleRepositoryViewModel : RepositoryBindable<hi_dd_role>
    {
        public override async Task Add(object obj)
        {
            hi_dd_role m = new hi_dd_role();
            RoleEditPresenter roleViewModel = new RoleEditPresenter(m);
            bool? dialog = await IocMessage.Dialog.Show(roleViewModel, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = "新增";
            });
            if (dialog != true)
            {
                IocMessage.ShowSnackInfo("取消操作");
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
            bool? r = await IocMessage.Dialog.Show(roleViewModel, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = "编辑";
            });
            if (r != true)
            {
                IocMessage.ShowSnackInfo("取消操作");
                return;
            }

            int rs = this.Repository == null ? 1 : await this.Repository.SaveAsync();

            if (rs > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo("保存成功");
            }
            else
            {
                IocMessage.ShowSnackInfo("保存失败，数据库保存错误");
            }
        }
    }

    [Display(Name = "角色管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行角色管理")]
    public class RoleViewPresenter : IRoleViewPresenter
    {


    }
}
