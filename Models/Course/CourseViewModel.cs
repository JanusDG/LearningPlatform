namespace LearningPlatform.Models
{

    public class CourseViewModel 
    {
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public string Name {get; set;} = default!;
        public string? Description {get; set;}
        public ICollection<LessonViewModel>? Lessons {get; set;}
    }

}