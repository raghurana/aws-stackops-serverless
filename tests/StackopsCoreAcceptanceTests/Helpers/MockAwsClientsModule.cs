using Amazon.EC2;
using Autofac;
using Moq;

namespace StackopsCoreAcceptanceTests.Helpers
{
    public class MockAwsClientsModule : Module
    {
        private Mock<IAmazonEC2> mockEc2Client;

        public MockAwsClientsModule(Mock<IAmazonEC2> mockEc2Client)
        {
            this.mockEc2Client = mockEc2Client;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(mockEc2Client.Object)
                .As<IAmazonEC2>();
        }
    }
}