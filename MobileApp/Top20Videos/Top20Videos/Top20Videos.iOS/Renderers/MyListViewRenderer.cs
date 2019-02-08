using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Foundation;
using Top20Videos.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(CustomListViewRenderer))]
namespace Top20Videos.iOS.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            // to remove separator line
            if (Control == null) return;
            var list = Control;
            list.SeparatorColor = UIColor.FromRGBA(0, 0, 0, 0);
            list.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            list.Bounces = false;
        }

    }
}
