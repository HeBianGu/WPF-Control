
using System.IO;
using System.Reflection;
using System.Resources;

namespace H.Controls.ZoomBox
{
    internal class ResourceHelper
    {
        internal static Stream LoadResourceStream(Assembly assembly, string resId)
        {
            string basename = Path.GetFileNameWithoutExtension(assembly.ManifestModule.Name) + ".g";
            ResourceManager resourceManager = new ResourceManager(basename, assembly);

            // resource names are lower case and contain only forward slashes
            resId = resId.ToLower();
            resId = resId.Replace('\\', '/');
            return resourceManager.GetObject(resId) as Stream;
        }
    }
}
