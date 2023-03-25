using System;

namespace Bergs.Pxc.Pxcwclxn
{
    class Principal
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (MinhaTela telaCliente = new MinhaTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                telaCliente.Executar();
            }
        }
    }
}
