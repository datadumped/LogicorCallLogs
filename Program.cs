using Radzen;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;
using LogicorSupportCalls.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(o =>
{
    o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
});
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<LogicorSupportCalls.SQL2022_1033788_pnjService>();
builder.Services.AddDbContext<LogicorSupportCalls.Data.SQL2022_1033788_pnjContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL2022_1033788_pnjConnection"));
});

string serilogConnectionString = builder.Configuration.GetConnectionString("SerilogConnection");

Log.Logger = new LoggerConfiguration()
               .WriteTo.MSSqlServer(
                   connectionString: serilogConnectionString,
                   tableName: "LogicorLogs",
                   autoCreateSqlTable: true)
               .CreateLogger();

builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();