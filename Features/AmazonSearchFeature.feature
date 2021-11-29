Feature: AmazonSearchFeature
	In order to test the Amazon Search and Add to Cart functionality
	As a Tester
	I want0 to ensure that the functionalities are working properly

@mytag
Scenario: Search for a book in Amazon and add it to Cart
	Given I have navigated to Amazon website
	And I have accepted the Cookie Policy
	When I have navigated to the Books section of the website
	And I have entered Harry Potter and the Cursed Child as search keyword
	And I press the search button
	Then I should be redirected to the search results page
	And I verify that the first item has the title: Harry Potter and the Cursed Child - Parts One & Two
	And I verify if it has a badge
	And I verify the selected type
	And I verify the price is £4.00
	When I click the link to the book details
	Then I should be redirected to the book details page
	And I verify the title
	And I verify the badge if there is any
	And I verify the price on details page
	And I verify the type is Paperback
	When I click on the Add to Basket button
	Then I am redirected to the successfully added to cart screen
	And I verify that the notification for successfully adding the item is shown
	And I verify that the Added to Basket text is shown
	And I check the quantity of the cart is one
	When I click on Edit basket button
	Then I should be redirected to the cart screen
	And I verify that the book is shown on the list
	And I verify that the title, type of print and price are the same as on the search page, quantity is 1 and total price is same as item price





