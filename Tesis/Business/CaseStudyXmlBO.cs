using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Tesis.Business
{
    [XmlRoot("CaseStudy")]
    public class CaseStudyXml
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement("Section")]
        public List<SectionXml> Sections { get; set; }

        [XmlElement("InitialCharge")]
        public List<InitialChargeXml> InitialCharges { get; set; }
    }

    public class SectionXml
    {
        public string Number { get; set; }
    }

    public class InitialChargeXml
    {
        public short Demand { get; set; }
        public short Stddev { get; set; }
        public int Price { get; set; }
        public int PreparationCost { get; set; }
        public double AnnualMaintenanceCost { get; set; }
        public byte PreparationTime { get; set; }
        public byte FillTime { get; set; }
        public byte DeliveryTime { get; set; }
        public short SecurityStock { get; set; }
        public int InitialStock { get; set; }
        public string Product { get; set; }
    }
}