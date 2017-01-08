using SpecFlow.PeekSteps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(PeekStepsPlugin))]

namespace SpecFlow.PeekSteps
{
    public class PeekStepsPlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters)
        {
            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterTypeAs<PeekStepsTestRunner, ITestRunner>();
            };
        }
    }
}