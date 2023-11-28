﻿using H.Controls.Adorner;
using H.Providers.Ioc;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Linq;
using System.Reflection.Metadata;
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
        private bool _useCancel = false;
        public bool UseCancel
        {
            get { return _useCancel; }
            set
            {
                _useCancel = value;
                RaisePropertyChanged();
            }
        }

        private bool _useSumit = true;
        public bool UseSumit
        {
            get { return _useSumit; }
            set
            {
                _useSumit = value;
                RaisePropertyChanged();
            }
        }

        private static ManualResetEvent _waitHandle = new ManualResetEvent(false);

        public async Task<bool?> ShowDialog(Window owner = null)
        {
            var window = owner ?? Application.Current.MainWindow;
            var child = window.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(child);
            var adorner = new PresenterAdorner(child, this);
            layer.Add(adorner);
            _waitHandle.Reset();
            return await Task.Run(() =>
            {
                _waitHandle.WaitOne();
                return this.DialogResult;
            });
        }

        public void RefreshButton(DialogButton dialogButton)
        {
            switch (dialogButton)
            {
                case DialogButton.Sumit:
                    {
                        this.UseSumit = true;
                        this.UseCancel = false;
                        return;
                    }
                case DialogButton.None:
                    {
                        this.UseSumit = false;
                        this.UseCancel = false;
                        return;
                    }
                case DialogButton.Cancel:
                    {
                        this.UseSumit = false;
                        this.UseCancel = true;
                        return;
                    }
                case DialogButton.SumitAndCancel:
                    {
                        this.UseSumit = true;
                        this.UseCancel = true;
                        return;
                    }
                default:
                    {
                        this.UseSumit = true;
                        this.UseCancel = false;
                        return;
                    }
            }
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
                var child = Application.Current.MainWindow.Content as UIElement;
                var layer = AdornerLayer.GetAdornerLayer(child);
                var adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this);
                if (adorners == null)
                    return;
                foreach (var adorner in adorners)
                {
                    layer.Remove(adorner);
                }
                _waitHandle.Set();
            });
        }

        public Func<bool> CanSumit { get; set; }
        public bool IsCancel => this.DialogResult == false;
        #endregion
    }
}