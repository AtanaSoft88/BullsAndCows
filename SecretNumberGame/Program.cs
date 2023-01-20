using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data;
using SecretNumberGame.Data.Constants;
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
    
}).AddRoles<IdentityRole<Guid>>()
  .AddEntityFrameworkStores<SecretDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/User/Login";    
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(GlobalConstants.MY_POLICY, policy =>
    policy.RequireAssertion(context =>
                            context.User.IsInRole(GlobalConstants.ADMIN) &&
                            context.User.IsInRole(GlobalConstants.PLAYER)));
});

builder.Services.AddScoped<ISecretNumberService, SecretNumberService>();
builder.Services.AddScoped<ISaveResultService, SaveResultService>();
builder.Services.AddScoped<IScoreBoardService, ScoreBoardService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddNotyf(conf => 
{
    conf.DurationInSeconds = 10;
    conf.IsDismissable = true;
    conf.Position = NotyfPosition.TopCenter;
});
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
app.UseNotyf();
app.MapRazorPages();

app.Run();
