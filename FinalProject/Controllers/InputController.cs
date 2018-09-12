using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Web.Mvc;
using System;

namespace FinalProject.Controllers
{
    public class InputController : Controller
    {
        public ActionResult SaveInput(UserInput userInput)
        {
            HowsLifeEntities ORM = new HowsLifeEntities();
            ORM.UserInputs.Add(userInput);
            ORM.SaveChanges();
            return RedirectToAction("../Home/Index");
        }
    }
}