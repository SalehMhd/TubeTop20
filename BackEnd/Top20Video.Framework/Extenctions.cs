using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace Top20Video.Framework
{
    public static class Extenctions
    {
        /// <summary>
        /// to check string is parsable to decimal or not
        /// </summary>
        /// <param name="value">string numeric value</param>
        /// <returns>returns bool</returns>
        public static bool IsDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// to check string is parsable to integer or not
        /// </summary>
        /// <param name="value">string number value</param>
        /// <returns>returns bool</returns>
        public static bool IsNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// to check object is parsable to DateTime or not
        /// </summary>
        /// <param name="value">object date time value</param>
        /// <returns>returns bool</returns>     
        public static bool isDateTime(this object value)
        {
            if (value == null) { return false; }
            return (value is DateTime);
        }

        /// <summary>
        /// to check object is parsable to Date or not
        /// </summary>
        /// <param name="value">object date value</param>
        /// <returns>returns bool</returns>
        public static bool isDate(this object value)
        {
            if (value == null) { return false; }
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// to validate Loopcard number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidLoopCardNo(this string value)
        {
            return value.IsNumber() ? (value.Trim().Length == 16) : false;
            //return value.IsNumber();
        }

        /// <summary>
        /// to validate enctypted value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEncryptedID(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return value.ToDecrypt().IsNumber();
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// to encrypt string into MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnctyptedPassword(this string value)
        {
            Security objSecurity = new Security();
            return objSecurity.MD5Hash(value);
        }

        /// <summary>
        /// to encrypt value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnctypt(this string value)
        {
            // Commented on 29-03-2016
            //Security objSecutiry = new Security();
            //return objSecutiry.Encrypt(value);

            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            //return System.Convert.ToBase64String(plainTextBytes);
            return value;
        }

        /// <summary>
        /// to decrypt enctypted value
        /// </summary>
        /// <param name="value">enctypted</param>
        /// <returns></returns>
        public static string ToDecrypt(this string value)
        {
            // Commented on 29-03-2016
            //Security objSecutiry = new Security();
            //return objSecutiry.Decrypt(value);
            //var base64EncodedBytes = System.Convert.FromBase64String(value);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return value;
        }

        /// <summary>
        /// SR number format
        /// </summary>
        /// <param name="value">enctypted</param>
        /// <returns></returns>
        public static string ToSerialnumber(this long rfqId)
        {
            return rfqId.ToString("#000000");
        }
        /// <summary>
        /// to encrypt intger value
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>

        public static string EncodeInt(int plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText.ToString());
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// to decrypt intger value
        /// </summary>
        /// <param name="value">enctypted</param>
        /// <returns></returns>
        public static int DecodeInt(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes).ToString().ToInt();
        }
        /// <summary>
        /// Convert value into integer
        /// </summary>
        /// <returns>value in integer</returns>
        public static int ToInt(this string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// calculate number of records to be skip 
        /// </summary>
        /// <param name="value">current page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns>number of records to be skip</returns>
        public static int TableSkipRecord(this int value, int pageSize)
        {
            int skip = (value - 1) * pageSize;
            return skip;
        }

        /// <summary>
        /// to convert date to (dd hh mm) string format, ex. 03d 12h 22m
        /// </summary>
        /// <param name="value">expiration date</param>
        /// <returns>string date, ex. 03d 12h 22m </returns>
        public static string ToExpireTime(this DateTime value)
        {
            string days = (value - DateTime.Now).TotalDays.ToString("00d");
            string hours = (value - DateTime.Now).Hours.ToString("00h");
            string minutes = (value - DateTime.Now).Minutes.ToString("00m");
            string expireTime = value.ToString("dd") + "d " +
                value.ToString("hh") + "h " +
                value.ToString("mm") + "m ";
            expireTime = days + " " + hours.Replace("-", "") + " " + minutes.Replace("-", "");
            return expireTime;
        }

        /// <summary>
        /// to convert video name to video thumbnail name
        /// </summary>
        /// <param name="value">video file name</param>
        /// <returns></returns>
        public static string ToImageName(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            return value.Replace("mp4", "jpg");
        }

        /// <summary>
        /// to encode url
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value, Encoding.ASCII);
        }

        /// <summary>
        /// to decode encoded url
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlDecode(this string value)
        {
            return HttpUtility.UrlDecode(value, Encoding.ASCII);
        }

        /// <summary>
        /// to remove special charactar from string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlFilter(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            value = value.Replace(" ", "-");
            value = value.Replace("&", "and");
            value = Regex.Replace(value, "[^0-9a-zA-Z,-]+", "");
            return value.ToLower();
        }

        /// <summary>
        /// to replace special charactar to string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromUrlFilter(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            value = value.Replace("-", " ");
            value = value.Replace("&", "and");
            return value.ToLower();
        }

        /// <summary>
        /// to make video link
        /// </summary>
        /// <param name="title">video title</param>
        /// <param name="EncryptedId">video encrypted id</param>
        /// <returns>video url</returns>
        public static string ToMakeVideoLink(this string youTubeId)
        {
            //return "www.youtube.com/watch?v=" + youTubeId;
            // return string.Format("https://www.youtube.com/embed/{0}?rel=0&autoplay=1", youTubeId);
            return string.Format("https://www.youtube.com/embed/{0}?rel=0&autoplay=1&showinfo=0&controls=0", youTubeId);


        }

        /// <summary>
        /// to make the different thumbnail urls
        /// </summary>
        /// <param name="thumbnailUrl"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToMakeThumbnail(this string thumbnailUrl, ThumbnailType size)
        {
            switch (size)
            {
                case ThumbnailType.Medium:
                    thumbnailUrl = thumbnailUrl.Replace("default", "mqdefault");
                    break;
                case ThumbnailType.High:
                    thumbnailUrl = thumbnailUrl.Replace("default", "hqdefault");
                    break;
                case ThumbnailType.HD:
                    thumbnailUrl = thumbnailUrl.Replace("default", "maxresdefault");
                    break;
                case ThumbnailType.Standard:
                    thumbnailUrl = thumbnailUrl.Replace("default", "sddefault");
                    break;
            }
            return thumbnailUrl;
        }

        /// <summary>
        /// to convert display time of publishe like 4 hours ago.
        /// </summary>
        /// <param name="publishedAt"></param>
        /// <returns></returns>
        public static string ToTimeDisplay(this DateTime publishedAt)
        {
            string Ago = string.Empty;
           if ((utilityHelper.CurrentDateTime - publishedAt).TotalDays >= 1)
            {
                Ago = string.Format("{0} day ago", (utilityHelper.CurrentDateTime - publishedAt).TotalDays.ToString("0"));
            }
            else if ((utilityHelper.CurrentDateTime - publishedAt).TotalHours >= 1)
            {
                Ago = string.Format("{0} hours ago", (utilityHelper.CurrentDateTime - publishedAt).TotalHours.ToString("0"));
            }
            else if ((utilityHelper.CurrentDateTime - publishedAt).TotalMinutes >= 1)
            {
                Ago = string.Format("{0} minutes ago", (utilityHelper.CurrentDateTime - publishedAt).TotalMinutes.ToString("0"));
            }
            else
            {
                Ago = "few seconds ago";
            }

            return Ago;
        }

        /// <summary>
        /// to convert views into display format
        /// </summary>
        /// <param name="viewCount">No of views count</param>
        /// <returns>views count strin like 5M views</returns>
        public static string ToViewsCount(this long viewCount)
        {
            string views = string.Empty;
            if (viewCount >= 1000000)
            {
                views = string.Format("{0}M views", (viewCount / 1000000).ToString());
            }
            else if (viewCount >= 1000)
            {
                views = string.Format("{0}K views", (viewCount / 1000).ToString());
            }
            else
            {
                views = string.Format("{0} views", viewCount.ToString());
            }
            return views;
        }

        /// <summary>
        /// convert seconds to hours minutes and seconds
        /// </summary>
        /// <param name="duration">total duration in seconds</param>
        /// <returns>duration in minutes</returns>
        public static decimal ToDuration(this double totalSeconds)
        {
            int minutes = 0;
            int seconds = 0;
            decimal duration = 0;

            seconds = Convert.ToInt32(totalSeconds % 60);


            string time = "";
            if (totalSeconds >= 60)
            {
                minutes = Convert.ToInt32(totalSeconds / 60);
                //time = Convert.ToInt32(totalSeconds / 60).ToString("00.");
                //time += Convert.ToInt32(totalSeconds % 60).ToString();
            }
            else
            {
                //time = Convert.ToInt32(totalSeconds % 60).ToString("00.00");
            }

            duration = Convert.ToDecimal(minutes + (seconds * 0.01));

            return duration;
        }

        /// <summary>
        /// To check data file extension like csv,xls
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsDataFileExtension(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "csv" || fileExtension == "xls" || fileExtension == "xlsx");
        }

        /// <summary>
        /// To check csv file extension 
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsCsvFile(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "csv");
        }

        /// <summary>
        /// To check excel file extension xls,xlsx
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsXlsFile(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "xls" || fileExtension == "xlsx");
        }

        /// <summary>
        /// to check valid image file extension 
        /// 
        /// </summary>
        /// <param name="fileName">file name with extension</param>
        /// <returns></returns>
        public static bool IsValidImage(this string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                return true;
            else
                return false;
        }

        /// <summary>
        /// To get limited text
        /// </summary>
        /// <param name="value">text</param>
        /// <returns>string</returns>
        public static string GetLimitedText(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) { return ""; }

            if (value.Length > length)
            {
                string tmp = value.Substring(0, length);
                return tmp.Contains(' ') ? tmp.Substring(0, tmp.LastIndexOf(' ')) : tmp;
            }
            else
            {
                return value;
            }

        }

        /// <summary>
        /// to get model error message into a signle string with line break
        /// </summary>
        /// <param name="modelState">ModelStateDictionary</param>
        /// <returns></returns>
        public static string GetErrorMessage(this ModelStateDictionary modelState)
        {
            string errMessage = "";
            foreach (var key in modelState.Keys)
            {
                foreach (var error in modelState[key].Errors)
                {
                    errMessage += error.ErrorMessage + "<br/>";
                }
            }
            return errMessage;
        }

        //public static string GetDuration()
        //{
        //    Dictionary<string, string> regexMap = new Dictionary<string, string>();
        //    string regex2two = "(?<=[^\\d])(\\d)(?=[^\\d])";
        //    string two = "0$1";

        //    regexMap.Add("PT(\\d\\d)S", "00:$1");
        //    regexMap.Add("PT(\\d\\d)M", "$1:00");
        //    regexMap.Add("PT(\\d\\d)H", "$1:00:00");
        //    regexMap.Add("PT(\\d\\d)M(\\d\\d)S", "$1:$2");
        //    regexMap.Add("PT(\\d\\d)H(\\d\\d)S", "$1:00:$2");
        //    regexMap.Add("PT(\\d\\d)H(\\d\\d)M", "$1:$2:00");
        //    regexMap.Add("PT(\\d\\d)H(\\d\\d)M(\\d\\d)S", "$1:$2:$3");

        //    string[] dates = { "PT1S", "PT1M", "PT1H", "PT1M1S", "PT1H1S", "PT1H1M", "PT1H1M1S", "PT10H1M13S", "PT10H1S", "PT1M11S" };

        //    foreach (string date in dates)
        //    {
        //        string d = date.Replace(regex2two, two);
        //        string regex = getRegex(d);
        //        if (regex == null)
        //        {
        //          //  System.out.println(d + ": invalid");
        //            continue;
        //        }
        //        string newDate = d.Replace(regex, regexMap.Where(x=> x.Key== regex).FirstOrDefault().Value);
        //        System.out.println(date + " : " + newDate);
        //    }
        //}

        //private static string getRegex(string date)
        //{
        //    for (string r : regexMap.keySet())
        //        if (Pattern.matches(r, date))
        //            return r;
        //    return null;
        //}

        /// <summary>
        /// to convert youtube duration string into duration display format
        /// </summary>
        /// <param name="selectedtime"></param>
        /// <returns></returns>
        public static string ToDurationDisplayFormat(this string selectedtime)
        {
            var durationSpan = selectedtime != null
                ? XmlConvert.ToTimeSpan(selectedtime)
                : (TimeSpan?)null;
            if (durationSpan != null)
            {
                if (durationSpan.Value.Hours > 0)
                {
                    return
                        $"{Convert.ToInt16(durationSpan.Value.Hours).ToString("00")}:{Convert.ToInt16(durationSpan.Value.Minutes).ToString("00")}:{Convert.ToInt16(durationSpan.Value.Seconds).ToString("00")}";
                }
                else
                {
                    return
                        $"{Convert.ToInt16(durationSpan.Value.Minutes).ToString("00")}:{Convert.ToInt16(durationSpan.Value.Seconds).ToString("00")}";
                }
            }
            else
            {
                return "0:0";
            }
        }

    }
}
