using Application.Interfaces.CustomeMediatR;
using Application.Interfaces.CustomeMediatR.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Services
{
    public class CustomMediatR : ICustomMediatR
    {
        private readonly IServiceProvider _service;

        public CustomMediatR(IServiceProvider service)
        {
            _service = service;
        }

        public async Task<TResponse> SendAsync<TResponse>(ICommandWithResponse<TResponse> command, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler is resolving in CUSTOM MEDIATR...");


            // Get the runtime type of the command Ex. LoginCommand
            var commandType = command.GetType();

            // Build the closed generic handler type Ex. IcommandHandler<Command, TResult>
            var handlerType = typeof(ICommandHandlerWithResponse<,>).MakeGenericType(commandType, typeof(TResponse));

            // Get the registered handler in DI container
            var handler = _service.GetService(handlerType) ??
                throw new InvalidOperationException($"Handler not found {handlerType.Name}");

            // Get the Handle method from the handler interface 
            var runtimeMethod = handlerType
                .GetMethod(nameof(ICommandHandlerWithResponse<ICommandWithResponse<TResponse>, TResponse>.Handle)) ??
                throw new InvalidOperationException($"Not found{handlerType.FullName}");

            // Invoke the Handle method via reflection and cast the result to Task<TResponse>
            var invokeResult = (Task<TResponse>)runtimeMethod?.Invoke(handler, new object[] { command, cancellationToken })!;
            return await invokeResult;
        }
    }
}
