using MediatR;

namespace StackopsCore.Ec2.Commands
{
    public class StopEc2InstancesByIdCommand : IRequest<int>
    {
        public string[] InstanceIds { get; }

        public StopEc2InstancesByIdCommand(string[] instanceIds)
        {
            InstanceIds = instanceIds;
        }
    }
}