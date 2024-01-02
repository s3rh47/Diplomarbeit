﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;

namespace Hortrainingsprogramm.Components
{

    public class RippleEffectDecorator : ContentControl
    {
        static RippleEffectDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleEffectDecorator), new FrameworkPropertyMetadata(typeof(RippleEffectDecorator)));
        }

        public Brush HighlightBackground
        {
            get { return (Brush)GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register("HighlightBackground", typeof(Brush), typeof(RippleEffectDecorator), new PropertyMetadata(Brushes.White));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Ellipse ellipse = (GetTemplateChild("PART_ellipse") as Ellipse)!;
            Grid grid = (GetTemplateChild("PART_grid") as Grid)!;
            Storyboard animation = (grid.FindResource("PART_animation") as Storyboard)!;

            this.AddHandler(MouseDownEvent, new RoutedEventHandler((sender, e) =>
            {
                var targetWidth = Math.Max(ActualWidth, ActualHeight) * 3;
                var mousePosition = (e as MouseButtonEventArgs).GetPosition(this);
                var startMargin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);
                //set initial margin to mouse position
                ellipse.Margin = startMargin;
                //set the to value of the animation that animates the width to the target width
                (animation.Children[0] as DoubleAnimation).To = targetWidth;
                //set the to and from values of the animation that animates the distance relative to the container (grid)
                (animation.Children[1] as ThicknessAnimation).From = startMargin;
                (animation.Children[1] as ThicknessAnimation).To = new Thickness(mousePosition.X - targetWidth / 2, mousePosition.Y - targetWidth / 2, 0, 0);
                ellipse.BeginStoryboard(animation);
            }), true);
        }
    }

}
