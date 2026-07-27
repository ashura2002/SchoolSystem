using Application.Interfaces.CustomeMediatR.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.CustomeMediatR
{
    public interface ICustomMediatR
    {
        Task<TResponse> SendAsync<TResponse>(ICommandWithResponse<TResponse> command, CancellationToken cancellationToken);
    }
}
