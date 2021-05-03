### Repository structure

AutoWorkshop.DomainModel - sample domain model to write scenarios against directly.

AutoWorkshop.Specs - sample acceptance test suite of WebDriver and domain model tests.

### SpecFlow test suite principles

- Provide database and other infrastructure access via stateless repositories and services.
- Retain all SpecFlow types and parsing of SpecFlow parameters within step files.
- Avoid coupling steps to infrastructure methods. Pass DTOs or primitive types between them.
- Only perform assertions in steps.
- Only perform polling for eventually consistent values in steps, not infrastructure methods.
- Use hooks very sparingly and with explicit method names and file locations.
- Avoid sharing state between step files.
- When state is shared between steps in the same file, use local member variables, not injected types.
- Where state is shared between steps, assert the state with an explicit guard in dependent steps.

### Stateless v stateful step files philosophy

When developing scenarios, the preference is for highly readable scenarios without repetition. e.g.

```
@WebTest
Scenario: Create new customer
	Given the following customer details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Phone        |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 07575 456789 |

	When I create the customer in AutoWorkshop

	Then the customer is added to the system with the details provided
	And the customer is marked as manually invoiced
```

To aid readability some state has to be maintained in step files and ideally the Gherkin and steps are written by the same person. As complexity increases it may be necessary to scope step files to specific feature files to maintain readable scenarios, moving common parts of user journeys into separate, stateless classes. Infrastructure access should already be stateless and highly reusable.

As an alternative where maximum step re-usability is needed, perhaps where scenarios are to be written by non-programmer QAs, the Gherkin can be rewritten to pass much of the state to the steps. This is more verbose and less readable, but allows QAs to compose steps more easily. e.g.

```
@WebTest
Scenario: Create new customer
	Given there are no customers named 'Jane Jones'

	When I create a new customer in AutoWorkshop with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Phone        |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 07575 456789 |

	Then a customer is present in the system with the following details
 	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode | Phone        |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  | 07575 456789 |
  
	And customer 'Jane Jones' is marked as manually invoiced
```
