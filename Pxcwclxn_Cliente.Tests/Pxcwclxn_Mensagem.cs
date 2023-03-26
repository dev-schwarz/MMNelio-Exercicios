using System;

namespace Bergs.Pxc.Pxcwclxn.Tests
{
    class ClienteMensagem
    {
        public String Regra { get; set; }
        public String Mensagem { get; set; }
        public Boolean Ok { get; set; }

        public ClienteMensagem(string regra, string mensagem, bool ok)
        {
            Regra = regra;
            Mensagem = mensagem;
            Ok = ok;
        }
    }
}
