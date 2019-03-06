using Microsoft.AspNetCore.Mvc;
using PowerApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerApps.Controllers
{
    public class ControllerWithDataContext : Controller
    {
        internal DataContext _data;
        public ControllerWithDataContext(DataContext dataContext)
        {
            _data = dataContext;
        }
    }
}
