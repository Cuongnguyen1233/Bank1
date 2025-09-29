using Microsoft.EntityFrameworkCore;
using Bank1.Data;
using Bank1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("https://localhost:5000", "http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Seed sample data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
    DataSeeder.SeedData(context);
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowClient");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

// API endpoints to view sample data
app.MapGet("/api/accounts", (BankDbContext context) =>
{
    return context.Accounts.ToList();
});

app.MapGet("/api/transactions", (BankDbContext context) =>
{
    return context.Transactions.ToList();
});

app.MapGet("/api/transactions/{accountName}", (string accountName, BankDbContext context) =>
{
    return context.Transactions.Where(t => t.AccountName == accountName).ToList();
});

// API endpoints only

// Login API endpoint
app.MapPost("/api/login", (LoginRequest request, BankDbContext context) =>
{
    var account = context.Accounts.FirstOrDefault(a => a.AccountName == request.AccountName && a.Pincode == request.Pincode);
    
    if (account == null)
    {
        return Results.BadRequest(new { message = "Tên tài khoản hoặc mã PIN không đúng" });
    }
    
    return Results.Ok(new { accountName = account.AccountName, pincode = account.Pincode });
});

// Transaction API endpoint
app.MapPost("/api/transaction", (TransactionRequest request, BankDbContext context) =>
{
    var account = context.Accounts.FirstOrDefault(a => a.AccountName == request.AccountName);
    if (account == null)
    {
        return Results.BadRequest(new { message = "Tài khoản không tồn tại" });
    }
    
    // Get current balance
    var latestTransaction = context.Transactions
        .Where(t => t.AccountName == request.AccountName)
        .OrderByDescending(t => t.Id)
        .FirstOrDefault();
    
    decimal currentBalance = latestTransaction?.Balance ?? 0;
    decimal newBalance = currentBalance;
    
    // Calculate new balance
    if (request.TransactionType == "deposit")
    {
        newBalance = currentBalance + request.Amount;
    }
    else if (request.TransactionType == "withdraw")
    {
        if (request.Amount > currentBalance)
        {
            return Results.BadRequest(new { message = "Số dư không đủ để thực hiện giao dịch" });
        }
        newBalance = currentBalance - request.Amount;
    }
    
    // Create new transaction record
    var transaction = new Transaction
    {
        TransDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        Withdraw = request.TransactionType == "withdraw" ? request.Amount : 0,
        Deposit = request.TransactionType == "deposit" ? request.Amount : 0,
        Balance = newBalance,
        AccountName = request.AccountName
    };
    
    context.Transactions.Add(transaction);
    context.SaveChanges();
    
    return Results.Ok(new { 
        message = "Giao dịch thành công", 
        newBalance = newBalance,
        transactionId = transaction.Id 
    });
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

