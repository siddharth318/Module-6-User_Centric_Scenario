using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
string[] scope = new string[] { "https://storage.azure.com/user_impersonation" };

builder.Services.AddControllersWithViews();


//registering this service
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAD")
    .EnableTokenAcquisitionToCallDownstreamApi(scope)
    .AddInMemoryTokenCaches();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenario   s, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
