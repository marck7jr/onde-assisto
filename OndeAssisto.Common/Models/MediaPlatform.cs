using System;

namespace OndeAssisto.Common.Models
{
    public class MediaPlatform
    {
        public MediaPlatform()
        {
            Media = new Media();
            Platform = new Platform();
        }

        public MediaPlatform(Media media, Platform platform)
        {
            Media = media;
            Platform = platform;
        }

        public Guid MediaGuid { get; set; }
        public Media Media { get; set; }

        public Guid PlatformGuid { get; set; }
        public Platform Platform { get; set; }
    }
}
