// <copyright file="Program.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using Zearain.AoC23.Frontend;
using Zearain.AoC23.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
var adventDayUrl = builder.Configuration.GetValue<string>("AdventDayUrl");
builder.Services.AddHttpClient<AdventDayHttpClient>(client => client.BaseAddress = new Uri(adventDayUrl ?? builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();