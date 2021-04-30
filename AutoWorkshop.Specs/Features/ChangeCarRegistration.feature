Feature: Change Car Registration
	Change registration for a given car.

@WebTest
Scenario: Change car registration
	Given this existing car
	| Registration | Make        | Model |
	| AY16CPD      | Range Rover | Vogue |
	And there is no existing car with registration 'V8RVR'
	
	When I change the registration of 'AY16CPD' to 'V8RVR'

	Then I should see the success message 'AY16CPD successfully changed to V8RVR'
	And the following car should be present in the system
	| Registration | Make        | Model |
	| V8RVR        | Range Rover | Vogue |
	And there should be no car with registration 'AY16CPD'


@WebTest
Scenario: Cannot change car registration where proposed registration already in use
	Given these existing cars
	| Registration | Make        | Model |
	| AY16CPD      | Range Rover | Vogue |
	| V8RVR        | Overfinch   | 500   |
	
	When I change the registration of 'AY16CPD' to 'V8RVR'

	Then I should see the error message 'Error - registration V8RVR already held in database'
	And the following cars should be present in the system
	| Registration | Make        | Model |
	| AY16CPD      | Range Rover | Vogue |
	| V8RVR        | Overfinch   | 500   |
