namespace MemoryGame_API.Models
{
    public class ResetGame
    {
        public int Id { get; set; }
        public string ResetTime { get; set; } = string.Empty;  // Initialize non-nullable properties
        public string ResetPlayer { get; set; } = string.Empty;  // Initialize non-nullable properties
    }
}
