using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Metadatas
{
    [DisplayName("Semestre")]
    public class SemesterMetadata
    {  
        public System.Guid Id { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Section> Sections { get; set; }
    }
}