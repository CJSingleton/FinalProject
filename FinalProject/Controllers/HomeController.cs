using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Configuration;
using FinalProject.Models;
using System;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP05_0009E,DP05_0002E,DP02_0004E,DP02_0005E,DP03_0004E,DP03_0057E,DP04_0096E,DP04_0129E&for=state:26");
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

        //public ActionResult ApiQuery(int id, string incomerange, string age, string state, string gender)
        //{
        //    HowsLifeEntities ORM = new HowsLifeEntities();
        //    List<UserInput> dataString = ORM.UserInputs.Where(x => x.userid == id).ToList();
        //    UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            
        //    HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,{incomerange},{gender}&for=state:{state}");
        //    apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
        //    apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
        //    HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

        //    if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
        //    {
        //        StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
        //        string data = responseData.ReadToEnd(); //reads data from the response

        //        JArray jsonData = JArray.Parse(data);

        //        ViewBag.test1 = jsonData/*[1]*/;
        //        //ViewBag.triviadate = jsonCensusData["year"];

        //    }
        //}
        public ActionResult PresentInput()
        {
            HowsLifeEntities ORM = new HowsLifeEntities();
            
            UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            ViewBag.userInput = lastInput;
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
        public ActionResult TempOut(string state)
        {
            //apis for income
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP03_0052PE,DP03_0053PE,DP03_0054PE,DP03_0055PE,DP03_0056PE,DP03_0057PE,DP03_0058PE,DP03_0059PE,DP03_0060PE,DP03_0061PE&for=state:" + state);
            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response
                JArray jsonData = JArray.Parse(data);
                List<JToken> listy = jsonData.ToList();
                ViewBag.test1 = Convert.ToString(listy[1][0]);
               // List<string> listofIncome = listy.Where(x => !(x == null ||  x == listy[listy.Count -1])).ToList();

                double Less10 = Convert.ToDouble(listy[1][1]);
                ViewBag.Less10 = Less10;
                double to14 = Convert.ToDouble(listy[1][2]);
                ViewBag.to14 = to14;
                double to24 = Convert.ToDouble(listy[1][3]);
                ViewBag.to24 = to24;
                double to34 = Convert.ToDouble(listy[1][4]);
                ViewBag.to34 = to34;
                double to49 = Convert.ToDouble(listy[1][5]);
                ViewBag.to49 = to49;
                double to74 = Convert.ToDouble(listy[1][6]);
                ViewBag.to74 = to74;
                double to99 = Convert.ToDouble(listy[1][7]);
                ViewBag.to99 = to99;
                double to149 = Convert.ToDouble(listy[1][8]);
                ViewBag.to149 = to149;
                double to199 = Convert.ToDouble(listy[1][9]);
                ViewBag.to199 = to199;
                ////ViewBag.triviadate = jsonCensusData["year"];
            }
            return View();
        }

        private string JsonConvert(JToken jToken)
        {
            throw new NotImplementedException();
        }
    }
}