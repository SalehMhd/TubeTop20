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
using Top20Videos.Helpers;
using Top20Videos.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomListview), typeof(CustomListViewRenderer))]
namespace Top20Videos.Droid.Renderers
{
    class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;
            Control.OverScrollMode = OverScrollMode.Never;
            
            
        }
    }
}