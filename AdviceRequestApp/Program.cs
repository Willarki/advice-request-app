using AdviceRequestApp.Components;
using AdviceRequestApp.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
builder.Services.AddHttpClient<IODataService, ODataService>();
builder.Services.AddHttpClient<IFileService, FileService>();
builder.Services.AddHttpClient<IWorkflowService, WorkflowService>();
builder.Services.AddTransient<ILdapService, LdapService>();
builder.Services.AddScoped<FormStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
