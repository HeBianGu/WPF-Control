// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.DataBase;
global using H.Services.Message;
global using H.Services.Message.Dialog;
global using H.Services.Setting;
using H.Common;
using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Globalization.Properties;

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
                x.Title = Resources.Common_Add;
            });
            if (dialog != true)
            {
                IocMessage.ShowSnackInfo(Resources.Common_OperationCancel);
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
            if (entity.ID == "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}")
            {
                await IocMessage.ShowDialogMessage("管理员角色只读不可以编辑".GetStringResx(this.GetType().Assembly, "Message_CannotEditAdmin"));
                return;
            }
            RoleEditPresenter roleViewModel = new RoleEditPresenter(entity);
            bool? r = await IocMessage.Dialog.Show(roleViewModel, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = Resources.Common_Edit;
            });
            if (r != true)
            {
                IocMessage.ShowSnackInfo(Resources.Common_OperationCancel);
                return;
            }

            int rs = this.Repository == null ? 1 : await this.Repository.SaveAsync();

            if (rs > 0)
            {
                if (this.UseMessage)
                    IocMessage.ShowSnackInfo(Resources.Common_SaveSucceeded);
            }
            else
            {
                IocMessage.ShowSnackInfo(Resources.Common_SaveFailed);
            }
        }

        public override async Task Delete(object obj)
        {
            hi_dd_role entity = this.GetEntity(obj);
            if (entity == null)
                return;
            if (entity.ID == "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}")
            {
                await IocMessage.ShowDialogMessage("管理员角色只读不可以编辑");
                return;
            }
            await base.Delete(obj);
        }
    }

    [Icon(FontIcons.People)]
    [Display(Name = "角色管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行角色管理")]
    public class RoleViewPresenter : IRoleViewPresenter
    {

    }
}
