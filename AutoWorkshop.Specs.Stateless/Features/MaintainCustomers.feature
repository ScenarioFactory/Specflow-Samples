@Stateless
Feature: Maintain Customers
	** These scenarios are written to use largely stateless step files to allow maximum step reusability by non-programming QAs. **

@WebTest
Scenario: Create new customer
	Given there are no customers named 'Jane Jones'

	When I create a new customer with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |

	Then a customer is present in the system with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	And customer 'Jane Jones' is marked as manually invoiced

@WebTest
Scenario: View customer details
	Given there are no customers named 'Jane Jones'
	And the following customer is present in the system
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	
	When I view customer 'Jane Jones'

	Then I should see the following customer details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	And I should see the following toolbar options
	| Option                                  |
	| Maintain account for Jane Jones         |
	| Add a new car for Jane Jones            |
	| Add a new parts-only job for Jane Jones |
	| Add a new quote for Jane Jones          |

@WebTest
Scenario: Update existing customer
	Given there are no customers named 'Jane Jones'
	And the following customer is present in the system
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
	
	When I update customer 'Jane Jones' with a new mobile number of '07777 789456'

	Then customer 'Jane Jones' should be have a mobile number of '07777 789456'

@WebTest
Scenario: Search for customer as you type
	Given there are no customers named 'Jane Jones'
	And the following customer is present in the system
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |

	When I search for 'Jones'

	Then I should see 'Jane Jones' in the list of as-you-type results