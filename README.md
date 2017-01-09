# SpecFlow.PeekSteps
A plugin to peek at the SpecFlow steps

# Installation 
TODO nuget

# Usage
The plugin works by adding extention methods to [ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) and `ScenarioStepContext`

Context Extension method | pur
--- | --- | ---
[ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetAllSteps() | Get all steps information  
[ScenarioStepContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetCurrentStep() | Get all steps information  
 | GetPreviousStep() | Get all steps information  
 | GetNextStep() | Get all steps information  

# Limitation
# Build status

[![Build status](https://ci.appveyor.com/api/projects/status/lp1hh0ylv0j567nl?svg=true)](https://ci.appveyor.com/project/harvinders/specflow-peeksteps)
