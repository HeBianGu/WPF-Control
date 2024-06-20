﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
namespace H.Controls.OutlookBar
{
    public class AnimationDecorator : Decorator
    {
        private double targetHeight = 0;

        static AnimationDecorator()
        {
            // AnimationDecorator.ActualHeightProperty.OverrideMetadata(typeof(AnimationDecorator), new UIPropertyMetadata((double)0.0));
        }

        public AnimationDecorator()
            : base()
        {
            this.ClipToBounds = true;
        }


        /// <summary>
        /// Specify whether to apply opactiy animation.
        /// </summary>
        public bool OpacityAnimation
        {
            get { return (bool)GetValue(OpacityAnimationProperty); }
            set { SetValue(OpacityAnimationProperty, value); }
        }

        public static readonly DependencyProperty OpacityAnimationProperty =
            DependencyProperty.Register("OpacityAnimation",
            typeof(bool),
            typeof(AnimationDecorator),
            new UIPropertyMetadata(true));



        /// <summary>
        /// Gets or sets whether the decorator is expanded or collapsed.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded",
            typeof(bool),
            typeof(AnimationDecorator),
            new PropertyMetadata(true, IsExpandedChanged));


        public static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AnimationDecorator expander = d as AnimationDecorator;
            bool expanded = (bool)e.NewValue;
            if (expander.CanAnimate)
            {
                expander.AnimateExpandedChanged(expanded);
            }
            else
            {
                expander.UnanimatedExpandedChanged(expanded);
            }
        }


        /// <summary>
        /// Specify whether to apply animation when IsExpanded is changed.
        /// </summary>
        public DoubleAnimation HeightAnimation
        {
            get { return (DoubleAnimation)GetValue(HeightAnimationProperty); }
            set { SetValue(HeightAnimationProperty, value); }
        }

        public static readonly DependencyProperty HeightAnimationProperty =
            DependencyProperty.Register("HeightAnimation",
            typeof(DoubleAnimation),
            typeof(AnimationDecorator),
            new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the duration for the animation.
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(AnimationDecorator), new UIPropertyMetadata(new Duration(new TimeSpan(0, 0, 0, 0, 250))));


        private bool animating = false;


        private void UnanimatedExpandedChanged(bool expanded)
        {
            if (this.Child != null)
            {
                this.YOffset = expanded ? 0 : -this.Child.DesiredSize.Height;
            }
        }

        /// <summary>
        /// Perform the animation when teh IsExpanded has changed.
        /// </summary>
        /// <param name="expanded"></param>
        private void AnimateExpandedChanged(bool expanded)
        {
            if (this.Child != null)
            {
                if (this.YOffset > 0) this.YOffset = 0;
                if (-this.YOffset > this.Child.DesiredSize.Height) this.YOffset = -this.Child.DesiredSize.Height;
                DoubleAnimation animation = this.HeightAnimation;
                if (animation == null) animation = CreateDoubleAnimation();
                animation.From = null;
                animation.To = expanded ? 0 : -this.Child.DesiredSize.Height;
                animation.Completed += new EventHandler(animation_Completed);
                animating = true;
                this.BeginAnimation(AnimationDecorator.YOffsetProperty, animation);

                if (this.OpacityAnimation)
                {
                    animation.From = null;
                    animation.To = expanded ? 1 : 0;
                    this.BeginAnimation(AnimationDecorator.AnimationOpacityProperty, animation);
                }
            }
            else
            {
                this.YOffset = int.MinValue;
            }
        }

        private DoubleAnimation CreateDoubleAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.DecelerationRatio = 0.8;
            animation.Duration = this.Duration;
            return animation;
        }

        private void animation_Completed(object sender, EventArgs e)
        {
            animating = false;
        }


        /// <summary>
        /// Perform the animation when the child's height has changed.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Double AnimatedResize(Double h)
        {
            Double delta = targetHeight - h;
            DoubleAnimation animation = this.HeightAnimation;
            if (animation == null) animation = CreateDoubleAnimation();
            targetHeight = h;
            animation.From = delta;
            animation.To = 0;
            this.BeginAnimation(AnimationDecorator.HeightOffsetProperty, animation);
            return delta;
        }





        /// <summary>
        /// Gets or sets the Opacity for animation. This dependency property can be used to modify the opacity of an outer control.
        /// </summary>
        public double AnimationOpacity
        {
            get { return (double)GetValue(AnimationOpacityProperty); }
            set { SetValue(AnimationOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationOpacityProperty =
            DependencyProperty.Register("AnimationOpacity", typeof(double), typeof(AnimationDecorator), new UIPropertyMetadata(1.0));


        /// <summary>
        /// A helper value for the current state while in animation.
        /// </summary>
        internal Double HeightOffset
        {
            get { return (Double)GetValue(HeightOffsetProperty); }
            set { SetValue(HeightOffsetProperty, value); }
        }

        internal static readonly DependencyProperty HeightOffsetProperty =
            DependencyProperty.Register("HeightOffset", typeof(Double), typeof(AnimationDecorator), new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure));


        /// <summary>
        /// A helper value for the current state while in animation.
        /// </summary>
        internal Double YOffset
        {
            get { return (Double)GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        public static readonly DependencyProperty YOffsetProperty =
            DependencyProperty.Register("YOffset", typeof(Double), typeof(AnimationDecorator),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Measures the child element of a <see cref="T:System.Windows.Controls.Decorator"/> to prepare for arranging it during the <see cref="M:System.Windows.Controls.Decorator.ArrangeOverride(System.Windows.Size)"/> pass.
        /// </summary>
        /// <param name="constraint">An upper limit <see cref="T:System.Windows.Size"/> that should not be exceeded.</param>
        /// <returns>
        /// The target <see cref="T:System.Windows.Size"/> of the element.
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            if (this.Child == null) return new Size(0, 0);
            Size size;

            if (double.IsInfinity(constraint.Height))
            {
                this.Child.Measure(new Size(constraint.Width, Double.PositiveInfinity));
                Double childHeight = this.Child.DesiredSize.Height;
                Double deltaHeight = 0;
                if (this.AnimateOnContentHeightChanged && this.IsLoaded && this.IsVisible && this.CanAnimate)
                {
                    if (targetHeight != childHeight)
                    {
                        deltaHeight = AnimatedResize(childHeight);
                        if (animating)
                        {
                            AnimateExpandedChanged(this.IsExpanded);
                        }
                    }
                }
                else targetHeight = childHeight;

                double w = this.IsExpanded ? this.Child.DesiredSize.Width : 0;
                size = new Size(w, Math.Max(0d, childHeight + this.YOffset + this.HeightOffset + deltaHeight));

            }
            else
            {
                size = base.MeasureOverride(constraint);
            }
            if (this.Child != null)
            {
                this.Child.IsEnabled = size.Height > 0;
            }
            if (size.Height == 0) this.AnimationOpacity = 0;
            return size;
        }


        /// <summary>
        /// Arranges the content of a <see cref="T:System.Windows.Controls.Decorator"/> element.
        /// </summary>
        /// <param name="arrangeSize">The <see cref="T:System.Windows.Size"/> this element uses to arrange its child content.</param>
        /// <returns>
        /// The <see cref="T:System.Windows.Size"/> that represents the arranged size of this <see cref="T:System.Windows.Controls.Decorator"/> element and its child.
        /// </returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (this.Child == null) return arrangeSize;

            this.Child.Arrange(new Rect(0d, this.YOffset, arrangeSize.Width, this.Child.DesiredSize.Height));
            Double h = Math.Max(0, this.Child.DesiredSize.Height + this.YOffset);
            return new Size(arrangeSize.Width, h);
        }



        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanAnimate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(AnimationDecorator), new UIPropertyMetadata(true));




        public bool AnimateOnContentHeightChanged
        {
            get { return (bool)GetValue(AnimateOnContentHeightChangedProperty); }
            set { SetValue(AnimateOnContentHeightChangedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimateOnContentHeightChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimateOnContentHeightChangedProperty =
            DependencyProperty.Register("AnimateOnContentHeightChanged", typeof(bool), typeof(AnimationDecorator), new UIPropertyMetadata(true));



    }
}
