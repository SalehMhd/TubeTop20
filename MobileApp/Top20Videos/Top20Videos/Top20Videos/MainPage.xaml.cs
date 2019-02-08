using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Videos.Pages;
using Top20Videos.Services;
using Top20Videos.ViewModels;
using Xamarin.Forms;

namespace Top20Videos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        //public async Task Tblbtn_clicked(object sender, EventArgs es)
        //{
        //    var list = await CategoriesViewModel.GetCategory();
        //    var Countrylist = await CategoriesViewModel.GetRegion();
        //    Navigation.PushModalAsync(new CategoryList(list, Countrylist), true);
        //    //Navigation.PushModalAsync(new SamplesPage(), true);
        //}
    }
}
