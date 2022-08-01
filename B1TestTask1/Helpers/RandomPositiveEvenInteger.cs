namespace B1TestTask1.Helpers;

public class RandomPositiveEvenInteger: RandomBase
{
    public static int GetRandomPositiveEvenInteger()
    {
        return ParallelEnumerable.Range(
            1, 100000000).Where(i => i % 2 == 0).Select(
            value => value).ElementAt(random.Next(100000000));
    }
}
