using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class InitialChargeXml
    {
        public void Desirealize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CaseStudyXml));
            TextReader reader = new StreamReader(@"C:\Xml.xml");
            object obj = deserializer.Deserialize(reader);
            CaseStudyXml XmlData = (CaseStudyXml)obj;
            reader.Close();
        }
    }
}