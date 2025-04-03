using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Copper;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Industrial;
using H.Themes.Colors.Mineral;
using H.Themes.Colors.Platform;
using H.Themes.Colors.Purple;
using H.Themes.Colors.Technology;
using H.Themes.Colors.Web;

namespace H.ApplicationBases.Default
{
    public abstract partial class ThemesApplicationBase : ModulesApplicationBase
    {
        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseAllThemes();
        }
    }
}
