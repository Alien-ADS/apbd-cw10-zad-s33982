using Cwiczenia_10.Data;
using Cwiczenia_10.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(opt => {
        opt.SwaggerEndpoint("/openapi/v1.json", "Cwiczenia 10");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();