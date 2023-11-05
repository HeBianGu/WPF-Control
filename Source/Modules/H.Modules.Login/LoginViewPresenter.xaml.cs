using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Modules.Login
{
    public interface ILoginViewPresenter
    {

    }

    public class LoginViewPresenter : ILoginViewPresenter
    {
        public LoginViewPresenter(IOptions<LoginOptions> options)
        {
            //var options = Ioc.GetService<IOptions<LoginOptions>>();
            this.Name = options.Value.MyName;
        }

        public string Name { get; set; }
    }
}
