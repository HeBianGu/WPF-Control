// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard;

public class StoryBoardProvider
{
    public static void FountainAnimation(IEnumerable<DependencyObject> uclist, int pL = -1000, int pT = 1000, double Mul = 10, double middle_value = 0.5, double end_value = 1, double split = 0.05)
    {
        if (uclist.Count() <= 0) return;

        Storyboard storyboard = StoryboardFactory.Create();

        double Init = 0;
        double Org = 1;
        double first_value = 0;
        Random r2 = new Random();

        int i = 0;
        //  Do ：间隔时间 
        foreach (DependencyObject c in uclist)
        {
            double first = (i * split) + first_value;
            double middle = (i * split) + middle_value;
            double end = (i * split) + end_value;

            i++;

            EasingDoubleKeyFrame edf0 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));//所有元素起点都是0
            EasingDoubleKeyFrame edf1 = new EasingDoubleKeyFrame(Init, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(first)));
            EasingDoubleKeyFrame edf2 = new EasingDoubleKeyFrame(Mul, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
            EasingDoubleKeyFrame edf3 = new EasingDoubleKeyFrame(Org, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));

            DoubleAnimationUsingKeyFrames daukf1 = new DoubleAnimationUsingKeyFrames();
            daukf1.KeyFrames.Add(edf0);
            daukf1.KeyFrames.Add(edf1);
            daukf1.KeyFrames.Add(edf2);
            daukf1.KeyFrames.Add(edf3);
            storyboard.Children.Add(daukf1);
            Storyboard.SetTarget(daukf1, c);
            Storyboard.SetTargetProperty(daukf1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            DoubleAnimationUsingKeyFrames daukf2 = new DoubleAnimationUsingKeyFrames();
            daukf2.KeyFrames.Add(edf0);
            daukf2.KeyFrames.Add(edf1);
            daukf2.KeyFrames.Add(edf2);
            daukf2.KeyFrames.Add(edf3);
            storyboard.Children.Add(daukf2);
            Storyboard.SetTarget(daukf2, c);
            Storyboard.SetTargetProperty(daukf2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            DoubleAnimationUsingKeyFrames daukf3 = new DoubleAnimationUsingKeyFrames();
            //EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r2.Next(-1000, 2000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r2.Next(pL, pT), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
            EasingDoubleKeyFrame edf32 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
            daukf3.KeyFrames.Add(edf31);
            daukf3.KeyFrames.Add(edf32);
            storyboard.Children.Add(daukf3);
            Storyboard.SetTarget(daukf3, c);
            Storyboard.SetTargetProperty(daukf3, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));

            DoubleAnimationUsingKeyFrames daukf4 = new DoubleAnimationUsingKeyFrames();
            //EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(-2000, 2000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(pL, pT), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
            EasingDoubleKeyFrame edf42 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
            daukf4.KeyFrames.Add(edf41);
            daukf4.KeyFrames.Add(edf42);
            storyboard.Children.Add(daukf4);
            Storyboard.SetTarget(daukf4, c);
            Storyboard.SetTargetProperty(daukf4, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
        }
        storyboard.FillBehavior = FillBehavior.HoldEnd;
        storyboard.Begin();
    }

    public static void BeginAnimationX(IEnumerable<DependencyObject> controls, double startValue = -1000, double endValue = 0, double end_value = 1, double split = 0.05)
    {
        BeginAnimationPropertyPath(controls, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"), startValue, endValue, end_value, split);
    }

    public static void BeginAnimationY(IEnumerable<DependencyObject> controls, double startValue = -1000, double endValue = 0, double end_value = 1, double split = 0.05)
    {
        BeginAnimationPropertyPath(controls, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"), startValue, endValue, end_value, split);
    }

    public static void BeginAnimationOpacity(IEnumerable<DependencyObject> controls, double startValue = 0, double endValue = 1, double end_value = 1, double split = 0.05)
    {
        ConitnueAnimationPropertyPath(controls, new PropertyPath("(UIElement.Opacity)"), startValue, endValue, end_value, split);
    }

    public static void BeginAnimationPropertyPath(IEnumerable<DependencyObject> controls, PropertyPath property, double startValue, double endValue, double end_value, double split = 0.05)
    {
        if (controls.Count() <= 0)
            return;
        Storyboard storyboard = StoryboardFactory.Create();
        int i = 0;
        foreach (DependencyObject control in controls)
        {
            double first = 0;
            double end = (i * split) + end_value;
            i++;
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(startValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(first)));
            EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(endValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
            frames.KeyFrames.Add(key1);
            frames.KeyFrames.Add(key2);
            storyboard.Children.Add(frames);
            Storyboard.SetTarget(frames, control);
            Storyboard.SetTargetProperty(frames, property);
        }
        storyboard.FillBehavior = FillBehavior.HoldEnd;
        Timeline.SetDesiredFrameRate(storyboard, StoryboardSetting.DesiredFrameRate);
        storyboard.Begin();
    }

    /// <summary> 运行动画 (控件之间使用依次效果，运行完一个后执行另一个) </summary>
    public static void ConitnueAnimationPropertyPath(IEnumerable<DependencyObject> controls, PropertyPath property, double startValue, double endValue, double end_value, double split = 0.05)
    {
        if (controls.Count() <= 0)
            return;
        Storyboard storyboard = StoryboardFactory.Create();
        int i = 0;
        foreach (DependencyObject control in controls)
        {
            double first = 0;
            double middle = (i * end_value) + split;
            double end = middle + end_value;
            i++;
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(startValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(first)));
            EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(startValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
            EasingDoubleKeyFrame key3 = new EasingDoubleKeyFrame(endValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
            frames.KeyFrames.Add(key1);
            frames.KeyFrames.Add(key2);
            frames.KeyFrames.Add(key3);
            storyboard.Children.Add(frames);
            Storyboard.SetTarget(frames, control);
            Storyboard.SetTargetProperty(frames, property);
        }
        storyboard.FillBehavior = FillBehavior.HoldEnd;
        storyboard.Begin();
    }
}
