
using StockBook.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<StockBookDataBaseSettings>(
    builder.Configuration.GetSection("StockBookDatabase"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
