using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Tesis.Models;

namespace Tesis.Business
{
    public class CaseStudyXmlBL
    {
        public static CaseStudyXml Deserealize(Stream file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CaseStudyXml));
            TextReader reader = new StreamReader(file);
            object obj = deserializer.Deserialize(reader);
            CaseStudyXml CaseStudyXmlData = (CaseStudyXml)obj;
            reader.Close();
            return CaseStudyXmlData;
        }
    }
}