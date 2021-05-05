Feature: Scenario Variables Example
	*** Illustration of 'scenario variables' which can help non-programming QAs retrieve and work with system-generated values. ***
	*** This scenario is using largely stateless step files to allow maximum step reusability by non-programming QAs. ***

Scenario: User journey to create a new customer with a new car
	Given there are no customers named 'Melanie Morgan'
	And there is no existing car or jobs for registration 'V8MEL'

	When I create a new customer with the following details
	| Title | Name           | Address Line 1  | Address Line 2  | Address Line 3 | Postcode | Home Phone   | Mobile        |
	| Miss  | Melanie Morgan | 100 High Street | Chipping Norton | Oxfordshire    | OX15 2YH | 01865 715621 | 07779 5647889 |

	Then a customer is present in the system with the following details
	| Title | Name           | Address Line 1  | Address Line 2  | Address Line 3 | Postcode | Home Phone   | Mobile        |
	| Miss  | Melanie Morgan | 100 High Street | Chipping Norton | Oxfordshire    | OX15 2YH | 01865 715621 | 07779 5647889 |
	And customer 'Melanie Morgan' is marked as manually invoiced

	# set the scenario variable
	Given '<CustomerId>' is the customer ID of 'Melanie Morgan' 

	# use the scenario variable as a parameter to the step
	When I create a new car for customer '<CustomerId> with the following details
	| Registration | Make    | Model       | Year |
	| V8MEL        | Bentley | Continental | 2010 |
	
	# use the scenario variable as a value in the table
	Then a car is present in the system with the following details
	| Registration | Customer     | Make    | Model       | Year |
	| V8MEL        | <CustomerId> | Bentley | Continental | 2010 |

