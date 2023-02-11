using Microsoft.EntityFrameworkCore;
using Tryitter.Mapper;
using Tryitter.Repositories;
using Tryitter.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.

// builder.Services.AddDbContext<TryitterContext>(options => {
//     options.UseSqlServer(@"Server=127.0.0.1;Database=master;User=SA;Password=Password12!;Encrypt=false;");
// });

builder.Services.AddDbContext<TryitterContext>(opt => opt.UseInMemoryDatabase("Tryiter"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISecurityServices, SecurityServices>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Mapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
