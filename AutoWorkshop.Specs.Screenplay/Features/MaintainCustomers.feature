Feature: Maintain Customers
	Create, view and update customers.

@WebTest
Scenario: Create a new customer with default invoicing 
	Given there are no customers named 'Jane Jones'

	When I create a new customer with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Home Phone    | Mobile       |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 0121 756 2584 | 07575 456789 |
