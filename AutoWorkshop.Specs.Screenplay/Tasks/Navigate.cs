namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using System;
    using Drivers;
    using Screenplay;

    public class Navigate : WebTask
    {
        private readonly string _path;

        private Navigate(string path)
        {
            _path = path;
        }

        public static Navigate ToCustomerMaintenance() => new Navigate("custmaint.php");

        public override void PerformAs(Actor actor, AutoWorkshopDriver driver)
        {
            var appSettings = new AppSettings();
            driver.Navigate().GoToUrl(Uri.EscapeUriString($"{appSettings.BaseUrl}/{_path}"));
        }
    }
}
