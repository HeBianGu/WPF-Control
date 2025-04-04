using H.Services.Common.Theme;
using H.Services.Logger;

namespace H.Modules.Theme;
public class LoadThemeOptionsService : ILoadThemeOptionsService
{
    public bool Load(out string message)
    {
		try
		{
            return ThemeOptions.Instance.Load(out message);
        }
		catch (Exception ex)
		{
			message=ex.Message;
			IocLog.Instance?.Error(ex);
			return false;
		}
    }
}
