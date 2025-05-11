// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        public static void AddDefaultMessages(this IServiceCollection services)
        {
            services.AddAdornerDialogMessage();
            //services.AddWindowDialogMessage();
            services.AddWindowMessage();
            services.AddFormMessageService();
            services.AddNoticeMessage();
            services.AddSnackMessage();
            services.AddIOFolderDialogService();
        }
    }
}
