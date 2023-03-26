using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>Tipos de falha para a RN de Conta</summary>
    public enum TipoFalha
    {
        /// <summary>Número da agência deve ser maior que zero.</summary>
        RN01,
        /// <summary>Número da conta deve ser maior que zero.</summary>
        RN02,
        /// <summary>Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.</summary>
        RN03,
        /// <summary>Caso exista limite, este deve ser maior que zero.</summary>
        RN04,
        /// <summary>Saldo inicial deve ser igual a zero.</summary>
        RN05,
        /// <summary>Para "Sacar" e "Depositar", o valor da transação deve ser maior que zero.</summary>
        RN06,
        /// <summary>Valor solicitado para saque maior que o saldo.</summary>
        RN07,
    }

    /// <summary>Classe de mensagens para a RN de Conta</summary>
    public class MensagemConta : Mensagem
    {
        public MensagemConta(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.RN01:
                    this.mensagem = "Agência inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.RN02:
                    this.mensagem = "Conta inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.RN03:
                    this.mensagem = "Espécie inválida. Deve ser 35 para pessoa física e 06 para pessoa jurídica.";
                    break;
                case TipoFalha.RN04:
                    this.mensagem = "Limite deve ser maior que zero.";
                    break;
                case TipoFalha.RN05:
                    this.mensagem = "Saldo inicial deve ser zero.";
                    break;
                case TipoFalha.RN06:
                    this.mensagem = "Valor da transação deve ser maior que zero.";
                    break;
                case TipoFalha.RN07:
                    this.mensagem = String.Format("Saldo insuficiente. Total disponível para saque é {0}.", Double.Parse(parametro[0]).ToString("0,0.00"));
                    break;
                default:
                    break;
            }
        }
    }
}
