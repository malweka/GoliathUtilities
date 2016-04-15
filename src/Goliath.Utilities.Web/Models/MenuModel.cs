using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Goliath.Models
{
    public class MenuModel : IXmlSerializable
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Source { get; set; }
        public string Classes { get; set; }
        public string IconUrl { get; set; }
        public string Requires { get; set; }

        readonly List<MenuModel> subMenus = new List<MenuModel>();

        public List<MenuModel> SubMenus
        {
            get { return subMenus; }
        }

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

        public static MenuModel LoadFromFile(string filepath)
        {
            using (var xmlStream = new FileStream(filepath, FileMode.Open))
            {
                return Load(xmlStream);
            }
        }

        public static MenuModel Load(string xml)
        {
            using (TextReader reader = new StringReader(xml))
            {
                var doc = XDocument.Load(reader);
                return DeSerializeXmlDoc(doc);
            }
        }

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