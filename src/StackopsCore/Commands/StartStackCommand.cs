using MediatR;
using StackopsCore.Models;

namespace StackopsCore.Commands
{
    public class StartStackCommand : IRequest
    {
        public Stack StackToStart { get; }

        public StartStackCommand(Stack stackToStart)
        {
            StackToStart = stackToStart;
        }
    }
}