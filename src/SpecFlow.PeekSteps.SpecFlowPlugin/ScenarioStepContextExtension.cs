using System.Threading;
using SpecFlow.PeekSteps;
using ExecutionContext = SpecFlow.PeekSteps.ExecutionContext;

namespace TechTalk.SpecFlow
{
    public static class ScenarioStepContextExtension
    {
        public static StepDefinition CurrentStep(this ScenarioStepContext context)
        {
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].CurrentStep;
        }

        public static StepDefinition NextStep(this ScenarioStepContext context)
        {
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].NextStep;
        }

        public static StepDefinition PreviousStep(this ScenarioStepContext context)
        {
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PreviousStep;
        }
    }
}