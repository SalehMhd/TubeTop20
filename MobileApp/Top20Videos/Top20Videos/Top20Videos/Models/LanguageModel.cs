using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Top20Videos.Models
{
    //public class LanguageModel
    //{
    //    [JsonProperty("EncryptedID")]
    //    public string EncryptedID { get; set; }

    //    [JsonProperty("ID")]
    //    public int ID { get; set; }
    //    [JsonProperty("Name")]
    //    public string Name { get; set; }
    //    [JsonProperty("Code")]
    //    public string Code { get; set; }
    //    [JsonProperty("DisplayOrder")]
    //    public int DisplayOrder { get; set; }
    //}
    public class Language : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string RelevanceLanguageName { get; set; }
        public string RelevanceLanguage { get; set; }

        //public string _StackPadding = "0,15,0,0";
        //public string StackPadding
        //{
        //    get { return _StackPadding; }
        //    set
        //    {
        //        if (_StackPadding == value) return;
        //        _StackPadding = value;
        //        NotifyPropertyChanged("StackPadding");
        //    }
        //}
        public string _TextColor = "#9E9E9E";
        public string TextColor { get
            { return _TextColor; }
            set
            {
                if (_TextColor == value) return;
                _TextColor = value;
                NotifyPropertyChanged("TextColor");
            }
        }


        public string _isSelected = "#28292D";
        public string isSelected { get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                NotifyPropertyChanged("isSelected");
            }
        }

        public Language()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class LanguageList
    {
        private static List<Language> List;

        static LanguageList()
        {
            List = new List<Language>();
            List.Add(new Language() { ID = "0", RelevanceLanguageName = "GLOBAL", RelevanceLanguage = "Gn" });
            List.Add(new Language() { ID = "1",  RelevanceLanguageName = "ENGLISH",  RelevanceLanguage = "en" });
            List.Add(new Language() { ID = "2", RelevanceLanguageName = "ARABIC",  RelevanceLanguage = "ar"});
            List.Add(new Language() { ID = "3", RelevanceLanguageName = "FRENCH",    RelevanceLanguage = "fr"});
            List.Add(new Language() { ID = "4", RelevanceLanguageName = "SPANISH", RelevanceLanguage = "es"});
           
        }

    public static List<Language> GetAll()
        {
            return List;
        }

    }

}
