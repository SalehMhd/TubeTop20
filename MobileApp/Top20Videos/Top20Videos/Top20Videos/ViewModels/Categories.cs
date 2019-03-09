using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Videos.Helpers;
using Top20Videos.Models;
using Top20Videos.Services;
using Xamarin.Forms;

namespace Top20Videos.ViewModels
{
    public class Categories
    {
        public string CategoryName { get; set; }
        public ObservableCollection<VideoModel> CategoryVideolist { get; set; }
    }
    public class CategoriesViewModel
    {
        
        private CategoryModel Response { get; set; }
        public ObservableCollection<Categories> catos { get; set; }

        #region DataBinding With View Throuhg API
        public static async Task<ObservableCollection<Categories>> GetCategory(string regionId = "",string LanguageCode = "")
        {
            try
            {
                await Task.Yield();

                var regioncode = "";
                if (regionId != "")
                {  
                    regioncode = regionId;
                }
                else
                {
                    regioncode = "All";
                }
                if(LanguageCode == "")
                {
                    LanguageCode = "Gn";
                }
                else
                {
                    LanguageCode  = Helpers.ApplicationConstant.SelectedRegion.ToString();
                }
                RegionList.List = await GetRegions();

                List<CategoryModel> response = await WebUtil.GetAsync<List<CategoryModel>>(ApplicationConstant.Api_CategoryUrl);
                var videoData = await GetVideos(0, regioncode,LanguageCode);
                ObservableCollection<Categories> data = new ObservableCollection<Categories>();

                if (response.Count() > 0)
                {
                    foreach (var item in response.OrderBy(x => x.DisplayOrder).ToList())
                    {
                        if(item.Name == "tab24")
                            continue;

                        Categories cat = new Categories();
                        cat.CategoryName = item.Name;   
                        List<VideoModel> list = (videoData.Where(x => x.CategoryName == item.Name)).ToList();
                        int count = 1;
                        foreach (var vItem in list)
                        {
                            vItem.PublishAgo = string.Format("#{0} - {1} - {2}", count, vItem.ViewDisplay, vItem.PublishAgo);
                            vItem.Title = vItem.Title.GetLimitedText(50).Trim();
                            count++;
                        }
                        cat.CategoryVideolist = new ObservableCollection<VideoModel>(list);
                        data.Add(cat);
                    }
                }
                if (data == null && data.Count == 0)
                {
                    return SetDefault();
                }
                return data;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return SetDefault();
            }
        }

        public static ObservableCollection<Categories> SetDefault()
        {
            ObservableCollection<Categories> Blankdata = new ObservableCollection<Categories>();
            Blankdata.Add(new Categories() { CategoryName = "All", CategoryVideolist = new ObservableCollection<VideoModel>() });
            Blankdata.Add(new Categories() { CategoryName = "Music", CategoryVideolist = new ObservableCollection<VideoModel>() });
            Blankdata.Add(new Categories() { CategoryName = "Comedy", CategoryVideolist = new ObservableCollection<VideoModel>() });
            Blankdata.Add(new Categories() { CategoryName = "Sports", CategoryVideolist = new ObservableCollection<VideoModel>() });
            Blankdata.Add(new Categories() { CategoryName = "Gaming", CategoryVideolist = new ObservableCollection<VideoModel>() });

            return Blankdata;
        }

        public static async Task<ObservableCollection<VideoModel>> GetVideos(int? CategoryId, string regioncode,string LanguageCode)
        {
            if (CategoryId == null)
            {
                CategoryId = 0;
            }
            if (string.IsNullOrEmpty(regioncode))
            {
                regioncode = "";
            }
            //List<VideoModel> response = await WebUtil.GetAsync<List<VideoModel>>(string.Format(ApplicationConstant.Api_VideoUrl, CategoryId, regioncode,LanguageCode));
            List<VideoModel> response = await WebUtil.GetAsync<List<VideoModel>>(string.Format(ApplicationConstant.Api_VideoUrl, regioncode));

            for (int i = 0; i < response.Count; i++)
            {
                response[i].Title.GetLimitedText(30);
            }
            ObservableCollection<VideoModel> data = new ObservableCollection<VideoModel>(response);
            return data;
        }
        #endregion

        public static async Task<List<Region>> GetRegions()
        {
            List<Region> response = await WebUtil.GetAsync<List<Region>>(ApplicationConstant.Api_RegionUrl);
            return response;
        }
    }
}
