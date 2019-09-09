using MediatR;

public class Ec2IdsCommand : IRequest
{
    private readonly string[] instanceIds;
    private readonly CommandType commandType;

    public Ec2IdsCommand(string[] ec2InstanceIds, CommandType commandType)
    {
        this.instanceIds = ec2InstanceIds;
        this.commandType = commandType;
    }
}