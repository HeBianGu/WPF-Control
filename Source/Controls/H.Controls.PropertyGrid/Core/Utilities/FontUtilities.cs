// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    internal class FontUtilities
    {
        internal static IEnumerable<FontFamily> Families
        {
            get
            {
#if !VS2008
                // Workaround for a WPF 4 bug.
                foreach (FontFamily font in Fonts.SystemFontFamilies)
                {
                    try
                    {
                        // In WPF 4, this will throw an exception.
                        LanguageSpecificStringDictionary throwAcess = font.FamilyNames;
                    }
                    catch
                    {
                        // It throws. Go to the next font family.
                        continue;
                    }

                    // If it does not throw, return the font.
                    yield return font;
                }
#else
      return Fonts.SystemFontFamilies;
#endif
            }
        }

        internal static IEnumerable<FontWeight> Weights
        {
            get
            {
                yield return FontWeights.Black;
                yield return FontWeights.Bold;
                yield return FontWeights.ExtraBlack;
                yield return FontWeights.ExtraBold;
                yield return FontWeights.ExtraLight;
                yield return FontWeights.Light;
                yield return FontWeights.Medium;
                yield return FontWeights.Normal;
                yield return FontWeights.SemiBold;
                yield return FontWeights.Thin;
            }
        }

        internal static IEnumerable<FontStyle> Styles
        {
            get
            {
                yield return FontStyles.Italic;
                yield return FontStyles.Normal;
            }
        }

        internal static IEnumerable<FontStretch> Stretches
        {
            get
            {
                yield return FontStretches.Condensed;
                yield return FontStretches.Expanded;
                yield return FontStretches.ExtraCondensed;
                yield return FontStretches.ExtraExpanded;
                yield return FontStretches.Normal;
                yield return FontStretches.SemiCondensed;
                yield return FontStretches.SemiExpanded;
                yield return FontStretches.UltraCondensed;
                yield return FontStretches.UltraExpanded;
            }
        }
    }
}
