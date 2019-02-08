using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(Top20Videos.iOS.Renderers.ScrollViewExRenderer))]
namespace Top20Videos.iOS.Renderers
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;

        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.ShowsHorizontalScrollIndicator = false;
            this.ShowsVerticalScrollIndicator = false;
        }
    }
}