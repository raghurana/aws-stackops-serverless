using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;
using MediatR;

public class Ec2CommandsHandler : IRequestHandler<Ec2IdsCommand, bool>
{
    private readonly IAmazonEC2 ec2Client;

    public Ec2CommandsHandler(IAmazonEC2 ec2Client)
    {
        this.ec2Client = ec2Client;
    }

    public async Task<bool> Handle(Ec2IdsCommand request, CancellationToken cancellationToken)
    {
        var instanceList = new List<string>(request.InstanceIds);

        switch(request.CommandType)
        {
            case CommandType.Start:
                var startRequest  = new StartInstancesRequest(instanceList);
                var startResponse = await ec2Client.StartInstancesAsync(startRequest);
                return startResponse.HttpStatusCode == HttpStatusCode.OK;

            case CommandType.Stop:
                var stopRequest  = new StopInstancesRequest(instanceList);
                var stopResponse = await ec2Client.StopInstancesAsync(stopRequest);
                return stopResponse.HttpStatusCode == HttpStatusCode.OK;

            default:
                throw new ArgumentException($"CommandType: {request.CommandType} is unknown.");
        }
    }
}