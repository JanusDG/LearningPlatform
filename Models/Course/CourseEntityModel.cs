using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.Models
{

    public class CourseEntityModel
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        [Required]
        public string Name {get; set;} = default!;
        public string? Description {get; set;}
        // TODO: consider many to many relation between lessons and courses
        public ICollection<LessonEntityModel>? Lessons {get; set;}
    }

}