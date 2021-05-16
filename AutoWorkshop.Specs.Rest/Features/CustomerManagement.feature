Feature: Manage Customers via REST API

@RestApi
Scenario: Create a new customer via REST
	Given there are no customers named 'Holly Henshaw'

	When I create a new customer resource with the following details via REST
	| Title | Name          | Address Line 1 | Address Line 2 | Address Line 3 | Postcode | Home Phone   | Mobile       | Account Invoicing |
	| Miss  | Holly Henshaw | 101 Highfields | Duston         | Northampton    | NN5 6HH  | 01604 789456 | 07777 987654 | Yes               |

	Then I should receive an HTTP 201 Created response
	And I should receive the location of the created resource
	And the customer should be added to the system with the details provided

Scenario: Retrieve a customer via REST
	Given this existing customer
	| Title | Name          | Address Line 1 | Address Line 2 | Address Line 3 | Postcode | Home Phone   | Mobile       | Account Invoicing |
	| Miss  | Holly Henshaw | 101 Highfields | Duston         | Northampton    | NN5 6HH  | 01604 789456 | 07777 987654 | Yes               |

	When I request the customer resource via REST

	Then I should receive an HTTP 200 OK response
	And I should receive the full details of the customer
