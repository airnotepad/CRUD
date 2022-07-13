using Application.Common.UnitOfWork;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("Database"));
                        //options.UseSqlite("Filename=Database.db"));
                        //options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=testdb;Trusted_Connection=True;"));
            services.AddUnitOfWork<AppDbContext>();

            return services;
        }
    }
}