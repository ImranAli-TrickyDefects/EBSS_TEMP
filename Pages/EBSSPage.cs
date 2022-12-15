using EBSS.UI.Tests.Support.Common;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using EBSS.Automated.UI.Tests.Helpers;
using static EBSS.Automated.UI.Tests.TestData.ApplicationData;

namespace EBSS.Automated.UI.Tests.Pages
{
    public class EBSSPage : BasePage
    {
        ScenarioContext context;
        private string homePageUrl;
        readonly string applicationSummaryUrl;

        IWebElement FinanceBarrierStatus => Driver.FindElement(By.Id("finance-barriers"));
        IWebElement FormSectionLink(string sectionName) => Driver.FindElement(By.LinkText(sectionName));
        IWebElement ContinueButton => Driver.FindElement(By.CssSelector("button.btn.btn-success.publish.govuk-button"), 10);

        internal void FindAddress()
        {
            Hyperlink("Start now").Click();
            TextBox("PostCode", "HA03JJ");
            Button("Continue").Click();

        }

        internal void FindInvalidAddress()
        {
            Hyperlink("Start now").Click();
            TextBox("PostCode", "HA03JJ");
            Button("Continue").Click();

        }

        internal void ConfirmAddress()
        {
            RadioButton("IsThisYourAddress").Click(); 
            Button("Continue").Click();
        }

        internal void ConfirmAddressNo()
        {
            RadioButton("IsThisYourAddressNo").Click();
            Button("Continue").Click();
        }

        internal void ReceiveEBSSAlreadyYes()
        {
            RadioButton("HasReceivedPriorEnergyBillPayment").Click();
            Button("Continue").Click();
        }

        internal void ReceiveEBSSAlreadyNo()
        {
            RadioButton("HasReceivedPriorEnergyBillPayment").Click();
            Button("Continue").Click();
        }

        internal void EligibleNo()
        {
            CheckText("You do not need to apply for energy bills support from these schemes");
        }

      
        internal void EnterValidEligibilityDetails()
        {
            //change this to Button when developers change this to <button> tag
            Hyperlink("Continue").Click();
            TextBox("FullName", "John Smith");
            Button("Continue").Click();

            DateBox("Day", "12");
            DateBox("Month", "12");
            DateBox("Year", "2000");
            Button("Continue").Click();

            TextBox("EmailAddress", "JS@JS.COM");
            Button("Continue").Click();

            TextBox("PhoneNumber", "07894321123");
            Button("Continue").Click();

            RadioButton("counciltax-billpayer-yes").Click();
            Button("Continue").Click();

            RadioButton("DoYouHaveABankAccount").Click();
            Button("Continue").Click();

            TextBox("NameOnTheAcccount", "J SMITH");
            TextBox("SortCode", "123456");
            TextBox("AccountNumber", "12345678");
            TextBox("RollNumber", "123456");
            Tickbox("IsABusinessAccount");
            Button("Continue").Click();
            CheckText("Check your answers before sending your application");
            Button("Accept and send");
            CheckText("Application received");


        }

        internal void EligibleYes()
        {
            CheckText("You can apply for the £400 Energy Bills Support Scheme payment");
        }


        //IsThisYourMainHomeYes();
        //RentedHome();
        //PrivateLandlord();
        //GettingDiscounts();
        //CheckText("You do not need to apply for energy bills support from these schemes");

        internal void FindAddressPostcode()
        {
            //          SelectDropDown("SelectedAddress", "8 STAPENHILL ROAD, Wembley, HA0 3JJ");
            SelectDropDown("UPRN", "8 STAPENHILL ROAD, WEMBLEY, HA0 3JJ");
            Button("Continue").Click();
        }

        public void GettingDiscounts()
        {
            RadioButton("HasReceivedPriorEnergyBillPayment").Click(); //getting discounts
            Button("Continue").Click();
        }

        public void PrivateLandlord()
        {
            RadioButton("RentedAccommodationType").Click(); //private landlord
            Button("Continue").Click();
        }

        public void RentedHome()
        {
            RadioButton("WhereDoYouLive").Click(); //rented home
            Button("Continue").Click();
        }

        public void OwnHome()
        {
            RadioButton("WhereDoYouLiveHouseIOwn").Click(); //own house
            Button("Continue").Click();
        }

        public void IsThisYourMainHomeYes()
        {
            RadioButton("IsThisYourMainHome").Click(); 
            Button("Continue").Click();
        }

        internal void ResidentialParkHome()
        {
            RadioButton("WhereDoYouLiveParkHome").Click();
            Button("Continue").Click();
        }

        public void IsThisYourMainHomeNo()
        {
            RadioButton("IsThisYourMainHomeNo").Click();
            Button("Continue").Click();
        }

        public EBSSPage(ScenarioContext context) : base (context)
        {
            this.context = context;
            //applicationSummaryUrl = BaseUrl + "/FundApplication/section/application-summary";
            applicationSummaryUrl = "";
            //homePageUrl = BaseUrl;
            homePageUrl = "http://ebss-dev.uksouth.cloudapp.azure.com/";
        }


        private string FormSectionStatus(string sectionName)
        {
            var sectionRow = Driver.FindElements(By.CssSelector("li.app-task-list__item"))
                                .FirstOrDefault(r=>r.FindElement(By.CssSelector("span.app-task-list__task-name > a")).Text == sectionName);
            return sectionRow.FindElement(By.TagName("strong")).Text;
        }

      

        public void Open()
        {
            Driver.Navigate().GoToUrl(applicationSummaryUrl);
            Wait.Until(x => FinanceBarrierStatus.Displayed);
        }

        internal void ClickToOpenSection(string sectionName)
        {
            FormSectionLink(sectionName).Click();
            Wait.Until(x => ContinueButton.Displayed);
        }

        internal void VerifySectionCompletionStatus(string sectionName, string status)
        {
            FormSectionStatus(sectionName).Should().Be(status);
        }

        internal void VerifyDetailsEnteredInEachBasicInfoSection()
        {
            FormSectionLink("Company Details").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnCompanyDetailsPage(BasicInfo.CompanyName, BasicInfo.CompanyNumber);
            Open();
            FormSectionLink("Project Summary").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnProjectSummaryPage(BasicInfo.ProjectName, BasicInfo.TurnoverDate);
            Open();
            FormSectionLink("Project Details").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnProjectDetailsPage(BasicInfo.BriefProjectDescription, BasicInfo.ProjectCostEndBy2024);
            Open();
            FormSectionLink("Funding").Click();
            context.Get<BasicInformationInputPage>().VerifySelectionFundingPage(BasicInfo.CurrentFunding);
            Open();
        }

        internal void VerifyUpdatedDetailsInEachBasicInfoSection()
        {
            FormSectionLink("Company Details").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnCompanyDetailsPage(BasicInfo.UpdatedCompanyName, BasicInfo.UpdatedCompanyNumber);
            Open();
            FormSectionLink("Project Summary").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnProjectSummaryPage(BasicInfo.UpdatedProjectName, BasicInfo.UpdatedTurnoverDate);
            Open();
            FormSectionLink("Project Details").Click();
            context.Get<BasicInformationInputPage>().VerifyDataOnProjectDetailsPage(BasicInfo.UpdatedBriefProjectDescription, BasicInfo.UpdatedProjectCostEndBy2024);
            Open();
            FormSectionLink("Funding").Click();
            context.Get<BasicInformationInputPage>().VerifySelectionFundingPage(BasicInfo.UpdatedCurrentFunding);
            Open();
        }

 
        public void CompleteBusinessProposalWithoutMandatoryFields()
        {
            //Hyperlink("Start now").Click();
            Button("Accept analytics cookies").Click();
            ClickLink("business-proposal").Click();
            Hyperlink("5.1. Business proposal").Click();
         

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter business proposal before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            //CheckValidationMessage("No file was uploaded.");
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();


            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter business plan before marking as complete");
            TextBoxCharCountdown("test");

            SaveContinue().Click();


            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter technology readiness level before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Select type of technology benefit before marking as complete");
            Tickbox("checkboxSelect-conversion-efficiency").Click();
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter cost and performance details before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            /*** NO. 9**/
            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter infrastructure before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter environmental impact before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter regulations before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter impact on climate change before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project plan before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project evaluation before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter experience and skills before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and return to business proposal overview").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and return to business proposal overview").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();

            Button("Save and return to business proposal overview").Click();

            Hyperlink("5.1. Business proposal").Click();

            ResetTickbox();

            Button("Remove").Click();
            RadioValue("NotApplicable").Click();
            Button("Save and continue to next step").Click();

            ResetTickbox();
            ResetTickbox();

            Button("Remove").Click();
            RadioValue("NotApplicable").Click();
            Button("Save and continue to next step").Click();


            Tickbox("checkboxSelect-conversion-efficiency").Click();
            Button("Save and continue to next step").Click();
            ResetTickbox();

            Button("Remove").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            //8

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            ResetTickbox();
            ResetTickbox();

            ResetTickbox();
            Button("Remove").Click();
            RadioValue("NotApplicable").Click();
            Button("Save and continue to next step").Click();

            ResetTickbox();

            Button("Remove").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            ResetTickbox();

            Button("Remove").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            ResetTickbox();

            Button("Remove").Click();
            RadioValue("NotApplicable").Click();
            Button("Save and continue to next step").Click();

            Button("Remove").Click();
            RadioValue("NotApplicable").Click();

            Button("Save and return to business proposal overview").Click();

        }

       

        internal void CheckFilesNotUploaded()
        {
            CheckText("There is a problem");
        }

        internal void CheckFilesUploaded()
        {
            CheckTextNotDisplayed("There is a problem");
        }

        internal void UploadFiles(Table table)
        {
            foreach (var row in table.Rows)
            {
                UploadItem(row["File"]);

                    //.VerifySectionCompletionStatus(row["Section"], row["Status"]);
            }
            //# Tickbox("fileUploadMarkComplete").Click();
            //# Button("Save and continue to next step").Click();
            //#         //CheckValidationMessage("No file was uploaded.");
            //# CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            //# UploadItem("fileUploadExcel.xlsx");
            //# Tickbox("fileUploadNotComplete").Click();
            //# Button("Save and continue to next step").Click();
            //# CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            //# Tickbox("fileUploadMarkComplete").Click();
            //# Button("Save and continue to next step").Click();
        }

        internal void GoToEqualityDataSurvey()
        {
            Driver.Navigate().GoToUrl(BaseUrl+ "/FundApplication/ApplicationEquality");
        }

        internal void TestDelete()
        {
            Driver.Navigate().GoToUrl("bbc.co.uk");
        }

        internal void CompleteFinanceProposalSummaryAidForResearchAndDevelopmentWithoutMandatoryFields()
        {
            Button("Accept analytics cookies").Click();
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1. Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-yes").Click();
            Button("Continue").Click();

            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            Tickbox("DataInput").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("6.2 File Upload.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter financial background before marking as complete");
            TextBoxCharCountdown("Test");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            Button("Save and return to the financial proposal overview").Click();
            CheckValidationMessage("Enter value for money before marking as complete");
            TextBoxCharCountdown("Test");
            Button("Save and return to the financial proposal overview").Click();

            Hyperlink("6.1. Funding levels and subsidy requirements").Click();
            Button("Continue").Click();

            RadioButton("radio-yes").Click();
            Button("Continue").Click();

            Button("Continue").Click();

            Button("Continue").Click();

            Button("Continue").Click();

            Tickbox("DataInput").Click();
            Button("Save and continue to next step").Click();
            
            Tickbox("MarkAsComplete").Click();
            Button("Remove").Click();
            Button("Save and continue to next step").Click();
     
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and return to the financial proposal overview").Click();

        }

        internal void DownloadDocuments()
        {
            Hyperlink("Download your application").Click();
            Thread.Sleep(1000);
            Hyperlink("Download an accessible text version").Click();
            Thread.Sleep(1000);
            Hyperlink("Application guidance, questions and templates").Click();
            Thread.Sleep(1000);
            Hyperlink("Energy Entrepreneurs Fund phase 9. Application questions").Click();
            Thread.Sleep(1000);
            Hyperlink("Project cost breakdown form").Click();
            Thread.Sleep(1000);
            Hyperlink("Consortium partner details").Click();
            Thread.Sleep(1000);
            Hyperlink("Cost and performance pathway template").Click();
            Thread.Sleep(1000);
            Hyperlink("Risk register").Click();
            Thread.Sleep(1000);
            Hyperlink("Gantt chart").Click();
            Thread.Sleep(1000);
            Hyperlink("Energy Entrepreneurs Fund phase 9. Guidance document").Click();
            Thread.Sleep(1000);

        }

        internal void ConfirmEligibilityYes()
        {
            CheckText("You are eligible to apply");
        }

        internal void ConfirmEligibilityNo()
        {
            CheckText("You are not eligible to apply");
        }

        internal void GoToHomePage()
        {
            Driver.Navigate().GoToUrl(homePageUrl);
        }

        internal void CompletePerformanceDataWithoutMandatoryFields()
        {

            /*** accept analytics for pipelines ***/
            Button("Accept analytics cookies").Click(); 
            ClickLink("performance-data").Click();
            Hyperlink("7.1. Type of development").Click();
            //AnalyzePage(Driver, "PerformanceData");

            RadioValue("Product development").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            TextBox("number-of-employees", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter number of employees before marking as complete");
            TextBox("number-of-employees", "10");
            Button("Save and continue to next step").Click();

            TextBox("fte-jobs-retained", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter full-time employment (fte) jobs retained before marking as complete");
            TextBox("fte-jobs-retained", "10");
            Button("Save and continue to next step").Click();

            TextBox("fte-jobs-created", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter full-time employment (fte) jobs created before marking as complete");
            TextBox("fte-jobs-created", "10");
            Button("Save and continue to next step").Click();

            TextBox("number-of-partner-organisations", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter number of partner organisations before marking as complete");
            TextBox("number-of-partner-organisations", "10");
            Button("Save and continue to next step").Click();

            RadioValue("1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            TextBox("consumer-trials-participant", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter consumer trials participants before marking as complete");
            TextBox("consumer-trials-participant", "10");
            Button("Save and continue to next step").Click();

            RadioValue("TRL1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            RadioValue("TRL1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Select current market barriers before marking as complete");
            Tickbox("checkboxSelect-none-of-the-above").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and return to performance data").Click();
            CheckValidationMessage("Select innovation benefits before marking as complete");
            Tickbox("checkboxSelect-increasing-investment-in-the-uk").Click();


            Button("Save and return to performance data").Click();
             Hyperlink("Back to application overview").Click();
            CheckText("Application overview");

            ClickLink("performance-data").Click();
            Hyperlink("7.1. Type of development").Click();
            for (int i=0;i<9; i++)
            { ResetTickbox(); }
            Tickbox("checkboxSelect-none-of-the-above").Click();
            SaveContinue().Click();
            Tickbox("checkboxSelect-increasing-investment-in-the-uk").Click();

            Button("Save and return to performance data").Click();
            Hyperlink("Back to application overview").Click();
            CheckText("Application overview");

        }

        internal void CompletePerformanceData()
        {
            ClickLink("performance-data").Click();
            Hyperlink("7.1. Type of development").Click();

            RadioValue("Product development").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            TextBox("number-of-employees", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter number of employees before marking as complete");
            TextBox("number-of-employees", "10");
            Button("Save and continue to next step").Click();

            TextBox("fte-jobs-retained", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter full-time employment (fte) jobs retained before marking as complete");
            TextBox("fte-jobs-retained", "10");
            Button("Save and continue to next step").Click();

            TextBox("fte-jobs-created", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter full-time employment (fte) jobs created before marking as complete");
            TextBox("fte-jobs-created", "10");
            Button("Save and continue to next step").Click();

            TextBox("number-of-partner-organisations", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter number of partner organisations before marking as complete");
            TextBox("number-of-partner-organisations", "10");
            Button("Save and continue to next step").Click();

            RadioValue("1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            TextBox("consumer-trials-participant", "");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter consumer trials participants before marking as complete");
            TextBox("consumer-trials-participant", "10");
            Button("Save and continue to next step").Click();

            RadioValue("TRL1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            RadioValue("TRL1").Click();
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Select current market barriers before marking as complete");
            Tickbox("checkboxSelect-none-of-the-above").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and return to performance data").Click();
            CheckValidationMessage("Select innovation benefits before marking as complete");
            Tickbox("checkboxSelect-increasing-investment-in-the-uk").Click();


            Button("Save and return to performance data").Click();
         Hyperlink("Back to application overview").Click();
            CheckText("Application overview");


        }

        internal void CompleteFinanceProposal()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1. Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-yes").Click();
            Button("Save and continue to next question").Click();
            Button("Continue").Click();
            RadioButton("radio-yes").Click();
            Button("Continue").Click();
            RadioButton("radio-yes").Click();
            Button("Continue").Click();
            Button("Save and continue to next question").Click();
            Button("Save and continue to next question").Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
            Hyperlink("Back to application overview").Click();
        }

        internal void CompleteExperimentalDevelopment()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1 Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-no").Click();
            Button("Save and continue to next question").Click();
            RadioValue("2").Click();
            Button("Continue").Click();
            Button("Save and continue to next question").Click();
            Button("Save and continue to next question").Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
        }

        internal void SubmitApplication()
        {
 

            Hyperlink("Submit application").Click();

            Hyperlink("Accept and send application").Click();
            CheckText("Application complete");
            CheckText("We have received your application");

            Hyperlink("Continue");
        }

        internal void CompleteIndustrialResearch()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1 Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-no").Click();
            Button("Save and continue to next question").Click();
            RadioValue("1").Click();
            Button("Continue").Click();
            Button("Save and continue to next question").Click();
            Button("Save and continue to next question").Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
        }

        internal void CompleteSummaryOfFinancesWithoutMandatory()
        {
            /*** accept analytics for pipelines ***/
            Button("Accept analytics cookies").Click();
            ClickLink("summary-of-finances").Click();
            ClickLink("balance-sheet-total").Click();

         
            Thread.Sleep(3000);


            //AnalyzePage(Driver, "CompleteSummaryOfFinances");
            TextBox("balance-sheet-total", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter balance sheet total before marking as complete");

            TextBox("balance-sheet-total", "100000");
            SaveContinue().Click();


            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "");

            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must include a year");

            DateBox("Year", "2023");
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must be in the past");

            DateBox("Month", "13");
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must be a real date");

            DateBox("Month", "01");
            DateBox("Year", "2021");
            SaveContinue().Click();

            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            TextBox("recent-turnover", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter recent turnover before marking as complete");
            TextBox("recent-turnover", "100000");
            SaveContinue().Click();


            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "");

            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must include a year");

            DateBox("Year", "2023");
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must be in the past");

            DateBox("Month", "13");
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must be a real date");

            DateBox("Month", "01");
            DateBox("Year", "2021");
            SaveContinue().Click();


         Hyperlink("Back to application overview").Click();

            ClickLink("summary-of-finances").Click();
            ClickLink("balance-sheet-total").Click();
            ResetTickbox();
            ResetTickbox();
            ResetTickbox();
            ResetTickbox();
            ResetTickbox();
            ResetTickbox();
        }

       

        public void ResetTickbox() {
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
        }
        internal void CompleteFinanceFeasibilityStudy()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1 Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-no").Click();
            Button("Save and continue to next question").Click();
            RadioValue("0").Click();
            Button("Continue").Click();
            Button("Save and continue to next question").Click();
            Button("Save and continue to next question").Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
            TextBoxCharCountdown("Test");
            SaveContinue().Click();
        }

        internal void CompleteFinanceProposalFlowTwo()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1 Funding levels and subsidy requirements").Click();
            Button("Continue").Click();
            RadioButton("radio-yes").Click();
            Button("Continue").Click();
            Button("Continue").Click();
            RadioValue("1").Click();
            Button("Continue").Click();
            Button("Save and continue to next step").Click();
            Button("Save and continue to next step").Click();
            TextBoxCharCountdown("Test");
            Button("Save and continue to next step").Click();
            TextBoxCharCountdown("Test");
            Button("Save and return to the financial proposal").Click();

          }

        internal void CompleteSummaryOfFinances()
        {
            ClickLink("summary-of-finances").Click();
            ClickLink("balance-sheet-total").Click();

            TextBox("balance-sheet-total", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter balance sheet total before marking as complete");

            TextBox("balance-sheet-total", "100000");
            SaveContinue().Click();
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "");

            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must include a year");

            DateBox("Year", "2023");
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must be in the past");

            DateBox("Month", "13");
            SaveContinue().Click();
            CheckValidationMessage("Balance sheet date must be a real date");

            DateBox("Month", "01");
            DateBox("Year", "2021");
            SaveContinue().Click();

            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            TextBox("recent-turnover", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter recent turnover before marking as complete");
            TextBox("recent-turnover", "100000");
            SaveContinue().Click();


            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "");

            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must include a year");

            DateBox("Year", "2023");
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must be in the past");

            DateBox("Month", "13");
            SaveContinue().Click();
            CheckValidationMessage("Turnover date must be a real date");

            DateBox("Month", "01");
            DateBox("Year", "2021");
            SaveContinue().Click();

         Hyperlink("Back to application overview").Click();


        }

        internal void CompleteYourOrganisationWithoutMandatoryFields()
        {
            ClickLink("your-organisation").Click();
            Hyperlink("Organisation name").Click();
            //AnalyzePage(Driver, "Your Organisation");
            TextBox("organisation-name", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation name before marking as complete");
            TextBox("organisation-name", "Test Company");
            SaveContinue().Click();

            TextBox("organisation-website", "www.TestCo.com");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            TextBox("AddressLine1", "");
            TextBox("AddressLine2", "");
            TextBox("City", "");
            TextBox("PostCode", "");
            TextBox("County", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Please add your address to the address field");
            CheckValidationMessage("Please add the current city or town your business is based in into the town or city field");
            CheckValidationMessage("Please add your postcode into the Postcode field");

            TextBox("AddressLine1", "1");
            TextBox("AddressLine2", "London Road");
            TextBox("City", "London");
            TextBox("PostCode", "SE1 1EE");
            TextBox("County", "Surrey");
            SaveContinue().Click();

            TextBox("company-registration-number", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter company registration number before marking as complete");
            TextBox("company-registration-number", "123456");
            SaveContinue().Click();
            CheckValidationMessage("Company registration number must be between 7 and 9 characters");

            TextBox("company-registration-number", "12345678");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation introduction before marking as complete");

            TextBoxCharCountdown("Test Org");
            SaveContinue().Click();

            RadioValue("Private company").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("10 or fewer (micro enterprise)").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("Pre-startup").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Select organisation funding before marking as complete");

            Tickbox("checkboxSelect-no-funding").Click();
            SaveContinue().Click();
            RadioValue("0").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            RadioValue("1").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            Hyperlink("Back to application overview").Click();

            ClickLink("your-organisation").Click();
            Hyperlink("Organisation name").Click();
            for (int i = 1; i < 9; i++)
            {
                ResetTickbox();
            }
            Tickbox("checkboxSelect-no-funding").Click();
            for (int i = 1; i < 5; i++)
            {
                ResetTickbox();
            }
            }

        internal void CompleteYourOrganisationConsortiumNO()
        {
            ClickLink("your-organisation").Click();
            Hyperlink("Organisation name").Click();

            TextBox("textInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation name before marking as complete");
            TextBox("textInput", "Test Company");
            SaveContinue().Click();

            TextBox("textInput", "www.TestCo.com");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            TextBox("address-line-1", "");
            TextBox("address-line-2", "");
            TextBox("address-town", "");
            TextBox("address-postcode", "");
            TextBox("address-county", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Please add your Address to the Address field");
            CheckValidationMessage("Please add the current city or town your business is based in into the Town or City field");
            CheckValidationMessage("Please add your postcode into the Postcode field");


            TextBox("address-line-1", "1");
            TextBox("address-line-2", "London Road");
            TextBox("address-town", "London");
            TextBox("address-postcode", "SE1 1EE");
            TextBox("address-county", "Surrey");
            SaveContinue().Click();

            TextBox("textInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter company registration number before marking as complete");
            TextBox("textInput", "123456");
            SaveContinue().Click();
            CheckValidationMessage("Company registration number must be between 7 and 9 characters");

            TextBox("textInput", "12345678");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation introduction before marking as complete");

            TextBoxCharCountdown("Test Org");
            SaveContinue().Click();

            RadioValue("Private company").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("11 to 50 employees (small enterprise)").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("Pre-startup").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Select organisation funding before marking as complete");

            Tickbox("checkboxSelect-no-funding").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("No file was uploaded.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            RadioValue("0").Click();

            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            TextBox("textInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter parent organisation");
            TextBox("textInput", "Test parent organisation");
            SaveContinue().Click();

            TextBox("textInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter parent organisation website before marking as complete");
            TextBox("textInput", "Test parent organisation website");
            SaveContinue().Click();

            TextBox("address-line-1", "");
            TextBox("address-line-2", "");
            TextBox("address-town", "");
            TextBox("address-postcode", "");
            TextBox("address-county", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Please add your Address to the Address field");
            CheckValidationMessage("Please add the current city or town your business is based in into the Town or City field");
            CheckValidationMessage("Please add your postcode into the Postcode field");


            TextBox("address-line-1", "1");
            TextBox("address-line-2", "London Road");
            TextBox("address-town", "London");
            TextBox("address-postcode", "SE1 1EE");
            TextBox("address-county", "Surrey");
            SaveContinue().Click();


            TextBox("textInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("registration number before marking as complete");
            TextBox("textInput", "123456");
            SaveContinue().Click();
            CheckValidationMessage("number must be between 7 and 9 characters");
            TextBox("textInput", "12345678");
            SaveContinue().Click();

            RadioValue("11 to 50 employees (small enterprise)").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            RadioValue("Pre-startup").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter turnover before marking as complete");
            TextBox("currencyInput", "123000");
            SaveContinue().Click();

            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2021");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            TextBox("currencyInput", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter balance sheet before marking as complete");
            TextBox("currencyInput", "123456");
            SaveContinue().Click();

            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2021");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

        }

        internal void CompleteYourOrganisationConsortiumYES()
        {
            ClickLink("your-organisation").Click();
            Hyperlink("Organisation name").Click();

            TextBox("organisation-name", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation name before marking as complete");
            TextBox("organisation-name", "Test Company");
            SaveContinue().Click();

            TextBox("organisation-website", "www.TestCo.com");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            TextBox("AddressLine1", "");
            TextBox("AddressLine2", "");
            TextBox("City", "");
            TextBox("PostCode", "");
            TextBox("County", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Please add your address to the address field");
            CheckValidationMessage("Please add the current city or town your business is based in into the town or city field");
            CheckValidationMessage("Please add your postcode into the Postcode field");

            TextBox("AddressLine1", "1");
            TextBox("AddressLine2", "London Road");
            TextBox("City", "London");
            TextBox("PostCode", "SE1 1EE");
            TextBox("County", "Surrey");
            SaveContinue().Click();

            TextBox("company-registration-number", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter company registration number before marking as complete");
            TextBox("company-registration-number", "123456");
            SaveContinue().Click();
            CheckValidationMessage("Company registration number must be between 7 and 9 characters");

            TextBox("company-registration-number", "12345678");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter organisation introduction before marking as complete");

            TextBoxCharCountdown("Test Org");
            SaveContinue().Click();

            RadioValue("Private company").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("10 or fewer (micro enterprise)").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            RadioValue("Pre start-up").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("checkboxSelect-no-funding").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Select organisation funding before marking as complete");

            Tickbox("checkboxSelect-no-funding").Click();
            SaveContinue().Click();
            RadioValue("0").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();


            RadioValue("1").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            Hyperlink("Back to application overview").Click();

           

        }

        internal void CheckApplicationOverview()
        {
            CheckText("Application overview");

       }

        internal void CompleteDetailsEligibilityWithoutMandatory()
        {
            /**** FOR PIPELINES ****/
            Button("Accept analytics cookies").Click();
            ClickLink("your-details-and-eligibility").Click();
            ClickLink("eligibility-to-apply-for-fund").Click();
            //AnalyzePage(Driver, "eligibilityPage");
            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            TextBox("full-name", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            CheckValidationMessage("Enter full name before marking as complete");

            TextBox("full-name", "John Smith");
            SaveContinue().Click();

            TextBox("job-title", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            CheckValidationMessage("Enter job title before marking as complete");


            TextBox("job-title", "Developer");
            SaveContinue().Click();
            Hyperlink("Back to application overview").Click();


            //RESET VALUES
            ClickLink("your-details-and-eligibility").Click();
            ClickLink("eligibility-to-apply-for-fund").Click();
            ResetTickbox();
            ResetTickbox();
            ResetTickbox();

        }
        public void CompleteYourDetailsAndEligibility()
        {
            //Button("Accept analytics cookies").Click();
            ClickLink("your-details-and-eligibility").Click();
            ClickLink("eligibility-to-apply-for-fund").Click();
          
            Thread.Sleep(3000);
            RadioButton("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            TextBox("full-name", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            CheckValidationMessage("Enter full name before marking as complete");

            TextBox("full-name", "John Smith");
            SaveContinue().Click();

            TextBox("job-title", "");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            CheckValidationMessage("Enter job title before marking as complete");


            TextBox("job-title", "Developer");
            SaveContinue().Click();
            Hyperlink("Back to application overview").Click();


       
        }

        internal void CompleteFinanceProposalSummaryAidForResearchAndDevelopment()
        {
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1. Funding levels and subsidy requirements").Click();
            Button("Continue").Click();

            RadioButton("radio-yes").Click();
            Button("Continue").Click();

            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            Tickbox("DataInput").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("No file was uploaded.");
            UploadItem("6.2 File Upload.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter financial background before marking as complete");
            TextBoxCharCountdown("Test");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            Button("Save and return to the financial proposal overview").Click();
            CheckValidationMessage("Enter value for money before marking as complete");
            TextBoxCharCountdown("Test");
            Button("Save and return to the financial proposal overview").Click();


        }

        public void CompleteFinanceProposalFlowOne()
        {
            //AnalyzePage(Driver,"CompleteFinanceProposal");
            ClickLink("finance-proposal").Click();
            Hyperlink("6.1. Funding levels and subsidy requirements").Click();
            Button("Continue").Click();

            RadioButton("radio-yes").Click();
            Button("Continue").Click();

            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            RadioButton("radiobutton-0").Click();
            Button("Continue").Click();

            Tickbox("DataInput").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("6.2 File Upload.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Enter financial background before marking as complete");
            TextBoxCharCountdown("Test");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            TextBoxCharCountdown("Test");
            Button("Save and return to the financial proposal overview").Click();
            Hyperlink("Back to application overview").Click();
        }



        public void CompleteBusinessProposal()
        {

       
            ClickLink("business-proposal").Click();
            Hyperlink("5.1. Business proposal").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter business proposal before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            //CheckValidationMessage("No file was uploaded.");
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();


            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter business plan before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter technology readiness level before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("Select type of technology benefit before marking as complete");
            Tickbox("checkboxSelect-conversion-efficiency").Click();
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter cost and performance details before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            /*** NO. 9**/
            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter infrastructure before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter environmental impact before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter regulations before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter impact on climate change before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project plan before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project evaluation before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("MarkAsComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("This section has a mandatory file upload. You must first choose your file and then upload it using the upload button below.");
            UploadItem("fileUploadExcel.xlsx");
            Button("Save and continue to next step").Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter experience and skills before marking as complete");
            TextBoxCharCountdown("test");
            SaveContinue().Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and continue to next step").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and continue to next step").Click();

            Tickbox("fileUploadMarkComplete").Click();
            Button("Save and return to business proposal overview").Click();
            CheckValidationMessage("You have not uploaded any evidence. Please upload a file before marking as complete. If this step is not relevant to your application please select");
            UploadItem("fileUploadExcel.xlsx");
            Tickbox("fileUploadNotComplete").Click();
            Button("Save and return to business proposal overview").Click();
            CheckValidationMessage("You have told us this step is not applicable, your evidence will not be added to your proposal");
            Tickbox("fileUploadMarkComplete").Click();

            Button("Save and return to business proposal overview").Click();
            Hyperlink("Back to application overview").Click();
        }

        public void CompleteConsortiumUploadFile()
        {
            ClickLink("consortium-applicant").Click();
            RadioButton("radio-no").Click();
            ButtonId("SaveAndContinue");
            UploadItem("TestPdf.pdf");
            Button("Save and return to application overview").Click();
        }

        public void CompleteConsortium()
        {
            ClickLink("consortium-applicant").Click();
            RadioButton("radio-yes").Click();
            ButtonId("SaveAndContinue");
            CheckText("Application overview");
       
        }

        internal void CompleteEqualityDataWhiteMale()
        {
            //AnalyzePage(Driver, "EqualityData");
            RadioButton("yes-equality").Click();
            Button("Accept analytics cookies").Click();
            Thread.Sleep(3000);
            Button("Continue").Click();
            RadioValue("1").Click();
            javaScript("SaveAndContinue");
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            javaScript("SaveAndContinue");
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            CheckText("Thank you for completing the equality questions");

        }

        public void CompleteEqualityWithoutMandatoryFields()
        {
            Button("Submit application").Click();
            Button("Accept and send application").Click();
            RadioButton("yes-equality").Click();
            Button("Continue").Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            CheckText("Thank you for completing the equality questions");
            Hyperlink("Return to application overview").Click();
            CheckText("Application overview");

        }

        public void CompleteEqualityDataAsianFemale()
        {
            Button("Submit application").Click();
            Button("Accept and send application").Click();
            RadioButton("yes-equality").Click();
            Button("Continue").Click();
            RadioValue("0").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("2").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            RadioValue("1").Click();
            SaveContinue().Click();
            CheckText("Thank you for completing the equality questions");
            Hyperlink("Return to application overview").Click();
            CheckText("Application overview");

        }

        public void GoToApplicationOverview()
        {
            Thread.Sleep(2000);
            Hyperlink("Apply for the Energy Entrepreneurs Fund: phase 9").Click();

        }

        public void ReturnApplicationOverview()
        {
            //   Thread.Sleep(2000);
            //  Hyperlink("Apply for the Energy Entrepreneurs Fund: phase 9").Click();

            /***** TO BE ADDED WHEN APPLICATION IS COMPLETED ******/
            // CheckText("Application complete");
            // CheckText("You submitted your application");
        }

        public void CompleteProposalOverview()
        {

            ClickLink("proposal-summary").Click();
            Thread.Sleep(4000);
            Hyperlink("4.1. Project name").Click();
            Thread.Sleep(4000);
            //AnalyzePage(Driver, "ProposalOverview");
            ClearTextBox("project-name");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project name before marking as complete");
            TextBox("project-name", "test project");
            SaveContinue().Click();

            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2025");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Project start date must be the same as or before 31st March 2023");
            DateBox("Day", "");
            DateBox("Month", "");
            DateBox("Year", "");
            SaveContinue().Click();
            CheckValidationMessage("Enter project start date");
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2023");
            SaveContinue().Click();

            DateBox("Day", "30");
            DateBox("Month", "03");
            DateBox("Year", "2022");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Project end date must be after the 31st March 2022");
            DateBox("Day", "");
            DateBox("Month", "");
            DateBox("Year", "");
            SaveContinue().Click();
            CheckValidationMessage("Enter project end date");
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2023");
            SaveContinue().Click();

            Tickbox("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project summary before marking as complete");

            TextBoxCharCountdown("Test Project summary");
            SaveContinue().Click();





            Hyperlink("Back to application overview").Click();

        }

        public void CompleteProposalOverviewWIthoutMandatory()
        {
            /*** accept analytics for pipelines ***/
            Button("Accept analytics cookies").Click();
            Thread.Sleep(4000);
            ClickLink("proposal-summary").Click();
            Thread.Sleep(4000);
            Hyperlink("4.1. Project name").Click();
            Thread.Sleep(4000);
            //AnalyzePage(Driver, "ProposalOverview");
            ClearTextBox("project-name");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project name before marking as complete");
            TextBox("project-name", "test project");
            SaveContinue().Click();  
            
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2025");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Project start date must be the same as or before 31st March 2023");
            DateBox("Day", "");
            DateBox("Month", "");
            DateBox("Year", "");
            SaveContinue().Click();
            CheckValidationMessage("Enter project start date");
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2023");
            SaveContinue().Click();

            DateBox("Day", "30");
            DateBox("Month", "03");
            DateBox("Year", "2022");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Project end date must be after the 31st March 2022");
            DateBox("Day", "");
            DateBox("Month", "");
            DateBox("Year", "");
            SaveContinue().Click();
            CheckValidationMessage("Enter project end date");
            DateBox("Day", "01");
            DateBox("Month", "01");
            DateBox("Year", "2023");
            SaveContinue().Click();

            Tickbox("radio-yes").Click();
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();

            TextBoxCharCountdown("");
            Tickbox("MarkAsComplete").Click();
            SaveContinue().Click();
            CheckValidationMessage("Enter project summary before marking as complete");
            
            TextBoxCharCountdown("Test Project summary");
            SaveContinue().Click();

            Hyperlink("4.1. Project name").Click();

            for (int i = 0; i < 5; i++)
            {
                ResetTickbox();
            }






        }

    
    }
}
