using System.Text;
using AssetwiseApi.Context;
using AssetwiseApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<AWContext>((serviceProvider, dbContextOptions) =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    //add 
    var dbName = httpContextAccessor?.HttpContext?.Request.Headers["BUID"].First();
    var dbString = "";
    switch (dbName){
        case "NS":
            dbString = "NS";
            break;
        case "BurgerKing":
            dbString = "BK";
            break;
        case "McDonalds":
            dbString = "MCD";
            break;
    }
    dbContextOptions.UseMySql(builder.Configuration.GetConnectionString(dbString),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString(dbString)));
});

var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

builder.Services.AddIdentity<User, IdentityRole>(options =>
    options.User.AllowedUserNameCharacters += " ")
           .AddEntityFrameworkStores<AWContext>()
           .AddDefaultTokenProviders()
           .AddRoles<IdentityRole>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";   
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                      });
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.Configure<FormOptions>(o =>
{
    //Alter to an actual limit in prod
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
  //AddConnectionStringForEachCompany...
  /**Eg:
  *website : assetwise/login/burgerking
  *Api Url: user/login/burgerking
  *BurgerKing: "server=localhost;user=newuser;password=v1nt@ge2022;port=3306;database=BurgerKing"
  */
app.UseStaticFiles(new StaticFileOptions()
{   
    //Check if file directory exists to avoid production/deployment errors
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
