using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    public class fm_dd_video : fm_dd_image
    {
        [Display(Name = "缩略图")]
        public string Image { get; set; }

        [Display(Name = "上次播放位置")]
        public long LastPlayTimeStamp { get; set; }

        [Display(Name = "种子")]
        public string Torrent { get; set; }

        [Display(Name = "视频时长")]
        public long Duration { get; set; }

        [Display(Name = "比特率")]
        public string Bitrate { get; set; }

        /// <summary> 编码格式 h264 (Constrained Baseline) (avc1 / 0x31637661) </summary>
        [Display(Name = "编码格式")]
        public string VideoCode { get; set; }

        [Display(Name = "帧频")]
        public string Rate { get; set; }

        [Browsable(false)]
        [Display(Name = "缩率图位置")]
        public int SelectedImageIndex { get; set; }

        [Display(Name = "像素格式")]
        public string PixelFormat { get; set; }

        [Browsable(false)]
        [Display(Name = "预览")]
        public virtual ObservableCollection<fm_dd_video_image> Images { get; set; } = new ObservableCollection<fm_dd_video_image>();
    }
}
