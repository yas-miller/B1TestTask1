namespace B1TestTask1.Helpers;

public class RandomPositiveEvenInteger: RandomBase
{
    public static int GetRandomPositiveEvenInteger()
    {
        int outputInt;
        while ((outputInt = random.Next(1, 100000000 + 1)) % 2 != 0);
        return outputInt;
    }
}
