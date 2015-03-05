using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.Business
{
    public class CaseStudyXmlBL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public CaseStudyXml Deserealize(Stream file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CaseStudyXml));
            TextReader reader = new StreamReader(file);
            object obj = deserializer.Deserialize(reader);
            CaseStudyXml CaseStudyXmlData = (CaseStudyXml)obj;
            reader.Close();
            return CaseStudyXmlData;
        }

        public CaseStudy XmlToModel(CaseStudyXml caseStudyXml)
        {
            CaseStudy caseStudy = new CaseStudy();
            caseStudy.Id = Guid.NewGuid();
            caseStudy.Created = DateTime.Now;
            caseStudy.Name = caseStudyXml.Name;
            foreach (var initialChargeXml in caseStudyXml.InitialCharges)
            {
                InitialCharge initialCharge = new InitialCharge();
                initialCharge.Id = Guid.NewGuid();
                initialCharge.CaseStudyId = caseStudy.Id;
                initialCharge.AnnualMaintenanceCost = initialChargeXml.AnnualMaintenanceCost;
                initialCharge.DeliveryTime = initialChargeXml.DeliveryTime;
                initialCharge.Demand = initialChargeXml.Demand;
                initialCharge.FillTime = initialChargeXml.FillTime;
                initialCharge.InitialStock = initialChargeXml.InitialStock;
                initialCharge.PreparationCost = initialChargeXml.PreparationCost;
                initialCharge.PreparationTime = initialChargeXml.PreparationTime;
                initialCharge.Price = initialChargeXml.Price;
                initialCharge.Product = db.Products.Where(x => x.Name == initialChargeXml.Product).FirstOrDefault();
                initialCharge.SecurityStock = initialChargeXml.SecurityStock;
                initialCharge.Stddev = initialChargeXml.Stddev;
                caseStudy.InitialCharges.Add(initialCharge);
            }
            return caseStudy;
        }
    }
}