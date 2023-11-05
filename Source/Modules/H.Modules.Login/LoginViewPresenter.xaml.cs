using H.Providers.Ioc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace H.Modules.Login
{
    public class LoginViewPresenter : ILoginViewPresenter
    {
        public LoginViewPresenter(IOptions<LoginOptions> options)
        {
            this.MyName = options.Value.MyName;
        }

        public string MyName { get; set; }
    }
}
