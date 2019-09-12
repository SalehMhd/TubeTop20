using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace Top20Videos
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Region> RegionsList { get; set; }

        public ObservableCollection<Video> VideoList { get; set; }

        public MainPageViewModel _vm;

        public Region SelectedRegion { get; set; }

        public double ScreenWidth { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ScreenWidth = App.ScreenWidth;

            //Show Getting Region Info

            VideoList = new ObservableCollection<Video>();
            RegionsList = new ObservableCollection<Region>();
            RegionsPicker.ItemsSource = RegionsList;
            BindingContext = _vm = new MainPageViewModel();
            //webView.RegisterAction(data => DisplayAlert("Alert", "Hello " + data, "OK"));
            //webView.IsVisible = false;


            //VideosListView.ItemsSource = VideoList;
            //VideosListView1.ItemsSource = VideoList;

            //carousel.ItemsSource = CategoriesVideoList;

            Task.Run(async () =>
            {
                var client = new HttpClient();
                var regionResponse = await client.GetAsync(ApplicationConstant.Api_RegionUrl);
                if (regionResponse.IsSuccessStatusCode)
                {
                    var regionContentString = await regionResponse.Content.ReadAsStringAsync();

                    var regionSer = new DataContractJsonSerializer((new List<Region>()).GetType());
                    var tempRegionsList =
                        regionSer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(regionContentString))) as
                            List<Region>;
                    foreach (var region in tempRegionsList)
                    {
                        RegionsList.Add(region);
                    }

                    SelectedRegion = RegionsList[0];
                    await LoadVideos();
                    PrepareCategoriesWebViews();
                }
                else
                {

                }
            });
        }

        private void PrepareCategoriesWebViews()
        {
            _vm.CategoriesVideoList[0].IsVisible = false;
            _vm.CategoriesVideoList[1].IsVisible = false;
            _vm.CategoriesVideoList[2].IsVisible = false;
            _vm.CategoriesVideoList[3].IsVisible = false;
            _vm.CategoriesVideoList[4].IsVisible = false;
        }

        private async Task LoadVideos()
        {
            Console.WriteLine("LoadVideos FROM: " + SelectedRegion.Name);
            using (var client = new HttpClient())
            {
                var videoResponse =
                    await client.GetAsync(string.Format(ApplicationConstant.Api_VideoUrl, SelectedRegion.Code));
                if (videoResponse.IsSuccessStatusCode)
                {
                    var videoContentString = await videoResponse.Content.ReadAsStringAsync();

                    var videoSer = new DataContractJsonSerializer((new List<Video>()).GetType());
                    var tempVideoList =
                        videoSer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(videoContentString))) as
                            List<Video>;

                    Console.WriteLine("tempVideoListCount: " + tempVideoList.Count);

                    var tempCategoriesVideoList = new ObservableCollection<CategoryVideos>
                    {
                        new CategoryVideos(),
                        new CategoryVideos(),
                        new CategoryVideos(),
                        new CategoryVideos(),
                        new CategoryVideos()
                    };
                    tempCategoriesVideoList[0].InnerVideoList = new ObservableCollection<Video>();
                    tempCategoriesVideoList[1].InnerVideoList = new ObservableCollection<Video>();
                    tempCategoriesVideoList[2].InnerVideoList = new ObservableCollection<Video>();
                    tempCategoriesVideoList[3].InnerVideoList = new ObservableCollection<Video>();
                    tempCategoriesVideoList[4].InnerVideoList = new ObservableCollection<Video>();

                    foreach (var video in tempVideoList)
                    {
                        switch (video.CategoryId)
                        {
                            case ApplicationConstant.AllCategryId:
                                video.BindingCategoryIndex = 0;
                                tempCategoriesVideoList[0].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.MusicCategryId:
                                video.BindingCategoryIndex = 1;
                                tempCategoriesVideoList[1].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.ComedyCategryId:
                                video.BindingCategoryIndex = 2;
                                tempCategoriesVideoList[2].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.SportsCategryId:
                                video.BindingCategoryIndex = 3;
                                tempCategoriesVideoList[3].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.GamingCategryId:
                                video.BindingCategoryIndex = 4;
                                tempCategoriesVideoList[4].InnerVideoList.Add(video);
                                break;
                        }

                        //VideoList.Add(video);
                    }

                    _vm.CategoriesVideoList = tempCategoriesVideoList;

                    //carousel.ItemsSource = CategoriesVideoList;
                    //foreach (var video in tempVideoList)
                    //{
                    //    CategoriesVideoList[1].InnerVideoList.Add(video);
                    //    //VideoList.Add(video);
                    //}
                }
                Console.WriteLine($"Count is {VideoList.Count}");
            }
        }

        private void RegionPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("RegionPicker_OnSelectedIndexChanged");

            var picker = (Picker)sender;
            SelectedRegion = picker.SelectedItem as Region;

            Console.WriteLine("SelectedRegion: " + SelectedRegion);
            Task.Run(async () => { await LoadVideos(); });

        }

        private void SwipeGestureRecognizer_OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    Console.WriteLine("Left");
                    break;
                case SwipeDirection.Right:
                    Console.WriteLine("Right");
                    break;
                case SwipeDirection.Up:
                    Console.WriteLine("Up");
                    break;
                case SwipeDirection.Down:
                    Console.WriteLine("Down");
                    break;
            }
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            Console.WriteLine("Position " + e.NewValue + " selected.");
            //webView.IsVisible = false;
            _vm.CategoriesVideoList[0].IsVisible = false;
            _vm.CategoriesVideoList[1].IsVisible = false;
            _vm.CategoriesVideoList[2].IsVisible = false;
            _vm.CategoriesVideoList[3].IsVisible = false;
            _vm.CategoriesVideoList[4].IsVisible = false;
        }

        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            Console.WriteLine("Scrolled to " + e.NewValue + " percent.");
            Console.WriteLine("Direction = " + e.Direction);
        }

        private void CategoryTapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var category = ((sender as StackLayout).Children[0] as Label).Text.ToLower();

            switch (category)
            {
                case "all":
                    carousel.Position = 0;
                    break;
                case "music":
                    carousel.Position = 1;
                    break;
                case "comedy":
                    carousel.Position = 2;
                    break;
                case "sports":
                    carousel.Position = 3;
                    break;
                case "gaming":
                    carousel.Position = 4;
                    break;
            }
        }

        private void Play_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<MainPage, string>(this, "Hi", "IyH7YE0u-ys");
        }
        

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var video = e.SelectedItem as Video;
            //MessagingCenter.Send<MainPage, string>(this, "Hi", video.YouTubeId);
            _vm.CategoriesVideoList[video.BindingCategoryIndex].VMPlayState = 1;
            _vm.CategoriesVideoList[video.BindingCategoryIndex].IsVisible = true;
        }

        private void Carousel_OnChildAdded(object sender, ElementEventArgs e)
        {
            var ele = sender as Element;
            var v = sender as View;
            var g = e.Element;
            var j = "asd";
        }

        private void WebView_OnErrorOccured(object sender, HybridWebViewErrorEventArgs e)
        {
            DisplayAlert("Alert", "Hello " + e.data, "OK");
        }
    }
}
