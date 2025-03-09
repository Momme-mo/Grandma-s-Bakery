using eshop.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
builder.Services.AddDbContext<DataContext>(options =>
{
  // options.UseSqlServer(builder.Configuration.GetConnectionString("Prod"));
  // options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
  options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddControllers();

// Lägg till stöd för swagger...
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Pipeline...
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
  var context = services.GetRequiredService<DataContext>();

  await context.Database.MigrateAsync();
  await Seed.LoadAddressTypes(context);
  await Seed.LoadProducts(context);
  await Seed.LoadCustomerOrders(context);
  await Seed.LoadOrderItems(context);
}
catch (Exception ex)
{
  Console.WriteLine("{0}", ex.Message);
  throw;
}

app.MapControllers();

app.Run();