using System;

namespace Bergs.Pxc.Pxcwclxn
{
    class Principal
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (TestaClienteTela telaCliente = new TestaClienteTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                telaCliente.Executar();
            }
        }
    }
}
