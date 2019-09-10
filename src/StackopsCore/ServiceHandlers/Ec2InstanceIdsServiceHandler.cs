using System.Linq;
using System.Threading.Tasks;
using Amazon.EC2;
using StackopsCore.Constants;
using StackopsCore.Extensions;
using StackopsCore.Models;

namespace StackopsCore.ServiceHandlers
{
    public class Ec2InstanceIdsServiceHandler : IServiceHandler
    {
        private readonly IAmazonEC2 ec2;

        public Ec2InstanceIdsServiceHandler(IAmazonEC2 ec2)
        {
            this.ec2 = ec2;
        }

        public bool CanHandle(Stack stack)
        {
            return stack.Ec2InstanceIds.Any();
        }

        public async Task<int> StartStackService(Stack stack)
        {
            var stoppedInstanceIds = await ec2.FilterInstancesByState(stack.Ec2InstanceIds, AwsConstants.Ec2StoppedState);
            if(!stoppedInstanceIds.Any())
                return 0;
         
            var startInstanceResponse = await ec2.StartInstancesAsyncByIds(stoppedInstanceIds);
            return startInstanceResponse.StartingInstances.Count;
        }

        public async Task<int> StopStackService(Stack stack)
        {
            var startedInstanceIds = await ec2.FilterInstancesByState(stack.Ec2InstanceIds, AwsConstants.Ec2StartedState);
            if(!startedInstanceIds.Any())
                return 0;

            var stopInstanceResponse = await ec2.StopInstancesAsyncByIds(startedInstanceIds);
            return stopInstanceResponse.StoppingInstances.Count;
        }
    }
}