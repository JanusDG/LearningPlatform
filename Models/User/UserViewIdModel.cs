namespace LearningPlatform.Models
{
    public class UserViewIdModel
    {
        public int Id {get; set;}
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public string Username {get; set;} = default!;
        public string Firstname {get; set;} = default!;
        public string Surname {get; set;} = default!;
        public string Email {get; set;} = default!;
    }
}