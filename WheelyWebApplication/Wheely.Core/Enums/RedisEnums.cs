namespace Wheely.Core.Enums
{
    /// <summary>
    /// Absolute expiration for cache
    /// </summary>
    public enum AbsoluteExpiration
    {
        TwentyMinutes = 20,
        ThirtyMinutes = 30,
        FortyFiveMinutes = 45,
        OneHour = 60,
        OneAndHalfHours = 90,
        TwoHour = 120
    }

    /// <summary>
    /// Sliding expiration for cache
    /// </summary>
    public enum SlidingExpiration
    {
        OneMinute = 1,
        ThreeMinute = 3,
        FiveMinute = 5,
        TenMinute = 10
    }

    /// <summary>
    /// Redis keys search type
    /// </summary>
    public enum KeySearchType
    {
        EndWith = 1,
        StartWith = 2,
        Include = 3
    }
}
