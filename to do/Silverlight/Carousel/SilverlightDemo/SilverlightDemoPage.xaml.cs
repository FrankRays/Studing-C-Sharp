using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightCarouselDemo;

namespace SilverlightDemo
{
    public partial class SilverlightDemoPage : UserControl
    {
        public SilverlightDemoPage()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CarouselSpeedSlider.Value = 200;
            LookdownOffsetSlider.Value = 20;
            FadeSlider.Value = 0.5;
            ScaleSlider.Value = 0.5;



            Storyboard s = Resources["Intro"] as Storyboard;
            s.Begin();
        }


        #region Carousel

        private void CarouselSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.RotationSpeed = e.NewValue;
        }

        private void LookdownOffsetSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.LookDownOffset = e.NewValue;
            ExampleCarouselControl.SetElementPositions();
        }

        private void CarouselFadeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.Fade = e.NewValue;
            ExampleCarouselControl.SetElementPositions();
        }

        private void CarouselScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.Scale = e.NewValue;
            ExampleCarouselControl.SetElementPositions();
        }

        private void VerticalOrientationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.Width = 0;
            ExampleCarouselControl.Height = 600;
            ExampleCarouselControl.ReInitialize();
        }

        private void HorizontalOrientationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (ExampleCarouselControl == null) return;
            ExampleCarouselControl.Width = 600;
            ExampleCarouselControl.Height = 0;
            ExampleCarouselControl.ReInitialize();
        }

        private void ExampleCarouselControl_OnElementSelected(object sender)
        {
            SphereControl selected = ExampleCarouselControl.CurrentlySelected as SphereControl;
            CurrentlySelectedEllipse.Fill = selected.SphereFill;
            if ((CurrentlySelectedNameTextBlock != null) && (CurrentlySelectedNameShadowTextBlock != null))
            {
                CurrentlySelectedNameTextBlock.Foreground = selected.SphereFill;
                CurrentlySelectedNameTextBlock.Text = selected.Name;
                CurrentlySelectedNameShadowTextBlock.Text = selected.Name;
            }
        }

        #endregion





    }
}
