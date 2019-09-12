using System.Threading;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;
using Autofac;
using MediatR;
using Moq;
using StackopsCore;
using StackopsCore.Commands;
using StackopsCore.Models;
using StackopsCoreAcceptanceTests.Helpers;
using TestStack.BDDfy;
using Xunit;


namespace StackopsCoreAcceptanceTests
{
    public class StartMultipleStacksTest
    {
        private Stack[] stacksToStart;
        private StartStackCommand startStackCommand;
        private ILifetimeScope scope;
        private Mock<IAmazonEC2> mockEc2;

        void GivenACommandToStartAStackWithEc2Instances()
        {
            stacksToStart = new Stack[] 
            {
                new Stack 
                {
                    Name = "stack1",
                    Ec2InstanceIds = new string[] 
                    { 
                        "i-111", 
                        "i-222" 
                    }
                }
            };

            startStackCommand = new StartStackCommand(stacksToStart);
        }

        void AndGivenMockAwsDependencies()
        {
            mockEc2 = new Mock<IAmazonEC2>();
            scope = DependencyInjection.Init(new MockAwsClientsModule(mockEc2));
        }

        void AndGivenAllInstancesAreRunning()
        {
            //mockEc2.Setup(ec2 => ec2.descrive) 
        }
        
        async Task WhenSentToProgram()
        {
            var program = scope.Resolve<IMediator>();
            await program.Send(startStackCommand);
            scope.Dispose();
        }

        void ThenVerifyARequestWasSentToAwsEc2CientToStartInstances()
        {
            var checkRequest = It.Is<StartInstancesRequest>(r => 
                r.InstanceIds.Contains("i-111") && 
                r.InstanceIds.Contains("i-222"));

            mockEc2.Verify(ec2 => ec2.StartInstancesAsync(checkRequest, default(CancellationToken)));
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}