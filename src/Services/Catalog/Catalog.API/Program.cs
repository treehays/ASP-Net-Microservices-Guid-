using Catalog.API.Data;
using Catalog.API.Repositories;


namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<ICatalogContext, CatalogContext>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerDocument(c =>
            {
                c.Title = "CatalogAPI";
                c.Version = "v1";
                c.Description = "Practicig Microservices";
            });
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
