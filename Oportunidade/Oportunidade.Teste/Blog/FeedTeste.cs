using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oportunidade.Blog;

namespace Oportunidade.Teste.Blog
{
    [TestClass]
    public class FeedTeste
    {
        public FeedTeste()
        {
            ConfiguracaoTeste.Configuracao();
        }

        [TestMethod]
        public void RecuperarFeed()
        {
            Feed feed = new Feed();
            var rss = feed.ConsultarTopicos();

            Assert.IsNotNull(rss);
            Assert.IsNotNull(rss.Channel);
            Assert.AreEqual(10, rss.Channel.Items.Count);
        }
    }
}
