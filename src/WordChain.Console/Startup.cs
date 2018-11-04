using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace WordChain
{
    public class Startup
    {
        public IServiceProvider ConfigureService(IServiceCollection services)
        {
            services.AddScoped<ServiceFactory>(p => p.GetService);

            services.AddLogging(c => c.AddConsole());
            services.AddTransient<ILogger>(p => p.GetRequiredService<ILoggerFactory>().CreateLogger("Console"));
            services.AddSingleton<IWordChainConfiguration>(new WordChainConfiguration());

            services.Scan(scan =>
            {
                scan
                    .FromAssembliesOf(typeof(IMediator), typeof(IWordChainConfiguration))
                    .AddClasses(true)
                    .AsImplementedInterfaces();
            });

            return services.BuildServiceProvider();
        }

        public CommandLineApplication<WordChainApplication> Configure(IServiceProvider provider)
        {
            var app = new CommandLineApplication<WordChainApplication>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(provider);
            return app;
        }
    }

}
