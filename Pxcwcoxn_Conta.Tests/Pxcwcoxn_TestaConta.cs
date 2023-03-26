using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcscoxn;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwcoxn.Tests
{
    [TestFixture(Description = "Classe de testes de conta")]
    public class TestaConta : AplicacaoTela
    {
        #region
        private List<ContaMensagem> mensagens = new List<ContaMensagem>();
        #endregion

        #region Construtores
        public TestaConta() : base(@"C:\soft\pxc\data\Pxcz01da.mdb") { }
        #endregion

        #region Métodos de CRUD
        public void Incluir(Object o)
        {
            Console.WriteLine();
            //this.IncluirPessoaFisica();
            Console.WriteLine();
            //this.IncluirPessoaJuridica();
        }
        /// <summary>Incluir um novo conta do tipo Pessoa Física na base de dados.</summary>
        [Test(Description = "Incluir uma nova conta do tipo Pessoa Física na base de dados.")]
        public void IncluirContaPessoaFisica()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            TOConta toConta = new TOConta()
            {
                CodCliente = 00998966053,
                CodAgencia = 1000,
                CodConta = 101001,
                TipoPessoa = "F",
                CodEspecie = 35,
                Limite = 10,
                Saldo = 0,
            };

            try
            {
                Retorno<Int32> retornoIncluir = rnConta.Incluir(toConta);

                Assert.IsTrue(retornoIncluir.Ok, retornoIncluir.Mensagem.ParaOperador);

                toConta = new TOConta()
                {
                    TipoPessoa = "F",
                    CodCliente = 00998966053,
                };
                //Listando as contas filtrando pelo CodCliente acima.
                Retorno<List<TOConta>> retornoListar = rnConta.Listar(toConta);
                Assert.IsTrue(retornoListar.Ok, retornoListar.Mensagem.ParaOperador);
                Assert.AreEqual(retornoListar.Dados.Count, 1);
                Assert.AreEqual(retornoListar.Dados[0].CodCliente.LerConteudoOuPadrao().ToString(), toConta.CodCliente.ToString());
                //Excluíndo o conta da base de dados para limpeza.
                Retorno<Int32> retornoExcluir = rnConta.Excluir(toConta);
                Assert.IsTrue(retornoExcluir.Ok, retornoExcluir.Mensagem.ParaOperador);
                Console.WriteLine("Incluir [Pessoa Física] - PASSOU - Conta do tipo Pessoa Física foi incluída na base de dados com êxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incluir [Pessoa Física] - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }
        /// <summary>Incluir um novo conta do tipo Pessoa Jurídica na base de dados.</summary>
        //[Test(Description = "Incluir um novo conta do tipo Pessoa Jurídica na base de dados.")]
        //public void IncluirPessoaJuridica()
        //{
        //    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

        //    TOConta toConta = new TOConta()
        //    {
        //        CodCliente = 47334689000154,
        //        TipoPessoa = "J",
        //        NomeCliente = "Flinders Fleury",
        //        DataNasc = new DateTime(2000, 10, 24),
        //        Telefone = 51999998888,
        //        RatingCliente = "A",
        //        RendaFamiliar = 10000.00,
        //        DataAtuRating = DateTime.Today,
        //        DataCadastro = DateTime.Today,
        //    };

        //    try
        //    {
        //        Retorno<Int32> retornoIncluir = rnConta.Incluir(toConta);

        //        Assert.IsTrue(retornoIncluir.Ok, retornoIncluir.Mensagem.ParaOperador);

        //        toConta = new TOConta()
        //        {
        //            TipoPessoa = "J",
        //            CodCliente = 47334689000154,
        //        };
        //        //Listando os contas filtrando pelo CodCliente acima.
        //        Retorno<List<TOConta>> retornoListar = rnConta.Listar(toConta);
        //        Assert.IsTrue(retornoListar.Ok, retornoListar.Mensagem.ParaOperador);
        //        Assert.AreEqual(retornoListar.Dados.Count, 1);
        //        Assert.AreEqual(retornoListar.Dados[0].CodCliente.LerConteudoOuPadrao().ToString(), toConta.CodCliente.ToString());
        //        //Excluíndo o conta da base de dados para limpeza.
        //        Retorno<Int32> retornoExcluir = rnConta.Excluir(toConta);
        //        Assert.IsTrue(retornoExcluir.Ok, retornoExcluir.Mensagem.ParaOperador);
        //        Console.WriteLine("Incluir [Pessoa Jurídica] - PASSOU - Cliente do tipo Pessoa Jurídica foi incluído, encontrado e excluído da base de dados.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Incluir [Pessoa Jurídica] - FALHOU - Retornou:\n{0}", ex.Message);
        //    }
        //}

        //private void Alterar(Object o)
        //{
        //    Console.WriteLine();
        //    this.AlterarPessoaFisica();
        //}
        /// <summary>Altera um conta do tipo Pessoa Física na base de dados.</summary>
        //[Test(Description = "Altera um conta do tipo Pessoa Física na base de dados.")]
        //public void AlterarPessoaFisica()
        //{
        //    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
        //    TOConta toConta = this.PopularCamposObrigatorios("F");

        //    try
        //    {
        //        //Incluíndo um conta de forma temporária, para posteriormente fazer a sua alteração.
        //        this.AuxIncluirEValidar(toConta);

        //        //Alterando os valores dos campos do conta.
        //        toConta.NomeCliente = "Nome alterado";
        //        toConta.Telefone = 1234567990;
        //        //Efetuando a alteração no banco de dados.
        //        Retorno<Int32> retornoAlterar = rnConta.Alterar(toConta);
        //        //Validando o retorno da alteração.
        //        Assert.IsTrue(retornoAlterar.Ok, retornoAlterar.Mensagem.ParaOperador);

        //        //Obtendo o conta do banco de dados para verificar a efetividade da alteração.
        //        Retorno<TOConta> retornoObter = this.AuxObterEValidar(toConta);
        //        TOConta toContaObtido = retornoObter.Dados;
        //        //Comparando o conta obtido do banco de dados com o original.
        //        if (toConta.CodCliente != toContaObtido.CodCliente || toConta.NomeCliente != toContaObtido.NomeCliente || toConta.Telefone != toContaObtido.Telefone)
        //        {
        //            throw new Exception("Cliente não foi alterado com sucesso no banco de dados (inconsistência nos valores obtidos).");
        //        }

        //        //Excluíndo o conta anteriormente inserido de forma temporária.
        //        this.AuxExcluirEValidar(toConta);

        //        Console.WriteLine("Alterar [Pessoa Física] - PASSOU - Cliente do tipo Pessoa Física foi alterado com êxito na base de dados.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Alterar - FALHOU - Retornou:\n{0}", ex.Message);
        //    }
        //}

        //private void ConsultarTest(Object o)
        //{
        //    Console.WriteLine();
        //    this.Consultar();
        //}
        /// <summary>Consultar um conta na base de dados.</summary>
        /// <param name="o"></param>
        //[Test(Description = "Consultar um conta na base de dados.")]
        //public void Consultar()
        //{
        //    TOConta toConta = this.PopularCamposObrigatorios("J");

        //    try
        //    {
        //        //Incluíndo um conta de forma temporária, para posteriormente consultá-lo.
        //        this.AuxIncluirEValidar(toConta);

        //        //Obtendo o conta do banco de dados para verificar a efetividade da alteração.
        //        Retorno<TOConta> retornoObter = this.AuxObterEValidar(toConta);
        //        TOConta toContaObtido = retornoObter.Dados;
        //        //Comparando o conta obtido do banco de dados com o original.
        //        if (toConta.CodCliente != toContaObtido.CodCliente || toConta.Telefone != toContaObtido.Telefone)
        //        {
        //            throw new Exception("Cliente não foi encontrado com sucesso no banco de dados (inconsistência nos valores obtidos).");
        //        }

        //        //Excluíndo o conta anteriormente inserido de forma temporária.
        //        this.AuxExcluirEValidar(toConta);

        //        Console.WriteLine("Consultar [Pessoa {0}] - PASSOU - Cliente do tipo Pessoa {0} foi obtido com êxito da base de dados.",
        //            toConta.TipoPessoa == "F" ? "Física" : "Jurídica");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Consultar - FALHOU - Retornou:\n{0}", ex.Message);
        //    }
        //}

        //private void ExcluirTest(Object o)
        //{
        //    Console.WriteLine();
        //    this.Excluir();
        //}

        /// <summary>Excluir um conta da base de dados.</summary>
        /// <param name="o"></param>
        //[Test(Description = "Excluir um conta da base de dados.")]
        //public void Excluir()
        //{
        //    TOConta toConta = this.PopularCamposObrigatorios("F");

        //    try
        //    {
        //        //Incluíndo um conta de forma temporária, para posteriormente consultá-lo.
        //        this.AuxIncluirEValidar(toConta);

        //        //Excluíndo o conta anteriormente inserido de forma temporária.
        //        this.AuxExcluirEValidar(toConta);

        //        Console.WriteLine("Excluir - PASSOU - Cliente foi excluído com êxito da base de dados.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Excluir - FALHOU - Retornou:\n{0}", ex.Message);
        //    }
        //}
        #endregion

        #region Métodos de validação das regras de negócio
        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="o"></param>
        private void RN01(Object o)
        {
            ValidaRN01();
            Console.WriteLine();
        }
        /// <summary>Valida RN01. Número da agência deve ser maior que zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN01 - Número da agência deve ser maior que zero.")]
        public void ValidaRN01()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN01",
                "O número da Agência deve ser maior que zero",
                "Falha",
                "Número da Agência igual a zero",
                "Agência inválida, deve ser maior que zero.",
                "Número da Agência validado com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 0;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN02. Número da conta deve ser maior que zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN01 - Número da conta deve ser maior que zero.")]
        public void ValidaRN02()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN02",
                "Número da Conta deve ser maior que zero",
                "Falha",
                "Número da Conta igual a zero",
                "Conta inválida, deve ser maior que zero.",
                "Número da Conta validado com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 0;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN03. Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN03 - Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.")]
        public void ValidaRN03_EspecieInvalidaParaPessoaFisica()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN03",
                "Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06",
                "Falha",
                "Tipo de Pessoa Física, e Espécie igual a 6",
                "Espécie inválida. Deve ser 35 para pessoa física e 06 para pessoa jurídica.",
                "Espécie validada com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 6;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN03. Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN03 - Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06.")]
        public void ValidaRN03_EspecieInvalidaParaPessoaJuridica()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN03",
                "Para Tipo de Pessoa='F', espécie deve ser igual a 35, para Tipo de Pessoa='J', espécie deve ser igual a 06",
                "Falha",
                "Tipo de Pessoa Jurídica, e Espécie igual a 35, que é de Pessoa Física",
                "Espécie inválida. Deve ser 35 para pessoa física e 06 para pessoa jurídica.",
                "Espécie validada com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "J";
            toConta.Saldo = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN04. Caso exista limite, este deve ser maior que zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN04 - Caso exista limite, este deve ser maior que zero.")]
        public void ValidaRN04()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN04",
                "Caso exista limite, este deve ser maior que zero.",
                "Falha",
                "Limite igual a 0.",
                "Limite deve ser maior que zero.",
                "Limite validado com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 0;
            toConta.Limite = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN05. Saldo inicial deve ser igual a zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN05 - Saldo inicial deve ser igual a zero.")]
        public void ValidaRN05()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN05",
                "Saldo inicial deve ser igual a zero.",
                "Falha",
                "Saldo inicial igual a 1.",
                "Saldo inicial deve ser zero.",
                "Saldo inicial validado com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 1;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Incluir(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN06. Para "Sacar" e "Depositar", o valor da transação deve ser maior que zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN06 - Para 'Sacar' e 'Depositar', o valor da transação deve ser maior que zero.")]
        public void ValidaRN06_SaqueComValorTransacaoIgualA0()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN06",
                "Para 'Sacar' e 'Depositar', o valor da transação deve ser maior que zero.",
                "Falha",
                "Valor da Transação para Saque igual a 0.",
                "Valor da transação deve ser maior que zero.",
                "Valor da Transação validado para Saque com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 6;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 1;
            toConta.ValorTransacao = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Sacar(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN06. Para "Sacar" e "Depositar", o valor da transação deve ser maior que zero.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN06 - Para 'Sacar' e 'Depositar', o valor da transação deve ser maior que zero.")]
        public void ValidaRN06_DepositoComValorTransacaoIgualA0()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN06",
                "Para 'Sacar' e 'Depositar', o valor da transação deve ser maior que zero.",
                "Falha",
                "Valor da Transação para Depósito igual a 0.",
                "Valor da transação deve ser maior que zero.",
                "Valor da Transação validado para Depóstio com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 6;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 1;
            toConta.ValorTransacao = 0;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Depositar(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Valida RN07. Valor solicitado para saque não pode ser maior que o saldo.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN07 - Valor solicitado para saque não pode ser maior que o saldo.")]
        public void ValidaRN07()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN06",
                "Valor solicitado para saque não pode ser maior que o saldo.",
                "Falha",
                "Valor da Transação (1000) maior que o saldo (150).",
                "Saldo insuficiente. Total disponível para saque é 150,00.",
                "Valor da Transação validado para Saque com êxito e não deveria");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();

            //Criando novo TOConta com os campos setados para a realização do teste de validação da RN.
            TOConta toConta = new TOConta();
            toConta.CodConta = 1;
            toConta.CodCliente = 00000000001;
            toConta.CodEspecie = 6;
            toConta.CodAgencia = 1000;
            toConta.TipoPessoa = "F";
            toConta.Saldo = 150;
            toConta.ValorTransacao = 10000;

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnConta.Sacar(toConta);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemStatusValidacao);
                if (mensageiro.Ok)
                {
                    mensagens.Add(new ContaMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ContaMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="o"></param>
        private void TodasRNs(Object o)
        {
            RN01(null);
            //RN02(null);
            //RN03(null);
            //RN04(null);
            //RN05(null);
            //RN06(null);
            //RN07(null);
        }
        #endregion
    }
}
