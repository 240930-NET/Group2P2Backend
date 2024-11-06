using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MoviesP2.API.Services;
using MoviesP2.Data;
using MoviesP2.Data.Repos;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API Documentation",
            Version = "v1.0",
            Description = ""
        });
        options.ResolveConflictingActions(x => x.First());
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            BearerFormat = "JWT",
            Flows = new OpenApiOAuthFlows
            {
                Implicit  = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
                    AuthorizationUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/authorize?audience={builder.Configuration["Auth0:Audience"]}"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenId" },       
                    }
                }
            }
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                },
                new[] { "openid" }
            }
        });

    });

string connectionString = builder.Configuration["ConnectionString"]!; 
//set up DbContext
builder.Services.AddDbContext<MoviesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("P2")));
//Repos
builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IMovieService, MovieService>();



builder.Services.AddCors(options =>
{
    /*if(builder.Environment.IsDevelopment()) {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(
                builder.Configuration["Auth0:ClientOriginUrl"]!, builder.Configuration["Auth0:SwaggerOriginUrl"]!)
                .WithHeaders([
                    HeaderNames.ContentType,
                    HeaderNames.Authorization,
                ])
                .AllowAnyMethod()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
        });
    }
    else {*/
         options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(
                builder.Configuration["Auth0:ClientOriginUrl"]!)
                .WithHeaders([
                    HeaderNames.ContentType,
                    HeaderNames.Authorization,
                ])
                .AllowAnyMethod()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
        });
    //}
    options.AddPolicy("TestingOnly", policy =>
    {
        policy.WithOrigins(builder.Configuration["Auth0:SwaggerOriginURL"]!)
            .WithHeaders([
                HeaderNames.ContentType,
                HeaderNames.Authorization,
            ])
            .AllowAnyMethod()
            .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
    });
    options.AddPolicy("TestingOnly", policy =>
    {
        policy.WithOrigins("*")
            .WithHeaders([
                HeaderNames.ContentType,
                HeaderNames.Authorization,
            ])
            .AllowAnyMethod()
            .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{   
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAuthentication(options => 
{ 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
}).AddJwtBearer(options => 
{ 
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}"; 
    options.Audience = builder.Configuration["Auth0:Audience"]; 
    options.TokenValidationParameters = new TokenValidationParameters { 
        ValidateIssuer = true, 
        ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}", 
        ValidateAudience = true, ValidAudience = builder.Configuration["Auth0:Audience"], 
        ValidateLifetime = true 
    }; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
      settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
      settings.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
      settings.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
      settings.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => {
    return builder.Configuration["Auth0:ClientOriginUrl"];
});

app.Run();