namespace AutoWorkshop.Specs
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

        public string AuthenticationCookieName => Configuration["AutoWorkshop:AuthenticationCookie:Name"];

        public string AuthenticationCookieValue => Configuration["AutoWorkshop:AuthenticationCookie:Value"];

        public string BaseUrl => Configuration["AutoWorkshop:Url"];

        public string BlobStorageConnectionString => Configuration["AutoWorkshop:BlobStorageConnectionString"];

        public string MySqlConnectionString => Configuration["AutoWorkshop:MySqlConnectionString"];

        public string ServiceBusConnectionString => Configuration["AutoWorkshop:ServiceBusConnectionString"];
    }
}
