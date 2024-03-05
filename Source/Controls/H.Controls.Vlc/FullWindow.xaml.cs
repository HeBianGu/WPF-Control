// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
