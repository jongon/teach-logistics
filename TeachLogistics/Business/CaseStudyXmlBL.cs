using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TeachLogisticsTest.DAL;
using TeachLogisticsTest.Models;

namespace TeachLogisticsTest.Business
{
    public class CaseStudyXmlBL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CaseStudyXml Deserealize(Stream file)
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(CaseStudyXml));
                TextReader reader = new StreamReader(file);
                object obj = deserializer.Deserialize(reader);
                CaseStudyXml CaseStudyXmlData = (CaseStudyXml)obj;
                reader.Close();
                return CaseStudyXmlData;
            }
            catch (Exception)
            {
                throw new Exception("Ha habido un error en la deserealización");
            }
        }

        public CaseStudy XmlToModel(CaseStudyXml caseStudyXml)
        {
            Guid caseStudyXmlId = Guid.NewGuid();
            List<InitialCharge> initialCharges = new List<InitialCharge>();
            foreach (var initialChargeXml in caseStudyXml.InitialCharges)
            {
                InitialCharge initialCharge = new InitialCharge();
                initialCharge.Demand = initialChargeXml.Demand;
                initialCharge.InitialStock = initialChargeXml.InitialStock;
                initialCharge.Price = initialChargeXml.Price;
                initialCharge.ProductId = db.Products.Where(x => x.Name == initialChargeXml.Product).FirstOrDefault().Id;
                initialCharge.SecurityStock = initialChargeXml.SecurityStock;
                initialCharge.Stddev = initialChargeXml.Stddev;
                initialCharge.CaseStudyId = caseStudyXmlId;
                initialCharges.Add(initialCharge);
            }

            CaseStudy caseStudy = new CaseStudy
            {
                Id = caseStudyXmlId,
                Created = DateTime.Now,
                AnnualMaintenanceCost = caseStudyXml.AnnualMaintenanceCost,
                CourierCharges = caseStudyXml.CourierCharges,
                PreparationCost = caseStudyXml.PreparationCost,
                PurchaseOrderRecharge = caseStudyXml.PurchaseOrderRecharge,
                InitialCharges = initialCharges,
            };
            return caseStudy;
        }
    }
}