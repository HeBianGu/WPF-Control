using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Feedback
{
    [Display(Name = " 用户反馈设置", GroupName = SettingGroupNames.GroupSystem, Description = "用户反馈设置的信息")]
    public class FeedbackOptions : IocOptionInstance<FeedbackOptions>
    {
        private string _mailAccount;
        [Required]
        [DefaultValue("HeBianGu2024@163.com")]
        [Display(Name = "接收用户反馈的邮箱")]
        [ReadOnly(true)]
        public string MailAccount
        {
            get { return _mailAccount; }
            set
            {
                _mailAccount = value;
                RaisePropertyChanged();
            }
        }
    }

}
