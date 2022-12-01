using Microsoft.Build.Framework;

namespace OperationCHAN.Models;

public class CourseLinksModel
{
    public CourseLinksModel()
    {
    }
    public CourseLinksModel(string courseLink)
    {
        CourseLink = courseLink;
    }

    public int Id { get; set; }

    [Required]
    public string CourseLink { get; set; } = String.Empty;
    

}