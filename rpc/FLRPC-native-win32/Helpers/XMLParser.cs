using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DiscordRPC.Logging;
namespace FLRPC.Helpers
{
    public static class XMLParser
    {
        public static XmlDocument LoadDocument(string path)
        {
            //First check if file exists
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("XML Document wasn't found at path!");
            }
            XmlDocument d = new XmlDocument();
            d.Load(path);
            return d;
        }
        public static string[] FindByTag(string NodeName, XmlDocument document)
        {
            XmlNodeList tags = document.GetElementsByTagName(@NodeName);

            // Check if nodes were found
            if (tags.Count < 1)
            {
                // Return null if not found
                return null;
            }
            else
            {

                List<string> l = new List<string>();

                //Go though each found node and add it to the list
                foreach(XmlNode n in tags)
                {
                    l.Add(n.InnerText);
                }
                
                //Convert it to array, clear the list and return
                string[] f = l.ToArray();
                l.Clear();
                return f;
            }
        }
    }
    public struct XmlSettings
    {
        public LogLevel logLevel { get; set; }
        public string ClientID { get; set; }
        public int Pipe { get; set; }
        public bool Secret { get; set; }
        public string SecretMessage { get; set; }
        public string NoNameMessage { get; set; }
        public int RefeshInterval { get; set; }
        public bool AcceptedWarning { get; set; }
    }
}
