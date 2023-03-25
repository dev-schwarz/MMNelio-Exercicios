using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela SEGURO</summary>
    public class TOSeguro : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codSeguro;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<String> matricula;
        private CampoTabela<String> placa;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<String> tipoSeguro;
        private CampoTabela<Double> valorFranquia;
        private CampoTabela<Double> valorSeguro;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela SEGURO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_SEGURO da tabela SEGURO</summary>
        [XmlElement("cod_seguro")]
        public CampoTabela<Int32> CodSeguro
        {
            get { return codSeguro; }
            set { codSeguro = value; }
        }
        /// <summary>Campo MATRICULA da tabela SEGURO</summary>
        [XmlElement("matricula")]
        public CampoTabela<String> Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }
        /// <summary>Campo PLACA da tabela SEGURO</summary>
        [XmlElement("placa")]
        public CampoTabela<String> Placa
        {
            get { return placa; }
            set { placa = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela SEGURO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo TIPO_SEGURO da tabela SEGURO</summary>
        [XmlElement("tipo_seguro")]
        public CampoTabela<String> TipoSeguro
        {
            get { return tipoSeguro; }
            set { tipoSeguro = value; }
        }
        /// <summary>Campo VALOR_FRANQUIA da tabela SEGURO</summary>
        [XmlElement("valor_franquia")]
        public CampoTabela<Double> ValorFranquia
        {
            get { return valorFranquia; }
            set { valorFranquia = value; }
        }
        /// <summary>Campo VALOR_SEGURO da tabela SEGURO</summary>
        [XmlElement("valor_seguro")]
        public CampoTabela<Double> ValorSeguro
        {
            get { return valorSeguro; }
            set { valorSeguro = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOSeguro</summary>
        /// <param name="linha">Linha para popular os campos da TOSeguro</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_SEGURO":
                        this.codSeguro = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "MATRICULA":
                        this.matricula = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "PLACA":
                        this.placa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_SEGURO":
                        this.tipoSeguro = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_FRANQUIA":
                        this.valorFranquia = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_SEGURO":
                        this.valorSeguro = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
