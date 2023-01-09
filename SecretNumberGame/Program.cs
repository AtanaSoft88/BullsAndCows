using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data;
using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services;
using SecretNumberGame.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SecretDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    //options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<SecretDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/User/Login";    
});

builder.Services.AddScoped<ISecretNumberService, SecretNumberService>();
builder.Services.AddScoped<ISaveResultService, SaveResultService>();
builder.Services.AddScoped<IScoreBoardService, ScoreBoardService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
