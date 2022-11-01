using Microsoft.Build.Framework;

namespace OperationCHAN.Models;

public class CourseModel
{
    public CourseModel(){}

    public CourseModel(string courseCode, DateTime courseEnd, DateTime courseStart, string[] courseRooms)
    {
        CourseCode = courseCode;
        CourseStart = courseStart;
        CourseEnd = courseEnd;

        CourseRoom1 = courseRooms[0];
        if (courseRooms.Length > 1) CourseRoom2 = courseRooms[1];
        if (courseRooms.Length > 2) CourseRoom3 = courseRooms[2];
        if (courseRooms.Length > 3) CourseRoom4 = courseRooms[3];
        
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
