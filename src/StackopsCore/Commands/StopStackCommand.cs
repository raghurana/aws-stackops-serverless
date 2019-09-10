using MediatR;
using StackopsCore.Models;

namespace StackopsCore.Commands
{
    public class StopStackCommand : IRequest
    {
        public Stack StackToStop { get; }

        public StopStackCommand(Stack stackToStop)
        {
            StackToStop = stackToStop;
        }
    }
}