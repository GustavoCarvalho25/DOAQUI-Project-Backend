using System.ComponentModel.DataAnnotations; 

namespace Doaqui.src.dtos
{ 
    /// <summary>
    /// <para>Resumo: Classe espelho para cria nova postagem de doação</para>
    /// <para>Criado por: Renata Nunes</para>
    /// <para>Versão: 2.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
    public class NovaDoacaoDTO
    {
        [Required, StringLength(100)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        [Required, StringLength(100)]
        public string Contato { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required, StringLength(100)]
        public string Validade  { get; set; }

        [Required, StringLength(500)]
        public string Foto { get; set; }
        
        [Required, StringLength(100)]
        public string CNPJDoador { get; set; }

        public NovaDoacaoDTO(string titulo, string descricao, string contato, int quantidade, string validade, string foto, string cNPJDoador)
        {
            Titulo = titulo;
            Descricao = descricao;
            Contato = contato;
            Quantidade = quantidade;
            Validade = validade;
            Foto = foto;
            CNPJDoador = cNPJDoador;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar uma doacao</para>
    /// <para>Criado por: Renata Nunes</para>
    /// <para>Versão: 2.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
    public class AtualizarDoacaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        [Required, StringLength(100)]
        public string Contato { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required, StringLength(100)]
        public string Validade  { get; set; }

        [Required, StringLength(100)]
        public string Foto { get; set; }
        
        [Required, StringLength(100)]
        public string CNPJDoador { get; set; }

        public AtualizarDoacaoDTO(int id, string titulo, string descricao, string contato, int quantidade, string validade, string foto, string cNPJDoador)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Contato = contato;
            Quantidade = quantidade;
            Validade = validade;
            Foto = foto;
            CNPJDoador = cNPJDoador;
        }
    }
}