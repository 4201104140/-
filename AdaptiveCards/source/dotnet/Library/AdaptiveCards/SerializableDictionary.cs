using System.Xml;
using System.Xml.Serialization;

namespace AdaptiveCards;

[XmlRoot("dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable where TKey : notnull
{
	public SerializableDictionary()
		: base()
	{
	}

	public SerializableDictionary(IEqualityComparer<TKey> comparer)
		: base(comparer)
	{
	}

    #region IXmlSerializable Members
	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null!;
	}

	public void ReadXml(System.Xml.XmlReader reader)
	{
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
		XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

		bool wasEmpty = reader.IsEmptyElement;
		reader.Read();

		if (wasEmpty)
			return;

		while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
		{
			reader.ReadStartElement("item");

			reader.ReadStartElement("key");
		}
		reader.ReadEndElement();
    }

	public void WriteXml(XmlWriter writer)
	{
		throw new NotImplementedException();
	}
	#endregion
}
