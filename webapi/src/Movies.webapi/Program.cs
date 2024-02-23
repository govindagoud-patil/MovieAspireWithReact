using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infrastructure;
using Movies.Application;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Movies.webapi.Extensions;
using Movies.webapi.Exceptions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAppLogging();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
    
          In = ParameterLocation.Header,
          Description="Please enter 'Bearer [jwt]'",
          Name="Authorization",
          Type=SecuritySchemeType.ApiKey
    
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference= new OpenApiReference {  Type= ReferenceType.SecurityScheme,Id="Bearer"     }
    };
    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme,Array.Empty<string>() } });

});

builder.Services.AddDbContext<MovieDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});


builder.Services.AddIdentityApiEndpoints<MovieUser>().AddEntityFrameworkStores<MovieDbContext>();

builder.Services.AddCors(opt => {

    opt.AddPolicy("AllowReactApp", opt => {
        opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();// WithOrigins("http://localhost:3001");
    });
    
    });
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGroup("api").MapIdentityApi<MovieUser>().WithTags("UserManagement");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.AddMovieEndpoints(false);

app.Run();
