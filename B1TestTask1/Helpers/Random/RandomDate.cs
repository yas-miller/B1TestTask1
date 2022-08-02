namespace B1TestTask1.Helpers;

public class RandomDate: RandomBase
{
    public static DateOnly GetRandomDateOnly()
    {
        var overPastYears = 5;

        var start = DateOnly.FromDateTime(DateTime.Today);
        start = start.AddYears(-overPastYears);
        int range = (DateTime.Today - start.ToDateTime(new TimeOnly())).Days;
        return start.AddDays(random.Next(range));
    }
}
