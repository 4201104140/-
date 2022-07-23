using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdaptiveCards;

/// <summary>
/// Base for almost all representable elements in AdaptiveCards.
/// </summary>
public abstract class AdaptiveTypedElement
{
    /// <summary>
    /// The AdaptiveCard element that this class implements.
    /// </summary>
    [XmlIgnore]
    public abstract string Type { get; set; }

    [XmlElement]
    public SerializableDictionary<string, object> AdditionalProperties { get; set; } = new SerializableDictionary<string, object>(StringComparer.OrdinalIgnoreCase);

    public bool ShouldSerializeAdditionalProperties() => this.AdditionalProperties.Count > 0;
}
