using Autofac;
using Amazon;
using Amazon.EC2;
using MediatR.Extensions.Autofac.DependencyInjection;
using System.Reflection;
using StackopsCore.ServiceHandlers;

namespace StackopsCore
{
    public static class DependencyInjection
    {
        public static ILifetimeScope Init()
        {
            return Init(new RealExternalDependencies());
        }

        public static ILifetimeScope Init(params Autofac.Module[] externalDependenciesModules)
        {
            var builder = new ContainerBuilder();

            builder
                .AddMediatR(typeof(DependencyInjection).Assembly);

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<IServiceHandler>()
                .AsImplementedInterfaces();

            foreach(var autofacModule in externalDependenciesModules)
                builder.RegisterModule(autofacModule);
            
            return builder.Build().BeginLifetimeScope();
        }
    }

    public class RealExternalDependencies : Autofac.Module
    {
        public static readonly RegionEndpoint DefaultAwsRegion = RegionEndpoint.APSoutheast2;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(new AmazonEC2Client(DefaultAwsRegion))
                .As<IAmazonEC2>()
                .SingleInstance();
        }
    }
}
