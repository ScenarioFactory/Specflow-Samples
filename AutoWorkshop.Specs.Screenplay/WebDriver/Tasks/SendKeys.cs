namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using OpenQA.Selenium;
    using Pattern;

    public class SendKeys : WebTask
    {
        private readonly By _locator;
        private readonly string _keys;
        private bool _clearElement = true;
        private bool _oneKeyAtATime;

        private SendKeys(By locator, string keys)
        {
            _locator = locator;
            _keys = keys;
        }

        public static SendKeys To(By locator, string keys)
        {
            return new SendKeys(locator, keys);
        }

        public SendKeys WithoutClearing()
        {
            _clearElement = false;
            return this;
        }

        public SendKeys OneKeyAtATime()
        {
            _oneKeyAtATime = true;
            return this;
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            IWebElement element = driver.WaitForElement(_locator);
            
            if (_clearElement)
            {
                element.Clear();
            }

            if (_oneKeyAtATime)
            {
                foreach (char ch in _keys)
                {
                    element.SendKeys(new string(new[] {ch}));
                }
            }
            else
            {
                element.SendKeys(_keys);
            }
        }
    }
}
