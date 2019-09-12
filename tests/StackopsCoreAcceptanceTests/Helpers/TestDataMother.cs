using Amazon.EC2.Model;
using StackopsCore.Constants;
using StackopsCore.Models;

namespace StackopsCoreAcceptanceTests.Helpers
{
    public static class TestDataMother
    {
        public static readonly Stack StackOne = 
            new Stack 
            {
                Name = "stack1",
                Ec2InstanceIds = new[] 
                {
                    "i-111",
                    "i-222"
                }
            };

        public static InstanceState Ec2StartedState = 
            new InstanceState
            {
                 Name = AwsConstants.Ec2StartedState
            };
        
    }
}