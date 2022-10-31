using System.Diagnostics;

namespace OperationCHAN;

public class Timeedit
{
    public async void StartLoop()
    {
        await Task.Run(() => GetDataLoop());
    }

    public void GetData()
    {
        RunPython();
    }

    void GetDataLoop()
    {
        while (true)
        {
            Console.WriteLine("Test");
            Thread.Sleep(2000);
        }
    }

    void RunPython()
    {
        String path =
            "C:/Users/nikol/OneDrive/SKULE/Universitet/Semester 3/IKT201-G Internettjenester/Prosjekt/chan/System/Script/timeedit.py";
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "my/full/path/to/python.exe";
        start.Arguments = string.Format(path);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using(Process process = Process.Start(start))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);
            }
        }
    }
}