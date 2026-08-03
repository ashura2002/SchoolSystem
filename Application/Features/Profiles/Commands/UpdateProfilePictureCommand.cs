using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public record UpdateProfilePictureCommand(
        Stream Stream,
        string FileName
        ) : IRequest<UploadedImage>;
}
