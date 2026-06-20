using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Setting
{
    public class JwtSetting
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public  int ExpiryInHours { get; set; }
    }
}
