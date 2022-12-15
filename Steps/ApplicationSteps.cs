using EBSS.Automated.UI.Tests.Pages;
using TechTalk.SpecFlow;
using static EBSS.Automated.UI.Tests.TestData.ApplicationData;

namespace EBSS.Automated.UI.Tests.Steps
{
    [Binding]
    public sealed class ApplicationSteps
    {

        private readonly ScenarioContext scenarioContext;

        public ApplicationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.Set(new BasicInformationInputPage(scenarioContext));
            this.scenarioContext.Set(new EBSSPage(scenarioContext));
        }

        [Given(@"I find address")]
        public void GivenIFindAddress()
        {
            scenarioContext.Get<EBSSPage>().FindAddress();
        }

        [Given(@"I find address via postcode")]
        public void GivenIFindAddressViaPostcode()
        {
            scenarioContext.Get<EBSSPage>().FindAddressPostcode();
        }

        [Given(@"I confirm this is my address")]
        public void GivenIConfirmThisIsMyAddress()
        {
            scenarioContext.Get<EBSSPage>().ConfirmAddress();
        }

        [Given(@"this is my main home")]
        public void GivenThisIsMyMainHome()
        {
            scenarioContext.Get<EBSSPage>().IsThisYourMainHomeYes();
        }


        [When(@"I don't receive EBSS discounts already")]
        public void WhenIDontReceiveEBSSDiscountsAlready()
        {
            scenarioContext.Get<EBSSPage>().ReceiveEBSSAlreadyNo();
        }

        [Then(@"I am not eligible for EBSS discounts")]
        public void ThenIAmNotEligibleForEBSSDiscounts()
        {
            scenarioContext.Get<EBSSPage>().EligibleNo();
        }

        [When(@"I enter my eligibility details")]
        public void WhenIEnterMyEligibilityDetails()
        {
            scenarioContext.Get<EBSSPage>().EnterValidEligibilityDetails();
        }

     

        [When(@"I receive EBSS discounts already")]
        public void WhenIReceiveEBSSDiscountsAlready()
        {
            scenarioContext.Get<EBSSPage>().ReceiveEBSSAlreadyYes();
        }

        [Then(@"I am eligible for EBSS discounts")]
        public void ThenIAmEligibleForEBSSDiscounts()
        {
            scenarioContext.Get<EBSSPage>().EligibleYes();
        }


        [Given(@"this is not my main home")]
        public void GivenThisIsNotMyMainHome()
        {
            scenarioContext.Get<EBSSPage>().IsThisYourMainHomeNo();
        }

        [When(@"I navigate back to the application summary page")]
        [Given(@"I navigate back to the application summary page")]
        [Given(@"I am on the Application Summary page")]
        public void GivenIAmOnTheApplicationSummarypage()
        {
            scenarioContext.Get<EBSSPage>().Open();
        }

        [Given(@"I enter all the basic information details and save")]
        [When(@"I enter all the basic information details and save")]
        public void WhenIEnterAllTheBasicInformationDetails()
        {
            scenarioContext.Get<BasicInformationInputPage>().EnterValidBasicInformationInputs();
        }

        [Given(@"I am on the first basic info section page")]
        public void GivenIAmOnTheFirstBasicInfoSectionPage()
        {
            scenarioContext.Get<EBSSPage>().ClickToOpenSection("Company Details");
        }

  

        [When(@"I update basic info details on each section")]
        public void WhenIClickChangeAndUpdateEachOfBasicInformationInput()
        {
            scenarioContext.Get<BasicInformationInputPage>().UpdateBasicInformationInputs();
        }

        [Then(@"upon visting each basic information section I should be able to see that the details are updated")]
        public void ThenOnTheSummaryPageTheUpdatedValuesAreShownAsExpected()
        {
            scenarioContext.Get<EBSSPage>().VerifyUpdatedDetailsInEachBasicInfoSection();
        }

        [When(@"I click continue without filling any details on the Company details page")]
        [Then(@"I click continue without slecting any of the funding options")]
        [Then(@"I click continue without filling any details on the Project Summary page")]
        [Then(@"I click continue without filling any details on the Project Details page")]
        public void WhenIClickContinueWithoutFillingAnyDetailsOnTheProjectDetailsPage()
        {
            scenarioContext.Get<BasicInformationInputPage>().ContinueToNextPage();
        }

        [Then(@"I should see the validations ""(.*)""")]
        public void ThenIShouldSeeTheValidations(string validations)
        {
            scenarioContext.Get<BasicInformationInputPage>().VerifyValidationSummaryMatches(validations);
        }

        [Then(@"I enter valid company details and continue")]
        public void ThenIEnterValidCompanyDetailsAndContinue()
        {
            scenarioContext.Get<BasicInformationInputPage>()
                .EnterCompanyDetails(BasicInfo.CompanyName, BasicInfo.CompanyNumber, "Entering");
            scenarioContext.Get<BasicInformationInputPage>().ContinueToNextPage();
        }


        [Then(@"I enter valid project details and continue")]
        public void ThenIEnterValidProjectDetailsAndContinue()
        {
            scenarioContext.Get<BasicInformationInputPage>()
                .EnterProjectDetails(BasicInfo.BriefProjectDescription, BasicInfo.ProjectCostEndBy2024, "Entering");
            scenarioContext.Get<BasicInformationInputPage>().ContinueToNextPage();
        }

        [Then(@"I enter valid project introduction input and continue")]
        public void ThenIEnterValidProjectIntorductionInputAndContinue()
        {
            scenarioContext.Get<BasicInformationInputPage>()
                .EnterProjectSummary(BasicInfo.ProjectName, BasicInfo.TurnoverDate, "Entering");
            scenarioContext.Get<BasicInformationInputPage>().ContinueToNextPage();
        }

        

        [Given(@"I am on upload project plan page")]
        public void GivenIAmOnUploadProjectPlanPage()
        {
            scenarioContext.Get<BasicInformationInputPage>().OpenUploadProjectPlanPage();
        }

        [When(@"I upload a document")]
        public void WhenIUploadADocument()
        {
            scenarioContext.Get<BasicInformationInputPage>().UploadTestDocument();
        }

        [Then(@"I should see that the document is uploaded")]
        public void ThenIShouldSeeThatTheDocumentIsUploaded()
        {
            scenarioContext.Get<BasicInformationInputPage>().VerifyUploadedDocument();
        }

        [When(@"I click on the download document link")]
        public void WhenIClickOnTheDownloadDocumentLink()
        {
            scenarioContext.Get<BasicInformationInputPage>().DownloadTheTestDocument();
        }

        [Then(@"I should see that the document is downloaded")]
        public void ThenIShouldSeeThatTheDocumentIsDownloaded()
        {
            scenarioContext.Get<BasicInformationInputPage>().VerifyTestDocumentIsDownload();
        }

        [Then(@"I should see status next to each of the basic information section link as per below")]
        public void ThenIShouldSeeStatusNextToEachOfTheBasicInformationSectionLink(Table table)
        {
            foreach (var row in table.Rows)
            {
                scenarioContext.Get<EBSSPage>()
                    .VerifySectionCompletionStatus(row["Section"], row["Status"]);
            }            
        }

        [Then(@"upon visting each basic information section I should be able to see that the details are populated")]
        public void ThenUponVistingEachBasicInformationSectionWeShouldBeAbleToSeeThatDeatilsArePopulated()
        {
            scenarioContext.Get<EBSSPage>().VerifyDetailsEnteredInEachBasicInfoSection();
        }

        [When(@"complete Business Proposal")]
        public void WhenCompleteBusinessProposal()
        {
            scenarioContext.Get<EBSSPage>().CompleteBusinessProposal();

         }

        [When(@"attempt to upload the following files")]
        public void WhenAttemptToUploadTheFollowingFiles(Table table)
        {
            scenarioContext.Get<EBSSPage>().UploadFiles(table);

        }

        [Then(@"files are not uploaded")]
        public void ThenFilesAreNotUploaded()
        {
            scenarioContext.Get<EBSSPage>().CheckFilesNotUploaded();
        }

        [Then(@"files are uploaded successfully")]
        public void ThenFilesAreUploadedSuccessfully()
        {
            scenarioContext.Get<EBSSPage>().CheckFilesUploaded();          
        }


        [When(@"complete Performance Data")]
        public void WhenCompletePerformanceData()
        {
            scenarioContext.Get<EBSSPage>().CompletePerformanceData();

        }

        //[Then(@"Performance Data is completed")]
        //public void ThenPerformanceDataIsCompleted()
        //{
        //    scenarioContext.Get<ApplicationSummaryPage>().CheckApplicationOverview();
        //}


        [When(@"complete Finance Proposal Summary")]
        public void WhenCompleteFinanceProposalSummary()
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceProposal();
        }

        [When(@"complete Finance Proposal Summary aid for start ups 1")]
        public void WhenCompleteFinanceProposalSummaryAidForStartUpsOne()
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceProposalFlowOne();
        }

        [Given(@"I am on the equality data survey")]
        public void GivenIAmOnTheEqualityDataSurvey()
        {
            scenarioContext.Get<EBSSPage>().GoToEqualityDataSurvey();
        }


        [When(@"complete Finance Proposal Summary aid for Feasibility Study")]
        public void WhenCompleteFinanceProposalSummaryAidForFeasibilityStudy()
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceFeasibilityStudy();
        }

        [When(@"complete Finance Proposal Summary aid for Industrial Research")]
        public void WhenCompleteFinanceProposalSummaryAidForIndustrialResearch()
        {
            scenarioContext.Get<EBSSPage>().CompleteIndustrialResearch();
        }

        [When(@"complete Finance Proposal Summary aid for Experimental development")]
        public void WhenCompleteFinanceProposalSummaryAidForExperimentalDevelopment()
        {
            scenarioContext.Get<EBSSPage>().CompleteExperimentalDevelopment();
        }


        [When(@"complete Finance Proposal Summary aid for research and development")]
        public void WhenCompleteFinanceProposalSummaryAidForResearchAndDevelopment()
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceProposalSummaryAidForResearchAndDevelopment();
        }

        [When(@"complete Finance Proposal Summary aid for start ups (.*) without mandatory fields")]
        public void WhenCompleteFinanceProposalSummaryAidForStartUpsWithoutMandatoryFields(int p0)
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceProposalSummaryAidForResearchAndDevelopmentWithoutMandatoryFields();
        }


        [When(@"complete Finance Proposal Summary aid for start ups 2")]
        public void WhenCompleteFinanceProposalSummaryAidForStartUpsTwo()
        {
            scenarioContext.Get<EBSSPage>().CompleteFinanceProposalFlowTwo();
        }

        [Then(@"Finance Proposal Summary is completed")]
        public void ThenFinanceProposalSummaryIsCompleted()
        {
           
        }

        [When(@"complete Proposal Overview without mandatory fields")]
        public void WhenCompleteProposalOverviewWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompleteProposalOverviewWIthoutMandatory();
        }

        [When(@"complete Consortium")]
        public void WhenCompleteConsortium()
        {
            scenarioContext.Get<EBSSPage>().CompleteConsortium();
        }

        [When(@"complete Consortium with uploaded file")]
        public void WhenCompleteConsortiumWithUploadedFile()
        {
            scenarioContext.Get<EBSSPage>().CompleteConsortiumUploadFile();
        }


        [Then(@"validation messages are displayed")]
        public void ThenValidationMessagesAreDisplayed()
        {

        }

        [When(@"complete Business Proposal without mandatory fields")]
        public void WhenCompleteBusinessProposalWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompleteBusinessProposalWithoutMandatoryFields();
        }


        [When(@"complete Proposal Overview")]
        public void WhenCompleteProposalOverview()
        {
            scenarioContext.Get<EBSSPage>().CompleteProposalOverview();
        }

        [Then(@"Proposal Overview is completed")]
        public void ThenProposalOverviewIsCompleted()
        {
            scenarioContext.Get<EBSSPage>().CheckApplicationOverview();
        }

        [Then(@"Business Proposal is completed")]
        public void ThenBusinessProposalIsCompleted()
        {
            scenarioContext.Get<EBSSPage>().CheckApplicationOverview();
        }

        [When(@"complete Equality Data Questionnaire for white male")]
        public void WhenCompleteEqualityDataQuestionnaireForWhiteMale()
        {
            scenarioContext.Get<EBSSPage>().CompleteEqualityDataWhiteMale();
        }

        [When(@"complete Equality Data Questionnaire for asian female")]
        public void WhenCompleteEqualityDataQuestionnaireForAsianFemale()
        {
            scenarioContext.Get<EBSSPage>().CompleteEqualityDataAsianFemale();
        }

        [When(@"I complete application")]
        public void WhenICompleteApplication()
        {
            scenarioContext.Get<EBSSPage>().SubmitApplication();
        }

        [When(@"complete Performance Data without mandatory fields")]
        public void WhenCompletePerformanceDataWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompletePerformanceDataWithoutMandatoryFields();
        }



        [Then(@"I can return to application overview")]
        public void ThenICanReturnToApplicationOverview()
        {
            scenarioContext.Get<EBSSPage>().ReturnApplicationOverview();
        }

        [When(@"complete Equality Data Questionnaire without mandatory fields")]
        public void WhenCompleteEqualityDataQuestionnaireWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompleteEqualityWithoutMandatoryFields();
        }

        [When(@"complete Your details and eligibility")]
        public void WhenCompleteYourDetailsAndEligibility()
        {
            scenarioContext.Get<EBSSPage>().CompleteYourDetailsAndEligibility();
        }

        [When(@"complete Your details and eligibility without mandatory fields")]
        public void WhenCompleteYourDetailsAndEligibilityWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompleteDetailsEligibilityWithoutMandatory();
        }

        [When(@"complete Summary of finances without mandatory information")]
        public void WhenCompleteSummaryOfFinancesWithoutMandatoryInformation()
        {
            scenarioContext.Get<EBSSPage>().CompleteSummaryOfFinancesWithoutMandatory();
        }



        [Then(@"Your details and eligibility is completed")]
        public void ThenYourDetailsAndEligibilityIsCompleted()
        {
            scenarioContext.Get<EBSSPage>().CheckApplicationOverview();
        }

        [When(@"complete Summary of finances")]
        public void WhenCompleteSummaryOfFinances()
        {
            scenarioContext.Get<EBSSPage>().CompleteSummaryOfFinances();
        }

        [Then(@"Summary of finances is completed")]
        public void ThenSummaryOfFinancesIsCompleted()
        {
            scenarioContext.Get<EBSSPage>().CheckApplicationOverview();
        }

        [When(@"complete Your Organisation Consortium YES")]
        public void WhenCompleteYourOrganisationConsortiumYES()
        {
            scenarioContext.Get<EBSSPage>().CompleteYourOrganisationConsortiumYES();
        }

        [When(@"complete Your Organisation Consortium NO")]
        public void WhenCompleteYourOrganisationConsortiumNO()
        {
            scenarioContext.Get<EBSSPage>().CompleteYourOrganisationConsortiumNO();
        }

        [When(@"complete Your Organisation without mandatory fields")]
        public void WhenCompleteYourOrganisationWithoutMandatoryFields()
        {
            scenarioContext.Get<EBSSPage>().CompleteYourOrganisationWithoutMandatoryFields();
        }

        [Then(@"Your Organisation is completed")]
        public void ThenYourOrganisationIsCompleted()
        {
            scenarioContext.Get<EBSSPage>().CheckApplicationOverview();
        }

        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            scenarioContext.Get<EBSSPage>().GoToHomePage();
        }

        [Given(@"it is my own home")]
        public void GivenItIsMyOwnHome()
        {
            scenarioContext.Get<EBSSPage>().OwnHome();
        }

        [Given(@"it is rented home")]
        public void GivenItIsRentedHome()
        {
            scenarioContext.Get<EBSSPage>().RentedHome();
        }

        [Given(@"it is a residential park home")]
        public void GivenItIsResidentialParkHome()
        {
            scenarioContext.Get<EBSSPage>().ResidentialParkHome();
        }



        [Then(@"I am eligible")]
        public void ThenIAmEligible()
        {
            scenarioContext.Get<EBSSPage>().ConfirmEligibilityYes();
        }

        [Then(@"I am not eligible")]
        public void ThenIAmNotEligible()
        {
            scenarioContext.Get<EBSSPage>().ConfirmEligibilityNo();
        }

        [When(@"download documents")]
        public void WhenDownloadDocuments()
        {
            scenarioContext.Get<EBSSPage>().DownloadDocuments();
        
        }

        [Then(@"there are no errors")]
        public void ThenThereAreNoErrors()
        {
            //scenarioContext.Get<ApplicationSummaryPage>().GoToApplicationOverview();
        }

    }
}
