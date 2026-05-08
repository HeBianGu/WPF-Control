// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.DataBase.Repository
{
    public interface IDateTimeObservableSourceRepositoryBindable
    {
        DateTime EndTime { get; set; }
        DateTime StartTime { get; set; }
    }

    public class DateTimeObservableSourceRepositoryBindable<TEntity> : ObservableSourceRepositoryBindable<TEntity>, IDateTimeObservableSourceRepositoryBindable where TEntity : DbModelBase, new()
    {
        public override void RefreshData(Action after = null, params string[] includes)
        {
            includes = includes ?? this.GetIncludes().ToArray();
            IEnumerable<SelectBindable<TEntity>> collection = includes == null ? this.Repository.GetList(x => x.CDATE > this.StartTime && x.CDATE < this.EndTime).Select(x => new SelectBindable<TEntity>(x))
            : this.Repository.GetList(includes).Select(x => new SelectBindable<TEntity>(x));
            this.ObservableSource.Load(collection, after);
        }

        private DateTime _startTime = DateTime.Now.AddDays(-7).Date;
        /// <summary> 说明  </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _endTime = DateTime.Now.AddDays(1).Date;
        /// <summary> 说明  </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
