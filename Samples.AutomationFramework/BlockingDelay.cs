namespace Samples.AutomationFramework
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;

    public static class BlockingDelay
    {
        public static void Wait(TimeSpan waitTime, string reason)
        {
            reason.Should().NotBeNullOrEmpty(); // enforce a reason for information purposes

            Task.Delay(waitTime).Wait();
        }
    }
}