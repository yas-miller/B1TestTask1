using System.Reflection;
using B1TestTask1.Extensions;
using B1TestTask1.Models;

namespace B1TestTask1.Helpers;

public class CustomFilesGenerator: CustomFilesBaseHelper
{
    private int filesCount = 100;
    private int stringsCount = 100000;

    private static object locker = new object();

    public CustomFilesGenerator()
    {
        
    }
    
    public CustomFilesGenerator(ref List<CustomFile> customFiles)
    {
        this.CustomFiles = customFiles;
    }

    
    public void SaveCustomFile(CustomFile customFile)
    {
        lock (locker)
        {
            if (!Directory.Exists(this.FolderPath))
            {
                Directory.CreateDirectory(this.FolderPath);
            }
        }

        using (var file = File.CreateText(Path.ChangeExtension(Path.Combine(this.FolderPath, Path.GetRandomFileName()),
                   ".txt")))
        {
            foreach (var customRecord in customFile.CustomRecordArray)
            {
                file.WriteLine($"{customRecord.RandomDate}{Separator}{customRecord.RandomLatinString}{Separator}{customRecord.RandomRussianString}{Separator}{customRecord.RandomPositiveEvenInteger}{Separator}{customRecord.RandomPositiveDouble}{Separator}");
            }
        }
    }


    public CustomFile GenerateCustomFile()
    {
        var customFile = new CustomFile
            { CustomRecordArray = CustomRecord5Randomizer.BatchGetRandomCustomRecordsInParallel(stringsCount) };
        CustomFiles.Add(customFile);
        return customFile;
    }

    public List<CustomFile> BatchGenerateAndSaveCustomFilesInParallel(int? count = null)
    {
        count ??= this.filesCount;
        List<CustomFile> outputCustomFilesList = new List<CustomFile>();

        int divideBy = (int) count;
        while (divideBy % 2 == 0)
        {
            divideBy /= 2;
        }
        
        Task<List<CustomFile>>[] tasksArray = new Task<List<CustomFile>>[(int) (count / divideBy)];
        for (int i = 0; i < count / divideBy; i++)
        {
            tasksArray[i] = Task.Delay(5000).ContinueWith(_ => BatchGenerateAndSaveCustomFiles(divideBy));
        }

        
        var whenAll = tasksArray.WhenAllEx(
            _ =>
            {
                Console.WriteLine(
                    $"Генерация и сохранение файлов: {((double) _.Count(task => task.IsCompleted) / tasksArray.Length) * 100}% завершено");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            });
        
        whenAll.Wait();
        Console.WriteLine($"Все файлы ({CustomFiles.Count}) успешно сгененированы и сохранены по пути {FolderPath}");


        foreach (var task in tasksArray)
        {
            outputCustomFilesList.AddRange(task.Result);
        }

        return outputCustomFilesList;
    }
    
    private List<CustomFile> BatchGenerateAndSaveCustomFiles(int count)
    {
        List<CustomFile> outputCustomFilesList = new List<CustomFile>();
        for (int i = 0; i < count; i++)
        {
            SaveCustomFile(GenerateCustomFile());
        }
        return outputCustomFilesList;
    }
}
