namespace B1TestTask1.Models.Services;

public class CustomFilesService
{
    public static CustomFilesService instance = new CustomFilesService();

    private CustomFilesService()
    {
        
    }
    
    public void saveCustomFilesToDB(IReadOnlyList<CustomFile> customFiles)
    {
        if (customFiles.Count == 0)
        {
            throw new Exception("Нет файлов для сохранения в БД");
        }
        
        int allLinesCount = 0;
        foreach (var customFile in customFiles)
        {
            allLinesCount += customFile.CustomRecordArray.Count;
        }

        int linesImported = 0;
        using (var db = new ApplicationDBContext())
        {
            foreach (var customFile in customFiles)
            {
                db.CustomRecords.AddRange(customFile.CustomRecordArray);
                db.SaveChanges();

                linesImported += customFile.CustomRecordArray.Count;
                Console.WriteLine(
                    $"Добавление строк в БД: {((double) linesImported / allLinesCount) * 100}% завершено");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }
    
    public void saveCustomRecordToDB(CustomRecord5 customRecord)
    {
        using (var db = new ApplicationDBContext())
        {
            db.CustomRecords.Add(customRecord);
            db.SaveChanges();
        }
    }
}
