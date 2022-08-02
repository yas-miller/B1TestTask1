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
3. Импортировать файлы в БД
0. Завершить программу");

        if ((consoleKey = Console.ReadKey().KeyChar) != '0')
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
                    string? folderPath = Console.ReadLine();
                    Console.WriteLine("\nУдалить последовательность символов? Если да, укажите требуемые сочетания символов (Нет - нажмите Enter)");
                    string? charSequenceToRemove = Console.ReadLine();
                    customFilesJoiner.batchJoinCustomFilesInParallel(folderPath, charSequenceToRemove);
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
