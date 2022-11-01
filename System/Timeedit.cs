using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ical.Net;
using Ical.Net.CalendarComponents;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN;

public class Timeedit
{
    private ApplicationDbContext _db;
    static readonly HttpClient client = new HttpClient();
    
    /**
     * Initialize the TimeEdit class
     */
    public Timeedit(ApplicationDbContext db)
    {
        _db = db;
    }
    
    /**
     * Starts the loop to get data from TimeEdit
     */
    public async void StartLoop()
    {
        await Task.Run(() => GetDataLoop());
    }

    /**
     * Starts the manual call to TimeEdit to get retrieve desired data
     */
    public void GetData()
    {
        GetDataFromTimeEdit();
    }

    /**
     * Private method that invokes the update method for TimeEdit regularly
     */
    void GetDataLoop()
    {
        while (true)
        {
            GetDataFromTimeEdit();
            Thread.Sleep(2000);
        }
    }
    
    /**
     * Makes a call to TimeEdit, gets the .ics file,
     * and puts the data into the database
     */
    public async Task GetDataFromTimeEdit() 
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
            if (fag.ToLower().Contains("lab") || fag.ToLower().Contains("øving"))
            {
                String name = e.Summary;
                String rooms = e.Location;
                var begin = e.DtStart.Value;
                var end = e.DtEnd.Value;
                

                Console.WriteLine("Here:" + name);
                await _db.Courses.AddAsync(new CourseModel(name, end, begin, rooms));
            }
            Console.WriteLine("Here:After:" + i);
        }
        await _db.SaveChangesAsync();
    }
}