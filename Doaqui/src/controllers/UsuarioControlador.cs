using System;
using System.Threading.Tasks;
using Doaqui.src.dtos;
using Doaqui.src.models;
using Doaqui.src.repositories;
using Doaqui.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doaqui.src.controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        private readonly IAutenticacao _servicos;
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio, IAutenticacao servicos)
        {
            _repositorio = repositorio;
            _servicos = servicos;
        }
        #endregion 

        #region Métodos

        /// <summary>
        /// Pegar todos usuarios
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de usuario</response>
        /// <response code="204">Não existe usuario</response>
        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult> PegarTodosUsuariosAsync()
        {
            var lista = await _repositorio.PegarTodosUsuariosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }
        
        /// <summary>
        /// Pegar usuario pelo Id
        /// </summary>
        /// <param name="idUsuario">Id do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [HttpGet("id/{idUsuario}")]
        [Authorize]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPeloIdAsync(idUsuario));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Pegar usuario pelo Email
        /// </summary>
        /// <param name="emailUsuario">Email do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">E-mail não existe</response>
        [HttpGet("email/{emailUsuario}")]
        [Authorize]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
                var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);
                if (usuario == null) return NotFound(new { message = "Usuario não existe" });
                return Ok(usuario);
        }

        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="dto">NovoUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Usuarios
        ///     {
        ///        "nome": "Naomy Santana",
        ///        "email": "naozinha@email.com",
        ///        "senha": "134652",
        ///        "foto": "URLFOTO",
        ///        "telefone": "123456789",
        ///        "endereco": "Rua cinco, São Paulo - 265",
        ///        "cnpj": "444.444.444-44",
        ///        "tipo": "ONG"
        ///      }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] NovoUsuarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(dto);
                return Created($"api/Usuarios/email/{dto.Email}", dto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary>
        /// <param name="dto">AtualizarUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Usuarios
        ///     {
        ///        "id": 1, 
        ///        "nome": "Naomy Santana",
        ///        "email": "naozinha@email.com",
        ///        "foto": "URLFOTO",
        ///        "telefone": "123456789",
        ///        "endereco": "Rua cinco, São Paulo - 265",
        ///        "cnpj": "444.444.444-44"
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPut("usuario")]
        [Authorize]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] AtualizarUsuarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _repositorio.AtualizarUsuarioAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                 return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar senha Usuario
        /// </summary>
        /// <param name="dto">AtualizarSenhaUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Usuarios
        ///     {
        ///        "id": 1, 
        ///        "senhaAntiga": "134652",
        ///        "senhaNova": "123456"
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPut("senha")]
        [Authorize]
        public async Task<ActionResult> AtualizarSenhaUsuarioAsync([FromBody] AtualizarSenhaUsuarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _repositorio.AtualizarSenhaUsuarioAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                 return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletar usuario pelo Id
        /// </summary>
        /// <param name="id">Id usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [HttpDelete("deletar/{id}")]
        [Authorize]
        public async Task<ActionResult> DeletarUsuarioAsync([FromRoute] int id)
        {
            try
            {
                await _repositorio.DeletarUsuarioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                 return NotFound(new { message = ex.Message });
            }
        }

        #endregion Métodos
    }
}