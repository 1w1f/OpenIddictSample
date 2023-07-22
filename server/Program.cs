using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Quartz;
using server;
using server.Data;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<ClientHostService>();


#region identity

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region 配置QuartZ定时任务

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});
builder.Services.AddQuartzHostedService(Options => Options.WaitForJobsToComplete = true);

#endregion

#region openIddict 配置

builder.Services.AddDbContext<AppDbContext>(dbContextOptions =>
{
    dbContextOptions.UseSqlite($"FileName=.//Db//Database.db", builder => builder.MigrationsAssembly("server"));
    dbContextOptions.UseOpenIddict();
});

builder.Services.AddOpenIddict().AddCore(options => { options.UseEntityFrameworkCore().UseDbContext<AppDbContext>(); })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("authorize");
        options.SetIntrospectionEndpointUris("introspect");
        options.SetTokenEndpointUris("token");

        options.AllowAuthorizationCodeFlow();
        options.AllowRefreshTokenFlow();
        options.AddEncryptionKey(
            new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
        options.AddDevelopmentSigningCertificate();
        options.UseAspNetCore().EnableAuthorizationEndpointPassthrough();
    }).AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

#endregion


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(p => p.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:5555"));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();