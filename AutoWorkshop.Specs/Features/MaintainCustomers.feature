@MaintainCustomers
Feature: Maintain Customers
	Create and maintain customers.
	New customers are created with manual invoicing by default.

@WebTest
Scenario: Create new customer
	Given there are no customers named 'Jane Jones'

	When I create a new customer with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |

	Then the customer is added to the system with the details provided
	And the customer is marked as manually invoiced

@WebTest
Scenario: View customer details
	Given the following existing customer
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	
	When I view the customer

	Then I should see the stored customer details
	And I should see the following toolbar options
	| Option                                  |
	| Maintain account for Jane Jones         |
	| Add a new car for Jane Jones            |
	| Add a new parts-only job for Jane Jones |
	| Add a new quote for Jane Jones          |

@WebTest
Scenario: Update existing customer
	Given the following existing customer
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	
	When I update the customer with a new mobile number of '07777 789456'

	Then the stored customer should be updated with mobile '07777 789456'

@WebTest
Scenario: Search for customer as you type
	Given the following existing customer
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |

	When I search for 'Jones'

	Then I should see the customer in the list of as-you-type results