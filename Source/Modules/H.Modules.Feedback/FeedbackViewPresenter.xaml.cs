using H.Services.Common;
using H.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Feedback
{
    [Display(Name = "意见反馈", GroupName = SettingGroupNames.GroupSystem, Description = "点击显示意见反馈")]
    public class FeedbackViewPresenter : BindableBase, IFeedbackViewPresenter
    {
        private string _title;
        [Display(Name = "您的称呼")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _contact;
        [Browsable(false)]
        [Display(Name = "联系方式")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged();
            }
        }

        private string _text;
        [Browsable(false)]
        [Display(Name = "问题描述")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        public string MailAccount => FeedbackOptions.Instance.MailAccount;

        private ObservableCollection<string> _files = new ObservableCollection<string>();
        public ObservableCollection<string> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddFilesCommand => new RelayCommand(async l =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG文件(*.png)|*.png|JPG文件(*.jpg)|*.jpg|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = true;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;

            if (this.Files.Count + openFileDialog.FileNames.Length > 5)
            {
                await IocMessage.Dialog.Show("已超过附件上限5个");
                return;
            }
            this.Files.AddRange(openFileDialog.FileNames);
        });
    }
}
