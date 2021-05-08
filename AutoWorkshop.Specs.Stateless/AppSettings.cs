namespace AutoWorkshop.Specs.Stateless
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

        public string MySqlConnectionString => Configuration["AutoWorkshop:MySqlConnectionString"];
    }
}
