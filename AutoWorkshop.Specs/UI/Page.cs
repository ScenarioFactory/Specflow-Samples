namespace AutoWorkshop.Specs.UI
{
    public abstract class Page
    {
        protected Page(AutoWorkshopDriver driver)
        {
            Driver = driver;
            Toolbar = new Toolbar(driver);
        }

        protected AutoWorkshopDriver Driver { get; }

        protected Toolbar Toolbar { get; }
    }
}
