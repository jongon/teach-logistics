using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.ViewModels
{
    public class CreateByXmlViewModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase XmlUpload { get; set; }
    }
}