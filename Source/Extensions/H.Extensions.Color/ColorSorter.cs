namespace H.Extensions.Color;

public class ColorSorter : IComparer<System.Windows.Media.Color>
{
    public int Compare(System.Windows.Media.Color firstItem, System.Windows.Media.Color secondItem)
    {
        System.Windows.Media.Color colorItem1 = (System.Windows.Media.Color)firstItem;
        System.Windows.Media.Color colorItem2 = (System.Windows.Media.Color)secondItem;
        System.Drawing.Color drawingColor1 = System.Drawing.Color.FromArgb(colorItem1.A, colorItem1.R, colorItem1.G, colorItem1.B);
        System.Drawing.Color drawingColor2 = System.Drawing.Color.FromArgb(colorItem2.A, colorItem2.R, colorItem2.G, colorItem2.B);

        double hueColor1 = Math.Round((double)drawingColor1.GetHue(), 3);
        double hueColor2 = Math.Round((double)drawingColor2.GetHue(), 3);

        if (hueColor1 > hueColor2)
            return 1;
        else if (hueColor1 < hueColor2)
            return -1;
        else
        {
            double satColor1 = Math.Round((double)drawingColor1.GetSaturation(), 3);
            double satColor2 = Math.Round((double)drawingColor2.GetSaturation(), 3);

            if (satColor1 > satColor2)
                return 1;
            else if (satColor1 < satColor2)
                return -1;
            else
            {
                double brightColor1 = Math.Round((double)drawingColor1.GetBrightness(), 3);
                double brightColor2 = Math.Round((double)drawingColor2.GetBrightness(), 3);

                if (brightColor1 > brightColor2)
                    return 1;
                else if (brightColor1 < brightColor2)
                    return -1;
            }
        }

        return 0;
    }
}
