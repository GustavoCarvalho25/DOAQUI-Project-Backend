using System.Text.Json.Serialization;

namespace Doaqui.src.utilidades
{
    /// <summary>
    /// <para>Resumo: Enum responsavel por definir Tipos de usuario do sistema</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        ONG,
        ADMINISTRADOR
    }

    /// <summary>
    /// <para>Resumo: Enum responsavel por definir Status Doacao</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusDoacao
    {
        ATIVO,
        INATIVO
    }
}