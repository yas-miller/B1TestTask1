using B1TestTask1.Models;

namespace B1TestTask1.Helpers;

public class CustomFileGenerator
{
    public CustomFile CustomFile { get; set; }
    public string Separator { get; set; } = "||";
    public string FilePath { get; set; } = Path.Combine(AppContext.BaseDirectory, "text_files");

    private List<CustomFile> customFiles;
    private int stringsCount = 100000;

    public CustomFileGenerator()
    {
        
    }
    public CustomFileGenerator(ref CustomFile customFile)
    {
        this.CustomFile = customFile;
    }

    public void Generate()
    {
        using (var file = File.CreateText(Path.ChangeExtension(Path.Combine(this.FilePath, Path.GetRandomFileName()),
                   ".txt")))
        {
            for (int i = 0; i < this.stringsCount; i++)
            {
                
            }
            file.WriteLine();
        }
        
    }
}