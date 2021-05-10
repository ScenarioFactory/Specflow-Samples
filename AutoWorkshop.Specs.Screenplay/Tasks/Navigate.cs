namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using Pattern;

    public class Navigate : WebTask
    {
        private readonly string _path;

        private Navigate(string path)
        {
            _path = path;
        }

        public static Navigate ToCustomerMaintenance()
        {
            return new Navigate("custmaint.php");
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.NavigateTo(_path);
        }
    }
}
