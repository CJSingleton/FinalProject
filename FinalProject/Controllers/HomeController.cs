﻿using Newtonsoft.Json.Linq;
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
            HowsLifeEntities ORM = new HowsLifeEntities();
            UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            ViewBag.userInput = lastInput;

            List<string> ageCodes = new List<string> { "DP05_0004E", "DP05_0005E", "DP05_0006E", "DP05_0007E", "DP05_0008E", "DP05_0009E", "DP05_0010E", "DP05_0011E", "DP05_0012E", "DP05_0013E", "DP05_0014E", "DP05_0015E", "DP05_0016E" };
            List<string> ageLabels = new List<string> { "Under 5 years", "5 to 9 years", "10 to 14 years", "15 to 19 years", "20 to 24 years", "25 to 34 years", "35 to 44 years", "45 to 54 years", "55 to 59 years", "60 to 64 years", "65 to 74 years", "75 to 84 years", "85 years and over" };

            int codeIndexAge = ageCodes.IndexOf(lastInput.age);
            string correspondingLabelAge = ageLabels[codeIndexAge];
            ViewBag.AgeLabel = correspondingLabelAge;

            List<string> genderCodes = new List<string> { "DP05_0002E", "DP05_0003E" };
            List<string> genderLabels = new List<string> { "Male", "Female" };

            int codeIndexGender = genderCodes.IndexOf(lastInput.gender);
            string correspondingLabelGender = genderLabels[codeIndexGender];
            ViewBag.GenderLabel = correspondingLabelGender;

            List<string> incomeCodes = new List<string> { "DP03_0052E", "DP03_0053E", "DP03_0054E", "DP03_0055E", "DP03_0056E", "DP03_0057E", "DP03_0058E", "DP03_0059E", "DP03_0060E", "DP03_0061E" };
            List<string> incomeLabels = new List<string> { "Less than $10,000", "$10,000 to $14,999", "$15,000 to $24,999", "$25,000 to $34,999", "$35,000 to $49,999", "$50,000 to $74,999", "$75,000 to $99,999", "$100,000 to $149,999", "$150,000 to $199,999", "$200,000 or more" };

            int codeIndexIncome = incomeCodes.IndexOf(lastInput.incomerange);
            string correspondingLabelIncome = incomeLabels[codeIndexIncome];
            ViewBag.IncomeLabel = correspondingLabelIncome;

            HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME," +
                $"DP05_0002E," +
                $"DP05_0003E," +
                $"DP05_0008E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E," +
                $"DP03_0052E,DP03_0053E,DP03_0054E,DP03_0055E,DP03_0056E,DP03_0057E,DP03_0058E,DP03_0059E,DP03_0060E,DP03_0061E" +
                $",{lastInput.gender},{lastInput.age},{lastInput.incomerange}" +
                $"&for=state:{lastInput.state}");

            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response

                JArray jsonData = JArray.Parse(data);
                
                ViewBag.test1 = jsonData[1];
                ViewBag.userData = lastInput;
            }
            return View();
        }
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