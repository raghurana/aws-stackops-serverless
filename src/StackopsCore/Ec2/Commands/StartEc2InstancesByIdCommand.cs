using MediatR;

namespace StackopsCore.Ec2.Commands
{
    public class StartEc2InstancesByIdCommand : IRequest<int>
    {
        public string[] InstanceIds { get; }

        public StartEc2InstancesByIdCommand(string[] instanceIds)
        {
            InstanceIds = instanceIds;
        }
    }
}