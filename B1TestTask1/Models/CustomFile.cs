using System.ComponentModel.DataAnnotations.Schema;

namespace B1TestTask1.Models;

public class CustomFile
{
    [Column(TypeName="Date")]
    public DateOnly RandomDate { get; set; }
    public string RandomLatinString { get; set; }
    public string RandomRussianString { get; set; }
    public int RandomPositiveEvenInteger { get; set; }
    public double RandomPositiveDouble { get; set; }
}
