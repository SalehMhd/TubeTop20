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

[assembly: ExportRenderer(typeof(Label), typeof(LabelExRenderer))]
namespace MyApp.Droid
{
    public class LabelExRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!String.IsNullOrEmpty(Element.FontFamily))
            {
                switch (Element.FontFamily)
                {
                    case "HelveticaNeue-CondensedBold":
                        Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/HelveticaNeue-CondensedBold.otf");
                        break;
                    case "Helvetica-Condensed-Thin":
                        Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/Helvetica-Condensed-Thin.TTF");
                        Control.SetLineSpacing(4, 1);
                        
                        //Control.LetterSpacing = 0.1f;
                        break;
                }
            }
                
            //Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/" + Element.FontFamily + ".otf");
        }

    }
}