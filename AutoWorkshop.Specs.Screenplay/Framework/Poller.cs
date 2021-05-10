namespace AutoWorkshop.Specs.Screenplay.Framework
{
    using System;
    using System.Threading.Tasks;

    public static class Poller
    {
        public static bool PollForResult(Func<bool> predicate, int pollingLimit = 10, int pollingIntervalSeconds = 1)
        {
            for (var i = 0; i < pollingLimit; i++)
            {
                if (predicate())
                {
                    return true;
                }

                Task.Delay(TimeSpan.FromSeconds(pollingIntervalSeconds)).Wait();
            }

            return false;
        }
    }
}
