﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Providers.Mvvm;
using System;
using System.Diagnostics;
using System.IO;

namespace H.Extensions.Command
{
    public class ProcessCommand : MarkupCommandBase
    { 
        public override bool CanExecute(object parameter)
        {
            if (parameter == null) return false;
            bool result = File.Exists(parameter?.ToString()) || Directory.Exists(parameter?.ToString());
            if (parameter.ToString().ToUpper().StartsWith("http") == true) return true;
            return result;
        }

        public override void Execute(object parameter)
        {
            //System.Diagnostics.Process.Start(parameter?.ToString());
            Process.Start(new ProcessStartInfo(parameter?.ToString()) { UseShellExecute = true });

        }
    }
}
