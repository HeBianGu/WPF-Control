using H.Controls.TagBox;
using H.Extensions.ValueConverter;
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                if (value is fm_dd_video video)
                {
                    return this.GetImageCommands(video);
                }
                if (value is fm_dd_image image)
                {
                    return this.GetImageCommands(image);
                }
                if (value is fm_dd_file file)
                {
                    return this.GetFileCommands(file);
                }
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
            { Name = "收藏" })
            { IsCheckable = true, IsChecked = file.Favorite };
            result.Add(favorite);

            var scoreNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 9) { Name = $"评分" });
            result.Add(scoreNode);
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 10) { Name = $"评分=[{10}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 9) { Name = $"评分=[{9}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 8) { Name = $"评分=[{8}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 7) { Name = $"评分=[{7}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 6) { Name = $"评分=[{6}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 5) { Name = $"评分=[{5}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 4) { Name = $"评分=[{4}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 3) { Name = $"评分=[{3}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 2) { Name = $"评分=[{2}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 1) { Name = $"评分=[{1}]" }));
            scoreNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => file.Score = 0) { Name = $"评分=[{0}]" }));

            var tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "标签" });
            result.Add(tagNode);
            var tagService = Ioc.GetService<ITagService>();
            if (tagService != null)
            {
                {
                    var tags = tagService.Collection.Where(x => x.GroupName == null);
                    foreach (var tag in tags)
                    {
                        TreeNodeBase<ICommand> ctagNode = null;
                        ctagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                       {
                           var contain = tagService.ContainTag(file.Tags, tag);
                           if (contain)
                           {
                               file.Tags = tagService.ConvertToUnCheck(file.Tags, tag);
                           }
                           else
                           {
                               file.Tags = tagService.ConvertToCheck(file.Tags, tag);
                           }

                           //tagNode.Nodes.Foreach(x => x.IsChecked = false);
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

            var tagService = Ioc.GetService<ITagService>();
            if (tagService != null)
            {

                {
                    var tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "对象" });
                    result.Add(tagNode);
                    {
                        var tags = tagService.Collection.Where(x => x.GroupName == "Object");
                        foreach (var tag in tags)
                        {
                            tagNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                var list = file.Object?.Split(new char[] { ',', ' ' }).ToList();
                                var contain = list?.Contains(tag.Name) == true;
                                if (contain)
                                {
                                    var rs = list.Remove(tag.Name);
                                    file.Object = string.Join(',', list);
                                }
                                else
                                {
                                    file.Object += "," + tag.Name;
                                    file.Object.Trim(',');
                                }
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true });
                        }
                    }
                }

                {
                    var tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "国家" });
                    result.Add(tagNode);
                    {
                        var tags = tagService.Collection.Where(x => x.GroupName == "Area");
                        foreach (var tag in tags)
                        {
                            tagNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                var list = file.Area?.Split(new char[] { ',', ' ' }).ToList();
                                var contain = list?.Contains(tag.Name) == true;
                                if (contain)
                                {
                                    var rs = list.Remove(tag.Name);
                                    file.Area = string.Join(',', list);
                                }
                                else
                                {
                                    file.Area += "," + tag.Name;
                                    file.Area.Trim(',');
                                }
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true });
                        }
                    }
                }

                {
                    var tagNode = new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x => { }) { Name = "清晰度" });
                    result.Add(tagNode);
                    {
                        var tags = tagService.Collection.Where(x => x.GroupName == "Articulation");
                        foreach (var tag in tags)
                        {
                            tagNode.Nodes.Add(new TreeNodeBase<ICommand>(new DisplayCommand<fm_dd_file>(x =>
                            {
                                var list = file.Articulation?.Split(new char[] { ',', ' ' }).ToList();
                                var contain = list?.Contains(tag.Name) == true;
                                if (contain)
                                {
                                    var rs = list.Remove(tag.Name);
                                    file.Articulation = string.Join(',', list);
                                }
                                else
                                {
                                    file.Articulation += "," + tag.Name;
                                    file.Articulation.Trim(',');
                                }
                            })
                            { Name = $"标签=[{tag.Name}]" })
                            { IsCheckable = true });
                        }
                    }
                }
            }
            return result;
        }

    }
}
