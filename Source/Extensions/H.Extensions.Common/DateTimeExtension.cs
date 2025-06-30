// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Common;

public static class DateTimeExtension
{

    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        DateTimeOffset dto = new DateTimeOffset(dateTime);
        return dto.ToUnixTimeSeconds();
    }

    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = dateTime.ToUniversalTime() - origin;
        return (long)diff.TotalSeconds;
    }

    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
    {
        DateTimeOffset dto = new DateTimeOffset(dateTime);
        return dto.ToUnixTimeMilliseconds();
    }

    public static long ToUnixTimestampMilliseconds(this DateTime dateTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = dateTime.ToUniversalTime() - origin;
        return (long)diff.TotalMilliseconds;
    }

    public static DateTime UnixSecondsToDateTime(this long unixTimeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
        return dateTimeOffset.LocalDateTime; // 或 .UtcDateTime 获取UTC时间
    }

    public static DateTime UnixSecondsToDateTimeManual(this long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    public static DateTime UnixMillisecondsToDateTime(this long unixTimeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp);
        return dateTimeOffset.LocalDateTime; // 或 .UtcDateTime 获取UTC时间
    }

    public static DateTime UnixMillisecondsToDateTimeManual(this long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}
