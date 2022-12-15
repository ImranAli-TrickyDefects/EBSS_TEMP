using EBSS.UI.Tests.Support.Common;
using FluentAssertions;
using EBSS.Automated.UI.Tests.TestData;
using EBSS.Automated.UI.Tests.Utils;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace EBSS.Automated.UI.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly string loginUrl;
        private readonly string invalidLoginValidation = "Invalid login attempt. If you believe your account has been locked, please allow 5 minutes before logging in";
        ScenarioContext context;
        private string businessProposalfileUploadUrl;
        private string financeProposalfileUploadUrl;

        public LoginPage(ScenarioContext context) : base(context)
        {
            this.context = context;
            loginUrl = BaseUrl + "/Account/Login";
            businessProposalfileUploadUrl = BaseUrl + "/FundApplication/section/business-proposal-evidence";
            financeProposalfileUploadUrl = BaseUrl + "/FundApplication/section/project-cost-breakdown";
        }

        internal void OpenBusinessProposalUploadFilePage()
        {
            Driver.Navigate().GoToUrl(businessProposalfileUploadUrl);
        }

        internal void OpenFinanceProposalUploadFilePage()
        {
            Driver.Navigate().GoToUrl(financeProposalfileUploadUrl);
        }


        private IWebElement EmailAddress => Comp.TextInput("EmailAddress");

    
        private IWebElement Password => Comp.TextInput("Password");

        internal void RegisterNewUsers(Table table)
        {
            

            var credentials = table.CreateSet<Credentials>();
            foreach (var loginData in credentials)
            {
                Driver.Navigate().GoToUrl(loginUrl);
                Hyperlink("Register as a new user").Click();
                TextBox("Email", loginData.username);
                TextBox("ConfirmEmail", loginData.username);
                TextBox("Password", loginData.password);
                TextBox("ConfirmPassword", loginData.password);

                //var username = loginData.username;
                Tickbox("IsConsentedToUsePersonalInformation").Click();
                Tickbox("IsConsentedUseEmailAndSms").Click();

                Button("Create and continue").Click();
                TextBox("OrganisationName", "Test");
                Button("Continue").Click();




            }
        }

        internal void VerifyRegistrationValidation(string validationMessage)
        {
            CheckValidationMessage(validationMessage);
            Tickbox("IsConsentedToUsePersonalInformation").Click();
            Tickbox("IsConsentedUseEmailAndSms").Click();

        }

     

        internal void EnterRegistrationValuesAndSubmit(String email, String password)
        {
            TextBox("Email", email);
            if (email == "Missing")
            {
                TextBox("ConfirmEmail", "");
            }
            else
            {
                TextBox("ConfirmEmail", email);
            }
            TextBox("Password", password);
            TextBox("ConfirmPassword", password);
            if (password == "Notickbox1*")
            {
               
            }
            else
            {
                Tickbox("IsConsentedToUsePersonalInformation").Click();
                Tickbox("IsConsentedUseEmailAndSms").Click();
            }
        
            Button("Create and continue").Click();
       




        }

        private void EnterRegDetails(string email, string password)
        {
            throw new NotImplementedException();
        }

        internal void LoginInWithUsers(Table table)
        {
            var credentials = table.CreateSet<Credentials>();
            foreach (var loginData in credentials)
            {
                EnterUserLoginDetails(loginData.username, loginData.password);

                SubmitLogin();
                Thread.Sleep(2000);
                Button("Continue").Click();
            }
        }

        public void Open()
        {
            
            Driver.Navigate().GoToUrl(loginUrl);
            Wait.Until(x => EmailAddress.Displayed);
            VerifyLoginPageIsOpen();
        }

        internal void RegisterNewUser()
        {
            Hyperlink("Register as a new user").Click();
            //TextBox("Email", TestUser.Username);
            //TextBox("ConfirmEmail", TestUser.Username);
            //TextBox("Password", TestUser.Password);
            //TextBox("ConfirmPassword", TestUser.Password);
            //Tickbox("IsConsentedToUsePersonalInformation").Click();
            //Tickbox("IsConsentedUseEmailAndSms").Click();
            //Button("Create and continue").Click();
            //Open();
            //Hyperlink("Register as a new user").Click();
            //TextBox("Email", TestUser.Username);
            //TextBox("ConfirmEmail", TestUser.Username);
            //TextBox("Password", TestUser.Password);
            //TextBox("ConfirmPassword", TestUser.Password);
            //Tickbox("IsConsentedToUsePersonalInformation").Click();
            //Tickbox("IsConsentedUseEmailAndSms").Click();
            //Button("Create and continue").Click();



        }

        private void EnterUserLoginDetails(string email, string password)
        {
            EmailAddress.ClearAndEnter(email);
            Password.ClearAndEnter(password);
        }


        private void SubmitLogin()
        {
            Comp.Button("Sign in").Click();
        }

        public void LoginUser(string emailAddress, string password)
        {
            Open();
            EnterUserLoginDetails(emailAddress, password);
            SubmitLogin();
            VerifyPageIsDirectedToHomepage();
            VerifyApplicationIsAccessible();
        }

        public void LoginWithInvalidDetails()
        {
            EnterUserLoginDetails(TestUser.Username, TestUser.IncorrectPassword);
            SubmitLogin();
        }

        public void LoginTestUser()
        {
            EnterUserLoginDetails(AdminUser.Username, AdminUser.Password);
         
            SubmitLogin();
            Thread.Sleep(2000);
            Button("Continue").Click();
        }

  

        internal void VerifyIncompleteLoginEntryValidation(string email, string password, string fieldValidation, string validationSummary)
        {
            
            if(string.IsNullOrEmpty(email))
               CheckText(fieldValidation);
            if (string.IsNullOrEmpty(password))
                fieldValidation.Should().Contain(FieldValidation("Password").Text);
            CheckValidationMessage(fieldValidation);
        }

        internal void VerifyApplicationIsAccessible()
        {
            CheckText("Application overview");
            ClickLink("your-details-and-eligibility").Click();
         
        }

   

        internal void VerifyLoginPageIsOpen()
        {
            Comp.Button("Sign in").Displayed.Should().BeTrue();
        }

        internal void VerifyInvalidLoginValidation()
        {
            CheckValidationMessage(invalidLoginValidation);
        }



        internal void EnterLoginValuesAndSubmit(string username, string password)
        {
            EnterUserLoginDetails(username, password);
            SubmitLogin();
        }

        public void LoginAdminUser()
        {
            Open();
            EnterUserLoginDetails(AdminUser.Username, AdminUser.Password);
            SubmitLogin();
        }


        internal void EligibilityCheckerLaunch()
        {
            Driver.Navigate().GoToUrl("https://inzfs-lab.london.cloudapps.digital/eligibilityInfo/Eligibility-Start");
        }

        internal void LoginNewUser()
        {
            if (!Driver.Url.Contains(loginUrl))
                Open();
            EnterLoginValuesAndSubmit(context.Get<string>("TestApplicant"), NewRegUser.Password);
        }


        internal void EligibilityCheckerCompleteYes()
        {
            Thread.Sleep(1000);
            Hyperlink("Check you are eligible").Click();
            //AnalyzePage(Driver, "EligibilityChecker");
            Button("Accept analytics cookies").Click();
            Hyperlink("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkNo").Click();
            Button("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();

        }

        internal void EligibilityCheckerCompleteNo()
        {
            Hyperlink("Check you are eligible").Click();
            Button("Accept analytics cookies").Click();
            Hyperlink("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkNo").Click();
            Button("Continue").Click();
            RadioButton("checkYes").Click();
            Button("Continue").Click();
            RadioButton("checkNo").Click();
            Button("Continue").Click();

        }
    }

}
