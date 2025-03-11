using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace H.Mvvm
{
    public abstract class DisplayMarkupCommandBase : AsyncMarkupCommandBase, IIconable, INameable, IDescriptionable
    {
        protected DisplayMarkupCommandBase()
        {
            var d = this.GetType().GetCustomAttribute<DisplayAttribute>();
            if (d != null)
            {
                this.Name = d.Name;
                this.Description = d.Description;
            }

            var icon = this.GetType().GetCustomAttribute<IconAttribute>();
            this.Icon = icon?.Icon;
        }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}
