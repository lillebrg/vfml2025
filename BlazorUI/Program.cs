using BlazorUI;
using ChatBot.Interfaces;
using ChatBot.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NLG;
using NLU;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IDialogManager, DialogManager>();
builder.Services.AddScoped<INLGRepository, NLGRepository>();
builder.Services.AddScoped<INLURepository, NLURepository>();
builder.Services.AddScoped<IFAQRepository, FAQRepository>();
builder.Services.AddScoped<IEmergencyRespository, EmergencyRepository>();


await builder.Build().RunAsync();
