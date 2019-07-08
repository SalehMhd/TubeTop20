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
using Xamarin.Forms;

namespace Top20Videos
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Region> RegionsList { get; set; }

        public ObservableCollection<Video> VideoList { get; set; }

        public Region SelectedRegion { get; set; }

        public double ScreenWidth { get; set; }

        public ObservableCollection<CategoryVideos> CategoriesVideoList;

        public MainPage()
        {
            InitializeComponent();

            ScreenWidth = App.ScreenWidth;

            //Show Getting Region Info

            VideoList = new ObservableCollection<Video>();
            RegionsList = new ObservableCollection<Region>();            
            //VideosListView.ItemsSource = VideoList;
            RegionsPicker.ItemsSource = RegionsList;
            //VideosListView1.ItemsSource = VideoList;

            CategoriesVideoList = new ObservableCollection<CategoryVideos>();
            carousel.ItemsSource = CategoriesVideoList;
            CategoriesVideoList.Add(new CategoryVideos());
            CategoriesVideoList.Add(new CategoryVideos());
            CategoriesVideoList.Add(new CategoryVideos());
            CategoriesVideoList.Add(new CategoryVideos());
            CategoriesVideoList.Add(new CategoryVideos());
            CategoriesVideoList[0].InnerVideoList = new ObservableCollection<Video>();
            CategoriesVideoList[1].InnerVideoList = new ObservableCollection<Video>();
            CategoriesVideoList[2].InnerVideoList = new ObservableCollection<Video>();
            CategoriesVideoList[3].InnerVideoList = new ObservableCollection<Video>();
            CategoriesVideoList[4].InnerVideoList = new ObservableCollection<Video>();

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
                    //foreach (var region in tempRegionsList)
                    //{
                    //    RegionsList.Add(region);
                    //}

                    //SelectedRegion = RegionsList[0];
                    await LoadVideos();
                }
                else
                {

                }
            });
        }

        private async Task LoadVideos()
        {
            using (var client = new HttpClient())
            {
                var videoResponse =
                    await client.GetAsync(string.Format(ApplicationConstant.Api_VideoUrl, "DZ"));// SelectedRegion.Code));
                if (videoResponse.IsSuccessStatusCode)
                {
                    var videoContentString = await videoResponse.Content.ReadAsStringAsync();

                    var videoSer = new DataContractJsonSerializer((new List<Video>()).GetType());
                    var tempVideoList =
                        videoSer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(videoContentString))) as
                            List<Video>;

                    foreach (var video in tempVideoList)
                    {
                        switch (video.CategoryId)
                        {
                            case ApplicationConstant.AllCategryId:
                                CategoriesVideoList[0].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.MusicCategryId:
                                CategoriesVideoList[1].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.ComedyCategryId:
                                CategoriesVideoList[2].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.SportsCategryId:
                                CategoriesVideoList[3].InnerVideoList.Add(video);
                                break;
                            case ApplicationConstant.GamingCategryId:
                                CategoriesVideoList[4].InnerVideoList.Add(video);
                                break;
                        }

                        //VideoList.Add(video);
                    }

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
            Task.Run(async () => { await LoadVideos(); });

            //var picker = (Picker)sender;
            //SelectedRegion = picker.SelectedItem as Region;
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
    }
}
