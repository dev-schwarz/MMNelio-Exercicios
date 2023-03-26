using System;
using System.Collections.Generic;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqclxn;
using Bergs.Pxc.Pxcoiexn.Interface;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>
    /// Classe de acesso a tabela CLIENTE
    /// </summary>
    public class RNCliente : AplicacaoRegraNegocio
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCliente">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOCliente>> Listar(TOCliente toCliente)
        {
            try
            {
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<List<TOCliente>> retListar = bdCliente.Listar(toCliente);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCliente>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOCliente>>(retListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCliente>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOCliente toCliente)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toCliente.NomeCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NOME_CLIENTE"));
                }
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion

                #region Validação das regras de negócio
                Retorno<Int32> retornoValidacao;

                //VALIDANDO O TIPO DE PESSOA
                //Verificando se o Tipo de Pessoa deste cliente é válido (somente 'F' ou 'J' são aceitos).
                retornoValidacao = this.RN01_ValidaTipoDePessoa(toCliente);
                if (!retornoValidacao.Ok)
                {
                    //Se o retorno NÃO está OK, então ele é uma falha, então vamos retorná-lo.
                    return retornoValidacao;
                }

                //VALIDANDO O CPF OU CNPJ, CONFORME O TIPO DE PESSOA DO CLIENTE.
                //Verificando se o tipo de pessoa deste cliente é Física (F) ou Jurídica (J).
                if (toCliente.TipoPessoa == "F")
                {
                    //Cliente Pessoa Física, então precisamos validar o seu CPF.
                    retornoValidacao = this.RN02_ValidaCpf(toCliente);
                }
                else
                {
                    //Cliente Pessoa Jurídica, então precisamos validar o seu CNPJ.
                    retornoValidacao = this.RN03_ValidaCnpj(toCliente);
                }
                //Agora que já validamos o CPF ou o CNPJ do cliente (dependendo do seu Tipo de Pessoa),
                //vamos verificar se o que foi retornado dessa validação está OK ou Não.
                //Não estando OK, vamos retornar com esse valor que está com falha, interrompendo
                //a execução da inclusão por erro de validação.
                if (!retornoValidacao.Ok)
                {
                    //Se o retorno NÃO está OK, então ele é uma falha, então vamos retorná-lo.
                    return retornoValidacao;
                }

                //VALIDANDO O NOME DO CLIENTE
                retornoValidacao = this.RN04_ValidaNomeCliente(toCliente);
                if (!retornoValidacao.Ok)
                {
                    //Se o retorno NÃO está OK, então ele é uma falha, então vamos retorná-lo.
                    return retornoValidacao;
                }
                #endregion

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdCliente.Incluir(toCliente);
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
        /// <param name="toCliente">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion

                #region Validação das regras de negócio
                //Verificando se o Nome do Cliente foi preenchido pelo usuário no formulário onde o nosso
                //cliente está sendo alterado. Nesse caso, teremos que validar o novo Nome do Cliente, para
                //que não viole as nossas regras de negócios.
                if (toCliente.NomeCliente.TemConteudo)
                {
                    //VALIDANDO O NOME DO CLIENTE
                    Retorno<Int32> retornoValidacao = this.RN04_ValidaNomeCliente(toCliente);
                    if (!retornoValidacao.Ok)
                    {
                        //Se o retorno NÃO está OK, então ele é uma falha, então vamos retorná-lo.
                        return retornoValidacao;
                    }
                }
                #endregion

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdCliente.Alterar(toCliente);
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
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdCliente.Excluir(toCliente);
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

        #region Métodos de validação de regras de negócio
        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="toCliente"></param>
        /// <returns>True se o Tipo de Pessoa for válido.</returns>
        private Retorno<Int32> RN01_ValidaTipoDePessoa(TOCliente toCliente)
        {
            if (toCliente.TipoPessoa.LerConteudoOuPadrao() != "F" && toCliente.TipoPessoa.LerConteudoOuPadrao() != "J")
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.Falha_RN1_TipoDePessoaInvalida));
            }
            //Esta linha de retorno com sucesso nunca vai mudar para
            //todos os métodos de validação que criarmos a mão.
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem()); ;
        }

        /// <summary>Valida um número de CPF.</summary>
        /// <param name="toCliente"></param>
        /// <returns>True se o CPF for válido.</returns>
        private Retorno<Int32> RN02_ValidaCpf(TOCliente toCliente)
        {
            if (!Util.ValidaCpf(toCliente.CodCliente.LerConteudoOuPadrao().ToString().PadLeft(11, '0')))
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.Falha_RN2_CpfInvalido));
            }
            //Esta linha de retorno com sucesso nunca vai mudar para
            //todos os métodos de validação que criarmos a mão.
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem()); ;
        }

        /// <summary>Valida um número de CNPJ.</summary>
        /// <param name="toCliente"></param>
        /// <returns>True se o CNPJ for válido.</returns>
        private Retorno<Int32> RN03_ValidaCnpj(TOCliente toCliente)
        {
            if (!Util.ValidaCnpj(toCliente.CodCliente.LerConteudoOuPadrao().ToString().PadLeft(14, '0')))
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.Falha_RN3_CnpjInvalido));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }

        /// <summary>Valida o Nome do cliente, que deve ter 2 nomes, e no mínimo 2 letras no primeiro nome.</summary>
        /// <param name="toCliente"></param>
        /// <returns>True se o CNPJ for válido.</returns>
        private Retorno<Int32> RN04_ValidaNomeCliente(TOCliente toCliente)
        {
            //Dividimos os nomes do cliente, separando-os pelo espaço em branco que deve existir entre
            //cada um dos nomes, e armazenamos esses nome separados em uma array de String.
            String[] partesDoNome = toCliente.NomeCliente.LerConteudoOuPadrao().Trim().Split(' ');

            //Total de partes da array deve ser igual a 2,
            //e total de letras no primeiro nome também deve ser igual a 2.
            //Se qualquer um deles for diferente disso, retorna a falha.
            if (partesDoNome.Length != 2 || partesDoNome[0].Length < 2)
            {
                return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.Falha_RN4_NomeClienteInvalido));
            }
            return this.Infra.RetornarSucesso<Int32>(1, new OperacaoRealizadaMensagem());
        }
        #endregion
    }
}
