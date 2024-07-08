
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Options;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;
using Otus.Teaching.PromoCodeFactory.WebHost.Mapping;
using System.Reflection;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var executedAssembly = Assembly.GetExecutingAssembly();
            var config = new MapperConfiguration(cfg => cfg.AddMaps(executedAssembly));

            services.AddHealthChecks();
            services.AddControllers();
            services.AddSingleton(_ => config.CreateMapper());
            services.AddScoped<IDbInitializer, EfDbInitializer>();
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //NSwag - как добавить xml?
            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });

            var options = Configuration.GetSection<ConnectionOptions>();
            services.AddSingleton(options);

            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDB"));
            services.AddSingleton<IMongoRoleRepository, MongoRoleRepository>();
            services.AddSingleton<IMongoEmployeeRepository, MongoEmployeeRepository>();

            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddAutoMapper(typeof(AppMappingProfile));
            /*            services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataflowToUpper", Version = "v1" });
                            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                        });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi(x =>
            {
                x.DocExpansion = "list";
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
            // Add ReDoc UI to interact with the document
            // Available at: http://localhost:<port>/redoc
            //app.UseReDoc(options =>
            //{
            //    options.Path = "/redoc";
           // });
            dbInitializer.InitializeDb();
        }
    }
    /// <summary>
    /// Расширения для конфигурации приложения
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Получить объект конфигурации из appsettings.json
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <param name="sectionName">Наименование json секции</param>
        /// <returns>Возвращает преобразованный объект типа TEntity в случае наличия секции, иначе выдает исключение</returns>
        public static TEntity GetSection<TEntity>(this IConfiguration configuration, string? sectionName = null)
        {
            sectionName ??= typeof(TEntity).Name;
            return configuration.GetSection(sectionName).Get<TEntity>();
        }
    }
}