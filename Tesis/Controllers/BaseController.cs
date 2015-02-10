using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlash.Core;

namespace Tesis.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual IFlashPusher Flash { get; private set; }

        protected BaseController()
        {
            Flash = MvcFlash.Core.Flash.Instance;
        }
    }
    
}