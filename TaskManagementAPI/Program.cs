using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI
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

            //Whenever someone asks for ITaskService give them InMemoryTaskService
            builder.Services.AddScoped<ITaskService, InMemoryTaskService>();

            builder.Services.Configure<TaskServiceOptions>(
                builder.Configuration.GetSection("TaskServiceOptions"));

            builder.Services.AddHealthChecks();
            builder.Services.AddHealthChecks().AddCheck<TaskServiceHealthCheck>("taskService");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=task.db"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!db.Tasks.Any())
                {
                    db.Tasks.AddRange(
                        new TaskItem { Name = "database" },
                        new TaskItem { Name = "cpu" }

                        );
                    db.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<RequestTimingMiddleware>();

            app.MapHealthChecks("/health");

            app.MapControllers();

            app.Run();

        }
    }
}
