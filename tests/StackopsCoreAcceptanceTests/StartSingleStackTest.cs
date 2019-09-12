using System.Collections.Generic;
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
    public class StartSingleStackTest
    {
        private Stack[] stacksToStart;
        private StartStackCommand startStackCommand;
        private ILifetimeScope scope;
        private Mock<IAmazonEC2> mockEc2;

        void GivenACommandToStartAStackWithEc2Instances()
        {
            startStackCommand = new StartStackCommand(TestDataMother.StackOne);
        }

        void AndGivenMockAwsDependencies()
        {
            mockEc2 = new Mock<IAmazonEC2>();
            scope = DependencyInjection.Init(new MockAwsClientsModule(mockEc2));
        }

        void AndGivenAllInstancesAreRunning()
        {
            var describeRequest = It.Is<DescribeInstanceStatusRequest>( req => 
                req.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[0]) &&
                req.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[1]) &&
                req.IncludeAllInstances == true );
   
            var describeResponse = new DescribeInstanceStatusResponse
            {
                InstanceStatuses = new List<InstanceStatus>
                {
                    new InstanceStatus 
                    { 
                        InstanceId = TestDataMother.StackOne.Ec2InstanceIds[0], 
                        InstanceState = TestDataMother.Ec2StartedState
                    }, 
                    new InstanceStatus 
                    { 
                        InstanceId = TestDataMother.StackOne.Ec2InstanceIds[1], 
                        InstanceState = TestDataMother.Ec2StartedState
                    }
                }
            };

            mockEc2
                .Setup(ec2 => ec2.DescribeInstanceStatusAsync(describeRequest, default(CancellationToken)))
                .ReturnsAsync(describeResponse);
        }
        
        async Task WhenSentToProgram()
        {
            var program = scope.Resolve<IMediator>();
            await program.Send(startStackCommand);
            scope.Dispose();
        }

        void ThenVerifyEc2StartInstancesWasInvoked()
        {
            var checkRequest = It.Is<StartInstancesRequest>(r => 
                r.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[0]) && 
                r.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[1]));

            mockEc2.Verify(ec2 => ec2.StartInstancesAsync(checkRequest, default(CancellationToken)));
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}