using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePracBasic.Models
{
    public class MediaInfoModel
    {
        public MediaInfoModel()
        {
        }

        public MediaInfoModel(string avatarUrl, string name, string verifiedContent, bool userVerified)
        {
            AvatarUrl = avatarUrl;
            Name = name;
            VerifiedContent = verifiedContent;
            UserVerified = userVerified;
        }

        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public string VerifiedContent { get; set; }
        public bool UserVerified { get; set; }
    }
}
