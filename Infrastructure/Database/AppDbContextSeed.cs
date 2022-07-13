using Domain.Entity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class AppDbContextSeed
    {
        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(new[]
                {
                    new Order() { ProviderId = 3, Date = DateTime.Now, Number = "01001/22" },
                    new Order() { ProviderId = 2, Date = DateTime.Now.AddDays(-1), Number = "0101/22" },
                    new Order() { ProviderId = 2, Date = DateTime.Now.AddDays(-1), Number = "0102/22" },
                    new Order() { ProviderId = 2, Date = DateTime.Now.AddDays(-1), Number = "0103/22" },
                    new Order() { ProviderId = 2, Date = DateTime.Now.AddDays(-1), Number = "0104/22" },
                    new Order() { ProviderId = 3, Date = DateTime.Now.AddDays(-1), Number = "0105/22" },
                    new Order() { ProviderId = 1, Date = DateTime.Now.AddDays(-1), Number = "0106/22" },
                    new Order() { ProviderId = 1, Date = DateTime.Now.AddDays(-1), Number = "0107/22" },
                    new Order() { ProviderId = 1, Date = DateTime.Now.AddDays(-1), Number = "0108/22" },
                    new Order() { ProviderId = 3, Date = DateTime.Now.AddDays(-10), Number = "0100/22" },
                    new Order() { ProviderId = 1, Date = DateTime.Now.AddDays(-10), Number = "0199/22" },
                    new Order() { ProviderId = 3, Date = DateTime.Now.AddDays(-10), Number = "0198/22" },
                    new Order() { ProviderId = 1, Date = DateTime.Now.AddDays(-15), Number = "0197/22" },
                    new Order() { ProviderId = 3, Date = DateTime.Now.AddDays(-35), Number = "0196/22" },
                });

                await context.SaveChangesAsync(CancellationToken.None);
            }

            if (!context.OrderItems.Any())
            {
                context.OrderItems.AddRange(new[]
                {
                    new OrderItem() { OrderId = 1, Name = "Карналлит ", Quantity = 3000m, Unit = "т." },
                    new OrderItem() { OrderId = 2, Name = "Черный металл", Quantity = 111.1m, Unit = "т." },
                    new OrderItem() { OrderId = 3, Name = "Черный металл", Quantity = 100.0m, Unit = "т." },
                    new OrderItem() { OrderId = 3, Name = "Цветной металл", Quantity = 75.6m, Unit = "т." },
                    new OrderItem() { OrderId = 4, Name = "Цветной металл", Quantity = 300m, Unit = "т." },
                    new OrderItem() { OrderId = 5, Name = "Черный металл", Quantity = 181.3m, Unit = "т." },
                    new OrderItem() { OrderId = 6, Name = "Калий розовый", Quantity = 500m, Unit = "т." },
                    new OrderItem() { OrderId = 6, Name = "Калий белый", Quantity = 450m, Unit = "т." },
                    new OrderItem() { OrderId = 6, Name = "Карналлит", Quantity = 50m, Unit = "т." },
                    new OrderItem() { OrderId = 7, Name = "Черный металл", Quantity = 950m, Unit = "т." },
                    new OrderItem() { OrderId = 8, Name = "Цветной металл", Quantity = 100m, Unit = "т." },
                    new OrderItem() { OrderId = 9, Name = "Черный металл", Quantity = 181.3m, Unit = "т." },
                    new OrderItem() { OrderId = 10, Name = "Фосфор", Quantity = 150m, Unit = "кг." },
                    new OrderItem() { OrderId = 11, Name = "Черный металл", Quantity = 11m, Unit = "т." },
                    new OrderItem() { OrderId = 12, Name = "Калий", Quantity = 300.30m, Unit = "т." },
                    new OrderItem() { OrderId = 13, Name = "Черный металл", Quantity = 181.3m, Unit = "т." },
                    new OrderItem() { OrderId = 14, Name = "Калий", Quantity = 181.3m, Unit = "т." },
                    new OrderItem() { OrderId = 14, Name = "Фосфор", Quantity = 150m, Unit = "т." },

                });

                await context.SaveChangesAsync(CancellationToken.None);
            }

            if (!context.Providers.Any())
            {
                context.Providers.AddRange(new[]
                {
                    new Provider() { Name = "Завод #1" },
                    new Provider() { Name = "Уральский комбинат" },
                    new Provider() { Name = "УралКалий" },
                });

                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}