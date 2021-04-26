namespace AutoWorkshop.Model.Extensions
{
    using System;

    internal static class Guards
    {
        public static void ShouldNotBeNull<T>(this T o, string errorMessage) where T: class
        {
            if (o == null)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}
