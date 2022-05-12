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
    public interface IDoacao
    {
        List<DoacaoModelo> PegarTodasDoacoes();
        Task <DoacaoModelo> PegarDoacaoPeloIdAsync(int id);
        Task <DoacaoModelo> PegarDoacaoPeloCnpjAsync(int cnpj);
        Task <DoacaoModelo> PegarDoacaoPeloContatoAsync(string contato);
        Task NovaDoacaoAsync(NovaDoacaoDTO doacao);
        Task AtualizarDoacaoAsync(AtualizarDoacaoDTO doacao);
        Task DeletarDoacaoAsync(int id);
    }
}