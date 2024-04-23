// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.IO;
using System.Reflection;
using System.Resources;

namespace H.Controls.PropertyGrid
{
    internal class ResourceHelper
    {
        internal static Stream LoadResourceStream(Assembly assembly, string resId)
        {
            string basename = System.IO.Path.GetFileNameWithoutExtension(assembly.ManifestModule.Name) + ".g";
            ResourceManager resourceManager = new ResourceManager(basename, assembly);

            // resource names are lower case and contain only forward slashes
            resId = resId.ToLower();
            resId = resId.Replace('\\', '/');
            return resourceManager.GetObject(resId) as Stream;
        }
    }
}
