namespace MemoryGame.Data.Model
{
    public class DBImage
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[] Data { get; set; } = null!;
    }
}