using H.Controls.Adorner;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Dialog
{
    public partial class AdornerDialogPresenter : DesignPresenterBase, IDialog, ICancelable
    {
        public AdornerDialogPresenter(object presenter)
        {
            this.Presenter = presenter;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.Padding = new Thickness(20);
        }
        public string Title { get; set; } = "提示";
        public object Presenter { get; set; }

        private ManualResetEvent _waitHandle = new ManualResetEvent(false);
        public async Task<bool?> ShowDialog(Window owner = null)
        {
            Window window = owner ?? Application.Current.MainWindow;
            UIElement child = window.Content as UIElement;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
            PresenterAdorner adorner = new PresenterAdorner(child, this);
            layer.Add(adorner);
            _waitHandle.Reset();
            return await Task.Run(() =>
            {
                _waitHandle.WaitOne();
                return this.DialogResult;
            });
        }
        #region - IDialogWindow -
        public bool? DialogResult { get; set; }
        public void Sumit()
        {
            this.DialogResult = true;
            this.Close();
        }

        public void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        public void Close()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UIElement child = Application.Current.MainWindow.Content as UIElement;
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
                System.Collections.Generic.IEnumerable<PresenterAdorner> adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this);
                if (adorners == null)
                    return;
                foreach (PresenterAdorner adorner in adorners)
                {
                    layer.Remove(adorner);
                }
                _waitHandle.Set();
            });
        }

        public Func<bool> CanSumit { get; set; }
        public bool IsCancel => this.DialogResult == false;
        public DialogButton DialogButton { get; set; } = DialogButton.Sumit;
        public Window Owner { get; set; }
        #endregion
    }
}
