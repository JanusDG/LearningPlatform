using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class UserEntityModel
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        [Required]
        public string Username {get; set;} = default!;
        [Required]
        public string Firstname {get; set;} = default!;
         [Required]
        public string Role {get; set;} = default!;
        [Required]
        public string Surname {get; set;} = default!;
        [Required]
        public string Email {get; set;} = default!;
        [Required]
        public string Password {get; set;} = default!;

    }
}