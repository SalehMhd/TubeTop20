using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Top20Videos
{
    public class CategoryVideos
    {
        public ObservableCollection<Video> InnerVideoList { get; set; }
    }
}
