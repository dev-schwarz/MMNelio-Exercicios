using System;

namespace Bergs.Pxc.Pxcwclxn
{
    class GerenciadorMensagensTeste
    {
        #region Atribudos
        /// <summary>O nome da Regra de Negócio (ex: RN01).</summary>
        private readonly String nomeRegraNegocio;
        /// <summary>Uma breve descrição da Regra de Negócio.</summary>
        private readonly String descricaoRegraNegocio;
        /// <summary>Uma breve descrição do resultado esperado pelo teste.</summary>
        private readonly String resultadoEsperado;
        /// <summary>Uma breve descrição de como o teste está sendo realizado.</summary>
        private readonly String descricaoTeste;
        /// <summary>Mensagem de erro de status da validação, quando o 'Ok' retornado não era o esperado pelo teste.</summary>
        private readonly String mensagemEsperada;
        /// <summary>Mensagem retornada pela validação era a mensagem esperada.</summary>
        private readonly String statusValidacaoIncorreto;
        /// <summary>A mensagem que espera-se que seja retornada pela validação da Regra de Negócio.</summary>
        private String mensagemParaOperador;
        #endregion

        #region Propriedades
        /// <summary>Título de apresentação do teste para a Regra de Negócio, contendo o resultado que está sendo esperado, a descrição da regra e como o teste está sendo realizado.</summary>
        public String TituloRegraNegocio
        {
            get
            {
                return String.Format("Valida {0} - Esperado: {1} - RN: {2} - Teste com: {3}",
                    this.nomeRegraNegocio,
                    this.resultadoEsperado,
                    this.descricaoRegraNegocio,
                    this.descricaoTeste);
            }
        }

        /// <summary>Mensagem para exibir quando o status de retorno da Regra de Negócio não foi o esperado (ex: retorou 'OK=true' quando era esperado 'OK=false'). </summary>
        public String StatusValidacaoIncorreto
        {
            get
            {
                return String.Format("{0} - ERRO - {1}",
                    this.nomeRegraNegocio,
                    this.statusValidacaoIncorreto);
            }
        }

        /// <summary>Mensagem retornada pela validação da Regra de Negócio.</summary>
        public String MensagemRetornada
        {
            get
            {
                return String.Format("{0} - Resultado do teste: {1} - Mensagem retornada: {2}",
                    this.nomeRegraNegocio,
                    this.mensagemParaOperador == this.mensagemEsperada ? "PASSOU" : "FALHOU",
                    this.mensagemParaOperador);
            }
        }

        /// <summary>Mensagem para exibir com o status da validação da Regra de Negócio.</summary>
        public String MensagemValidacao
        {
            get
            {
                if (this.mensagemParaOperador == this.mensagemEsperada)
                {
                    return String.Format("Mensagem {0} - OK",
                        this.nomeRegraNegocio);
                }
                else
                {
                    return String.Format("Mensagem {0} - ERRADA - Mensagem esperada era: {1}",
                        this.nomeRegraNegocio,
                        this.mensagemEsperada);
                }
            }
        }

        /// <summary>A mensagem que espera-se que seja retornada pela validação da Regra de Negócio.</summary>
        public String MensagemEsperada
        {
            get
            {
                return this.mensagemEsperada;
            }
        }
        #endregion

        #region Construtores
        /// <summary>Cria nova instância.</summary>
        /// <param name="nomeRegraNegocio">O nome da Regra de Negócio (ex: RN01).</param>
        /// <param name="descricaoRegraNegocio">Uma breve descrição da Regra de Negócio.</param>
        /// <param name="resultadoEsperado">Uma breve descrição do resultado esperado pelo teste.</param>
        /// <param name="descricaoTeste">Uma breve descrição de como o teste está sendo realizado.</param>
        /// <param name="mensagemEsperada">A mensagem que espera-se que seja retornada pela validação da Regra de Negócio.</param>
        /// <param name="statusValidacaoIncorreta">Mensagem retornada pela validaçao foi diferente da mensagem esperada.</param>
        public GerenciadorMensagensTeste(String nomeRegraNegocio, String descricaoRegraNegocio, String resultadoEsperado, String descricaoTeste, String mensagemEsperada, String statusValidacaoIncorreta)
        {
            this.nomeRegraNegocio = nomeRegraNegocio;
            this.descricaoRegraNegocio = descricaoRegraNegocio;
            this.resultadoEsperado = resultadoEsperado;
            this.descricaoTeste = descricaoTeste;
            this.mensagemEsperada = mensagemEsperada;
            this.statusValidacaoIncorreto = statusValidacaoIncorreta;
        }
        #endregion

        #region Métodos
        /// <summary>Seta a mensagem para o operador que foi retornada pelo Regra de Negócio.</summary>
        /// <param name="mensagem">Mensagem para o operador.</param>
        public void SetMensagemParaOperador(String mensagem)
        {
            this.mensagemParaOperador = mensagem;
        }
        #endregion
    }
}
