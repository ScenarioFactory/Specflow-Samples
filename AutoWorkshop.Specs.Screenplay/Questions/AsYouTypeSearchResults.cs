﻿namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using System.Linq;
    using Drivers;
    using OpenQA.Selenium;
    using Pattern;

    public class AsYouTypeSearchResults : WebQuestion<string[]>
    {
        private readonly By _locator;

        public AsYouTypeSearchResults(By locator)
        {
            _locator = locator;
        }

        public static AsYouTypeSearchResults For(By locator)
        {
            return new AsYouTypeSearchResults(locator);
        }

        public override string[] AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            var anchors = driver.FindElements(_locator);

            return anchors
                .Where((a, i) => i % 2 != 0)
                .Select(a => a.Text)
                .ToArray();
        }
    }
}