// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Presenters.ParameterInfo.Base;

namespace H.Presenters.ParameterInfo.Presenters;

public class ParameterInfosPresenter : DisplayBindableBase
{
    private readonly IParameters _parameters;
    public ParameterInfosPresenter(IParameters parameters)
    {
        this._parameters = parameters;
        this.UpdateParameterInfos();
    }

    private void UpdateParameterInfos()
    {
        this.ParameterInfoPresenters = this._parameters.GetParameterInfos().Select(x => new ParameterInfoPresenter(this._parameters, x)).OfType<IParameterInfoPresenter>().ToObservable();
    }
    private ObservableCollection<IParameterInfoPresenter> _ParameterInfoPresenters = new ObservableCollection<IParameterInfoPresenter>();

    public ObservableCollection<IParameterInfoPresenter> ParameterInfoPresenters
    {
        get { return _ParameterInfoPresenters; }
        set
        {
            _ParameterInfoPresenters = value;
            RaisePropertyChanged();
        }
    }

    //[Icon(FontIcons.Setting)]
    //[Display(Name = "设置设备参数", GroupName = "参数设置", Description = "设置字符串参数")]
    //public DisplayCommand SetParamterCommand => new DisplayCommand(async x =>
    //{
    //    var r = await IocMessage.Form.ShowEdit(_setparameter, x => x.Title = "设置设备参数");
    //    if (r != true)
    //        return;
    //    var cr = await _setparameter.SetCameraParameterAsync(this._camera);
    //    if (!cr.Succeeded)
    //    {
    //        await IocMessage.ShowDialogMessage(cr.Message);
    //        this.Message = cr.Message;
    //        return;
    //    }
    //    await this.UpdateCameraParameterInfos();
    //}, x => this.IsOpen && !this.IsAcquiring);


    //[Icon(FontIcons.Add)]
    //[Display(Name = "添加设备参数", GroupName = "参数设置", Description = "设置字符串参数")]
    //public DisplayCommand GetParamterCommand => new DisplayCommand(async x =>
    //{
    //    var r = await IocMessage.Form.ShowEdit(_getparameter, x => x.Title = "添加设备参数");
    //    if (r != true)
    //        return;
    //    var info = _getparameter.GetCameraParameterInfo(this._camera);
    //    if (info == null)
    //    {
    //        await IocMessage.ShowDialogMessage("添加设备参数错误");
    //        this.Message = "添加设备参数错误";
    //        return;
    //    }
    //    this._customCameraParameterInfo.Add(info);
    //    await this.UpdateCameraParameterInfos();
    //}, x => this.IsOpen && !this.IsAcquiring);

}


