using Carpool.CoreApi.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Carpool.CoreApi.ApplicationCore.Database.Seeding
{
    public class CoreContextSeeder : IAsyncDbContextSeeder<CoreContext>
    {
        private static CoreContext? _context;
        private static List<CarType> _carTypes = new();
        private static List<Color> _colors = new();
        private static List<Car> _cars = new();
        private static List<Employee> _employees = new();

        public async Task SeedAsync(CoreContext context, IServiceProvider provider)
        {
            _context = context;

            IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                IDbContextTransaction transaction = context.Database.BeginTransaction();
                try
                {
                    await SeedCarTypesAsync();
                    await SeedColorsAsync();
                    await transaction.CommitAsync();
                    await SeedCarsAsync();
                    await SeedEmployeesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    ILogger<CoreContextSeeder>? logger = provider.GetService<ILogger<CoreContextSeeder>>();
                    logger?.LogError(e, "Error occurred while seeding database: {Err}", e.Message);
                    await transaction.RollbackAsync();
                }
            });
        }

        private async Task SeedCarTypesAsync()
        {

            for (int i = 0; i < 3; i++)
            {
                CarType? carType = new CarType()
                {
                    Name = $"Car type {i}",
                    Description = $"Description for car type {i}"
                };

                _carTypes.Add(carType);
            }

            await _context!.CarTypes.AddRangeAsync(_carTypes);
            await _context!.SaveChangesAsync();
        }

        private async Task SeedColorsAsync()
        {

            for (int i = 0; i < 3; i++)
            {
                Color? color = new Color()
                {
                    Name = $"Color {i}",
                    Description = $"Description for color {i}",
                    Hex = $"hex{i}"

                };

                _colors.Add(color);
            }

            await _context!.Colors.AddRangeAsync(_colors);
            await _context!.SaveChangesAsync();
        }

        private async Task SeedCarsAsync()
        {

            for (int i = 0; i < 3; i++)
            {
                Car? car = new Car()
                {
                    Name = $"Color {i}",
                    Plate = $"Plate {i}"
                };

                _cars.Add(car);
            }

            _cars[0].Color = _colors[0];
            _cars[0].CarType = _carTypes[0];

            _cars[1].Color = _colors[1];
            _cars[1].CarType = _carTypes[1];

            _cars[2].Color = _colors[2];
            _cars[2].CarType = _carTypes[2];


            await _context!.Cars.AddRangeAsync(_cars);
            await _context!.SaveChangesAsync();
        }

        private async Task SeedEmployeesAsync()
        {

            for (int i = 0; i < 4; i++)
            {
                Employee? employee = new()
                {
                    FirstName = $"First name {i}",
                    LastName = $"Last name {i}",
                    IsDriver = true
                };

                _employees.Add(employee);

                i++;

                employee = new()
                {
                    FirstName = $"First name {i}",
                    LastName = $"Last name {i}",
                    IsDriver = false
                };

                _employees.Add(employee);
            }


            await _context!.Employees.AddRangeAsync(_employees);
            await _context!.SaveChangesAsync();
        }
    }
}
