using System;
using Microsoft.Extensions.DependencyInjection;

namespace Spectre.CommandLine.DependencyInjection
{
    internal class InjectionResolver : IResolver
    {
        public InjectionResolver(IServiceCollection services)
        {
            Services = services;
        }
        internal IServiceCollection Services {get;set;}

        public object Resolve(Type type)
        {
            return (Services.BuildServiceProvider()).GetService(type) ?? Activator.CreateInstance(type);
        }
    }
}