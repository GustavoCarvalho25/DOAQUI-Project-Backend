using System.ComponentModel.DataAnnotations;
using Doaqui.src.utilidades;

namespace Doaqui.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo usuario</para>
    /// <para>Criado por: Renata Nunes</para>
    /// <para>Versão: 2.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NovoUsuarioDTO
    {
        [Required, StringLength(200)]
        public string Nome { get; set; }

        [Required, StringLength(200)]
        public string Email { get; set; }

        [Required, StringLength(200)]
        public string Senha { get; set; }

        [Required, StringLength(200)]
        public string Foto { get; set; }

        [Required, StringLength(200)]
        public string Telefone { get; set; }

        [Required, StringLength(200)]
        public string Endereco { get; set; }

        [Required, StringLength(200)]
        public string Cnpj { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }

        public NovoUsuarioDTO(string nome, string email, string senha, string foto, string telefone, string endereco, string cnpj, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Foto = foto;
            Telefone = telefone;
            Endereco = endereco;
            Cnpj = cnpj;
            Tipo = tipo;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar um usuario</para>
    /// <para>Criado por: Renata Nunes</para>
    /// <para>Versão: 2.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarUsuarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Nome { get; set; }

        [Required, StringLength(200)]
        public string Email { get; set; }

        [Required, StringLength(200)]
        public string Foto { get; set; }

        [Required, StringLength(200)]
        public string Telefone { get; set; }

        [Required, StringLength(200)]
        public string Endereco { get; set; }

        [Required, StringLength(200)]
        public string Cnpj { get; set; }

        public AtualizarUsuarioDTO(int id, string nome, string email, string foto, string telefone, string endereco, string cnpj)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Foto = foto;
            Telefone = telefone;
            Endereco = endereco;
            Cnpj = cnpj;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar senha do usuario</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 2.0</para>
    /// <para>Data: 16/06/2022</para>
    /// </summary>
    public class AtualizarSenhaUsuarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string SenhaAntiga { get; set; }

        [Required, StringLength(200)]
        public string SenhaNova { get; set; }

        public AtualizarSenhaUsuarioDTO(int id, string senhaAntiga, string senhaNova)
        {
            Id = id;
            SenhaAntiga = senhaAntiga;
            SenhaNova = senhaNova;
        }
    }
}
