using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.TypeLicense;

[Display(Name = "组件许可设置", GroupName = SettingGroupNames.GroupSystem, Description = "组件许可设置的信息")]
public class TypeLicenseOptions : IocOptionInstance<TypeLicenseOptions>, ITypeLicenseOptions
{

}
