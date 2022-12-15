using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using EBSS.UI.Tests.Support.Driver;
using EBSS.Automated.UI.Tests.Helpers;
using EBSS.Automated.UI.Tests.Pages;
using EBSS.Automated.UI.Tests.Pages.CMS;
using EBSS.Automated.UI.Tests.TestSetUp;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace EBSS.Automated.UI.Tests.Utils
{

    [Binding]
    public sealed class Hooks
    {
        private ScenarioContext context;
        
        private static AventStack.ExtentReports.ExtentReports extent;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        public Hooks(ScenarioContext context)
        {
            this.context = context;
        }

        [BeforeTestRun]
        public static void ReportInitialize()
        {
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(ReportHelpers.InitializeAndGetReporter());
            ReportHelpers.TimeStamp = DateTime.Now.ToString("ddMMyy");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario("UserCleanUp")]
        public void UserCleanUp()
        {
            context.Set(new LoginPage(context));
            context.Set(new UsersPage(context));
            context.Get<UsersPage>().DeleteTestUsers();
         //   JsonHelpers json = new JsonHelpers();
          //  context.Set(json.LoadJson());
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            scenario = feature.CreateNode<Scenario>(context.ScenarioInfo.Title);
            context.Set(new DriverSetUp().SetUpDriver(TestSettings.Options));
            //context.Get<IWebDriver>().Navigate().GoToUrl(ConfigurationSetUp.BaseUrl);
            context.Set(new ReportHelpers(context));
        //    JsonHelpers json = new JsonHelpers();
         //   context.Set(json.LoadJson());
        }


        [AfterScenario]
        public void AfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext) 
        {
            extent.Flush();
            //if (scenarioContext.ScenarioInfo.Title)
            Screenshot ss = ((ITakesScreenshot)context.Get<IWebDriver>()).GetScreenshot();
            string path = Directory.GetCurrentDirectory() + "SearchTestScreenshot.png" ;
            ss.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            context.Get<IWebDriver>().Quit();
            context.Get<ReportHelpers>().AttachTestArtifact();
            context.Clear();
        }

        [BeforeStep]
        public void BeforeStep()
        {
            
        }

        [AfterStep]
        public void AfterStep()
        {
            var text = context.TestError == null ? $" --> done: {context.StepContext.StepInfo.Text}" : " --> ERROR :";
            context.Get<ReportHelpers>().InsertStepsInReport(feature, scenario);

        }
    }
}
