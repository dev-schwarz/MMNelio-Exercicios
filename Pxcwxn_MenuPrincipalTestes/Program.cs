using System;

namespace Bergs.Pxc.Pxcwxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (MenuPrincipalTestesTela tela = new MenuPrincipalTestesTela())
            {
                tela.Executar();
            }
        }
    }
}
