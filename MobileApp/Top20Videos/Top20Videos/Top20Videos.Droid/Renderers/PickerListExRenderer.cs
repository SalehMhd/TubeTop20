using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MyApp.Droid;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Picker), typeof(PickerListExRenderer))]
namespace MyApp.Droid
{
    public class PickerListExRenderer : PickerRenderer
    {
        EditText label;
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                label = Control;
                Control.TextSize = 13f;
               // Control.SetMaxLines(1);
              //  Control.SetLineSpacing(10, 1);
             //   Control.TextAlignment = Android.Views.TextAlignment.ViewStart;
            }

        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName == Picker.SelectedItemProperty.PropertyName && Control != null)
        //    {
        //        Control.Text = ((Picker)sender).SelectedItem.ToString().Substring(0, 2);
        //        Control.SetText(((Picker)sender).SelectedItem.ToString().Substring(0, 2), TextView.BufferType.Normal);
                
        //    }
        //}
    }
}