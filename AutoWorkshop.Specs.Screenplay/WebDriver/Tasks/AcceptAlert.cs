namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using OpenQA.Selenium;
    using Pattern;

    public class AcceptAlert : WebTask
    {
        private readonly string _text;

        private AcceptAlert(string text)
        {
            _text = text;
        }

        public static AcceptAlert WithText(string text)
        {
            return new AcceptAlert(text);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            IAlert alert = driver.SwitchTo().Alert();

            if (alert.Text == _text)
            {
                driver.SwitchTo().Alert().Accept();
            }
        }
    }
}
