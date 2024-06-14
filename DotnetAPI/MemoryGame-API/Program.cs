using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext with MySQL
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MemorygameContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Add CORS services to allow all origins
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policyBuilder => policyBuilder  .AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();            

            app.MapControllers();

            app.Run();
        }
    }
}
