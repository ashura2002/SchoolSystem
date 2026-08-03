using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Setting
{
    public class CloudinarySettings
    {
        public const string SectionName = "Cloudinary";
        public required string CloudName { get; init; }
        public required string ApiSecret { get; init; }
        public required string ApiKey { get; init; }
        public required string Folder { get; init; }
    }
}
