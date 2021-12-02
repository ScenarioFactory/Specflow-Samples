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

        public static AcceptAlert StartsWithText(string text)
        {
            return new AcceptAlert(text);
        }

        protected override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            IAlert alert = driver.Wait(5).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            if (alert != null)
            {
                driver.SwitchTo().Alert();

                if (alert.Text.StartsWith(_text))
                {
                    driver.SwitchTo().Alert().Accept();
                }
            }
        }
    }
}
