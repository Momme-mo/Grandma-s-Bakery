using eshop.api;
using eshop.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Skapa MySQL serverversion som variabel...
var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
// Knyta samman applikation med vår databas...
builder.Services.AddDbContext<DataContext>(options =>
{
  // options.UseSqlServer(builder.Configuration.GetConnectionString("Prod"));
  // options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
  options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

// Dependency injection...
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

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

  // await context.Database.MigrateAsync();
  await Seed.LoadProducts(context);
  await Seed.LoadSalesOrders(context);
  await Seed.LoadOrderItems(context);
  await Seed.LoadAddressTypes(context);
}
catch (Exception ex)
{
  Console.WriteLine("{0}", ex.Message);
  throw;
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();