using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.Models
{

    public class LessonViewModel
    {
        [Required]
        public string Name {get; set;} = default!;
        public string? Description {get; set;}
    }

}