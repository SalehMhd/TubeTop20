using System;
using System.Collections.Generic;
using System.Text;
using Top20Videos.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CellViewRenderer))]
namespace Top20Videos.iOS.Renderers
{
    public class CellViewRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            CGRect viewBound = cell.SelectedBackgroundView.Bounds;
            UIImageView imgBg = new UIImageView();
            imgBg.Frame = new CGRect(0, 0, viewBound.Width, viewBound.Height - 20);
            imgBg.Image = new UIImage("border.png");
            cell.SelectedBackgroundView = imgBg;
            
            return cell;
        }

        
        
    }
}