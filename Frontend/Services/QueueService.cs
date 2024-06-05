using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SecurityToken.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Frontend.Models;
using Frontend.Services.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Xml;

namespace Frontend.Services
{
    public class QueueService : IQueueService
    {
        HttpClient client;
        string currentMessage = "";
        public QueueService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com");
        }

        public async Task SendMessageAsync(string message)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com/click");
            request.Content = new StringContent(message);
            await client.SendAsync(request);
        }

        public async Task NextMessage()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com/test");
            var res = await client.SendAsync(request);
            Stream receiveStream = await res.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            var xmlStream = readStream.ReadToEnd();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlStream);
            try
            {
                 currentMessage = getXMLMessage(xml);
            }
            catch (Exception e) {
                var x = e;
                currentMessage = "no content";
            }
        }

        public string getXMLMessage(XmlDocument xml)
        {
            XmlNode main = xml.ChildNodes[1];
            if (main == null) return "no content";
            XmlNode message = main.FirstChild.FirstChild;
            if (message == null || message.Name != "Message") return "no content";
            XmlNode messageNode = message.ChildNodes[3];
            string messageContent =  messageNode.InnerText;
            return messageContent;
        }

        public void readXML(XmlNode xml)
        {
            
            Console.WriteLine(xml.Name + "->" + xml.Value);
            if (xml.HasChildNodes)
            {
                foreach (XmlNode node in xml.ChildNodes)
                {
                    readXML(node);
                }
            }
        }
        public string getMessage()
        {
            return currentMessage;
        }

    }
}
