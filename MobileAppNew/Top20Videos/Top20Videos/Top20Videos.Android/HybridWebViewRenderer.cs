
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Java.IO;
using System;
using System.ComponentModel;
using Android.Webkit;
using Top20Videos;
using Top20Videos.Droid;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace Top20Videos.Droid
{
    public class JavascriptResult : Java.Lang.Object, IValueCallback
    {
        public string Result;
        public void OnReceiveValue(Java.Lang.Object result)
        {
            Result = ((Java.Lang.String)result).ToString();
        }
    }

    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
    {
        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        Context _context;

        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "PlayState")
            {
                var g = Element.PlayState;
                var j = g;
                return;
            }

            if (e.PropertyName == "YouTubeId")
            {
                var webView = this.Control;
                webView.EvaluateJavascript(string.Format("loadPlayerVideoById(\"{0}\")", Element.YouTubeId),
                    new JavascriptResult());
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);
            
            if (Control == null)
            {
                var webView = new Android.Webkit.WebView(_context);
                webView.Settings.JavaScriptEnabled = true;
                webView.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}"));
                SetNativeControl(webView);

                MessagingCenter.Subscribe<MainPage, string>(this, "Hi", (sender, arg) => {
                    var f = arg;
                    webView.EvaluateJavascript(string.Format("loadPlayerVideoById(\"{0}\")", arg),
                        new JavascriptResult());
                    var he = arg;
                });
            }
            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (e.NewElement != null)
            {
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");
            }


        }
    }
}
