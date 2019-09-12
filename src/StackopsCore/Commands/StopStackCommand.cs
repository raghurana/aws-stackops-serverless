using MediatR;
using StackopsCore.Models;

namespace StackopsCore.Commands
{
    public class StopStackCommand : IRequest
    {
        public Stack[] StacksToStop { get; }

        public StopStackCommand(params Stack[] stacksToStop)
        {
            StacksToStop = stacksToStop;
        }
    }
}