using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verifarma.Controllers;
using Verifarma.Data;
using Verifarma.Interfaces;
using Verifarma.Models;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTest.Tests
{
    public class FarmaciaControllerTest
    {
        private readonly Mock<IFarmacyService> _farmacyServiceMock;
        private readonly ApplicationDbContext _context;
        private readonly FarmaciasController _controller;

        public FarmaciaControllerTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            _context = new ApplicationDbContext(options);
            _farmacyServiceMock = new Mock<IFarmacyService>();
            _controller = new FarmaciasController(_context, _farmacyServiceMock.Object);
        }

        [Fact]
        public async void Post_FaltanCampos()
        {
            var farmacia = new Farmacia { Id = 10 , Nombre = "Farmacia falsa", Latitud = 28, Longitud = 30 };
            var result = await _controller.Post(farmacia);
        }
        [Fact]
        public async void Post_Correcto()
        {
            var farmacia = new Farmacia { Nombre = "Farmacia falsa", Direccion = "Calle falsa 123", Latitud = 28, Longitud = 30 };
            var result = await _controller.Post(farmacia);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public void GetFarmaciaCercana_Caso()
        {
            var farmacia1 = new Farmacia {Id = 1, Nombre = "Farmacia falsa", Direccion = "Calle falsa 123", Latitud = 3, Longitud = 3.1F };
            var farmacia2 = new Farmacia {Id = 2, Nombre = "Farmacia falsa2", Direccion = "Calle falsa 123", Latitud = 28, Longitud = 30 };
            var farmacia3 = new Farmacia {Id = 3, Nombre = "Farmacia falsa3", Direccion = "Calle falsa 123", Latitud = 58, Longitud = 10 };
            _context.Farmacias.AddRange(farmacia1, farmacia2, farmacia3);
            _context.SaveChanges();

            var result = _controller.GetFarmaciaCercana(3, 5);
            Assert.True(result.Id == farmacia1.Id);
        }
    }
}
