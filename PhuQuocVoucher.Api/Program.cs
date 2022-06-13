using CrudApiTemplate.Repositories;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services;
using PhuQuocVoucher.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PhuQuocDataContext>();
builder.Services.AddScoped<IUnitOfWork, PqUnitOfWork>();
builder.Services.InitServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();