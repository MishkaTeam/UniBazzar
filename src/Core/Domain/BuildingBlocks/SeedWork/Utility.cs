namespace Domain.BuildingBlocks.SeedWork;

public static class DateTimeUtility
{
    
    public static long GetCurrentUnixUTCTimeSeconds()
    {
        //long unixTimeMilliseconds = 
        //    new Instant().ToUnixTimeSeconds();

        long unixTimeMilliseconds = 
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        return unixTimeMilliseconds;
    }

    public static DateTime FromUnixUtcTimeMilliseconds(this long time)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(time);

        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);

        return dateTimeOffset.DateTime;
    }

    public static DateTime FromUnixUtcTimeSeconds(this long time)
    {
        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);

        return dateTimeOffset.DateTime;
    }

    public static DateTime ToLocalTimeMilliseconds(this long time)
    {
        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);


        return dateTimeOffset.DateTime.ToLocalTime();
    }

    public static DateTime ToLocalTimeSeconds(this long time)
    {
        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);

        return dateTimeOffset.DateTime.ToLocalTime();
    }

    public static DateTime ConvertToTimeZoneMilliseconds(this long time, double UTCOffset)
    {
        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);


        return dateTimeOffset.DateTime.AddHours(UTCOffset);
    }

    public static DateTime ConvertToTimeZoneSeconds(this long time, double UTCOffset)
    {
        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);


        return dateTimeOffset.DateTime.AddHours(UTCOffset);
    }
}