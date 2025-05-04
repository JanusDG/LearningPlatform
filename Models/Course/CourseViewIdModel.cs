namespace LearningPlatform.Models
{

    public class CourseViewIdModel 
    {
        public int Id {get; set;}
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public string Name {get; set;} = default!;
        public string? Description {get; set;}
        public ICollection<LessonEntityModel>? Lessons {get; set;}
    }

}