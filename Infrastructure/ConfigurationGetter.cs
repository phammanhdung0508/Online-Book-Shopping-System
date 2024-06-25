using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class ConfigurationGetter
{
    public static IConfigurationRoot BuildConfiguration()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();

        return config;
    }
}
