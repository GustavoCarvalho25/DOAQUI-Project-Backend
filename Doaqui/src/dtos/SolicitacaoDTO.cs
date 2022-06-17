using Doaqui.src.models;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;


namespace Doaqui.src.dtos
{ 
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um nova solicitação</para>
    /// <para>Criado por: Renata Nunes</para>
    /// <para>Versão: 3.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>

    public class NovaSolicitacaoDTO
    {
        [Required]
        public int IdONG { get; set; }

        [Required]
        public int IdDoacao { get; set; }

        public NovaSolicitacaoDTO(int idONG, int idDoacao)
        {
            IdONG = idONG;
            IdDoacao = idDoacao;
        }
    }
}
