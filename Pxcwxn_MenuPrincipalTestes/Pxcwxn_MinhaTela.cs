using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcwclxn.Tests;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwxn
{
    class MenuPrincipalTestesTela : AplicacaoTela
    {
        public void Executar()
        {
            try
            {
                Menu menu = new Menu(
                    new ItemMenu[]
                    {
                        new ItemMenu(new KeyValuePair<int, string>(1, "Menu de Testes de Clientes"), TestesClientes, false),
                        new ItemMenu(new KeyValuePair<int, string>(0, "Sair"), null, true),
                    },
                    null
                    );
                Tela.ControlaMenu("MENU PRINCIPAL - TESTADOR DE RNs | Banrisul | Carlos Schwarz - 25/03/23", menu);
            }
            catch (Exception ex)
            {
                Console.Write("Erro {0}\nTecle algo...", ex.Message);
            }
        }

        private void TestesClientes(Object o)
        {
            TestaClienteComTela tela = new TestaClienteComTela();
            tela.Executar();
        }
    }
}
