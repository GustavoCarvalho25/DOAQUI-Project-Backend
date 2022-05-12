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
    public interface ISolicitacao
    {
        Task<List<SolicitacaoModelo>> PegarTodasSolicitacoesAsync();
        Task <SolicitacaoModelo> PegarSolicitacaoPeloCnpjAsync(int cnpj);
        Task NovaSolicitacaoAsync(NovaSolicitacaoDTO solicitacao);
        Task AtualizarSolicitacaoAsync(AtualizarSolicitacaoDTO solicitacao);
        Task DeletarSolicitacaoAsync(int id);
    }

}