using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oportunidade.Blog;
using Oportunidade.Interface.Blog;
using Oportunidade.Interface.Negocio;
using Oportunidade.Negocio;
using Oportunidade.WebApi.Controllers;

namespace Oportunidade.Testes
{
    [TestClass]
    public class FeedControllerTeste
    {
        [TestMethod]
        public void UltimosTopicos()
        {
            CriaControllers(out FeedController feedController);

            ConfiguracaoAPI.urlBlog = "https://www.minutoseguros.com.br/blog/feed/";

            object retorno = feedController.UltimosTopicos();

            Assert.AreEqual(retorno.GetType(), typeof(OkObjectResult));
        }

        #region CriaControllers
        public void CriaControllers(out FeedController feedController)
        {
            var services = new ServiceCollection();
            services.AddTransient<IFeed, Feed>();
            services.AddTransient<ITopico, Topico>();

            var serviceProvider = services.BuildServiceProvider();

            feedController = new FeedController(serviceProvider.GetService<ITopico>());
        }
        #endregion
    }
}
