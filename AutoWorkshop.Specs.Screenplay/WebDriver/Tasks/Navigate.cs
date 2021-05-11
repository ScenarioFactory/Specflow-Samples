namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using Pattern;

    public class Navigate : WebTask
    {
        private readonly string _path;

        private Navigate(string path)
        {
            _path = path;
        }

        public static Navigate ToMaintainCustomers()
        {
            return new Navigate("custmaint.php");
        }

        public static Navigate ToChangeRegistration()
        {
            return new Navigate("changereg.php");
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.NavigateTo(_path);
        }
    }
}
