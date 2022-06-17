using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Doaqui.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_doacao no banco.</para>
    /// <para>Criado por: Naomy Santana</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    
    [Table("tb_Doacao")]
    public class DoacaoModelo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Contato { get; set; }

        public int Quantidade { get; set; }

        public string Validade { get; set; }

        public string Foto { get; set; }

        public string CNPJDoador { get; set; }
          
    }
}