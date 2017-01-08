using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SpecFlow.PeekSteps
{
    internal class ExecutionContext
    {
        static ExecutionContext()
        {
            Steps = new List<StepDefinition>();
        }

        internal static IList<StepDefinition> Steps { get; set; }
        internal static StepDefinition CurrentStep { get; set; }
        internal static StepDefinition NextStep { get; set; }
        internal static StepDefinition PreviousStep { get; set; }

        internal static StepDefinitionType CurrentDefinitionType => Steps.Last().Type;
    }
}