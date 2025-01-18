using BankingSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connstring = builder.Configuration.GetConnectionString("BankingSystem");
builder.Services.AddSqlite<ApplicationDbContext>(connstring);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline and using Swagger for testing.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDb();

app.Run();
