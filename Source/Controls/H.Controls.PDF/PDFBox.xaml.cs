using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Win32;
using PdfiumViewer;
using PdfiumViewer.Core;
using PdfiumViewer.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace H.Controls.PDF
{
    [TemplatePart(Name = "Part_Renderer")]
    public class PDFBox : Control
    {
        static PDFBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFBox), new FrameworkPropertyMetadata(typeof(PDFBox)));
        }

        private CancellationTokenSource Cts { get; }

        public PDFBox()
        {

            this.Unloaded += (l, k) =>
            {
                this.Renderer?.Dispose();
            };

            Cts = new CancellationTokenSource();
            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Open);
                binding.Executed += (l, k) =>
                {
                    var dialog = new OpenFileDialog
                    {
                        Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*",
                        Title = "Open PDF File"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        var bytes = File.ReadAllBytes(dialog.FileName);
                        var mem = new MemoryStream(bytes);
                        this.Renderer.OpenPdf(mem);
                    }
                }; binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }



            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.SaveAsImages);
                binding.Executed += (l, k) =>
                {
                    // Create a "Save As" dialog for selecting a directory (HACK)
                    var dialog = new Microsoft.Win32.SaveFileDialog
                    {
                        Title = "Select a Directory",
                        Filter = "Directory|*.this.directory",
                        FileName = "select"
                    };
                    // instead of default "Save As"
                    // Prevents displaying files
                    // Filename will then be "select.this.directory"
                    if (dialog.ShowDialog() == true)
                    {
                        string path = dialog.FileName;
                        // Remove fake filename from resulting path
                        path = path.Replace("\\select.this.directory", "");
                        path = path.Replace(".this.directory", "");
                        // If user has changed the filename, create the new directory
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        // Our final value is in path
                        SaveAsImages(path);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Previous);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.PreviousPage();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Next);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.NextPage();
                }; binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.FitToWidth);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.SetZoomMode(PdfViewerZoomMode.FitWidth);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }



            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.FitToHeight);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.SetZoomMode(PdfViewerZoomMode.FitHeight);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.RenderToMemory);
                binding.Executed += async (l, k) =>
                {
                    try
                    {
                        var pageStep = this.Renderer.PagesDisplayMode == PdfViewerPagesDisplayMode.BookMode ? 2 : 1;
                        Dispatcher.Invoke(() => this.Renderer.GotoPage(0));
                        while (this.Renderer.PageNo < this.Renderer.PageCount - pageStep)
                        {
                            Dispatcher.Invoke(() => this.Renderer.NextPage());
                            await Task.Delay(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        Cts.Cancel();
                        Debug.Fail(ex.Message);
                        //MessageBox.Show(this, ex.Message, "Error!");
                        await IocMessage.ShowDialogMessage(ex.Message);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ZoomIn);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.ZoomIn();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ZoomOut);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.ZoomOut();

                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.RotateToLeft);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.Counterclockwise();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.RotateToRight);
                binding.Executed += (l, k) =>
                {
                    this.Renderer.ClockwiseRotate();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ShowInfo);
                binding.Executed += async (l, k) =>
                {
                    var info = this.Renderer.GetInformation();
                    if (info != null)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine($"Author: {info.Author}");
                        sb.AppendLine($"Creator: {info.Creator}");
                        sb.AppendLine($"Keywords: {info.Keywords}");
                        sb.AppendLine($"Producer: {info.Producer}");
                        sb.AppendLine($"Subject: {info.Subject}");
                        sb.AppendLine($"Title: {info.Title}");
                        sb.AppendLine($"Create Date: {info.CreationDate}");
                        sb.AppendLine($"Modified Date: {info.ModificationDate}");
                        await IocMessage.ShowDialogMessage(sb.ToString());
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ShowPageText);
                binding.Executed += (l, k) =>
                {
                    //var txtViewer = new TextViewer();
                    //var page = Renderer.PageNo;
                    //txtViewer.Body = Renderer.GetPdfText(page);
                    //txtViewer.Caption = $"Page {page + 1} contains {txtViewer.Body?.Length} character(s):";
                    //txtViewer.ShowDialog();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ShowBookmarks);
                binding.Executed += (l, k) =>
                {
                    Bookmarks = Renderer.Bookmarks;
                    if (Bookmarks?.Count > 0)
                        ShowBookmarks = !ShowBookmarks;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ContinuousPage);
                binding.Executed += (l, k) =>
                {
                    Renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.ContinuousMode;

                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.TwoPage);
                binding.Executed += (l, k) =>
                {
                    Renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.BookMode;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.SinglePage);
                binding.Executed += (l, k) =>
                {
                    Renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.SinglePageMode;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Transparent);
                binding.Executed += (l, k) =>
                {
                    if ((Renderer.Flags & PdfRenderFlags.Transparent) != 0)
                    {
                        Renderer.Flags &= ~PdfRenderFlags.Transparent;
                    }
                    else
                    {
                        Renderer.Flags |= PdfRenderFlags.Transparent;
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Searchterm);
                binding.Executed += (l, k) =>
                {
                    IsSearchOpen = !IsSearchOpen;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ToRight);
                binding.Executed += (l, k) =>
                {
                    Renderer.IsRightToLeft = true;
                    //OnPropertyChanged(nameof(IsRtl));
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.ToLeft);
                binding.Executed += (l, k) =>
                {
                    Renderer.IsRightToLeft = false;
                    //OnPropertyChanged(nameof(IsRtl));
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Close);
                binding.Executed += (l, k) =>
                {
                    try
                    {
                        //InfoBar.Foreground = System.Windows.Media.Brushes.Red;
                        Renderer.UnLoad();
                        //await Task.Delay(5000);
                        //InfoBar.Foreground = System.Windows.Media.Brushes.Black;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.HandTool);
                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is ToggleButton toggle)
                        Renderer.EnableKinetic = toggle.IsChecked == true;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.FindNext);
                binding.Executed += (l, k) =>
                {
                    if (SearchMatchItemNo > 1)
                    {
                        SearchMatchItemNo--;
                        // DisplayTextSpan(SearchMatches.Items[SearchMatchItemNo - 1].TextSpan);
                        SearchManager.FindNext(false);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.FindPrevious);
                binding.Executed += (l, k) =>
                {
                    if (SearchMatchesCount > SearchMatchItemNo)
                    {
                        SearchMatchItemNo++;
                        //DisplayTextSpan(SearchMatches.Items[SearchMatchItemNo - 1].TextSpan);
                        SearchManager.FindNext(true);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }
        }

        private PdfRenderer Renderer;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Renderer = this.Template.FindName("Part_Renderer", this) as PdfRenderer;

            SearchManager = new PdfSearchManager(Renderer);
            this.UseMatchCase = SearchManager.MatchCase;
            this.UseWholeWordOnly = SearchManager.MatchWholeWord;
            this.UseHighlightAllMatches = SearchManager.HighlightAllMatches;
        }


        private PdfSearchManager SearchManager { get; set; }

        public PdfBookmarkCollection Bookmarks
        {
            get { return (PdfBookmarkCollection)GetValue(BookmarksProperty); }
            set { SetValue(BookmarksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookmarksProperty =
            DependencyProperty.Register("Bookmarks", typeof(PdfBookmarkCollection), typeof(PDFBox), new FrameworkPropertyMetadata(default(PdfBookmarkCollection), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is PdfBookmarkCollection o)
                {

                }

                if (e.NewValue is PdfBookmarkCollection n)
                {

                }

            }));

        public int SearchMatchItemNo { get; set; }
        public int SearchMatchesCount { get; set; }

        public bool IsSearchOpen
        {
            get { return (bool)GetValue(IsSearchOpenProperty); }
            set { SetValue(IsSearchOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSearchOpenProperty =
            DependencyProperty.Register("IsSearchOpen", typeof(bool), typeof(PDFBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool ShowBookmarks
        {
            get { return (bool)GetValue(ShowBookmarksProperty); }
            set { SetValue(ShowBookmarksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowBookmarksProperty =
            DependencyProperty.Register("ShowBookmarks", typeof(bool), typeof(PDFBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        public int Page
        {
            get => Renderer?.PageNo ?? 0 + 1;
            set => Renderer.GotoPage(Math.Min(Math.Max(value - 1, 0), Renderer.PageCount - 1));
        }

        public int PageCount
        {
            get => Renderer?.PageNo ?? 0 + 1;
            set => Renderer.GotoPage(Math.Min(Math.Max(value - 1, 0), Renderer.PageCount - 1));
        }

        public FlowDirection IsRtl
        {
            get => Renderer?.IsRightToLeft == true ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            set => Renderer.IsRightToLeft = value == FlowDirection.RightToLeft ? true : false;
        }
        public PdfBookmark SelectedBookIndex
        {
            get { return (PdfBookmark)GetValue(SelectedBookIndexProperty); }
            set { SetValue(SelectedBookIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBookIndexProperty =
            DependencyProperty.Register("SelectedBookIndex", typeof(PdfBookmark), typeof(PDFBox), new FrameworkPropertyMetadata(default(PdfBookmark), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is PdfBookmark o)
                {

                }

                if (e.NewValue is PdfBookmark n)
                {
                    control.Renderer.GotoPage(n.PageIndex);
                }

            }));


        public bool UseMatchCase
        {
            get { return (bool)GetValue(UseMatchCaseProperty); }
            set { SetValue(UseMatchCaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseMatchCaseProperty =
            DependencyProperty.Register("UseMatchCase", typeof(bool), typeof(PDFBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseWholeWordOnly
        {
            get { return (bool)GetValue(UseWholeWordOnlyProperty); }
            set { SetValue(UseWholeWordOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseWholeWordOnlyProperty =
            DependencyProperty.Register("UseWholeWordOnly", typeof(bool), typeof(PDFBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseHighlightAllMatches
        {
            get { return (bool)GetValue(UseHighlightAllMatchesProperty); }
            set { SetValue(UseHighlightAllMatchesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseHighlightAllMatchesProperty =
            DependencyProperty.Register("UseHighlightAllMatches", typeof(bool), typeof(PDFBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermProperty); }
            set { SetValue(SearchTermProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTermProperty =
            DependencyProperty.Register("SearchTerm", typeof(string), typeof(PDFBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
            }));

        public async void Search()
        {
            SearchMatchItemNo = 0;
            SearchManager.MatchCase = this.UseMatchCase;
            SearchManager.MatchWholeWord = this.UseWholeWordOnly;
            SearchManager.HighlightAllMatches = this.UseHighlightAllMatches;
            //SearchMatchesTextBlock.Visibility = Visibility.Visible;

            if (!SearchManager.Search(SearchTerm))
            {
                await IocMessage.ShowDialogMessage("未搜索到数据");
            }
            else
            {
                SearchMatchesCount = SearchManager.MatchesCount;
            }

            if (!SearchManager.FindNext(true))
                await IocMessage.ShowDialogMessage("到达了搜索的起点");
        }

        private async void SaveAsImages(string path)
        {
            try
            {
                for (var i = 0; i < Renderer.PageCount; i++)
                {
                    var size = Renderer.Document.PageSizes[i];
                    var image = Renderer.Document.Render(i, (int)size.Width * 5, (int)size.Height * 5, 300, 300, false);
                    image.Save(Path.Combine(path, $"img{i}.png"));
                }
            }
            catch (Exception ex)
            {
                Cts.Cancel();
                Debug.Fail(ex.Message);
                //MessageBox.Show(this, ex.Message, "Error!");
                await IocMessage.ShowDialogMessage(ex.Message);
            }
        }
    }
}
