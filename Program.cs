using E_proc.Models;
using E_proc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using E_proc;
using System.Text.Json;
using E_proc.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();



var options = new DbContextOptionsBuilder<AuthContext>()
             .UseSqlServer(config.GetConnectionString("EprocDB"))
             .Options;
builder.Services.AddDbContext<AuthContext>(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.UseSqlServer(
        config.GetConnectionString("EprocDB"));
}

);
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>

    {


        options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });


using var db = new AuthContext(options);
//db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
//app.MapPost("/user", (UserLogin user, IUserService service) => AddUser(user, service));


//app.MapPost("/login", (UserLogin user, IUserService service) => Login(user, service));

//IResult AddUser(UserLogin user, IUserService service)
//{
//    var addUser = service.Post(db);
//    return Results.Ok(addUser);

//}
//IResult Login(UserLogin user, IUserService service)
//{
//}
app.Run();
