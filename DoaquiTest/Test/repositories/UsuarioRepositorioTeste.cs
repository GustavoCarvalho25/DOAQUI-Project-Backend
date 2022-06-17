using Doaqui.src.data;
using Doaqui.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doaqui.src.dtos;
using Doaqui.src.models;
using System.Collections.Generic;
using Doaqui.src.utilidades;
using Doaqui.src.repositories;
using System.Threading.Tasks;

namespace DoaquiTeste.Test.repositories
{
    [TestClass]
    public class UsuarioRepositorioTeste
    {

        private DoaquiContexto _contexto;
        private IUsuario _repositorio;

        [TestMethod]
        public async Task CriarUmUsuarioRetornaUmUsuarioAsync()
        {
            var opt = new DbContextOptionsBuilder<DoaquiContexto>()
            .UseInMemoryDatabase(databaseName: "db_doaqui")
            .Options;

            _contexto = new DoaquiContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO(445482400, "TesteUsuario", "testeemail@email.com",
                "40028922","av. pitangas 123", "senhateste", TipoUsuario.ONG, 46758495));
        }
    }
}
