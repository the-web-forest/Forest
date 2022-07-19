using Ipe.Configuration;

var builder = WebApplication.CreateBuilder(args);

Secrets.Configure(builder);
Databases.Configure(builder);
Services.Configure(builder);
Repositories.Configure(builder);
UseCases.Configure(builder);
JWTConfiguration.Configure(builder);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("corsapp");

app.MapControllers();
app.Run();

