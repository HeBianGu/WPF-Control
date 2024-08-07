﻿using H.Services.Common;
using H.Mvvm;
using System;

namespace H.Controls.TagBox
{
    public class CreateTagCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var service = Ioc.GetService<ITagService>();
            var tag = service.Create();
            tag.GroupName = parameter?.ToString();
            var r = await IocMessage.Form.ShowEdit(tag);
            if (r != true)
                return;
            service.Add(tag);
            service.Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<ITagService>();
        }
    }
}
