using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Top20Videos
{
	public class HybridWebView : View
	{
		Action<string> action;

		public static readonly BindableProperty UriProperty = BindableProperty.Create (
			propertyName: "Uri",
			returnType: typeof(string),
			declaringType: typeof(HybridWebView),
			defaultValue: default(string));
		
		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}

		public void Cleanup ()
		{
			action = null;
		}

        public delegate void WebViewErrorHandler(object sender, EventArgs e);

        public event WebViewErrorHandler OnWebViewError;


        public void InvokeAction (string data)
		{
			if (action == null || data == null) {
				return;
			}
			action.Invoke (data);
		}
	}
}
