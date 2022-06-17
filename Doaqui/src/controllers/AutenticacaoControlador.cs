using System;
using System.Threading.Tasks;
using Doaqui.src.dtos;
using Doaqui.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doaqui.src.controladores
{
    [ApiController]
    [Route("api/Autenticacao")]
    [Produces("application/json")]
    public class AutenticacaoControlador : ControllerBase
    {
        #region Atributos

        private readonly IAutenticacao _servicos;

        #endregion


        #region Construtores

        public AutenticacaoControlador(IAutenticacao servicos)
        {
            _servicos = servicos;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Pegar Autorização
        /// </summary>
        /// <param name="dto">AutenticarDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Autenticacao
        ///     {
        ///        "email": "naozinha@email.com",
        ///        "senha": "134652"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ou senha invalido</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AutenticarAsync([FromBody] AutenticarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var autorizacao = await _servicos.PegarAutorizacaoAsync(dto);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        #endregion
    }
}