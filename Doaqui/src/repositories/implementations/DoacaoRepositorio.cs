using Doaqui.src.data;
using Doaqui.src.dtos;
using Doaqui.src.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doaqui.src.repositories.implementations
{

    /// <summary>
    /// <para> Summary: Donation repository implementation of IDoacao interface. </para>
    /// <para> Created by: Nickole Bueno </para>
    /// <para> Version: 1.0 </para>
    /// <para> Date: 05/05/2022 </para>
    /// </summary>
    public class DoacaoRepositorio : IDoacao
    {

        #region Attributes
        private readonly DoaquiContexto _contexto;
        #endregion


        #region Constructors
        public DoacaoRepositorio(DoaquiContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Methods

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar doações</para>
        /// </summary>
        /// <return>Lista DoacaoModelo</return>
        public async Task<List<DoacaoModelo>> PegarTodasDoacoesAsync()
        {
            return await _contexto.Doacoes.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um doação pelo ID</para>
        /// </summary>
        /// <param name="id">Id da doação</param>
        /// <return>DoacaoModelo</return>
        /// <exception cref="Exception">Caso não encontre a doação</exception>
        public async Task<DoacaoModelo> PegarDoacaoPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de doação não encontrado");

            return await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Doacoes.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova doacao</para>
        /// </summary>
        /// <param name="dto">NovaDoacaoDTO</param>
        public async Task NovaDoacaoAsync(NovaDoacaoDTO dto)
        {
            await _contexto.Doacoes.AddAsync(new DoacaoModelo
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Contato = dto.Contato,
                Quantidade = dto.Quantidade,
                Validade = dto.Validade,
                Foto = dto.Foto,
                CNPJDoador = dto.CNPJDoador,
            });
           await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma doacao</para>
        /// </summary>
        /// <param name="dto">AtualizarDoacaoDTO</param>
        public async Task AtualizarDoacaoAsync(AtualizarDoacaoDTO dto)
        {
            var modelo = await PegarDoacaoPeloIdAsync(dto.Id);
            modelo.Titulo = dto.Titulo;
            modelo.Descricao = dto.Descricao;
            modelo.Contato = dto.Contato;
            modelo.Quantidade = dto.Quantidade;
            modelo.Validade = dto.Validade;
            modelo.Foto = dto.Foto;
            modelo.CNPJDoador = dto.CNPJDoador;
            _contexto.Update(modelo);
           await _contexto.SaveChangesAsync();
        } 

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma doacao</para>
        /// </summary>
        /// <param name="id">Id da doacao</param>
        public async Task DeletarDoacaoAsync(int id)
        {
            _contexto.Doacoes.Remove(await PegarDoacaoPeloIdAsync(id));
           await _contexto.SaveChangesAsync();
        }

        #endregion

    }
}
