using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Tipos de falha para a RN de Cliente</summary>
    public enum TipoFalha
    {
        Falha_RN1_TipoDePessoaInvalida,
        Falha_RN2_CpfInvalido,
        Falha_RN3_CnpjInvalido,
        Falha_RN4_NomeClienteInvalido
    }

    /// <summary>Classe de mensagens para a RN de Cliente</summary>
    public class MensagemCliente : Mensagem
    {
        public MensagemCliente(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.Falha_RN1_TipoDePessoaInvalida:
                    this.mensagem = "Tipo pessoa deve ser 'F' ou 'J'.";
                    break;
                case TipoFalha.Falha_RN2_CpfInvalido:
                    this.mensagem = "CPF inválido.";
                    break;
                case TipoFalha.Falha_RN3_CnpjInvalido:
                    this.mensagem = "CNPJ inválido.";
                    break;
                case TipoFalha.Falha_RN4_NomeClienteInvalido:
                    this.mensagem = "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.";
                    break;
                default:
                    break;
            }
        }
    }
}
