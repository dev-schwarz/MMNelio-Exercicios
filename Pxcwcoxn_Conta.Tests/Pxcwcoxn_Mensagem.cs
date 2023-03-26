using System;

namespace Bergs.Pxc.Pxcwcoxn.Tests
{
    class ContaMensagem
    {
        public String Regra { get; set; }
        public String Mensagem { get; set; }
        public Boolean Ok { get; set; }

        public ContaMensagem(string regra, string mensagem, bool ok)
        {
            Regra = regra;
            Mensagem = mensagem;
            Ok = ok;
        }
    }
}
