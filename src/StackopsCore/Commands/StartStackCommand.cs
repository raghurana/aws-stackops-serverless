using MediatR;
using StackopsCore.Models;

namespace StackopsCore.Commands
{
    public class StartStackCommand : IRequest
    {
        public Stack[] StacksToStart { get; }

        public StartStackCommand(params Stack[] stacksToStart)
        {
            StacksToStart = stacksToStart;
        }
    }
}