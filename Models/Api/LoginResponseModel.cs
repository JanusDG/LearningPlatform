namespace LearningPlatform.Models
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        
    }
}