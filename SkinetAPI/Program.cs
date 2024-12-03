using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SkinetAPI.Data;
using SkinetAPI.Extensions;
using SkinetAPI.Helpers;
using SkinetAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddEndpointsApiExplorer();



{
    var app = builder.Build();


    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseStatusCodePagesWithReExecute("/errors/{0}");

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseStaticFiles();

    app.UseAuthorization();

    app.UseSwaggerDocumentation();

    app.MapControllers();

    app.Run();
}

