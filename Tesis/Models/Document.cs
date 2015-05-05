using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Document
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        public string Path { get; set; }
    }
}