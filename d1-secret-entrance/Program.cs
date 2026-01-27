
using Aoc;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<ICombinationSafe, CombinationSafe>();
services.AddTransient<App>();

using ServiceProvider provider = services.BuildServiceProvider();
provider.GetRequiredService<App>().Run(args);