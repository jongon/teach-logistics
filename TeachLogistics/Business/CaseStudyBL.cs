using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Business
{
    public class CaseStudyBL
    {
        public void ModelStateForXML(ModelStateDictionary model) {
            model.Remove("Demand");
            model.Remove("Stddev");
            model.Remove("Price");
            model.Remove("PreparationCost");
            model.Remove("AcceleratedPreparationTime");
            model.Remove("ExistingFillTime");
            model.Remove("CourierDeliveryTime");            
            model.Remove("AnnualMaintenanceCost");
            model.Remove("PurchaseOrderRecharge");
            model.Remove("CourierCharges");
            model.Remove("PreparationTime");
            model.Remove("FillTime");
            model.Remove("DeliveryTime");
            model.Remove("SecurityStock");
            model.Remove("InitialStock");
            model.Remove("ProductId");
        }

        public void ModelStateInForm(ModelStateDictionary model)
        {
            model.Remove("XmlUpload");
            model.Remove("Demand");
            model.Remove("Stddev");
            model.Remove("Price");
            model.Remove("SecurityStock");
            model.Remove("InitialStock");
            model.Remove("ProductId");
            model.Remove("FillTimeOption");
            model.Remove("DeliveryTimeOption");
            model.Remove("PreparationTimeOption");
        }

        public List<InitialCharge> JsonToInitialChargeList(string initialCharges)
        {
            return (List<InitialCharge>)JsonConvert.DeserializeObject(initialCharges, typeof(List<InitialCharge>));
        }

        public InitialCharge ChangeTimes(CaseStudyViewModel caseStudy, InitialCharge initialCharge)
        {
            if (initialCharge.PreparationTime == 1)
            {
                initialCharge.PreparationTime = caseStudy.PreparationTime;
            }
            else if (initialCharge.PreparationTime == 0)
            {
                initialCharge.PreparationTime = caseStudy.AcceleratedPreparationTime;
            }

            if (initialCharge.FillTime == 1)
            {
                initialCharge.FillTime = caseStudy.FillTime;
            }
            else if (initialCharge.FillTime == 0)
            {
                initialCharge.FillTime = caseStudy.ExistingFillTime;
            }

            if (initialCharge.DeliveryTime == 1)
            {
                initialCharge.DeliveryTime = caseStudy.DeliveryTime;
            }
            else if (initialCharge.DeliveryTime == 0)
            {
                initialCharge.DeliveryTime = caseStudy.CourierDeliveryTime;
            }

            return initialCharge;
        }

        public CaseStudy CreateCase()
        {
            return new CaseStudy
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                AnnualMaintenanceCost = 0.5,
                PreparationCost = 100,
                CourierCharges = 0.005,
                PurchaseOrderRecharge = 100,
            };
        }

        public List<InitialCharge> CreateCharges()
        {
            throw new NotImplementedException();
        }
    }
}