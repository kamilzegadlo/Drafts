Feature: SearchPropertyAd
	In order to avoid silly mistakes
	I want to be sure that search functionality works

@Search
Scenario: GWD_SearchAdCheckIfResultIsEmpty
	Given I have typed test13 into the Title on Search Page
	And I have typed 1 into the Price From on Search Page
	And I have typed 2 into the Price To on Search Page
	When I click Search Button on Search Page
	Then the result should be empty

@Search
Scenario: GWD_SearchAdCheckIfManyResults
	Given I have typed test into the Title on Search Page
	And I have typed 1 into the Price From on Search Page
	And I have typed 999999 into the Price To on Search Page
	When I click Search Button on Search Page
	Then the result should should contain many hits

@Search
Scenario: GWD_SearchAdCheckIfResultIsOnlyOne
	Given I have typed test13 into the Title on Search Page
	And I have typed 1 into the Price From on Search Page
	And I have typed 999999 into the Price To on Search Page
	When I click Search Button on Search Page
	Then the result should should contain one hit



