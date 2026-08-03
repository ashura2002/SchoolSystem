using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IImageStorage
    {
        Task<UploadedImage> UploadPictureAsync(
            Stream stream,
            string fileName,
            CancellationToken cancellationToken
            );

        Task DeleteAsync(
            string publicUrl,
            CancellationToken cancellationToken
            );
    }
}
