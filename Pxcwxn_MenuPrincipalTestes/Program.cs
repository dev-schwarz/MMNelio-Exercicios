using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.Pxc.Pxcwxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (MenuPrincipalTestesTela tela = new MenuPrincipalTestesTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                tela.Executar();
            }
        }
    }
}
