using H.Extensions.Setting;
using Microsoft.Extensions.Options;

namespace H.Modules.Login
{
    public class LoginOptions : IocOptionInstance<LoginOptions>, IOptions<LoginOptions>
    {
        public string MyName { get; set; } = "我是名称";

        LoginOptions IOptions<LoginOptions>.Value
        {
            get { return this; }
        }
    }
}
