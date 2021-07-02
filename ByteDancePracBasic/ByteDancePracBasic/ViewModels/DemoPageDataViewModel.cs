using ByteDancePracBasic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePracBasic.ViewModels
{
    class DemoPageDataViewModel
    {
        public DemoPageDataViewModel(Type type, string title, string description)
        {
            Type = type;
            Title = title;
            Description = description;
        }

        public Type Type { private set; get; }
        public string Title { private set; get; }
        public string Description { private set; get; }

        static DemoPageDataViewModel()
        {
            All = new List<DemoPageDataViewModel>
            {
                new DemoPageDataViewModel(typeof(ImageFeedPage), "Practice #2: Feed列表原型.", "Feed cell prototype."),
                new DemoPageDataViewModel(typeof(ImageFeedPageWithAnimation),
                    "Practice #3-5: 点赞动画、联网拉取与本地数据库缓存.", "Animation, list fetching and local database caching."),
                new DemoPageDataViewModel(typeof(VideoFeedPage), "Practice #6: 支持视频播放.", "Supporting vedio in feed messages.")
            };
        }
        public static IList<DemoPageDataViewModel> All { private set; get; }
    }
}
