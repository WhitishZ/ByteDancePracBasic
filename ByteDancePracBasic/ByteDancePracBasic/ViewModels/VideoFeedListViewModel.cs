using ByteDancePracBasic.Models;
using ByteDancePracBasic.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ByteDancePracBasic.ViewModels
{
    public class VideoFeedListViewModel
    {
        static readonly JsonSerializerOptions SnakeCaseJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            // WriteIndented = true
        };
        public ObservableCollection<VideoFeedViewModel> FeedList { get; set; }

        public VideoFeedListViewModel()
        {
            FeedList = new ObservableCollection<VideoFeedViewModel>();

            var httpRequest = WebRequest.Create("https://getman.cn/mock/newcomer/feedv2");
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";

            List<VideoFeedModel> feedFromNetwork = new List<VideoFeedModel>();

            HttpWebResponse httpResponse = null;
            try
            {
                httpRequest.Timeout = 3000; // Time out limit is set to 3 seconds.
                httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    // TODO: HTTP GET error handling.
                }
                else
                {
                    using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string content = reader.ReadToEnd();
                        // TODO: FIXME: 
                        feedFromNetwork = JsonSerializer.Deserialize<List<VideoFeedModel>>(content, SnakeCaseJsonOptions);
                    }
                }
            }
            catch (WebException)
            {
                // TODO: Network disconnection error handling.
            }
            catch (JsonException)
            {
                // TODO: JSON deserialization error handling.
            }
            finally
            {
                httpResponse?.Close();
            }
            foreach (var feed in feedFromNetwork)
            {
                VideoFeedViewModel feedViewModel = new VideoFeedViewModel(feed);
                FeedList.Add(feedViewModel);
            }
        }
    }
}
