namespace B1TestTask1.Extensions;

public static class TaskExtension
{
    public static async Task WhenAllEx(this ICollection<Task> tasks, Action<ICollection<Task>> reportProgressAction)
    {
        var whenAllTask = Task.WhenAll(tasks);
        for (; ; )
        {
            var timer = Task.Delay(1000); // 1s
            await Task.WhenAny(whenAllTask, timer);
            if (whenAllTask.IsCompleted)
            {
                return;
            }
            reportProgressAction(tasks);
        }
    }
}
