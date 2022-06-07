using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Validations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ChatAppDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ChatAppDatabaseSettings)));

builder.Services.AddSingleton<IChatAppDatabaseSettings>(
    sp=> sp.GetRequiredService<IOptions<ChatAppDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("ChatAppDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<ISignupService, SignupService>();

builder.Services.AddTransient<ISignupValidations, SignupValivations>();


builder.Services.AddControllers();
//builder.Validations.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
