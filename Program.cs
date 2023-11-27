using Microsoft.EntityFrameworkCore;
using NeatBurger.Models.Entities;
using NeatBurger.Repositories;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddDbContext<NeatContext>(
    x => x.UseMySql("server=localhost;user=root;password=root;database=neat", 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.17-mysql"))
);

builder.Services.AddMvc();
var app = builder.Build();

app.UseFileServer();
app.MapDefaultControllerRoute();
app.Run();
