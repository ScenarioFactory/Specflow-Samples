namespace AutoWorkshop.Specs.Utilities
{
    using System;
    using System.Threading.Tasks;

    public static class Poller
    {
        public static bool PollForResult(Func<bool> fn, int pollingLimit = 10, int pollingIntervalSeconds = 1)
        {
            for (var i = 0; i < pollingLimit; i++)
            {
                if (fn())
                {
                    return true;
                }

                Task.Delay(TimeSpan.FromSeconds(pollingIntervalSeconds)).Wait();
            }

            return false;
        }
    }
}
