using System.Globalization;

namespace B1TestTask1.Helpers;

public class RandomPositiveDouble: RandomBase
{
    public static double GetRandomPositiveDouble()
    {
        int integerPart = random.Next(1, 20 + 1);
        int fractionalPart = random.Next(10000000, 100000000);
        var y = $"{integerPart}.{fractionalPart}";
        return double.Parse($"{integerPart}.{fractionalPart}", CultureInfo.InvariantCulture);
    }
}
