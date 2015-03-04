using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class CaseStudyXmlBL
    {
        public CaseStudyXml Deserealize(HttpPostedFileBase file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CaseStudyXml));
            //TextReader reader = new StreamReader(@"C:\Xml.xml");
            TextReader reader = new StreamReader(file.InputStream);
            object obj = deserializer.Deserialize(reader);
            CaseStudyXml XmlData = (CaseStudyXml)obj;
            reader.Close();
            return XmlData;
        }
    }
}