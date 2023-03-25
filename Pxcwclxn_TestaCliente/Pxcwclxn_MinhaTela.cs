using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsclxn;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwclxn
{
    class MinhaTela : AplicacaoTela
    {
        #region Atributos
        private List<ClienteMensagem> mensagens;
        #endregion

        #region Construtores
        public MinhaTela(String caminho) : base(caminho) { }
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
                        new ItemMenu(new KeyValuePair<int, string>(0, "Sair"), null, true),
                    },
                    null
                    );
                Tela.ControlaMenu("Banrisul - Programa de teste de clientes | Carlos Schwarz - 25/03/23", menu);
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
            ValidaRN01_PessoaFisica();
            Console.WriteLine();
            ValidaRN01_PessoaJuridica();
            Console.WriteLine();
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'F' de Física.</summary>
        /// <returns>True se válido.</returns>
        private Boolean ValidaRN01_PessoaFisica()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN01 - Esperado: Falha - Tipo de Pessoa = 'F' de Física, deve ter CPF inválido");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN01 - ERRO - CPF validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN01 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                //Mensagem não pode ser a de Tipo de Pessoa errada.
                if (retornoValidacao.Mensagem.ParaOperador != "Tipo pessoa deve ser 'F' ou 'J'.")
                {
                    Console.WriteLine("Mensagem RN01 - OK");
                    mensagens.Add(new ClienteMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN01 - Errada --------");
                    mensagens.Add(new ClienteMensagem("1", String.Format("RN01 - Mensagem Errada: {0}", retornoValidacao.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        /// <summary>Valida RN01, com o Tipo de Pessoa = 'J' de Jurídica.</summary>
        /// <returns>True se válido.</returns>
        private Boolean ValidaRN01_PessoaJuridica()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000000;
            toCliente.NomeCliente = "Hiper Mercado";
            toCliente.TipoPessoa = "J";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN01 - Falha - Tipo de Pessoa = 'J' de Jurídica, deve ter CNPJ inválido");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN01 - ERRO - CNPJ validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN01 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                //Mensagem não pode ser a de Tipo de Pessoa errada.
                if (retornoValidacao.Mensagem.ParaOperador != "Tipo pessoa deve ser 'F' ou 'J'.")
                {
                    Console.WriteLine("Mensagem RN01 OK");
                    mensagens.Add(new ClienteMensagem("1", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN01 - Errada --------");
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
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            GerenciadorMensagensTeste mensageiro = new GerenciadorMensagensTeste(
                "RN02",
                "Número de CPF inválido",
                "Falha",
                "Número de CPF inválido",
                "CPF inválido.",
                "CPF validado com êxito e não deveria"
                );

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine(mensageiro.TituloRegraNegocio);
            if (retornoValidacao.Ok)
            {
                Console.WriteLine(mensageiro.StatusValidacaoIncorreto);
                return false;
            }
            else
            {
                mensageiro.SetMensagemParaOperador(retornoValidacao.Mensagem.ParaOperador);
                Console.WriteLine(mensageiro.MensagemRetornada);
                Console.WriteLine(mensageiro.MensagemValidacao);
                if (retornoValidacao.Mensagem.ParaOperador == mensageiro.MensagemEsperada)
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    //Console.WriteLine(mensageiro.MensagemValidacaoOk);
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    //Console.WriteLine(mensageiro.MensagemValidacaoIncorreta);
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }
                return true;
            }
        }
        private Boolean ValidaRN02Backup()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000;
            toCliente.NomeCliente = "Nome Sobrenome";
            toCliente.TipoPessoa = "F";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN02 | Esperado: Falha | RN: Número de CPF inválido | Teste com: número de CPF inválido");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN02 - ERRO - CPF validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN02 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                if (retornoValidacao.Mensagem.ParaOperador == "CPF inválido.")
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    Console.WriteLine("Mensagem RN02 OK");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    Console.WriteLine("Mensagem RN02 - Errada --------");
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
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 00000000000000;
            toCliente.NomeCliente = "Nome Fantasia";
            toCliente.TipoPessoa = "J";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN03 - Falha - Numero de CNPJ inválido - Teste com número de CNPJ inválido");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN03 - ERRO - CNPJ validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN03 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                if (retornoValidacao.Mensagem.ParaOperador == "CNPJ inválido.")
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    Console.WriteLine("Mensagem RN03 OK");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    Console.WriteLine("Mensagem RN03 - Errada --------");
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
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "Nome";
            toCliente.TipoPessoa = "F";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN04 - Falha - Nome do Cliente deve ter 2 nomes - Teste com somente 1 nome");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN04 - ERRO - Nome do Cliente validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN04 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                if (retornoValidacao.Mensagem.ParaOperador == "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.")
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    Console.WriteLine("Mensagem RN04 OK");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    Console.WriteLine("Mensagem RN04 - Errada --------");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }
                return true;
            }
        }
        private Boolean ValidaRN04_NomeComMaisDeDuasPalavras()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "Nome Sobrenome TerceiroNome";
            toCliente.TipoPessoa = "F";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN04 - Falha - Nome do Cliente deve ter 2 nomes - Teste com 3 nomes");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN04 - ERRO - Nome do Cliente validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN04 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                if (retornoValidacao.Mensagem.ParaOperador == "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.")
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    Console.WriteLine("Mensagem RN04 OK");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    Console.WriteLine("Mensagem RN04 - Errada --------");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, false));
                }
                return true;
            }
        }
        /// <summary>Valida o Nome do cliente, que deve ter no mínimo 2 letras no primeiro nome.</summary>
        /// <param name="o"></param>
        private Boolean ValidaRN04_PrimeiroNomeComMenosDeDuasLetras()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            TOCliente toCliente = new TOCliente();
            toCliente.CodCliente = 93084196087;
            toCliente.NomeCliente = "   N   Sobrenome";
            toCliente.TipoPessoa = "F";

            Retorno<Int32> retornoValidacao = rnCliente.Incluir(toCliente);

            Console.WriteLine("Valida RN04 - Falha - Primeiro Nome do Cliente deve ter no mínimo 2 letras - Teste com menos de 2 letras");
            if (retornoValidacao.Ok)
            {
                Console.WriteLine("RN04 - ERRO - Nome do Cliente validado com êxito e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN04 - OK - retornou: {0}", retornoValidacao.Mensagem.ParaOperador);
                if (retornoValidacao.Mensagem.ParaOperador == "Nome deve ter 2 (dois) nomes e no mínimo 2(duas) letras no primeiro nome.")
                {
                    //Esta é a mensagem de erro que estamos esperando.
                    Console.WriteLine("Mensagem RN04 OK");
                    mensagens.Add(new ClienteMensagem("2", retornoValidacao.Mensagem.ParaOperador, true));
                }
                else
                {
                    //A mensagem de erro que veio não é a esperada.
                    Console.WriteLine("Mensagem RN04 - Errada --------");
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
