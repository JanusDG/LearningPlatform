namespace LearningPlatform.Models
{
    public class UserViewModel
    {
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public string Username {get; set;} = default!;
        public string Role {get; set;} = default!;
        public string Firstname {get; set;} = default!;
        public string Surname {get; set;} = default!;
        public string Email {get; set;} = default!;
        
    }
}