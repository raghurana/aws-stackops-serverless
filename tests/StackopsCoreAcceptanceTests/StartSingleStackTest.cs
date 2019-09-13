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
using StackopsCoreAcceptanceTests.Helpers;
using TestStack.BDDfy;
using Xunit;

namespace StackopsCoreAcceptanceTests
{
    public class StartSingleStackTest
    {
        private StartStackCommand startStackCommand;
        private ILifetimeScope scope;
        private Mock<IAmazonEC2> mockEc2;

        void GivenAStartStackCommandWithEc2Instances()
        {
            startStackCommand = new StartStackCommand(TestDataMother.StackOne);
        }

        void AndGivenAllInstancesAreStopped()
        {
            mockEc2 = new Mock<IAmazonEC2>();
            scope   = DependencyInjection.Init(new MockAwsClientsModule(mockEc2));

            var describeResponse = new DescribeInstanceStatusResponse
            {
                InstanceStatuses = new List<InstanceStatus>
                {
                    new InstanceStatus 
                    { 
                        InstanceId = TestDataMother.StackOne.Ec2InstanceIds[0], 
                        InstanceState = TestDataMother.Ec2StoppedInstanceState
                    }, 
                    new InstanceStatus 
                    { 
                        InstanceId = TestDataMother.StackOne.Ec2InstanceIds[1], 
                        InstanceState = TestDataMother.Ec2StoppedInstanceState
                    }
                }
            };

            mockEc2
                .Setup(ec2 => ec2.DescribeInstanceStatusAsync(It.Is<DescribeInstanceStatusRequest>(req => 
                        req.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[0]) &&
                        req.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[1]) && 
                        req.IncludeAllInstances == true), 
                        default(CancellationToken)))
                .ReturnsAsync(describeResponse);

             mockEc2
                .Setup(ec2 => ec2.StartInstancesAsync(It.IsAny<StartInstancesRequest>(), default(CancellationToken)))
                .ReturnsAsync(() => new StartInstancesResponse {
                    StartingInstances = new List<InstanceStateChange>
                    {
                        new InstanceStateChange { InstanceId = TestDataMother.StackOne.Ec2InstanceIds[0] },
                        new InstanceStateChange { InstanceId = TestDataMother.StackOne.Ec2InstanceIds[1] },
                    }
                });
        }
        
        async Task ThenVerifyAwsEc2ClientStartinstancesMethodWasInvoked()
        {
            await scope.Resolve<IMediator>().Send(startStackCommand);
            
             mockEc2.Verify(ec2 => 
                ec2.StartInstancesAsync(
                    It.Is<StartInstancesRequest>(r => 
                        r.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[0]) &&  
                        r.InstanceIds.Contains(TestDataMother.StackOne.Ec2InstanceIds[1]) ), 
                        default(CancellationToken)));

            scope.Dispose();
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}