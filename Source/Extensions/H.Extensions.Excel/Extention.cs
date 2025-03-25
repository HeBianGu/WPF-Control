using H.Services.Common.Excel;
using Microsoft.Extensions.DependencyInjection;

namespace H.Extensions.Excel;

public static class Extention
{

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddNpoiService(this IServiceCollection service)
    {
        service.AddSingleton<IExcelService, NpoiService>();
    }
}
