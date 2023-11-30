








using System;
using System.Windows;

namespace H.Controls.Dock.Themes
{
    /// <summary>Defines a base class to implement a method for storing the current H.Controls.Dock theme
    /// that provides a XAML Uri pointing to a <see cref="ResourceDictionary"/>.
    /// 
    /// This class can be used to create customized themes by loading a <see cref="ResourceDictionary"/>
    /// from an existing theme (by using theme.GetResourceUri()), and then replacing some key colors
    /// (typically the "Accent" colors).
    /// 
    /// See Issue https://github.com/Dirkster99/H.Controls.Dock/issues/189 for more details.</summary>
    public abstract class DictionaryTheme : Theme
    {
        #region Constructors
        /// <summary>Class constructor</summary>
        public DictionaryTheme()
        {
        }

        /// <summary>Class constructor from theme specific resource dictionary.</summary>
        /// <param name="themeResourceDictionary"></param>
        public DictionaryTheme(ResourceDictionary themeResourceDictionary)
        {
            this.ThemeResourceDictionary = themeResourceDictionary;
        }

        #endregion Constructors

        /// <summary>Gets the resource dictionary that is associated with this H.Controls.Dock theme.</summary>
        public ResourceDictionary ThemeResourceDictionary { get; private set; }

        /// <summary>Gets the <see cref="Uri"/> of the XAML that contains the definition for this H.Controls.Dock theme.</summary>
        /// <returns><see cref="Uri"/> of the XAML that contains the definition for this custom H.Controls.Dock theme</returns>
        public override Uri GetResourceUri()
        {
            return null;
        }
    }
}
