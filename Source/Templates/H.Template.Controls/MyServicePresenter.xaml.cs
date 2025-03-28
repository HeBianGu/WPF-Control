using H.Common.Attributes;
using H.Extensions.Setting;
using H.Iocable;
using H.Mvvm;
using H.Mvvm.ViewModels.Base;
using H.Services.Common;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace H.Template.Controls;
public interface IMyServicePresenter
{
    double MinHeight { get; set; }
}

public interface IMyServicePresenterOption : IMyServicePresenter
{

}

[Icon("\xE713")]
[Display(Name = "我的呈现", GroupName = "我的呈现分组", Description = "这是一个我的呈现的信息")]
public class MyServicePresenter : IocBindable<MyServicePresenter, IMyServicePresenter>, IMyServicePresenter, IMyServicePresenterOption
{
    private double _minHeight = 100;
    [DefaultValue(100)]
    [TypeConverter(typeof(LengthConverter))]
    [Display(Name = "最小高度", Description = "设置页面最小高度")]
    public double MinHeight
    {
        get { return _minHeight; }
        set
        {
            _minHeight = value;
            this.RaisePropertyChanged();
        }
    }
}

[Display(Name = "我的呈现设置", GroupName = SettingGroupNames.GroupStyle)]
internal class MyServicePresenterOption : IocOptionInstance<MyServicePresenterOption>, IMyServicePresenterOption
{
    private double _minHeight = 100;
    [DefaultValue(100)]
    [TypeConverter(typeof(LengthConverter))]
    [Display(Name = "最小高度", Description = "设置页面最小高度")]
    public double MinHeight
    {
        get { return _minHeight; }
        set
        {
            _minHeight = value;
            this.RaisePropertyChanged();
        }
    }
}

public static class Extention
{
    public static IServiceCollection AddMyServicePresenter(this IServiceCollection services, Action<IMyServicePresenterOption> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IMyServicePresenter, MyServicePresenter>());
        if (setupAction != null)
            services.Configure(setupAction);
        return services;
    }

    public static IApplicationBuilder UseMyServicePresenterSetting(this IApplicationBuilder builder, Action<IMyServicePresenterOption> option = null)
    {
        IocSetting.Instance.Add(MyServicePresenterOption.Instance);
        option?.Invoke(MyServicePresenterOption.Instance);
        return builder;
    }
}

