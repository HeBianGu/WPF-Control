


using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Markup;

namespace H.Controls.PrintBox
{
    public static class PageSizeNames
    {
        public static PageSize A0 { get; set; } = new PageSize(PageMediaSizeName.ISOA0, new Size(841, 1189), "A0");
        public static PageSize A1 { get; set; } = new PageSize(PageMediaSizeName.ISOA1, new Size(594, 841), "A1");
        public static PageSize A2 { get; set; } = new PageSize(PageMediaSizeName.ISOA2, new Size(420, 594), "A2");
        public static PageSize A3 { get; set; } = new PageSize(PageMediaSizeName.ISOA3, new Size(297, 420), "A3");
        public static PageSize A4 { get; set; } = new PageSize(PageMediaSizeName.ISOA4, new Size(210, 297), "A4");

        public static PageSize B0 { get; set; } = new PageSize(PageMediaSizeName.ISOB0, new Size(1000, 1414), "B0");
        public static PageSize B1 { get; set; } = new PageSize(PageMediaSizeName.ISOB1, new Size(707, 1000), "B1");
        public static PageSize B2 { get; set; } = new PageSize(PageMediaSizeName.ISOB2, new Size(500, 707), "B2");
        public static PageSize B3 { get; set; } = new PageSize(PageMediaSizeName.ISOB3, new Size(353, 500), "B3");
        public static PageSize B4 { get; set; } = new PageSize(PageMediaSizeName.ISOB4, new Size(250, 353), "B4");
    }

    public class PageSize
    {
        public PageSize(PageMediaSizeName sizeName, Size size, string name)
        {
            this.Name = name;
            this.SizeName = sizeName;
            this.SizeInInch = new Size(size.Width / 25.4, size.Height / 25.4);
            this.SizeDesc = size.Width.ToString() + " x " + size.Height.ToString() + " mm";
            this.Size = size;
            this.Rect = new Rect(new Point(), size);
        }

        public string Name { get; private set; }
        public string SizeDesc { get; private set; }
        public PageMediaSizeName SizeName { get; private set; }
        public Size SizeInInch {  get; private set; }

        public Size Size { get; private set; }

        public Rect Rect { get; private set; }

        private static readonly PageSize[] _defines;
        private static readonly Dictionary<PageMediaSizeName, PageSize> _sizeNameToSize
            = new Dictionary<PageMediaSizeName, PageSize>();

        static PageSize()
        {
            _defines = new PageSize[]
            {
                PageSizeNames.A0,
                PageSizeNames.A1,
                PageSizeNames.A2,
                PageSizeNames.A3,
                PageSizeNames.A4,
                PageSizeNames.B0,
                PageSizeNames.B1,
                PageSizeNames.B2,
                PageSizeNames.B3,
                PageSizeNames.B4,
            };

            foreach (PageSize md in _defines)
                _sizeNameToSize[md.SizeName] = md;
        }

        public static PageSize GetKnownSize(PageMediaSizeName sizeName)
        {
            PageSize size;
            _sizeNameToSize.TryGetValue(sizeName, out size);
            return size;
        }

        public static IEnumerable<PageSize> Defines => _defines;
    }
}
