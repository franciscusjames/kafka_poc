using System;

public static class MyTimeZone
{
    public static DateTime Now => DateTime.UtcNow.AddHours(-3);
}