using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StackopsCore.Commands;
using StackopsCore.ServiceHandlers;

namespace StackopsCore.CommandHandlers
{
    public class StartStopStackHandler :
        IRequestHandler<StartStackCommand>,
        IRequestHandler<StopStackCommand>
    {
        private readonly IServiceHandler[] serviceHandlers;

        public StartStopStackHandler(IServiceHandler[] serviceHandlers)
        {
            this.serviceHandlers = serviceHandlers;
        }

        public async Task<Unit> Handle(StartStackCommand request, CancellationToken cancellationToken)
        {
            foreach(var stack in request.StacksToStart)
            {
                foreach(var handler in serviceHandlers.Where(h => h.CanHandle(stack)))
                {
                    await handler.StartStackService(stack);
                }
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(StopStackCommand request, CancellationToken cancellationToken)
        {
            foreach(var stack in request.StacksToStop)
            {
                foreach(var handler in serviceHandlers.Where(h => h.CanHandle(stack)))
                {
                    await handler.StopStackService(stack);
                }
            }

            return Unit.Value;
        }
    }
}