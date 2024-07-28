using Xunit;
using Moq;
using LembreteApp.Controllers;
using LembreteApp.Services;
using LembreteApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LembreteApp.Interfaces;
using MongoDB.Driver.Linq;
using static Moq.MockBehavior;

namespace LembreteApp.Tests
{
    public class LembretesControllerTests
    {
        private readonly Mock<ILembreteService> _lembreteServiceMock;
        private readonly LembretesController _controller;

        public LembretesControllerTests()
        {
            _lembreteServiceMock = new Mock<ILembreteService>(Strict);
            _controller = new LembretesController(_lembreteServiceMock.Object);
        }

        private List<Lembrete> GetTestLembretes()
        {
            return new List<Lembrete>
            {
                new Lembrete { Id = "1", Nome = "Lembrete 1", Data = DateTime.Now.AddDays(1)},
                new Lembrete { Id = "2", Nome = "Lembrete 2", Data = DateTime.Now.AddDays(1) }
            };
        }

        [Fact]
        public void GetLembretes_DeveRetornarListaDeLembretes()
        {
            // Arrange
            var lembretes = GetTestLembretes();
            _lembreteServiceMock.Setup(service => service.Get()).Returns(lembretes);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Lembrete>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<List<Lembrete>>(okResult.Value);
            Assert.Equal(lembretes, model);
        }

        [Theory]
        [InlineData("", 1)]
        [InlineData("Lembrete 1", 0)]
        [InlineData("Lembrete 2", -1)]
        public void PostLembrete_DeveRetornarBadRequestQuandoNomeNaoPreenchidoOuDataInvalida(string nome, int dias)
        {
            // Arrange
            var lembrete = new Lembrete {Id = "1", Nome = nome, Data = DateTime.Now.AddDays(dias) };

            // Act
            var result = _controller.Create(lembrete);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Lembrete>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void PostLembrete_DeveRetornarCreatedAtRouteQuandoLembreteValido()
        {
            // Arrange
            var lembrete = new Lembrete { Id = "1", Nome = "Lembrete 1", Data = DateTime.Now.AddDays(1) };
            _lembreteServiceMock.Setup(service => service.Create(lembrete)).Returns(lembrete);

            // Act
            var result = _controller.Create(lembrete);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Lembrete>>(result);
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
            Assert.Equal("GetLembrete", createdAtRouteResult.RouteName);
            Assert.Equal(lembrete.Id, createdAtRouteResult.RouteValues["id"]);
            Assert.Equal(lembrete, createdAtRouteResult.Value);
        }

        public static IEnumerable<object?[]> LembreteData =>
            new List<object?[]>
            {
                new object?[] { null },
            };

        

        [Theory]
        [MemberData(nameof(LembreteData))]
        public void DeleteLembrete_DeveRetornarNotFoundOuNoContentQuandoLembreteNaoEncontradoOuEncontrado(Lembrete lembrete)
        {
            // Arrange
            _lembreteServiceMock.Setup(service => service.Get("1")).Returns(lembrete);
            if (lembrete != null)
            {
                _lembreteServiceMock.Setup(service => service.Remove("1")).Verifiable();
            }

            // Act
            var result = _controller.Delete("1");

            // Assert
            if (lembrete == null)
            {
                Assert.IsType<NotFoundResult>(result);
            }
            else
            {
                Assert.IsType<NoContentResult>(result);
                _lembreteServiceMock.Verify(service => service.Remove("1"), Times.Once);
            }
        }
    }
}