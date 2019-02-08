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


[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(Top20Videos.Droid.Renderers.WebViewExRenderer))]
namespace Top20Videos.Droid.Renderers
{
    public class WebViewExRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            this.VerticalScrollBarEnabled = false;
            this.HorizontalScrollBarEnabled = false;

            if (this.Control != null)
            {
                this.Control.Settings.MediaPlaybackRequiresUserGesture = false;
                this.Control.VerticalScrollBarEnabled = false;
                Control.HorizontalScrollBarEnabled = false;
                if (Convert.ToInt32(Android.OS.Build.VERSION.Sdk) >= 23) // 6.0 or leter
                {
                    Control.ScrollChange += Control_ScrollChange;
                }
            }

        }

        private void Control_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
            ((Android.Webkit.WebView)(sender)).ScrollTo(0, 0);
        }
    }
}