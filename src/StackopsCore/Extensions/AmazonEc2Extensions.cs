using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace StackopsCore.Extensions
{
    public static class AmazonEc2Extensions
    {
        public static async Task<string[]> FilterInstancesByState(this IAmazonEC2 ec2Client, IEnumerable<string> instanceIds, string stateName)
        {
            var describeRequest  = new DescribeInstanceStatusRequest {InstanceIds = new List<string>(instanceIds)};
            var describeResponse = await ec2Client.DescribeInstanceStatusAsync(describeRequest);

            return describeResponse
                .InstanceStatuses
                .Where(state => state.InstanceState.Name == stateName)
                .Select(s => s.InstanceId)
                .ToArray();
        }

        public static Task<StartInstancesResponse> StartInstancesAsyncByIds(this IAmazonEC2 ec2Client,  IEnumerable<string> instanceIds)
        {
            var startRequest = new StartInstancesRequest(new List<string>(instanceIds));
            return ec2Client.StartInstancesAsync(startRequest);
        }

        public static Task<StopInstancesResponse> StopInstancesAsyncByIds(this IAmazonEC2 ec2Client, IEnumerable<string> instanceIds)
        {
            var stopRequest = new StopInstancesRequest(new List<string>(instanceIds));
            return ec2Client.StopInstancesAsync(stopRequest);
        }
    }
}