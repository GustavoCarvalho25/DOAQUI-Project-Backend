using Microsoft.EntityFrameworkCore;
using Doaqui.src.data;
using Doaqui.src.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DoaquiTest.Test.data
{
    [TestClass]
    public class DoaquiContextoTeste
    {
        private DoaquiContexto _context;
        
        [TestInitialize]
        public void Inicio()
        {
            DbContextOptions<DoaquiContexto> opt = new DbContextOptionsBuilder<DoaquiContexto>()
            .UseInMemoryDatabase(databaseName: "db_doaqui")
            .Options;

            _context = new DoaquiContexto(opt);
        }

        [TestMethod]
        public void InsertNewUserReturnsUser()
        {
            UsuarioModelo model = new UsuarioModelo();
            model.Id = 000000000;
            model.Nome = "TestName";
            model.Email = "TestEmail";
            model.Telefone = "TestPhone";
            model.Endereco = "TestAddress";
            model.Senha = "TestPassword";
            _context.Usuarios.Add(model);
            _context.SaveChanges();
            Assert.IsNotNull(_context.Usuarios.FirstOrDefault(u => u.Senha == "TestPassword"));
        }

        [TestMethod]
        public void InsertNewRequestReturnsRequest()
        {
            SolicitacaoModelo model = new SolicitacaoModelo();
            model.ONG = null;
            model.Doacao = null;
            _context.Solicitacoes.Add(model);
            _context.SaveChanges();
            Assert.IsNotNull(_context.Solicitacoes.FirstOrDefault(r => r.Doacao == null));
        }

        [TestMethod]
        public void InsertNewDonationReturnsDonation()
        {
            DoacaoModelo model = new DoacaoModelo();
            model.Contato = "TestContact";
            model.Quantidade = 10000;
            model.Validade = "TestValidity";
            model.Descricao = "TestDescription";
            model.CNPJ_Doador = "000000000";
            _context.Doacoes.Add(model);
            _context.SaveChanges();
            Assert.IsNotNull(_context.Doacoes.FirstOrDefault(d => d.Id == 1));
        }
    }
}

      