using LearningPlatform.Data;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Data.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with dependency injection using SQL Server
builder.Services.AddDbContext<LearningPlatformDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LearningPlatformDbContext") 
    ?? throw new InvalidOperationException("Connection string 'LearningPlatformDbContext' not found.")));

// add the user service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
