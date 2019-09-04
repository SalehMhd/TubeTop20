using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Top20Videos.Annotations;

namespace Top20Videos
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CategoryVideos> _CategoriesVideoList;

        public ObservableCollection<CategoryVideos> CategoriesVideoList {
            set
            {
                _CategoriesVideoList = value;
                OnPropertyChanged("CategoriesVideoList");
            }
            get => _CategoriesVideoList;
        }

        public MainPageViewModel()
        {
            _CategoriesVideoList = new ObservableCollection<CategoryVideos>
            {
                new CategoryVideos(),
                new CategoryVideos(),
                new CategoryVideos(),
                new CategoryVideos(),
                new CategoryVideos()
            };
            _CategoriesVideoList[0].InnerVideoList = new ObservableCollection<Video>();
            _CategoriesVideoList[1].InnerVideoList = new ObservableCollection<Video>();
            _CategoriesVideoList[2].InnerVideoList = new ObservableCollection<Video>();
            _CategoriesVideoList[3].InnerVideoList = new ObservableCollection<Video>();
            _CategoriesVideoList[4].InnerVideoList = new ObservableCollection<Video>();

        }
    }
}
