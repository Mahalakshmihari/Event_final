using EventManagement_Backend.Models;
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EventManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("")));
//var configuration = builder.Configuration;
builder.Services.AddHttpClient<IReviewService, ReviewService>();
builder.Services.AddHttpClient<IEventService, EventService>();
builder.Services.AddHttpClient<IBookingService, BookingService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<IProfileService, ProfileService>();
//builder.Services.AddHttpClient<ITicketService, TicketService>();
//builder.Services.AddSingleton<IConfiguration>(configuration);
//builder.Services.AddScoped<ICategoryService, CategoryService>();

Console.WriteLine($"Environment Variable - BaseUrl: {Environment.GetEnvironmentVariable("CatUrl")}");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=EventAppPage}/{id?}");

app.Run();
