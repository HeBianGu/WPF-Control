// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Input;

namespace H.Controls.Vlc
{
    /// <summary>
    /// Interaction logic for FullWindow.xaml
    /// </summary>
    public partial class FullWindow : Window
    {
        public FullWindow()
        {
            InitializeComponent();

            CommandBinding binding = new CommandBinding(SystemCommands.CloseWindowCommand);

            binding.Executed += (l, k) =>
              {
                  this.Close();
              };
            this.CommandBindings.Add(binding);
        }

    }
}
