using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Prueba.ApiClients;
using Prueba.Entities;
using Prueba.Middleware;
using Prueba.Repositorio;

namespace Prueba
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        //Method 1
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // #========= Configuración de datos en memoria [Productos y Diccionario de estados] =========#
            services.AddMemoryCache();
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            var dictionary = new Dictionary<int, string>
            {
                {1, "Active"},
                {0, "Inactive"}
            };
            services.AddSingleton<IDictionary<int, string>>(dictionary);
            services.AddSingleton(new List<Producto>());
            services.AddTransient(provider =>
            {
                var cache = provider.GetRequiredService<IMemoryCache>();
                var cachedDictionary = cache.Get<IDictionary<int, string>>("productStates");
                if (cachedDictionary == null)
                {
                    cachedDictionary = dictionary;
                    cache.Set("productStates", cachedDictionary, cacheOptions);
                }
                return cachedDictionary;
            });

            // #========= Inyección de dependencias =========#
            services.AddTransient<IRepository<Producto>, ProductoRepository>();
            services.AddHttpClient<DescuentosApiClient>();


            // #========= Mapper =========#
            services.AddAutoMapper(typeof(Startup));
        }

        //Method 2
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

