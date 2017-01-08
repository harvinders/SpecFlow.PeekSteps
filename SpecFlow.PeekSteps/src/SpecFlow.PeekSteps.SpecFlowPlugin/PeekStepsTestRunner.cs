using System;
using System.Collections.Generic;
using BoDi;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlow.PeekSteps
{
    public class PeekStepsTestRunner : ITestRunner
    {
        private readonly ITestExecutionEngine normalExecutionEngine;
        private readonly ITestExecutionEngine nullExecutionEngine;
        private bool isPeekingEnabled = true;

        internal List<Action<ITestExecutionEngine>> StepsToReplay = new List<Action<ITestExecutionEngine>>();

        public int ThreadId { get; private set; }

        private class NullBindingInvoker : IBindingInvoker
        {
            public object InvokeBinding(IBinding binding, IContextManager contextManager, object[] arguments, ITestTracer testTracer,
                out TimeSpan duration)
            {
                duration = TimeSpan.Zero;
                return null;
            }
        }

        public PeekStepsTestRunner(ITestExecutionEngine executionEngine, IObjectContainer container)
        {
            this.normalExecutionEngine = executionEngine;
            var nullContainer = new ObjectContainer(container);

            nullContainer.RegisterInstanceAs((IBindingInvoker)new NullBindingInvoker());
            this.nullExecutionEngine = nullContainer.Resolve<ITestExecutionEngine>();
        }

        public void InitializeTestRunner(int threadId)
        {
            ThreadId = threadId;
        }

        public void OnTestRunStart()
        {
            ExecutionEngine.OnTestRunStart();
        }

        public void OnTestRunEnd()
        {
            ExecutionEngine.OnTestRunEnd();
        }

        public void OnFeatureStart(FeatureInfo featureInfo)
        {
            ExecutionEngine.OnFeatureStart(featureInfo);
        }

        public void OnFeatureEnd()
        {
            ExecutionEngine.OnFeatureEnd();
        }

        public void OnScenarioStart(ScenarioInfo scenarioInfo)
        {
            //if (scenarioInfo.Tags.Contains("enable-peeking"))
            //    this.isPeekingEnabled = true;

            ExecutionContext.Steps.Clear();
            this.StepsToReplay.Clear();

            if (this.isPeekingEnabled)
            {
                this.StepsToReplay.Add(e => e.OnScenarioStart(scenarioInfo));
            }
            else
            {
                ExecutionEngine.OnScenarioStart(scenarioInfo);
            }
        }

        public void CollectScenarioErrors()
        {
            if (this.isPeekingEnabled)
            {
                this.StepsToReplay.Add(e => e.OnAfterLastStep());
            }
            else
            {
                ExecutionEngine.OnAfterLastStep();
            }
        }

        public void OnScenarioEnd()
        {
            this.StepsToReplay.Add(e => e.OnScenarioEnd());

            //try
            //{
            if (this.isPeekingEnabled)
            {
                int i = 0;
                foreach (var action in this.StepsToReplay)
                {
                    if (0 != i && i <= ExecutionContext.Steps.Count)
                    {
                        ExecutionContext.CurrentStep = ExecutionContext.Steps[i - 1];
                        ExecutionContext.PreviousStep = i <= 1 ? null : ExecutionContext.Steps[i - 2];
                        ExecutionContext.NextStep = i >= ExecutionContext.Steps.Count ? null : ExecutionContext.Steps[i];
                    }
                    action(ExecutionEngine);
                    i++;
                }
            }
            //}
            //finally
            //{
            //    //this.isPeekingEnabled = false;
            //}
        }

        public void Given(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.isPeekingEnabled)
            {
                ExecutionContext.Steps.Add(new StepDefinition(StepDefinitionType.Given, text, tableArg, multilineTextArg));
                this.StepsToReplay.Add(
                    e => e.Step(StepDefinitionKeyword.Given, keyword, text, multilineTextArg, tableArg));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.Given, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void When(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.isPeekingEnabled)
            {
                ExecutionContext.Steps.Add(new StepDefinition(StepDefinitionType.When, text, tableArg, multilineTextArg));
                this.StepsToReplay.Add(
                    e => e.Step(StepDefinitionKeyword.When, keyword, text, multilineTextArg, tableArg));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.When, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void Then(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.isPeekingEnabled)
            {
                ExecutionContext.Steps.Add(new StepDefinition(StepDefinitionType.Then, text, tableArg, multilineTextArg));
                this.StepsToReplay.Add(
                    e => e.Step(StepDefinitionKeyword.Then, keyword, text, multilineTextArg, tableArg));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.Then, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void And(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.isPeekingEnabled)
            {
                ExecutionContext.Steps.Add(new StepDefinition(ExecutionContext.CurrentDefinitionType, text, tableArg, multilineTextArg));
                this.StepsToReplay.Add(e => e.Step(StepDefinitionKeyword.And, keyword, text, multilineTextArg, tableArg));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.And, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void But(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.isPeekingEnabled)
            {
                ExecutionContext.Steps.Add(new StepDefinition(ExecutionContext.CurrentDefinitionType, text, tableArg, multilineTextArg));
                this.StepsToReplay.Add(e => e.Step(StepDefinitionKeyword.But, keyword, text, multilineTextArg, tableArg));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.But, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void Pending() => ExecutionEngine.Pending();

        public FeatureContext FeatureContext => ExecutionEngine.FeatureContext;

        public ScenarioContext ScenarioContext => ExecutionEngine.ScenarioContext;

        public ITestExecutionEngine ExecutionEngine => this.isPeekingEnabled ? this.nullExecutionEngine : this.normalExecutionEngine;
    }
}