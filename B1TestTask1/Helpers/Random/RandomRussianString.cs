namespace B1TestTask1.Helpers;

public class RandomRussianString: RandomBase
{
    public static string GetRandomRussianString()
    {
        var length = 10;
        
        string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        alphabet += alphabet.ToLower();
        return new string(Enumerable.Repeat(alphabet, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
