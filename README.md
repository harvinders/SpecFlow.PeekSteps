# SpecFlow.PeekSteps
A plugin to peek at the SpecFlow steps

# Installation 
TODO nuget

# Usage
The plugin works by adding extention methods to [ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) and `ScenarioStepContext`

Context Extension method | pur
--- | --- | ---
[ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetAllSteps() | Get all steps information  
[ScenarioStepContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetCurrentStep() | Get details about the currently executing step  
 | GetPreviousStep() | Get details about the previously executed step  
 | GetNextStep() | Get details about the next step to be executed
 
 ## StepDefinition
The step information is presented as `StepDefinition` instead of `StepInfo` object. However, the information present in `StepDefinition` is same as provided by `StepInfo`. 

# Limitation
The steps that are invoked from within step bindings are not visible. These invoked steps would have access to Previous and Next steps information, but most likely it would be incorrect. 

# Build status

[![Build status](https://ci.appveyor.com/api/projects/status/lp1hh0ylv0j567nl?svg=true)](https://ci.appveyor.com/project/harvinders/specflow-peeksteps)
