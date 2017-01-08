using System.Collections.Generic;
using SpecFlow.PeekSteps;

namespace TechTalk.SpecFlow
{
    public static class ScenarioContextExtentions
    {
        public static IEnumerable<StepDefinition> AllSteps(this ScenarioContext context)
        {
            return ExecutionContext.Steps;
        }

        public static StepDefinition CurrentStep(this ScenarioContext context)
        {
            return ExecutionContext.CurrentStep;
        }

        public static StepDefinition NextStep(this ScenarioContext context)
        {
            return ExecutionContext.NextStep;
        }

        public static StepDefinition PreviousStep(this ScenarioContext context)
        {
            return ExecutionContext.PreviousStep;
        }
    }
}