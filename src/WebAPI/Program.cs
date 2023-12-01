// <copyright file="Program.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application;
using Zearain.AoC23.Infrastructure;
using Zearain.AoC23.WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication(typeof(AdventDayHub).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<AdventDayHub>("/adventdayhub");

app.Run();