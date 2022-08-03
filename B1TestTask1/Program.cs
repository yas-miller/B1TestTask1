using B1TestTask1.Helpers;
using B1TestTask1.Models.Services;

char consoleKey;
while (true)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine(@"Выберите действие:
1. Сгенерировать файлы
2. Объединить файлы в один
3. Импортировать файлы в БД
0. Завершить программу");

    if ((consoleKey = Console.ReadKey().KeyChar) != '0')
    {
        try
        {
            switch (consoleKey)
            {
                case '1':
                    Console.WriteLine("\nСколько файлов сгенерировать? (100 - нажмите Enter)");
                    int customFilesCount;
                    if (int.TryParse(Console.ReadLine(), out customFilesCount))
                    {
                        var test = new CustomFilesGenerator().BatchGenerateAndSaveCustomFilesInParallel(customFilesCount);
                    }
                    else
                    {
                        var test = new CustomFilesGenerator().BatchGenerateAndSaveCustomFilesInParallel();
                    }
                    break;
                case '2':
                    var customFilesJoiner = new CustomFilesJoiner();
                    Console.WriteLine($"\nУкажите путь до папки с файлами ({customFilesJoiner.FolderPath} - нажмите Enter)");
                    string? filesFolderPathToJoin = Console.ReadLine();
                    Console.WriteLine("Удалить последовательность символов? Если да, укажите требуемые сочетания символов (Нет - нажмите Enter)");
                    string? charSequenceToRemove = Console.ReadLine();
                    customFilesJoiner.joinCustomFilesAndSaveToOneFile(filesFolderPathToJoin == "" ? null : filesFolderPathToJoin, 
                        charSequenceToRemove == "" ? null : charSequenceToRemove);
                    break;
                case '3':
                    var customFilesReader = new CustomFilesReader();
                    Console.WriteLine($"\nУкажите путь до папки с файлами ({customFilesReader.FolderPath} - нажмите Enter)");
                    string? filesFolderPathToRead = Console.ReadLine();
                    customFilesReader.batchReadCustomFilesInParallel(filesFolderPathToRead == "" ? null : filesFolderPathToRead);
                
                    CustomFilesService.instance.saveCustomFilesToDB(customFilesReader.CustomFiles);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nПроизошла ошибка {e.GetType().ToString()} - {e.Message}");
        }
    }
    else
    {
        break;
    }
}

