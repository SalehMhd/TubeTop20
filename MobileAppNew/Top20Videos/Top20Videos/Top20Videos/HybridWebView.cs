using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Top20Videos
{
	public class HybridWebView : View
    {
        public event EventHandler<HybridWebViewErrorEventArgs> ErrorOccured;

		public static readonly BindableProperty UriProperty = BindableProperty.Create (
			propertyName: "Uri",
			returnType: typeof(string),
			declaringType: typeof(HybridWebView),
			defaultValue: default(string));
		
		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}

		public void InvokeAction (string data)
		{
			if (ErrorOccured != null)
            {
                Console.WriteLine("WebView Error Occured");
                Console.WriteLine(data);
                ErrorOccured.Invoke(this, new HybridWebViewErrorEventArgs(){ data = data });
            }
            
		}

        public void Cleanup()
        {
            ErrorOccured = null;
        }
    }

    public class HybridWebViewErrorEventArgs : EventArgs
    {
        public string data { get; set; }
    }
}
