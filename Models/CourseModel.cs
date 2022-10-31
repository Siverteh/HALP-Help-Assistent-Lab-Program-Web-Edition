using Microsoft.Build.Framework;

namespace OperationCHAN.Models;

public class CourseModel
{
    public CourseModel(){}

    public CourseModel(string courseCode, DateTime courseEnd, DateTime courseStart, string courseRooms)
    {
        var code = courseCode.Split(',');
        CourseCode = code[0];
        
        CourseStart = courseStart;
        CourseEnd = courseEnd;

        var courseRoom = courseRooms.Split(',', '/');

        for (int i = 0; i < courseRoom.Length; i++)
        {
            if (courseRoom[i].Contains("GRM"))
            {
                if (courseRoom[i][0] == ' ')
                {
                    courseRoom[i] = courseRoom[i].Remove(0, 1);
                }
            }
            else
            {
                var tmp = courseRoom[i-1].Substring(0, 7);
                courseRoom[i] = tmp + courseRoom[i];
            }
        }
        
        CourseRoom1 = courseRoom[0];
        if (courseRoom.Length > 1) CourseRoom2 = courseRoom[1];
        if (courseRoom.Length > 2) CourseRoom3 = courseRoom[2];
        if (courseRoom.Length > 3) CourseRoom4 = courseRoom[3];
        
    }
    
    public int Id { get; set; }
    
    //[Required]
    public string CourseCode { get; set; } = String.Empty;
    
    [Required]
    public string CourseRoom1 { get; set; } = String.Empty;
    public string CourseRoom2 { get; set; } = String.Empty;
    public string CourseRoom3 { get; set; } = String.Empty;
    public string CourseRoom4 { get; set; } = String.Empty;
    
    
    //[Required]
    public DateTime CourseStart { get; set; }
    
    //[Required]
    public DateTime CourseEnd { get; set; }
}
