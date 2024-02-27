
using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Controls.FilterColumnDataGrid
{
    public interface IFilter
    {
        #region Public Properties

        object Content { get; set; }
        int ContentLength { get; set; }
        Type FieldType { get; set; }
        bool IsChanged { get; set; }
        object Label { get; set; }
        int Level { get; set; }

        #endregion Public Properties
    }

    public class FilterItem : NotifyProperty, IFilter
    {
        #region Private Fields

        private bool initialState;
        private bool isChecked;

        #endregion Private Fields

        #region Public Properties
        public object Content { get; set; }
        public int ContentLength { get; set; }
        public Type FieldType { get; set; }
        public bool Initialize
        {
            set
            {
                initialState = value;
                isChecked = value;
            }
        }

        public bool IsChanged { get; set; }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                IsChanged = value != initialState;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public object Label { get; set; }
        public int Level { get; set; }

        #endregion Public Properties
    }

    public class FilterItemDate : NotifyProperty, IFilter
    {
        #region Private Fields

        private bool? initialState;
        private bool? isChecked;

        #endregion Private Fields

        #region Public Properties

        public List<FilterItemDate> Children { get; set; }

        public object Content { get; set; }

        public int ContentLength { get; set; }

        public Type FieldType { get; set; }

        public bool? Initialize
        {
            set
            {
                initialState = value;
                isChecked = value;
            }
        }

        public bool IsChanged { get; set; }

        public bool? IsChecked
        {
            get => isChecked;
            set => SetIsChecked(value, true, true);
        }

        public FilterItem Item { get; set; }

        public object Label { get; set; }

        public int Level { get; set; }

        public FilterItemDate Parent { get; set; }

        public List<FilterItemDate> Tree { get; set; }

        #endregion Public Properties

        #region Private Methods

        private void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == isChecked)
                return;
            isChecked = value;
            IsChanged = initialState != isChecked;
            if (Item != null)
            {
                Item.IsChanged = IsChanged;
                Item.Initialize = IsChecked == true;
            }
            if (Level == 0)
                Tree?.Skip(1).ToList().ForEach(c => { c.SetIsChecked(value, true, true); });
            if (updateChildren && isChecked.HasValue && Level > 0)
                Children?.ForEach(c => { c.SetIsChecked(value, true, false); });

            if (updateParent) Parent?.VerifyCheckedState();

            OnPropertyChanged(nameof(IsChecked));
        }

        private void VerifyCheckedState()
        {
            bool? b = null;

            for (var i = 0; i < Children.Count; ++i)
            {
                var item = Children[i];
                var current = item.IsChecked;

                if (i == 0)
                {
                    b = current;
                }
                else if (b != current)
                {
                    b = null;
                    break;
                }
            }

            SetIsChecked(b, false, true);
        }

        #endregion Private Methods
    }
}