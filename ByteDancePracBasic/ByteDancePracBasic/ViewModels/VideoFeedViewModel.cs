using ByteDancePracBasic.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace ByteDancePracBasic.ViewModels
{
    public class VideoFeedViewModel : INotifyPropertyChanged
    {
        public VideoFeedModel Feed { get; set; }
        public bool ShowVideoPlaceholder { get; set; }
        public ICommand PlayVideo { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void LoadStreamVideoUrl()
        {
            using (var client = new HttpClient())
            {
                var videoPageContent = client.GetStringAsync(Feed.VideoUrl).Result;
                var videoParameters = HttpUtility.ParseQueryString(videoPageContent);
                var encodedStreamsDelimited = WebUtility.HtmlDecode(videoParameters["player_response"]);
                JObject jObject = JObject.Parse(encodedStreamsDelimited);
                string url = (string)jObject["streamingData"]["formats"][0]["url"];
                Feed.VideoUrl = url;
            }
        }
        public VideoFeedViewModel(VideoFeedModel feed)
        {
            Feed = feed;
            ShowVideoPlaceholder = true;
            PlayVideo = new Command(() =>
            {
                ShowVideoPlaceholder = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowVideoPlaceholder"));
            });
            Feed.VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4";
            // The url of given video is dead. Use another sample video instead.
            // LoadStreamVideoUrl();
        }
    }
}
