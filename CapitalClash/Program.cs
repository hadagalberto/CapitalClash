
using CapitalClash.Hubs;
using CapitalClash.Models;
using CapitalClash.Services;
using MediatR;
using System.Reflection;

namespace CapitalClash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000", "http://localhost", "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton<GameService>();
            builder.Services.AddSingleton<GameStore>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseCors();

            app.UseRouting();

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<GameHub>("/gamehub");

            app.MapPost("/rooms", (GameStore store) =>
            {
                var room = new GameRoom();
                store.Rooms[room.RoomCode] = room;
                return Results.Ok(room.RoomCode);
            });

            app.Run();
        }
    }
}
