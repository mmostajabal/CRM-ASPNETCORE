using CRMAPP.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CRMAPP.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using CRMAPP.DataAccess.Contracts.Customers;
using CRMAPP.DataAccess.Services.Customers;
using CRMAPP.DataAccess.mapping;
using CRMAPP.DataAccess.Contracts.Language;
using CRMAPP.DataAccess.Services.Language;
using CRMAPP.DataAccess.Contracts.Calls;
using CRMAPP.DataAccess.Services.Calls;
using FastReport.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("CRMConnection") ?? throw new InvalidOperationException("Connection string 'CheckAreaContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath= $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; 
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Maps));

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<ICustomer, CusromerSRV>();

builder.Services.AddTransient<ILanguage, LanguageSRV>();

builder.Services.AddScoped<ICalls, CallSRV>();
builder.Services.AddFastReport();
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseFastReport();
app.UseStaticFiles();
app.UseFastReport();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Employee}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
