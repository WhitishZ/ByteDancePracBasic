using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePracBasic.Models
{
    public class VideoFeedModel
    {
        public VideoFeedModel(string @abstract, MediaInfoModel mediaInfo, int commentCount, int shareCount, int likeCount, VideoPlaceholderModel detailVideoLargeImage, string videoUrl, int videoDuration)
        {
            Abstract = @abstract;
            MediaInfo = mediaInfo;
            CommentCount = commentCount;
            ShareCount = shareCount;
            LikeCount = likeCount;
            DetailVideoLargeImage = detailVideoLargeImage;
            VideoUrl = videoUrl;
            VideoDuration = videoDuration;
        }
        public string Abstract { get; set; }
        public MediaInfoModel MediaInfo { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
        public int LikeCount { get; set; }
        public VideoPlaceholderModel DetailVideoLargeImage { get; set; }
        public string VideoUrl { get; set; }
        public int VideoDuration { get; set; }
    }
}
