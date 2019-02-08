using System;
using System.Collections.Generic;
using System.Text;
using Top20Videos.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(Picker), typeof(PickerViewRenderer))]
namespace Top20Videos.iOS.Renderers
{
    public class PickerViewRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.AdjustsFontSizeToFitWidth = true;
            }
        }
    }
}