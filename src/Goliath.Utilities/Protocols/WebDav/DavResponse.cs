using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Goliath.Protocols.WebDav
{
    public class DavResponse
    {
        public string ContentType { get; set; }
        public string StatusDescription { get; set; }
        public StatusCode ResponseCode { get; internal set; }
        public XmlTextWriter OutputXml { get; internal set; }
        public Stream OutputStream { get; internal set; }

        private Dictionary<string, string> headers = new Dictionary<string, string>();
        public Dictionary<string, string> ResponseHeaders
        {
            get { return headers; }
        }

        //TODO: provide cache info
    }
}