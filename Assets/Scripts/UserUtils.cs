using System;

public class UserUtils
{
    private static Random s_random = new Random();

    public static int GenerateRandomNumber(int min, int max)
    {
        int randomValue = s_random.Next(min, max);

        return randomValue;
    }
}
