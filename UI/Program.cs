using Database;
using UI.Areas.Identity;
using UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Services;
using UnitOfWorkSpace;
using DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// ������������ ���������� ������
builder.Services.AddControllersWithViews();
builder.Services.AddUnitOfWork();
builder.Services.AddServices();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;

})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<VacancyService>();
builder.Services.AddScoped<ApplicationService>();

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

var app = builder.Build();

// ������������ ����� HTTP ������
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.MapRazorPages();

app.Run();

