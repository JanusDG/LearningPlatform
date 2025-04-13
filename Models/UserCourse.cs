namespace LearningPlatform.Models
{
    public class UserCourse
    {
        public int UserId { get; set; }
        public UserEntityModel User { get; set; }  = default!;

        public int CourseId { get; set; }
        public CourseEntityModel Course { get; set; }  = default!;
    }

}