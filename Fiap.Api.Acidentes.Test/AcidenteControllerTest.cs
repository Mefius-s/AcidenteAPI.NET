using AutoMapper;
using Fiap.Api.Acidentes.Controllers;
using Fiap.Api.Acidentes.Data.Contexts;
using Fiap.Api.Acidentes.Models;
using Fiap.Api.Acidentes.Services;
using Fiap.Api.Acidentes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Api.Acidentes.Test
{
    public class AcidenteControllerTest
    {

        private readonly Mock<IAcidenteService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AcidenteController _acidenteController;

        public AcidenteControllerTest()
        {
            _mockService = new Mock<IAcidenteService>();
            _mockMapper = new Mock<IMapper>();
            _acidenteController = new AcidenteController(_mockService.Object, _mockMapper.Object);
        }

        // Método para criar e configurar um DbSet mock para AcidenteModel
        private IQueryable<AcidenteModel> GetTestAcidentes()
        {
            return new List<AcidenteModel>
            {
                new AcidenteModel { Id = 1, DataAcidente = DateTime.Now },
                new AcidenteModel { Id = 2, DataAcidente = DateTime.Now }
            }.AsQueryable();
        }


        //-------------------------------TESTE ACIDENTE POR ID--------------------------------------------//
        //-----------------------------------STATUS CODE 200---------------------------------------------//
        [Fact]
        public void GetById_returnStatusCode200()
        {
            // Arrange
            long acidenteId = 1;
            var acidente = new AcidenteModel { Id = acidenteId, DataAcidente = DateTime.Now };
            var acidenteViewModel = new AcidenteViewModel { Id = acidenteId, DataAcidente = acidente.DataAcidente };

            _mockService.Setup(service => service.ObterAcidentePorId(acidenteId)).Returns(acidente);
            _mockMapper.Setup(mapper => mapper.Map<AcidenteViewModel>(acidente)).Returns(acidenteViewModel);

            // Act
            var result = _acidenteController.Get(acidenteId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AcidenteViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);

            var returnValue = Assert.IsType<AcidenteViewModel>(okResult.Value);
            Assert.Equal(acidente.Id, returnValue.Id);
            Assert.Equal(acidente.DataAcidente, returnValue.DataAcidente);
            // Adicione mais verificações conforme necessário para outras propriedades do ViewModel
        }
        //-----------------------------------------------------------------------------------------------//




        //----------------------------TESTE LISTAR ACIDENTES PAGINADO---------------------------------------//
        //------------------------------------STATUS CODE 200----------------------------------------------//
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var acidentes = GetTestAcidentes().ToList();
            var acidenteViewModel = acidentes.Select(a => new AcidenteViewModel { Id = a.Id, DataAcidente = a.DataAcidente }).ToList();

            _mockService.Setup(service => service.ListarAcidentesReferencia(It.IsAny<int>(), It.IsAny<int>())).Returns(acidentes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<AcidenteViewModel>>(It.IsAny<IEnumerable<AcidenteModel>>())).Returns(acidenteViewModel);

            // Act
            var result = _acidenteController.Get(0, 10);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AcidentePaginacaoReferenciaViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);
            var returnValue = Assert.IsType<AcidentePaginacaoReferenciaViewModel>(okResult.Value);
            Assert.Equal(acidenteViewModel.Count, returnValue.Acidentes.Count());
        }

        //-----------------------------------------------------------------------------------------------//





        //--------------------------------TESTE CRIAR ACIDENTE--------------------------------------------//
        //-----------------------------------STATUS CODE 201---------------------------------------------//
        [Fact]
        public void Post_ReturnsStatusCode201()
        {
            // Arrange
            var acidenteViewModel = new AcidenteViewModel
            {
                DataAcidente = DateTime.Now,
                HoraAcidente = TimeSpan.FromHours(10),
                Gravidade = "Grave",
                Endereco = "Rua Teste, 123"
            };

            var acidenteModel = new AcidenteModel
            {
                Id = 1,
                DataAcidente = acidenteViewModel.DataAcidente,
                HoraAcidente = acidenteViewModel.HoraAcidente,
                Gravidade = acidenteViewModel.Gravidade,
                Endereco = acidenteViewModel.Endereco
            };

            _mockMapper.Setup(mapper => mapper.Map<AcidenteModel>(acidenteViewModel)).Returns(acidenteModel);

            // Act
            var result = _acidenteController.Post(acidenteViewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//






        //-------------------------------TESTE ATUALIZAR ACIDENTE-----------------------------------------//
        //-----------------------------------STATUS CODE 204---------------------------------------------//
        [Fact]
        public void Put_ReturnsStatusCode204()
        {
            // Arrange
            var acidenteId = 1;
            var acidenteViewModel = new AcidenteViewModel
            {
                Id = acidenteId,
                DataAcidente = DateTime.Now,
                HoraAcidente = TimeSpan.FromHours(12),
                Gravidade = "Moderada",
                Endereco = "Rua Nova, 456"
            };

            var acidenteModel = new AcidenteModel
            {
                Id = acidenteId,
                DataAcidente = acidenteViewModel.DataAcidente,
                HoraAcidente = acidenteViewModel.HoraAcidente,
                Gravidade = acidenteViewModel.Gravidade,
                Endereco = acidenteViewModel.Endereco
            };

            _mockService.Setup(service => service.ObterAcidentePorId(acidenteId)).Returns(acidenteModel);

            // Act
            var result = _acidenteController.Put(acidenteId, acidenteViewModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//






        //--------------------------------TESTE EXCLUIR ACIDENTE------------------------------------------//
        //-----------------------------------STATUS CODE 204---------------------------------------------//
        [Fact]
        public void Delete_ReturnsStatusCode204()
        {
            // Arrange
            var acidenteId = 1;

            _mockService.Setup(service => service.ObterAcidentePorId(acidenteId)).Returns(new AcidenteModel { Id = acidenteId });

            // Act
            var result = _acidenteController.Delete(acidenteId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//










    }
}
