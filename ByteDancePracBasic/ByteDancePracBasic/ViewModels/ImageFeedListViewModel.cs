using ByteDancePracBasic.Data;
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
    public class ImageFeedListViewModel
    {
        static readonly JsonSerializerOptions SnakeCaseJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            // WriteIndented = true
        };
        public ObservableCollection<ImageFeedViewModel> FeedList { get; set; }

        /// <summary>
        /// Load the feeds in local database and append them to FeedList.
        /// If there is no feed data in local database, online data will be fetched and cached to local databse.
        /// Please note that the image file itself is automatically cached by Xamarin.Forms. Time limit is 24 hours by default.
        /// </summary>
        protected async void LoadFeedList()
        {
            FeedDatabase feedDB = await FeedDatabase.Instance;
            List<ImageFeedModel> imageFeedsFromLocalDB = await feedDB.GetImageFeedsAsync();
            List<MediaInfoModel> mediaInfoFromLocalDB = await feedDB.GetMediaInfosAsync();
            // TODO: Specify when to load the cache in local DB.
            // In this prototype, the local database is loaded as long as it is not empty.
            if (imageFeedsFromLocalDB.Count > 0)
            {
                // FIXME:
                // 实践要求文档中设计的Model类很不合理, 但又受限于Json格式无法自行修改.
                // FeedModel和MediaInfoModel均没有主键, 但在数据库中两者应为1-1/1-*关系, SQLite.NET作为一个最简单的数据库, 并不内置表的关系操作,
                // 因为无法使用外键(不考虑将MediaInfo序列化放在表中, 因为这使得数据库本身就失去了意义), 故这里采取的是最危险但也无奈的查询后手动赋值的方法.
                // 实际开发应用时, 应摒弃这种做法, 认真设计表的关联关系, 在读取FeedModel时自动获取级联的MediaInfo信息.
                // 请修改Data/FeedDatabase.cs, Models/FeedModel.cs和本方法, 将MediaInfo的相关不合理操作删除.
                // See https://forums.xamarin.com/discussion/4401/how-do-you-use-sqlite-net-with-custom-defined-types
                // and https://bitbucket.org/twincoders/sqlite-net-extensions/src/master/
                for (int i = 0; i < imageFeedsFromLocalDB.Count; ++i)
                {
                    ImageFeedModel feed = imageFeedsFromLocalDB[i];
                    feed.MediaInfo = mediaInfoFromLocalDB[i];   // NOT RECOMMENDED
                    FeedList.Add(new ImageFeedViewModel(feed));
                }
            }
            else
            {
                // No local feed data found. Try getting them online.
                var httpRequest = WebRequest.Create("https://getman.cn/mock/newcomer/feed");
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "GET";

                List<ImageFeedModel> feedFromNetwork = new List<ImageFeedModel>();

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
                            feedFromNetwork = JsonSerializer.Deserialize<List<ImageFeedModel>>(content, SnakeCaseJsonOptions);
                        }
                    }
                }
                catch (WebException)
                {
                    // TODO: Network disconnection error handling.
                    // Don't reference the app directly from the view model if possible, if flys in the face of the many benefits
                    // of a clear SOC between UI and viewmodel and is anti MVVM pattern.
                    // See https://forums.xamarin.com/discussion/146204/how-to-use-displayalert-from-a-viewmodel-without-additional-frameworks
                    // Application.Current.MainPage.DisplayAlert("Warning", "Internet connection failed.", "OK");   // NOT RECOMMENDED.
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
                    await feedDB.SaveMediaInfoAsync(feed.MediaInfo);
                    await feedDB.SaveImageFeedAsync(feed);   // Cache the newly fetched feed data to local database.
                    FeedList.Add(new ImageFeedViewModel(feed));
                }
            }
        }

        public ImageFeedListViewModel()
        {
            FeedList = new ObservableCollection<ImageFeedViewModel>();
            FeedList.Add(new ImageFeedViewModel(new ImageFeedModel("Feed message template.", new MediaInfoModel("default_avatar", "Author", "Verification message.", true), 0, 0, 0, "image_not_found")));
            LoadFeedList();
        }
    }
}
