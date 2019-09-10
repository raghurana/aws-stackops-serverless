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
            foreach(var handler in serviceHandlers.Where(h => h.CanHandle(request.StackToStart)))
            {
                await handler.StartStackService(request.StackToStart);
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(StopStackCommand request, CancellationToken cancellationToken)
        {
            foreach(var handler in serviceHandlers.Where(h => h.CanHandle(request.StackToStop)))
            {
                await handler.StopStackService(request.StackToStop);
            }

            return Unit.Value;
        }
    }
}