using System.Diagnostics;
using System.Net;
using System.Reflection;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace OperationCHAN;

public class Timeedit
{
    public async void StartLoop()
    {
        await Task.Run(() => GetDataLoop());
    }

    public void GetData()
    {
        RunDotNet();
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
        var p = System.AppContext.BaseDirectory;
        String path = "/System/Script/timeedit.py";
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python.exe";
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

    static readonly HttpClient client = new HttpClient();
    async void RunDotNet()
    {
        using HttpResponseMessage response = await client.GetAsync(
                "https://cloud.timeedit.net/uia/web/tp/ri15667y6Z0655Q097QQY656Z067057Q469W95.ics");
        
        string responseBody = await response.Content.ReadAsStringAsync();

        var calendar = Calendar.Load(responseBody);

        var events = calendar.Events.ToList();
        
        for (int i = 0; i < calendar.Events.Count; i++)
        {
            Console.WriteLine(calendar.Events[i]);
        }
    }
}