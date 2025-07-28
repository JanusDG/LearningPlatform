using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.Models
{

    public class LessonViewIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description {get; set;}
    }

}