// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common;
using Microsoft.Extensions.Options;
using System;
using System.Text.RegularExpressions;
using H.Extensions.DataBase;
using H.Services.Operation;

namespace H.Modules.Identity
{
    internal class RegisterService : IRegisterService
    {
        private readonly IOptions<IdentifyOptions> _options;
        private readonly IStringRepository<hi_dd_user> _repository;
        public RegisterService(IStringRepository<hi_dd_user> repository, IOptions<IdentifyOptions> options)
        {
            _repository = repository;
            _options = options;
        }

        public bool Register(string mail, string account, string password, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                message = "用户和密码不能为空";
                return false;
            }

            var hasMail = _repository.FirstOrDefaultAsync(x => x.Mail == mail && !string.IsNullOrEmpty(mail)).Result;
            if (hasMail != null)
            {
                message = "该邮箱已经注册过用户";
                return false;
            }

            var hasUser = _repository.FirstOrDefaultAsync(x => x.Account == account).Result;
            if (hasUser != null)
            {
                message = "该账号已经注册";
                return false;
            }

            if (Regex.Match(account, @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$").Success == false)
            {
                message = "账号不合法，字母开头，允许5-16字节，允许字母数字下划线";
                return false;
            }

            if (Regex.Match(password, @"^[0-9]{5,17}$").Success == false)
            {
                message = "密码不合法，以字母开头，长度在6~18之间，只能包含字母、数字和下划线";
                return false;
            }

            hi_dd_user user = new hi_dd_user()
            {
                Account = account,
                Mail = mail,
                Password = password,
                Enable = !_options.Value.UseAdiminCheckOnRegister,
                LicenseDeadline = DateTime.Now.Add(_options.Value.UserLicenseDefaultTryTime)
            };
            _repository.Insert(user, true);
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("用户注册", $"{user.Account}");
            return true;


        }

        public bool ResetPassword(string mail, string account, string password, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                message = "用户和密码不能为空";
                return false;
            }

            if (Regex.Match(password, @"^[0-9]{5,17}$").Success == false)
            {
                message = "密码不合法，以字母开头，长度在6~18之间，只能包含字母、数字和下划线";
                return false;
            }

            if (!string.IsNullOrEmpty(mail))
            {
                var hasMail = _repository.FirstOrDefaultAsync(x => x.Mail == mail).Result;
                if (hasMail == null)
                {
                    message = "邮箱不存在注册用户";
                    return false;
                }
            }

            var hasUser = _repository.FirstOrDefaultAsync(x => x.Account == account).Result;
            if (hasUser == null)
            {
                message = "该账号不存在";
                return false;
            }

            hasUser.Password = password;
            //_repository.Update(hasUser,true);
            _repository.Save();
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("重置密码", $"{hasUser.Account}");
            return true;
        }
    }
}
