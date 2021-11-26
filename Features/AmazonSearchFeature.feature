Feature: AmazonSearchFeature
	In order to test the Amazon Search functionality
	As a Tester
	I wanto to ensure that the functionality is working properly

@mytag
Scenario: Youtube should search for the given keyword and should navigate to the search results page
	Given I have navigated to Amazon website
	And I have accepted the Cookie Policy
	And I have entered Harry Potter and the Cursed Child as search keyword
	When I press the search button
	Then I should be redirected to the search results page