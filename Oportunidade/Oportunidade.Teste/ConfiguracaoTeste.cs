using Microsoft.Extensions.Configuration;

namespace Oportunidade.Teste
{
    public static class ConfiguracaoTeste
    {
        public static void Configuracao()
        {
            var config = InitConfiguration();
            ConfiguracaoAPI.urlBlog = config["urlBlog"];
        }

        public static IConfiguration InitConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
        }
    }
}
