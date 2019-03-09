using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Videos.Helpers
{
    /// <summary>
    /// Enumerator 
    /// </summary>
    public class ApplicationConstant
    {
        public const string TopNavigationFontFamilyName = "HelveticaNeue-CondensedBold";// "Helvetica-Condensed-Thin";// "Helvetica-Condensed-Condensed";
        public const string ListingsFontFamilyName = "Helvetica-Medium-Condensed";
        public const string smalltextFontFamilyName = "Helvetica-light-Condensed"; //"Helvetica-Condensed-Thin"

        public static string SelectedRegion = "US";

       // public static string selectedlanguage { get; set; }

        public const string TabTextColorHex = "#979797";
        public const string ActiveTabTextColorHex = "#E0E0E0";

        /*
        public const string Api_VideoUrl = "http://dotnet4.peaceofmind.in/Top20Video_9359/api/video?categoryId={0}&regionCode={1}&LanguageCode={2}"; //"http://www.onepushpower.com/api/video?categoryId={0}&regionCode={1}";
        public const string Api_CategoryUrl = "http://dotnet4.peaceofmind.in/Top20Video_9359/api/category";//http://www.onepushpower.com/api/category";
        public const string Api_Language = "http://dotnet4.peaceofmind.in/Top20Video_9359/api/language";
        */

        //public const string Api_VideoUrl = "http://www.onepushpower.com/api/video?categoryId={0}&regionCode={1}&LanguageCode={2}"; 
        //public const string Api_CategoryUrl = "http://www.onepushpower.com/api/category";
        //public const string Api_Language = "http://www.onepushpower.com/api/language";

        //public const string Api_VideoUrl = "http://192.168.1.25:45455/api/video?categoryId={0}&regionCode={1}&LanguageCode={2}";
        //public const string Api_CategoryUrl = "http://192.168.1.25:45455/api/category";
        //public const string Api_Language = "http://192.168.1.25:45455/api/language";

        public const string Api_VideoUrl = "http://onepushpower.com/api/trendingrecent?regionCode={0}";
        public const string Api_CategoryUrl = "http://onepushpower.com/api/category";
        public const string Api_RegionUrl = "http://onepushpower.com/api/regionrecent";

        //public const string Api_VideoUrl = "http://www.onepushpower.com/api/video?categoryId={0}&regionCode={1}";
        //public const string Api_CategoryUrl = "http://www.onepushpower.com/api/category";

        #region Local Host
        //public const string Api_VideoUrl = "http://localhost:60808/api/video?categoryId={0}&regionCode={1}&LanguageCode={2}";
        //public const string Api_CategoryUrl = "http://localhost:60808/api/category";
        //public const string Api_Language = "http://localhost:60808/api/language";


        #endregion

        public const string YTSourceUrl = "http://www.onepushpower.com/yt.html";
    }
}

