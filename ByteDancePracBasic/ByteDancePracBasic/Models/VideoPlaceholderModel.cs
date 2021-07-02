using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePracBasic.Models
{
    public class VideoPlaceholderModel
    {
        public VideoPlaceholderModel(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;
        }

        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
