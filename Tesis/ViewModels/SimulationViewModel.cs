using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class SellViewModel {

        [DisplayName("Sección")]
        public virtual Section Section { get; set; }

        [DisplayName("Sección")]
        [Required(ErrorMessage = "El Id de la sección es requerido")]
        public virtual Guid SectionId { get; set; }

        public ICollection<Product> Products { get; set; }

        [DisplayName("Productos")]
        [Required(ErrorMessage = "Los Productos son requeridos")]
        [UIHint("Products")]
        public ICollection<ProductSell> ProductSells { get; set; }
    }

    public class ProductSell
    {
        [DisplayName("Producto")]
        public Product Product { get; set; }

        [DisplayName("Cantidad")]
        [Required(ErrorMessage = "La Cantidad de productos es requerida")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int Quantity { get; set; }
    }
}