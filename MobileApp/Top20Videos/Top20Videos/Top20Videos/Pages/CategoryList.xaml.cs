using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Videos.Helpers;
using Top20Videos.Models;
using Top20Videos.ViewModels;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Diagnostics;
using System.Net.Http;
using Plugin.Share;

namespace Top20Videos.Pages
{
    public partial class CategoryList : ContentPage
    {
        #region[Global or class lavel Variables]
        int selectedIndex;
        int CrasualPreviousPosition = 0;
        //ListView currentListView;
        VideoModel previous;
        VideoModel Next;
        VideoModel current;
        ObservableCollection<Categories> list;
        ObservableCollection<VideoModel> currentListViewItems;
        //Not REquired bool isTappedOnTab = false;
        INetworkConnection networkConnection;
        string currentTab;
        double previousWidth = 0;
        double previousHeight = 0;
        bool isPlaying = false;
        bool isPotriat = true;
        bool playClick = false;
        bool pageload = true;
        bool openregionpopup = false;
        private static List<ActiveListViewOnPage> _activeListOnPage;
        string previousCat = "";
        bool isOrientationChange = false;
        bool appLaunch = true;
        bool stop = true;
        string HtmlContent = "";
        //Not REquired bool isOpeningFirstTime = true;
        string videoUrl = "";
        List<Region> Regions;
        #endregion

        public CategoryList()
        {
            Regions = new List<Region>();
            InitializeComponent();

            LoadInitial();

            //LoadRegionList();
        }
        public void LoadInitial()
        {

            if (Device.RuntimePlatform == Device.iOS) { ListLoadIndicator.IsRunning = true; ListLoadIndicator.IsVisible = true; }
            ply.IsVisible = false;

            Device.BeginInvokeOnMainThread(() =>
            {

            });

            // check network connection
            networkConnection = DependencyService.Get<INetworkConnection>();
            ShowNetworkAlert();

            #region Region
            //Not REquired var regions = RegionList.GetAll().OrderBy(x => x.Code).ToList();
            //Not REquired foreach (var item in regions)
            //Not REquired {
            //regionList.Items.Add(string.Format("{0} - {1}", item.Code ?? "", item.Name ?? ""));

            //Not required Region regionList.Items.Add(item.Code ?? "");
            //Not REquired }
            //Not Required regionList.SelectedIndex = 73;
            //Not REquired regionList.BackgroundColor = Color.FromRgba(0, 0, 0, 0);

            #endregion

            #region Regions
            //var regions = LanguageList.GetAll().OrderBy(x => x.ID).ToList();
            //foreach (var item in regions)
            //{
            //    LangList.Items.Add(item.RelevanceLanguageName ?? "");
            //}

            //LangList.SelectedIndex = 0;
            //LangList.BackgroundColor = Color.FromRgba(0, 0, 0, 0);

            GetDefaultUSVideo();
            #endregion

            bindPlayerString((App.ScreenHeight / 3).ToString(), App.ScreenWidth.ToString());

            currentTab = all.Text;
            SetSelected();
            ListLoadIndicator.IsVisible = false;

            if (Device.RuntimePlatform == Device.iOS) { ListLoadIndicator.IsRunning = true; ListLoadIndicator.IsVisible = true; }
            ply.IsVisible = true;
            App.VideoPlayer = viewPlayer;


            // LanguageListview.ItemSelected += LanguageListview_ItemSelected;


        }
        private void LoadRegionList()
        {
            if (!openregionpopup)
            {
                openregionpopup = true;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var HeaderHeight = HeaderStackLayout.Height;
                    //LanguageListview.Margin = new Thickness(0, HeaderHeight + 8, 0, 0);
                    Regions = RegionList.GetAll().OrderBy(x => x.ID).ToList();
                    LanguageListview.ItemsSource = Regions;
                    LanguageListview.RowHeight = 45;
                    LanguageListview.HeightRequest = (45 * Regions.Count);
                    LanguageListview.WidthRequest = 110;
                    //LSTBox.HeightRequest = LanguageListview.Height;
                    //LSTBox.WidthRequest = LanguageListview.Width;
                    LanguageListview.Margin = 1;

                }
                else
                {
                    var HeaderHeight = HeaderStackLayout.Height;
                    
                    //LanguageListview.Margin = new Thickness(0, HeaderHeight + 8, 0, 0);
                    Regions = RegionList.GetAll().OrderBy(x => x.ID).ToList();
                    LanguageListview.ItemsSource = Regions;
                    LanguageListview.RowHeight = 50;
                    LanguageListview.HeightRequest = (50* Regions.Count);
                    LanguageListview.WidthRequest = 110;
                    LanguageListview.Margin = 1;
                    
                    //LSTBox.Margin = new Thickness(0, HeaderHeight + 6, 0,0);
                    //LSTBox.HeightRequest = ListviewHeight + 2;
                }
                
                
                ///***Logic to change color
                var selected = Regions.FirstOrDefault(o => o.Code.ToLower() == ApplicationConstant.SelectedRegion.ToLower());
                
                selected.isSelected = "#F50000";
                selected.TextColor = "#FFFFFF";
               // selected.StackPadding = "0,10,0,0";

                foreach (var item in Regions)
                {
                    if (item != selected)
                    {
                        item.isSelected = "#28292D";
                        item.TextColor = "#9E9E9E";
                      //item.StackPadding = "0,15,0,0";
                    }
                }
                
                
            }
            else
            {
                openregionpopup = false;
                LanguageGridView.IsVisible = false;
                //LanguageListview.IsVisible = false;
                //LSTBox.IsVisible = false;
            }
        }


        //private void LanguageListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var position = e.SelectedItem;

        //}

        private void bindPlayerString(string h = "", string w = "", string v = "", bool overwrite = false)
        {
            if (viewPlayer.Source == null || overwrite)
            {
                bindPlayerStingURL(h, w, v);
            }
            else
            {
                switch (viewPlayer.Source.GetType().Name)
                {
                    case "UrlWebViewSource":
                        if (!((UrlWebViewSource)viewPlayer.Source).Url.Contains("file://"))
                            bindPlayerStingURL(h, w, v);
                        break;
                    default:
                        break;
                }
            }
        }
        private void bindPlayerStingURL(string h, string w, string v)
        {
            if (string.IsNullOrEmpty(HtmlContent.Trim()))
            {
                HtmlContent = "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width\"/><style>body{margin:0px 0px 0px 0px;}</style></head> <body> <div id=\"player\"></div><script>var tag=document.createElement('script'); tag.src=\"https://www.youtube.com/iframe_api\"; var firstScriptTag=document.getElementsByTagName('script')[0]; firstScriptTag.parentNode.insertBefore(tag, firstScriptTag); var player; function onYouTubeIframeAPIReady(){player=new YT.Player('player',{width:'" + w + "', height: '" + h + "',videoId: '" + v + "',playerVars:{'controls': 1, 'showinfo': 0, 'rel': 0, 'playsinline':1}, events:{'onReady': onPlayerReady}});}function onPlayerReady(event){event.target.playVideo();}function loadPlayerVideoById(id){player.loadVideoById(id);}function pause(){player.pauseVideo();}function stop(){player.stopVideo();}function play(){player.playVideo();}function setPlayerSize(h, w){player.setSize(w,h)}</script> </body></html>";
            }
            viewPlayer.Source = new HtmlWebViewSource
            {
                Html = HtmlContent
            };
        }

        #region [Header TapGesture]
        private void ALL_OnTapped(object sender, EventArgs es)
        {

            if (Carousel.Item != null)
            {
                currentTab = all.Text.Trim();
                Carousel.Position = 0;
            }

        }
        private void MUSIC_OnTapped(object sender, EventArgs es)
        {
            if (Carousel.Item != null)
            {
                currentTab = music.Text.Trim();
                Carousel.Position = 2;
            }
        }
        private void tab24_OnTapped(object sender, EventArgs es)
        {
            if (Carousel.Item != null)
            {
                currentTab = tab24.Text.Trim();
                Carousel.Position = 1;
            }
        }
        private void SPORTS_OnTapped(object sender, EventArgs es)
        {
            if (Carousel.Item != null)
            {
                currentTab = sports.Text.Trim();
                Carousel.Position = 4;
            }
        }
        private void NEWS_OnTapped(object sender, EventArgs es)
        {
            if (Carousel.Item != null)
            {
                currentTab = comedy.Text.Trim(); ;
                Carousel.Position = 3;
            }
        }
        private void GAMING_OnTapped(object sender, EventArgs es)
        {
            if (Carousel.Item != null)
            {
                currentTab = gaming.Text.Trim();
                Carousel.Position = 5;
            }
        }
        #endregion

        #region [List Events]
        private async void SelectedItem(object sender, SelectedItemChangedEventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            if (current == (VideoModel)es.SelectedItem)
            {
                return;
            }

            UserDialogs.Instance.Toast(string.Format("Loading Video"), TimeSpan.FromMilliseconds(1500));
            VideoModel lstobj = (VideoModel)es.SelectedItem;

            if (pageload)
                playClick = true;

            if (lstobj != null)
            {
                if (Device.RuntimePlatform == Device.Android)
                    ((ListView)sender).ScrollTo(es.SelectedItem, ScrollToPosition.MakeVisible, true);
                //LoadIndicator.IsRunning = true;
                wbVid.IsVisible = true;
                //webview player source set
                if (isPotriat)
                {
                    wbVid.HeightRequest = (Height / 3);
                    wbVid.WidthRequest = Width;
                    viewPlayer.HeightRequest = (Height / 3);
                    viewPlayer.WidthRequest = Width;
                    setMargin.Margin = new Thickness(0, wbVid.HeightRequest + 15, 0, 50);
                }
                if (viewPlayer.Source == null)
                {
                    bindPlayerString(viewPlayer.Height.ToString(), viewPlayer.Width.ToString(), lstobj.YouTubeId);
                }
                else
                {
                    viewPlayer.Eval(string.Format("loadPlayerVideoById(\"{0}\")", lstobj.YouTubeId));
                    viewPlayer.Eval(string.Format("setPlayerSize(\"{0}\",\"{1}\")", viewPlayer.HeightRequest, viewPlayer.WidthRequest));
                }
                videoUrl = lstobj.VideoUrl;
                isPlaying = true;
                ply.Source = "pause_bottom.png";
                ytplayBtn.Source = "ytplay.png";
                pageload = false;
            }
            current = lstobj;
            stop = false;
            currentListViewItems = (ObservableCollection<VideoModel>)CurrentListViewOnPage(currentTab).ItemsSource;
            setNextAndPrevious(current);
        }

        private void ItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {

        }

        protected void setNextAndPrevious(VideoModel selectedItem)
        {
            // VideoPlayer.Pause();
            if (selectedItem != null && currentListViewItems != null)
            {
                var Current = currentListViewItems.Where(x => x.ID == selectedItem.ID).FirstOrDefault();
                int currentIndex = currentListViewItems.IndexOf(Current);
                previous = null;
                Next = null;
                selectedIndex = currentIndex;
                if (currentIndex > 0)
                {
                    previous = currentListViewItems[currentIndex - 1];
                    pre.Source = "previous.png"; //Enable disable previous button
                }
                else
                {
                    pre.Source = "previous_disable.png"; //Enable disable previous button
                }
                if (currentIndex < currentListViewItems.Count - 1)
                {
                    Next = currentListViewItems[currentIndex + 1];
                    next.Source = "next_bottom.png";//Enable disable Next button
                }
                else
                {
                    next.Source = "next_disable.png";//Enable disable Next button
                }
            }
            else
            {
                if (currentListViewItems != null)
                {
                    ply.Source = "play_bottom.png";
                    ytplayBtn.Source = "ytplaydis.png";
                    isPlaying = false;
                    previous = null;
                    Next = null;
                    next.Source = "next_disable.png";
                    pre.Source = "previous_disable.png";
                }

            }
        }
        #endregion

        #region[Footer Actions]  
        private async void previous_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            if (previous != null && CurrentListViewOnPage(currentTab) != null)
            {
                viewPlayer.Eval("stop()");
                CurrentListViewOnPage(currentTab).SelectedItem = previous; //make it selected         
            }
        }

        private async void play_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            // currentListView = null;            
            //currentListView = CurrentListViewOnPage(currentTab);
            playClick = true;
            if (CurrentListViewOnPage(currentTab) != null && (CurrentListViewOnPage(currentTab).SelectedItem == null || pageload))
            {
                CurrentListViewOnPage(currentTab).SelectedItem = list.Where(x => x.CategoryName.ToUpper() == currentTab.ToUpper()).FirstOrDefault().CategoryVideolist.FirstOrDefault();
                // ply.Source = "play_bottom.png";
                pageload = false;
                ply.Source = "pause_bottom.png";
                ytplayBtn.Source = "ytplay.png";
                isPlaying = true;
            }
            else
            {
                if (isPlaying)
                {
                    ply.Source = "play_bottom.png";
                    //ytplayBtn.Source = "ytplay.png";
                    isPlaying = false;
                    viewPlayer.Eval("pause()");
                }
                else
                {
                    ply.Source = "pause_bottom.png";
                    //ytplayBtn.Source = "ytplay.png";
                    isPlaying = true;
                    viewPlayer.Eval("play()");
                }
            }
        }

        private async void next_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            if (Next != null && CurrentListViewOnPage(currentTab) != null)
            {
                viewPlayer.Eval("stop()");
                CurrentListViewOnPage(currentTab).SelectedItem = Next; // make it current           
            }
        }

        private async void reply_bottom_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            string message = "";

            if (current != null)
            {
                message = "This video made it to today's YouTube top list: " + current.VideoUrl + System.Environment.NewLine + "To see the full toplist download this app: http://onelink.to/tubetop20";
            }
            else
            {
                message = "Download this app for today's YouTube video top list: http://onelink.to/tubetop20";
            }

            //string Message = string.Format("This video made it to today's YouTube top list: {0}, To see the full toplist download this app: {1}", current != null ? current.VideoUrl : "", Device.RuntimePlatform == Device.Android ? PackageName.AndroidAppUrl : PackageName.IOSAppUrl);

            Plugin.Share.Abstractions.ShareMessage msg = new Plugin.Share.Abstractions.ShareMessage();
            msg.Text = message;
            msg.Title = "TubeTop20 TODAY";

            await CrossShare.Current.Share(msg);
        }

        private void star_bottom_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            if (Device.RuntimePlatform == Device.iOS)
            {
                //Device.OpenUri(new Uri("http://itunes.apple.com/app/id=" + PackageName.IOSName));                

                Device.OpenUri(new Uri("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=1240804037&pageNumber=0&sortOrdering=2&mt=8"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://play.google.com/store/apps/details?id=" + PackageName.AndoridName));
            }
        }

        #region region Not REquired
        //private void RegionList_Change(object sender, EventArgs es)
        //{
        //    setMargin.Margin = new Thickness(0, 15, 0, 50);
        //    wbVid.IsVisible = false; // to hide webView

        //    ply.Source = "play_bottom.png";
        //    isPlaying = false;
        //    viewPlayer.Eval("stop()");
        //    var selectedValue = regionList.Items[regionList.SelectedIndex];
        //    //var selectedValue = regionList.Items[regionList.SelectedIndex].Substring(0, 3);

        //    if (Device.RuntimePlatform == Device.Android)
        //    {
        //        Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading {0} Videos", selectedValue));
        //    }
        //    else
        //    {
        //        ListLoadIndicator.IsRunning = true;
        //        ListLoadIndicator.IsVisible = true;
        //        UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", selectedValue));
        //    }

        //    Device.BeginInvokeOnMainThread(async () =>
        //     {
        //         ShowNetworkAlert();
        //         list = await CategoriesViewModel.GetCategory(selectedValue);
        //         BindingContext = list;
        //         if (Device.RuntimePlatform == Device.Android) { UserDialogs.Instance.HideLoading(); }
        //         else
        //         {
        //             ListLoadIndicator.IsRunning = false;
        //             ListLoadIndicator.IsVisible = false;
        //         }
        //         if (!isOpeningFirstTime)
        //         {
        //             UserDialogs.Instance.Toast("Region change only affect videos with region restrictions");
        //         }
        //         else
        //         {
        //             isOpeningFirstTime = false;
        //         }


        //     });

        //}

        private void LanguageIcon_OnTapped(object sender, EventArgs es)
        {
            LanguageGridView.IsVisible = !LanguageGridView.IsVisible;
            var languageiconHeight = HeaderStackLayout.Height;
            if (Device.RuntimePlatform == Device.iOS)
            {
                LSTBox.HeightRequest = 27.5;
                LSTBox.WidthRequest = 112.5;
                LanguageGridView.Margin = new Thickness(0, languageiconHeight + 8, 12, 0);
            }
            else
            {
                LSTBox.HeightRequest = 52;
                LSTBox.WidthRequest = 112;
                LanguageGridView.Margin = new Thickness(0, languageiconHeight + 8, 12, 0);
            }


            //LanguageListview.IsVisible = !LanguageListview.IsVisible;
            //LSTBox.IsVisible = !LSTBox.IsVisible;

            
            //LanguageListview.Margin = new Thickness(0, languageiconHeight + 8, 0, 0);
            
            //LSTBox.Margin = new Thickness(0, languageiconHeight + 8, 0, 0);
           // LSTBox.HeightRequest = ListviewHeight + 2;


        }

        private void contentView_OnTapped(object sender,EventArgs e)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
        }
        private void StackLayoutLanguageHeader_OnTapped(object sender, EventArgs e)
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
        }
        

        private void LanguageListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var listview = sender as ListView;
            
            if (listview.SelectedItem == null) return;
            foreach (Region item in Regions)
            {
                if ((Region)listview.SelectedItem == item)
                {
                    item.isSelected = "#F50000";
                    item.TextColor = "#ffffff";
                    //item.StackPadding = "0,10,0,0";
                    ApplicationConstant.SelectedRegion = item.Code;

                    //var lang = (sender as ListView).SelectedItem;

                    LanguageGridView.IsVisible = false;
                    //LanguageListview.IsVisible = false;
                    //LSTBox.IsVisible = false;

                    #region After tapped process

                    setMargin.Margin = new Thickness(0, 15, 0, 50);
                    wbVid.IsVisible = false; // to hide webView

                    ply.Source = "play_bottom.png";
                    isPlaying = false;
                    viewPlayer.Eval("stop()");
                    var selectedValue = item.Code;
                    ApplicationConstant.SelectedRegion = selectedValue; //RegionChange(selectedValue);
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading {0} Videos", selectedValue));
                    }
                    else
                    {
                        ListLoadIndicator.IsRunning = true;
                        ListLoadIndicator.IsVisible = true;
                        UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", selectedValue));
                    }

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        ShowNetworkAlert();
                        list = await CategoriesViewModel.GetCategory(selectedValue, "");
                        BindingContext = list;
                        if (Device.RuntimePlatform == Device.Android) { UserDialogs.Instance.HideLoading(); }
                        else
                        {
                            ListLoadIndicator.IsRunning = false;
                            ListLoadIndicator.IsVisible = false;
                        }
                    });
                    #endregion
                }
                else
                {
                    item.isSelected = "#28292D";
                    item.TextColor = "#9E9E9E";
                   // item.StackPadding = "0,15,0,0";
                }
            }
            listview.SelectedItem = null;


        }

        string RegionChange(string Region)
        {
            string RegionCode = string.Empty;

            switch (Region)
            {
                case "ENGLISH":
                    RegionCode = "en";
                    break;
                case "ARABIC":
                    RegionCode = "ar";
                    break;
                case "FRENCH":
                    RegionCode = "fr";
                    break;
                case "SPANISH":
                    RegionCode = "es";
                    break;
                case "GLOBAL":
                    RegionCode = "Gn";
                    break;


            }
            return RegionCode;
        }

        private void Fbicon_OnTapped(object sender, EventArgs es)
        {
            //Device.OpenUri(new Uri("https://www.facebook.com/TubeTop20Today/ "));

            // check network connection
            networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                DisplayAlert("No Internet Connection", "Please check your internet connection and try again.", "OK");
            }
            else
            {
                try
                {
                   // Device.OpenUri(new Uri("facebook://page?id=Xamarin_tuts_6789-682661745431901"));
                    Device.OpenUri(new Uri("fb://page/1433307326741231"));
                }
                catch(Exception ex)
                {
                    Device.OpenUri(new Uri("https://www.facebook.com/TubeTop20Today/ "));
                }
               
            }

            
        }

        //void OnLanguageLabelClicked(object sender, EventArgs es)
        //{

        //    var lang = (sender as Label).Text;

        //    LanguageListview.IsVisible = false;

        //    #region After tapped process

        //    setMargin.Margin = new Thickness(0, 15, 0, 50);
        //    wbVid.IsVisible = false; // to hide webView

        //    ply.Source = "play_bottom.png";
        //    isPlaying = false;
        //    viewPlayer.Eval("stop()");
        //    var selectedValue = (sender as Label).Text;
        //    ApplicationConstant.SelectedRegion = RegionChange(selectedValue);
        //    if (Device.RuntimePlatform == Device.Android)
        //    {
        //        Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading {0} Videos", selectedValue));
        //    }
        //    else
        //    {
        //        ListLoadIndicator.IsRunning = true;
        //        ListLoadIndicator.IsVisible = true;
        //        UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", selectedValue));
        //    }

        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        ShowNetworkAlert();
        //        list = await CategoriesViewModel.GetCategory("", selectedValue);
        //        BindingContext = list;
        //        if (Device.RuntimePlatform == Device.Android) { UserDialogs.Instance.HideLoading(); }
        //        else
        //        {
        //            ListLoadIndicator.IsRunning = false;
        //            ListLoadIndicator.IsVisible = false;
        //        }
        //    });
        //    #endregion

        //}

        //private void LangList_Change(object sender, EventArgs es)
        //{
        //    setMargin.Margin = new Thickness(0, 15, 0, 50);
        //    wbVid.IsVisible = false; // to hide webView

        //    ply.Source = "play_bottom.png";
        //    isPlaying = false;
        //    viewPlayer.Eval("stop()");
        //    var selectedValue = LangList.Items[LangList.SelectedIndex];
        //    ApplicationConstant.SelectedRegion = RegionChange(LangList.Items[LangList.SelectedIndex]);
        //    //ApplicationConstant.SelectedRegion = selectedValue;
        //    //var selectedValue = regionList.Items[regionList.SelectedIndex].Substring(0, 3);

        //    if (Device.RuntimePlatform == Device.Android)
        //    {
        //        Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading {0} Videos", selectedValue));
        //    }
        //    else
        //    {
        //        ListLoadIndicator.IsRunning = true;
        //        ListLoadIndicator.IsVisible = true;
        //        UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", selectedValue));
        //    }

        //    Device.BeginInvokeOnMainThread(async () =>
        //     {
        //         ShowNetworkAlert();
        //         list = await CategoriesViewModel.GetCategory("",selectedValue);
        //         BindingContext = list;
        //         if (Device.RuntimePlatform == Device.Android) { UserDialogs.Instance.HideLoading(); }
        //         else
        //         {
        //             ListLoadIndicator.IsRunning = false;
        //             ListLoadIndicator.IsVisible = false;
        //         }
        //         //if (!isOpeningFirstTime)
        //         //{
        //         //    UserDialogs.Instance.Toast("Region change only affect videos with region restrictions");
        //         //}
        //         //else
        //         //{
        //         //    isOpeningFirstTime = false;
        //         //}


        //     });

        //}
        #endregion
        private void GetDefaultUSVideo()
        {
            setMargin.Margin = new Thickness(0, 15, 0, 50);
            wbVid.IsVisible = false; // to hide webView
            ply.Source = "play_bottom.png";
            ytplayBtn.Source = "ytplaydis.png";
            isPlaying = false;
            viewPlayer.Eval("stop()");
            var selectedValue = "All";
            // var selectedLanguage = "en-GB";
            if (Device.RuntimePlatform == Device.Android)
            {
                // Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading Videos...", selectedValue));
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading(string.Format("Loading Videos...", Helpers.ApplicationConstant.SelectedRegion));
            }
            else
            {
                ListLoadIndicator.IsRunning = true;
                ListLoadIndicator.IsVisible = true;
                //UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", selectedValue));
                UserDialogs.Instance.Toast(string.Format("Loading {0} Videos", Helpers.ApplicationConstant.SelectedRegion));
            }
            Device.BeginInvokeOnMainThread(async () =>
             {
                 ShowNetworkAlert();
                 list = await CategoriesViewModel.GetCategory(Helpers.ApplicationConstant.SelectedRegion, "");
                 BindingContext = list;
                 LoadRegionList();
                 if (Device.RuntimePlatform == Device.Android) { UserDialogs.Instance.HideLoading(); }
                 else
                 {
                     ListLoadIndicator.IsRunning = false;
                     ListLoadIndicator.IsVisible = false;
                 }
             });

        }
        #endregion

        #region [Manage Carousel]
        public void SetSelected()
        {
            LanguageGridView.IsVisible = false;
            //LanguageListview.IsVisible = false;
            //LSTBox.IsVisible = false;
            previousCat = currentTab;

            Label[] arrheaderTab = { all, tab24, music, comedy, sports, gaming };
            BoxView[] arrIndicator = { FirstBox, secondBox, thirdBox, FourthBox, FifthBox, sixthBox };
            for (int i = 0; i < arrheaderTab.Length; i++)
            {
                if (Carousel.Position == i)
                {
                    arrheaderTab[i].TextColor = Color.FromHex(ApplicationConstant.ActiveTabTextColorHex);
                    //   arrheaderTab[i].FontAttributes = FontAttributes.Bold;
                    arrheaderTab[i].FontFamily = ApplicationConstant.TopNavigationFontFamilyName;
                    arrIndicator[i].Color = Color.FromHex("#F50000");

                    if (i >= 3)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            scrollBar.ScrollToAsync(gaming, ScrollToPosition.End, true);
                        });
                    }
                    if (i <= 2)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            scrollBar.ScrollToAsync(0, scrollBar.ScrollY, true);
                        });
                    }
                    CrasualPreviousPosition = Carousel.Position;
                    if (list != null && list.Count > i && list[i].CategoryVideolist.Count() == 0) { DisplayAlert("No Internet Connection", "Please Check Your Internet Connection or Change your Region Code", "OK"); }
                }
                else
                {
                    arrheaderTab[i].TextColor = Color.FromHex(ApplicationConstant.TabTextColorHex);
                    arrIndicator[i].Color = Color.Transparent;
                }

            }

            wbVid.IsVisible = false;
            if (_activeListOnPage != null && CurrentListViewOnPage(currentTab) != null)
                CurrentListViewOnPage(currentTab).SelectedItem = null;

            ply.Source = "play_bottom.png";
            ytplayBtn.Source = "ytplaydis.png";
            pre.Source = "previous_disable.png";
            isPlaying = false;
            videoUrl = "";
            viewPlayer.Eval("stop()");
            previous = null;
            Next = null;
            next.Source = "next_disable.png";
            playClick = false;
            pageload = true;
            stop = true;
            setMargin.Margin = new Thickness(0, 15, 0, 50);
            current = null;
        }
        public void Carousel_Change(object sender, EventArgs es)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    if (!isOrientationChange || (isPotriat && wbVid.IsVisible))
                    {
                        currentTab = (Carousel.Item as Categories).CategoryName;
                        SetSelected();
                        CrasualPreviousPosition = Carousel.Position;
                        if (isOrientationChange)
                            isOrientationChange = false;
                    }
                    else
                    {
                        isOrientationChange = false;
                        Carousel.Position = CrasualPreviousPosition;
                        //SetSelected();
                    }
                    break;
                default:
                    currentTab = (Carousel.Item as Categories).CategoryName;
                    SetSelected();
                    break;
            }
        }
        #endregion

        private void ShowNetworkAlert()
        {
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                DisplayAlert("No Internet Connection", "Please check your internet connection and try again.", "OK");
                //UserDialogs.Instance.Toast("No internet connection");
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            if (appLaunch)
            {
                isOrientationChange = false;
                appLaunch = false;
            }
            else
            {
                isOrientationChange = true;
            }
            base.OnSizeAllocated(width, height);

            isPotriat = !(width > height);
            if (previousWidth != width || previousHeight != height)
            {
                previousWidth = width;
                previousHeight = height;

                #region to set width 

                if (width > height)
                {
                    wbVid.Margin = new Thickness(0, -80, 0, -50);
                    wbVid.HeightRequest = height;
                    wbVid.WidthRequest = width;

                    //to hide player control in landscape mode
                    pnlPlayerControl.IsVisible = false;
                    boxBottomLine.IsVisible = false;
                    viewPlayer.HeightRequest = height;
                    viewPlayer.WidthRequest = width;

                    wbVid.IsVisible = true;
                    if (stop)
                        bindPlayerString(viewPlayer.Height.ToString(), viewPlayer.Width.ToString(), current != null ? current.YouTubeId : "", true);
                    else
                        isOrientationChange = false;


                    viewPlayer.Eval(string.Format("setPlayerSize(\"{0}\",\"{1}\")", viewPlayer.HeightRequest, viewPlayer.WidthRequest));
                }
                else
                {
                    if (stop)
                        wbVid.IsVisible = false;
                    else
                        isOrientationChange = false;

                    wbVid.Margin = new Thickness(0, 0, 0, 0);
                    wbVid.HeightRequest = (Height / 3);
                    wbVid.WidthRequest = width;
                    // to show player control again 
                    pnlPlayerControl.IsVisible = true;
                    boxBottomLine.IsVisible = true;
                    viewPlayer.HeightRequest = (Height / 3);
                    viewPlayer.WidthRequest = width;
                    viewPlayer.Eval(string.Format("setPlayerSize(\"{0}\",\"{1}\")", viewPlayer.HeightRequest, viewPlayer.WidthRequest));

                }
                #endregion

            }
        }
        /// <summary>
        /// Handles the OnPlayerStateChanged event of the VideoPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="VideoPlayerStateChangedEventArgs"/> instance containing the event data.</param>

        #region Store All views
        private void lstItm_BindingContextChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).ItemsSource != null && ((ListView)sender).ItemsSource.Cast<VideoModel>().FirstOrDefault() != null)
            {
                AddActiveListViewOnPage((ListView)sender, ((ListView)sender).ItemsSource.Cast<VideoModel>().FirstOrDefault().CategoryName);
            }
        }
        public ListView CurrentListViewOnPage(string category)
        {

            return _activeListOnPage.Where(x => x.Category.ToUpper() == category.ToUpper()).FirstOrDefault() != null ? _activeListOnPage.Where(x => x.Category.ToUpper() == category.ToUpper()).FirstOrDefault().CarousalListView : null;
        }
        public void AddActiveListViewOnPage(ListView listView, string category)
        {
            if (_activeListOnPage == null)
            {
                _activeListOnPage = new List<ActiveListViewOnPage>();
            }
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    if (_activeListOnPage.Where(x => x.CarousalListView.Id == listView.Id || x.Category == category).Count() < 1)
                    {
                        _activeListOnPage.Add(new ActiveListViewOnPage() { CarousalListView = listView, Category = category });
                    }
                    else
                    {
                        if (_activeListOnPage.Where(x => x.CarousalListView.Id == listView.Id && x.Category == category).Count() > 0)
                            return;

                        _activeListOnPage.RemoveAll(x => x.CarousalListView.Id == listView.Id || x.Category == category);
                        _activeListOnPage.Add(new ActiveListViewOnPage() { CarousalListView = listView, Category = category });
                    }
                    break;
                case Device.iOS:
                    if (_activeListOnPage.Where(x => x.CarousalListView.Id == listView.Id || x.Category == category).Count() < 1)
                    {
                        _activeListOnPage.Add(new ActiveListViewOnPage() { CarousalListView = listView, Category = category });
                    }
                    else
                    {
                        if (_activeListOnPage.Where(x => x.CarousalListView.Id == listView.Id && x.Category == category).Count() > 0)
                            return;

                        _activeListOnPage.RemoveAll(x => x.CarousalListView.Id == listView.Id || x.Category == category);
                        _activeListOnPage.Add(new ActiveListViewOnPage() { CarousalListView = listView, Category = category });
                        if (playClick)
                        {
                            var xC = current;
                            if (CurrentListViewOnPage(currentTab) != null)
                            {
                                CurrentListViewOnPage(currentTab).SelectedItem = null;
                                if (xC == null)
                                    CurrentListViewOnPage(currentTab).SelectedItem = list.Where(x => x.CategoryName.ToUpper() == currentTab.ToUpper()).FirstOrDefault().CategoryVideolist.FirstOrDefault();
                                else
                                    CurrentListViewOnPage(currentTab).SelectedItem = xC;
                                xC = null;
                            }
                            playClick = false;
                            pageload = false;
                        }
                    }

                    break;
                default:
                    break;
            }

        }
        #endregion
        private void lstItm_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        private void viewPlayer_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }
        private void viewPlayer_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("watch?"))
            {
                e.Cancel = true;
                Device.OpenUri(new Uri(e.Url));
            }
            else
                e.Cancel = true;
        }
        private void ytplayBtn_OnTapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(videoUrl))
                Device.OpenUri(new Uri(videoUrl));
        }
    }
    internal class ActiveListViewOnPage
    {
        public ListView CarousalListView;
        public string Category;
    }

    //public string _btnColor = "White";
    //public string ButtonColor
    //{
    //    get
    //    {
    //        return _btnColor;
    //    }
    //    set
    //    {
    //        if (_btnColor == value)
    //            return;
    //        _btnColor = value;
    //        OnPropertyChanged("ButtonColor");
    //    }
    //}
    //public bool _isSeen = true;
    //public bool ItemsSeen
    //{
    //    get
    //    {
    //        return _isSeen;
    //    }
    //    set
    //    {
    //        if (_isSeen == value)
    //            return;
    //        _isSeen = value;
    //        ButtonColor = _isSeen ? "White" : "#CCD1D1";
    //        OnPropertyChanged("ItemsSeen");
    //    }
    //}
}
