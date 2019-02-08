using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Videos.Helpers;
using Top20Videos.Models;
using Top20Videos.Pages;
using Top20Videos.Services;
using Top20Videos.ViewModels;
using Xamarin.Forms;

namespace Top20Videos
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static WebView VideoPlayer;
        public App()
        {
            InitializeComponent();

            MainPage = new CategoryList();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            VideoPlayer.Eval("pause()");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }
}
