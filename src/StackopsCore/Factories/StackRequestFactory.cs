using System;
using System.Linq;
using System.Collections.Generic;
using StackopsCore.Models;
using MediatR;
using StackopsCore.Commands;
using StackopsCore.Extensions;

namespace StackopsCore.Factories
{
    public static class StackRequestFactory
    {
        public static IRequest CreateMediatorRequest(StackActionRequest request, IList<Stack> allStacks)
        {
            var matchedStack = allStacks.FirstOrDefault(s => s.Name.EqualsIgnoreCase(request.StackName));

            if(matchedStack == null)
                throw new ArgumentException($"Could not find a matching stack with name {request.StackName}");

            if(request.Action.EqualsIgnoreCase("start"))
                return new StartStackCommand(matchedStack);

            if(request.Action.EqualsIgnoreCase("stop"))
                return new StopStackCommand(matchedStack);

            throw new ArgumentException($"Unknown stack action {request.Action}");
        }
    }
}