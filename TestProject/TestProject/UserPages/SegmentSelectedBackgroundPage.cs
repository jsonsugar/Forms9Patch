﻿using System;

using Xamarin.Forms;
using Forms9Patch;

namespace Forms9PatchDemo
{
    public class SegmentSelectedBackgroundPage : ContentPage
    {

        public SegmentSelectedBackgroundPage()
        {
            var matSegCtrl = new MaterialSegmentedControl
            {
                Padding = 4,
                BackgroundColor = Color.FromRgb(112, 128, 144),
                OutlineColor = Color.FromRgb(112, 128, 144).WithLuminosity(0.25),
                Segments =
                {
                    new Segment { Text = "Orange" },
                    new Segment { Text = "Blue" },
                    new Segment { Text = "Yellow" }
                }
            };

            matSegCtrl.SegmentSelected += (sender, e) =>
            {
                switch (e.Segment.Text)
                {
                    case "Orange":
                        matSegCtrl.SelectedBackgroundColor = Color.Orange;
                        matSegCtrl.SelectedTextColor = Color.Default;
                        break;
                    case "Blue":
                        matSegCtrl.SelectedBackgroundColor = Color.Blue;
                        matSegCtrl.SelectedTextColor = Color.White;
                        break;
                    case "Yellow":
                        matSegCtrl.SelectedBackgroundColor = Color.Yellow;
                        matSegCtrl.SelectedTextColor = Color.Default;
                        break;
                }
            };

            Content = new Xamarin.Forms.StackLayout
            {
                Padding = new Thickness(20),
                Children = {
                    new Xamarin.Forms.Label { Text = "SegmentSelectedBackgroundPage" },
                    matSegCtrl
                }
            };
        }
    }
}

