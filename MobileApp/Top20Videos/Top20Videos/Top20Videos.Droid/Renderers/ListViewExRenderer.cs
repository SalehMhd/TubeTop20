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

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(Top20Videos.Droid.Renderers.ListViewExRenderer))]
namespace Top20Videos.Droid.Renderers
{
    public class ListViewExRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;
            var listView = Control as global::Android.Widget.ListView;
            listView.Divider = new Android.Graphics.Drawables.ColorDrawable(Android.Graphics.Color.Transparent);
            listView.DividerHeight = 15;

        }

        float StartX, StartY;
        int IsHorizontal = -1;
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.Action)
            {
                case MotionEventActions.Down:
                    StartX = ev.RawX;
                    StartY = ev.RawY;
                    this.Parent.RequestDisallowInterceptTouchEvent(true);
                    break;
                case MotionEventActions.Move:
                    if ((IsHorizontal * Math.Abs(StartX - ev.RawX))+75 < IsHorizontal * Math.Abs(StartY - ev.RawY))
                        this.Parent.RequestDisallowInterceptTouchEvent(false);
                    break;
                case MotionEventActions.Up:
                    this.Parent.RequestDisallowInterceptTouchEvent(false);
                    break;
            }
            return base.OnInterceptTouchEvent(ev);
        }

    }
}