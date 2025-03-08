using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class BaseEntityIdValue<T>{
        [Key]
        public int Id { get; set; } 
        [Required]
        public T Value { get; set; } = default!;
    }

}