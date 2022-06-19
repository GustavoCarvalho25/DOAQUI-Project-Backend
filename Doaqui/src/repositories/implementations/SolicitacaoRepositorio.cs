using Doaqui.src.data;
using Doaqui.src.dtos;
using Doaqui.src.models;
using Doaqui.src.utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doaqui.src.repositories.implementations
{

    /// <summary>
    /// <para> Summary: Request repository implementation of ISolicitacao interface. </para>
    /// <para> Created by: Nickole Bueno </para>
    /// <para> Version: 1.0 </para>
    /// <para> Date: 05/05/2022 </para>
    /// </summary>
    public class SolicitacaoRepositorio : ISolicitacao
    {

        #region Attributes
        private readonly DoaquiContexto _contexto;
        #endregion


        #region Constructors
        public SolicitacaoRepositorio(DoaquiContexto context)
        {
            _contexto = context;
        }
        #endregion


        #region Methods

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas solicitacoes</para>
        /// </summary>
        /// <return>Lista SolicitacoesModelo</return>
        public async Task<List<SolicitacaoModelo>> PegarTodasSolicitacoesAsync()
        {
            return await _contexto.Solicitacoes
                .Include(s => s.ONG)
                .Include(s => s.Doacao)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma solicitacao pelo Cnpj</para>
        /// </summary>
        /// <param name="idONG">Id da solicitacao</param>
        /// <return>Lista SolicitacoesModelo</return>
        /// <exception cref="Exception">Caso não encontre a ONG</exception>
        public async Task<List<SolicitacaoModelo>> PegarSolicitacaoPeloIdONGAsync(int idONG)
        {
            if (!ExisteId(idONG)) throw new Exception("Id da ONG não encontrado");

            return await _contexto.Solicitacoes
                .Include(s => s.ONG)
                .Include(s => s.Doacao)
                .Where(s => s.ONG.Id == idONG)
                .ToListAsync();

            // função auxiliar
            bool ExisteId(int idONG)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == idONG);
                return auxiliar != null;
            }
        }

         /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova solicitacao</para>
        /// </summary>
        /// <param name="dto">NovaSolicitacaoDTO</param>
        public async Task NovaSolicitacaoAsync(NovaSolicitacaoDTO dto)
        {
            if (!ExisteIdONG(dto.IdONG)) throw new Exception("Id da ONG não encontrado");

            if (!ExisteIdDoacao(dto.IdDoacao)) throw new Exception("Id da doação não encontrado");

            await RegraRestaInativa(dto.IdDoacao);

            await _contexto.Solicitacoes.AddAsync(new SolicitacaoModelo
            {
                ONG = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == dto.IdONG),
                Doacao = await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == dto.IdDoacao)
            });
            await _contexto.SaveChangesAsync();

            // função auxiliar
            bool ExisteIdONG(int idONG)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == idONG);
                return auxiliar != null;
            }

            bool ExisteIdDoacao(int idDoacao)
            {
                var auxiliar = _contexto.Doacoes.FirstOrDefault(d => d.Id == idDoacao);
                return auxiliar != null;
            }

            async Task RegraRestaInativa(int idDoacao)
            {
                var aux1 = await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == idDoacao);
                var result1 = aux1.Quantidade - aux1.Limite;

                if (result1 <= 0)
                {
                    aux1.Quantidade = 0;
                    aux1.Status = StatusDoacao.INATIVO;
                }
                else
                {
                    aux1.Quantidade = result1;
                }
                await _contexto.SaveChangesAsync();
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma solicitacao</para>
        /// </summary>
        /// <param name="id">Id da solicitacao</param>
        public async Task DeletarSolicitacaoAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id da solicitação não encontrado");

            _contexto.Solicitacoes.Remove(await _contexto.Solicitacoes.FirstOrDefaultAsync(s => s.Id == id));
            await _contexto.SaveChangesAsync();
        
            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Solicitacoes.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        #endregion

    }
}
