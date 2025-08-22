using System;

public static class RandomTransmitter
{
    private static Random s_random = new Random();

    public static int ReadInt(int minValue, int maxValue) => s_random.Next(minValue, maxValue);

    public static float ReadFloat() => (float)s_random.NextDouble();
}