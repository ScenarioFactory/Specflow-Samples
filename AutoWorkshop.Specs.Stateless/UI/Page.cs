namespace AutoWorkshop.Specs.Stateless.UI
{
    public abstract class Page
    {
        protected Page(AutoWorkshopDriver driver)
        {
            Driver = driver;
            Toolbar = new Toolbar(driver);
        }

        protected AutoWorkshopDriver Driver { get; }

        public Toolbar Toolbar { get; }
    }
}
