global using H.Services.Message;
using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace H.Controls.PrintBox
{
    public partial class PrintBox : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PrintBox), "S.PrintBox.Default");

        static PrintBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrintBox), new FrameworkPropertyMetadata(typeof(PrintBox)));
        }
        PrintDialog _printDialog = new PrintDialog();
        public PrintBox()
        {
            PrintCapabilities capabilities = null;
            try
            {
                capabilities = _printDialog.PrintQueue.GetPrintCapabilities(_printDialog.PrintTicket);
            }
            catch (Exception)
            {
                IocMessage.Dialog.Show("请先设置系统默认打印机");
                throw;
            }
            this.PrintableAreaWidth = _printDialog.PrintableAreaWidth;
            this.PrintableAreaHeight = _printDialog.PrintableAreaHeight;
            this.ExtentWidth = capabilities.PageImageableArea.ExtentWidth;
            this.ExtentHeight = capabilities.PageImageableArea.ExtentHeight;

            {
                CommandBinding binding = new CommandBinding(PrintCommands.PrintView);
                binding.Executed += (l, k) =>
                {
                    var fixedDoc = this.GetFixedDocument();
                    DocumentViewPresenter presenter = new DocumentViewPresenter();
                    presenter.Document = fixedDoc;
                    IocMessage.Dialog.Show(presenter, x =>
                    {
                        x.Padding = new Thickness(50);
                        x.Title = "打印预览";
                    });

                };
                this.CommandBindings.Add(binding);
            }
            {
                CommandBinding binding = new CommandBinding(PrintCommands.PrintSetting);
                binding.Executed += (l, k) =>
                {
                    _printDialog.ShowDialog();
                    this.PrintableAreaWidth = _printDialog.PrintableAreaWidth;
                    this.PrintableAreaHeight = _printDialog.PrintableAreaHeight;
                    this.ExtentWidth = capabilities.PageImageableArea.ExtentWidth;
                    this.ExtentHeight = capabilities.PageImageableArea.ExtentHeight;
                };
                this.CommandBindings.Add(binding);
            }
            {
                CommandBinding binding = new CommandBinding(PrintCommands.Print);
                binding.Executed += (l, k) =>
                {
                    FixedDocument fixedDoc = this.GetFixedDocument();
                    IocMessage.ShowSnackInfo("正在打印..");
                    this._printDialog.PrintDocument(fixedDoc.DocumentPaginator, "打印");
                    IocMessage.ShowSnackInfo("打印完成..");
                };
                this.CommandBindings.Add(binding);
            }
            //{
            //    CommandBinding binding = new CommandBinding(PrintCommands.ParamSetting);
            //    binding.Executed += async (l, k) =>
            //    {
            //        //if (this.ItemsSource == null)
            //        //    return;
            //        //await IocMessage.Form.ShowEdit(PrintCommands.ParamSetting.Text, this.ItemsSource.OfType<object>().ToArray());
            //        await IocMessage.Dialog.Show(new PagerSizeViewPresenter());

            //    };
            //    this.CommandBindings.Add(binding);
            //}
            {
                CommandBinding binding = new CommandBinding(PrintCommands.PageSize);
                binding.Executed += async (l, k) =>
                {
                    var pageSize = new PagerSizeViewPresenter();
                    //pageSize.SelectedPagerSizeData = pageSize.Collection.Where(x=>x.);
                    var r = await IocMessage.Dialog.Show(pageSize);
                    if (r == true)
                    {
                        var size = pageSize.GetSize();
                        this.PrintableAreaWidth = size.Width;
                        this.PrintableAreaHeight = size.Height;
                    }

                };
                this.CommandBindings.Add(binding);
            }
        }

        FixedDocument GetFixedDocument()
        {
            FixedDocument fixedDoc = new FixedDocument();
            foreach (var item in this.Items)
            {
                List<PageContent> pageContents = null;
                if (item is PrintPage page)
                {
                    var element = page.GetElement() as FrameworkElement;
                    pageContents = this.GetPageContents(element, this._printDialog);
                    foreach (var pageContent in pageContents)
                    {
                        fixedDoc.Pages.Add(pageContent);
                    }
                }
                else if (item is FrameworkElement framework)
                {
                    pageContents = this.GetPageContents(framework, _printDialog);
                    foreach (var pageContent in pageContents)
                    {
                        fixedDoc.Pages.Add(pageContent);
                    }
                }
                else
                {
                    var container = this.ItemContainerGenerator.ContainerFromItem(item);
                    if (container is PrintPage print)
                    {
                        var element = print.GetElement() as FrameworkElement;
                        pageContents = this.GetPageContents(element, _printDialog);
                        foreach (var pageContent in pageContents)
                        {
                            fixedDoc.Pages.Add(pageContent);
                        }
                    }
                }

            }
            return fixedDoc;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PrintPage();
        }

        public double PrintableAreaWidth
        {
            get { return (double)GetValue(PrintableAreaWidthProperty); }
            set { SetValue(PrintableAreaWidthProperty, value); }
        }

        public static readonly DependencyProperty PrintableAreaWidthProperty =
            DependencyProperty.Register("PrintableAreaWidth", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

                control.RefreshPrintPageWidth();

            }));

        public Thickness FixedPageMargin
        {
            get { return (Thickness)GetValue(FixedPageMarginProperty); }
            set { SetValue(FixedPageMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FixedPageMarginProperty =
            DependencyProperty.Register("FixedPageMargin", typeof(Thickness), typeof(PrintBox), new FrameworkPropertyMetadata(new Thickness(10, 20, 10, 20), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is Thickness o)
                {

                }

                if (e.NewValue is Thickness n)
                {

                }
                control.RefreshPrintPageHeight();
                control.RefreshPrintPageWidth();

            }));

        public double PrintableAreaHeight
        {
            get { return (double)GetValue(PrintableAreaHeightProperty); }
            set { SetValue(PrintableAreaHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrintableAreaHeightProperty =
            DependencyProperty.Register("PrintableAreaHeight", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
                control.RefreshPrintPageHeight();

            }));

        private void RefreshPrintPageHeight()
        {
            this.PrintPageHeight = this.PrintableAreaHeight - this.FixedPageMargin.Top - this.FixedPageMargin.Bottom;
        }

        private void RefreshPrintPageWidth()
        {
            this.PrintPageWidth = this.PrintableAreaWidth - this.FixedPageMargin.Left - this.FixedPageMargin.Right;
        }

        public double PrintPageHeight
        {
            get { return (double)GetValue(PrintPageHeightProperty); }
            private set { SetValue(PrintPageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrintPageHeightProperty =
            DependencyProperty.Register("PrintPageHeight", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public double PrintPageWidth
        {
            get { return (double)GetValue(PrintPageWidthProperty); }
            private set { SetValue(PrintPageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrintPageWidthProperty =
            DependencyProperty.Register("PrintPageWidth", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public double ExtentWidth
        {
            get { return (double)GetValue(ExtentWidthProperty); }
            set { SetValue(ExtentWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtentWidthProperty =
            DependencyProperty.Register("ExtentWidth", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public double ExtentHeight
        {
            get { return (double)GetValue(ExtentHeightProperty); }
            set { SetValue(ExtentHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtentHeightProperty =
            DependencyProperty.Register("ExtentHeight", typeof(double), typeof(PrintBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public List<PageContent> GetPageContents(FrameworkElement toPrint, PrintDialog printDialog)
        {
            List<PageContent> pages = new List<PageContent>();
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            Size size = toPrint.DesiredSize;
            double current = this.FixedPageMargin.Top;
            double yOffset = 0;
            double xOffset = this.FixedPageMargin.Left;
            while (yOffset < size.Height)
            {
                VisualBrush vb = new VisualBrush(toPrint);
                vb.Stretch = Stretch.None;
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.None;
                vb.Viewbox = new Rect(xOffset, current + yOffset, visibleSize.Width, visibleSize.Height);

                double bottom = yOffset + visibleSize.Height - this.FixedPageMargin.Top - this.FixedPageMargin.Bottom;
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
                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;
                page.Margin = this.FixedPageMargin;

                Canvas canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height - this.FixedPageMargin.Top - this.FixedPageMargin.Bottom;
                if (point != null && visibleSize.Height != (point.Value.Y + this.FixedPageMargin.Top + this.FixedPageMargin.Bottom))
                    canvas.Height = visibleSize.Height - point.Value.Y - this.FixedPageMargin.Top - this.FixedPageMargin.Bottom;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += canvas.Height;
                pages.Add(pageContent);
            }
            return pages;
        }

        public bool UseRight
        {
            get { return (bool)GetValue(UseRightProperty); }
            set { SetValue(UseRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRightProperty =
            DependencyProperty.Register("UseRight", typeof(bool), typeof(PrintBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PrintBox control = d as PrintBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

    }
}
