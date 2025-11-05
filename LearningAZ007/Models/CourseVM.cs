namespace LearningAZ007.Models;

public class CourseVM
{
    public List<Course> CourseList { get; set; }
}
public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int Rating { get; set; }
}
