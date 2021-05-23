Feature: Manage Cars via REST API

@RestApi
Scenario: Create a new car via REST
	Given there is no car with registration 'V8RVR'
	And the following existing customer
	| Title | Name          |
	| Miss  | Holly Henshaw |

	When I create a new car resource with the following details
	| Registration | Customer      | Make        | Model | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Range Rover | Vogue | 31/12/2021 | No                    |

	Then I should receive an HTTP 201 Created response
	And I should receive the location of the created resource
	And the car should be added to the system with the details provided

@RestApi
Scenario: Retrieve a car via REST
	Given the following existing customer
	| Title | Name          |
	| Miss  | Holly Henshaw |
	And the following existing car
	| Registration | Customer      | Make        | Model | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Range Rover | Vogue | 31/12/2021 | No                    |

	When I request the car resource

	Then I should receive an HTTP 200 OK response
	And the response should be in JSON format
	And I should receive the full details of the car

@RestApi
Scenario: Update a car via REST
	Given the following existing customer
	| Title | Name          |
	| Miss  | Holly Henshaw |
	And the following existing car
	| Registration | Customer      | Make        | Model | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Range Rover | Vogue | 31/12/2021 | No                    |

	When I update the car resource with the following changes
	| Model    | MOT Expiry |
	| Vogue SE | 31/12/2022 |

	Then I should receive an HTTP 204 No Content response
	And the changes should be made to the car in the system

@RestApi
Scenario: Delete a car via REST
	Given the following existing customer
	| Title | Name          |
	| Miss  | Holly Henshaw |
	And the following existing car
	| Registration | Customer      | Make        | Model | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Range Rover | Vogue | 31/12/2021 | No                    |

	When I delete the car resource

	Then I should receive an HTTP 204 No Content response
	And the car should be removed from the system

@RestApi
Scenario: Attempting to retrieve a non existent car via REST returns Not Found
	Given there is no car with registration 'V8RVR'

	When I request a car resource with registration 'V8RVR'

	Then I should receive an HTTP 404 Not Found response

@RestApi
Scenario: Cannot create a new car with an existing registration
	Given the following existing customer
	| Title | Name          |
	| Miss  | Holly Henshaw |
	And the following existing car
	| Registration | Customer      | Make        | Model | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Range Rover | Vogue | 31/12/2021 | No                    |

	When I create a new car resource with the following details
	| Registration | Customer      | Make       | Model     | MOT Expiry | Suppress MOT Reminder |
	| V8RVR        | Holly Henshaw | Land Rover | Discovery |            | No                    |

	Then I should receive an HTTP 409 Conflict response
	And the content should contain 'Registration already in use'
	And the car should remain unchanged in the system
