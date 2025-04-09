global using H.Controls.Adorner;
global using H.Mvvm;
global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Windows;
global using System.Windows.Documents;
global using System.Windows.Controls;
global using H.Mvvm.ViewModels.Base;
global using H.Controls.Adorner.Adorner;
global using H.Common.Transitionable;
global using H.Services.Message.Dialog;
global using H.Common.Attributes;

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
        public async void Sumit()
        {
            if (this.CanSumit == null)
            {
                this.DialogResult = true;
                this.Close();
                return;
            }
            var t = this.CanSumit.Invoke();
            if (t == null)
            {
                this.DialogResult = true;
                this.Close();
                return;
            }

            var r = await t;
            if (r != true)
                return;
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

        public Func<Task<bool>> CanSumit { get; set; }
        public bool IsCancel => this.DialogResult == false;
        public DialogButton DialogButton { get; set; } = DialogButton.Sumit;
        public Window Owner { get; set; }
        #endregion

        public ITransitionable Transitionable { get; set; }
        public DataTemplate PresenterTemplate { get; set; }
    }
}
