using CoreWCF_Demo_Infrastructure.Models.Common;
using Microsoft.AspNetCore.Http;
using System.IO.Pipelines;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme
{
    public static class HttpRequestExtensions
    {
        public static async Task<UsernameToken?> GetAuthenticationHeaderFromSoapEnvelope(this HttpRequest request)
        {
            ReadResult requestBodyInBytes = await request.BodyReader.ReadAsync();
            string body = Encoding.UTF8.GetString(requestBodyInBytes.Buffer.FirstSpan);
            request.BodyReader.AdvanceTo(requestBodyInBytes.Buffer.Start, requestBodyInBytes.Buffer.End);

            UsernameToken authTokenFromHeader = null;

            if (body?.Contains(@"http://www.w3.org/2003/05/soap-envelope") == true)
            {
                body = body.Replace("%", "&amp;");

                using (TextReader reader = new StringReader(body))
                {
                    XmlReader xmlReader = XmlReader.Create(reader, new XmlReaderSettings { IgnoreWhitespace = true });
                    xmlReader.MoveToContent();
                    if (xmlReader.ReadToDescendant("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
                    {
                        var serializer = new XmlSerializer(typeof(UsernameToken));
                        authTokenFromHeader = (UsernameToken)serializer.Deserialize(xmlReader);
                    }
                }
            }

            return authTokenFromHeader;
        }

    }
}
