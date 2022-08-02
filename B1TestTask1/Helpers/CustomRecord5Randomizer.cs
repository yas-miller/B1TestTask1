namespace B1TestTask1.Models;

public class CustomRecord5Randomizer
{
    private static CustomRecord5 GetRandomCustomRecord()
    {
        return new CustomRecord5
        {
            RandomDate = Helpers.RandomDate.GetRandomDateOnly(),
            RandomLatinString = Helpers.RandomLatinString.GetRandomLatinString(),
            RandomRussianString = Helpers.RandomRussianString.GetRandomRussianString(),
            RandomPositiveEvenInteger = Helpers.RandomPositiveEvenInteger.GetRandomPositiveEvenInteger(),
            RandomPositiveDouble = Helpers.RandomPositiveDouble.GetRandomPositiveDouble()
        };
    }

    public static List<CustomRecord5> BatchGetRandomCustomRecordsInParallel(int count)
    {
        List<CustomRecord5> outputCustomRecordsList = new List<CustomRecord5>();

        int divideBy = count;
        while (divideBy % 2 == 0)
        {
            divideBy /= 2;
        }
        
        Task<List<CustomRecord5>>[] tasksArray = new Task<List<CustomRecord5>>[count / divideBy];
        for (int i = 0; i < count / divideBy; i++)
        {
            tasksArray[i] = Task.Run(() => BatchGetRandomCustomRecord(divideBy));
        }
        
        Task.WaitAll(tasksArray);
        
        foreach (var task in tasksArray)
        {
            outputCustomRecordsList.AddRange(task.Result);
        }

        return outputCustomRecordsList;
    }
    
    private static List<CustomRecord5> BatchGetRandomCustomRecord(int count)
    {
        List<CustomRecord5> outputCustomRecordsList = new List<CustomRecord5>();
        for (int i = 0; i < count; i++)
        {
            outputCustomRecordsList.Add(GetRandomCustomRecord());
        }
        return outputCustomRecordsList;
    }
}
