using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsclxn;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwclxn.Tests
{
    [TestFixture(Description = "Classe de testes de cliente")]
    public class TestaClienteComTela : AplicacaoTela
    {
        #region Atributos
        private List<ClienteMensagem> mensagens = new List<ClienteMensagem>();
        #endregion

        #region Construtores
        public TestaClienteComTela() : base(@"C:\soft\pxc\data\Pxcz01da.mdb") { }
        #endregion

        #region Método de controle e exibição do menu de testes
        public void Executar()
        {
            try
            {
                mensagens = new List<ClienteMensagem>();
                Menu menu = new Menu(
                    new ItemMenu[]
                    {
                        new ItemMenu(new KeyValuePair<int, string>(1, "Incluir"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int, string>(2, "Alterar"), Alterar, false),
                        new ItemMenu(new KeyValuePair<int, string>(3, "Consultar"), ConsultarTest, false),
                        new ItemMenu(new KeyValuePair<int, string>(4, "Excluir"), ExcluirTest, false),
                        new ItemMenu(new KeyValuePair<int, string>(5, "Valida RN01 - Tipo de Pessoa deve ser 'F' ou 'J'"), RN01, false),
                        new ItemMenu(new KeyValuePair<int, string>(6, "Valida RN02 - Cliente Pessoa Física deve ter CPF válido"), RN02, false),
                        new ItemMenu(new KeyValuePair<int, string>(7, "Valida RN03 - Cliente Pessoa Jurídica deve ter CNPJ válido"), RN03, false),
                        new ItemMenu(new KeyValuePair<int, string>(8, "Valida RN04 - Nome do cliente deve ter 2 nomes, e no mínimo 2 letras no primeiro nome"), RN04, false),
                        new ItemMenu(new KeyValuePair<int, string>(9, "Valida Todas RNs"), TodasRNs, false),
                        new ItemMenu(new KeyValuePair<int, string>(0, "Voltar"), null, true, false),
                    },
                    null
                    );
                Tela.ControlaMenu("MENU DE TESTES DE CLIENTES | Banrisul | Carlos Schwarz - 25/03/23", menu);
            }
            catch (Exception ex)
            {
                Console.Write("Erro {0}\nTecle algo...", ex.Message);
            }
        }
        #endregion

        #region Métodos de CRUD
        public void Incluir(Object o)
        {
            Console.WriteLine();
            this.IncluirPessoaFisica();
            Console.WriteLine();
            this.IncluirPessoaJuridica();
        }
        /// <summary>Incluir um novo cliente do tipo Pessoa Física na base de dados.</summary>
        [Test(Description = "Incluir um novo cliente do tipo Pessoa Física na base de dados.")]
        public void IncluirPessoaFisica()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente()
            {
                CodCliente = 00998966053,
                TipoPessoa = "F",
                NomeCliente = "Flinders Fleury",
                DataNasc = new DateTime(2000, 10, 24),
                Telefone = 51999998888,
                RatingCliente = "A",
                RendaFamiliar = 10000.00,
                DataAtuRating = DateTime.Today,
                DataCadastro = DateTime.Today,
            };

            try
            {
                Retorno<Int32> retornoIncluir = rnCliente.Incluir(toCliente);

                Assert.IsTrue(retornoIncluir.Ok, retornoIncluir.Mensagem.ParaOperador);

                toCliente = new TOCliente()
                {
                    TipoPessoa = "F",
                    CodCliente = 00998966053,
                };
                //Listando os clientes filtrando pelo CodCliente acima.
                Retorno<List<TOCliente>> retornoListar = rnCliente.Listar(toCliente);
                Assert.IsTrue(retornoListar.Ok, retornoListar.Mensagem.ParaOperador);
                Assert.AreEqual(retornoListar.Dados.Count, 1);
                Assert.AreEqual(retornoListar.Dados[0].CodCliente.LerConteudoOuPadrao().ToString(), toCliente.CodCliente.ToString());
                //Excluíndo o cliente da base de dados para limpeza.
                Retorno<Int32> retornoExcluir = rnCliente.Excluir(toCliente);
                Assert.IsTrue(retornoExcluir.Ok, retornoExcluir.Mensagem.ParaOperador);
                Console.WriteLine("Incluir [Pessoa Física] - PASSOU - Cliente do tipo Pessoa Física foi incluído, encontrado e excluído da base de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incluir [Pessoa Física] - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }
        /// <summary>Incluir um novo cliente do tipo Pessoa Jurídica na base de dados.</summary>
        [Test(Description = "Incluir um novo cliente do tipo Pessoa Jurídica na base de dados.")]
        public void IncluirPessoaJuridica()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente()
            {
                CodCliente = 47334689000154,
                TipoPessoa = "J",
                NomeCliente = "Flinders Fleury",
                DataNasc = new DateTime(2000, 10, 24),
                Telefone = 51999998888,
                RatingCliente = "A",
                RendaFamiliar = 10000.00,
                DataAtuRating = DateTime.Today,
                DataCadastro = DateTime.Today,
            };

            try
            {
                Retorno<Int32> retornoIncluir = rnCliente.Incluir(toCliente);

                Assert.IsTrue(retornoIncluir.Ok, retornoIncluir.Mensagem.ParaOperador);

                toCliente = new TOCliente()
                {
                    TipoPessoa = "J",
                    CodCliente = 47334689000154,
                };
                //Listando os clientes filtrando pelo CodCliente acima.
                Retorno<List<TOCliente>> retornoListar = rnCliente.Listar(toCliente);
                Assert.IsTrue(retornoListar.Ok, retornoListar.Mensagem.ParaOperador);
                Assert.AreEqual(retornoListar.Dados.Count, 1);
                Assert.AreEqual(retornoListar.Dados[0].CodCliente.LerConteudoOuPadrao().ToString(), toCliente.CodCliente.ToString());
                //Excluíndo o cliente da base de dados para limpeza.
                Retorno<Int32> retornoExcluir = rnCliente.Excluir(toCliente);
                Assert.IsTrue(retornoExcluir.Ok, retornoExcluir.Mensagem.ParaOperador);
                Console.WriteLine("Incluir [Pessoa Jurídica] - PASSOU - Cliente do tipo Pessoa Jurídica foi incluído, encontrado e excluído da base de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incluir [Pessoa Jurídica] - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }

        private void Alterar(Object o)
        {
            Console.WriteLine();
            this.AlterarPessoaFisica();
        }
        /// <summary>Altera um cliente do tipo Pessoa Física na base de dados.</summary>
        [Test(Description = "Altera um cliente do tipo Pessoa Física na base de dados.")]
        public void AlterarPessoaFisica()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = this.PopularCamposObrigatorios("F");

            try
            {
                //Incluíndo um cliente de forma temporária, para posteriormente fazer a sua alteração.
                this.AuxIncluirEValidar(toCliente);

                //Alterando os valores dos campos do cliente.
                toCliente.NomeCliente = "Nome alterado";
                toCliente.Telefone = 1234567990;
                //Efetuando a alteração no banco de dados.
                Retorno<Int32> retornoAlterar = rnCliente.Alterar(toCliente);
                //Validando o retorno da alteração.
                Assert.IsTrue(retornoAlterar.Ok, retornoAlterar.Mensagem.ParaOperador);

                //Obtendo o cliente do banco de dados para verificar a efetividade da alteração.
                Retorno<TOCliente> retornoObter = this.AuxObterEValidar(toCliente);
                TOCliente toClienteObtido = retornoObter.Dados;
                //Comparando o cliente obtido do banco de dados com o original.
                if (toCliente.CodCliente != toClienteObtido.CodCliente || toCliente.NomeCliente != toClienteObtido.NomeCliente || toCliente.Telefone != toClienteObtido.Telefone)
                {
                    throw new Exception("Cliente não foi alterado com sucesso no banco de dados (inconsistência nos valores obtidos).");
                }

                //Excluíndo o cliente anteriormente inserido de forma temporária.
                this.AuxExcluirEValidar(toCliente);

                Console.WriteLine("Alterar [Pessoa Física] - PASSOU - Cliente do tipo Pessoa Física foi alterado com êxito na base de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alterar - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }

        private void ConsultarTest(Object o)
        {
            Console.WriteLine();
            this.Consultar();
        }
        /// <summary>Consultar um cliente na base de dados.</summary>
        /// <param name="o"></param>
        [Test(Description = "Consultar um cliente na base de dados.")]
        public void Consultar()
        {
            TOCliente toCliente = this.PopularCamposObrigatorios("J");

            try
            {
                //Incluíndo um cliente de forma temporária, para posteriormente consultá-lo.
                this.AuxIncluirEValidar(toCliente);

                //Obtendo o cliente do banco de dados para verificar a efetividade da alteração.
                Retorno<TOCliente> retornoObter = this.AuxObterEValidar(toCliente);
                TOCliente toClienteObtido = retornoObter.Dados;
                //Comparando o cliente obtido do banco de dados com o original.
                if (toCliente.CodCliente != toClienteObtido.CodCliente || toCliente.Telefone != toClienteObtido.Telefone)
                {
                    throw new Exception("Cliente não foi encontrado com sucesso no banco de dados (inconsistência nos valores obtidos).");
                }

                //Excluíndo o cliente anteriormente inserido de forma temporária.
                this.AuxExcluirEValidar(toCliente);

                Console.WriteLine("Consultar [Pessoa {0}] - PASSOU - Cliente do tipo Pessoa {0} foi obtido com êxito da base de dados.",
                    toCliente.TipoPessoa == "F" ? "Física" : "Jurídica");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Consultar - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }

        private void ExcluirTest(Object o)
        {
            Console.WriteLine();
            this.Excluir();
        }

        /// <summary>Excluir um cliente da base de dados.</summary>
        /// <param name="o"></param>
        [Test(Description = "Excluir um cliente da base de dados.")]
        public void Excluir()
        {
            TOCliente toCliente = this.PopularCamposObrigatorios("F");

            try
            {
                //Incluíndo um cliente de forma temporária, para posteriormente consultá-lo.
                this.AuxIncluirEValidar(toCliente);

                //Excluíndo o cliente anteriormente inserido de forma temporária.
                this.AuxExcluirEValidar(toCliente);

                Console.WriteLine("Excluir - PASSOU - Cliente foi excluído com êxito da base de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excluir - FALHOU - Retornou:\n{0}", ex.Message);
            }
        }
        #endregion

        #region Métodos auxiliares para operações de CRUD

        /// <summary>Cria um TOCliente do tipo Pessoa Física com os campos preenchidos.</summary>
        /// <param name="tipoPessoa">O Tipo de Pessoa usado para popular os campos do TOCliente.</param>
        /// <returns>Um TOCliente com os campos populados.</returns>
        private TOCliente PopularCamposObrigatorios(String tipoPessoa)
        {
            return new TOCliente()
            {
                CodCliente = tipoPessoa == "F" ? 00998966053 : 47334689000154,
                TipoPessoa = tipoPessoa,
                NomeCliente = "Flinders Fleury",
                DataNasc = new DateTime(2000, 10, 24),
                Telefone = 51999998888,
                RatingCliente = "A",
                RendaFamiliar = 10000.00,
                DataAtuRating = DateTime.Today,
                DataCadastro = DateTime.Today,
            };
        }

        /// <summary>Cria um TOCliente do tipo Pessoa Jurídica com os campos preenchidos.</summary>
        /// <returns>Um TOCliente.</returns>
        private TOCliente PopularCamposObrigatoriosPessoaJuridica()
        {
            return new TOCliente()
            {
                CodCliente = 47334689000154,
                TipoPessoa = "J",
                NomeCliente = "Flinders Fleury",
                DataNasc = new DateTime(2000, 10, 24),
                Telefone = 51999998888,
                RatingCliente = "A",
                RendaFamiliar = 10000.00,
                DataAtuRating = DateTime.Today,
                DataCadastro = DateTime.Today,
            };
        }

        /// <summary>Inclui um cliente no banco de dados e certifica o sucesso da inclusão.</summary>
        /// <param name="toCliente">O TOCliente.</param>
        /// <returns>O Retorno contendo o número de registros afetados.</returns>
        private Retorno<Int32> AuxIncluirEValidar(TOCliente toCliente)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<Int32> retornoIncluir = rnCliente.Incluir(toCliente);
            Assert.IsTrue(retornoIncluir.Ok, "Erro ao tentar incluir um cliente no banco de dados.");
            return retornoIncluir;
        }

        /// <summary>Exclui um cliente do banco de dados e certifica o sucesso da exclusão.</summary>
        /// <param name="toCliente">O TOCliente.</param>
        /// <returns>O Retorno contendo o número de registros afetados.</returns>
        private Retorno<Int32> AuxExcluirEValidar(TOCliente toCliente)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<Int32> retornoIncluir = rnCliente.Excluir(toCliente);
            Assert.IsTrue(retornoIncluir.Ok, "Erro ao tentar excluir um cliente do banco de dados.");
            return retornoIncluir;
        }

        /// <summary>Obtem um cliente do banco de dados e certifica o sucesso da obtenção.</summary>
        /// <param name="toCliente">O TOCliente com os campos para filtrar.</param>
        /// <returns>O Retorno contendo o TOCliente obtido.</returns>
        private Retorno<TOCliente> AuxObterEValidar(TOCliente toCliente)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            //Listando os clientes filtrando pelo CodCliente.
            Retorno<List<TOCliente>> retornoListar = rnCliente.Listar(toCliente);
            Assert.IsTrue(retornoListar.Ok, retornoListar.Mensagem.ParaOperador);
            Assert.AreEqual(retornoListar.Dados.Count, 1);
            Assert.AreEqual(retornoListar.Dados[0].CodCliente.LerConteudoOuPadrao().ToString(), toCliente.CodCliente.ToString());
            return this.Infra.RetornarSucesso<TOCliente>(retornoListar.Dados[0], retornoListar.Mensagem);
        }
        #endregion

        #region Métodos de validação das regras de negócio
        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="o"></param>
        private void RN01(Object o)
        {
            ValidaRN01_TipoDePessoaInvalido();
            Console.WriteLine();
            ValidaRN01_PessoaFisica();
            Console.WriteLine();
            ValidaRN01_PessoaJuridica();
            Console.WriteLine();
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'A', que não deve existir e ser considerado válido.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN01 - Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.")]
        public void ValidaRN01_TipoDePessoaInvalido()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN01",
                "Tipo de Pessoa deve ser 'F' ou 'J'",
                "Falha",
                "Tipo de Pessoa = 'A', que deve ser inválido",
                "Tipo pessoa deve ser 'F' ou 'J'.",
                "Tipo de Pessoa validado com êxito e não deveria");

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000000;
            toCliente.NomeCliente = "Hiper Mercado";
            toCliente.TipoPessoa = "A";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    mensagens.Add(new ClienteMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ClienteMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'F' de Física.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN01 - Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.")]
        public void ValidaRN01_PessoaFisica()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN01",
                "Tipo de Pessoa deve ser 'F' ou 'J'",
                "Falha",
                "Tipo de Pessoa Física, que deve ter CPF inválido",
                "CPF inválido.",
                "CPF validado com êxito e não deveria");

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000001;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    mensagens.Add(new ClienteMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ClienteMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'J' de Jurídica.</summary>
        /// <returns>True se válido.</returns>
        [Test(Description = "RN01 - Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.")]
        public void ValidaRN01_PessoaJuridica()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN01",
                "Tipo de Pessoa deve ser 'F' ou 'J'",
                "Falha",
                "Tipo de Pessoa Jurídica, que deve ter CNPJ inválido",
                "CNPJ inválido.",
                "CNPJ validado com êxito e não deveria");

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000001;
            toCliente.NomeCliente = "Hiper Mercado";
            toCliente.TipoPessoa = "J";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    mensagens.Add(new ClienteMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    mensagens.Add(new ClienteMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }
        
        private void RN02(Object o)
        {
            ValidaRN02();
            Console.WriteLine();
        }
        /// <summary>Valida um número de CPF.</summary>
        /// <param name="o"></param>
        [Test(Description = "RN02 - Valida um número de CPF.")]
        public void ValidaRN02()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN02",
                "Número de CPF deve ser válido",
                "Falha",
                "Um número de CPF inválido",
                "CPF inválido.",
                "CPF validado com êxito e não deveria"
                );

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000001;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    //Esta é a mensagem de erro que estamos esperando.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        private void RN03(Object o)
        {
            ValidaRN03();
            Console.WriteLine();
        }
        /// <summary>Valida um número de CNPJ.</summary>
        /// <param name="o"></param>
        [Test(Description = "RN03 - Valida um número de CPF.")]
        public void ValidaRN03()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN03",
                "Número de CNPJ deve ser válido",
                "Falha",
                "Um número de CNPJ inválido",
                "CNPJ inválido.",
                "CNPJ validado com êxito e não deveria"
                );

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000001;
            toCliente.NomeCliente = "Nome Fantasia";
            toCliente.TipoPessoa = "J";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    //Esta é a mensagem de erro que estamos esperando.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }

        private void RN04(Object o)
        {
            ValidaRN04_NomeComMenosDeDuasPalavras();
            Console.WriteLine();
            ValidaRN04_NomeComMaisDeDuasPalavras();
            Console.WriteLine();
            ValidaRN04_PrimeiroNomeComMenosDeDuasLetras();
            Console.WriteLine();
        }
        /// <summary>Valida o Nome do cliente, que deve ter 2 nomes.</summary>
        /// <param name="o"></param>
        [Test(Description = "RN04 - Valida o Nome do cliente, que deve ter 2 nomes.")]
        public void ValidaRN04_NomeComMenosDeDuasPalavras()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN04",
                "Nome do Cliente deve ter 2 nomes",
                "Falha",
                "Somente 1 nome",
                "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.",
                "Nome do Cliente validado com êxito e não deveria"
                );

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "Nome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    //Esta é a mensagem de erro que estamos esperando.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }
        /// <summary>Valida o Nome do cliente, que deve ter 2 nomes.</summary>
        /// <param name="o"></param>
        [Test(Description = "RN04 - Valida o Nome do cliente, que deve ter 2 nomes.")]
        public void ValidaRN04_NomeComMaisDeDuasPalavras()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN04",
                "Nome do Cliente deve ter 2 nomes",
                "Falha",
                "3 nomes",
                "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.",
                "Nome do Cliente validado com êxito e não deveria"
                );

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "Nome Sobrenome TerceiroNome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    //Esta é a mensagem de erro que estamos esperando.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }

                Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
                Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
            }
        }
        /// <summary>Valida o Nome do cliente, que deve ter no mínimo 2 letras no primeiro nome.</summary>
        /// <param name="o"></param>
        [Test(Description = "RN04 - Valida o Nome do cliente, que deve ter no mínimo 2 letras no primeiro nome.")]
        public void ValidaRN04_PrimeiroNomeComMenosDeDuasLetras()
        {
            //Criando uma instância do mensageiro, que irá nos auxiliar na hora de imprimir os textos das mensagens de validação no console para o usuário.
            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN04",
                "Primeiro Nome do Cliente deve ter no mínimo 2 letras",
                "Falha",
                "Somente 1 letra",
                "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.",
                "Nome do Cliente validado com êxito e não deveria"
                );

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            //Criando novo TOCliente com os campos setados para a realização do teste de validação da RN.
            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "   N   Sobrenome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

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
                    //Esta é a mensagem de erro que estamos esperando.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }
            }

            Assert.IsFalse(retornoValidacao.Ok, retornoValidacao.Mensagem.ParaOperador);
            Assert.AreEqual(mensageiro.MensagemRetornoEsperada, retornoValidacao.Mensagem.ParaOperador);
        }

        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="o"></param>
        private void TodasRNs(Object o)
        {
            RN01(null);
            RN02(null);
            RN03(null);
            RN04(null);
        }
        #endregion
    }
}
