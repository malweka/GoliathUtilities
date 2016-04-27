using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Goliath.IXmlSerializable" />
    public class MenuModel : IXmlSerializable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the classes.
        /// </summary>
        /// <value>
        /// The classes.
        /// </value>
        public string Classes { get; set; }
        /// <summary>
        /// Gets or sets the icon URL.
        /// </summary>
        /// <value>
        /// The icon URL.
        /// </value>
        public string IconUrl { get; set; }
        /// <summary>
        /// Gets or sets the requires.
        /// </summary>
        /// <value>
        /// The requires.
        /// </value>
        public string Requires { get; set; }

        readonly List<MenuModel> subMenus = new List<MenuModel>();

        /// <summary>
        /// Gets the sub menus.
        /// </summary>
        /// <value>
        /// The sub menus.
        /// </value>
        public List<MenuModel> SubMenus
        {
            get { return subMenus; }
        }

        /// <summary>
        /// Serializes to XML.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Name is required.</exception>
        public XmlNode SerializeToXml(XmlDocument doc)
        {
            var menuElement = doc.CreateElement("menu");
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Name is required.");

            menuElement.SetAttribute("name", Name);

            if (!string.IsNullOrWhiteSpace(DisplayName))
                menuElement.SetAttribute("displayName", DisplayName);

            if (!string.IsNullOrWhiteSpace(Source))
                menuElement.SetAttribute("source", Source);

            if (!string.IsNullOrWhiteSpace(Classes))
                menuElement.SetAttribute("classes", Classes);

            if (!string.IsNullOrWhiteSpace(IconUrl))
                menuElement.SetAttribute("iconUrl", IconUrl);

            if (!string.IsNullOrWhiteSpace(Requires))
                menuElement.SetAttribute("requires", Requires);

            if (subMenus.Count > 0)
            {
                var menus = doc.CreateElement("subMenus");
                foreach (var subMenu in SubMenus)
                {
                    var subMen = ((IXmlSerializable) subMenu).SerializeToXml(doc);
                    menus.AppendChild(subMen);
                }

                menuElement.AppendChild(menus);
            }

            return menuElement;
        }

        /// <summary>
        /// Consume the XML document and load the values.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        /// Could not load menu. Please make sure the XML is in correct format.
        /// or
        /// Menu name attribute is mandatory.
        /// </exception>
        public void LoadXml(XContainer data)
        {
            var elm = data as XElement;

            if (elm == null)
                throw new SerializationException("Could not load menu. Please make sure the XML is in correct format.");

            var name = elm.Attribute("name");
            if (name == null)
                throw new SerializationException("Menu name attribute is mandatory.");
            Name = name.Value;

            var dispName = elm.Attribute("displayName");
            DisplayName = dispName?.Value;

            var src = elm.Attribute("source");
            Source = src?.Value;

            var classes = elm.Attribute("classes");
            Classes = classes?.Value;

            var icon = elm.Attribute("iconUrl");
            IconUrl = icon?.Value;

            var req = elm.Attribute("requires");
            Requires = req?.Value;

            var subs = elm.Descendants("subMenus").Descendants("menu");
            foreach (var xElement in subs)
            {
                var mn = new MenuModel();
                ((IXmlSerializable) mn).LoadXml(xElement);
                SubMenus.Add(mn);
            }
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns></returns>
        public static MenuModel LoadFromFile(string filepath)
        {
            using (var xmlStream = new FileStream(filepath, FileMode.Open))
            {
                return Load(xmlStream);
            }
        }

        /// <summary>
        /// Loads the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static MenuModel Load(string xml)
        {
            using (TextReader reader = new StringReader(xml))
            {
                var doc = XDocument.Load(reader);
                return DeSerializeXmlDoc(doc);
            }
        }

        /// <summary>
        /// Loads the specified XML stream.
        /// </summary>
        /// <param name="xmlStream">The XML stream.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static MenuModel Load(Stream xmlStream)
        {
            if (xmlStream == null) throw new ArgumentNullException(nameof(xmlStream));
            var doc = XDocument.Load(xmlStream);
            return DeSerializeXmlDoc(doc);
        }

        static MenuModel DeSerializeXmlDoc(XDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            //var xmenu = (from m in doc.Elements("menu") select m).FirstOrDefault();
            MenuModel model = new MenuModel();

            var xMenus = doc.Descendants("navMenu").Elements("menu");
            foreach (var xElement in xMenus)
            {
                IXmlSerializable menu = new MenuModel();
                menu.LoadXml(xElement);
                model.SubMenus.Add((MenuModel) menu);
            }

            return model;
        }
    }
}