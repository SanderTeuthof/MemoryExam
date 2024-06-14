using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Models
{
    public class MemorygameContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MemorygameContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<ResetGame> ResetGame { get; set; }  // Ensure this is defined correctly
        public DbSet<Names> Names { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .ToTable("memoryimages")
                .HasIndex(i => i.ImageName)
                .IsUnique();

            modelBuilder.Entity<Image>()
                .Property(i => i.Id)
                .HasColumnName("id");

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageName)
                .HasColumnName("image_name")
                .HasMaxLength(45);

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageData)
                .HasColumnName("image_data");

            modelBuilder.Entity<ResetGame>()
                .ToTable("resetgame")
                .Property(r => r.Id)
                .HasColumnName("id");

            modelBuilder.Entity<ResetGame>()
                .Property(r => r.ResetTime)
                .HasColumnName("reset_time");

            modelBuilder.Entity<ResetGame>()
                .Property(r => r.ResetPlayer)
                .HasColumnName("reset_player")
                .HasMaxLength(100);

            modelBuilder.Entity<Names>()
                .ToTable("names")
                .Property(n => n.Id)
                .HasColumnName("id");

            modelBuilder.Entity<Names>()
                .Property(n => n.Name1)
                .HasColumnName("name1");

            modelBuilder.Entity<Names>()
                .Property(n => n.Name2)
                .HasColumnName("name2");

        }
    }
}
