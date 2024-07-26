using H.Extensions.Setting;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Services.Common;

namespace H.Extensions.TypeLicense;

[Display(Name = "组件许可设置", GroupName = SettingGroupNames.GroupSystem, Description = "组件许可设置的信息")]
public class TypeLicenseOptions : IocOptionInstance<TypeLicenseOptions>
{

}
