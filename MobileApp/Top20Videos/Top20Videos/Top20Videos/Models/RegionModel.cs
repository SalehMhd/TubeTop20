//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Top20Videos.Annotations;

namespace Top20Videos.Models
{
    //    //public class RegionModel
    //    //{
    //    //    [JsonProperty("EncryptedID")]
    //    //    public string EncryptedID { get; set; }

    //    //    [JsonProperty("ID")]
    //    //    public int ID { get; set; }
    //    //    [JsonProperty("Name")]
    //    //    public string Name { get; set; }
    //    //    [JsonProperty("Code")]
    //    //    public string Code { get; set; }
    //    //    [JsonProperty("DisplayOrder")]
    //    //    public int DisplayOrder { get; set; }
    //    //}
    public class Region : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Region()
        {

        }

        public string _TextColor = "#9E9E9E";

        public string TextColor
        {
            get { return _TextColor; }
            set
            {
                if (_TextColor == value) return;
                _TextColor = value;
                NotifyPropertyChanged("TextColor");
            }
        }


        public string _isSelected = "#28292D";

        public string isSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                NotifyPropertyChanged("isSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class RegionList
    {
        public static List<Region> List;

        /*
        static RegionList()
        {
            List = new List<Region>();
            List.Add(new Region() {ID = 1, Name = "Algeria", Code = "DZ"});
            List.Add(new Region() {ID = 2, Name = "Argentina", Code = "AR"});
            List.Add(new Region() {ID = 3, Name = "Australia", Code = "AU"});
            List.Add(new Region() {ID = 4, Name = "Austria", Code = "AT"});
            List.Add(new Region() {ID = 5, Name = "Azerbaijan", Code = "AZ"});
            List.Add(new Region() {ID = 6, Name = "Bahrain", Code = "BH"});
            List.Add(new Region() {ID = 7, Name = "Belarus", Code = "BY"});
            List.Add(new Region() {ID = 8, Name = "Belgium", Code = "BE"});
            List.Add(new Region() {ID = 9, Name = "Bosnia and Herzegovina", Code = "BA"});
            List.Add(new Region() {ID = 10, Name = "Brazil", Code = "BR"});
            List.Add(new Region() {ID = 11, Name = "Bulgaria", Code = "BG"});
            List.Add(new Region() {ID = 12, Name = "Canada", Code = "CA"});
            List.Add(new Region() {ID = 13, Name = "Chile", Code = "CL"});
            List.Add(new Region() {ID = 14, Name = "Colombia", Code = "CO"});
            List.Add(new Region() {ID = 15, Name = "Croatia", Code = "HR"});
            List.Add(new Region() {ID = 16, Name = "Czech Republic", Code = "CZ"});
            List.Add(new Region() {ID = 17, Name = "Denmark", Code = "DK"});
            List.Add(new Region() {ID = 18, Name = "Egypt", Code = "EG"});
            List.Add(new Region() {ID = 19, Name = "Estonia", Code = "EE"});
            List.Add(new Region() {ID = 20, Name = "Finland", Code = "FI"});
            List.Add(new Region() {ID = 21, Name = "France", Code = "FR"});
            List.Add(new Region() {ID = 22, Name = "Georgia", Code = "GE"});
            List.Add(new Region() {ID = 23, Name = "Germany", Code = "DE"});
            List.Add(new Region() {ID = 24, Name = "Ghana", Code = "GH"});
            List.Add(new Region() {ID = 25, Name = "Greece", Code = "GR"});
            List.Add(new Region() {ID = 26, Name = "Hong Kong", Code = "HK"});
            List.Add(new Region() {ID = 27, Name = "Hungary", Code = "HU"});
            List.Add(new Region() {ID = 28, Name = "Iceland", Code = "IS"});
            List.Add(new Region() {ID = 29, Name = "India", Code = "IN"});
            List.Add(new Region() {ID = 30, Name = "Indonesia", Code = "ID"});
            List.Add(new Region() {ID = 31, Name = "Iraq", Code = "IQ"});
            List.Add(new Region() {ID = 32, Name = "Ireland", Code = "IE"});
            List.Add(new Region() {ID = 33, Name = "Israel", Code = "IL"});
            List.Add(new Region() {ID = 34, Name = "Italy", Code = "IT"});
            List.Add(new Region() {ID = 35, Name = "Jamaica", Code = "JM"});
            List.Add(new Region() {ID = 36, Name = "Japan", Code = "JP"});
            List.Add(new Region() {ID = 37, Name = "Jordan", Code = "JO"});
            List.Add(new Region() {ID = 38, Name = "Kazakhstan", Code = "KZ"});
            List.Add(new Region() {ID = 39, Name = "Kenya", Code = "KE"});
            List.Add(new Region() {ID = 40, Name = "Kuwait", Code = "KW"});
            List.Add(new Region() {ID = 41, Name = "Latvia", Code = "LV"});
            List.Add(new Region() {ID = 42, Name = "Lebanon", Code = "LB"});
            List.Add(new Region() {ID = 43, Name = "Libya", Code = "LY"});
            List.Add(new Region() {ID = 44, Name = "Lithuania", Code = "LT"});
            List.Add(new Region() {ID = 45, Name = "Luxembourg", Code = "LU"});
            List.Add(new Region() {ID = 46, Name = "Macedonia", Code = "MK"});
            List.Add(new Region() {ID = 47, Name = "Malaysia", Code = "MY"});
            List.Add(new Region() {ID = 48, Name = "Mexico", Code = "MX"});
            List.Add(new Region() {ID = 49, Name = "Montenegro", Code = "ME"});
            List.Add(new Region() {ID = 50, Name = "Morocco", Code = "MA"});
            List.Add(new Region() {ID = 51, Name = "Nepal", Code = "NP"});
            List.Add(new Region() {ID = 52, Name = "Netherlands", Code = "NL"});
            List.Add(new Region() {ID = 53, Name = "New Zealand", Code = "NZ"});
            List.Add(new Region() {ID = 54, Name = "Nigeria", Code = "NG"});
            List.Add(new Region() {ID = 55, Name = "Norway", Code = "NO"});
            List.Add(new Region() {ID = 56, Name = "Oman", Code = "OM"});
            List.Add(new Region() {ID = 57, Name = "Pakistan", Code = "PK"});
            List.Add(new Region() {ID = 58, Name = "Peru", Code = "PE"});
            List.Add(new Region() {ID = 59, Name = "Philippines", Code = "PH"});
            List.Add(new Region() {ID = 60, Name = "Poland", Code = "PL"});
            List.Add(new Region() {ID = 61, Name = "Portugal", Code = "PT"});
            List.Add(new Region() {ID = 62, Name = "Puerto Rico", Code = "PR"});
            List.Add(new Region() {ID = 63, Name = "Qatar", Code = "QA"});
            List.Add(new Region() {ID = 64, Name = "Romania", Code = "RO"});
            List.Add(new Region() {ID = 65, Name = "Russia", Code = "RU"});
            List.Add(new Region() {ID = 66, Name = "Saudi Arabia", Code = "SA"});
            List.Add(new Region() {ID = 67, Name = "Senegal", Code = "SN"});
            List.Add(new Region() {ID = 68, Name = "Serbia", Code = "RS"});
            List.Add(new Region() {ID = 69, Name = "Singapore", Code = "SG"});
            List.Add(new Region() {ID = 70, Name = "Slovakia", Code = "SK"});
            List.Add(new Region() {ID = 71, Name = "Slovenia", Code = "SI"});
            List.Add(new Region() {ID = 72, Name = "South Africa", Code = "ZA"});
            List.Add(new Region() {ID = 73, Name = "South Korea", Code = "KR"});
            List.Add(new Region() {ID = 74, Name = "Spain", Code = "ES"});
            List.Add(new Region() {ID = 75, Name = "Sri Lanka", Code = "LK"});
            List.Add(new Region() {ID = 76, Name = "Sweden", Code = "SE"});
            List.Add(new Region() {ID = 77, Name = "Switzerland", Code = "CH"});
            List.Add(new Region() {ID = 78, Name = "Taiwan", Code = "TW"});
            List.Add(new Region() {ID = 79, Name = "Tanzania", Code = "TZ"});
            List.Add(new Region() {ID = 80, Name = "Thailand", Code = "TH"});
            List.Add(new Region() {ID = 81, Name = "Tunisia", Code = "TN"});
            List.Add(new Region() {ID = 82, Name = "Turkey", Code = "TR"});
            List.Add(new Region() {ID = 83, Name = "Uganda", Code = "UG"});
            List.Add(new Region() {ID = 84, Name = "Ukraine", Code = "UA"});
            List.Add(new Region() {ID = 85, Name = "United Arab Emirates", Code = "AE"});
            List.Add(new Region() {ID = 86, Name = "United Kingdom", Code = "GB"});
            List.Add(new Region() {ID = 87, Name = "United States", Code = "US"});
            List.Add(new Region() {ID = 88, Name = "Vietnam", Code = "VN"});
            List.Add(new Region() {ID = 89, Name = "Yemen", Code = "YE"});
            List.Add(new Region() {ID = 90, Name = "Zimbabwe", Code = "ZW"});
        }
        */

        public static List<Region> GetAll()
        {
            return List;
        }

    }
}
