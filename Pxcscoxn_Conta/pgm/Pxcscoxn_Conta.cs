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
    /// Classe de acesso a tabela CONTA
    /// </summary>
    public class RNConta : AplicacaoRegraNegocio
    {
        private RegrasNegocio rn = new RegrasNegocio();

        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toConta">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOConta>> Listar(TOConta toConta)
        {
            try
            {
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<List<TOConta>> retListar = bdConta.Listar(toConta);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOConta>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOConta>>(retListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOConta>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toConta">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOConta toConta)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toConta.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toConta.Saldo.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("SALDO"));
                }
                if (!toConta.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion

                #region Validação das regras de negócio
                Retorno<Int32> retornoValidacao;

                retornoValidacao = this.rn.RN01(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }

                retornoValidacao = this.rn.RN02(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }

                retornoValidacao = this.rn.RN03(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }

                retornoValidacao = this.rn.RN04(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }

                retornoValidacao = this.rn.RN05(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }
                #endregion

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdConta.Incluir(toConta);
                    if (!retIncluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retIncluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retIncluir.Dados, new OperacaoRealizadaMensagem("Inclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion

                #region Validação das regras de negócio
                //Não é permitido alterar nenhum campo além do limite da conta.
                toConta.CodCliente = new Pxcoiexn.BD.CampoTabela<double>();
                toConta.TipoPessoa = new Pxcoiexn.BD.CampoTabela<string>();
                toConta.Saldo = new Pxcoiexn.BD.CampoTabela<double>();

                Retorno<Int32> retornoValidacao;

                retornoValidacao = this.rn.RN04(toConta);
                if (!retornoValidacao.Ok)
                {
                    return retornoValidacao;
                }
                #endregion

                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toConta);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retAlterar.Dados, new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Efetua um saque na conta com o valor contido na propriedade 'ValorTransacao'
        /// </summary>
        /// <param name="toConta">Campos para alteração de saque</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Sacar(TOConta toConta)
        {
            Retorno<Int32> retornoValidacao = this.rn.RN06(toConta);
            if (!retornoValidacao.Ok)
            {
                return retornoValidacao;
            }
            retornoValidacao = this.rn.RN07(toConta);
            if (!retornoValidacao.Ok)
            {
                return retornoValidacao;
            }

            toConta.Saldo -= toConta.ValorTransacao;
            toConta.ValorTransacao = 0;
            return this.Alterar(toConta);
        }

        /// <summary>
        /// Efetua um depósito na conta com o valor contido na propriedade 'ValorTransacao'
        /// </summary>
        /// <param name="toConta">Campos para alteração de depósito</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Depositar(TOConta toConta)
        {
            Retorno<Int32> retornoValidacao = this.rn.RN06(toConta);
            if (!retornoValidacao.Ok)
            {
                return retornoValidacao;
            }

            toConta.Saldo += toConta.ValorTransacao;
            toConta.ValorTransacao = 0;
            return this.Alterar(toConta);
        }

        /// <summary>
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toConta">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdConta.Excluir(toConta);
                    if (!retExcluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retExcluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retExcluir.Dados, new OperacaoRealizadaMensagem("Exclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }
        #endregion
    }
}
