﻿using Autofac;
using Amazon;
using Amazon.EC2;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace StackopsCore
{
    public static class DependencyInjection
    {
        private static readonly RegionEndpoint DefaultRegion = RegionEndpoint.APSoutheast2;

        public static ILifetimeScope Init()
        {
            var builder = new ContainerBuilder();
            
            builder
                .AddMediatR(typeof(DependencyInjection).Assembly);
                
            builder
                .RegisterInstance(new AmazonEC2Client(DefaultRegion))
                .As<IAmazonEC2>()
                .SingleInstance();

            return builder.Build().BeginLifetimeScope();
        }
    }
}
