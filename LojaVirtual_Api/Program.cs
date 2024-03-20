using Infra.Data.Ioc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Repositories_Context_Injection(builder.Configuration);//DependencyInjection
builder.Services.TokenConfig(builder.Configuration);//DependencyInjection
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.serviceDescriptors();//Add SwaggerGen
//builder.Services.AddSwaggerGen();

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
