
using StockBook.Models;
using StockBook.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<StockBookDataBaseSettings>(
    builder.Configuration.GetSection("StockBookDatabase"));
builder.Services.AddSingleton<BooksService>();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.Run();