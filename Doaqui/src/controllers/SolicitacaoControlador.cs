using System;
using System.Threading.Tasks;
using Doaqui.src.dtos;
using Doaqui.src.models;
using Doaqui.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doaqui.src.controllers
{

    [ApiController]
    [Route("api/Solicitacoes")]
    [Produces("application/json")]
    public class SolicitacaoControlador : ControllerBase
    {
        #region Atbutos

        private readonly ISolicitacao _repositorio;

        #endregion

        #region Contrutores
        public SolicitacaoControlador(ISolicitacao repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Pegar todas solicitacoes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todas solicitacoes</response>
        /// <response code="204">Não existe solicitação</response>
        [HttpGet("todas")]
        [Authorize]
        public async Task<ActionResult> PegarTodasSolicitacoesAsync()
        {
            var lista = await _repositorio.PegarTodasSolicitacoesAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar solicitacao pelo Id da ONG
        /// </summary>
        /// <param name="idONG">Id da ONG</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a solicitacao</response>
        /// <response code="404">Usuario não existente</response>
        [HttpGet("id/{idONG}")]
        [Authorize]
        public async Task<ActionResult> PegarSolicitacaoPeloIdONGAsync([FromRoute] int idONG)
        {
            try
            {
                return Ok(await _repositorio.PegarSolicitacaoPeloIdONGAsync(idONG));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Criar nova solicitacao
        /// </summary>
        /// <param name="dto">NovaSolicitacaoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Solicitacoes
        ///     {
        ///        "idONG": 1,
        ///        "idDoacao": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna solicitacao criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovaSolicitacaoAsync([FromBody] NovaSolicitacaoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _repositorio.NovaSolicitacaoAsync(dto);

                return Created($"api/Solicitacoes/todas", dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletar solicitacao pelo Id
        /// </summary>
        /// <param name="idsolicitacao">Id da solicitação</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [HttpDelete("delete/{idsolicitacao}")]
        [Authorize]
        public async Task<ActionResult> DeletarSolicitacaoAsync([FromRoute] int idsolicitacao)
        {
            try
            {
                await _repositorio.DeletarSolicitacaoAsync(idsolicitacao);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        #endregion
    }
}
