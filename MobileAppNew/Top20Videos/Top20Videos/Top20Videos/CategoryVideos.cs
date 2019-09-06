using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Top20Videos.Annotations;

namespace Top20Videos
{
    public class CategoryVideos : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<Video> InnerVideoList { get; set; }


        public Action<string> _RegisterAction;

        public Action<string> RegisterAction
        {
            set
            {
                _RegisterAction = value;
                OnPropertyChanged("RegisterAction");
            }
            get => _RegisterAction;
        }


        public bool _IsVisible;

        public bool IsVisible
        {
            set
            {
                _IsVisible = value;
                OnPropertyChanged("IsVisible");
            }
            get => _IsVisible;
        }

        

    }
}
