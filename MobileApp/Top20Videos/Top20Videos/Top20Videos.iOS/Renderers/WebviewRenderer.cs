using System;
using System.Collections.Generic;
using System.Text;
using Top20Videos.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using Mobile.iOS;
using System.Threading.Tasks;
using System.IO;
using Foundation;

[assembly: ExportRenderer(typeof(WebView), typeof(WebViewRender))]
namespace Mobile.iOS
{
    public class WebViewRender : WebViewRenderer
    {
        public override void Scrolled(UIScrollView scrollView)
        {
            
            scrollView.ScrollEnabled = false;
            
            base.Scrolled(scrollView);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.ScrollView.ScrollEnabled = false;
            this.ScrollView.Bounces = false;
            this.AllowsInlineMediaPlayback = true;
            this.MediaPlaybackAllowsAirPlay = true;
            this.MediaPlaybackRequiresUserAction = false;
            this.BackgroundColor = UIColor.Clear;
            
        }


    }
}

 