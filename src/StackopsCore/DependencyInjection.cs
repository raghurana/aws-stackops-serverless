using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace StackopsCore
{
    public static class DependencyInjection
    {
        public static ILifetimeScope Init()
        {
            var builder = new ContainerBuilder();
            
            builder
                .AddMediatR(typeof(DependencyInjection)
                .Assembly);

            return builder.Build().BeginLifetimeScope();
        }
    }
}
