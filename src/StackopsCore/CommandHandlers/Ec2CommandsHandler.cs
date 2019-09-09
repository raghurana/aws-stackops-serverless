using System.Threading;
using System.Threading.Tasks;
using MediatR;

public class Ec2CommandsHandler : IRequestHandler<Ec2IdsCommand>
{
    public Task<Unit> Handle(Ec2IdsCommand request, CancellationToken cancellationToken)
    {
        

        return Task.FromResult(Unit.Value);
    }
}