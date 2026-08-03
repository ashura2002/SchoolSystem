using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public sealed record UploadedImage(string Url, string PublicId);
}
