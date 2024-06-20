using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace H.Controls.HorseTextBlock
{
    public class HorseTextBlock : TextBlock
    {
        static HorseTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorseTextBlock), new FrameworkPropertyMetadata(typeof(HorseTextBlock)));
        }

        public HorseTextBlock()
        {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(HorseTextBlock));
            descriptor.AddValueChanged(this, (o, e) =>
            {
                this.Begin();
            });
        }
        public double From
        {
            get { return (double)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(double), typeof(HorseTextBlock), new FrameworkPropertyMetadata(300.0, (d, e) =>
            {
                HorseTextBlock control = d as HorseTextBlock;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
            }));

        public double To
        {
            get { return (double)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(double), typeof(HorseTextBlock), new FrameworkPropertyMetadata(-300.0, (d, e) =>
            {
                HorseTextBlock control = d as HorseTextBlock;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
            }));

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(HorseTextBlock), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(2)), (d, e) =>
            {
                HorseTextBlock control = d as HorseTextBlock;

                if (control == null) return;

                if (e.OldValue is Duration o)
                {

                }

                if (e.NewValue is Duration n)
                {

                }
            }));

        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }


        public static readonly DependencyProperty RepeatBehaviorProperty =
            DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(HorseTextBlock), new FrameworkPropertyMetadata(new RepeatBehavior(3), (d, e) =>
            {
                HorseTextBlock control = d as HorseTextBlock;

                if (control == null) return;

                if (e.OldValue is RepeatBehavior o)
                {

                }

                if (e.NewValue is RepeatBehavior n)
                {

                }
                control.Begin();
            }));
        private Storyboard storyboard = null;
        private void Begin()
        {
            if (this.IsLoaded == false)
                return;
            if (storyboard != null)
                storyboard.Stop();
            this.Visibility = Visibility.Visible;
            storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation(this.From, this.To, this.Duration);
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            storyboard.Completed += (l, k) =>
            {
                this.Visibility = Visibility.Collapsed;
                this.OnCompleted();
            };
            storyboard.RepeatBehavior = this.RepeatBehavior;
            storyboard.FillBehavior = FillBehavior.Stop;
            storyboard.Begin();
        }

        public static readonly RoutedEvent CompletedRoutedEvent =
            EventManager.RegisterRoutedEvent("Completed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(HorseTextBlock));

        public event RoutedEventHandler Completed
        {
            add { this.AddHandler(CompletedRoutedEvent, value); }
            remove { this.RemoveHandler(CompletedRoutedEvent, value); }
        }

        protected void OnCompleted()
        {
            RoutedEventArgs args = new RoutedEventArgs(CompletedRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }
}
