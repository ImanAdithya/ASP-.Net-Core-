using DefaultNamespace;
using WebApplication1.Data;


var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGenreEndPoint();
app.MapGamesEndpoints();

await app.MigrateDbAsync();

app.Run();