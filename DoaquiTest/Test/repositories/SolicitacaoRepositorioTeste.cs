using Doaqui.src.data;
using Doaqui.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doaqui.src.dtos;
using Doaqui.src.models;
using System.Collections.Generic;
using Doaqui.src.repositories;
using System.Threading.Tasks;

namespace DoaquiTeste.Test.repositories
{
    [TestClass]
    public class SolicitacaoRepositorioTeste
    {

        private DoaquiContexto _contexto;
        private ISolicitacao _repositorio;

        [TestMethod]
        public async Task CriarUmaSolicitacaoRetornaUmaSolicitacaoAsync()
        {
            var opt = new DbContextOptionsBuilder<DoaquiContexto>()
            .UseInMemoryDatabase(databaseName: "db_doaqui")
            .Options;

            _contexto = new DoaquiContexto(opt);
            _repositorio = new SolicitacaoRepositorio(_contexto);


           await _repositorio.NovaSolicitacaoAsync(
                new NovaSolicitacaoDTO(new UsuarioModelo(), new DoacaoModelo()));

            List<SolicitacaoModelo> solicitacaoModelos = await _repositorio.PegarTodasSolicitacoesAsync();
            Assert.AreEqual(1, solicitacaoModelos.Count);
        }
    }
}
