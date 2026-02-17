using Microsoft.AspNetCore.HttpOverrides;
using ConsoleUI.Library.Api;
using ConsoleUI.Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IApiHelper, ApiHelper>();
builder.Services.AddSingleton<IRegistrationEndpoint, RegistrationEndpoint>();
builder.Services.AddTransient<IRegistrationNumberModel, RegistrationNumberModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.Use((context, next) =>
{
    context.Request.Scheme = "https";
	return next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
