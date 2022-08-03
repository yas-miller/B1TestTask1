namespace B1TestTask1.Helpers;

public class CustomFilesJoiner: CustomFilesReader
{
    public void joinCustomFilesAndSaveToOneFile(string? folderPath = null, string? charSequenceToRemoveFromRecord = null)
    {
        this.batchReadCustomFilesInParallel(folderPath, charSequenceToRemoveFromRecord);
        saveAllCustomFilesToOneFile();
    }
    
    private string saveAllCustomFilesToOneFile()
    {
        var composedCustomFileFullName = Path.ChangeExtension(Path.Combine(this.FolderPath, Path.GetRandomFileName()),
            ".txt");
        using (var file = File.CreateText(composedCustomFileFullName))
        {
            foreach (var customFile in this.CustomFiles)
            {
                if (customFile.CustomRecordArray is not null)
                {
                    foreach (var customRecord in customFile.CustomRecordArray)
                    {
                        file.WriteLine($"{customRecord.RandomDate}{Separator}{customRecord.RandomLatinString}{Separator}{customRecord.RandomRussianString}{Separator}{customRecord.RandomPositiveEvenInteger}{Separator}{customRecord.RandomPositiveDouble}{Separator}");
                    }
                }
            }
        }
        Console.WriteLine($"Все файлы ({CustomFiles.Count}) успешно объединены в один файл по пути {composedCustomFileFullName}. \nСтрок с заданным сочетанием символов удалено: {linesRemovedCounter}");
        return composedCustomFileFullName;
    }
}
