Feature: Maintain Customers
	Create and maintain customers.
	New customers are created with manual invoicing by default.

@WebTest
Scenario: Create new customer
	Given there are no customers named 'John Smith'

	When the user creates a new customer with the following details
	| Title | Name       | Address Line 1 | Address Line 2 | Address Line 3 | Postcode | Home Phone   | Mobile       |
	| Mr    | John Smith | 1 High Street  | Oakhampton     | Wessex         | WX1 5QT  | 01234 100200 | 07777 987654 |

	Then the customer is added to the system
	And the customer is marked as manually invoiced
