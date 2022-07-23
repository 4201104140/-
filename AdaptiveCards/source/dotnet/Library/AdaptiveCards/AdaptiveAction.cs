using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdaptiveCards
{
    public abstract class AdaptiveAction : AdaptiveTypedElement
    {
        [XmlAttribute]
        public string? Title { get; set; }

        public string? Speak { get; set; }

        public string? IconUrl { get; set; }

        public string Style { get; set; } = "default"; 
    }
}
