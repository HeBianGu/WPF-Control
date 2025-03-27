global using H.Services.Message;
using H.Services.Message.IODialog;
using Microsoft.Win32;
using PdfiumViewer;
using PdfiumViewer.Core;
using PdfiumViewer.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace H.Controls.PDF
{
    [TemplatePart(Name = "PART_Renderer")]
    public class PDFBox : Control
    {
        static PDFBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFBox), new FrameworkPropertyMetadata(typeof(PDFBox)));
        }

        private readonly CancellationTokenSource _cts;
        private PdfSearchManager _searchManager;
        private PdfRenderer _renderer;
        public PDFBox()
        {

            this.Unloaded += (l, k) =>
            {
                this._renderer?.Dispose();
            };

            _cts = new CancellationTokenSource();
            {
                CommandBinding binding = new CommandBinding(PDFBoxCommands.Open);
                binding.Executed += async (l, k) =>
                {
                    var r = IocMessage.IOFileDialog?.ShowOpenFile(IOFileDialogOptionActions.Pdf);
                    if (r == null)
                        return;
                    this.PageIndex = 0;
                    this.Bookmarks?.Clear();
                    Stream mem;
                    if (IocMessage.Dialog == null)
                    {
                        var bytes = File.ReadAllBytes(r);
                        mem = new MemoryStream(bytes);
                    }
                    else
                    {
                        mem = await IocMessage.Dialog.ShowWait(x =>
                        {
                            var bytes = File.ReadAllBytes(r);
                            return new MemoryStream(bytes);
                        });
                    }
                    this._renderer.OpenPdf(mem);
                    this._renderer.Bookmarks.OrderBy(x => x.PageIndex);
                    this.Bookmarks = this._renderer.Bookmarks;
                    this.ShowBookmarks = this.Bookmarks?.Count > 0;
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
                        Title = "选择文件夹",
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
                    this._renderer.PreviousPage();
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
                    this._renderer.NextPage();
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
                    this._renderer.SetZoomMode(PdfViewerZoomMode.FitWidth);
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
                    this._renderer.SetZoomMode(PdfViewerZoomMode.FitHeight);
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
                        var pageStep = this._renderer.PagesDisplayMode == PdfViewerPagesDisplayMode.BookMode ? 2 : 1;
                        this.Dispatcher.Invoke(() => this._renderer.GotoPage(0));
                        while (this._renderer.PageNo < this._renderer.PageCount - pageStep)
                        {
                            this.Dispatcher.Invoke(() => this._renderer.NextPage());
                            await Task.Delay(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        _cts.Cancel();
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
                    this._renderer.ZoomIn();
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
                    this._renderer.ZoomOut();

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
                    this._renderer.Counterclockwise();
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
                    this._renderer.ClockwiseRotate();
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
                    var info = this._renderer.GetInformation();
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
                    this.Bookmarks = Renderer.Bookmarks;
                    if (this.Bookmarks?.Count > 0)
                        this.ShowBookmarks = !this.ShowBookmarks;
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
                    _renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.ContinuousMode;

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
                    _renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.BookMode;
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
                    _renderer.PagesDisplayMode = PdfViewerPagesDisplayMode.SinglePageMode;
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
                    if ((_renderer.Flags & PdfRenderFlags.Transparent) != 0)
                    {
                        _renderer.Flags &= ~PdfRenderFlags.Transparent;
                    }
                    else
                    {
                        _renderer.Flags |= PdfRenderFlags.Transparent;
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
                    this.IsSearchOpen = !this.IsSearchOpen;
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
                    _renderer.IsRightToLeft = true;
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
                    _renderer.IsRightToLeft = false;
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
                binding.Executed += async (l, k) =>
                {
                    try
                    {
                        _renderer.UnLoad();
                    }
                    catch (Exception exception)
                    {
                        await IocMessage.ShowDialogMessage(exception.Message);
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
                        _renderer.EnableKinetic = toggle.IsChecked == true;
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
                    if (this.SearchMatchItemNo > 1)
                    {
                        this.SearchMatchItemNo--;
                        // DisplayTextSpan(SearchMatches.Items[SearchMatchItemNo - 1].TextSpan);
                        _searchManager.FindNext(false);
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
                    if (this.SearchMatchesCount > this.SearchMatchItemNo)
                    {
                        this.SearchMatchItemNo++;
                        //DisplayTextSpan(SearchMatches.Items[SearchMatchItemNo - 1].TextSpan);
                        _searchManager.FindNext(true);
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
            this._renderer = this.Template.FindName("PART_Renderer", this) as PdfRenderer;
            this._renderer.PropertyChanged += (l, k) =>
            {
                this.PageIndex = this._renderer.PageNo + 1;
                this.ZoomPercent = this._renderer.Zoom * 100;
            };

            _searchManager = new PdfSearchManager(_renderer);
            this.UseMatchCase = _searchManager.MatchCase;
            this.UseWholeWordOnly = _searchManager.MatchWholeWord;
            this.UseHighlightAllMatches = _searchManager.HighlightAllMatches;
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

        public int SearchMatchItemNo
        {
            get { return (int)GetValue(SearchMatchItemNoProperty); }
            set { SetValue(SearchMatchItemNoProperty, value); }
        }


        public static readonly DependencyProperty SearchMatchItemNoProperty =
            DependencyProperty.Register("SearchMatchItemNo", typeof(int), typeof(PDFBox), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }

            }));


        public int SearchMatchesCount
        {
            get { return (int)GetValue(SearchMatchesCountProperty); }
            set { SetValue(SearchMatchesCountProperty, value); }
        }


        public static readonly DependencyProperty SearchMatchesCountProperty =
            DependencyProperty.Register("SearchMatchesCount", typeof(int), typeof(PDFBox), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }

            }));


        public bool IsSearchOpen
        {
            get { return (bool)GetValue(IsSearchOpenProperty); }
            set { SetValue(IsSearchOpenProperty, value); }
        }


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


        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }


        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(PDFBox), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {
                    control._renderer.GotoPage(Math.Min(Math.Max(n - 1, 0), control._renderer.PageCount - 1));
                    if (control.Bookmarks == null)
                        return;
                    PdfBookmark find = null;
                    Action<PdfBookmark> action = null;
                    action = x =>
                      {
                          foreach (var bookmark in x.Children)
                          {
                              if (bookmark.PageIndex <= n)
                              {
                                  find = bookmark;
                              }
                              action.Invoke(bookmark);
                          }
                      };
                    foreach (var bookmark in control.Bookmarks)
                    {
                        if (bookmark.PageIndex <= n)
                        {
                            find = bookmark;
                        }
                        action.Invoke(bookmark);
                    }
                    if (control._isrefreshBookMark)
                        return;
                    control._isrefreshBookMark = true;
                    if (find != null)
                        control.SelectedBookIndex = find;
                    control._isrefreshBookMark = false;
                }
            }));


        public double ZoomPercent
        {
            get { return (double)GetValue(ZoomPercentProperty); }
            set { SetValue(ZoomPercentProperty, value); }
        }


        public static readonly DependencyProperty ZoomPercentProperty =
            DependencyProperty.Register("ZoomPercent", typeof(double), typeof(PDFBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {
                    control._renderer.SetZoom(n / 100);
                }
            }));


        public FlowDirection IsRtl
        {
            get => _renderer?.IsRightToLeft == true ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            set => _renderer.IsRightToLeft = value == FlowDirection.RightToLeft ? true : false;
        }


        bool _isrefreshBookMark = false;
        public PdfBookmark SelectedBookIndex
        {
            get { return (PdfBookmark)GetValue(SelectedBookIndexProperty); }
            set { SetValue(SelectedBookIndexProperty, value); }
        }


        public static readonly DependencyProperty SelectedBookIndexProperty =
            DependencyProperty.Register("SelectedBookIndex", typeof(PdfBookmark), typeof(PDFBox), new FrameworkPropertyMetadata(default(PdfBookmark), (d, e) =>
            {
                PDFBox control = d as PDFBox;

                if (control == null) return;

                if (e.OldValue is PdfBookmark o)
                {

                }

                if (control._isrefreshBookMark)
                    return;
                control._isrefreshBookMark = true;
                if (e.NewValue is PdfBookmark n)
                {
                    control._renderer.GotoPage(n.PageIndex);
                }
                control._isrefreshBookMark = false;
            }));


        public bool UseMatchCase
        {
            get { return (bool)GetValue(UseMatchCaseProperty); }
            set { SetValue(UseMatchCaseProperty, value); }
        }


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
            this.SearchMatchItemNo = 0;
            _searchManager.MatchCase = this.UseMatchCase;
            _searchManager.MatchWholeWord = this.UseWholeWordOnly;
            _searchManager.HighlightAllMatches = this.UseHighlightAllMatches;
            //SearchMatchesTextBlock.Visibility = Visibility.Visible;
            if (!_searchManager.Search(this.SearchTerm))
            {
                await IocMessage.ShowDialogMessage("未搜索到数据");
            }
            else

                this.SearchMatchesCount = _searchManager.MatchesCount;

            if (!_searchManager.FindNext(true))
                await IocMessage.ShowDialogMessage("到达了搜索的起点");
        }

        private async void SaveAsImages(string path)
        {
            try
            {
                for (var i = 0; i < _renderer.PageCount; i++)
                {
                    var size = _renderer.Document.PageSizes[i];
                    var image = _renderer.Document.Render(i, (int)size.Width * 5, (int)size.Height * 5, 300, 300, false);
                    image.Save(Path.Combine(path, $"img{i}.png"));
                }
            }
            catch (Exception ex)
            {
                _cts.Cancel();
                Debug.Fail(ex.Message);
                //MessageBox.Show(this, ex.Message, "Error!");
                await IocMessage.ShowDialogMessage(ex.Message);
            }
        }
    }
}
