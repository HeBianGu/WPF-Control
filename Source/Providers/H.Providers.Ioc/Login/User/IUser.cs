// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface IUser
    {
        string ID { get; }
        string Account { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        bool IsValid(string authorId);
    }
}