
using Bl.Interfaces;
using Bl.Repository;
using DAL.AppContext;
using Microsoft.EntityFrameworkCore;

namespace DemoIdentity
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
            builder.Services.AddDbContext<DemoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnetionToSql"))
            );

            builder.Services.AddScoped<IAppRole,AppRoleRepository>();
            builder.Services.AddScoped<IAppUser, AppUserRepository>();
            builder.Services.AddScoped<IAppUserAppRole, AppUserAppRoleRepository>();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
