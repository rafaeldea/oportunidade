using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Oportunidade.Models.Blog
{
    [XmlRoot("channel")]
    public class Channel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("lastBuildDate")]
        public string LastBuildDate { get; set; }
        public DateTime dtLastBuildDate { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("generator")]
        public string Generator { get; set; }

        [XmlElement("item")]
        public List<Item> Items { get; set; }
    }
}
