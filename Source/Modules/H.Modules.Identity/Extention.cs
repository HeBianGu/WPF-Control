// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Extensions.DataBase.Repository;
global using H.Modules.Identity;
global using H.Services.Identity;
global using H.Services.Identity.User;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using System.Collections.Generic;
using H.Services.Identity.Author;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddUserViewPresenter(this IServiceCollection service)
        {
            service.AddSingleton<IRepositoryBindable<hi_dd_user>, RepositoryBindable<hi_dd_user>>();
            service.AddSingleton<IUserViewPresenter, UserViewPresenter>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLoginService(this IServiceCollection service)
        {
            service.AddSingleton<ILoginService, LoginService>();
        }

        public static IServiceCollection AddRegisterService(this IServiceCollection services, Action<IIdentifyOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IRegisterService, RegisterService>());
            if (setupAction != null)
                services.Configure(new Action<IdentifyOptions>(setupAction));
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddRoleViewPresenter(this IServiceCollection service)
        {
            service.AddSingleton<IRepositoryBindable<hi_dd_role>, RoleRepositoryViewModel>();
            service.AddSingleton<IRoleViewPresenter, RoleViewPresenter>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAuthorityViewPresenter(this IServiceCollection service)
        {
            service.AddSingleton<IRepositoryBindable<hi_dd_author>, RepositoryBindable<hi_dd_author>>();
            service.AddSingleton<IAuthorityViewPresenter, AuthorityViewPresenter>();
        }

        public static IApplicationBuilder UseIdentifyOptions(this IApplicationBuilder builder, Action<IIdentifyOptions> option = null)
        {
            IocSetting.Instance.Add(IdentifyOptions.Instance);
            option?.Invoke(IdentifyOptions.Instance);
            return builder;
        }

        public static void BuildIdentifySeed(this ModelBuilder modelBuilder)
        {
            List<hi_dd_author> authors = new List<hi_dd_author>();
            hi_dd_author author1 = new hi_dd_author();
            author1.ID = "{63860D6B-BD59-4620-919D-0DE51877676F}";
            author1.Name = "用户管理";
            authors.Add(author1);
            hi_dd_author author2 = new hi_dd_author();
            author2.ID = "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}";
            author2.Name = "角色管理";
            authors.Add(author2);

            List<hi_dd_role> roles = new List<hi_dd_role>();
            hi_dd_role role1 = new hi_dd_role();
            role1.ID = "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}";
            role1.Name = "管理员";
            role1.Code = "01";
            roles.Add(role1);
            hi_dd_role role2 = new hi_dd_role();
            role2.ID = "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}";
            role2.Name = "普通用户";
            role2.Code = "02";
            roles.Add(role2);

            List<hi_dd_user> users = new List<hi_dd_user>();
            hi_dd_user user1 = new hi_dd_user();
            user1.ID = "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}";
            user1.Account = "admin";
            user1.Password = "123456";
            user1.Name = "超级用户";
            user1.RoleID = role1.ID;
            users.Add(user1);
            hi_dd_user user2 = new hi_dd_user();
            user2.ID = "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}";
            user2.Account = "user";
            user2.Password = "123456";
            user2.Name = "HeBianGu";
            user2.RoleID = role2.ID;
            users.Add(user2);

            //role1.Authors.Add(author1);
            //role1.Authors.Add(author2);
            //author1.Roles.Add(role1);
            //author2.Roles.Add(role1);

            modelBuilder.Entity<hi_dd_author>().HasData(authors);
            modelBuilder.Entity<hi_dd_role>().HasData(roles);
            modelBuilder.Entity<hi_dd_user>().HasData(users);
        }

    }
}
