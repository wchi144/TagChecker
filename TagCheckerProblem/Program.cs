using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TagCheckerProblem;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddTransient<ITagChecker, TagChecker>())
    .Build();