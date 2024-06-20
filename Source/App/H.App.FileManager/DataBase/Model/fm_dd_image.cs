using H.Extensions.Behvaiors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H.App.FileManager
{
    public class fm_dd_image : fm_dd_file
    {
        [Browsable(false)]
        [ReadOnly(true)]
        [Display(Name = "宽度")]
        public int PixelWidth { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        [Display(Name = "高度")]
        public int PixelHeight { get; set; }

        private string _object;
        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "对象")]
        public string Object
        {
            get { return _object; }
            set
            {
                _object = value;
                RaisePropertyChanged();
            }
        }

        [Display(Name = "年份")]
        public string Year { get; set; }

        private string _area;
        [Display(Name = "国家")]
        public string Area
        {
            get { return _area; }
            set
            {
                _area = value;
                RaisePropertyChanged();
            }
        }

        private string _articulation;
        [Display(Name = "清晰度")]
        public string Articulation
        {
            get { return _articulation; }
            set
            {
                _articulation = value;
                RaisePropertyChanged();
            }
        }

        [Display(Name = "简介")]
        public string Introduction { get; set; }

        /// <summary> 宽高比 1920x1080 </summary>
        [Display(Name = "宽高比")]
        [NotMapped]
        public string Aspect => $"{this.PixelWidth}x{this.PixelHeight}";

    }
}
