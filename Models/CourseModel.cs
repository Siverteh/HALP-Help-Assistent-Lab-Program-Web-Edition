using Microsoft.Build.Framework;

namespace OperationCHAN.Models;

public class CourseModel
{
    public CourseModel(){}

    public CourseModel(string courseCode)
    {
        CourseCode = courseCode;
        //CourseStart = couseStart;
        //CourseEnd = courseEnd;
        //CourseRoom1 = courseRooms;
    }
    
    public int Id { get; set; }
    
    [Required]
    public string CourseCode { get; set; } = String.Empty;
    
    [Required]
    public string CourseRoom1 { get; set; } = String.Empty;
    public string CourseRoom2 { get; set; } = String.Empty;
    public string CourseRoom3 { get; set; } = String.Empty;
    public string CourseRoom4 { get; set; } = String.Empty;
    
    
    [Required]
    public DateTime CourseStart { get; set; }
    
    [Required]
    public DateTime CourseEnd { get; set; }
    
    public int Age { get; set; }
    
    
}
