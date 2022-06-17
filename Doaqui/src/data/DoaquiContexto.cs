using Doaqui.src.models;
using Microsoft.EntityFrameworkCore;

namespace Doaqui.src.data
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Naomy Santana</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class DoaquiContexto : DbContext
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; }
        public DbSet<DoacaoModelo> Doacoes { get; set; }
        public DbSet<SolicitacaoModelo> Solicitacoes { get; set; }

        public DoaquiContexto(DbContextOptions<DoaquiContexto> opt) : base(opt)
        {

        }
    }
}