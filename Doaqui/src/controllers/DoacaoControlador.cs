using Doaqui.src.dtos;
using Microsoft.AspNetCore.Mvc;
using Doaqui.src.repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Doaqui.src.models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Doaqui.src.controllers
{
    [ApiController]
    [Route("api/Doacoes")]
    [Produces("application/json")]
    public class DoacaoControlador :ControllerBase
    {
        #region Atributos

        private readonly IDoacao _repositorio;
        
        #endregion

        #region Construtores

        public DoacaoControlador(IDoacao repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Pegar todas doacoes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de doações</response>
        /// <response code="204">Não existe doações</response>
        [HttpGet("todas")]
        [Authorize]
        public async Task<ActionResult> PegarTodasDoacoesAsync()
        {
            var lista = await _repositorio.PegarTodasDoacoesAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar doacao pelo Id
        /// </summary>
        /// <param name="idDoacao">Id da doação</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a doação</response>
        /// <response code="404">Doação não existente</response>
        [HttpGet("id/{idDoacao}")]
        [Authorize]
        public async Task<ActionResult> PegarDoacaoPeloIdAsync([FromRoute] int idDoacao)
        {
            try
            {
                return Ok(await _repositorio.PegarDoacaoPeloIdAsync(idDoacao));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Criar nova doacao
        /// </summary>
        /// <param name="dto">NovaDoacaoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Doacoes
        ///     {
        ///        "titulo": "Doação de cesta Básica",    
        ///        "descricao": "Cestas para doação comunitaria",
        ///        "contato": "empresa@email.com",
        ///        "quantidade": 25,
        ///        "limite": 5,
        ///        "validade": "dd/mm/aaaa",
        ///        "foto": "URL DA FOTO",
        ///        "cNPJDoador": "999.999.999.99"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna doacao feita</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NovaDoacaoAsync([FromBody] NovaDoacaoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repositorio.NovaDoacaoAsync(dto);

            return Created($"api/Doacoes/todas", dto);
        }

        /// <summary>
        /// Atualizar Doacao
        /// </summary>
        /// <param name="dto">AtualizarDoacaoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Doacoes
        ///     {
        ///        "id": 1    
        ///        "titulo": "Doação de cesta Básica",    
        ///        "descricao": "Cestas para doação comunitaria",
        ///        "contato": "empresa@email.com",
        ///        "quantidade": 25,
        ///        "limite": 5,
        ///        "validade": "dd/mm/aaaa",
        ///        "foto": "URL DA FOTO",
        ///        "cNPJDoador": "999.999.999.99"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna doacao atualizada</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarDoacaoAsync([FromBody] AtualizarDoacaoDTO dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _repositorio.AtualizarDoacaoAsync(dto);
            
            return Ok(dto);
        }

        /// <summary>
        /// Deletar doacao pelo Id
        /// </summary>
        /// <param name="idDoacao">Id da doação</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Doacaio deletada</response>
        [HttpDelete("deletar/{idDoacao}")]
        [Authorize]
        public async Task<ActionResult> DeletarDoacaoAsync([FromRoute] int idDoacao)
        {
            try
            {
                await _repositorio.DeletarDoacaoAsync(idDoacao);
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