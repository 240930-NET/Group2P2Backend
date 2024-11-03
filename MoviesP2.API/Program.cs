using Microsoft.EntityFrameworkCore;
using MoviesP2.Data;
using MoviesP2.Data.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration["ConnectionString"]!; 
//set up DbContext
builder.Services.AddDbContext<MoviesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("P2")));

builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();
//builder.Services.AddScoped<IMovieRepo, MovieRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.Run();

