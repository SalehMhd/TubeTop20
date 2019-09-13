﻿using System;
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

        public int _VMPlayState;

        public int VMPlayState {
            set
            {
                _VMPlayState = value;
                OnPropertyChanged("VMPlayState");
            }
            get => _VMPlayState;
        }

        public string _CurrentVideoYouTubeId;

        public string CurrentVideoYouTubeId
        {
            set
            {
                _CurrentVideoYouTubeId = value;
                OnPropertyChanged("CurrentVideoYouTubeId");
            }
            get => _CurrentVideoYouTubeId;
        }
    }
}
