using B1TestTask1.Models;

namespace B1TestTask1.Helpers;

public abstract class CustomFilesBaseHelper
{
    public List<CustomFile> CustomFiles { get; set; } = new List<CustomFile>();
    public string Separator { get; set; } = "||";
    public string FolderPath { get; set; } = Path.Combine(AppContext.BaseDirectory, "text_files");
}
