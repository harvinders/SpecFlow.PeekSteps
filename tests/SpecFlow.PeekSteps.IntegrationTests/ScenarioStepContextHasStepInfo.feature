Feature: ScenarioContext has steps information in BeforeStep hook
	In order to hook into interesting events
	As a SpecFlow statement writer
	I want to be have access to steps in BeforeStep hook


Scenario: Scenario step context is filled current, previous and next steps
	Given I have a simple given statement
	And I have a another simple given statement
	When I successfully enquire about the current statement
	And I successfully enquire about the next statement
	And I successfully enquire about the previous statement
	Then I have a simple then statement
