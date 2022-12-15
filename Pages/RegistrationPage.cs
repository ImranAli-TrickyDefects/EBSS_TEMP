using EBSS.Automated.UI.Tests.TestData;
using OpenQA.Selenium;
using System;
using EBSS.UI.Tests.Support.Common;
using FluentAssertions;
using TechTalk.SpecFlow;
using EBSS.Automated.UI.Tests.Utils;
using EBSS.Automated.UI.Tests.Pages.CMS;

namespace EBSS.Automated.UI.Tests.Pages
{
    public class RegistrationPage : BasePage
    {
        private readonly string registerUrl;

        private readonly ScenarioContext context;
        public RegistrationPage(ScenarioContext context) : base(context)
        {
            registerUrl = BaseUrl + "/Registration/Register";
            this.context = context;
        }

     
        private readonly string emailValidation = "The Email field is required.";
        private readonly string passwordValidation = "The Password field is required.";
        private readonly string consentSMS = "Please consent to BEIS contacting you by email and SMS.";
        private readonly string consentPersonalInfo = "Please consent to BEIS processing your personal information.";
        private readonly string mismatchPasswordValidation = "'ConfirmPassword' and 'Password' do not match.";

        private IWebElement Email => Comp.TextInput("Email");

        private IWebElement ConfirmEmail => Comp.TextInput("ConfirmEmail");
        private IWebElement Password => Comp.TextInput("Password");
        private IWebElement ConfirmPassword => Comp.TextInput("ConfirmPassword");

        private IWebElement ConsentSMS => Comp.ConsentBox("IsConsentedUseEmailAndSms");
        private IWebElement ConsentPersonalInfo => Comp.ConsentBox("IsConsentedToUsePersonalInformation");
        public void Open()
        {
            Driver.Navigate().GoToUrl(registerUrl);
        }

        public void SubmitRegistration()
        {
            Comp.Button("Create and continue").Click();
        }

        public void EnterValidRegistrationDetails()
        {
            Email.ClearAndEnter(NewRegUser.Username);
            ConfirmEmail.ClearAndEnter(NewRegUser.Username);
            Password.ClearAndEnter(NewRegUser.Password);
            ConfirmPassword.ClearAndEnter(NewRegUser.Password);
            ConsentPersonalInfo.Click();
            ConsentSMS.Click();
        }

        internal void EnterExistingUserRegistrationDetails()
        {
            Email.SendKeys(TestUser.Username);
            Password.SendKeys(NewRegUser.Password);
            ConfirmPassword.SendKeys(NewRegUser.Password);
        }

        public void VerifyTestUserIsCreated()
        {
            //context.Get<UsersPage>().VerifyTestUserIsPresent();
        }

        internal void VerifyFieldAndSummaryValidations(string field, string fieldValidation, string summaryValidation)
        {
            ValidationSummary.Displayed.Should().BeTrue();
            ValidationSummary.Text.Replace(Environment.NewLine, string.Empty).Should().Contain(summaryValidation.Trim());
            FieldValidation(field).Text.Should().Contain(fieldValidation);
        }

        internal void VerifyFieldValidations()
        {
            ValidationSummary.Displayed.Should().BeTrue();
            ValidationSummary.Text.Should().Contain(emailValidation);
            ValidationSummary.Text.Should().Contain(passwordValidation);
            ValidationSummary.Text.Should().Contain(consentSMS);
            ValidationSummary.Text.Should().Contain(consentPersonalInfo);
            FieldValidation("Email").Text.Should().Be(emailValidation);
            FieldValidation("Password").Text.Should().Be(passwordValidation);
            FieldValidation("IsConsentedToUsePersonalInformation").Text.Should().Be(consentPersonalInfo);
            FieldValidation("IsConsentedUseEmailAndSms").Text.Should().Be(consentSMS);
        }

        internal void EnterValidButDifferentPassword()
        {
            Password.ClearAndEnter(NewRegUser.Password);
            ConfirmPassword.ClearAndEnter("MismatchPass123");
            ConsentPersonalInfo.Click();
            ConsentSMS.Click();
        }

        internal void VerifyPasswordMismatchValidation()
        {
            ValidationSummary.Displayed.Should().BeTrue();
            ValidationSummary.Text.Should().Contain(mismatchPasswordValidation);
            FieldValidation("ConfirmPassword").Text.Should().Be(mismatchPasswordValidation);
        }

        public void EnterRegistrationValueInTheField(string value, string field)
        {
            if (field == "Password")
            {
                Password.ClearAndEnter(value);
                ConfirmPassword.ClearAndEnter(value);
                ConsentPersonalInfo.Click();
                ConsentSMS.Click();
            }
            if (field == "Email")
            {
                Email.ClearAndEnter(value);
                ConfirmEmail.ClearAndEnter(value);
            }
            else
            {
                Comp.TextInput(field).ClearAndEnter(value);
            }
        }

        internal void RegisterNewTestApplicant()
        {
            if (!Driver.Url.Contains(registerUrl))
                Open();
            string timeStamp = DateTime.Now.ToString("ddMMHHmmss");
            Email.ClearAndEnter($"{NewRegUser.Username + timeStamp}@Test.com");
            Password.ClearAndEnter(NewRegUser.Password);
            ConfirmPassword.ClearAndEnter(NewRegUser.Password);
            SubmitRegistration();
            context.Add("TestApplicant", NewRegUser.Username + timeStamp);
        }
    }
}
