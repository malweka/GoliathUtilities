using System.Xml;
using System.Xml.Linq;

namespace Goliath
{
    public interface IXmlSerializable
    {
        /// <summary>
        /// Serializes to XML.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        XmlNode SerializeToXml(XmlDocument doc);

        /// <summary>
        /// Consume the XML document and load the values.
        /// </summary>
        /// <param name="data">The data.</param>
        void LoadXml(XContainer data);

    }
}