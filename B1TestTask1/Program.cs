// See https://aka.ms/new-console-template for more information

using B1TestTask1.Helpers;

try
{
    char consoleKey;
    while (true)
    {
        Console.WriteLine(@"Выберите действие:
1. Сгенерировать файлы
2. Объединить файлы в один
3. Импортировать файлы в БД");

        if ((consoleKey = Console.ReadKey().KeyChar) != '0')
        {
            switch (consoleKey)
            {
                case '1':
                    Console.WriteLine("\nСколько файлов сгенерировать? (100 - Enter)");
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
                    Console.WriteLine($"\nУкажите путь до папки с файлами ({customFilesJoiner.FolderPath} - Enter)");
                    int customFile;
                    if (int.TryParse(Console.ReadLine(), out customFile))
                    {
                        var test = new CustomFilesGenerator().BatchGenerateAndSaveCustomFilesInParallel(customFile);
                    }
                    else
                    {
                        var test = new CustomFilesGenerator().BatchGenerateAndSaveCustomFilesInParallel();
                    }
                    break;
                case '3':
                    break;
            }
        }
        else
        {
            break;
        }
    }
}
catch (Exception e)
{
    
}
