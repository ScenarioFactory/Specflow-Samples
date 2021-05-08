Feature: Issue MOT Reminders
	Issue MOT reminders in bulk.
	Uses Azure Service Bus and Blob Storage

@ServiceBus @BlobStorage
Scenario: Create MOT Reminders for certificates expiring in the next 21 days
	Given the following customers
	| Title | Name           | Address Line 1   | Address Line 2  | Address Line 3 | Postcode | Home Phone    | Mobile        |
	| Mrs   | Jane Jones     | 72 Acacia Avenue | Shepherds Bush  | London         | W12 8QT  | 0121 756 2584 | 07575 456789  |
	| Miss  | Melanie Morgan | 100 High Street  | Chipping Norton | Oxfordshire    | OX15 2YH | 01865 715621  | 07779 5647889 |
	And the following cars
	| Registration | Make        | Model       | Customer       | MOT Expiry | Suppress MOT Reminder | Note                      |
	| AY16CPD      | Range Rover | Vogue       | Jane Jones     | 10/05/2021 | No                    | MOT expiry within 21 days |
	| MGB1J        | MG          | MGB         | Jane Jones     | 10/05/2021 | Yes                   | MOT reminder suppressed   |
	| V8MEL        | Bentley     | Continental | Melanie Morgan | 15/05/2021 | No                    | MOT expiry within 21 days |
	| F1LAM        | Lamborghini | Countach    | Melanie Morgan | 22/05/2022 | No                    | MOT expiry over 21 days   |
	And there have been no MOT Reminders issued
	And the date is '01/05/2021'

	When I issue MOT Reminders

	Then the following MOT Reminders should be issued
	| Registration | MOT Expiry | Make        | Model       | Title | Name           | Address Line 1   | Address Line 2  | Address Line 3 | Postcode |
	| AY16CPD      | 10/05/2021 | Range Rover | Vogue       | Mrs   | Jane Jones     | 72 Acacia Avenue | Shepherds Bush  | London         | W12 8QT  |
	| V8MEL        | 15/05/2021 | Bentley     | Continental | Miss  | Melanie Morgan | 100 High Street  | Chipping Norton | Oxfordshire    | OX15 2YH |
Scenario: Continues..
	And the following MOT Reminder documents have been generated
	| Registration |
	| AY16CPD      |
	| V8MEL        |
