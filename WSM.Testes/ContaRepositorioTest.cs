using Moq;
using WSM.ResApi.Data.Repository;
using WSM.ResApi.Models;

namespace WSM.Testes
{
    public class ContaRepositorioTest
    {
        [Fact(DisplayName = "Adicionar Conta com Sucesso")]
        [Trait("Categoria", "Conta repositorio Mock Tests")]
        public void ContaRepositorio_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var conta = new Conta("Conta Bancaria", "Conta de pessoa fisica");
            conta.Id = new Guid();

            var contaRepo = new Mock<IContaRepository>();

            // Act
            contaRepo.Object.Adicionar(conta);

            // Assert
            Assert.True(conta != null);
            contaRepo.Verify(r => r.Adicionar(conta), Times.Once);
        }
    }
}