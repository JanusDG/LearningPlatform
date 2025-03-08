namespace LearningPlatform.Models
{
    public class User : BaseEntityIdValue<string>
    {
        public ICollection<UserCourse> UserCourses { get; set; }= new List<UserCourse>();
    }
}