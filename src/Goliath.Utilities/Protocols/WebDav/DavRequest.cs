using System.Collections.Generic;

namespace Goliath.Protocols.WebDav
{
    public class DavRequest
    {
        public DavWebMethod WebMethod { get; private set; }
        public string Host { get; set; }
        public string RequestUrl { get; private set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public int Depth { get; set; }
        public string Destination { get; set; }

        readonly Dictionary<string,string> headersDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> Headers
        {
            get { return headersDictionary; }
        }
       
        internal DavRequest(DavWebMethod method, string requestUrl)
        {
            WebMethod = method;
            RequestUrl = requestUrl;
        }

        protected DavRequest(string webMethod)
        {

        }
    }
}