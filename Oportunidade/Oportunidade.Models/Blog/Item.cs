using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Oportunidade.Models.Blog
{
    public class Item
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("comments")]
        public string Comments { get; set; }

        [XmlElement("category")]
        public List<string> Categories { get; set; }

        [XmlElement("guid")]
        public string Guid { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
        public string ContendEncoded { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }
        public DateTime dtPubDate { get; set; }


    }
}
