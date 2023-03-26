using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqcoxn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>
    /// Classe de regras de negócio da tabela CONTA
    /// </summary>
    internal class RegrasNegocio : AplicacaoRegraNegocio
    {
        /// <summary>Número da agência deve ser maior que zero.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN01(TOConta toConta)
        {
            if (toConta.CodAgencia <= 0)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN01));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Número da conta deve ser maior que zero.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN02(TOConta toConta)
        {
            if (toConta.CodConta <= 0)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN02));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN03(TOConta toConta)
        {
            if ((toConta.TipoPessoa == "F" && toConta.CodEspecie != 35) || (toConta.TipoPessoa == "J" && toConta.CodEspecie != 6))
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN03));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Caso exista limite, este deve ser maior que zero.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN04(TOConta toConta)
        {
            if (toConta.Limite.TemConteudo && toConta.Limite <= 0)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN04));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Saldo inicial deve ser igual a zero.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN05(TOConta toConta)
        {
            if (toConta.Saldo != 0)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN05));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Para "Sacar" e "Depositar", o valor da transação deve ser maior que zero.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN06(TOConta toConta)
        {
            if (toConta.ValorTransacao <= 0)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN06));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Valor solicitado para saque não pode ser maior que o saldo.</summary>
        /// <param name="toConta"></param>
        /// <returns>True se validado com êxito.</returns>
        public Retorno<Int32> RN07(TOConta toConta)
        {
            if (toConta.ValorTransacao > toConta.Saldo)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.RN07, toConta.Saldo.ToString()));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }
    }
}
