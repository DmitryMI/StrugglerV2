using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StrugglerV2.Preferences
{
    public class PreferencesContainer
    {
        public class PreferencesFileReadingException : Exception
        {
            public PreferencesFileReadingException(string message, Exception inner) : base(message, inner)
            {

            }

            public PreferencesFileReadingException(string message) : base(message)
            {

            }

            public string GetUserMessage()
            {
                if (InnerException != null)
                {
                    return Message + "\t Inner: " + InnerException.Message;
                }
                else
                {
                    return Message;
                }
            }
        }

        public KeyCombination ToggleKey { get; set; }
        public Keys TargetKey { get; set; }

        public int PeriodOuterMs { get; set; }
        public int PeriodInnerMs { get; set; }
        public PreferencesContainer()
        {
            ToggleKey = new KeyCombination(Keys.Back, null);
            TargetKey = Keys.Space;
            PeriodOuterMs = 500;
            PeriodInnerMs = 100;
        }

        public bool IsNotDefault =>
            ToggleKey != new KeyCombination(Keys.Back, null) ||
            TargetKey != Keys.Space ||
            PeriodOuterMs != 500 ||
            PeriodInnerMs != 100;

        public void SaveToFile(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            //XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(xmlDeclaration);

            XmlElement rootNode = xmlDocument.CreateElement("StrugglerV2");
            xmlDocument.AppendChild(rootNode);
            //xmlDocument.InsertBefore(xmlDeclaration, rootNode);

            XmlElement toggleKeyElement = xmlDocument.CreateElement("ToggleKey");
            XmlElement targetKeyElement = xmlDocument.CreateElement("TargetKey");
            XmlElement periodOuterElement = xmlDocument.CreateElement("PeriodOuterMs");
            XmlElement periodInnerElement = xmlDocument.CreateElement("PeriodInnerMs");

            rootNode.AppendChild(toggleKeyElement);
            rootNode.AppendChild(targetKeyElement);
            rootNode.AppendChild(periodOuterElement);
            rootNode.AppendChild(periodInnerElement);

            XmlText toggleKeyElementText = xmlDocument.CreateTextNode(ToggleKey.ToString());
            XmlText targetKeyElementText = xmlDocument.CreateTextNode(TargetKey.ToString());
            XmlText periodOuterElementText = xmlDocument.CreateTextNode(PeriodOuterMs.ToString());
            XmlText periodInnerElementText = xmlDocument.CreateTextNode(PeriodInnerMs.ToString());
            
            toggleKeyElement.AppendChild(toggleKeyElementText);
            targetKeyElement.AppendChild(targetKeyElementText);
            periodOuterElement.AppendChild(periodOuterElementText);
            periodInnerElement.AppendChild(periodInnerElementText);

            xmlDocument.Save(path);
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new PreferencesFileReadingException($"File {path} does not exist");
            }

            try
            {

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);

                XmlElement root = (XmlElement) xmlDocument.GetElementsByTagName("StrugglerV2")[0];

                XmlElement toggleKeyElement = (XmlElement) root.GetElementsByTagName("ToggleKey")[0];
                XmlElement targetKeyElement = (XmlElement) root.GetElementsByTagName("TargetKey")[0];
                XmlElement periodOuterElement = (XmlElement) root.GetElementsByTagName("PeriodOuterMs")[0];
                XmlElement periodInnerElement = (XmlElement) root.GetElementsByTagName("PeriodInnerMs")[0];
                
                bool parsingOk = true;
                parsingOk &= KeyCombination.TryParse(toggleKeyElement.InnerText, out KeyCombination toggleKey);
                parsingOk &= Enum.TryParse(targetKeyElement.InnerText, out Keys targetKey);

                parsingOk &= int.TryParse(periodOuterElement.InnerText, out int periodOuter);
                parsingOk &= int.TryParse(periodInnerElement.InnerText, out int periodInner);

                if (parsingOk)
                {
                    TargetKey = targetKey;
                    ToggleKey = toggleKey;
                    PeriodOuterMs = periodOuter;
                    PeriodInnerMs = periodInner;
                }
                else
                {
                    throw new PreferencesFileReadingException("Values parsing failed");
                }
            }
            catch (XmlException xmlException)
            {
                throw new PreferencesFileReadingException("Xml parsing error. See inner exception", xmlException);
            }
            catch (PreferencesFileReadingException)
            {
                throw;
            }

            
        }
    }
}
