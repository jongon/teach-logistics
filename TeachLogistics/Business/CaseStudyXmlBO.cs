using System.Collections.Generic;
using System.Xml.Serialization;

namespace TeachLogisticsTest.Business
{
    [XmlRoot("Caso_De_Estudio")]
    public class CaseStudyXml
    {
        [XmlElement("Recargo_Orden_Compra")]
        public short PurchaseOrderRecharge { get; set; }

        [XmlElement("Cargo_Courier")]
        public double CourierCharges { get; set; }

        [XmlElement("Costo_Preparacion")]
        public int PreparationCost { get; set; }

        [XmlElement("Costo_Anual_Mantener")]
        public double AnnualMaintenanceCost { get; set; }

        [XmlElement("InitialCharge")]
        public List<InitialChargeXml> InitialCharges { get; set; }
    }

    public class InitialChargeXml
    {
        [XmlElement("Tiempo_Preparacion")]
        public byte PreparationTime { get; set; }

        [XmlElement("Tiempo_Surtido")]
        public byte FillTime { get; set; }

        [XmlElement("Tiempo_Entrega")]
        public byte DeliveryTime { get; set; }

        [XmlElement("Demanda")]
        public short Demand { get; set; }

        [XmlElement("Desviacion_Estandar")]
        public short Stddev { get; set; }

        [XmlElement("Precio")]
        public int Price { get; set; }

        [XmlElement("Inventario_Seguridad")]
        public short SecurityStock { get; set; }

        [XmlElement("Inventario_Inicial")]
        public int InitialStock { get; set; }

        [XmlElement("Producto")]
        public string Product { get; set; }
    }
}