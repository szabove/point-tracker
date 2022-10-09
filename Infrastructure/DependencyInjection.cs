using Application.Abstraction;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayerDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddDbContext<PointTrackerDbContext>(options =>
                // Using temporary database, after app closing data will be lost
                options.UseInMemoryDatabase(databaseName: "PointTracker")
            );
            services.AddScoped<PointTrackerDbContext>();
            
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}

