using ECommercePlatform.API.Startup.Extensions;
using ECommercePlatform.API.Startup.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddValidationExtensions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configs
builder.Services.AddApplicationServices()
                .AddDatabase(builder.Configuration)
                .AddDefaultCors(builder.Configuration)
                .AddJwtAuth(builder.Configuration)
                .AddAutoMapperProfiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiResponseMiddleware>();

app.UseHttpsRedirection();

app.UseDefaultCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
