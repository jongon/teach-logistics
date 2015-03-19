using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                initialCharge.Product = db.Products.Where(x => x.Name == initialChargeXml.Product).FirstOrDefault();
                initialCharge.SecurityStock = initialChargeXml.SecurityStock;
                initialCharge.Stddev = initialChargeXml.Stddev;
                if (Controller.TryUpdateModel(initialCharge))
                {
                    initialCharges.Add(initialCharge);
                }
                else
                {
                    throw new Exception("Se ha encontrado un error en el archivo XML");
                }
            }

            CaseStudy caseStudy = new CaseStudy
            {
                Id = caseStudyXmlId,
                Created = DateTime.Now,
                Name = caseStudyXml.Name,
                AcceleratedPreparationTime = caseStudyXml.AcceleratedPreparationTime,
                AnnualMaintenanceCost = caseStudyXml.AnnualMaintenanceCost,
                CourierCharges = caseStudyXml.CourierCharges,
                CourierDeliveryTime = caseStudyXml.CourierDeliveryTime,
                DeliveryTime = caseStudyXml.DeliveryTime,
                ExistingFillTime = caseStudyXml.ExistingFillTime,
                FillTime = caseStudyXml.FillTime,
                PreparationCost = caseStudyXml.PreparationCost,
                PreparationTime = caseStudyXml.PreparationTime,
                PurchaseOrderRecharge = caseStudyXml.PurchaseOrderRecharge,
                InitialCharges = initialCharges,
            };

            if (!Controller.TryValidateModel(caseStudy))
            {
                throw new Exception("Se ha encontrado un error en el archivo XML");
                
            }
            return caseStudy;
        }
    }
}