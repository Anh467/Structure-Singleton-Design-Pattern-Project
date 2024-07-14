using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ***
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IRepository<Enrollment>, EnrollmentRepository>();

// ***
builder.Services.AddControllers().AddOData(option => option.Select().Filter().OrderBy().Expand());
var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connectionString);
});

// ***
builder
    .Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var app = builder.Build();
// ***
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
