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

        public static Navigate To(string path)
        {
            return new Navigate(path);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.NavigateTo(_path);
        }
    }
}
