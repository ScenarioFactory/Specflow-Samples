namespace Samples.AutomationFramework
{
    using System;
    using Polly;

    public static class FunctionRetrier
    {
        public static void RetryOnException<TException>(Action action, int retryCount = 5, int waitSeconds = 1)
            where TException : Exception
        {
            Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(action);
        }

        public static TResult RetryOnException<TResult, TException>(Func<TResult> function, int retryCount = 5, int waitSeconds = 1)
            where TException : Exception
        {
            return Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(function);
        }

        public static TResult RetryOnExceptions<TResult, TException1, TException2>(Func<TResult> function, int retryCount = 5, int waitSeconds = 1)
            where TException1 : Exception
            where TException2 : Exception
        {
            return Policy
                .Handle<Exception>(ex => ex is TException1 || ex is TException2)
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(function);
        }
    }
}