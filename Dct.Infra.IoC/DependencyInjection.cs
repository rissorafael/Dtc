using Dct.Infra.CrossCutting.Configuration;
using Dct.Infra.CrossCutting.Kafka;
using Dct.Infra.Data.Connection;
using Dct.Infra.Data.Context;
using Dct.Infra.Data.Repository;
using Dtc.Domain.Interfaces;
using Dtc.Service.Products;
using DTC.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dct.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Inicializa leitura de variáveis/configurações
            ExatractConfiguration.Initialize(configuration);

            // ConnectionFactory
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<KafkaProducer>();

            // Repositórios
            services.AddScoped<IProductsRepository, ProductsRepository>();

            // DbContext com EF Core
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseSqlServer(ExatractConfiguration.GetSqlServerConnectionString));

            // Serviços de domínio
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductsMongoApplication, ProductsMongoApplication>();

        }
    }
}
