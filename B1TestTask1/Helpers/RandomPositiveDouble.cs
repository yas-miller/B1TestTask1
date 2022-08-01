namespace B1TestTask1.Helpers;

public class RandomPositiveDouble: RandomBase
{
    public static double GetRandomPositiveDouble()
    {
        int integerPart = ParallelEnumerable.Range(
            1, 20).Select(
            value => value).ElementAt(random.Next(20));
        int fractionalPart = ParallelEnumerable.Range(
            10000000, 100000000 - 1).Select(
            value => value).ElementAt(random.Next(100000000 - 1));
        return Convert.ToDouble($"{integerPart}.{fractionalPart}");
    }
}
