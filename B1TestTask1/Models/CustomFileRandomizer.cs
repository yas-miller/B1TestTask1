using System.Runtime.CompilerServices;

namespace B1TestTask1.Models;

public class CustomFileRandomizer: CustomFile
{
    public static CustomFile Randomizer()
    {
        var t = 1;
        return new CustomFile();
        //return new CustomFile {RandomDate = Helpers.RandomDate.GetRandomDateOnly(), RandomLatinString = }
    }
}
