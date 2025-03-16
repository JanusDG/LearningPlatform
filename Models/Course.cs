namespace LearningPlatform.Models
{

    public class Course : BaseEntityIdValue<string>
    {
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }

}