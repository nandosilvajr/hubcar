

using HubCar.Services;
using HubCar.Services.Converters;
using HubCar.Shared.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace HubCar.UnitTest
{
    [TestFixture]
    public class NUnitTest
    {
        private Mock<ICarService> _mockCarService;

        [SetUp]
        public void Setup()
        {
            _mockCarService = new Mock<ICarService>();
        }
        [Test]
        public async Task GetCarsAsync_ReturnsFilteredCars()
        {
            var filterRequest = new FilterRequest
            {
                Make = "Toyota",
                MimimumBid = 0,
                MaximiumBid = 0,
                IsFavorite = false,
                FilterSort = FilterSort.MakeAscending.Name,
                PageNumber = 5,
                PageSize = 10
            };

            var expectedCars = new List<Car>
            {
                new Car { Make = "Toyota", Model = "Corolla", Year = 2020 },
                new Car { Make = "Toyota", Model = "Camry", Year = 2019 }
            };

            _mockCarService.Setup(service => service.GetCarsAsync(It.IsAny<FilterRequest>()))
                .ReturnsAsync(expectedCars);

            var carService = _mockCarService.Object;
            
            var cars = await carService.GetCarsAsync(filterRequest);
            
            ClassicAssert.IsNotNull(cars);
            ClassicAssert.AreEqual(2, cars.Count);
            ClassicAssert.AreEqual("Toyota", cars[0].Make);
            ClassicAssert.AreEqual("Camry", cars[1].Model);
        }
        
        [Test]
        public async Task GetMakeAsync_ReturnsDistinctMakes()
        {
            // Arrange
            var expectedMakes = new List<string?> { "Toyota", "Honda" };

            var cars = new List<Car>
            {
                new Car { Make = "Toyota", Model = "Camry", Year = 2020 },
                new Car { Make = "Honda", Model = "Civic", Year = 2019 },
                new Car { Make = "Toyota", Model = "Corolla", Year = 2018 }
            };

            _mockCarService.Setup(service => service.GetMakeAsync())
                .ReturnsAsync(expectedMakes);

            var carService = _mockCarService.Object;
    
            // Act
            var makes = await carService.GetMakeAsync();

            // Assert
            ClassicAssert.IsNotNull(makes);
            ClassicAssert.AreEqual(2, makes.Count);
            ClassicAssert.AreEqual("Toyota", makes[0]);
            ClassicAssert.AreEqual("Honda", makes[1]);
        }
        
        [Test]
        public async Task GetModelsAsync_ReturnsDistinctModels_ForSpecificMake()
        {
            // Arrange
            string make = "Toyota";
            var expectedModels = new List<string?> { "Camry", "Corolla" };

            var cars = new List<Car>
            {
                new Car { Make = "Toyota", Model = "Camry", Year = 2020 },
                new Car { Make = "Toyota", Model = "Corolla", Year = 2019 },
                new Car { Make = "Honda", Model = "Civic", Year = 2018 }
            };

            _mockCarService.Setup(service => service.GetModelsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedModels);

            var carService = _mockCarService.Object;
            
            var models = await carService.GetModelsAsync(make);
            
            ClassicAssert.IsNotNull(models);
            ClassicAssert.AreEqual(2, models.Count);
            ClassicAssert.AreEqual("Camry", models[0]);
            ClassicAssert.AreEqual("Corolla", models[1]);
        }
    }
}