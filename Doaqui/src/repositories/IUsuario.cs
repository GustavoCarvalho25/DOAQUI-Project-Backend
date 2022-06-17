using Doaqui.src.dtos;
using Doaqui.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doaqui.src.repositories
{

    /// <summary>
    /// <para> Summary: Representação do CRUD relacionado a usuarios </para>
    /// <para> Created by: Nickole Bueno </para>
    /// <para> Version: 1.0 </para>
    /// <para> Date: 05/05/2022 </para>
    /// </summary>
    public interface IUsuario
    {
        Task<List<UsuarioModelo>> PegarTodosUsuariosAsync();
        Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id);
        Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(NovoUsuarioDTO dto);
        Task AtualizarUsuarioAsync(AtualizarUsuarioDTO dto);
        Task AtualizarSenhaUsuarioAsync(AtualizarSenhaUsuarioDTO dto);
        Task DeletarUsuarioAsync(int id);
    }

}