using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBusLite.Bus;
using EventBusLite.Bus.InMemory;
using EventBusLite.Bus.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBusLite.ApiSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // builder.AddRedisEventBus(typeof(Startup).GetTypeInfo().Assembly);
            builder.AddInMemoryEventBus(typeof(Startup).GetTypeInfo().Assembly);

            services.AddMvc();
            builder.Populate(services);
            var container = builder.Build();

            container.Resolve<IEventConsumer>().Subscriber.Subscribe();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
