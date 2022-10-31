namespace OperationCHAN;

public class Timeedit
{
    public async void StartLoop()
    {
        await Task.Run(() => GetDataLoop());
    }

    void GetDataLoop()
    {
        while (true)
        {
            Console.WriteLine("Test");
            Thread.Sleep(2000);
        }
    }
}