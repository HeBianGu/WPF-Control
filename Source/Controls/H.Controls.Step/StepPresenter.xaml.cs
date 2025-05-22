// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.ViewModels.Base;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.Windows.Controls;

namespace H.Controls.Step
{
    [Display(Name = "步骤")]
    public class StepPresenter : DisplayBindableBase
    {
        private Orientation _orientation = Orientation.Horizontal;
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IStepItemPresenter> _collection = new ObservableCollection<IStepItemPresenter>();
        public ObservableCollection<IStepItemPresenter> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
