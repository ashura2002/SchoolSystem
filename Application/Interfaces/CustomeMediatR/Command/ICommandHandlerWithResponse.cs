using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.CustomeMediatR.Command
{
    public interface ICommandHandlerWithResponse<TCommand, TResponse> where TCommand: ICommandWithResponse<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
