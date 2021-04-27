namespace AutoWorkshop.Specs
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Configuration
    {
        public static IConfiguration AppSettings { get; }

        static Configuration()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }
    }
}
