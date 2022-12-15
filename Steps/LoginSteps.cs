using EBSS.Automated.UI.Tests.Pages;
using EBSS.Automated.UI.Tests.Utils;
using TechTalk.SpecFlow;

namespace EBSS.Automated.UI.Tests.Steps
{
    [Binding]
    public sealed class LoginSteps
    {
        private readonly ScenarioContext scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.Set(new LoginPage(scenarioContext));
        }

        [Given(@"I am on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            scenarioContext.Get<LoginPage>().Open();
        }

        [Given(@"I am on the Business Proposal upload file page")]
        public void GivenIAmOnTheBusinessProposalUploadFilePage()
        {
            scenarioContext.Get<LoginPage>().OpenBusinessProposalUploadFilePage();
        }

        [Given(@"I am on the Finance Proposal upload file page")]
        public void GivenIAmOnTheFinanceProposalUploadFilePage()
        {
            scenarioContext.Get<LoginPage>().OpenFinanceProposalUploadFilePage();
        }

        [Given(@"I register with the following users")]
        public void GivenIRegisterWithTheFollowingUsers(Table table)
        {
            scenarioContext.Get<LoginPage>().RegisterNewUsers(table);
        }

        [Given(@"I submit valid login details with the following login details")]
        public void GivenISubmitValidLoginDetailsWithTheFollowingLoginDetails(Table table)
        {
            scenarioContext.Get<LoginPage>().LoginInWithUsers(table);
        }


        [When(@"I submit valid login details")]
        public void WhenTheUserSubmitsValidLoginDetails()
        {
        
            scenarioContext.Get<LoginPage>().LoginTestUser();
        }

        [When(@"I register for a new user")]
        public void WhenIRegisterANewUser()
        {
            scenarioContext.Get<LoginPage>().RegisterNewUser();
          
        }



        [When(@"I submit invalid login details")]
        public void WhenTheUserSubmitsInvalidLoginDetails()
        {
            scenarioContext.Get<LoginPage>().LoginWithInvalidDetails();
        }

        [When(@"I submit with the below combination of field values then I should see the FieldValidation and ValidationSummary accordingly")]
        public void WhenISubmitWithTheBelowCombinationOfFieldValuesThenIShouldSeeTheFieldValidationAndValidationSummaryAccordingly(Table table)
        {
            //scenarioContext.Get<Logger>().Log("Verifying field and summary level validations...");
            foreach (var row in table.Rows)
            {
    
                scenarioContext.Get<LoginPage>().EnterLoginValuesAndSubmit(row["EmailValue"], row["PasswordValue"]);
                scenarioContext.Get<LoginPage>().VerifyIncompleteLoginEntryValidation(row["EmailValue"], row["PasswordValue"], row["FieldValidation"], row["ValidationSummary"]);
            }
        }

        [Then(@"I should see relevant error message")]
        public void ThenIShouldSeeRelevantErrorMessage()
        {
            scenarioContext.Get<LoginPage>().VerifyInvalidLoginValidation();
        }

        [Then(@"I should remain on the Login page")]
        public void ThenIShouldRemainOnTheLoginPage()
        {
            scenarioContext.Get<LoginPage>().VerifyLoginPageIsOpen();
        }

        [Then(@"I am able to access application page")]
        public void ThenIAmAbleToAccessApplicationPage()
        {
            scenarioContext.Get<LoginPage>().VerifyApplicationIsAccessible();
        }

        [Given(@"I have logged in with the new user details")]
        public void GivenIHaveLoggedIn()
        {
            scenarioContext.Get<LoginPage>().LoginNewUser();
        }


        [Given(@"I am on the Eligibility Checker")]
        public void GivenIAmOnTheEligibilityChecker()
        {
            scenarioContext.Get<LoginPage>().EligibilityCheckerLaunch();
            


        }

        [When(@"I can successfully complete eligibility")]
        [Then(@"I can successfully complete eligibility")]
        public void ThenICanSuccessfullyCompleteEligibility()
        {
            scenarioContext.Get<LoginPage>().EligibilityCheckerCompleteYes();
       
        }

        [When(@"I unsuccessfully complete eligibility")]
        [Then(@"I unsuccessfully complete eligibility")]
        public void IUnsuccessfullyCompleteEligibility()
        {
            scenarioContext.Get<LoginPage>().EligibilityCheckerCompleteNo();
        }

    }


}
