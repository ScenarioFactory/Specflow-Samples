Feature: CreateJobs
	Create workshop jobs to work on cars.

@EndToEndTest @WebTest
Scenario: User journey to book in a new customer with a new car, create a job and raise an invoice
	Given there are no customers named 'Melanie Morgan'
	And there is no existing car with registration 'V8MEL'

	# create new customer
	When I create a new customer with the following details
	| Title | Name           | Address Line 1  | Address Line 2  | Address Line 3 | Postcode | Home Phone   | Mobile        |
	| Miss  | Melanie Morgan | 100 High Street | Chipping Norton | Oxfordshire    | OX15 2YH | 01865 715621 | 07779 5647889 |

	Then the customer is added to the system with the details provided
	And the customer is marked as manually invoiced

	# create new car
	When I select the option to create a new car for the customer
	And I create a new car for customer 'Melanie Morgan' with the following details
	| Registration | Make    | Model       | Year |
	| V8MEL        | Bentley | Continental | 2010 |
	
	Then the car is added to the system with the details provided

	# create new job
	When I select the option to create a new job for the car
	And I create the following job for car 'V8MEL'
	| Description              | Date       | Hours | Mileage |
	| Full service and cambelt | 30/04/2021 | 6.5   | 42500   |

	Then the job is added to the system with the details provided

