using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oportunidade.Blog;
using Oportunidade.Negocio;

namespace Oportunidade.Teste.Negocio
{
    [TestClass]
    public class TopicoTeste
    {
        public TopicoTeste()
        {
            ConfiguracaoTeste.Configuracao();
        }

        [TestMethod]
        public void CopnsultarTopicos()
        {
            Topico topico = new Topico(new Feed());

            var topicos = topico.UltimosTopicos();

            Assert.IsNotNull(topicos);
            Assert.AreEqual(10, topicos.Count);
        }
    }


}
