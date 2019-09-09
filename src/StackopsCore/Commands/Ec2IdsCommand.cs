using MediatR;

public class Ec2IdsCommand : IRequest<bool>
{
    public string[] InstanceIds { get; private set; }

    public CommandType CommandType { get; private set; }

    public Ec2IdsCommand(string[] ec2InstanceIds, CommandType commandType)
    {
        InstanceIds = ec2InstanceIds;
        CommandType = commandType;
    }
}