using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcwclxn;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcwxn
{
    class MenuPrincipalTestesTela : AplicacaoTela
    {
        private readonly String caminho;

        public MenuPrincipalTestesTela(String caminho)
        {
            this.caminho = caminho;
        }

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
            TestaClienteTela tela = new TestaClienteTela(this.caminho);
            tela.Executar();
        }
    }
}
