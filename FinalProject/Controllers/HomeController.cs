using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;    
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP05_0009E,DP05_0002E,DP02_0004E,DP02_0005E,DP03_0004E,DP03_0057E,DP04_0096E,DP04_0129E&for=county+subdivision:33460&in=state:24+county:033");
            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response

                JArray jsonData = JArray.Parse(data);
                
                ViewBag.test1 = jsonData/*[1]*/;
                //ViewBag.triviadate = jsonCensusData["year"];

            }
            return View();
        }
        public ActionResult PresentInput()
        {
            HowsLifeEntities ORM = new HowsLifeEntities();
            List<UserInput> infoList = ORM.UserInputs.ToList();
            ViewBag.userInput = infoList;
            return View("PresentInput");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Questions()
        {
            ViewBag.Message = "Your form page.";

            return View();
        }
        public ActionResult frontendtemp()
        {
            return View();
        }
        public ActionResult apiTest(string state)
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP02_0001E,DP02_0002E,DP02_0003E&for=state:" + state + "&for=cd115:*");
            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response
                JArray jsonData = JArray.Parse(data);
                ViewBag.test1 = jsonData/*[1]*/;
                //ViewBag.triviadate = jsonCensusData["year"];
            }
            return View();
        }
        public ActionResult QuestionsTemp()
        {
            return View();
        }
        public ActionResult TempOut()
        {
            return View();
        }
    }
}