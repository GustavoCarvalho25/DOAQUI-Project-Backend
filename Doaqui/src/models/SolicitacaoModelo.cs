using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doaqui.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_solicitacao no banco.</para>
    /// <para>Criado por: Naomy Santana</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    
    [Table("tb_Solicitacao")]
    public class SolicitacaoModelo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("fk_usuario")]
        public UsuarioModelo ONG { get; set; }

        [ForeignKey("fk_doacao")]
        public DoacaoModelo Doacao { get; set; }
    }
}
