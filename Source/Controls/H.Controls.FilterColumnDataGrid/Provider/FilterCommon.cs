
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace H.Controls.FilterColumnDataGrid
{
    [DataContract]
    public sealed class FilterCommon : NotifyProperty
    {
        #region Private Fields

        private bool isFiltered;

        #endregion Private Fields

        #region Public Constructors

        public FilterCommon()
        {
            PreviouslyFilteredItems = new HashSet<object>(EqualityComparer<object>.Default);
        }

        #endregion Public Constructors

        #region Public Properties

        [DataMember(Name = "FilteredItems")]
        public HashSet<object> PreviouslyFilteredItems { get; set; }

        [DataMember(Name = "FieldName")]
        public string FieldName { get; set; }

        public Button FilterButton { get; set; }
        public Loc Translate { get; set; }
        public Type FieldType { get; set; }
        public bool IsFiltered
        {
            get => isFiltered;
            set
            {
                isFiltered = value;
                OnPropertyChanged(nameof(IsFiltered));
            }
        }

        #endregion Public Properties

        #region Public Methods
        public void AddFilter(Dictionary<string, Predicate<object>> criteria)
        {
            if (IsFiltered)
                return;
            bool Predicate(object o)
            {
                var value = FieldType == typeof(DateTime)
                    ? ((DateTime?)o.GetPropertyValue(FieldName))?.Date
                    : o.GetPropertyValue(FieldName);

                return !PreviouslyFilteredItems.Contains(value);
            }
            criteria.Add(FieldName, Predicate);

            IsFiltered = true;
        }

        #endregion Public Methods
    }
}