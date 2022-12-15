using EBSS.Automated.UI.Tests.Utils;
using System;
using EBSS.UI.Tests.Support.Common;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;
using EBSS.Automated.UI.Tests.DTOs;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Selenium.Axe;
using System.Reflection;
using System.IO;

namespace EBSS.Automated.UI.Tests.Pages
{
    public abstract class BasePage 
    {
        public IWebDriver Driver;

        public Components Comp;

        public readonly string BaseUrl = ConfigurationSetUp.BaseUrl;
        protected WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
        IWebElement NewApplicationButton => Driver.FindElement(By.CssSelector("a.govuk-button.govuk-\\!-margin-top-6"), 10);

        private ScenarioContext context;
        protected BasePage(ScenarioContext context)
        {
            this.context = context;
            Driver = context.Get<IWebDriver>();
            Comp = new Components(Driver);
        }

        public void UploadItem(string fileName)
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var count = Driver.FindElements(By.CssSelector("input[type='file']")).ToList();
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            //string paths = @"C:\AutoTests\EBSS.Automated.UI.Tests\EBSS.Automated.UI.Tests\" + "TestData\\"+fileName;
            
            string paths = projectPath.ToString() + "bin\\Release\\net5.0\\TestData\\" + fileName;
            Console.WriteLine("This is the actual path " + actualPath.ToString());
            Console.WriteLine("This is the project path "+ projectPath.ToString());
            Console.WriteLine("This is the file upload path " + paths.ToString());
            //file must be made always copy in properties
            count.ElementAt(0).SendKeys(paths);
            Thread.Sleep(1000);
            Button("Upload").Click();

        }


        public static AxeResult AnalyzePage(IWebDriver driver, string PageName)

        {

            var result = new AxeBuilder(driver).WithTags("wcag21aa", "wcag2aa").Analyze();

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            outPutDirectory = outPutDirectory.Substring(0, outPutDirectory.IndexOf("bin"));

            outPutDirectory = outPutDirectory.Substring(outPutDirectory.IndexOf("\\") + 1);

            String path = Path.Combine(outPutDirectory, "Reports\\AccessibiltyReport\\" + PageName + "-AxeReport.html");

            driver.CreateAxeHtmlReport(result, path);

            return result;

        }

        public IWebElement Button(String buttonName)
        {
            return Driver.FindElement(By.XPath("//button[contains(text(),'"+buttonName+"')]"));
        }


        public void SelectDropDown(String dropdown, String dropDownOption)
        {
            IWebElement element = Driver.FindElement(By.Id(dropdown));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(dropDownOption);
        }

        public void ButtonId(String buttonName)
        {
            Driver.FindElement(By.Id(buttonName)).Click();
        }

        public IWebElement Accordian(String accordianName)
        {
            return Driver.FindElement(By.XPath("//button[contains(text(),'" + accordianName + "')]"));
        }

        public IWebElement BackButton()
        {
            return Driver.FindElement(By.Id("BackButton"));
        }

        public IWebElement Hyperlink(String hyperlinkName)
        {
            return Driver.FindElement(By.XPath("//a[contains(text(),'" + hyperlinkName + "')]"));
        }


        public IWebElement SaveContinue()
        {
            return Driver.FindElement(By.Id("SaveAndContinue"));
        }

        public IWebElement Tickbox(String tickboxName)
        {
            return Driver.FindElement(By.Id(tickboxName));
        }

        public void CheckValidationMessage(string message)
        {
             Assert.That(Driver.PageSource.Contains(message));
       
        }

        public void CheckText(string message)
        {
            Assert.That(Driver.PageSource.Contains(message));

        }

        public void CheckTextNotDisplayed(string message)
        {
            Assert.IsFalse(Driver.PageSource.Contains(message));

        }

        public IWebElement RadioButton(String radioButtonName)
        {
            return Driver.FindElement(By.Id(radioButtonName));
        }

        public IWebElement RadioValue(String value)
        {
            return Driver.FindElement(By.XPath("//input[contains(@value,'" + value + "')]"));
        }

        public IWebElement DropDown()
        {
            return Driver.FindElement(By.Id(""));
        }
        
        public void DateBox(String textboxName, String text)
        {
            Driver.FindElement(By.Id(textboxName)).Clear();
            Driver.FindElement(By.Id(textboxName)).SendKeys(text);
        }

        public void ClearTextBox(String textboxName)
        {
            Driver.FindElement(By.Id(textboxName)).Clear();
        }

        public void TextBox(String textboxName, String text)
        {
            Driver.FindElement(By.Id(textboxName)).Clear();
            Driver.FindElement(By.Id(textboxName)).SendKeys(text);
        }

        public void TextBoxCharCountdown(String text)
        {
            Driver.FindElement(By.Id("with-hint")).Clear();
            Driver.FindElement(By.Id("with-hint")).SendKeys(text);
        }

        public void moveToElement(string value)
        {
            Actions action = new Actions(Driver);
            var element = Driver.FindElement(By.Id(value));
            action.MoveToElement(element).Perform();
         }

        public IWebElement ClickLink(String sectionName)
        {
            return Driver.FindElement(By.Id(sectionName));
        }

        public IWebElement GetElementById(String Page, String FieldName)
        {
            Elements elements = context.Get<Elements>();
            var page = elements.application.sections.pages.Where(p => p.name.Equals(Page));
            var elementName = page.Where(p => p.fieldName.Equals(FieldName));
            String fieldName = elementName.ElementAt(0).fieldName;
     
            return Driver.FindElement(By.Id(fieldName));
        }

        public IWebElement GetElementByXPath(String Page, String FieldName)
        {
            Elements elements = context.Get<Elements>();
            var page = elements.application.sections.pages.Where(p => p.name.Equals(Page));
            var elementName = page.Where(p => p.fieldName.Equals(FieldName));
            String fieldName = elementName.ElementAt(0).fieldName;

            return Driver.FindElement(By.XPath("//*[contains(text(),'"+fieldName+"')]"));
        }

        public IWebElement DeleteThis()
        {
            return Driver.FindElement(By.XPath("//input[@value='0']"));
        }

     


        internal IWebElement ValidationSummary => Driver.FindElement(By.Id("validationSummary"));

        internal IWebElement FieldValidation(string field)
        {
            return Driver.FindElements(By.CssSelector("span.govuk-error-message.field-validation-error"))
                .First(x => x.GetAttribute("data-valmsg-for").ToLower() == field.ToLower());
        }

        public void WaitForPageToLoad()
        {
            Wait.Until(x => NewApplicationButton.Displayed);
        }        

        public void VerifyPageIsDirectedToHomepage()
        {
           // WaitForPageToLoad();
            CheckText("Apply for the Energy Entrepreneurs Fund: phase 9");
        }

        public void javaScript(string jScript)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("document.getElementById('SaveAndContinue').click();");

        }
    }
}
