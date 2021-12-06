namespace Samples.AutomationFramework
{
    using System;
    using Newtonsoft.Json;
    using OpenQA.Selenium;

    public static class WebDriverJavaScriptExtensions
    {
        private const string SelectFunction = @"
            if (!Array.prototype.select) {
                Array.prototype.select = function (projection) {
                    var projected = [];
                    for (var i = 0; i < this.length; i++) {
                        projected.push(projection(this[i]));
                    }
                    return projected;
                };
            }";

        private const string WhereFunction = @"
            if (!Array.prototype.where) {
                Array.prototype.where = function (predicate) {
                    var matching = [];
                    for (var i = 0; i < this.length; i++) {
                        if (predicate(this[i])) {
                            matching.push(this[i]);
                        }
                    }
                    return matching;
                };
            }";

        public static void EnsureJavaScriptLinq(this IWebDriver driver)
        {
            driver.ExecuteScript(SelectFunction);
            driver.ExecuteScript(WhereFunction);

            BlockingDelay.Wait(TimeSpan.FromSeconds(3),
                "We need an artificial delay here to allow the browser to add the new functions before they are available for subsequent calls");
        }

        public static void ExecuteScript(this IWebDriver driver, string script)
        {
            ((IJavaScriptExecutor) driver).ExecuteScript(script);
        }

        public static T ExecuteScript<T>(this IWebDriver driver, string script)
        {
            object result = ((IJavaScriptExecutor) driver).ExecuteScript($"return JSON.stringify({script})");
            return JsonConvert.DeserializeObject<T>(result.ToString());
        }
    }
}