using System.Xml.Serialization;

namespace Oportunidade.Models.Blog
{
    [XmlRoot("rss")]
    public class RSS
    {
        [XmlElement("channel")]
        public Channel Channel { get; set; }
    }
}
