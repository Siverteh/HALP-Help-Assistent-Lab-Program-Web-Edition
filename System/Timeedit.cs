using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Microsoft.EntityFrameworkCore;
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
    public async Task StartLoop(int sleepTime = 60*60*24)
    {
        await Task.Run(() => GetDataLoop(sleepTime * 1000));
    }

    /**
     * Starts the manual call to TimeEdit to get retrieve desired data
     */
    public async Task GetData()
    {
        await GetDataFromTimeEdit();
    }

    /**
     * Private method that invokes the update method for TimeEdit regularly
     */
    private async Task GetDataLoop(int sleepTime)
    {
        while (true)
        {
            await GetDataFromTimeEdit();
            Thread.Sleep(sleepTime);
        }
    }
    
    /**
     * Makes a call to TimeEdit, gets the .ics file,
     * and puts the data into the database
     */
    private async Task GetDataFromTimeEdit()
    {
        await EmptyTable();
        
        using HttpResponseMessage response = await client.GetAsync(
                "https://cloud.timeedit.net/uia/web/tp/ri15667y6Z0655Q097QQY656Z067057Q469W95.ics");
        
        string responseBody = await response.Content.ReadAsStringAsync();

        var calendar = Calendar.Load(responseBody);

        for (int i = 0; i < calendar.Events.Count; i++)
        {
            CalendarEvent e = new CalendarEvent();
            e = calendar.Events[i];
            string fag = e.Summary;
            if (fag.ToLower().Contains("lab") || fag.ToLower().Contains("øving"))
            {
                string courseCode = GetCourseCode(e.Summary);
                string[] rooms = ProcessRooms(e.Location);
                var begin = e.DtStart.Value;
                var end = e.DtEnd.Value;
                await _db.Courses.AddAsync(new CourseModel(courseCode, end, begin, rooms));
            }
        }
        await _db.SaveChangesAsync();
    }

    /**
     * Extract the course code from the string retrieved from TimeEdit
     */
    private string GetCourseCode(string course)
    {
        var code = course.Split(',');
        return code[0];
    }

    /**
     * Turn the string of given rooms into an array
     */
    private string[] ProcessRooms(string rooms)
    {
        var courseRooms = rooms.Split(',', '/');

        for (int i = 0; i < courseRooms.Length; i++)
        {
            if (courseRooms[i].Contains("GRM"))
            {
                if (courseRooms[i][0] == ' ')
                {
                    courseRooms[i] = courseRooms[i].Remove(0, 1);
                }
            }
            else
            {
                var tmp = courseRooms[i-1].Substring(0, 7);
                courseRooms[i] = tmp + courseRooms[i];
            }
        }

        return courseRooms;
    }

    /**
     * Empty the table so it is ready for new TimeEdit data
     */
    private async Task EmptyTable()
    {
        _db.Courses.RemoveRange(_db.Courses.ToList());
        await _db.Database.ExecuteSqlRawAsync("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Courses';");
        await _db.SaveChangesAsync();
    }

}