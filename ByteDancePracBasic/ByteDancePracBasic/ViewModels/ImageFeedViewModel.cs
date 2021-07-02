using ByteDancePracBasic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ByteDancePracBasic.ViewModels
{
    public class ImageFeedViewModel : INotifyPropertyChanged
    {
        public ImageFeedModel Feed { get; set; }

        public ImageFeedViewModel(ImageFeedModel feed)
        {
            Feed = feed;
            LikedByCurrentUser = false;
            LikeThisFeed = new Command(() =>
            {
                if (LikedByCurrentUser == true) return;
                ++Feed.LikeCount;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Feed"));
                LikedByCurrentUser = true;
            });
        }

        public ICommand LikeThisFeed { get; private set; }

        /// <summary>
        /// Whether the current user has hit Like button for this feed.
        /// </summary>
        public bool LikedByCurrentUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
