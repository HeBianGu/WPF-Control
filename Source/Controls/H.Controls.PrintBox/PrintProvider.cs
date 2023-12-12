using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace H.Controls.PrintBox
{
    public static class PrintProvider
    {
        public static FixedDocument GetFixedDocument(FrameworkElement toPrint, PrintDialog printDialog, double margin = 20)
        {
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            FixedDocument fixedDoc = new FixedDocument();
            toPrint.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));
            Size size = toPrint.DesiredSize;
            double yOffset = 0;
            while (yOffset < size.Height)
            {
                VisualBrush vb = new VisualBrush(toPrint);
                vb.Stretch = Stretch.None;
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.None;
                vb.Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height);

                double bottom = yOffset + visibleSize.Height;
                FrameworkElement find = null;
                VisualTreeHelper.HitTest(toPrint, null, f =>
                {
                    if (f.VisualHit is StackPanel)
                        return HitTestResultBehavior.Continue;
                    if (f.VisualHit is FrameworkElement frameworkElement)
                    {
                        find = frameworkElement;
                        return HitTestResultBehavior.Stop;
                    }
                    return HitTestResultBehavior.Continue;
                }, new GeometryHitTestParameters(new LineGeometry(new Point(0, bottom), new Point(visibleSize.Width, bottom))));
                Point? point = null;
                if (find != null)
                    point = toPrint.TranslatePoint(new Point(0, bottom), find);

                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;

                Canvas canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += canvas.Height;
            }
            return fixedDoc;
        }

        public static List<FixedPage> GetFixedPages(FrameworkElement toPrint, PrintDialog printDialog, double margin = 20)
        {
            List<FixedPage> pages = new List<FixedPage>();
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            toPrint.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));

            Size size = toPrint.DesiredSize;
            double yOffset = 0;
            while (yOffset < size.Height)
            {
                VisualBrush vb = new VisualBrush(toPrint);
                vb.Stretch = Stretch.None;
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.None;
                vb.Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height);

                double bottom = yOffset + visibleSize.Height;
                FrameworkElement find = null;
                VisualTreeHelper.HitTest(toPrint, null, f =>
                {
                    if (f.VisualHit is StackPanel)
                        return HitTestResultBehavior.Continue;
                    if (f.VisualHit is Border)
                        return HitTestResultBehavior.Continue;
                    if (f.VisualHit is FrameworkElement frameworkElement)
                    {
                        find = frameworkElement;
                        return HitTestResultBehavior.Stop;
                    }
                    return HitTestResultBehavior.Continue;
                }, new GeometryHitTestParameters(new LineGeometry(new Point(0, bottom), new Point(visibleSize.Width, bottom))));
                Point? point = null;
                if (find != null)
                    point = toPrint.TranslatePoint(new Point(0, bottom), find);
                FixedPage page = new FixedPage();
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;

                Canvas canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += canvas.Height;
                pages.Add(page);
            }
            return pages;
        }
    }
}
