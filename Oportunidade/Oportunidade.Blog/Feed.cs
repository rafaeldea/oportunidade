using HtmlAgilityPack;
using Oportunidade.Interface.Blog;
using Oportunidade.Models.Blog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Oportunidade.Blog
{
    public class Feed : IFeed
    {
        public RSS ConsultarTopicos()
        {
            // Recupera os tópicos do blog
            RSS rss = (RSS)CallAPI(ConfiguracaoAPI.urlBlog, HttpMethodEnum.GET, typeof(RSS));

            TratarDadosRetorno(rss);

            return rss;
        }

        #region TratarDadosRetorno
        private void TratarDadosRetorno(RSS rss)
        {
            // Converte a string para DateTime
            rss.Channel.dtLastBuildDate = ConverterData(rss.Channel.LastBuildDate);

            // Converte a string em DateTime e remove tags HTML dos textos
            Parallel.ForEach(rss.Channel.Items, item =>
            {
                item.dtPubDate = ConverterData(item.PubDate);
                item.Description = RemoveTagsHTML(item.Description);
                item.ContendEncoded = RemoveTagsHTML(item.ContendEncoded);
            });
        }
        #endregion

        #region RemoveTagsHTML
        private DateTime ConverterData(string valor)
        {
            return DateTime.Parse(valor, new System.Globalization.CultureInfo("en-US"));
        }
        #endregion

        #region RemoveTagsHTML
        private string RemoveTagsHTML(string valor)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(valor);
            return htmlDoc.DocumentNode.InnerText;
        }
        #endregion

        #region CallAPI
        private object CallAPI(string url, HttpMethodEnum httpMethod, Type responseType)
        {
            var baseAddress = url;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/xml";
            http.ContentType = "application/xml";
            http.Method = httpMethod.ToString();

            WebResponse response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            if (responseType != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RSS));
                using (TextReader reader = new StringReader(content))
                {
                    return (RSS)serializer.Deserialize(reader);
                }
            }
            else
            {
                return content;
            }
        }
        #endregion
    }
}
