using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePracBasic.Models
{
    public class ImageFeedModel
    {
        public ImageFeedModel()
        {
        }

        public ImageFeedModel(string content, MediaInfoModel mediaInfo, int commentCount, int shareCount, int likeCount, string imageUrl)
        {
            Content = content;
            MediaInfo = mediaInfo;
            CommentCount = commentCount;
            ShareCount = shareCount;
            LikeCount = likeCount;
            ImageUrl = imageUrl;
        }

        public string Content { get; set; }
        [ForeignKey(typeof(MediaInfoModel))]
        [OneToOne]
        public MediaInfoModel MediaInfo { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
        public int LikeCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
