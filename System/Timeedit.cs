using System.Diagnostics;
using System.Net;
using System.Reflection;
using Ical.Net;
using Ical.Net.CalendarComponents;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN;

public class Timeedit
{
    private ApplicationDbContext _db;
    public Timeedit(ApplicationDbContext db)
    {
        _db = db;
    }
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

        for (int i = 0; i < calendar.Events.Count; i++)
        {
            CalendarEvent e = new CalendarEvent();
            e = calendar.Events[i];
            String fag = e.Summary;
            if (fag.ToLower().Contains("lab"))
            {
                String name = e.Summary;
                String room = e.Location;
                DateTime date = e.DtStart.Date;
                DateTime begin = e.DtStart.Date;
                DateTime end = e.DtEnd.Date;
                
                
                _db.Courses.Add(new CourseModel(name));
                await _db.SaveChangesAsync();
                Console.WriteLine("Here:" + name);
            }
            Console.WriteLine("Here:After");
        }
    }
}