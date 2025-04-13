using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.Models
{

    public class LessonEntityModel
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;} = default!;
        public string? Description {get; set;}
    }

}