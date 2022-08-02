using B1TestTask1.Models;

namespace B1TestTask1.Helpers;

public class CustomFilesJoiner: CustomFilesBaseHelper
{
    
    public CustomFilesJoiner()
    {
        
    }
    
    public CustomFilesJoiner(ref List<CustomFile> customFiles)
    {
        this.CustomFiles = customFiles;
    }
}