// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Extensions.DataBase;
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Extensions.DataBase.Repository
{
    public class DateRepositoryBindable<TEntity> : RepositoryBindable<TEntity> where TEntity : DbModelBase, new()
    {
        public override void RefreshData(params string[] includes)
        {
            includes = includes ?? this.GetIncludes().ToArray();
            IEnumerable<SelectBindable<TEntity>> collection = includes == null ? this.Repository.GetList(x => x.CDATE > this.StartTime && x.CDATE < this.EndTime).Select(x => new SelectBindable<TEntity>(x))
            : this.Repository.GetList(includes).Select(x => new SelectBindable<TEntity>(x));
            this.Collection.Load(collection);
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
