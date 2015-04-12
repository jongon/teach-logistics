using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using Tesis.Models;

namespace Tesis.Business
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
        }

        public List<InitialCharge> JsonToInitialChargeList(string initialCharges)
        {
            return (List<InitialCharge>)JsonConvert.DeserializeObject(initialCharges, typeof(List<InitialCharge>));
        }
    }
}