using Microsoft.EntityFrameworkCore;
using NeatBurger.Models.Entities;
using NeatBurger.Repositories;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<Repository<Clasificacion>>();
builder.Services.AddTransient<MenuRepository>();
builder.Services.AddDbContext<NeatContext>(
    x => x.UseMySql("server=localhost;user=root;password=root;database=neat", 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.17-mysql"))
);

builder.Services.AddMvc();
var app = builder.Build();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.UseFileServer();
app.MapDefaultControllerRoute();
app.Run();
