using System;
using StackopsCore.Models;
using MediatR;
using StackopsCore.Commands;
using StackopsCore.Extensions;

namespace StackopsCore.Factories
{
    public static class StackRequestFactory
    {
        public static IRequest CreateMediatorRequest(Stack[] allStacks, string action)
        {
            if(action.EqualsIgnoreCase("start"))
                return new StartStackCommand(allStacks);

            if(action.EqualsIgnoreCase("stop"))
                return new StopStackCommand(allStacks);

            throw new ArgumentException($"Unknown stack action {action}");
        }
    }
}