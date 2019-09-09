using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Amazon.EC2;
using StackopsCore.Ec2.Commands;
using StackopsCore.Extensions;

namespace StackopsCore.Ec2.CommandsHandler
{
    public class StartStopEc2InstancesByIdCommandHandler : 
        IRequestHandler<StartEc2InstancesByIdCommand, int>,
        IRequestHandler<StopEc2InstancesByIdCommand, int>
    {
        private readonly IAmazonEC2 ec2;

        public StartStopEc2InstancesByIdCommandHandler(IAmazonEC2 ec2)
        {
            this.ec2 = ec2;
        }

        public async Task<int> Handle(StartEc2InstancesByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await ec2.StartInstancesAsyncByIds(request.InstanceIds);
            return response.StartingInstances.Count;
        }

        public async Task<int> Handle(StopEc2InstancesByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await ec2.StopInstancesAsyncByIds(request.InstanceIds);
            return response.StoppingInstances.Count;
        }
    }
}