Feature: ScenarioContext has steps information in BeforeScenario hook
	In order to hook into interesting events
	As a SpecFlow statement writer
	I want to be have access to steps in BeforeScenario hook

@testof-sceanrio-context
Scenario: Scenario context is filled with the below steps in OnBeforeScenario hook
	Given I have a simple given statement
	And I have a another simple given statement
	When I have a simple when statement
	Then I have a simple then statement
