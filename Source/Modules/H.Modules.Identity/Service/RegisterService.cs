// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Providers.Ioc;
using System;

namespace H.Modules.Identity
{
    internal class RegisterService : IRegisterService
    {
        private Random random = new Random();
        public bool Register(string mail, string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            //Thread.Sleep(2000);
            message = "当前手机号已经注册";
            ////Operationner.Instance?.Log(result.ToString(), message);
            //OparationViewPresenterProxy.Instance?.Log(message, null, null, false);
            return result;
        }

        public bool ResetPassword(string mail, string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            //Thread.Sleep(2000);
            message = "操作超时";
            ////Operationner.Instance?.Log(result.ToString(), message);
            //OparationViewPresenterProxy.Instance?.Log(message, message, null, result);
            return result;
        }
    }
}
