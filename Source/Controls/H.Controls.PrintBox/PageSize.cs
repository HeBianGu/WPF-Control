


using System.Collections.Generic;
using System.Printing;
using System.Windows;

namespace H.Controls.PrintBox
{
    public class PageSize
    {
        public PageSize(PageMediaSizeName sizeName, Size size, string name)
        {
            this.Name = name;
            this.SizeName = sizeName;
            this.SizeInInch = new Size(size.Width / 25.4, size.Height / 25.4);
            this.SizeDesc = size.Width.ToString() + " x " + size.Height.ToString() + " mm";
        }

        public string Name { get; set; }
        public string SizeDesc { get; set; }
        public PageMediaSizeName SizeName { get; set; }
        public Size SizeInInch { get; set; }

        private static readonly PageSize[] _defines;
        private static readonly Dictionary<PageMediaSizeName, PageSize> _sizeNameToSize
            = new Dictionary<PageMediaSizeName, PageSize>();

        static PageSize()
        {
            _defines = new PageSize[]
            {
                new PageSize( PageMediaSizeName.ISOA0, new Size(841, 1189), "A0"),
                new PageSize( PageMediaSizeName.ISOA1, new Size(594, 841),  "A1" ),
                new PageSize( PageMediaSizeName.ISOA2, new Size(420, 594), "A2" ),
                new PageSize( PageMediaSizeName.ISOA3, new Size(297, 420),"A3" ),
                new PageSize( PageMediaSizeName.ISOA4, new Size(210, 297), "A4" ),

                new PageSize( PageMediaSizeName.ISOB0, new Size(1000, 1414),"B0"),
                new PageSize( PageMediaSizeName.ISOB1, new Size(707, 1000), "B1"),
                new PageSize( PageMediaSizeName.ISOB2, new Size(500, 707),"B2"),
                new PageSize( PageMediaSizeName.ISOB3, new Size(353, 500), "B3"),
                new PageSize( PageMediaSizeName.ISOB4, new Size(250, 353),"B4"),
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
