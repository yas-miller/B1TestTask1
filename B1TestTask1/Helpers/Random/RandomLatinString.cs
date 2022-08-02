namespace B1TestTask1.Helpers;

public class RandomLatinString: RandomBase
{
    public static string GetRandomLatinString()
    {
        var length = 10;
        
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        alphabet += alphabet.ToLower();
        return new string(Enumerable.Repeat(alphabet, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
