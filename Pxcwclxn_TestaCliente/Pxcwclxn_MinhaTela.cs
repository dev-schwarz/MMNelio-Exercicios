using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsclxn;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwclxn
{
    public class TestaClienteTela : AplicacaoTela
    {
        #region Atributos
        private List<ClienteMensagem> mensagens;
        #endregion

        #region Construtores
        public TestaClienteTela(String caminho) : base(caminho) { }
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
                        new ItemMenu(new KeyValuePair<int, string>(1, "Valida RN01 - Tipo de Pessoa deve ser 'F' ou 'J'"), RN01, false),
                        new ItemMenu(new KeyValuePair<int, string>(2, "Valida RN02 - Cliente Pessoa Física deve ter CPF válido"), RN02, false),
                        new ItemMenu(new KeyValuePair<int, string>(3, "Valida RN03 - Cliente Pessoa Jurídica deve ter CNPJ válido"), RN03, false),
                        new ItemMenu(new KeyValuePair<int, string>(4, "Valida RN04 - Nome do cliente deve ter 2 nomes, e no mínimo 2 letras no primeiro nome"), RN04, false),
                        new ItemMenu(new KeyValuePair<int, string>(5, "Todos"), Todos, false),
                        new ItemMenu(new KeyValuePair<int, string>(0, "Voltar"), null, true),
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

        #region Métodos de teste
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
        private Boolean ValidaRN01_TipoDePessoaInvalido()
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
                return false;
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
                return true;
            }
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'F' de Física.</summary>
        /// <returns>True se válido.</returns>
        private Boolean ValidaRN01_PessoaFisica()
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
            toCliente.CodCliente = 00000000000;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
                return false;
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
                return true;
            }
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'J' de Jurídica.</summary>
        /// <returns>True se válido.</returns>
        private Boolean ValidaRN01_PessoaJuridica()
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
            toCliente.CodCliente = 00000000000000;
            toCliente.NomeCliente = "Hiper Mercado";
            toCliente.TipoPessoa = "J";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
                return false;
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
                return true;
            }
        }

        /// <summary>Valida um número de CPF.</summary>
        /// <param name="o"></param>
        private void RN02(Object o)
        {
            ValidaRN02();
            Console.WriteLine();
        }
        private Boolean ValidaRN02()
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
            toCliente.CodCliente = 00000000000;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
                return false;
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
                return true;
            }
        }

        /// <summary>Valida um número de CNPJ.</summary>
        /// <param name="o"></param>
        private void RN03(Object o)
        {
            ValidaRN03();
            Console.WriteLine();
        }
        private Boolean ValidaRN03()
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
            toCliente.CodCliente = 00000000000000;
            toCliente.NomeCliente = "Nome Fantasia";
            toCliente.TipoPessoa = "J";

            //Executando o método de inclusão para verificar a validação da RN.
            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine(mensageiro.TituloRegraNegocio);

            if (retornoValidacao.Ok)
            {
                //Retornou Ok quando deveria ter retornado uma Falha.
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
                return false;
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
                return true;
            }
        }

        /// <summary>Valida o Nome do cliente, que deve ter 2 nomes.</summary>
        /// <param name="o"></param>
        private void RN04(Object o)
        {
            ValidaRN04_NomeComMenosDeDuasPalavras();
            Console.WriteLine();
            ValidaRN04_NomeComMaisDeDuasPalavras();
            Console.WriteLine();
            ValidaRN04_PrimeiroNomeComMenosDeDuasLetras();
            Console.WriteLine();
        }
        private Boolean ValidaRN04_NomeComMenosDeDuasPalavras()
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
                return false;
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
                return true;
            }
        }
        private Boolean ValidaRN04_NomeComMaisDeDuasPalavras()
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
                return false;
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
                return true;
            }
        }
        /// <summary>Valida o Nome do cliente, que deve ter no mínimo 2 letras no primeiro nome.</summary>
        /// <param name="o"></param>
        private Boolean ValidaRN04_PrimeiroNomeComMenosDeDuasLetras()
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
                return false;
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
                return true;
            }
        }

        /// <summary>Um Tipo de Pessoa deve ser somente ou 'F' de Física, ou 'J' de Jurídica.</summary>
        /// <param name="o"></param>
        private void Todos(Object o)
        {
            RN01(null);
            RN02(null);
            RN03(null);
            RN04(null);
        }
        #endregion
    }
}
