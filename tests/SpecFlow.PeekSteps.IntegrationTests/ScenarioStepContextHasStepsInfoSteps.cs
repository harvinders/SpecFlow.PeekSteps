using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlow.PeekSteps.IntegrationTests
{
    [Binding]
    public class ScenarioContextHasStepsInformationInBeforeStepHookSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ScenarioContextHasStepsInformationInBeforeStepHookSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I successfully enquire about the current statement")]
        public void WhenISuccessfullyEnquireAboutTheCurrentStatement()
        {
            Assert.Equal("I successfully enquire about the current statement",
                this.scenarioContext.StepContext.CurrentStep().Text);
        }

        [When(@"I successfully enquire about the next statement")]
        public void WhenISuccessfullyEnquireAboutTheNextStatement()
        {
            Assert.Equal("I successfully enquire about the previous statement",
                this.scenarioContext.StepContext.NextStep().Text);
        }

        [When(@"I successfully enquire about the previous statement")]
        public void WhenISuccessfullyEnquireAboutThePreviousStatement()
        {
            Assert.Equal("I successfully enquire about the next statement",
                this.scenarioContext.StepContext.PreviousStep().Text);
        }
    }
}