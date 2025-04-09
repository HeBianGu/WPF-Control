using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mail;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Feedback;

[Icon(FontIcons.Feedback)]
[Display(Name = "用户反馈设置", GroupName = SettingGroupNames.GroupSystem, Description = "用户反馈设置的信息")]
public class FeedbackOptions : SmtpSendOptions<FeedbackOptions>, IFeedbackOptions
{

}
