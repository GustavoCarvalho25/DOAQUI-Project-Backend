using Doaqui.src.data;
using Doaqui.src.dtos;
using Doaqui.src.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doaqui.src.repositories.implementations
{

    /// <summary>
    /// <para> Summary: usuario repository implementation of IUsuario interface. </para>
    /// <para> Created by: Nickole Bueno </para>
    /// <para> Version: 1.0 </para>
    /// <para> Date: 05/05/2022 </para>
    /// </summary>
    public class UsuarioRepositorio : IUsuario
    {

        #region Attributes
        private readonly DoaquiContexto _contexto;
        #endregion


        #region Constructors
        public UsuarioRepositorio(DoaquiContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Methods

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuarios</para>
        /// </summary>
        /// <return>Lista UsuarioModelo</return>
        public async Task<List<UsuarioModelo>> PegarTodosUsuariosAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo ID</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        /// <exception cref="Exception">Caso não encontre o usuario</exception>
        public async Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de usuario não encontrado");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        /// <exception cref="Exception">Caso não encontre o usuario</exception>
        public async Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email)
        {
            if (!await ExisteEmail(email)) throw new Exception("Email de usuario não encontrado");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            // função auxiliar
            async Task<bool> ExisteEmail(string email)
            {
                var auxiliar = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
                return auxiliar != null;
            }
        }

         /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="dto">NovoUsuarioDTO</param>
        public async Task NovoUsuarioAsync(NovoUsuarioDTO dto)
        {
            await _contexto.Usuarios.AddAsync(new UsuarioModelo
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Foto = dto.Foto,
                Telefone = dto.Telefone,
                Endereco = dto.Endereco,
                Tipo = dto.Tipo,
                Cnpj = dto.Cnpj

            });
           await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="dto">AtualizarUsuarioDTO</param>
        public async Task AtualizarUsuarioAsync(AtualizarUsuarioDTO dto)
        {
            UsuarioModelo modelo = await PegarUsuarioPeloIdAsync(dto.Id);
            modelo.Nome = dto.Nome;
            modelo.Email = dto.Email;
            modelo.Foto = dto.Foto;
            modelo.Telefone = dto.Telefone;
            modelo.Endereco = dto.Endereco;
            modelo.Cnpj = dto.Cnpj;
            _contexto.Update(modelo);
           await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar senha de usuario</para>
        /// </summary>
        /// <param name="dto">AtualizarUsuarioDTO</param>
        public async Task AtualizarSenhaUsuarioAsync(AtualizarSenhaUsuarioDTO dto)
        {
            var modelo = await PegarUsuarioPeloIdAsync(dto.Id);

            if (modelo.Senha != CodificarSenha(dto.SenhaAntiga)) throw new Exception("Senha antiga incorreta");

            modelo.Senha = CodificarSenha(dto.SenhaNova);

            _contexto.Update(modelo);
            await _contexto.SaveChangesAsync();

            // função auxiliar
            string CodificarSenha(string senha)
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _contexto.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
           await _contexto.SaveChangesAsync();
        }

        #endregion
    }
}
