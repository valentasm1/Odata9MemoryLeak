using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using OData.Swagger.Services;
using ODataMemoryLeak.Services;

namespace ODataMemoryLeak;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IDataService, DataService>();
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("testdb"));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc(ODataConfiguration.SwaggerDocVersion, new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = ODataConfiguration.SwaggerDocTitle,
                Description = ODataConfiguration.SwaggerDocDescription,
                Version = ODataConfiguration.SwaggerDocVersion
            });
        });

        var model = ODataBuilder.GetEdmModel();

        builder.Services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false)
            .AddOData(options =>
            {
                options
                    .EnableQueryFeatures()
                    .AddRouteComponents("odata", model, srv =>
                    {
                    })
                    .Select().Filter().OrderBy();

            });


        builder.Services.AddMvc();
        builder.Services.AddOdataSwaggerSupport();

        //**********************************************************
        //**********************************************************
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();

    }
}