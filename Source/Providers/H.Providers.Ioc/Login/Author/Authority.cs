// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public class Authority : IAuthority
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public bool IsAuthority { get; set; }
    }
}