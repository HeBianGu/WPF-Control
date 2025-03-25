using H.Services.Common.Crypt;
using Microsoft.Extensions.DependencyInjection;

namespace H.Extensions.Encryption;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddDESCryptService(this IServiceCollection service)
    {
        service.AddSingleton<ICryptService, DESCryptService>();
    }
}
