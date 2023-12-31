using H.Controls.TagBox;
using H.Extensions.ValueConverter;
using H.Extensions.ViewModel;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace H.App.FileManager
{
    public class GetFileMenuItemConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DbIoc.GetService<IRepositoryViewModel<fm_dd_file>>() is FileRepositoryViewModel vm)
            {
                List<TreeNodeBase<ICommand>> result = new List<TreeNodeBase<ICommand>>();
                if (value is fm_dd_video video)
                    result = this.GetImageCommands(video);
                if (value is fm_dd_image image)
                    result = this.GetImageCommands(image);
                if (value is fm_dd_file file)
                    result = this.GetFileCommands(file);
                foreach (IRelayCommand item in vm.MenuCommands)
                {
                    result.Add(new TreeNodeBase<ICommand>(item));
                }
                return result;
            }
            return null;
        }

        public List<TreeNodeBase<ICommand>> GetFileCommands(fm_dd_file file)
        {
            List<TreeNodeBase<ICommand>> result = new List<TreeNodeBase<ICommand>>();
            TreeNodeBase<ICommand> favorite = null;
            favorite = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
            {
                file.Favorite = !file.Favorite;
                favorite.IsChecked = file.Favorite;
            })
            { Name = "喜欢" })
            { IsCheckable = true, IsChecked = file.Favorite };
            result.Add(favorite);

            TreeNodeBase<ICommand> scoreNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 9) { Name = $"评分" });
            result.Add(scoreNode);

            foreach (int item in Enumerable.Range(0, 11).Reverse())
            {
                TreeNodeBase<ICommand> score = null;
                score = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                {
                    file.Score = item;
                    scoreNode.Nodes.Foreach(x => x.IsChecked = false);
                    score.IsChecked = true;
                })
                { Name = $"评分=[{item}]" })
                { IsCheckable = true, IsChecked = file.Score == item };
                scoreNode.Nodes.Add(score);
            }

            TreeNodeBase<ICommand> tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "标签" });
            result.Add(tagNode);
            ITagService tagService = Ioc.GetService<ITagService>();
            if (tagService != null)
            {
                {
                    IEnumerable<ITag> tags = tagService.Collection.Where(x => x.GroupName == null);
                    foreach (ITag tag in tags)
                    {
                        TreeNodeBase<ICommand> ctagNode = null;
                        ctagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                       {
                           bool contain = tagService.ContainTag(file.Tags, tag);
                           if (contain)
                           {
                               file.Tags = tagService.ConvertToUnCheck(file.Tags, tag);
                           }
                           else
                           {
                               file.Tags = tagService.ConvertToCheck(file.Tags, tag);
                           }
                           ctagNode.IsChecked = !contain;
                       })
                        { Name = $"标签=[{tag.Name}]" })
                        { IsCheckable = true, IsChecked = tagService.ContainTag(file.Tags, tag) };
                        tagNode.Nodes.Add(ctagNode);
                    }
                }
            }
            return result;

        }

        public List<TreeNodeBase<ICommand>> GetImageCommands(fm_dd_image file)
        {
            List<TreeNodeBase<ICommand>> result = this.GetFileCommands(file);

            ITagService tagService = Ioc.GetService<ITagService>();
            if (tagService != null)
            {

                {
                    TreeNodeBase<ICommand> tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "对象" });
                    result.Add(tagNode);
                    {
                        IEnumerable<ITag> tags = tagService.Collection.Where(x => x.GroupName == "Object");
                        foreach (ITag tag in tags)
                        {
                            TreeNodeBase<ICommand> ctagNode = null;
                            ctagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                bool contain = tagService.ContainTag(file.Object, tag);
                                if (contain)
                                {
                                    file.Object = tagService.ConvertToUnCheck(file.Object, tag);
                                }
                                else
                                {
                                    file.Object = tagService.ConvertToCheck(file.Object, tag);
                                }
                                ctagNode.IsChecked = !contain;
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true, IsChecked = tagService.ContainTag(file.Object, tag) };
                            tagNode.Nodes.Add(ctagNode);
                        }
                    }
                }

                {
                    TreeNodeBase<ICommand> tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "国家" });
                    result.Add(tagNode);
                    {
                        IEnumerable<ITag> tags = tagService.Collection.Where(x => x.GroupName == "Area");
                        foreach (ITag tag in tags)
                        {
                            TreeNodeBase<ICommand> ctagNode = null;
                            ctagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                bool contain = tagService.ContainTag(file.Area, tag);
                                if (contain)
                                {
                                    file.Area = tagService.ConvertToUnCheck(file.Area, tag);
                                }
                                else
                                {
                                    file.Area = tagService.ConvertToCheck(file.Area, tag);
                                }
                                ctagNode.IsChecked = !contain;
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true, IsChecked = tagService.ContainTag(file.Area, tag) };
                            tagNode.Nodes.Add(ctagNode);
                        }
                    }
                }

                {
                    TreeNodeBase<ICommand> tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "清晰度" });
                    result.Add(tagNode);
                    {
                        IEnumerable<ITag> tags = tagService.Collection.Where(x => x.GroupName == "Articulation");
                        foreach (ITag tag in tags)
                        {
                            TreeNodeBase<ICommand> ctagNode = null;
                            ctagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                bool contain = tagService.ContainTag(file.Articulation, tag);
                                if (contain)
                                {
                                    file.Articulation = tagService.ConvertToUnCheck(file.Articulation, tag);
                                }
                                else
                                {
                                    file.Articulation = tagService.ConvertToCheck(file.Articulation, tag);
                                }
                                ctagNode.IsChecked = !contain;
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true, IsChecked = tagService.ContainTag(file.Articulation, tag) };
                            tagNode.Nodes.Add(ctagNode);
                        }
                    }
                }
            }
            return result;
        }

    }

    public class GetTickToMillisecond : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long l)
                return TimeSpan.FromTicks(l).TotalMilliseconds;
            return value;
        }
    }
}
