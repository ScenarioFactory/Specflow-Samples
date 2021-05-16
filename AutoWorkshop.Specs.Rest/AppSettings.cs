namespace AutoWorkshop.Specs.Rest
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class AppSettings
    {
        private static readonly IConfiguration Configuration;

        static AppSettings()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        public string MySqlConnectionString => Configuration["AutoWorkshop:MySqlConnectionString"];

        public string RestApiUrl => Configuration["AutoWorkshop:RestApiUrl"];
    }
}
