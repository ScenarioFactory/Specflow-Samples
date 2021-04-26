namespace AutoWorkshop.Specs
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class TestConfig
    {
        static TestConfig()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        public static IConfigurationRoot AppSettings { get; }
    }
}
