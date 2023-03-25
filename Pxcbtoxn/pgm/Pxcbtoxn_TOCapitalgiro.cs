using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CAPITALGIRO</summary>
    public class TOCapitalgiro : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Double> codCliente;
        private CampoTabela<DateTime> dataEmprestimo;
        private CampoTabela<String> tipoPessoa;
        #endregion

        #region Campos da tabela
        private CampoTabela<DateTime> dataFim;
        private CampoTabela<Double> valor;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela CAPITALGIRO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo DATA_EMPRESTIMO da tabela CAPITALGIRO</summary>
        [XmlElement("data_emprestimo")]
        public CampoTabela<DateTime> DataEmprestimo
        {
            get { return dataEmprestimo; }
            set { dataEmprestimo = value; }
        }
        /// <summary>Campo DATA_FIM da tabela CAPITALGIRO</summary>
        [XmlElement("data_fim")]
        public CampoTabela<DateTime> DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CAPITALGIRO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR da tabela CAPITALGIRO</summary>
        [XmlElement("valor")]
        public CampoTabela<Double> Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOCapitalgiro</summary>
        /// <param name="linha">Linha para popular os campos da TOCapitalgiro</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "DATA_EMPRESTIMO":
                        this.dataEmprestimo = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "DATA_FIM":
                        this.dataFim = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR":
                        this.valor = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
