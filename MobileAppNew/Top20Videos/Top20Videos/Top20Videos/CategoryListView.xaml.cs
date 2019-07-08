using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Top20Videos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryListView : ContentView
	{
        public ListView VideoListView
        {
            get { return VideosListView; }
        }

        public CategoryListView ()
		{
			InitializeComponent ();
        }
	}
}