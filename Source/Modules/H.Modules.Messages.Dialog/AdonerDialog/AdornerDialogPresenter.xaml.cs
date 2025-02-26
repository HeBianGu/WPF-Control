using H.Controls.Adorner;
using H.Services.Common;
using H.Mvvm;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace H.Modules.Messages.Dialog
{
    [Icon("\xEA8F")]
    public partial class AdornerDialogPresenter : DesignPresenterBase, IDialog, ICancelable
    {
        public AdornerDialogPresenter(object presenter)
        {
            this.Presenter = presenter;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.Padding = new Thickness(10, 6, 10, 6);
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
            await this.TransactionShow(adorner);
            _waitHandle.Reset();
            return await Task.Run(() =>
            {
                _waitHandle.WaitOne();
                return this.DialogResult;
            });
        }
        #region - IDialogWindow -
        protected virtual async Task TransactionShow(PresenterAdorner presenterAdorner)
        {
            if (this.Transitionable == null && presenterAdorner.Presenter is ITransitionHostable hostable)
                await hostable.Show(presenterAdorner.ContentPresenter);
            else
                await this.Show(presenterAdorner.ContentPresenter);
        }

        protected virtual async Task TransactionClose(PresenterAdorner presenterAdorner)
        {
            if (this.Transitionable == null && presenterAdorner.Presenter is ITransitionHostable hostable)
                await hostable.Close(presenterAdorner.ContentPresenter);
            else
                await this.Close(presenterAdorner.ContentPresenter);
        }
        public bool? DialogResult { get; set; }
        public void Sumit()
        {
            if (this.CanSumit?.Invoke() != false)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        public void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        public void Close()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                UIElement child = Application.Current.MainWindow.Content as UIElement;
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
                System.Collections.Generic.IEnumerable<PresenterAdorner> adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this);
                if (adorners == null)
                    return;
                foreach (PresenterAdorner adorner in adorners)
                {
                    await this.TransactionClose(adorner);
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

        public ITransitionable Transitionable { get; set; }
    }
}
