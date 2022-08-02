using System.Reflection.Metadata;
using B1TestTask1.Extensions;
using B1TestTask1.Models;

namespace B1TestTask1.Helpers;

public class CustomFilesJoiner: CustomFilesBaseHelper
{
    private int linesRemovedCounter;
    
    private List<string> filesFullNames;
    private object locker = new object();
    public CustomFilesJoiner()
    {
        
    }
    
    public CustomFilesJoiner(ref List<CustomFile> customFiles)
    {
        this.CustomFiles = customFiles;
    }


    public string batchJoinCustomFilesInParallel(string? folderPath = null, string? charSequenceToRemoveFromRecord = null)
    {
        CustomFiles.Clear();
        
        folderPath ??= this.FolderPath;
        getAllFileNamesInFolder();

        if (filesFullNames.Count == 0)
        {
            throw new Exception("Нет файлов в папке для объединения");
        }
        
        int count = filesFullNames.Count;
        int divideBy = count;
        while (divideBy % 2 == 0)
        {
            divideBy /= 2;
        }
        
        Task<List<CustomFile>>[] tasksArray = new Task<List<CustomFile>>[(int) (count / divideBy)];
        for (int i = 0; i < count / divideBy; i++)
        {
            int startIndex = i * divideBy;
            tasksArray[i] = Task.Delay(5000).ContinueWith(_ => batchReadCustomFiles(this.filesFullNames.GetRange(startIndex, divideBy), charSequenceToRemoveFromRecord));
        }

        
        var whenAll = tasksArray.WhenAllEx(
            _ =>
            {
                Console.WriteLine(
                    $"Чтение файлов: {((double) _.Count(task => task.IsCompleted) / tasksArray.Length) * 100}% завершено");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            });
        
        whenAll.Wait();


        foreach (var task in tasksArray)
        {
            this.CustomFiles.AddRange(task.Result);
        }

        var composedCustomFileFullName = saveAllCustomFilesToOneFile();
        Console.WriteLine($"Все файлы ({CustomFiles.Count}) успешно объединены в один файл по пути {composedCustomFileFullName}. Строк с заданным сочетанием символов {charSequenceToRemoveFromRecord} удалено: {linesRemovedCounter}");
        return composedCustomFileFullName;
    }

    private List<CustomFile> batchReadCustomFiles(List<string> filesFullNames, string? charSequenceToRemoveFromRecord = null)
    {
        List<CustomFile> outputCustomFilesList = new List<CustomFile>();
        foreach (var fileFullName in filesFullNames)
        {
            outputCustomFilesList.Add(readCustomFile(fileFullName, charSequenceToRemoveFromRecord));
        }
        return outputCustomFilesList;
    }

    private CustomFile readCustomFile(string fileFullName, string? charSequenceToRemoveFromRecord = null)
    {
        CustomFile outputCustomFile = new CustomFile();
        using (var file = File.OpenText(fileFullName))
        {
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                if (line.Contains(charSequenceToRemoveFromRecord))
                {
                    incrementLinesRemovedCounter();
                    continue;
                }
                
                var splittedData = file.ReadLine()?.Split("||");
                if (splittedData is null)
                {
                    break;
                }
                
                if (splittedData.Length != 6)
                {
                    throw new Exception("Строка в файле в неверном формате!");
                }

                if (outputCustomFile.CustomRecordArray is null)
                {
                    outputCustomFile.CustomRecordArray = new List<CustomRecord5>();
                }
                outputCustomFile.CustomRecordArray.Add(new CustomRecord5
                {
                    RandomDate = DateOnly.FromDateTime(DateTime.Parse(splittedData[0])),
                    RandomLatinString = splittedData[1],
                    RandomRussianString = splittedData[2],
                    RandomPositiveEvenInteger = int.Parse(splittedData[3]),
                    RandomPositiveDouble = double.Parse(splittedData[4])
                });
            }
        }
        return outputCustomFile;
    }

    private void incrementLinesRemovedCounter()
    {
        lock (locker)
        {
            linesRemovedCounter += 1;
        }
    }
    
    private string saveAllCustomFilesToOneFile()
    {
        var composedCustomFileFullName = Path.ChangeExtension(Path.Combine(this.FolderPath, Path.GetRandomFileName()),
            ".txt");
        using (var file = File.CreateText(composedCustomFileFullName))
        {
            foreach (var customFile in this.CustomFiles)
            {
                foreach (var customRecord in customFile.CustomRecordArray)
                {
                    file.WriteLine($"{customRecord.RandomDate}{Separator}{customRecord.RandomLatinString}{Separator}{customRecord.RandomRussianString}{Separator}{customRecord.RandomPositiveEvenInteger}{Separator}{customRecord.RandomPositiveDouble}{Separator}");
                }
            }
        }
        return composedCustomFileFullName;
    }

    private void getAllFileNamesInFolder()
    {
        this.filesFullNames = Directory.GetFiles(this.FolderPath, "*.txt").ToList();
    }
}
